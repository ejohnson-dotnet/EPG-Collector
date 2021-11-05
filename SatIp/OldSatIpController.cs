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

using DomainObjects;

using UPnP;
using Rtp;
using Sdp;
using Rtsp;
using SatIpDomainObjects;

namespace SatIp
{
    /// <summary>
    /// The class that describes the SAT>IP controller.
    /// </summary>
    public unsafe class SatIpController : ITunerDataProvider, ISampleDataProvider
    {
        internal static SatIpConfiguration Configuration { get; private set; }

        /// <summary>
        /// Get the protocol identification.
        /// </summary>
        public static string SatIpProtocolId { get { return ("Sat>IP"); } }

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

        /// <summary>
        /// Get the tuning frequency.
        /// </summary>
        public TuningFrequency Frequency { get { return (currentTuningSpec.Frequency); } }

        /// <summary>
        /// Get the signal strength.
        /// </summary>
        public int SignalStrength
        {
            get
            {
                if (Configuration.NoAnnouncements)
                    return (100);

                if (currentAnnouncement != null)
                {
                    SatIpMediaFormat mediaFormat = currentAnnouncement.MediaFormat as SatIpMediaFormat;
                    if (mediaFormat != null)
                        return (mediaFormat.Level);
                    else
                        return (0);
                }
                else
                    return (0);
            }
        }

        /// <summary>
        /// Get the signal quality.
        /// </summary>
        public int SignalQuality
        {
            get
            {
                if (Configuration.NoAnnouncements)
                    return (15);

                if (currentAnnouncement != null)
                {
                    SatIpMediaFormat mediaFormat = currentAnnouncement.MediaFormat as SatIpMediaFormat;
                    if (mediaFormat != null)
                        return (mediaFormat.Quality);
                    else
                        return (0);
                }
                else
                    return (0);
            }
        }

        /// <summary>
        /// Return true if a signal is present; false otherwise.
        /// </summary>
        public bool SignalPresent { get { return (SignalLocked); } }

        /// <summary>
        /// Return true if the signal is locked; false otherwise.
        /// </summary>
        public bool SignalLocked
        {
            get
            {
                if (Configuration.NoAnnouncements)
                    return (true);

                if (currentAnnouncement != null)
                {
                    SatIpMediaFormat mediaFormat = currentAnnouncement.MediaFormat as SatIpMediaFormat;
                    if (mediaFormat != null)
                        return (mediaFormat.SignalLock);
                    else
                        return (false);
                }
                else
                    return (false);
            }
        }

        /// <summary>
        /// Get the tuner.
        /// </summary>
        public Tuner Tuner { get { return (currentServer); } }

        private BackgroundWorker dataWorker;
        private BackgroundWorker announcementWorker;
        private BackgroundWorker optionsWorker;

        private SatIpMediaFormat satIpFormat;
        private SatIpCap satIpCap;

        private SatIpServer currentServer;
        private RtspSession currentSession;
        private int rtspPort = 554;

        private ManagedReceiver receiver;
        private TuningSpec currentTuningSpec;
        private TuningParameters currentTuningParameters;
        private AnnouncementPacket currentAnnouncement;

        /// <summary>
        /// Initialize a new instance of the SatIpController class.
        /// </summary>
        public SatIpController()
        {
            UPnPMessage.MulticastAddress = IPAddress.Parse("239.255.255.250");
            UPnPMessage.MulticastPort = 1900;
            UPnPMessage.UPnPClientPort = 19000;

            satIpFormat = new SatIpMediaFormat();
            MediaFormat.RegisterFormat(satIpFormat);

            satIpCap = new SatIpCap();
            Device.RegisterLoader(satIpCap);

            SatIpControlAttribute satIpControlAttribute = new SatIpControlAttribute();
            ControlAttribute.RegisterControlCreator(satIpControlAttribute);

            ControlPacket.CustomPacket = new AnnouncementPacket();

            Configuration = new SatIpConfiguration();
            Configuration.Load();
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
        /// Load all SAT>IP servers.
        /// </summary>
        /// <param name="servers">The list to be updated.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public static ErrorSpec LoadServers(Collection<SatIpServer> servers)
        {
            Collection<UPnPMessage> serverResponses = new Collection<UPnPMessage>();

            string searchTarget = "urn:ses-com:device:SatIPServer:";

            for (int serverNumber = 1; serverNumber < 2; serverNumber++)
            {
                ErrorSpec errorSpec = MSearchMessage.SearchForServers(searchTarget + serverNumber, serverResponses);
                if (errorSpec != null)
                    SatIpDomainObjects.SatIpLogger.Instance.Write(errorSpec.ToString());
            }

            SatIpDomainObjects.SatIpLogger.Instance.Write("Sat>IP servers found: " + serverResponses.Count);

            foreach (UPnPMessage response in serverResponses)
            {
                SatIpServer server = new SatIpServer();
                server.Address = response.LocationAddress;
                server.Port = response.LocationPort;
                server.UniqueIdentity = getUniqueIdentity(response);

                server.DvbsFrontEnds = 4;
                server.DvbtFrontEnds = 0;
                server.DvbcFrontEnds = 0;

                if (response.Description.Root.Devices != null)
                {
                    foreach (Device device in response.Description.Root.Devices)
                    {
                        if (device.FriendlyName != null)
                            server.Name = device.FriendlyName;

                        if (device.CustomLines != null)
                        {
                            foreach (IDescriptionLine line in device.CustomLines)
                            {
                                SatIpCap capabilities = line as SatIpCap;
                                if (capabilities != null)
                                {
                                    if (capabilities.ModulationSystems != null)
                                    {
                                        server.DvbsFrontEnds = 0;
                                        server.DvbtFrontEnds = 0;

                                        foreach (ModulationSystem modulationSystem in capabilities.ModulationSystems)
                                        {
                                            if (modulationSystem.Type.ToUpperInvariant() == "DVBS" ||
                                                modulationSystem.Type.ToUpperInvariant() == "DVBS2")
                                                server.DvbsFrontEnds += modulationSystem.Count;
                                            else
                                            {
                                                if (modulationSystem.Type.ToUpperInvariant() == "DVBT" ||
                                                    modulationSystem.Type.ToUpperInvariant() == "DVBT2")
                                                    server.DvbtFrontEnds += modulationSystem.Count;
                                                else
                                                {
                                                    if (modulationSystem.Type.ToUpperInvariant() == "DVBC" ||
                                                        modulationSystem.Type.ToUpperInvariant() == "DVBC2")
                                                        server.DvbcFrontEnds += modulationSystem.Count;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }

                if (server.Name == null)
                    server.Name = response.UniqueServiceName;

                servers.Add(server);
            }

            return (null);
        }

        private static string getUniqueIdentity(UPnPMessage response)
        {
            string[] parts = response.UniqueServiceName.Split(new char[] { ':' });

            if (parts.Length < 2 || parts[0].ToLowerInvariant() != "uuid")
                return ("Unknown");
            else
                return (parts[1]);
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
            currentTuningParameters.Pids = new Collection<int>();

            foreach (int pid in pids)
                currentTuningParameters.Pids.Add(pid);

            if (currentSession == null)
                Run(currentServer, currentTuningParameters, null, null);
            else
            {
                ErrorSpec reply = currentSession.Modify(currentTuningParameters);
                if (reply != null)
                    SatIpDomainObjects.SatIpLogger.Instance.Write("Change PID mapping failed: " + reply);
            }

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
        /// <param name="path">The path.</param>
        /// <param name="dumpFileName">The file to dump the data to. Null if not used.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Run(SatIpServer server, TuningParameters tuningParameters, string path, string dumpFileName)
        {
            return (Run(server, rtspPort, tuningParameters, path, dumpFileName));
        }

        /// <summary>
        /// Start collecting data.
        /// </summary>
        /// <param name="server">The server to use.</param>
        /// <param name="serverPort">The server port.</param>
        /// <param name="tuningParameters">The tuning parameters.</param>
        /// <param name="path">The path.</param>
        /// <param name="dumpFileName">The file to dump the data to. Null if not used.</param>        
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Run(SatIpServer server, int serverPort, TuningParameters tuningParameters, string path, string dumpFileName)
        {
            currentServer = server;
            currentTuningParameters = tuningParameters;
            rtspPort = serverPort;

            currentSession = RtspSession.CreateInstance(currentServer.Address, serverPort);
            ErrorSpec reply = currentSession.Start(tuningParameters, path);
            if (reply != null)
                return (reply);

            if (currentTuningParameters == null)
                currentTuningParameters = new DvbsParameters(0);

            IPAddress localAddress = Utils.GetLocalIpAddress(SatIpLogger.Instance);
            if (localAddress == null)
                return (new ErrorSpec(SatIpProtocolId, ErrorCode.ServiceUnavailable, 0, "Sat>IP Controller: No IPv4 address located to set the local IP address", null));

            SatIpLogger.Instance.Write("Local IP address set to " + localAddress);

            IPEndPoint localDataPoint = new IPEndPoint(localAddress, currentSession.Setup.UnicastClientDataPort);
            IPEndPoint remoteDataPoint = new IPEndPoint(IPAddress.Parse(currentSession.Setup.Source), currentSession.Setup.UnicastServerDataPort);
            startDataThread(localDataPoint, remoteDataPoint, dumpFileName);

            if (!Configuration.NoAnnouncements)
            {
                IPEndPoint localControlPoint = new IPEndPoint(localAddress, currentSession.Setup.UnicastClientControlPort);
                IPEndPoint remoteControlPoint = new IPEndPoint(IPAddress.Parse(currentSession.Setup.Source), currentSession.Setup.UnicastServerControlPort);
                startAnnouncementThread(localControlPoint, remoteControlPoint);
            }

            IPEndPoint remoteOptionsPoint = new IPEndPoint(IPAddress.Parse(currentSession.Setup.Source), rtspPort);
            startOptionsThread(remoteOptionsPoint, currentSession.Setup.SessionTimeout, currentSession.Setup.SessionId, currentSession.Setup.StreamId);

            reply = currentSession.Play();
            if (reply != null)
                return (reply);

            return (null);
        }

        /// <summary>
        /// Stop receiving data.
        /// </summary>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Stop()
        {
            ErrorSpec errorSpec = currentSession.Describe();
            if (errorSpec != null)
                return (errorSpec);

            stopDataThread();
            stopAnnouncementThread();
            stopOptionsThread();

            errorSpec = currentSession.Stop();
            if (errorSpec != null)
                return (errorSpec);

            return (null);
        }

        private void startDataThread(IPEndPoint localEndPoint, IPEndPoint remoteEndPoint, string dumpFileName)
        {
            dataWorker = new BackgroundWorker();
            dataWorker.WorkerSupportsCancellation = true;
            dataWorker.DoWork += new DoWorkEventHandler(dataWorkerDoWork);
            dataWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(dataWorkerCompleted);
            dataWorker.RunWorkerAsync(new LinkSpec(localEndPoint, remoteEndPoint, dumpFileName));
        }

        private void stopDataThread()
        {
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

            int logLevel = 0;

            DebugEntry debugEntry = DebugEntry.FindEntry(DebugName.LogLevel, true);
            if (debugEntry != null)
                logLevel = debugEntry.Parameter;

            receiver = new ManagedReceiver(logLevel, RunParameters.StreamLogFileName);
            receiver.InitializeUdp(0, null, linkSpec.LocalEndPoint.Port, null, 0, linkSpec.DumpFileName);

            if (currentTuningParameters.Pids != null)
            {
                foreach (int pid in currentTuningParameters.Pids)
                    receiver.AddPid(pid);
            }

            receiver.StartReceivingUdp();

            SatIpDomainObjects.SatIpLogger.Instance.Write("Data worker thread stopped");
        }

        private void dataWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw new InvalidOperationException("Data worker failed - see inner exception", e.Error);
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
            if (announcementWorker == null)
                return;

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

            byte[] inputBuffer = new byte[2048];

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
                    {
                        SatIpDomainObjects.SatIpLogger.Instance.Write("Announcement packet: " + errorSpec);
                        return;
                    }
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

        private void startOptionsThread(IPEndPoint remoteEndPoint, TimeSpan? timeout, string sessionID, int streamId)
        {
            if (timeout != null)
                SatIpDomainObjects.SatIpLogger.Instance.Write("Options timeout: " + timeout);
            else
                SatIpDomainObjects.SatIpLogger.Instance.Write("Options timeoutdefaulted to 30 seconds");

            optionsWorker = new BackgroundWorker();
            optionsWorker.WorkerSupportsCancellation = true;
            optionsWorker.DoWork += new DoWorkEventHandler(optionsWorkerDoWork);
            optionsWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(optionsWorkerCompleted);
            optionsWorker.RunWorkerAsync(new LinkSpec(remoteEndPoint, timeout, sessionID, streamId));
        }

        private void stopOptionsThread()
        {
            optionsWorker.CancelAsync();
            while (optionsWorker.IsBusy)
                Thread.Sleep(500);
        }

        private void optionsWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            SatIpDomainObjects.SatIpLogger.Instance.Write("Options worker thread starting");

            Thread.CurrentThread.Name = "Options Thread";
            Thread.CurrentThread.Priority = ThreadPriority.Normal;

            LinkSpec linkSpec = e.Argument as LinkSpec;
            if (linkSpec == null)
                throw (new ArgumentException("Options worker started with incorrect parameter"));

            double timeout;
            double countdown;

            if (linkSpec.Timeout != null)
                timeout = linkSpec.Timeout.Value.TotalSeconds / 2;
            else
                timeout = 30;
            countdown = timeout;

            int cseq = 0;

            while (!((BackgroundWorker)sender).CancellationPending)
            {
                if (countdown > 0)
                {
                    Thread.Sleep(1000);
                    countdown--;
                }
                else
                {
                    cseq++;

                    RtspOptions options = new RtspOptions();
                    options.Process(linkSpec.RemoteEndPoint.Address.ToString(), linkSpec.RemoteEndPoint.Port, cseq, linkSpec.SessionId, linkSpec.StreamId);

                    countdown = timeout;
                }
            }

            SatIpDomainObjects.SatIpLogger.Instance.Write("Options worker thread stopped");
        }

        private void optionsWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw new InvalidOperationException("Options worker failed - see inner exception", e.Error);
        }

        /// <summary>
        /// Find a receiver.
        /// </summary>
        /// <param name="tuners">The list of tuners to try.</param>
        /// <param name="tunerNodeType">The node type the tuner must have.</param>
        /// <param name="tuningSpec">A tuning spec instance with tuning details.</param>
        /// <param name="lastTuner">The last tuner used or null if all are to be considered.</param>
        /// <param name="diseqcRequest">The Diseqc parameters.</param>
        /// <returns>A Sat>IP controller instance or null if no tuner available.</returns>
        public static SatIpController FindReceiver(Collection<SelectedTuner> tuners, TunerNodeType tunerNodeType, TuningSpec tuningSpec, Tuner lastTuner, int diseqcSetting)
        {
            return (FindReceiver(tuners, tunerNodeType, tuningSpec, lastTuner, diseqcSetting, null));
        }

        /// <summary>
        /// Find a receiver.
        /// </summary>
        /// <param name="tuners">The list of tuners to try.</param>
        /// <param name="tunerNodeType">The node type the tuner must have.</param>
        /// <param name="tuningSpec">A tuning spec instance with tuning details.</param>
        /// <param name="lastTuner">The last tuner used or null if all are to be considered.</param>
        /// <param name="diseqcRequest">The Diseqc parameters.</param>
        /// <param name="dumpFileName">The full path of the duump file. Null if not used.</param>
        /// <returns>A Sat>IP controller instance or null if no tuner available.</returns>
        public static SatIpController FindReceiver(Collection<SelectedTuner> tuners, TunerNodeType tunerNodeType, TuningSpec tuningSpec, Tuner lastTuner, int diseqcSetting, string dumpFileName)
        {
            bool process = (lastTuner == null);

            if (tuners.Count != 0)
            {
                for (int index = 0; index < tuners.Count; index++)
                {
                    int tunerNumber = tuners[index].TunerNumber - 1;

                    if (tunerNumber < Tuner.TunerCollection.Count && Tuner.TunerCollection[tunerNumber].Supports(tunerNodeType))
                    {
                        Tuner tuner = Tuner.TunerCollection[tunerNumber];

                        if (tuner.IsSatIpTuner && process)
                        {
                            SatIpController controller = checkReceiverAvailability(tuner, tuningSpec, diseqcSetting, dumpFileName);
                            if (controller != null)
                            {
                                Logger.Instance.Write("Using tuner " + (tunerNumber + 1) + ": " + tuner.Name);
                                return (controller);
                            }
                        }

                        if (!process)
                            process = (tuner == lastTuner);
                    }
                }
                return (null);
            }

            for (int index = 0; index < Tuner.TunerCollection.Count; index++)
            {
                Tuner tuner = Tuner.TunerCollection[index];

                if (tuner.IsSatIpTuner && process)
                {
                    if (tuner.Supports(tunerNodeType))
                    {
                        SatIpController controller = checkReceiverAvailability(tuner, tuningSpec, diseqcSetting, dumpFileName);
                        if (controller != null)
                        {
                            Logger.Instance.Write("Using tuner " + (Tuner.TunerCollection.IndexOf(tuner) + 1) + ": " + tuner.Name);
                            return (controller);
                        }
                    }
                }

                if (!process)
                    process = (tuner == lastTuner);
            }

            return (null);
        }

        private static SatIpController checkReceiverAvailability(Tuner tuner, TuningSpec tuningSpec, int diseqcSetting, string dumpFileName)
        {
            SatIpServer server = tuner as SatIpServer;
            if (server == null)
                return (null);

            SatIpController controller = new SatIpController();
            controller.currentTuningSpec = tuningSpec;

            TuningParameters tuningParameters = TuningParameters.GetParameters(tuningSpec, diseqcSetting);
            tuningParameters.Pids = new Collection<int>();
            tuningParameters.Pids.Add(0x03);

            ErrorSpec runReply = controller.Run(server, tuningParameters, null, dumpFileName);
            if (runReply == null)
                return (controller);
            else
                return (null);
        }

        internal class LinkSpec
        {
            internal IPEndPoint LocalEndPoint { get; private set; }
            internal IPEndPoint RemoteEndPoint { get; private set; }
            internal TimeSpan? Timeout { get; private set; }
            internal string SessionId { get; private set; }
            internal int StreamId { get; private set; }
            internal string DumpFileName { get; private set; }

            private LinkSpec() { }

            internal LinkSpec(IPEndPoint localEndPoint, IPEndPoint remoteEndPoint)
            {
                LocalEndPoint = localEndPoint;
                RemoteEndPoint = remoteEndPoint;
            }

            internal LinkSpec(IPEndPoint remoteEndPoint, TimeSpan? timeout, string sessionId, int streamId)
            {
                RemoteEndPoint = remoteEndPoint;
                Timeout = timeout;
                SessionId = sessionId;
                StreamId = streamId;
            }

            internal LinkSpec(IPEndPoint localEndPoint, IPEndPoint remoteEndPoint, string dumpFileName)
            {
                LocalEndPoint = localEndPoint;
                RemoteEndPoint = remoteEndPoint;
                DumpFileName = dumpFileName;
            }
        }
    }
}*/

