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
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Reflection;

using DomainObjects;
using NetReceiver;

using NetworkProtocols;
using NetworkProtocols.UPnP;
using NetworkProtocols.Rtsp;

namespace VBox
{
    /// <summary>
    /// The class that describes the VBox controller.
    /// </summary>
    public class VBoxController : ITunerDataProvider, ISampleDataProvider
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

        /// <summary>
        /// Get the buffer spaces used receiving data.
        /// </summary>
        public int BufferSpaceUsed
        {
            get
            {
                if (receiver != null)
                    return (receiver.BufferSpaceUsed);
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
                    return ((IntPtr)receiver.BufferAddress);
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
                    return (receiver.SyncByteSearches);
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
                    return (receiver.SamplesDropped);
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
                    return (receiver.MaximumSampleSize);
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
                    return (receiver.DumpFileSize);
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
                    return (receiver.DataFlowing);
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
        public long SignalStrength
        {
            get
            {
                VBoxQueryLockStatusResponse response = VBoxApi.QueryLockStatus(currentServer.Address, currentServer.TunerId);
                if (response.IsOK)
                    return (response.SignalStrength);
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
                VBoxQueryLockStatusResponse response = VBoxApi.QueryLockStatus(currentServer.Address, currentServer.TunerId);
                if (response.IsOK)
                    return (response.RfLevel);
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
                VBoxQueryLockStatusResponse response = VBoxApi.QueryLockStatus(currentServer.Address, currentServer.TunerId);
                if (response.IsOK)
                    return (response.IsLocked);
                else
                    return (false);
            }
        }

        /// <summary>
        /// Get the tuner.
        /// </summary>
        public Tuner Tuner { get { return (currentServer); } }

        internal static VBoxConfiguration Configuration { get; private set; }
        
        private BackgroundWorker dataWorker;
        private bool running;
        
        private VBoxTuner currentServer;
        private int muxId = 1;
                
        private HttpReceiver receiver;
        private TuningSpec currentTuningSpec;
        private VBoxTuningParameters currentTuningParameters;

        private int[] currentPids;
        
        /// <summary>
        /// Initialize a new instance of the VBoxController class.
        /// </summary>
        public VBoxController()
        {
            UPnPMessage.MulticastAddress = IPAddress.Parse("239.255.255.250");
            UPnPMessage.MulticastPort = 1900;
            UPnPMessage.UPnPClientPort = 29000;

            NetworkConfiguration.VBoxConfiguration = new VBoxConfiguration();
            NetworkConfiguration.VBoxConfiguration.Load();
        }

        /// <summary>
        /// Dispose the controller.
        /// </summary>
        public void Dispose()
        {
            VBoxLogger.Instance.Write("Dispose request received");

            ErrorSpec errorSpec = Stop();
            if (errorSpec != null)
                VBoxLogger.Instance.Write("Dispose failed: " + errorSpec);
            else
                VBoxLogger.Instance.Write("Dispose request processed successfully");
        }

        /// <summary>
        /// Load all VBox servers.
        /// </summary>
        /// <param name="servers">The list to be updated.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public static ErrorSpec LoadServers(Collection<VBoxTuner> servers)
        {
            Collection<UPnPMessage> serverResponses = new Collection<UPnPMessage>();

            string searchTarget = "urn:schemas-upnp-org:device:MediaServer:1";

            for (int serverNumber = 1; serverNumber < 2; serverNumber++)
            {
                ErrorSpec errorSpec = MSearchMessage.SearchForServers(searchTarget, serverResponses, "b7531642-0123-3210-bbbb", StreamServerType.VBox);
                if (errorSpec != null)
                    VBoxLogger.Instance.Write(errorSpec.ToString());
            }

            VBoxLogger.Instance.Write("VBox servers found: " + serverResponses.Count);

            foreach (UPnPMessage response in serverResponses)
            {
                VBoxQueryNumOfTunersResponse countResponse = VBoxApi.GetCountOfTuners(response.LocationAddress);
                if (countResponse.IsOK)
                {
                    for (int index = 0; index < countResponse.TunerCount; index++)
                    {
                        VBoxQueryTunerTypeResponse typeResponse = VBoxApi.GetTunerType(response.LocationAddress, index + 1);
                        if (typeResponse.IsOK)
                        {
                            VBoxTuner server = new VBoxTuner();
                            server.Address = response.LocationAddress;
                            server.UniqueIdentity = getUniqueIdentity(response) + "-" + (index + 1);
                            server.TunerId = index + 1;

                            string tunerSuffix = "tuner " + server.TunerId;

                            if (response.Description.Root.Devices != null && response.Description.Root.Devices.Count > 0)
                                server.Name = response.Description.Root.Devices[0].FriendlyName + " " + tunerSuffix;
                            else
                                server.Name = response.UniqueServiceName + " " + tunerSuffix;

                            if (typeResponse.IsDvbs)
                                server.DvbsFrontEnds = 1;
                            else
                            {
                                if (typeResponse.IsDvbt)
                                    server.DvbtFrontEnds = 1;
                                else
                                {
                                    if (typeResponse.IsDvbc)
                                        server.DvbcFrontEnds = 1;
                                }
                            }

                            servers.Add(server);
                        }
                    }
                }
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
            VBoxLogger.Instance.Write("Changing pid mapping");

            if (currentPids != null)
            {
                if (currentPids.Length == pids.Length)
                {
                    int matched = 0;

                    foreach (int pid in pids)
                    {
                        foreach (int currentPid in currentPids)
                        {
                            if (pid == currentPid)
                            {
                                matched++;
                                break;
                            }
                        }
                    }

                    if (matched == currentPids.Length)
                    {
                        VBoxLogger.Instance.Write("No pid changes");
                        return;
                    }
                }
            }

            VBoxResponse response = VBoxApi.RemovePidsFromMuxStream(currentServer.Address, currentServer.TunerId, muxId, null); 
            if (!response.IsOK)
                VBoxLogger.Instance.Write("VBox: Remove pids failed: " + response.ErrorCode + " " + response.ErrorDescription);

            //receiver.WaitForEmptyStream();            

            receiver.SetPids(pids);
            receiver.ClearMemoryBuffer();

            int[] actualPidList;

            if (pids.Length > 11)
            {
                actualPidList = new int[11];
                for (int index = 0; index < 11; index++)
                    actualPidList[index] = pids[index];
            }
            else
                actualPidList = pids;

            if (actualPidList == null || actualPidList.Length == 0 || actualPidList[0] == -1)
            {
                VBoxApi.CloseMuxStream(currentServer.Address, currentServer.TunerId, muxId);
                response = VBoxApi.OpenMuxStream(currentServer.Address, currentServer.TunerId, muxId, null);
                if (!response.IsOK)
                    VBoxLogger.Instance.Write("VBox: Add all pids failed: " + response.ErrorCode + " " + response.ErrorDescription);
            }
            else
            {
                response = VBoxApi.AddPidsToMuxStream(currentServer.Address, currentServer.TunerId, muxId, actualPidList);
                if (!response.IsOK)
                    VBoxLogger.Instance.Write("VBox: Add pids failed: " + response.ErrorCode + " " + response.ErrorDescription);
            }

            currentPids = pids;
        }

        /// <summary>
        /// Tune the VBox.
        /// </summary>
        /// <param name="server">The server to use.</param>
        /// <param name="tuningParameters">The tuning parameters.</param>
        /// <param name="dumpFileName">The file to dump the data to. Null if not used.</param>
        /// <param name="completeTs">True if the open mux request should specify all pids; false otherwise.</param>   
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Run(VBoxTuner server, VBoxTuningParameters tuningParameters, string dumpFileName, bool completeTs)
        {
            currentServer = server;
            currentTuningParameters = tuningParameters;

            VBoxApi.SoftwareVersion = VBoxApi.QuerySoftwareVersion(server.Address);
            if (!VBoxApi.SoftwareVersion.IsOK)
                return (new ErrorSpec(VBoxApi.ProtocolId, ErrorCode.ServerError, VBoxApi.SoftwareVersion.ErrorCode, VBoxApi.SoftwareVersion.ErrorDescription, null));

            VBoxApi.RegisterTuning(server.Address, server.TunerId);

            if (tuningParameters as VBoxDvbsParameters != null && !server.LnbSet)
            {
                VBoxDvbsParameters dvbsParameters = tuningParameters as VBoxDvbsParameters;
                VBoxResponse lnbResponse = VBoxApi.SetSatLnb(server.Address, server.TunerId, dvbsParameters.LnbType, dvbsParameters.LnbLow, dvbsParameters.LnbHigh);
                if (!lnbResponse.IsOK)
                    return (new ErrorSpec(VBoxApi.ProtocolId, ErrorCode.ServerError, lnbResponse.ErrorCode, lnbResponse.ErrorDescription, null));
                else
                    server.LnbSet = true;
            }

            VBoxResponse setResponse = VBoxApi.SetFrequency(server.Address, server.TunerId, tuningParameters);
            if (!setResponse.IsOK)
                return (new ErrorSpec(VBoxApi.ProtocolId, ErrorCode.ServerError, setResponse.ErrorCode, setResponse.ErrorDescription, null));

            bool locked = false;
            int timeout = RunParameters.Instance.LockTimeout.Seconds;

            while (!locked && timeout > 0)
            {
                Thread.Sleep(1000);
                VBoxQueryLockStatusResponse lockResponse = VBoxApi.QueryLockStatus(server.Address, server.TunerId);
                if (lockResponse.IsOK)
                {
                    locked = lockResponse.IsLocked;
                    if (!locked)
                        timeout--;
                }
                else
                    timeout--;
            }

            if (!locked)
                return (new ErrorSpec(VBoxApi.ProtocolId, ErrorCode.ServerError, setResponse.ErrorCode, setResponse.ErrorDescription, null));

            VBoxOpenMuxStreamResponse openResponse = null;
            muxId = 1;

            currentPids = new int[] { completeTs ? -1 : 3 };

            while ((openResponse == null || !openResponse.IsOK) && muxId < 9)
            {
                openResponse = VBoxApi.OpenMuxStream(currentServer.Address, currentServer.TunerId, muxId, currentPids);
                if (!openResponse.IsOK)
                {
                    VBoxLogger.Instance.Write("Failed to open VBox mux stream ID " + muxId + ":" +
                        " error code: " + openResponse.ErrorCode +
                        " description: " + openResponse.ErrorDescription);
                    muxId++;
                }
            }

            if (muxId > 8)
            {
                VBoxLogger.Instance.Write("No VBox mux streams available");
                return (new ErrorSpec(VBoxApi.ProtocolId, ErrorCode.ServerError, openResponse.ErrorCode, openResponse.ErrorDescription, null));
            }

            VBoxLogger.Instance.Write("Mux Id: " + muxId + " Mux Url: " + openResponse.MuxUrl);

            dataWorker = new BackgroundWorker();
            dataWorker.WorkerSupportsCancellation = true;
            dataWorker.DoWork += new DoWorkEventHandler(dataWorkerDoWork);
            dataWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(dataWorkerCompleted);
            dataWorker.RunWorkerAsync(new LinkSpec(openResponse.MuxAddress, openResponse.MuxPort, openResponse.MuxPath, dumpFileName));

            while (receiver == null || !receiver.Initialized)
                Thread.Sleep(500);

            return (null);
        }

        /// <summary>
        /// Stop receiving data.
        /// </summary>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Stop()
        {
            receiver.Terminate(true);
            while (running)
                Thread.Sleep(500);

            VBoxResponse closeMuxResponse = VBoxApi.CloseMuxStream(currentServer.Address, currentServer.TunerId, muxId);
            if (!closeMuxResponse.IsOK)                
                return (new ErrorSpec(VBoxApi.ProtocolId, ErrorCode.ServerError, closeMuxResponse.ErrorCode, closeMuxResponse.ErrorDescription, null));

            VBoxResponse unregisterResponse = VBoxApi.UnregisterTuning(currentServer.Address, currentServer.TunerId);
            if (!unregisterResponse.IsOK)
                return (new ErrorSpec(VBoxApi.ProtocolId, ErrorCode.ServerError, unregisterResponse.ErrorCode, unregisterResponse.ErrorDescription, null));

            return (null);
        }

        private void dataWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                VBoxLogger.Instance.Write("Data worker thread starting");
                running = true;

                if (RunParameters.IsWindows)
                    Thread.CurrentThread.Name = "Data Thread";
                Thread.CurrentThread.Priority = ThreadPriority.Highest;

                LinkSpec linkSpec = e.Argument as LinkSpec;
                if (linkSpec == null)
                    throw (new ArgumentException("VBox data worker has been started with an incorrect parameter"));

                int logLevel = 0;
                DebugEntry debugEntry = DebugEntry.FindEntry(DebugName.LogLevel, true);
                if (debugEntry != null)
                    logLevel = debugEntry.Parameter;

                receiver = new HttpReceiver(logLevel, RunParameters.StreamLogFileName);
                ReplyCode replyCode = receiver.Initialize("VBox", linkSpec.MuxAddress, linkSpec.MuxPort, "/" + linkSpec.MuxPath, linkSpec.DumpFileName, true);
                if (replyCode != ReplyCode.OK)
                {
                    e.Result = replyCode;                    
                    return;
                }

                e.Result = receiver.StartReceiving((BackgroundWorker)sender);

                VBoxResponse closeResponse = VBoxApi.CloseMuxStream(currentServer.Address, currentServer.TunerId, muxId);
                if (!closeResponse.IsOK)
                    e.Result = closeResponse.ErrorCode;
                else
                {
                    VBoxResponse unregisterResponse = VBoxApi.UnregisterTuning(currentServer.Address, currentServer.TunerId);
                    if (!unregisterResponse.IsOK)
                        e.Result = unregisterResponse.ErrorCode;
                }
            }
            finally
            {
                running = false;
                VBoxLogger.Instance.Write("Data worker thread stopped");
            }
        }

        private void dataWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw new InvalidOperationException("Data worker failed - see inner exception", e.Error);
        }

        /// <summary>
        /// Find a receiver.
        /// </summary>
        /// <param name="tuners">The list of tuners to try.</param>
        /// <param name="tunerNodeType">The node type the tuner must have.</param>
        /// <param name="tuningSpec">A tuning spec instance with tuning details.</param>
        /// <param name="lastTuner">The last tuner used or null if all are to be considered.</param>
        /// <param name="diseqcSetting">The Diseqc parameters.</param>
        /// <param name="completeTs">True if the complete TS is to be processed; false otherwise.</param>
        /// <returns>A VBox controller instance or null if no tuner available.</returns>
        public static VBoxController FindReceiver(Collection<SelectedTuner> tuners, TunerNodeType tunerNodeType, TuningSpec tuningSpec, Tuner lastTuner, int diseqcSetting, bool completeTs)
        {
            return (FindReceiver(tuners, tunerNodeType, tuningSpec, lastTuner, diseqcSetting, null, completeTs));
        }

        /// <summary>
        /// Find a receiver.
        /// </summary>
        /// <param name="tuners">The list of tuners to try.</param>
        /// <param name="tunerNodeType">The node type the tuner must have.</param>
        /// <param name="tuningSpec">A tuning spec instance with tuning details.</param>
        /// <param name="lastTuner">The last tuner used or null if all are to be considered.</param>
        /// <param name="diseqcSetting">The Diseqc parameters.</param>
        /// <param name="dumpFileName">The full path of the duump file. Null if not used.</param>
        /// <param name="completeTs">True if the complete TS is to be processed; false otherwise.</param>
        /// <returns>A VBox controller instance or null if no tuner available.</returns>
        public static VBoxController FindReceiver(Collection<SelectedTuner> tuners, TunerNodeType tunerNodeType, TuningSpec tuningSpec, Tuner lastTuner, int diseqcSetting, string dumpFileName, bool completeTs)
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

                        if (tuner.IsVBoxTuner && process)
                        {
                            VBoxController controller = checkReceiverAvailability(tuner, tuningSpec, diseqcSetting, dumpFileName, completeTs);
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

                if (tuner.IsVBoxTuner && process)
                {
                    if (tuner.Supports(tunerNodeType))
                    {
                        VBoxController controller = checkReceiverAvailability(tuner, tuningSpec, diseqcSetting, dumpFileName, completeTs);
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

        private static VBoxController checkReceiverAvailability(Tuner tuner, TuningSpec tuningSpec, int diseqcSetting, string dumpFileName, bool completeTs)
        {
            VBoxTuner server = tuner as VBoxTuner;
            if (server == null)
                return (null);

            VBoxController controller = new VBoxController();
            controller.currentTuningSpec = tuningSpec;

            VBoxTuningParameters tuningParameters = VBoxTuningParameters.GetParameters(tuningSpec, diseqcSetting);            

            ErrorSpec runReply = controller.Run(server, tuningParameters, dumpFileName, completeTs);
            if (runReply == null)
                return (controller);
            else
                return (null);
        }

        internal class LinkSpec
        {
            internal string MuxAddress { get; private set; }
            internal int MuxPort { get; private set; }
            internal string MuxPath { get; private set; }
            internal string DumpFileName { get; private set; }

            private LinkSpec() { }

            internal LinkSpec(string muxAddress, int muxPort, string muxPath, string dumpFileName)
            {
                MuxAddress = muxAddress;
                MuxPort = muxPort;
                MuxPath = muxPath;
                DumpFileName = dumpFileName;
            }
        }
    }
}

