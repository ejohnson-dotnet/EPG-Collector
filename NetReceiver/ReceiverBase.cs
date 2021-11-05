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
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
using System.Reflection;
using System.Text;

using DomainObjects;

namespace NetReceiver
{
    public abstract class ReceiverBase
    {
        /// <summary>
        /// Get the full assembly version number.
        /// </summary>
        public static string AssemblyVersion
        {
            get
            {
                System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
                return (version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision);
            }
        }

        public bool DataFlowing
        {
            get
            {
                if (lastSampleCount != sampleCount)
                {
                    lastSampleCount = sampleCount;
                    LogMessage(3, "DataFlowing returning true");
                    return (true);
                }
                else
                {
                    LogMessage(3, "DataFlowing returning false");
                    return (false);
                }
            }
        }

        public int BufferSpaceUsed 
        { 
            get 
            {
                LogMessage(3, "GetBufferSpaceUsed returning " + (memoryOffset - reservedMemorySize));
                return (memoryOffset - reservedMemorySize); 
            } 
        }
        
        public IntPtr BufferAddress 
        { 
            get 
            {
                LogMessage(3, "GetBufferAddress returning " + (memoryPointer != IntPtr.Zero ? memoryPointer.ToString() : "null"));
                return (memoryPointer); 
            } 
        }

        public int SyncByteSearches 
        { 
            get 
            {
                LogMessage(3, "GetSyncByteSearches returning 0");
                return (0); 
            } 
        }

        public int SamplesDropped 
        { 
            get 
            {
                LogMessage(3, "GetSamplesDropped returning " + samplesDropped);
                return (samplesDropped); 
            } 
        }

        public int BufferingErrors { get { return (bufferingErrors); } }
        
        public int MaximumSampleSize 
        { 
            get 
            {
                LogMessage(3, "GetMaximumSampleSize returning " + maximumSampleSize);
                return (maximumSampleSize); 
            } 
        }
        
        public int DumpFileSize 
        { 
            get 
            {
                LogMessage(3, "GetDumpFileSize returning " + dumpFileSize);
                return (dumpFileSize); 
            } 
        }

        public bool Initialized { get { return (initialized); } }

        private int logLevel;
        private Logger logger;

        protected Socket socket;
        protected BackgroundWorker receiveWorker;

        private bool[] pidsAllowed = new bool[8192];
        private bool allPidsAllowed;

        protected FileStream dumpWriter;
        protected int dumpBufferSize;
        protected int dumpFileSize;
        protected int dumpPacketCount = 2000;
        protected byte[] dumpBuffer;

        protected ReceiverMode receiverMode;

        protected int sampleCount;
        protected int lastSampleCount = 0;
        protected int maximumSampleSize;
        protected int samplesDropped;
        protected int bufferingErrors;
        protected int blockSequenceErrors;

        protected IntPtr memoryPointer;
        protected int memorySize;
        protected const int reservedMemorySize = 136;
        protected volatile int memoryOffset = reservedMemorySize;

        protected int packetSize = 1328;
        protected int sampleUnitSize = 188;
        protected int samplesPerPacket = 7;
        protected int receiverTimeout = 2000;

        protected byte[] inputBuffer = new byte[4096];

        protected int[] pidCounts = new int[8192];        

        protected volatile bool initialized;
        protected ReplyCode okReply = ReplyCode.OK;

        protected ReceiverBase() { }

        public ReceiverBase(int logLevel, string logFileName)
        {
            this.logLevel = logLevel;            

            if (logLevel != 0 && logFileName != null)
                this.logger = new Logger(logFileName);

            memorySize = RunParameters.Instance.BufferSize * 1024 * 1024;
        }

        public abstract ReplyCode StartReceiving(BackgroundWorker worker);

        protected ReplyCode WriteStream(byte[] buffer, int startOffset, int length)
        {
            /*LogMessage(3, "WriteStream called sample size = " + length);*/
            /*if (length == 0 || length % 188 != 0)
                return (okReply);*/
            
            sampleCount++;
            if (length > maximumSampleSize)
                maximumSampleSize = length;

            if (dumpWriter != null)
            {
                if (allPidsAllowed)
                {
                    if (dumpBufferSize + length > dumpBuffer.Length)
                        EmptyDumpBuffer(false);

                    Array.Copy(buffer, startOffset, dumpBuffer, dumpBufferSize, length);
                    dumpBufferSize += length;
                    /*LogMessage(2, "Sample added to dump buffer: size now " + dumpBufferSize);*/
                    return (okReply);
                }
            }
            else
            {
                if (memoryPointer == IntPtr.Zero)
                {
                    samplesDropped++;
                    LogMessage(2, "Sample ignored (memory buffer closed)");
                    return (okReply);
                }
            }

            int offset = startOffset;
            int bytesDone = 0;

            while (bytesDone < length)
            {
                if (buffer[offset] != 0x47)
                {
                    samplesDropped++;
                    /*LogMessage(2, "WriteStream returning - first byte of sample not 0x47");*/
                    return (okReply);
                }

                int pid = (((int)(buffer[offset + 1] & 0x1f)) << 8) | buffer[offset + 2];
                pidCounts[pid]++;

                if (pidsAllowed[pid] || allPidsAllowed)
                {
                    if (dumpWriter == null)
                    {
                        if ((memoryOffset + sampleUnitSize) < memorySize)
                        {
                            Marshal.Copy(buffer, offset, memoryPointer + memoryOffset, sampleUnitSize);
                            memoryOffset += sampleUnitSize;
                        }
                    }
                    else
                    {
                        if (dumpBufferSize + sampleUnitSize > dumpBuffer.Length)
                            EmptyDumpBuffer(false);

                        Array.Copy(buffer, offset, dumpBuffer, dumpBufferSize, sampleUnitSize);
                        dumpBufferSize += sampleUnitSize;

                        /*LogMessage(2, "Sample dumped: size " + sampleUnitSize + " bytes");*/
                    }                        
                }
                else
                {
                    samplesDropped++;
                    /*LogMessage(2, "Sample ignored - pid not requested (got " + pid + ")");*/
                }                

                offset += sampleUnitSize;
                bytesDone += sampleUnitSize;
            }

            if (dumpWriter == null)
                Marshal.WriteInt32(memoryPointer, memoryOffset - reservedMemorySize);
            
            /*LogMessage(3, "WriteStream returning");*/
            return (okReply);
        }

        protected void EmptyDumpBuffer(bool close)
        {
            if (dumpWriter == null || dumpBufferSize == 0)
                return;

            dumpWriter.Write(dumpBuffer, 0, dumpBufferSize);
            dumpFileSize += dumpBufferSize;

            LogMessage(2, "Dump file written: " + dumpBufferSize +
                    " bytes dump file now " + dumpFileSize);

            dumpBufferSize = 0;

            if (close)
                dumpWriter.Close();
        }

        public ReplyCode Terminate(bool release)
        {
            LogMessage(3, "Terminate called - release is " + release);

            if (release)
            {
                receiveWorker.CancelAsync();
                while (receiveWorker.IsBusy)
                    Thread.Sleep(500);
            }

            if (socket != null)
            {
                LogMessage(2, "Terminate closing socket");
                socket.Close();
                socket = null;
            }

            if (bufferingErrors != 0)
                LogMessage(1, "Buffering errors = " + bufferingErrors);

            LogMessage(2, "Sample count = " + sampleCount +
                " Max sample size = " + maximumSampleSize);

            if (release && memoryPointer != IntPtr.Zero)
            {
                LogMessage(2, "Terminate closing shared memory");
                Marshal.FreeHGlobal(memoryPointer);                
            }

            if (dumpWriter != null)
            {
                LogMessage(2, "Closing dump file");
                dumpWriter.Close();
                dumpWriter = null;
            }

            LogMessage(3, "Terminate returning");
            return (ReplyCode.OK);
        }

        public void ClearMemoryBuffer()
        {
            LogMessage(3, "ClearMemoryBuffer called");

            if (memoryPointer != IntPtr.Zero)
            {
                memoryOffset = reservedMemorySize;
                Marshal.WriteInt32(memoryPointer, 0);
            }
        }

        public void ClearPids()
        {
            LogMessage(3, "ClearPids called");            
            allowAllPids();
        }

        private void allowAllPids()
        {
            for (int index = 0; index < pidsAllowed.Length; index++ )
                pidsAllowed[index] = false;

            allPidsAllowed = true;            
        }

        public void AddPid(int pid)
        {
            LogMessage(3, "AddPid called for pid " + pid);

            if (pid == -1)
            {
                allowAllPids();                
                LogMessage(3, "AddPid returning - pids all set to allowed");
                return;
            }

            if (pid  < 0 || pid > 8191)
            {
                LogMessage(3, "AddPid returning - pid out of range (0-8191)");
                return;
            }

            pidsAllowed[pid] = true;
            allPidsAllowed = false;
            
            LogMessage(3, "AddPid returning - pid added");
        }

        public void SetPids(int[] pids)
        {
            StringBuilder pidString = new StringBuilder();

            foreach (int pid in pids)
            {
                if (pidString.Length != 0)
                    pidString.Append(",");
                pidString.Append(pid);
            }

            LogMessage(3, "SetPids called for pids " + pidString);

            ClearPids();

            foreach (int pid in pids)
            {
                if (pid < 0 || pid > 8191)
                    LogMessage(3, "SetPids ignored pid " + pid + " - pid out of range (0-8191)");
                else
                {
                    pidsAllowed[pid] = true;
                    allPidsAllowed = false;
                }
            }

            LogMessage(3, "SetPids returning");
        }

        protected void LogMessage(int logLevel, string message)
        {
            if (logger == null || this.logLevel < logLevel)
                return;

            logger.Write(message); 
        }

        protected void LogDump(int logLevel, string heading, byte[] buffer, int offset, int length)
        {
            if (logger == null || this.logLevel < logLevel)
                return;

            logger.Dump(heading, buffer, offset, length);
        }
    }
}

