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
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;

using DomainObjects;
using DirectShow;
using DVBServices;
using SatIp;
using VBox;

namespace EPGCentre
{
    public partial class ChannelScanControl : UserControl, IUpdateControl
    {
        /// <summary>
        /// Get the general window heading for the data.
        /// </summary>
        public string Heading { get { return ("EPG Centre - Channel Scan"); } }
        /// <summary>
        /// Get the default directory.
        /// </summary>
        public string DefaultDirectory { get { return (null); } }
        /// <summary>
        /// Get the default output file name.
        /// </summary>
        public string DefaultFileName { get { return (null); } }
        /// <summary>
        /// Get the save file filter.
        /// </summary>
        public string SaveFileFilter { get { return (null); } }
        /// <summary>
        /// Get the save file title.
        /// </summary>
        public string SaveFileTitle { get { return (null); } }
        /// <summary>
        /// Get the save file suffix.
        /// </summary>
        public string SaveFileSuffix { get { return (null); } }

        /// <summary>
        /// Return the state of the data set.
        /// </summary>
        public DataState DataState { get { return (new DataState()); } }

        internal static Collection<ChannelScanResult> ScanResults { get { return scanResults; } }

        private BackgroundWorker scanWorker;
        private AutoResetEvent resetEvent = new AutoResetEvent(false);

        private static Collection<ChannelScanResult> scanResults;

        private delegate DialogResult ShowMessage(string message, MessageBoxButtons buttons, MessageBoxIcon icon);
        private delegate void UpdateProgress(bool clear, string message);
        private delegate void Finish();

        private string startScanText = "Start Scan";
        private string stopScanText = "Stop Scan";

        private int[] atscVHFLowBand = { 57000, 63000, 69000, 79000, 85000 };
        private int[] atscVHFHighBand = { 177000, 213000 };
        private int[] atscUHFBand = { 473000, 7010000 };

        private int signalLockTimeout;
        private int dataCollectionTimeout;

        /// <summary>
        /// Initialize a new instance of the ChannelScanControl class.
        /// </summary>
        public ChannelScanControl()
        {
            InitializeComponent();            
        }

        private void btTimeoutDefaults_Click(object sender, EventArgs e)
        {
            nudSignalLockTimeout.Value = 5;
            nudDataCollectionTimeout.Value = 10;

            signalLockTimeout = 5;
            dataCollectionTimeout = 10;
        }

        private void nudSignalLockTimeout_ValueChanged(object sender, EventArgs e)
        {
            signalLockTimeout = (int)nudSignalLockTimeout.Value;
        }

        private void nudDataCollectionTimeout_ValueChanged(object sender, EventArgs e)
        {
            dataCollectionTimeout = (int)nudDataCollectionTimeout.Value;
        }

        internal void Process()
        {
            cmdScan.Text = startScanText;

            lvDvbResults.Visible = false;
            lvAtscResults.Visible = false;

            frequencySelectionControl.Visible = true;
            cmdScan.Visible = true;
            lbProgress.Visible = true;
            gpTimeouts.Visible = true;

            frequencySelectionControl.Process(true, true, true);
        }

        internal void ViewResults()
        {            
        }

        private void cmdScan_Click(object sender, EventArgs e)
        {
            if (cmdScan.Text == startScanText)
                startScan();
            else
                stopScan();
        }

        private void stopScan()
        {
            cmdScan.Enabled = false;

            scanWorker.CancelAsync();
            resetEvent.WaitOne(new TimeSpan(0, 0, 15));

            MessageBox.Show("Channel scan aborted", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);

            cmdScan.Text = startScanText;
            cmdScan.Enabled = true;
        }

        private void startScan()
        {
            if (!checkData())
                return;

            signalLockTimeout = (int)nudSignalLockTimeout.Value;
            dataCollectionTimeout = (int)nudDataCollectionTimeout.Value;

            cmdScan.Text = stopScanText;
            scanResults = new Collection<ChannelScanResult>();

            Logger.Instance.Write("Channel scan started for " + frequencySelectionControl.SelectedFrequency.Provider.Name);
            
            MainWindow.ChangeMenuItemAvailability(false);
            MainWindow.SetLoadFromScanState(false);

            scanWorker = new BackgroundWorker();
            scanWorker.WorkerSupportsCancellation = true;
            scanWorker.DoWork += new DoWorkEventHandler(doScan);
            scanWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(scanWorkerCompleted);
            scanWorker.RunWorkerAsync(frequencySelectionControl.SelectedFrequency);
        }

        private bool checkData()
        {
            string reply = frequencySelectionControl.ValidateForm();
            if (reply != null)
            {
                MessageBox.Show(reply, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (false);
            }

            return (true);
        }

        private void doScan(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker scanWorker = sender as BackgroundWorker;
            TuningFrequency baseFrequency = e.Argument as TuningFrequency;

            lbProgress.Invoke(new UpdateProgress(updateProgress), true, "Starting channel scan for " + baseFrequency.Provider.Name);

            scanResults.Clear();
            int processedCount = 0;
            int failedCount = 0;
            
            foreach (TuningFrequency currentFrequency in baseFrequency.Provider.Frequencies)
            {
                if (scanWorker.CancellationPending)
                    return;

                processedCount++;
                lbProgress.Invoke(new UpdateProgress(updateProgress), false, "Processing frequency " + currentFrequency + " (" + processedCount + " of " + baseFrequency.Provider.Frequencies.Count + ")");

                ITunerDataProvider dataProvider = getTuner(baseFrequency, currentFrequency, scanWorker);

                if (scanWorker.CancellationPending)
                    return;

                if (dataProvider == null)
                {
                    lbProgress.Invoke(new UpdateProgress(updateProgress), false, "Failed to tune to " + currentFrequency);
                    failedCount++;
                }
                else
                {
                    RunParameters.Instance.CurrentFrequency = currentFrequency;
                    RunParameters.Instance.Repeats = dataCollectionTimeout / 2;

                    FrequencyScanner frequencyScanner = new FrequencyScanner((ISampleDataProvider)dataProvider, new int[] { BDAGraph.SdtPid }, false, sender as BackgroundWorker);

                    if (scanWorker.CancellationPending)
                        return;

                    Collection<TVStation> channels = frequencyScanner.FindTVStations();
                    if (channels != null)
                        lbProgress.Invoke(new UpdateProgress(updateProgress), false, "Found " + channels.Count + " channels on " + currentFrequency);
                    else
                        lbProgress.Invoke(new UpdateProgress(updateProgress), false, "Found 0 channels on " + currentFrequency);

                    foreach (TVStation channel in channels)
                        scanResults.Add(new ChannelScanResult(currentFrequency, channel));

                    dataProvider.Dispose();
                }
            }

            lbProgress.Invoke(new UpdateProgress(updateProgress), false, "Frequencies processed: " + processedCount);
            lbProgress.Invoke(new UpdateProgress(updateProgress), false, "Failed to tune: " + failedCount);
            lbProgress.Invoke(new UpdateProgress(updateProgress), false, "Channels found: " + scanResults.Count);
        }

        private ITunerDataProvider getTuner(TuningFrequency baseFrequency, TuningFrequency currentFrequency, BackgroundWorker scanWorker)
        {
            TunerNodeType tunerNodeType;
            TuningSpec tuningSpec;

            SatelliteFrequency satelliteFrequency = currentFrequency as SatelliteFrequency;
            if (satelliteFrequency != null)
            {
                tunerNodeType = TunerNodeType.Satellite;
                tuningSpec = new TuningSpec((Satellite)satelliteFrequency.Provider, satelliteFrequency);
                satelliteFrequency.SatelliteDish = ((SatelliteFrequency)baseFrequency).SatelliteDish;
            }
            else
            {
                TerrestrialFrequency terrestrialFrequency = currentFrequency as TerrestrialFrequency;
                if (terrestrialFrequency != null)
                {
                    tunerNodeType = TunerNodeType.Terrestrial;
                    tuningSpec = new TuningSpec(terrestrialFrequency);
                }
                else
                {
                    CableFrequency cableFrequency = currentFrequency as CableFrequency;
                    if (cableFrequency != null)
                    {
                        tunerNodeType = TunerNodeType.Cable;
                        tuningSpec = new TuningSpec(cableFrequency);
                    }
                    else
                    {
                        AtscFrequency atscFrequency = currentFrequency as AtscFrequency;
                        if (atscFrequency != null)
                        {
                            if (atscFrequency.TunerType == TunerType.ATSC)
                                tunerNodeType = TunerNodeType.ATSC;
                            else
                                tunerNodeType = TunerNodeType.Cable;
                            tuningSpec = new TuningSpec(atscFrequency);
                        }
                        else
                        {
                            ClearQamFrequency clearQamFrequency = currentFrequency as ClearQamFrequency;
                            if (clearQamFrequency != null)
                            {
                                tunerNodeType = TunerNodeType.Cable;
                                tuningSpec = new TuningSpec(clearQamFrequency);
                            }
                            else
                            {
                                ISDBSatelliteFrequency isdbSatelliteFrequency = currentFrequency as ISDBSatelliteFrequency;
                                if (isdbSatelliteFrequency != null)
                                {
                                    tunerNodeType = TunerNodeType.ISDBS;
                                    tuningSpec = new TuningSpec((Satellite)isdbSatelliteFrequency.Provider, isdbSatelliteFrequency);
                                }
                                else
                                {
                                    ISDBTerrestrialFrequency isdbTerrestrialFrequency = currentFrequency as ISDBTerrestrialFrequency;
                                    if (isdbTerrestrialFrequency != null)
                                    {
                                        tunerNodeType = TunerNodeType.ISDBT;
                                        tuningSpec = new TuningSpec(isdbTerrestrialFrequency);
                                    }
                                    else
                                        throw (new InvalidOperationException("Tuning frequency not recognized"));
                                }
                            }
                        }
                    }
                }
            }

            Tuner currentTuner = null;
            bool finished = false;

            while (!finished)
            {
                if (scanWorker.CancellationPending)
                {
                    Logger.Instance.Write("Scan abandoned by user");                    
                    return null;
                }

                ITunerDataProvider graph = BDAGraph.FindTuner(baseFrequency.SelectedTuners, tunerNodeType, tuningSpec, currentTuner);
                if (graph == null)
                {
                    graph = SatIpController.FindReceiver(baseFrequency.SelectedTuners, tunerNodeType, tuningSpec, currentTuner, TuningFrequency.GetDiseqcSetting(tuningSpec.Frequency));
                    if (graph == null)
                    {
                        graph = VBoxController.FindReceiver(baseFrequency.SelectedTuners, tunerNodeType, tuningSpec, currentTuner, TuningFrequency.GetDiseqcSetting(tuningSpec.Frequency), true);
                        if (graph == null)
                        {
                            Logger.Instance.Write("<e> No tuner able to tune frequency " + currentFrequency);
                            finished = true;
                        }
                    }
                }

                if (!finished)
                {
                    string tuneReply = checkTuning(graph, scanWorker);

                    if (scanWorker.CancellationPending)
                    {
                        Logger.Instance.Write("Find abandoned by user");
                        graph.Dispose();
                        return null;
                    }

                    if (tuneReply == null)
                        return graph;
                    else
                    {
                        Logger.Instance.Write("Failed to tune frequency " + currentFrequency.ToString());
                        graph.Dispose();
                        currentTuner = graph.Tuner;
                    }
                }   
            }

            return null;     
        }

        private string checkTuning(ITunerDataProvider graph, BackgroundWorker scanWorker)
        {
            TimeSpan timeout = new TimeSpan();
            bool done = false;
            bool locked = false;            

            while (!done)
            {
                if (scanWorker.CancellationPending)
                {
                    Logger.Instance.Write("Find abandoned by user");
                    return (null);
                }

                locked = graph.SignalLocked;
                if (!locked)
                {
                    if (graph.SignalQuality > 0)
                    {
                        locked = true;
                        done = true;
                    }
                    else
                    {
                        if (graph.SignalPresent)
                        {
                            locked = true;
                            done = true;
                        }
                        else
                        {
                            Logger.Instance.Write("Signal not acquired: lock is " + graph.SignalLocked + " quality is " + graph.SignalQuality + " signal not present");
                            Thread.Sleep(1000);
                            timeout = timeout.Add(new TimeSpan(0, 0, 1));
                            done = (timeout.TotalSeconds == signalLockTimeout);
                        }
                    }
                }
                else
                {
                    Logger.Instance.Write("Signal acquired: lock is " + graph.SignalLocked + " quality is " + graph.SignalQuality + " strength is " + graph.SignalStrength);
                    done = true;
                }
            }

            if (locked)
                return (null);
            else
                return ("<e> The tuner failed to acquire a signal");
        }

        private void updateProgress(bool clear, string message)
        {
            if (clear)
                lbProgress.Items.Clear();

            lbProgress.Items.Add(message);
            lbProgress.TopIndex = lbProgress.Items.Count - 1;
        }

        private DialogResult showMessage(string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return (MessageBox.Show(message, "EPG Centre", buttons, icon));
        }

        private void scanWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                throw new InvalidOperationException("Background worker failed - see inner exception", e.Error);

            if (!lbProgress.InvokeRequired)
                finish();
            else
                lbProgress.Invoke(new Finish(finish));     
        }

        private void finish()
        {
            if (frequencySelectionControl.SelectedFrequency.Provider as AtscProvider != null)
                formatAtscResults();
            else
                formatDvbResults();

            frequencySelectionControl.Visible = false;
            cmdScan.Visible = false;
            lbProgress.Visible = false;
            gpTimeouts.Visible = false;

            MainWindow.ChangeMenuItemAvailability(true);
            cmdScan.Text = startScanText;

            MainWindow.SetLoadFromScanState(true);
        }

        private void formatDvbResults()
        {
            lvDvbResults.Items.Clear();
            string currentFrequency = null;

            foreach (ChannelScanResult scanResult in scanResults)
            {
                ListViewItem item = new ListViewItem();
                
                if (currentFrequency == null)
                {
                    currentFrequency = scanResult.TuningFrequency.ToString();
                    item.Text = scanResult.TuningFrequency.ToString();
                }
                else
                {
                    if (scanResult.TuningFrequency.ToString() != currentFrequency)
                    {
                        lvDvbResults.Items.Add(new ListViewItem());
                        currentFrequency = scanResult.TuningFrequency.ToString();
                        item.Text = scanResult.TuningFrequency.ToString();
                    }
                }

                item.SubItems.Add(scanResult.Channel.Name);
                item.SubItems.Add(scanResult.Channel.ProviderName);
                item.SubItems.Add(scanResult.Channel.ServiceType.ToString());
                item.SubItems.Add(scanResult.Channel.OriginalNetworkID.ToString());
                item.SubItems.Add(scanResult.Channel.TransportStreamID.ToString());
                item.SubItems.Add(scanResult.Channel.ServiceID.ToString());
                item.SubItems.Add(scanResult.Channel.Encrypted ? "yes" : "no");
                item.SubItems.Add(scanResult.Channel.NextFollowingAvailable ? "yes" : "no");
                item.SubItems.Add(scanResult.Channel.ScheduleAvailable ? "yes" : "no");

                lvDvbResults.Items.Add(item);
            }

            lvDvbResults.Visible = true;
        }

        private void formatAtscResults()
        {
            lvAtscResults.Items.Clear();
            int currentFrequency = -1;

            foreach (ChannelScanResult scanResult in scanResults)
            {
                ListViewItem item = new ListViewItem();

                if (currentFrequency == -1)
                {
                    currentFrequency = scanResult.TuningFrequency.Frequency;
                    item.Text = scanResult.TuningFrequency.Frequency.ToString();
                    item.SubItems.Add(((ChannelTuningFrequency)scanResult.TuningFrequency).ChannelNumber.ToString());
                }
                else
                {
                    if (scanResult.TuningFrequency.Frequency != currentFrequency)
                    {
                        lvAtscResults.Items.Add(new ListViewItem());
                        currentFrequency = scanResult.TuningFrequency.Frequency;
                        item.Text = scanResult.TuningFrequency.Frequency.ToString();
                        item.SubItems.Add(((ChannelTuningFrequency)scanResult.TuningFrequency).ChannelNumber.ToString());
                    }
                    else
                        item.SubItems.Add(string.Empty);
                }

                item.SubItems.Add(scanResult.Channel.Name);
                item.SubItems.Add(scanResult.Channel.ProviderName);                
                item.SubItems.Add(scanResult.Channel.TransportStreamID.ToString());
                item.SubItems.Add(scanResult.Channel.ServiceID.ToString());
                item.SubItems.Add(scanResult.Channel.AtscTSID.ToString());
                item.SubItems.Add(scanResult.Channel.AtscProgramNumber.ToString());
                item.SubItems.Add(scanResult.Channel.AtscServiceType.ToString());
                item.SubItems.Add(scanResult.Channel.AtscHidden ? "yes" : "no");
                item.SubItems.Add(scanResult.Channel.AtscGuideHidden ? "yes" : "no");
                item.SubItems.Add(scanResult.Channel.AtscAccessControlled ? "yes" : "no");
                item.SubItems.Add(scanResult.Channel.AtscOutOfBand ? "yes" : "no");

                lvAtscResults.Items.Add(item);
            }

            lvAtscResults.Visible = true;
        }

        /// <summary>
        /// Prepare to save update data.
        /// </summary>
        /// <returns>False. This function is not implemented.</returns>
        public bool PrepareToSave()
        {
            return (false);
        }

        /// <summary>
        /// Save updated data.
        /// </summary>
        /// <returns>False. This function is not implemented.</returns>
        public bool Save()
        {
            return (false);
        }

        /// <summary>
        /// Save updated data to a file.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>False. This function is not implemented.</returns>
        public bool Save(string fileName)
        {
            return (false);
        }
    }
}

