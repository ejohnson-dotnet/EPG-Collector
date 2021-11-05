////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2020 nzsjb                                           //
//                                                                              //
//  This Program is free software; you can redistribute it and/or modify        //
//  it under the terms of the GNU General Public License as published by        //
//  the Free Software Foundation; either version 2, or (at your option)         //
//  any later version.                                                          //
//                                                                              //
//  This Program is distributed in the hope that it will be useful,             //
//  but WITHOUT ANY WARRANTY; without even the implied warranty of              //
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                //
//  GNU General Public License for more details.                                //
//                                                                              //
//  You should have received a copy of the GNU General Public License           //
//  along with GNU Make; see the file COPYING.  If not, write to                //
//  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.       //
//  http://www.gnu.org/copyleft/gpl.html                                        //
//                                                                              //  
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

using DomainObjects;

namespace NetReceiver
{
    public class HttpReceiver : ReceiverBase
    {
        private string hostName;
        private string addressUsed;
        private int portUsed;
        private string httpPath;

        private int packetsReceived;
        private long bytesReceived;        

        private DateTime startTime;
        private DateTime endTime;

        private byte[] storedBlock = new byte[0];	
        private volatile bool inputStreamEmpty;

        private HttpReceiver() { }

        public HttpReceiver(int logLevel, string logFileName) : base(logLevel, logFileName) 
        {
            LogMessage(1, "HTTP receiver log level is " + logLevel);
            LogMessage(3, "HTTP constructor returning");
        }

        public ReplyCode Initialize(string serverName, string remoteAddress, int remotePort, string path, string dumpFileName, bool first)
        {
            if (first)
                LogMessage(3, "Initialize HTTP called for first time - path is " + path);
            else
                LogMessage(3, "Initialize HTTP called to continue - path is " + path);

            receiverMode = ReceiverMode.Http;

            hostName = serverName;
            addressUsed = remoteAddress;
            portUsed = remotePort;
            httpPath = path;

            LogMessage(2, "Initialize HTTP creating receiver socket");
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);            

            LogMessage(2, "Initialize HTTP connecting to " + remoteAddress + " port " + remotePort);

            try
            {
                socket.Connect(remoteAddress, remotePort);
            }
            catch (SocketException e)
            {
                LogMessage(1, "Initialize HTTP returning - connect failed");
                LogMessage(1, e.Message);
                return (ReplyCode.ServerConnectError);
            }
            
            if (first)
            {
                LogMessage(2, "Initialize HTTP creating shared memory");
                memoryPointer = Marshal.AllocHGlobal(memorySize);
                ClearPids();
            }

            if (dumpFileName != null)
            {
                if (first)
                {
                    LogMessage(2, "Initialize HTTP creating dump file");
                    dumpWriter = new FileStream(dumpFileName, FileMode.Create, FileAccess.Write);                    
                }
                else
                {
                    LogMessage(2, "Initialize HTTP appending to dump file");
                    dumpWriter = new FileStream(dumpFileName, FileMode.Append, FileAccess.Write);
                }

                dumpBuffer = new byte[samplesPerPacket * sampleUnitSize * dumpPacketCount];

                LogMessage(2, "Initialize HTTP created/opened dump file");
                dumpBufferSize = 0;
            }

            initialized = true;

            LogMessage(3, "Initialize HTTP returning - ok");
            return (ReplyCode.OK);
        }

        public override ReplyCode StartReceiving(BackgroundWorker receiveWorker)
        {
            LogMessage(3, "StartReceiving HTTP called");

            this.receiveWorker = receiveWorker;
	
	        LogMessage(2, "StartReceiving HTTP sending GET message to " + addressUsed +" port " + portUsed + " server " + hostName);
            LogMessage(2, "StartReceiving HTTP path is " + httpPath);

            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            streamWriter.WriteLine("GET " + httpPath + " HTTP/1.1");
            streamWriter.WriteLine("Host: " + hostName + ":" + portUsed);
            streamWriter.WriteLine("User-Agent: EPG Collector");
            streamWriter.WriteLine("Range: bytes=0-");
            streamWriter.WriteLine("Connection: close");
            streamWriter.WriteLine("");
            streamWriter.Close();

            byte[] requestMessage = memoryStream.ToArray();

            try
            {
                EndPoint remoteEndPoint = (EndPoint)new IPEndPoint(IPAddress.Parse(addressUsed), portUsed);
                socket.SendTo(requestMessage, remoteEndPoint);
            }
            catch (SocketException e)
            {
                LogMessage(1, "StartReceiving HTTP returning - send to failed");
                LogMessage(1, e.Message);
                return (ReplyCode.GetRequestError);
            }
            catch (FormatException e)
            {
                LogMessage(1, "StartReceiving HTTP returning - send to failed");
                LogMessage(1, e.Message);
                return (ReplyCode.GetRequestError);
            }

	        sampleCount = 0;
	        lastSampleCount = 0;
	        maximumSampleSize = 0;
	        samplesDropped = 0;
            startTime = DateTime.Now;

            int receivedLength = -1;

            socket.ReceiveTimeout = receiverTimeout;
            LogMessage(1, "StartReceiving HTTP receive timeout set to " + socket.ReceiveTimeout + " msecs");
	
            while (!receiveWorker.CancellationPending)
	        {
                try
                {
                    receivedLength = socket.Receive(inputBuffer);
                    
                    ReplyCode processReply = processReceivedData(inputBuffer, receivedLength);
                    if (processReply != ReplyCode.OK)
                        return processReply;

                    inputStreamEmpty = false;
                }
                catch (SocketException e)
                {
                    LogMessage(1, "StartReceiving HTTP returning - receive failed - error code " + e.ErrorCode);
                    LogMessage(1, e.Message);

                    if (e.ErrorCode != 10060)
                    {
                        EmptyDumpBuffer(true);
                        Terminate(false);
                        return ReplyCode.ReceiveError;
                    }
                    else
                        inputStreamEmpty = true;
                }
			}

            endTime = DateTime.Now;

            if (dumpWriter != null)
            {
                EmptyDumpBuffer(true);
                LogMessage(2, "HTTP receiver dump file last block written: dump file now " + dumpFileSize + " bytes");
            }                

	        Terminate(false);

            Logger.Instance.WriteSeparator("Http Receiver Statistics");

            long bitsReceived = bytesReceived * 8;
            double totalSeconds = (endTime - startTime).TotalSeconds;

            Logger.Instance.Write("Http packets received = " + packetsReceived +
                " bytes received = " + bytesReceived);
            Logger.Instance.Write("Http packets/sec = " + (packetsReceived / totalSeconds).ToString("0.000") +
                " Mbits/sec = " + ((bitsReceived / totalSeconds) / 1000000).ToString("0.000"));
            Logger.Instance.Write("Http memory buffer used = " + memoryOffset +
                " (" + ((((decimal)memoryOffset) * 100) / memorySize).ToString("0.000") + "%)");

            int totalPids = 0;

            foreach (int pidCount in pidCounts)
                totalPids += pidCount;

            for (int index = 0; index < pidCounts.Length; index++)
            {
                if (pidCounts[index] != 0)
                    Logger.Instance.Write("Http PID " + index.ToString("0000") + " 0x" + index.ToString("x4") + " count = " + pidCounts[index] +
                        " (" + ((((decimal)pidCounts[index]) * 100) / totalPids).ToString("0.000") + "%)");

            }

	        LogMessage(3, "StartReceiving HTTP returning");
	        return(ReplyCode.OK);
        }

        private ReplyCode processReceivedData(byte[] inputBuffer, int receivedLength)
        {
            LogMessage(2, "StartReceiving HTTP processing " + receivedLength + " bytes");            

            if (receivedLength == 0)
                return ReplyCode.OK;

            LogDump(5, "Receive buffer", inputBuffer, 0, receivedLength);

            packetsReceived++;
            bytesReceived += receivedLength;

            int startIndex = 0;
            bool endFound = false;

            if (inputBuffer[0] == 'H' && inputBuffer[1] == 'T' && inputBuffer[2] == 'T' && inputBuffer[3] == 'P')
            {
                LogMessage(4, "StartReceiving HTTP response received");

                for (; startIndex < receivedLength && !endFound; startIndex++)
                {
                    if (inputBuffer[startIndex] == 0x0d && inputBuffer[startIndex + 1] == 0x0a &&
                        inputBuffer[startIndex + 2] == 0x0d && inputBuffer[startIndex + 3] == 0x0a)
                    {
                        startIndex += 3;
                        endFound = true;
                    }
                }

                if (endFound)
                {
                    if (startIndex < receivedLength)
                    {
                        LogMessage(4, "StartReceiving HTTP response start index set to " + startIndex);

                        if (inputBuffer[startIndex] != 0x47)
                        {
                            LogDump(4, "Get Response", inputBuffer, startIndex, inputBuffer.Length - startIndex);
                            LogMessage(4, "StartReceiving HTTP first byte after HTTP response not 0x47");
                            bufferingErrors++;
                            EmptyDumpBuffer(true);
                            Terminate(false);
                            return (ReplyCode.ReceiveError);
                        }
                    }
                    else
                        LogMessage(4, "StartReceiving HTTP response has no extra data");
                }
                else
                    LogMessage(4, "StartReceiving HTTP response fills buffer");
            }
            else
                endFound = true;

            if (endFound && startIndex < receivedLength)
            {
                int totalLength = receivedLength + storedBlock.Length - startIndex;
                int remainder = totalLength % sampleUnitSize;
                int blockLength = totalLength - remainder;

                if (blockLength != 0)
                {
                    byte[] transferBuffer = new byte[blockLength];

                    if (storedBlock.Length != 0)
                        Array.Copy(storedBlock, transferBuffer, storedBlock.Length);
                    Array.Copy(inputBuffer, startIndex, transferBuffer, storedBlock.Length, receivedLength - remainder - startIndex);

                    for (int index = 0; index < blockLength; index += sampleUnitSize)
                    {
                        if (transferBuffer[index] != 0x47)
                        {
                            bufferingErrors++;
                            blockLength = index;
                            LogMessage(4, "StartReceiving HTTP 0x47 missing - block truncated to " + index + " bytes");
                        }
                    }

                    if (blockLength > 0)
                        WriteStream(transferBuffer, 0, blockLength);

                    if (remainder != 0)
                    {
                        storedBlock = new byte[remainder];
                        Array.Copy(inputBuffer, receivedLength - remainder, storedBlock, 0, remainder);

                        LogMessage(4, "StartReceiving HTTP saved " + storedBlock.Length + " bytes");

                        if (storedBlock[0] != 0x47)
                        {
                            bufferingErrors++;
                            EmptyDumpBuffer(true);
                            Terminate(false);
                            LogMessage(4, "StartReceiving HTTP stored block does not start with 0x47");
                            return (ReplyCode.ReceiveError);
                        }
                    }
                    else
                        storedBlock = new byte[0];
                }
                else
                {
                    byte[] temporaryBuffer = new byte[totalLength];
                    Array.Copy(storedBlock, temporaryBuffer, storedBlock.Length);
                    Array.Copy(inputBuffer, startIndex, temporaryBuffer, storedBlock.Length, receivedLength);

                    storedBlock = new byte[totalLength];
                    Array.Copy(temporaryBuffer, storedBlock, totalLength);

                    LogMessage(4, "StartReceiving HTTP combined and saved " + totalLength + " bytes");
                }
            }

            return ReplyCode.OK;
        }

        public void WaitForEmptyStream()
        {
            while (!inputStreamEmpty)
                Thread.Sleep(100);
        }
    }
}

