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

/*using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;

using DomainObjects;

using UPnP;
using Rtp;
using Sdp;
using Rtsp;
using SatIpDomainObjects;

namespace SatIp
{
    /// <summary>
    /// The class that describes the stream controller.
    /// </summary>
    public unsafe class StreamController : ISampleDataProvider
    {
        /// <summary>
        /// Get the protocol identification.
        /// </summary>
        public static string StreamProtocolId { get { return ("Stream"); } }

        /// <summary>
        /// Get the current tuning frequency.
        /// </summary>
        public TuningFrequency Frequency { get { return (currentFrequency); } }

        /// <summary>
        /// Get the buffer spaces used receiving data.
        /// </summary>
        public int BufferSpaceUsed
        {
            get
            {
                if (receiver != null)
                    return (receiver.GetBufferSpaceUsed());
                else
                    return (0);
            }
        }

        /// <summary>
        /// Get the buffer address.
        /// </summary>
        public IntPtr BufferAddress
        {
            get
            {
                if (receiver != null)
                    return ((IntPtr)receiver.GetBufferAddress());
                else
                    return (new IntPtr(0));
            }
        }

        /// <summary>
        /// Get the count of sync byte searches.
        /// </summary>
        public int SyncByteSearches
        {
            get
            {
                if (receiver != null)
                    return (receiver.GetSyncByteSearches());
                else
                    return (0);
            }
        }

        /// <summary>
        /// Get the number of samples dropped.
        /// </summary>
        public int SamplesDropped
        {
            get
            {
                if (receiver != null)
                    return (receiver.GetSamplesDropped());
                else
                    return (0);
            }
        }

        /// <summary>
        /// Get the maximum sample size.
        /// </summary>
        public int MaximumSampleSize
        {
            get
            {
                if (receiver != null)
                    return (receiver.GetMaximumSampleSize());
                else
                    return (0);
            }
        }

        /// <summary>
        /// Get the dump file size.
        /// </summary>
        public int DumpFileSize
        {
            get
            {
                if (receiver != null)
                    return (receiver.GetDumpFileSize());
                else
                    return (0);
            }
        }

        /// <summary>
        /// Return true if data is flowing; false otherwise.
        /// </summary>
        public bool DataFlowing
        {
            get
            {
                if (receiver != null)
                    return (receiver.IsDataFlowing());
                else
                    return (false);
            }
        }

        private StreamFrequency currentFrequency;

        private BackgroundWorker dataWorker;
        private BackgroundWorker announcementWorker;

        private ManagedReceiver receiver;
        private AnnouncementPacket currentAnnouncement;

        private bool receiverInitialized;
        private int receiverError;
        private ErrorSpec httpReceiverError;

        /// <summary>
        /// Initialize a new instance of the StreamController class.
        /// </summary>
        public StreamController(string ipAddress, int portNumber)
        {
            try
            {
                UPnPMessage.MulticastAddress = IPAddress.Parse(ipAddress);
            }
            catch (Exception) { }
            UPnPMessage.MulticastPort = portNumber;
            UPnPMessage.UPnPClientPort = 29000;

            ControlPacket.CustomPacket = new AnnouncementPacket();
        }

        /// <summary>
        /// Dispose the controller.
        /// </summary>
        public void Dispose()
        {
            SatIpDomainObjects.SatIpLogger.Instance.Write("Dispose request received");

            ErrorSpec errorSpec = Stop();
            if (errorSpec != null)
                SatIpDomainObjects.SatIpLogger.Instance.Write("Dispose failed: " + errorSpec);
            else
                SatIpDomainObjects.SatIpLogger.Instance.Write("Dispose request processed successfully");
        }

        /// <summary>
        /// Change the PID mapping.
        /// </summary>
        /// <param name="pid">The pid to be mapped.</param>
        public void ChangePidMapping(int pid)
        {
            ChangePidMapping(new int[] { pid });
        }

        /// <summary>
        /// Change the PID mapping.
        /// </summary>
        /// <param name="pids">The list of PID's to be mapped.</param>
        public void ChangePidMapping(int[] pids)
        {
            while (receiver == null)
                Thread.Sleep(100);

            receiver.ClearPids();
            foreach (int pid in pids)
                receiver.AddPid(pid);
            receiver.ClearMemoryBuffer();
        }

        /// <summary>
        /// Start collecting data.
        /// </summary>
        /// <param name="server">The server to use.</param>
        /// <param name="tuningParameters">The tuning parameters.</param>
        /// <param name="dumpFileName">The file to dump the data to. Null if not used.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Run(StreamFrequency streamFrequency, string dumpFileName)
        {
            currentFrequency = streamFrequency;
            IPAddress localAddress = Utils.GetLocalIpAddress(SatIpLogger.Instance);
            if (localAddress == null)
                return (new ErrorSpec(streamFrequency.Protocol.ToString(), ErrorCode.ServiceUnavailable, 0, "Stream Controller: No IPv4 address located to set the local IP address", null));

            switch (streamFrequency.Protocol)
            {
                case StreamProtocol.Rtsp:
                    return (processRtsp(localAddress, streamFrequency, dumpFileName));
                case StreamProtocol.Rtp:
                case StreamProtocol.Udp:
                    return (processRtpUdp(localAddress, streamFrequency, dumpFileName));
                case StreamProtocol.Http:
                    return (processHttp(localAddress, streamFrequency, dumpFileName));
                default:
                    return (new ErrorSpec(streamFrequency.Protocol.ToString(), ErrorCode.NotRecognized, 0, "Protocol not recognized", null));
            }
        }

        private ErrorSpec processRtsp(IPAddress localAddress, StreamFrequency streamFrequency, string dumpFileName)
        {
            SatIpServer server = new SatIpServer();
            server.Address = streamFrequency.IPAddress;

            SatIpController controller = new SatIpController();
            return (controller.Run(server, streamFrequency.PortNumber, null, streamFrequency.Path, dumpFileName));
        }

        private ErrorSpec processRtpUdp(IPAddress localAddress, StreamFrequency streamFrequency, string dumpFileName)
        {
            SatIpLogger.Instance.Write("Local IP address set to " + localAddress);

            IPEndPoint localDataPoint = new IPEndPoint(localAddress, streamFrequency.PortNumber);

            IPEndPoint remoteDataPoint;
            string addressGroup = streamFrequency.IPAddress.ToString().Substring(0, 3);
            if (addressGroup.CompareTo("224") < 0 || addressGroup.CompareTo("239") > 0)
            {
                IPHostEntry remoteHostEntry = Dns.GetHostEntry(streamFrequency.IPAddress);
                remoteDataPoint = new IPEndPoint(StreamFrequency.GetAddress(remoteHostEntry.AddressList), streamFrequency.PortNumber);
            }
            else
                remoteDataPoint = new IPEndPoint(IPAddress.Parse(streamFrequency.IPAddress), streamFrequency.PortNumber);

            startDataThread(streamFrequency.Protocol,
                localDataPoint,
                remoteDataPoint,
                streamFrequency.MulticastSource,
                streamFrequency.MulticastPort,
                dumpFileName);

            if (streamFrequency.Protocol == StreamProtocol.Rtp)
            {
                IPEndPoint localControlPoint = new IPEndPoint(localAddress, streamFrequency.PortNumber + 1);
                IPEndPoint remoteControlPoint = new IPEndPoint(IPAddress.Parse(streamFrequency.IPAddress), streamFrequency.PortNumber + 1);
                startAnnouncementThread(localControlPoint, remoteControlPoint);
            }

            while (!receiverInitialized)
                Thread.Sleep(100);

            if (receiverError != 0)
                return (new ErrorSpec(streamFrequency.Protocol.ToString(), ErrorCode.ServerError, receiverError, translateErrorCode(receiverError), null));
            else
                return (null);
        }

        private ErrorSpec processHttp(IPAddress localAddress, StreamFrequency streamFrequency, string dumpFileName)
        {
            SatIpLogger.Instance.Write("Local IP address set to " + localAddress);

            dataWorker = new BackgroundWorker();
            dataWorker.WorkerSupportsCancellation = true;
            dataWorker.DoWork += new DoWorkEventHandler(httpDataWorkerDoWork);
            dataWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(httpDataWorkerCompleted);
            dataWorker.RunWorkerAsync(new LinkSpec(localAddress, streamFrequency, dumpFileName));

            while (!receiverInitialized)
                Thread.Sleep(100);

            if (httpReceiverError != null)
                return (httpReceiverError);
            else
                return (null);
        }

        /// <summary>
        /// Stop receiving data.
        /// </summary>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Stop()
        {
            stopDataThread();

            if (announcementWorker != null)
                stopAnnouncementThread();

            return (null);
        }

        private void startDataThread(StreamProtocol protocol, IPEndPoint localEndPoint, IPEndPoint remoteEndPoint, string multicastSource, int multicastPort, string dumpFileName)
        {
            dataWorker = new BackgroundWorker();
            dataWorker.WorkerSupportsCancellation = true;
            dataWorker.DoWork += new DoWorkEventHandler(dataWorkerDoWork);
            dataWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(dataWorkerCompleted);
            dataWorker.RunWorkerAsync(new LinkSpec(protocol, localEndPoint, remoteEndPoint, multicastSource, multicastPort, dumpFileName));
        }

        private void stopDataThread()
        {
            dataWorker.CancelAsync();
            receiver.Terminate(true);

            while (dataWorker.IsBusy)
                Thread.Sleep(500);
        }

        private void dataWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            SatIpDomainObjects.SatIpLogger.Instance.Write("Data worker thread starting");

            Thread.CurrentThread.Name = "Data Thread";
            Thread.CurrentThread.Priority = ThreadPriority.Normal;

            LinkSpec linkSpec = e.Argument as LinkSpec;
            if (linkSpec == null)
                throw (new ArgumentException("Data worker started with incorrect parameter"));

            receiver = new ManagedReceiver(4, RunParameters.StreamLogFileName);

            int mode = 0;

            string addressGroup = linkSpec.RemoteEndPoint.Address.ToString().Substring(0, 3);
            bool isMulticast = true;

            if (addressGroup.CompareTo("224") < 0 || addressGroup.CompareTo("239") > 0)
                isMulticast = false;

            if (linkSpec.Protocol == StreamProtocol.Rtp)
                mode = isMulticast ? 1 : 2;
            else
                mode = isMulticast ? 3 : 4;

            int reply = receiver.InitializeUdp(mode,
                linkSpec.RemoteEndPoint.Address.ToString(),
                linkSpec.RemoteEndPoint.Port,
                linkSpec.MulticastSource,
                linkSpec.MulticastPort,
                linkSpec.DumpFileName);
            if (reply == 0)
            {
                receiverInitialized = true;
                reply = receiver.StartReceivingUdp();
            }
            else
            {
                receiverInitialized = true;
                receiverError = reply;
                SatIpDomainObjects.SatIpLogger.Instance.Write("Receiver has failed");
            }

            SatIpDomainObjects.SatIpLogger.Instance.Write("Data worker thread stopped");
        }

        private void dataWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw new InvalidOperationException("Data worker failed - see inner exception", e.Error);
        }

        private void httpDataWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            SatIpDomainObjects.SatIpLogger.Instance.Write("HTTP data worker thread starting");

            Thread.CurrentThread.Name = "HTTP Data Thread";
            Thread.CurrentThread.Priority = ThreadPriority.Normal;

            LinkSpec linkSpec = e.Argument as LinkSpec;
            if (linkSpec == null)
                throw (new ArgumentException("HTTP data worker started with incorrect parameter"));

            IPEndPoint localDataPoint = new IPEndPoint(linkSpec.LocalAddress, linkSpec.StreamFrequency.PortNumber);
            IPEndPoint remoteDataPoint;

            try
            {
                IPHostEntry remoteHostEntry = Dns.GetHostEntry(linkSpec.StreamFrequency.IPAddress);
                if (remoteHostEntry.AddressList == null || remoteHostEntry.AddressList.Length == 0)
                {
                    SatIpDomainObjects.SatIpLogger.Instance.Write("HTTP data worker thread host entry has empty address list");
                    httpReceiverError = new ErrorSpec(StreamProtocol.Http.ToString(), ErrorCode.ServerError, 0, "Host entry has no address list", null);
                    return;
                }
                remoteDataPoint = new IPEndPoint(StreamFrequency.GetAddress(remoteHostEntry.AddressList), linkSpec.StreamFrequency.PortNumber);
            }
            catch (SocketException se)
            {
                SatIpDomainObjects.SatIpLogger.Instance.Write("<e> HTTP data worker thread - exception setting up remote end point");
                SatIpDomainObjects.SatIpLogger.Instance.Write("<e> " + se.Message);
                httpReceiverError = new ErrorSpec(StreamProtocol.Http.ToString(), ErrorCode.ServerError, (int)se.SocketErrorCode, "Exception setting up remote end point", se.Message);
                return;
            }

            receiver = new ManagedReceiver(4, RunParameters.StreamLogFileName);
            bool first = true;

            Collection<string> pathsProcessed = new Collection<string>();

            while (!((BackgroundWorker)sender).CancellationPending)
            {
                Collection<StreamFrequency> sources = new Collection<StreamFrequency>();
                ErrorSpec sourceReply = getSources(linkSpec.StreamFrequency, sources);
                if (sourceReply != null)
                {
                    receiverInitialized = true;
                    httpReceiverError = sourceReply;
                    return;
                }

                foreach (StreamFrequency sourceFrequency in sources)
                {
                    if (!((BackgroundWorker)sender).CancellationPending)
                    {
                        SatIpDomainObjects.SatIpLogger.Instance.Write("HTTP data worker thread processing " + sourceFrequency.Path);

                        if (pathsProcessed.Contains(sourceFrequency.Path))
                            SatIpDomainObjects.SatIpLogger.Instance.Write("HTTP data worker path already processed - ignored");
                        else
                        {
                            pathsProcessed.Add(sourceFrequency.Path);
                            if (pathsProcessed.Count > 10)
                                pathsProcessed.RemoveAt(0);

                            int reply = receiver.InitializeHttp(linkSpec.StreamFrequency.IPAddress,
                                remoteDataPoint.Address.ToString(),
                                remoteDataPoint.Port,
                                sourceFrequency.Path,
                                linkSpec.DumpFileName,
                                first);
                            if (reply == 0)
                            {
                                first = false;
                                receiverInitialized = true;
                                reply = receiver.StartReceivingHttp();
                            }
                            else
                            {
                                receiverInitialized = true;
                                httpReceiverError = new ErrorSpec(StreamProtocol.Http.ToString(), ErrorCode.ServerError, reply, translateErrorCode(reply), null);
                                SatIpDomainObjects.SatIpLogger.Instance.Write("HTTP Receiver has failed");
                                return;
                            }
                        }
                    }
                }
            }

            SatIpDomainObjects.SatIpLogger.Instance.Write("HTTP Data worker thread stopped");
        }

        private ErrorSpec getSources(StreamFrequency streamFrequency, Collection<StreamFrequency> sources)
        {
            if (string.IsNullOrWhiteSpace(streamFrequency.Path))
            {
                sources.Add(streamFrequency);
                return (null);
            }

            if (!streamFrequency.Path.EndsWith(".m3u") && !streamFrequency.Path.EndsWith(".m3u8"))
            {
                sources.Add(streamFrequency);
                return (null);
            }

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://" + streamFrequency.IPAddress + "/" + streamFrequency.Path);
            webRequest.UserAgent = "EPG Collector " + RunParameters.SystemVersion;
            webRequest.AddRange(0);

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);

            bool firstLine = true;
            bool extInfLine = false;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                SatIpLogger.Instance.Write("HTTP response line: " + line);

                if (firstLine)
                {
                    if (line.ToUpperInvariant() != "#EXTM3U")
                    {
                        data.Close();
                        reader.Close();
                        return (new ErrorSpec("HTTP", ErrorCode.FormatError, 0, "M3U file line 1 wrong", line));
                    }
                    else
                        firstLine = false;
                }
                else
                {
                    if (line.ToUpperInvariant().StartsWith("#EXTINF:"))
                        extInfLine = true;
                    else
                    {
                        if (extInfLine)
                        {
                            StreamFrequency newFrequency = (StreamFrequency)streamFrequency.Clone();
                            newFrequency.Path = createPath(line, streamFrequency.Path);

                            if (streamFrequency.Path.EndsWith(".m3u") && !streamFrequency.Path.EndsWith(".m3u8"))
                            {
                                ErrorSpec errorSpec = getSources(streamFrequency, sources);
                                if (errorSpec != null)
                                    return (errorSpec);
                            }
                            else
                                sources.Add(newFrequency);

                            extInfLine = false;
                        }
                    }
                }
            }

            data.Close();
            reader.Close();

            return (null);
        }

        private string createPath(string line, string basePath)
        {
            if (line.ToLowerInvariant().StartsWith("http://"))
                return (createAbsolutePath(line, basePath));

            if (line.StartsWith("../"))
                return (createRelativePath(line, basePath));

            return (line);
        }

        private string createAbsolutePath(string line, string basePath)
        {
            int index = line.IndexOf('/', 7);
            if (index == -1)
                return (line);

            return (line.Substring(index + 1));
        }

        private string createRelativePath(string line, string basePath)
        {
            if (!line.StartsWith("../"))
                return (line);

            string editedLine = line;
            string editedPath;

            int index = basePath.LastIndexOf('/');
            if (index != -1)
                editedPath = basePath.Substring(0, index);
            else
                return (line);

            while (editedLine.StartsWith("../"))
            {
                editedLine = editedLine.Substring(3);
                index = editedPath.LastIndexOf('/');
                if (index != -1)
                    editedPath = string.Empty;
            }

            return (editedPath + "/" + editedLine);
        }

        private void httpDataWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw new InvalidOperationException("HTTP Data worker failed - see inner exception", e.Error);
        }

        private void startAnnouncementThread(IPEndPoint localEndPoint, IPEndPoint remoteEndPoint)
        {
            announcementWorker = new BackgroundWorker();
            announcementWorker.WorkerSupportsCancellation = true;
            announcementWorker.DoWork += new DoWorkEventHandler(announcementWorkerDoWork);
            announcementWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(announcementWorkerCompleted);
            announcementWorker.RunWorkerAsync(new LinkSpec(localEndPoint, remoteEndPoint));
        }

        private void stopAnnouncementThread()
        {
            announcementWorker.CancelAsync();
            while (announcementWorker.IsBusy)
                Thread.Sleep(500);
        }

        private void announcementWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            SatIpDomainObjects.SatIpLogger.Instance.Write("Announcement worker thread starting");

            Thread.CurrentThread.Name = "Announcement Thread";
            Thread.CurrentThread.Priority = ThreadPriority.Normal;

            LinkSpec linkSpec = e.Argument as LinkSpec;
            if (linkSpec == null)
                throw (new ArgumentException("Announcement worker started with incorrect parameter"));

            byte[] inputBuffer = new byte[1024];

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(linkSpec.LocalEndPoint);
            socket.ReceiveTimeout = 2000;

            do { receiveAnnouncementMessage(socket, linkSpec.RemoteEndPoint, inputBuffer); }
            while (!((BackgroundWorker)sender).CancellationPending);

            socket.Close();

            SatIpDomainObjects.SatIpLogger.Instance.Write("Announcement worker thread stopped");
        }

        private void receiveAnnouncementMessage(Socket socket, EndPoint remoteEndPoint, byte[] inputBuffer)
        {
            try
            {
                int receiveCount = socket.ReceiveFrom(inputBuffer, ref remoteEndPoint);

                int offset = 0;

                while (offset < receiveCount)
                {
                    object announcement = ControlPacket.GetInstance(inputBuffer, offset);
                    ErrorSpec errorSpec = announcement as ErrorSpec;
                    if (errorSpec != null)
                        SatIpDomainObjects.SatIpLogger.Instance.Write("Announcement packet: " + errorSpec);
                    else
                    {
                        AnnouncementPacket announcementPacket = announcement as AnnouncementPacket;
                        if (announcementPacket != null)
                        {
                            SatIpDomainObjects.SatIpLogger.Instance.Write(announcementPacket.ToString());
                            currentAnnouncement = announcementPacket;
                        }
                        else
                            SatIpDomainObjects.SatIpLogger.Instance.Write("Announcement packet type " + announcement.GetType() + " received and processed");
                    }

                    offset += ((ControlPacket)announcement).Length;
                }
            }
            catch (SocketException e)
            {
                SatIpDomainObjects.SatIpLogger.Instance.Write("Announcement socket exception: " + e.Message);
            }
        }

        private void announcementWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw new InvalidOperationException("Announcement worker failed - see inner exception", e.Error);
        }

        private string translateErrorCode(int receiverError)
        {
            switch (receiverError)
            {
                case 1:
                    return ("Failed to initialize Windows Sockets subsystem");
                case 2:
                    return ("Failed to create socket");
                case 3:
                    return ("Failed to set port to multiple use");
                case 4:
                    return ("Failed to bind socket");
                case 5:
                    return ("Failed to connect to server");
                case 6:
                    return ("Failed to join multicast group");
                case 7:
                    return ("Failed to create dump file");
                case 8:
                    return ("Failed to send GET request to server");
                case 9:
                    return ("Failed to get shared memory");
                case 10:
                    return ("Failed to get position at end of dump file");
                case 11:
                    return ("Failed to join multicast group with source address");
                default:
                    return ("Unknown error code - " + receiverError);
            }
        }

        internal class LinkSpec
        {
            internal StreamProtocol Protocol { get; private set; }
            internal string RemoteName { get; private set; }
            internal IPEndPoint LocalEndPoint { get; private set; }
            internal IPEndPoint RemoteEndPoint { get; private set; }
            internal TimeSpan? Timeout { get; private set; }
            internal string SessionId { get; private set; }
            internal int StreamId { get; private set; }
            internal string Path { get; private set; }
            internal string DumpFileName { get; private set; }
            internal StreamFrequency StreamFrequency { get; private set; }
            internal IPAddress LocalAddress { get; private set; }
            internal string MulticastSource { get; private set; }
            internal int MulticastPort { get; private set; }

            private LinkSpec() { }

            internal LinkSpec(IPEndPoint localEndPoint, IPEndPoint remoteEndPoint)
            {
                LocalEndPoint = localEndPoint;
                RemoteEndPoint = remoteEndPoint;
            }

            internal LinkSpec(StreamProtocol protocol, IPEndPoint localEndPoint, IPEndPoint remoteEndPoint, string multicastSource, int multicastPort, string dumpFileName)
            {
                Protocol = protocol;
                LocalEndPoint = localEndPoint;
                RemoteEndPoint = remoteEndPoint;
                MulticastSource = multicastSource;
                MulticastPort = multicastPort;
                DumpFileName = dumpFileName;
            }

            internal LinkSpec(StreamProtocol protocol, string remoteName, IPEndPoint localEndPoint, IPEndPoint remoteEndPoint, string path, string dumpFileName)
            {
                Protocol = protocol;
                RemoteName = remoteName;
                LocalEndPoint = localEndPoint;
                RemoteEndPoint = remoteEndPoint;
                Path = path;
                DumpFileName = dumpFileName;
            }

            internal LinkSpec(IPAddress localAddress, StreamFrequency streamFrequency, string dumpFileName)
            {
                LocalAddress = localAddress;
                StreamFrequency = streamFrequency;
                DumpFileName = dumpFileName;
            }
        }
    }
}
*//