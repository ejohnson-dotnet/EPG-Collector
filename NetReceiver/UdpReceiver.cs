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
using System.Collections.ObjectModel;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;

using DomainObjects;

namespace NetReceiver
{    
    public class UdpReceiver : ReceiverBase
    {
        private int portFilter;

        private BackgroundWorker messageWorker;
        private BlockingCollection<MessageSpec> messageQueue = new BlockingCollection<MessageSpec>();
        
        private byte[] receiveBuffer;                
        private int activeRequests = 250;

        private int packetsReceived;
        private long bytesReceived;
        private int messagesProcessed;
                                        
        private DateTime startTime;
        private DateTime endTime;

        private object lockObject = new object();

        private UdpReceiver() { }

        public UdpReceiver(int logLevel, string logFileName) : base(logLevel, logFileName) 
        {
            LogMessage(3, "UDP constructor returning");
        }

        public ReplyCode Initialize(ReceiverMode receiverMode, string multicastAddress, int port, string multicastSource, int multicastPort, string dumpFileName)
        {
            LogMessage(3, "Initialize UDP called - mode is " + receiverMode);

            base.receiverMode = receiverMode;
            portFilter = multicastPort;            

            LogMessage(2, "Initialize UDP creating receiver socket");
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            if (receiverMode == ReceiverMode.MulticastRtp || receiverMode == ReceiverMode.MulticastUdp)
            {
                LogMessage(2, "Initialize UDP setting receiver socket to reused");
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            }

            LogMessage(2, "Initialize UDP binding socket to any address port " + port);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            try
            {
                socket.Bind(endPoint);
            }
            catch (SocketException e)
            {
                LogMessage(1, "Initialize UDP returning - bind failed");
                LogMessage(1, e.Message);
                return (ReplyCode.BindError);
            }

            if (receiverMode == ReceiverMode.MulticastRtp || receiverMode == ReceiverMode.MulticastUdp)
            {
                if (multicastSource == null)
                {
                    LogMessage(2, "Initialize UDP joining multicast group " + multicastAddress);
                    IPAddress address = IPAddress.Parse(multicastAddress);
                    MulticastOption option = new MulticastOption(address, IPAddress.Any);
                    try
                    {
                        socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, option);
                    }
                    catch (SocketException e)
                    {
                        LogMessage(1, "Initialize UDP returning - join multicast group failed");
                        LogMessage(1, e.Message);
                        return (ReplyCode.JoinMulticastGroupError);
                    }
                }
                else
                {
                    LogMessage(2, "Initialize UDP joining multicast source group " + multicastAddress +
                        " source is " + multicastSource);
                    IPAddress address1 = IPAddress.Parse(multicastAddress);
                    IPAddress address2 = IPAddress.Parse(multicastSource);
                    MulticastOption option = new MulticastOption(address1, address2);
                    try
                    {
                        socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddSourceMembership, option);
                    }
                    catch (SocketException e)
                    {
                        LogMessage(1, "Initialize UDP returning - join multicast source group failed");
                        LogMessage(1, e.Message);
                        return (ReplyCode.JoinMulticastSourceError);
                    }
                }
            }

            if (dumpFileName == null)
            {
                LogMessage(2, "Initialize UDP creating shared memory");
                memoryPointer = Marshal.AllocHGlobal(memorySize);
            }
            else
            {
                LogMessage(2, "Initialize UDP creating dump file");
                dumpWriter = new FileStream(dumpFileName, FileMode.Create, FileAccess.Write);
                dumpBuffer = new byte[samplesPerPacket * sampleUnitSize * dumpPacketCount];
            }

            ClearPids();
            initialized = true;

            LogMessage(3, "Initialize UDP returning - ok");
            return (ReplyCode.OK);
        }

        public override ReplyCode StartReceiving(BackgroundWorker receiveWorker)
        {
            LogMessage(3, "StartReceiving UDP called");

            this.receiveWorker = receiveWorker;

            messageWorker = new BackgroundWorker();
            messageWorker.DoWork += new DoWorkEventHandler(messageWorkerDoWork);
            messageWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(messageWorkerRunWorkerCompleted);
            messageWorker.WorkerSupportsCancellation = true;
            messageWorker.RunWorkerAsync();

            Thread.Sleep(1000);

            socket.ReceiveTimeout = receiverTimeout;

            receiveBuffer = new byte[packetSize * activeRequests];
            Collection<SocketAsyncEventArgs> activeEvents = new Collection<SocketAsyncEventArgs>();

            while (activeEvents.Count < activeRequests)
            {
                SocketAsyncEventArgs eventArgs = new SocketAsyncEventArgs();
                eventArgs.SetBuffer(receiveBuffer, activeEvents.Count * packetSize, packetSize);
                eventArgs.RemoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                eventArgs.AcceptSocket = socket;
                eventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(inputCompleted);
                eventArgs.UserToken = activeEvents.Count;

                activeEvents.Add(eventArgs);
            }

            startTime = DateTime.Now;

            foreach (SocketAsyncEventArgs eventArgs in activeEvents)
            {
                try
                {
                    bool reply = socket.ReceiveFromAsync(eventArgs);
                    if (!reply)
                        inputCompleted(null, eventArgs);
                }
                catch (SocketException e)
                {
                    LogMessage(1, "StartReceiving UDP returning - receive from async failed");
                    LogMessage(1, e.Message);
                    return (ReplyCode.ReceiveError);
                }
            }

            while (!receiveWorker.CancellationPending)
                Thread.Sleep(1000);

            LogMessage(3, "UDP receiver thread cancelled");            

            socket.Close();

            MessageSpec endMessage = new MessageSpec(true);
            messageQueue.Add(endMessage);

            Thread.Sleep(1000);

            while (messageWorker.IsBusy)
            {
                Logger.Instance.Write("UDP receiver waiting for message worker to finish");
                Thread.Sleep(1000);
            }
           
            endTime = DateTime.Now;

            if (dumpWriter != null)
            {
                EmptyDumpBuffer(true);
                LogMessage(2, "UDP receiver dump file last block written: dump file now " +
                    dumpFileSize + " bytes");
            }

            Logger.Instance.WriteSeparator("Udp Receiver Statistics");

            long bitsReceived = bytesReceived * 8;
            double totalSeconds = (endTime - startTime).TotalSeconds;

            Logger.Instance.Write("Udp packets received = " + packetsReceived +
                " bytes received = " + bytesReceived);
            Logger.Instance.Write("Udp packets/sec = " + (packetsReceived / totalSeconds).ToString("0.000") +
                " Mbits/sec = " + ((bitsReceived / totalSeconds) / 1000000).ToString("0.000"));
            Logger.Instance.Write("Udp messages processed = " + messagesProcessed +
                " memory buffer used = " + memoryOffset +
                " (" + ((((decimal)memoryOffset) * 100) / memorySize).ToString("0.000") + "%)");

            int totalPids = 0;

            foreach (int pidCount in pidCounts)
                totalPids += pidCount;

            for (int index = 0; index < pidCounts.Length; index++)
            {
                if (pidCounts[index] != 0)
                    Logger.Instance.Write("Udp PID " + index.ToString("0000") + " 0x" + index.ToString("x4") + " count = " + pidCounts[index] +
                        " (" + ((((decimal)pidCounts[index]) * 100) / totalPids).ToString("0.000") + "%)");

            }

            LogMessage(3, "UDP receiver returning");
            return (ReplyCode.OK);
        }

        private void inputCompleted(object sender, SocketAsyncEventArgs e)
        {
            Monitor.Enter(lockObject);

            if (e.SocketError != SocketError.Success)
            {
                LogMessage(1, "Input completed with error " + e.SocketError);
                if (e.SocketError == SocketError.OperationAborted)
                {
                    Monitor.Exit(lockObject);
                    return;
                }
            }

            Byte[] message = new byte[e.BytesTransferred];
            Array.Copy(e.Buffer, e.Offset, message, 0, e.BytesTransferred);            
            messageQueue.Add(new MessageSpec(message, 0, e.BytesTransferred, e.RemoteEndPoint, (int)e.UserToken));
                
            packetsReceived++;
            bytesReceived += e.BytesTransferred;

            try
            {
                bool reply = e.AcceptSocket.ReceiveFromAsync(e);
                if (!reply)
                {
                    Monitor.Exit(lockObject);
                    inputCompleted(null, e);
                    return;
                }
            }
            catch (ObjectDisposedException)
            {
                Monitor.Exit(lockObject);
                return;
            }
            catch (SocketException ex)
            {
                LogMessage(1, "StartReceiving UDP returning - receive from async failed");
                LogMessage(1, ex.Message);
            }
            catch (ArgumentOutOfRangeException ae)
            {
                LogMessage(1, "StartReceiving UDP returning - receive from async failed");
                LogMessage(1, ae.Message);
            }

            Monitor.Exit(lockObject);
        }

        private void messageWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker == null)
                throw (new ArgumentException("Message worker thread has been started with an incorrect sender"));

            if (RunParameters.IsWindows)
                Thread.CurrentThread.Name = "UDP Message Worker Thread";
            Thread.CurrentThread.Priority = ThreadPriority.Normal;

            MessageSpec inputMessage;
            bool finished = false;
                                    
            LogMessage(3, "Starting message processing loop");

            do
            {
                inputMessage = messageQueue.Take();

                if (!inputMessage.EndMessage)
                {
                    messagesProcessed++;
                    
                    if (inputMessage.Length == 1320)
                    {
                        int blockNumber = (inputMessage.Buffer[inputMessage.Offset + 1316] << 24) | (inputMessage.Buffer[inputMessage.Offset + 1317] << 16) |
                        (inputMessage.Buffer[inputMessage.Offset + 1318] << 8) | inputMessage.Buffer[inputMessage.Offset + 1319];

                        Logger.Instance.Write("Got " + inputMessage.Offset + " seq " + blockNumber + " receiver " + inputMessage.ReceiverNumber);                        
                    }

                    if (portFilter == 0 || portFilter == ((IPEndPoint)inputMessage.RemoteEndPoint).Port)
                    {
                        switch (receiverMode)
                        {
                            case ReceiverMode.SatIp:
                            case ReceiverMode.MulticastRtp:
                            case ReceiverMode.UnicastRtp:
                                int sourceCount = inputMessage.Buffer[inputMessage.Offset + 0] & 0x0f;
                                bool extHeaderPresent = (inputMessage.Buffer[inputMessage.Offset + 0] & 0x10) != 0;
                                int offset = 12 + (sourceCount * 4);

                                if (extHeaderPresent)
                                {
                                    int headerLength = (inputMessage.Buffer[inputMessage.Offset + offset + 2] * 256) +
                                        inputMessage.Buffer[inputMessage.Offset + offset + 3];
                                    offset += headerLength + 4;
                                }

                                if (inputMessage.Length > offset)
                                    WriteStream(inputMessage.Buffer, inputMessage.Offset + offset, inputMessage.Length - offset);
                                break;
                            case ReceiverMode.MulticastUdp:
                            case ReceiverMode.UnicastUdp:
                                WriteStream(inputMessage.Buffer, inputMessage.Offset, inputMessage.Length);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                        LogMessage(2, "UDP message worker filtered data out - expected from port " + portFilter +
                            " came from " + ((IPEndPoint)inputMessage.RemoteEndPoint).Port); 
                }
                else
                {
                    LogMessage(2, "UDP message worker found closedown message");
                    finished = true;
                }
            }
            while (!finished);

            e.Cancel = true;
            LogMessage(2, "UDP message worker returning");
        }

        private static void messageWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw new InvalidOperationException("Message background worker failed - see inner exception", e.Error);
        }        
    }
}