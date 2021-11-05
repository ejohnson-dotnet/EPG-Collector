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
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Text;

using DomainObjects;
using DirectShow;
using DVBServices;
using XmltvParser;
using MxfParser;
using SatIp;
using VBox;

namespace EPGCentre
{
    internal partial class DVBLogicParametersControl : UserControl, IUpdateControl
    {
        /// <summary>
        /// Get the general window heading for the data.
        /// </summary>
        public string Heading { get { return ("EPG Centre - DVBLogic Plugin Parameters - "); } }
        /// <summary>
        /// Get the default directory.
        /// </summary>
        public string DefaultDirectory { get { return (RunParameters.DataDirectory); } }
        /// <summary>
        /// Get the default output file name.
        /// </summary>
        public string DefaultFileName { get { return (getSelectedFrequency()); } }
        /// <summary>
        /// Get the save file filter.
        /// </summary>
        public string SaveFileFilter { get { return ("INI Files (*.ini)|*.ini"); } }
        /// <summary>
        /// Get the save file title.
        /// </summary>
        public string SaveFileTitle { get { return ("Save EPG DVBLogic Plugin Parameter File"); } }
        /// <summary>
        /// Get the save file suffix.
        /// </summary>
        public string SaveFileSuffix { get { return ("ini"); } }

        /// <summary>
        /// Return true if file is new; false otherwise.
        /// </summary>
        public bool NewFile { get { return (newFile); } }

        /// <summary>
        /// Return the state of the data set.
        /// </summary>
        public DataState DataState { get { return (hasDataChanged()); } }

        private delegate DialogResult ShowMessage(string message, MessageBoxButtons buttons, MessageBoxIcon icon);

        private RunParameters runParameters;
        
        private string currentFileName;
        private RunParameters originalData;
        private bool newFile;

        private const int timeoutLock = 10;
        private const int timeoutCollection = 300;
        private const int timeoutRetries = 5;
        private const int bufferSize = 50;
        private const int bufferFills = 1;

        private BackgroundWorker workerScanStations;
        private AutoResetEvent resetEvent = new AutoResetEvent(false);
        private TuningFrequency scanningFrequency;
        private ChannelScanParameters scanParameters;

        private BindingList<TVStation> bindingList;
        private BindingList<ImportChannelChange> xmltvChannelBindingList;

        private string sortedColumnName;
        private string sortedKeyName;
        private bool sortedAscending;

        private static string currentXmltvOutputPath;
        private static string currentAreaChannelOutputPath;
        private static string currentBladeRunnerOutputPath;
        private static string currentSageTVOutputPath;
        private static string currentLookupBasePath;
        private static string currentXmltvImportPath;
        private static string currentChannelLogoPath;

        internal DVBLogicParametersControl()
        {
            InitializeComponent();

            Satellite.Load();
            TerrestrialProvider.Load();
            CableProvider.Load();
            AtscProvider.Load();
            ClearQamProvider.Load();
        }

        internal void Process()
        {
            runParameters = new RunParameters(ParameterSet.Plugin, RunType.Centre);
            runParameters.OutputFileName = string.Empty;

            currentFileName = null;
            newFile = true;
            start();
        }

        internal void Process(string fileName, bool newFile)
        {
            currentFileName = fileName;
            this.newFile = newFile;

            runParameters = new RunParameters(ParameterSet.Plugin, RunType.Centre);

            Cursor.Current = Cursors.WaitCursor;
            ExitCode reply = runParameters.Process(fileName);
            Cursor.Current = Cursors.Arrow;

            if (!runParameters.OutputFileSet)
                runParameters.OutputFileName = string.Empty;
            
            if (reply != ExitCode.OK)
            {
                MessageBox.Show(runParameters.LastError + Environment.NewLine + Environment.NewLine +
                    "Some parameters may not have been processed.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            originalData = runParameters.Clone();
            start();
        }

        private void start()
        {
            tbcParameters.SelectedTab = tbcParameters.TabPages[0];

            tbcParameters.TabPages.RemoveByKey("tabUpdate");

            initializeTuningTab();
            initializeOutputTab();
            initializeFilesTab();
            initializeChannelsTab();
            initializeTimeShiftTab();
            initializeFiltersTab();
            initializeRepeatsTab();
            initializeAdvancedTab();
            initializeLookupTab();
            initializeDiagnosticsTab();
            initializeUpdateTab();
            initializeXmltvTab();
            initializeEditTab();
        }

        private void initializeTuningTab()
        {
            initializeSatelliteGroup();
            initializeTerrestrialGroup();
            initializeCableGroup();
            initializeAtscGroup();
            initializeClearQamGroup();

            lvSelectedFrequencies.Items.Clear();

            if (runParameters.FrequencyCollection.Count != 0)
            {
                TuningFrequency tuningFrequency = findFrequencyDetails(runParameters.FrequencyCollection[0]);
                if (tuningFrequency == null)
                {
                    enableTuningGroups();
                    return;
                }

                tuningFrequency.AdvancedRunParamters = runParameters.FrequencyCollection[0].AdvancedRunParamters.Clone();

                ListViewItem item = new ListViewItem(tuningFrequency.ToString());
                item.Tag = tuningFrequency.Clone();

                if (tuningFrequency.Provider != null && tuningFrequency.Provider.Name != null)
                    item.SubItems.Add(tuningFrequency.Provider.Name);

                switch (tuningFrequency.TunerType)
                {
                    case TunerType.Satellite:
                        item.SubItems.Add("Satellite");
                        break;
                    case TunerType.Terrestrial:
                        item.SubItems.Add("Terrestrial");
                        break;
                    case TunerType.Cable:
                        item.SubItems.Add("Cable");
                        break;
                    case TunerType.ATSC:
                        item.SubItems.Add("ATSC Terrestrial");
                        break;
                    case TunerType.ATSCCable:
                        item.SubItems.Add("ATSC Cable");
                        break;
                    case TunerType.ClearQAM:
                        item.SubItems.Add("Clear QAM");
                        break;
                    default:
                        item.SubItems.Add("Unknown");
                        break;
                }

                item.SubItems.Add(tuningFrequency.CollectionType.ToString());
                lvSelectedFrequencies.Items.Add(item);
            }
            else
                enableTuningGroups();
        }

        private TuningFrequency findFrequencyDetails(TuningFrequency tuningFrequency)
        {
            switch (tuningFrequency.TunerType)
            {
                case TunerType.Satellite:
                    Provider satelliteProvider = Satellite.FindSatellite(tuningFrequency.Provider.Name);
                    if (satelliteProvider != null)
                    {
                        TuningFrequency frequency = satelliteProvider.FindFrequency(tuningFrequency.Frequency);
                        if (frequency != null)
                            return(frequency.Clone());

                    }
                    break;
                case TunerType.Terrestrial:
                    Provider terrestrialProvider = TerrestrialProvider.FindProvider(tuningFrequency.Provider.Name);
                    if (terrestrialProvider != null)
                    {
                        TuningFrequency frequency = terrestrialProvider.FindFrequency(tuningFrequency.Frequency);
                        if (frequency != null)
                            return(frequency.Clone());

                    }
                    break;
                case TunerType.Cable:
                    Provider cableProvider = CableProvider.FindProvider(tuningFrequency.Provider.Name);
                    if (cableProvider != null)
                    {
                        TuningFrequency frequency = cableProvider.FindFrequency(tuningFrequency.Frequency);
                        if (frequency != null)
                            return(frequency.Clone());

                    }
                    break;
                case TunerType.ATSC:
                case TunerType.ATSCCable:
                    Provider atscProvider = AtscProvider.FindProvider(tuningFrequency.Provider.Name);
                    if (atscProvider != null)
                    {
                        TuningFrequency frequency = atscProvider.FindFrequency(tuningFrequency.Frequency);
                        if (frequency != null)
                            return(frequency.Clone());

                    }
                    break;
                case TunerType.ClearQAM:
                    Provider clearQamProvider = ClearQamProvider.FindProvider(tuningFrequency.Provider.Name);
                    if (clearQamProvider != null)
                    {
                        TuningFrequency frequency = clearQamProvider.FindFrequency(tuningFrequency.Frequency);
                        if (frequency != null)
                            return(frequency.Clone());

                    }
                    break;
                default:
                    break;
            }

            return (null);
        }

        private void enableTuningGroups()
        {
            cboSatellite.Enabled = true;
            cboDVBSScanningFrequency.Enabled = true;

            cboCountry.Enabled = true;
            cboArea.Enabled = true;
            cboDVBTScanningFrequency.Enabled = true;

            cboCable.Enabled = true;
            cboCableScanningFrequency.Enabled = true;

            cboAtsc.Enabled = true;
            cboAtscScanningFrequency.Enabled = true;

            cboClearQam.Enabled = true;
            cboClearQamScanningFrequency.Enabled = true;

            gpSatellite.Enabled = true;
            gpTerrestrial.Enabled = true;
            gpCable.Enabled = true;
            gpAtsc.Enabled = true;
            gpClearQam.Enabled = true;
        }

        private void initializeSatelliteGroup()
        {
            if (cboSatellite.Items.Count == 0)
            {
                foreach (Satellite satellite in Satellite.Providers)
                    cboSatellite.Items.Add(satellite);
            }

            if (cboDVBSCollectionType.Items.Count == 0)
            {
                foreach (CollectionType collectionType in System.Enum.GetValues(typeof(CollectionType)))
                    cboDVBSCollectionType.Items.Add(collectionType);
            }

            if (runParameters.FrequencyCollection.Count != 0)
            {
                SatelliteFrequency satelliteFrequency = runParameters.FrequencyCollection[0] as SatelliteFrequency;
                if (satelliteFrequency != null)
                {
                    cboSatellite.Text = satelliteFrequency.Provider.Name;
                    cboDVBSScanningFrequency.Text = satelliteFrequency.ToString();
                    cboDVBSCollectionType.Text = satelliteFrequency.CollectionType.ToString();

                    cboSatellite.Enabled = false;
                    cboDVBSScanningFrequency.Enabled = false;

                    gpSatellite.Enabled = true;
                    gpTerrestrial.Enabled = false;
                    gpCable.Enabled = false;
                    gpAtsc.Enabled = false;
                    gpClearQam.Enabled = false;
                }
                else
                {
                    cboSatellite.SelectedIndex = 0;
                    cboDVBSScanningFrequency_SelectedIndexChanged(null, null);
                }
            }
            else
            {
                cboSatellite.SelectedIndex = 0;
                cboDVBSScanningFrequency_SelectedIndexChanged(null, null);
            }
        }

        private void initializeTerrestrialGroup()
        {
            if (cboCountry.Items.Count == 0)
            {
                foreach (Country country in TerrestrialProvider.Countries)
                    cboCountry.Items.Add(country);                
            }
            
            if (cboDVBTCollectionType.Items.Count == 0)
            {
                foreach (CollectionType collectionType in System.Enum.GetValues(typeof(CollectionType)))
                    cboDVBTCollectionType.Items.Add(collectionType);                
            }

            if (runParameters.FrequencyCollection.Count != 0)
            {
                TerrestrialFrequency terrestrialFrequency = runParameters.FrequencyCollection[0] as TerrestrialFrequency;
                if (terrestrialFrequency != null)
                {
                    TerrestrialProvider provider = TerrestrialProvider.FindProvider(terrestrialFrequency.Provider.Name);
                    if (provider != null)
                    {
                        cboCountry.Text = provider.Country.Name;
                        cboArea.Text = provider.Area.Name;
                        TuningFrequency providerFrequency = provider.FindFrequency(terrestrialFrequency.Frequency);
                        if (providerFrequency != null)
                        {
                            cboDVBTScanningFrequency.SelectedItem = providerFrequency;
                            cboDVBTCollectionType.Text = terrestrialFrequency.CollectionType.ToString();
                        }
                        else
                            cboDVBTScanningFrequency.SelectedIndex = 0;
                    }
                    else
                        cboCountry.SelectedIndex = 0;

                    cboCountry.Enabled = false;
                    cboArea.Enabled = false;
                    cboDVBTScanningFrequency.Enabled = false;

                    gpSatellite.Enabled = false;
                    gpTerrestrial.Enabled = true;
                    gpCable.Enabled = false;
                    gpAtsc.Enabled = false;
                    gpClearQam.Enabled = false;
                }
                else
                    cboCountry.SelectedIndex = 0;
            }
            else
                cboCountry.SelectedIndex = 0;
        }

        private void initializeCableGroup()
        {
            if (cboCable.Items.Count == 0)
            {
                foreach (CableProvider cableProvider in CableProvider.Providers)
                    cboCable.Items.Add(cableProvider);
            }

            if (cboCableCollectionType.Items.Count == 0)
            {
                foreach (CollectionType collectionType in System.Enum.GetValues(typeof(CollectionType)))
                    cboCableCollectionType.Items.Add(collectionType);
            }

            if (runParameters.FrequencyCollection.Count != 0)
            {
                CableFrequency cableFrequency = runParameters.FrequencyCollection[0] as CableFrequency;
                if (cableFrequency != null)
                {
                    CableProvider provider = CableProvider.FindProvider(cableFrequency.Provider.Name);
                    if (provider != null)
                    {
                        cboCable.SelectedItem = provider;
                        TuningFrequency providerFrequency = provider.FindFrequency(cableFrequency.Frequency);
                        if (providerFrequency != null)
                        {
                            cboCableScanningFrequency.SelectedItem = providerFrequency;
                            cboCableCollectionType.Text = cableFrequency.CollectionType.ToString();
                        }
                        else
                            cboCableScanningFrequency.SelectedIndex = 0;
                    }
                    else
                        cboCable.SelectedIndex = 0;

                    cboCable.Enabled = false;
                    cboCableScanningFrequency.Enabled = false;

                    gpSatellite.Enabled = false;
                    gpTerrestrial.Enabled = false;
                    gpCable.Enabled = true;
                    gpAtsc.Enabled = false;
                    gpClearQam.Enabled = false;
                }
                else
                    cboCable.SelectedIndex = 0;
            }
            else
                cboCable.SelectedIndex = 0;
        }

        private void initializeAtscGroup()
        {
            if (cboAtsc.Items.Count == 0)
            {
                foreach (AtscProvider atscProvider in AtscProvider.Providers)
                    cboAtsc.Items.Add(atscProvider);
            }

            if (cboAtscCollectionType.Items.Count == 0)
            {
                foreach (CollectionType collectionType in System.Enum.GetValues(typeof(CollectionType)))
                    cboAtscCollectionType.Items.Add(collectionType);
            }

            if (runParameters.FrequencyCollection.Count != 0)
            {
                AtscFrequency atscFrequency = runParameters.FrequencyCollection[0] as AtscFrequency;
                if (atscFrequency != null)
                {
                    AtscProvider provider = AtscProvider.FindProvider(atscFrequency.Provider.Name);
                    if (provider != null)
                    {
                        cboAtsc.SelectedItem = provider;
                        TuningFrequency providerFrequency = provider.FindFrequency(atscFrequency.Frequency);
                        if (providerFrequency != null)
                        {
                            cboAtscScanningFrequency.SelectedItem = providerFrequency;
                            cboAtscCollectionType.Text = atscFrequency.CollectionType.ToString();
                        }
                        else
                            cboAtscScanningFrequency.SelectedItem = provider.Frequencies[0];
                    }
                    else
                        cboAtsc.SelectedIndex = 0;

                    cboAtsc.Enabled = false;
                    cboAtscScanningFrequency.Enabled = false;

                    gpSatellite.Enabled = false;
                    gpTerrestrial.Enabled = false;
                    gpCable.Enabled = false;
                    gpAtsc.Enabled = true;
                    gpClearQam.Enabled = false;
                }
                else
                    cboAtsc.SelectedIndex = 0;
            }
            else
                cboAtsc.SelectedIndex = 0;
        }

        private void initializeClearQamGroup()
        {
            if (cboClearQam.Items.Count == 0)
            {
                foreach (ClearQamProvider clearQamProvider in ClearQamProvider.Providers)
                    cboClearQam.Items.Add(clearQamProvider);
            }

            if (cboClearQamCollectionType.Items.Count == 0)
            {
                foreach (CollectionType collectionType in System.Enum.GetValues(typeof(CollectionType)))
                    cboClearQamCollectionType.Items.Add(collectionType);
            }

            if (runParameters.FrequencyCollection.Count != 0)
            {
                ClearQamFrequency clearQamFrequency = runParameters.FrequencyCollection[0] as ClearQamFrequency;
                if (clearQamFrequency != null)
                {
                    ClearQamProvider provider = ClearQamProvider.FindProvider(clearQamFrequency.Provider.Name);
                    if (provider != null)
                    {
                        cboClearQam.SelectedItem = provider;
                        TuningFrequency providerFrequency = provider.FindFrequency(clearQamFrequency.Frequency);
                        if (providerFrequency != null)
                        {
                            cboClearQamScanningFrequency.SelectedItem = providerFrequency;
                            cboClearQamCollectionType.Text = clearQamFrequency.CollectionType.ToString();
                        }
                        else
                            cboClearQamScanningFrequency.SelectedItem = provider.Frequencies[0];
                    }
                    else
                        cboClearQam.SelectedIndex = 0;

                    cboClearQam.Enabled = false;
                    cboClearQamScanningFrequency.Enabled = false;

                    gpSatellite.Enabled = false;
                    gpTerrestrial.Enabled = false;
                    gpCable.Enabled = false;
                    gpAtsc.Enabled = false;
                    gpClearQam.Enabled = true;
                }
                else
                    cboClearQam.SelectedIndex = 0;
            }
            else
                cboClearQam.SelectedIndex = 0;
        }

        private void initializeOutputTab()
        {
            cbAllowBreaks.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.AcceptBreaks);
            cbRoundTime.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.RoundTime);
            cbRemoveExtractedData.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.NoRemoveData);
            cbCreateSameData.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.DuplicateSameChannels);
            cbNoLogExcluded.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.NoLogExcluded);
            cbTcRelevantChannels.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.TcRelevantOnly);
            cbAddSeasonEpisodeToDesc.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.AddSeasonEpisodeToDesc);
            cbNoDataNoFile.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.NoDataNoFile);

            if (OptionEntry.IsDefined(runParameters.Options, OptionName.PluginImport) || (!runParameters.OutputFileSet && !OptionEntry.IsDefined(runParameters.Options, OptionName.WmcImport)))
            {
                cbPluginOutputEnabled.Checked = true;

                if (OptionEntry.IsDefined(runParameters.Options, OptionName.ChannelIdSid))
                    cboTVSourceChannelIDFormat.SelectedItem = cboTVSourceChannelIDFormat.Items[1];
                else
                {
                    if (OptionEntry.IsDefined(runParameters.Options, OptionName.UseChannelId))
                        cboTVSourceChannelIDFormat.SelectedItem = cboTVSourceChannelIDFormat.Items[2];
                    else
                    {
                        if (OptionEntry.IsDefined(runParameters.Options, OptionName.ChannelIdSeqNo))
                            cboTVSourceChannelIDFormat.SelectedItem = cboTVSourceChannelIDFormat.Items[3];
                        else
                            cboTVSourceChannelIDFormat.SelectedItem = cboTVSourceChannelIDFormat.Items[0];
                    }
                }
            }
            else
            {
                cbPluginOutputEnabled.Checked = false;
                resetTVSourceOptions();
            }

            if (runParameters.OutputFileSet)
            {
                cbXmltvOutputEnabled.Checked = true;

                txtOutputFile.Text = runParameters.OutputFileName;

                if (OptionEntry.IsDefined(runParameters.Options, OptionName.ChannelIdSeqNo))
                    cboChannelIDFormat.SelectedItem = cboChannelIDFormat.Items[2];
                else
                {
                    if (OptionEntry.IsDefined(runParameters.Options, OptionName.ChannelIdFullName))
                        cboChannelIDFormat.SelectedItem = cboChannelIDFormat.Items[3];
                    else
                    {
                        if (OptionEntry.IsDefined(runParameters.Options, OptionName.UseChannelId))
                            cboChannelIDFormat.SelectedItem = cboChannelIDFormat.Items[1];
                        else
                            cboChannelIDFormat.SelectedItem = cboChannelIDFormat.Items[0];
                    }
                }

                if (OptionEntry.IsDefined(runParameters.Options, OptionName.UseNumericCrid))
                    cboEpisodeTagFormat.SelectedIndex = 3;
                else
                {
                    if (OptionEntry.IsDefined(runParameters.Options, OptionName.UseRawCrid))
                        cboEpisodeTagFormat.SelectedIndex = 2;
                    else
                    {
                        if (OptionEntry.IsDefined(runParameters.Options, OptionName.UseBsepg))
                            cboEpisodeTagFormat.SelectedIndex = 1;
                        else
                        {
                            if (OptionEntry.IsDefined(runParameters.Options, OptionName.ValidEpisodeTag))
                                cboEpisodeTagFormat.SelectedIndex = 0;
                            else
                            {
                                if (OptionEntry.IsDefined(runParameters.Options, OptionName.NoEpisodeTag))
                                    cboEpisodeTagFormat.SelectedIndex = 4;
                                else
                                    cboEpisodeTagFormat.SelectedIndex = 0;
                            }
                        }
                    }
                }

                cbUseLCN.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.UseLcn);
                cbCreateADTag.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.CreateAdTag);
                cbElementPerTag.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.ElementPerTag);
                cbOmitPartNumber.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.OmitPartNumber);
                cbPrefixDescWithAirDate.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.PrefixDescriptionWithAirDate);
                cbPrefixSubtitleWithSeasonEpisode.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.PrefixSubtitleWithSeasonEpisode);

                tbChannelLogoPath.Text = runParameters.ChannelLogoPath;                
            }
            else
            {
                cbXmltvOutputEnabled.Checked = false;
                resetXmltvOptions();
            }

            if (Environment.OSVersion.Version.Major != 5 || CommandLine.WmcPresent)
            {
                cbWmcOutputEnabled.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.WmcImport);

                if (cbWmcOutputEnabled.Checked)
                {
                    txtImportName.Text = runParameters.WMCImportName;
                    cbAutoMapEPG.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.AutoMapEpg);
                    cbWMCFourStarSpecial.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.WmcStarSpecial);
                    cbDisableInbandLoader.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.DisableInbandLoader);

                    if (OptionEntry.IsDefined(runParameters.Options, OptionName.UseWmcRepeatCheck))
                        cboWMCSeries.SelectedIndex = 1;
                    else
                    {
                        if (OptionEntry.IsDefined(runParameters.Options, OptionName.UseWmcRepeatCheckBroadcast))
                            cboWMCSeries.SelectedIndex = 2;
                        else
                            cboWMCSeries.SelectedIndex = 0;
                    }
                }
                else
                {
                    cbWmcOutputEnabled.Checked = false;
                    resetWMCOptions();
                }
            }
            else
            {
                cbWmcOutputEnabled.Checked = false;
                resetWMCOptions();
            }

            txtImportName.KeyPress -= new KeyPressEventHandler(txtImportName_KeyPressAlphaNumeric);
            txtImportName.KeyPress += new KeyPressEventHandler(txtImportName_KeyPressAlphaNumeric);            
        }

        private void resetTVSourceOptions()
        {
            gpTVSourceOptions.Enabled = false;

            cboTVSourceChannelIDFormat.SelectedItem = cboTVSourceChannelIDFormat.Items[0];
        }

        private void resetXmltvOptions()
        {
            gpXmltvOptions.Enabled = false;

            txtOutputFile.Text = null;
            cboChannelIDFormat.SelectedItem = cboChannelIDFormat.Items[0];
            cboEpisodeTagFormat.SelectedItem = cboEpisodeTagFormat.Items[0];
            cbUseLCN.Checked = false;
            cbCreateADTag.Checked = false;
            cbElementPerTag.Checked = false;
            cbOmitPartNumber.Checked = false;

            tbChannelLogoPath.Text = null;            
        }

        private void resetWMCOptions()
        {
            gpWMCOptions.Enabled = false;

            txtImportName.Text = string.Empty;
            cbAutoMapEPG.Checked = false;
            cbWMCFourStarSpecial.Checked = false;
            cbDisableInbandLoader.Checked = false;
            cboWMCSeries.SelectedIndex = 0;
        }

        private void initializeFilesTab()
        {
            cbBladeRunnerFile.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.CreateBrChannels);
            gpBladeRunnerFile.Enabled = cbBladeRunnerFile.Checked;
            if (cbBladeRunnerFile.Checked)
                tbBladeRunnerFileName.Text = runParameters.BladeRunnerFileName;
            else
                tbBladeRunnerFileName.Text = null;

            cbAreaRegionFile.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.CreateArChannels);
            gpAreaRegionFile.Enabled = cbAreaRegionFile.Checked;
            if (cbAreaRegionFile.Checked)
                tbAreaRegionFileName.Text = runParameters.AreaRegionFileName;
            else
                tbAreaRegionFileName.Text = null;

            cbSageTVFile.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.CreateSageTvFrq);
            gpSageTVFile.Enabled = cbSageTVFile.Checked;
            if (cbSageTVFile.Checked)
                tbSageTVFileName.Text = runParameters.SageTVFileName;
            else
                tbSageTVFileName.Text = null;

            cbSageTVFileNoEPG.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.SageTvOmitNoEpg);

            if (runParameters.SageTVSatelliteNumber != -1)
                tbSageTVSatelliteNumber.Text = runParameters.SageTVSatelliteNumber.ToString();
            else
                tbSageTVSatelliteNumber.Text = string.Empty;
        }

        private void initializeChannelsTab()
        {
            if (runParameters.StationCollection.Count != 0)
            {
                Collection<TVStation> sortedStations = new Collection<TVStation>();

                foreach (TVStation station in runParameters.StationCollection)
                    addInOrder(sortedStations, station, true, "Name");

                bindingList = new BindingList<TVStation>();
                foreach (TVStation station in sortedStations)
                    bindingList.Add(station);

                tVStationBindingSource.DataSource = bindingList;
            }
            else
                tVStationBindingSource.DataSource = null;

            sortedColumnName = "nameColumn";
            sortedKeyName = "Name";
            sortedAscending = true;
        }

        private void initializeTimeShiftTab()
        {
            populatePlusChannels(lbPlusSourceChannel);
            populatePlusChannels(lbPlusDestinationChannel);
            lvPlusSelectedChannels.Items.Clear();

            foreach (TimeOffsetChannel timeOffsetChannel in runParameters.TimeOffsetChannels)
            {
                ListViewItem newItem = new ListViewItem(timeOffsetChannel.SourceChannel.Name);
                newItem.Tag = timeOffsetChannel;
                newItem.SubItems.Add(timeOffsetChannel.DestinationChannel.Name);
                newItem.SubItems.Add(timeOffsetChannel.Offset.ToString());
                lvPlusSelectedChannels.Items.Add(newItem);
            }

            btPlusDelete.Enabled = (lvPlusSelectedChannels.SelectedItems.Count != 0);
        }

        private void initializeFiltersTab()
        {
            lvExcludedIdentifiers.Items.Clear();

            foreach (ChannelFilterEntry filterEntry in runParameters.ChannelFilters)
            {
                ListViewItem newItem = null;

                if (filterEntry.OriginalNetworkID != -1)
                    newItem = new ListViewItem(filterEntry.OriginalNetworkID.ToString());
                else
                    newItem = new ListViewItem(string.Empty);

                newItem.Tag = filterEntry;

                if (filterEntry.TransportStreamID != -1)
                    newItem.SubItems.Add(filterEntry.TransportStreamID.ToString());
                else
                    newItem.SubItems.Add("");
                if (filterEntry.StartServiceID != -1)
                    newItem.SubItems.Add(filterEntry.StartServiceID.ToString());
                else
                    newItem.SubItems.Add("");
                if (filterEntry.EndServiceID != -1)
                    newItem.SubItems.Add(filterEntry.EndServiceID.ToString());
                else
                    newItem.SubItems.Add("");
                lvExcludedIdentifiers.Items.Add(newItem);
            }

            if (runParameters.MaxService != -1)
                tbExcludedMaxChannel.Text = runParameters.MaxService.ToString();
            else
                tbExcludedMaxChannel.Text = string.Empty;
        }

        private void initializeRepeatsTab()
        {
            tbRepeatTitle.Text = string.Empty;
            tbRepeatDescription.Text = string.Empty;
            lvRepeatPrograms.Items.Clear();
            tbPhrasesToIgnore.Text = string.Empty;

            cbCheckForRepeats.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.CheckForRepeats);

            if (!cbCheckForRepeats.Checked)
            {
                cbNoSimulcastRepeats.Checked = false;
                cbNoSimulcastRepeats.Enabled = false;
                cbIgnoreWMCRecordings.Checked = false;
                cbIgnoreWMCRecordings.Enabled = false;
                return;
            }

            cbNoSimulcastRepeats.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.NoSimulcastRepeats);
            cbNoSimulcastRepeats.Enabled = true;
            cbIgnoreWMCRecordings.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.IgnoreWmcRecordings);
            cbIgnoreWMCRecordings.Enabled = true;

            foreach (RepeatExclusion repeatExclusion in runParameters.Exclusions)
            {
                ListViewItem newItem = new ListViewItem(repeatExclusion.Title);
                newItem.SubItems.Add(repeatExclusion.Description);
                lvRepeatPrograms.Items.Add(newItem);
            }

            StringBuilder phrasesToIgnore = new StringBuilder();
            foreach (string phrase in runParameters.PhrasesToIgnore)
            {
                if (phrasesToIgnore.Length != 0)
                    phrasesToIgnore.Append(',');
                phrasesToIgnore.Append(phrase);
            }

            tbPhrasesToIgnore.Text = phrasesToIgnore.ToString();
        }

        private void initializeAdvancedTab()
        {
            cbStoreStationInfo.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.StoreStationInfo);
            cbUseStoredStationInfo.Checked = OptionEntry.IsDefined(runParameters.Options, OptionName.UseStoredStationInfo);
            
            nudDataCollectionTimeout.Value = (decimal)runParameters.FrequencyTimeout.TotalSeconds;
            nudScanRetries.Value = (decimal)runParameters.Repeats;
            nudBufferSize.Value = (decimal)runParameters.BufferSize;
            nudBufferFills.Value = (decimal)runParameters.BufferFills;

            cbManualTime.Checked = runParameters.TimeZoneSet;
            gpTimeAdjustments.Enabled = runParameters.TimeZoneSet;

            if (cbManualTime.Checked)
            {
                nudCurrentOffsetHours.Value = runParameters.TimeZone.Hours;
                nudCurrentOffsetMinutes.Value = runParameters.TimeZone.Minutes;
                nudNextOffsetHours.Value = runParameters.NextTimeZone.Hours;
                nudNextOffsetMinutes.Value = runParameters.NextTimeZone.Minutes;
                tbChangeDate.Text = runParameters.NextTimeZoneChange.Date.ToString("ddMMyy");
                nudChangeHours.Value = runParameters.NextTimeZoneChange.Hour;
                nudChangeMinutes.Value = runParameters.NextTimeZoneChange.Minute;
            }
            else
            {
                nudCurrentOffsetHours.Value = 0;
                nudCurrentOffsetMinutes.Value = 0;
                nudNextOffsetHours.Value = 0;
                nudNextOffsetMinutes.Value = 0;
                tbChangeDate.Text = string.Empty;
                nudChangeHours.Value = 0;
                nudChangeMinutes.Value = 0;
            }
        }

        private void initializeLookupTab()
        {
            cbMovieLookupEnabled.Checked = runParameters.MovieLookupEnabled;
            if (runParameters.DownloadMovieThumbnail == LookupImageType.Thumbnail)
                cboxMovieLookupImageType.SelectedIndex = 0;
            else
            {
                if (runParameters.DownloadMovieThumbnail == LookupImageType.Poster)
                    cboxMovieLookupImageType.SelectedIndex = 1;
                else
                    cboxMovieLookupImageType.SelectedIndex = 2;
            }
            nudLookupMovieLowDuration.Value = runParameters.MovieLowTime;
            nudLookupMovieHighDuration.Value = runParameters.MovieHighTime;

            tbLookupMoviePhrases.Text = string.Empty;
            foreach (string lookupPhrase in runParameters.LookupMoviePhrases)
            {
                if (tbLookupMoviePhrases.Text.Length != 0)
                    tbLookupMoviePhrases.Text = tbLookupMoviePhrases.Text + runParameters.MoviePhraseSeparator + lookupPhrase;
                else
                    tbLookupMoviePhrases.Text = tbLookupMoviePhrases.Text + lookupPhrase;
            }

            cboLookupNotMovie.Items.Clear();
            if (runParameters.LookupNotMovie != null)
            {
                foreach (string notMovie in runParameters.LookupNotMovie)
                    cboLookupNotMovie.Items.Add(notMovie);

                if (cboLookupNotMovie.Items.Count > 0)
                    cboLookupNotMovie.SelectedIndex = 0;
            }
            else
                cboLookupNotMovie.Items.Clear();

            gpMovieLookup.Enabled = runParameters.MovieLookupEnabled;

            cbTVLookupEnabled.Checked = runParameters.TVLookupEnabled;
            switch (runParameters.DownloadTVThumbnail)
            {
                case LookupImageType.Poster:
                    cboxTVLookupImageType.SelectedIndex = 0;
                    break;
                case LookupImageType.Banner:
                    cboxTVLookupImageType.SelectedIndex = 1;
                    break;
                case LookupImageType.Fanart:
                    cboxTVLookupImageType.SelectedIndex = 2;
                    break;
                case LookupImageType.SmallPoster:
                    cboxTVLookupImageType.SelectedIndex = 3;
                    break;
                case LookupImageType.SmallFanart:
                    cboxTVLookupImageType.SelectedIndex = 4;
                    break;
                case LookupImageType.None:
                    cboxTVLookupImageType.SelectedIndex = 5;
                    break;
                default:
                    cboxTVLookupImageType.SelectedIndex = 5;
                    break;
            }
            gpTVLookup.Enabled = runParameters.TVLookupEnabled;

            gpLookupMisc.Enabled = gpMovieLookup.Enabled || gpTVLookup.Enabled;

            cbxLookupMatching.Text = runParameters.LookupMatching.ToString();
            cbLookupNotFound.Checked = runParameters.LookupNotFound;
            cbLookupReload.Checked = runParameters.LookupReload;
            cbLookupIgnoreCategories.Checked = runParameters.LookupIgnoreCategories;
            cbLookupProcessAsTVSeries.Checked = runParameters.LookupProcessAsTVSeries;
            nudLookupTime.Value = runParameters.LookupTimeLimit;
            nudLookupErrors.Value = runParameters.LookupErrorLimit;
            
            tbLookupIgnoredPhrases.Text = string.Empty;
            foreach (string lookupPhrase in runParameters.LookupIgnoredPhrases)
            {
                if (tbLookupIgnoredPhrases.Text.Length != 0)
                    tbLookupIgnoredPhrases.Text = tbLookupIgnoredPhrases.Text + runParameters.LookupIgnoredPhraseSeparator + lookupPhrase;
                else
                    tbLookupIgnoredPhrases.Text = tbLookupIgnoredPhrases.Text + lookupPhrase;
            }

            tbLookupImagePath.Text = runParameters.LookupImagePath;
            
            udIgnorePhraseSeparator.Text = runParameters.LookupIgnoredPhraseSeparator;
            udMoviePhraseSeparator.Text = runParameters.MoviePhraseSeparator;
        }

        private void initializeDiagnosticsTab()
        {
            tbDebugIDs.Text = string.Empty;
            foreach (DebugEntry debugEntry in runParameters.DebugIDs)
            {
                if (tbDebugIDs.Text.Length != 0)
                    tbDebugIDs.Text = tbDebugIDs.Text + "," + debugEntry;
                else
                    tbDebugIDs.Text = tbDebugIDs.Text + debugEntry;
            }

            tbTraceIDs.Text = string.Empty;
            foreach (TraceEntry traceEntry in runParameters.TraceIDs)
            {
                if (tbTraceIDs.Text.Length != 0)
                    tbTraceIDs.Text = tbTraceIDs.Text + "," + traceEntry;
                else
                    tbTraceIDs.Text = tbTraceIDs.Text + traceEntry;
            }
        }

        private void initializeUpdateTab()
        {
            gpDVBLink.Enabled = runParameters.ChannelUpdateEnabled;

            cbDVBLinkUpdateEnabled.Checked = runParameters.ChannelUpdateEnabled;

            switch (runParameters.ChannelMergeMethod)
            {
                case ChannelMergeMethod.None:
                    cboMergeMethod.SelectedIndex = 0;
                    break;
                case ChannelMergeMethod.Name:
                    cboMergeMethod.SelectedIndex = 1;
                    break;                
                case ChannelMergeMethod.Number:
                    cboMergeMethod.SelectedIndex = 2;
                    break;
                case ChannelMergeMethod.NameNumber:
                    cboMergeMethod.SelectedIndex = 3;
                    break;
                default:
                    cboMergeMethod.SelectedIndex = 0;
                    break;
            }
            cboMergeMethod.Text = cboMergeMethod.Items[cboMergeMethod.SelectedIndex].ToString();

            switch (runParameters.ChannelEPGScanner)
            {
                case ChannelEPGScanner.EITScanner:
                    cboEPGScanner.SelectedIndex = 3;
                    break;
                case ChannelEPGScanner.EPGCollector:
                    cboEPGScanner.SelectedIndex = 2;
                    break;
                case ChannelEPGScanner.None:
                    cboEPGScanner.SelectedIndex = 0;
                    break;
                case ChannelEPGScanner.Default:
                    cboEPGScanner.SelectedIndex = 1;
                    break;
                case ChannelEPGScanner.Xmltv:
                    cboEPGScanner.SelectedIndex = 4;
                    break;
                default:
                    cboEPGScanner.SelectedIndex = 0;
                    break;
            }
            cboEPGScanner.Text = cboEPGScanner.Items[cboEPGScanner.SelectedIndex].ToString();

            cbChildLock.Checked = runParameters.ChannelChildLock;
            cbLogNetworkMap.Checked = runParameters.ChannelLogNetworkMap;
        }

        private void initializeXmltvTab()
        {
            btXmltvDelete.Enabled = false;
            btXmltvLoadFiles.Enabled = false;
            btXmltvClear.Enabled = false;
            btXmltvIncludeAll.Enabled = false;
            btXmltvExcludeAll.Enabled = false;

            if (cboXmltvLanguage.Items.Count == 0)
            {
                foreach (LanguageCode languageCode in LanguageCode.LanguageCodeList)
                    cboXmltvLanguage.Items.Add(languageCode);
            }
            cboXmltvLanguage.SelectedIndex = 0;

            cboXmltvPrecedence.SelectedIndex = 0;
            cboXmltvIdFormat.SelectedIndex = 0;

            lvXmltvSelectedFiles.Items.Clear();

            if (runParameters.ImportFiles != null)
            {
                foreach (ImportFileSpec importFileSpec in runParameters.ImportFiles)
                {
                    ListViewItem item = new ListViewItem(importFileSpec.FileName);
                    item.Tag = importFileSpec;

                    if (importFileSpec.Language != null)
                        item.SubItems.Add(importFileSpec.Language.Description);
                    else
                        item.SubItems.Add(string.Empty);

                    item.SubItems.Add(importFileSpec.Precedence.ToString());
                    item.SubItems.Add(importFileSpec.NoLookup ? "Yes" : "No");
                    item.SubItems.Add(importFileSpec.AppendOnly ? "Yes" : "No");

                    lvXmltvSelectedFiles.Items.Add(item);
                }

                btXmltvDelete.Enabled = lvXmltvSelectedFiles.Items.Count != 0;
                btXmltvLoadFiles.Enabled = lvXmltvSelectedFiles.Items.Count != 0;
            }

            if (runParameters.ImportChannelChanges != null)
            {
                xmltvChannelBindingList = new BindingList<ImportChannelChange>();
                foreach (ImportChannelChange channelChange in runParameters.ImportChannelChanges)
                    addChannelChange(xmltvChannelBindingList, channelChange);

                xmltvChannelChangeBindingSource.DataSource = xmltvChannelBindingList;
                dgXmltvChannelChanges.DataSource = xmltvChannelChangeBindingSource;

                btXmltvClear.Enabled = true;
                btXmltvIncludeAll.Enabled = true;
                btXmltvExcludeAll.Enabled = true;
            }
            else
            {
                dgXmltvChannelChanges.DataSource = null;
                xmltvChannelBindingList = null;
            }
        }

        private void initializeEditTab()
        {
            btEditDelete.Enabled = false;

            tbEditText.Text = null;
            tbEditReplacementText.Text = null;
            cboEditLocation.Text = cboEditLocation.Items[0].ToString();
            cboEditApplyTo.Text = cboEditApplyTo.Items[2].ToString();

            lvEditSpecs.Items.Clear();

            if (runParameters.EditSpecs != null)
            {
                foreach (EditSpec editSpec in runParameters.EditSpecs)
                {
                    ListViewItem item = new ListViewItem(editSpec.Text);
                    item.Tag = editSpec;

                    if (editSpec.ApplyToTitles)
                    {
                        if (editSpec.ApplyToDescriptions)
                            item.SubItems.Add("Titles and descriptions");
                        else
                            item.SubItems.Add("Titles only");
                    }
                    else
                    {
                        if (editSpec.ApplyToDescriptions)
                            item.SubItems.Add("Descriptions only");
                    }

                    item.SubItems.Add(editSpec.Location.ToString());

                    if (editSpec.ReplacementText != null)
                        item.SubItems.Add(editSpec.ReplacementText);
                    else
                        item.SubItems.Add(string.Empty);

                    switch (editSpec.ReplacementMode)
                    {
                        case TextReplacementMode.TextOnly:
                            item.SubItems.Add("Text only");
                            break;
                        case TextReplacementMode.TextAndFollowing:
                            item.SubItems.Add("Text and following");
                            break;
                        case TextReplacementMode.TextAndPreceeding:
                            item.SubItems.Add("Text and preceeding");
                            break;
                        case TextReplacementMode.Everything:
                            item.SubItems.Add("Everything");
                            break;
                        default:
                            item.SubItems.Add("Text only");
                            break;
                    }

                    lvEditSpecs.Items.Add(item);
                }

                btEditDelete.Enabled = false;
            }
        }

        private void cboSatellite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSatellite.SelectedItem != null)
            {
                cboDVBSScanningFrequency.Items.Clear();
                foreach (TuningFrequency tuningFrequency in ((Satellite)cboSatellite.SelectedItem).Frequencies)
                    cboDVBSScanningFrequency.Items.Add(tuningFrequency);
                cboDVBSScanningFrequency.SelectedIndex = 0;
            }
        }

        private void cboDVBSScanningFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            TuningFrequency tuningFrequency = cboDVBSScanningFrequency.SelectedItem as TuningFrequency;
            cboDVBSCollectionType.Text = tuningFrequency.CollectionType.ToString();
        }

        private void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCountry.SelectedItem != null)
            {
                cboArea.Items.Clear();
                foreach (Area area in ((Country)cboCountry.SelectedItem).Areas)
                    cboArea.Items.Add(area);
                cboArea.SelectedIndex = 0;
            }
        }

        private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            TerrestrialProvider provider = TerrestrialProvider.FindProvider(cboCountry.Text, cboArea.Text);
            if (provider == null)
                return;

            cboDVBTScanningFrequency.Items.Clear();
            foreach (TuningFrequency tuningFrequency in provider.Frequencies)
                cboDVBTScanningFrequency.Items.Add(tuningFrequency);
            cboDVBTScanningFrequency.SelectedIndex = 0;
        }

        private void cboDVBTScanningFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            TuningFrequency tuningFrequency = cboDVBTScanningFrequency.SelectedItem as TuningFrequency;
            cboDVBTCollectionType.Text = tuningFrequency.CollectionType.ToString();
        }

        private void cboCable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCable.SelectedItem != null)
            {
                cboCableScanningFrequency.Items.Clear();
                foreach (TuningFrequency tuningFrequency in ((CableProvider)cboCable.SelectedItem).Frequencies)
                    cboCableScanningFrequency.Items.Add(tuningFrequency);
                cboCableScanningFrequency.SelectedIndex = 0;
            }
        }

        private void cboCableScanningFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            TuningFrequency tuningFrequency = cboCableScanningFrequency.SelectedItem as TuningFrequency;
            cboCableCollectionType.Text = tuningFrequency.CollectionType.ToString();
        }

        private void cboAtsc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAtsc.SelectedItem != null)
            {
                cboAtscScanningFrequency.Items.Clear();
                foreach (TuningFrequency tuningFrequency in ((AtscProvider)cboAtsc.SelectedItem).Frequencies)
                    cboAtscScanningFrequency.Items.Add(tuningFrequency);
                cboAtscScanningFrequency.SelectedIndex = 0;
            }
        }

        private void cboAtscScanningFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            TuningFrequency tuningFrequency = cboAtscScanningFrequency.SelectedItem as TuningFrequency;
            cboAtscCollectionType.Text = tuningFrequency.CollectionType.ToString();
        }

        private void cboClearQam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboClearQam.SelectedItem != null)
            {
                cboClearQamScanningFrequency.Items.Clear();
                foreach (TuningFrequency tuningFrequency in ((ClearQamProvider)cboClearQam.SelectedItem).Frequencies)
                    cboClearQamScanningFrequency.Items.Add(tuningFrequency);
                cboClearQamScanningFrequency.SelectedIndex = 0;
            }
        }

        private void cboClearQamScanningFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            TuningFrequency tuningFrequency = cboClearQamScanningFrequency.SelectedItem as TuningFrequency;
            cboClearQamCollectionType.Text = tuningFrequency.CollectionType.ToString();
        }

        private void cbPluginOutputEnabled_CheckedChanged(object sender, EventArgs e)
        {            
            gpTVSourceOptions.Enabled = cbPluginOutputEnabled.Checked;

            if (cbPluginOutputEnabled.Checked)
            {
                cbWmcOutputEnabled.Checked = false;
                gpWMCOptions.Enabled = false;
            }
        }

        private void cbXmltvOutputEnabled_CheckedChanged(object sender, EventArgs e)
        {
            gpXmltvOptions.Enabled = cbXmltvOutputEnabled.Checked;
        }

        private void cbWmcOutputEnabled_CheckedChanged(object sender, EventArgs e)
        {
            gpWMCOptions.Enabled = cbWmcOutputEnabled.Checked;

            if (cbWmcOutputEnabled.Checked)
            {
                cbPluginOutputEnabled.Checked = false;
                gpTVSourceOptions.Enabled = false;
            }
        }

        private void txtImportName_KeyPressAlphaNumeric(object sender, KeyPressEventArgs e)
        {
            Regex alphaNumericPattern = new Regex(@"[a-zA-Z0-9\s\b]");
            e.Handled = !alphaNumericPattern.IsMatch(e.KeyChar.ToString());
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseFile = new FolderBrowserDialog();
            browseFile.Description = "EPG Centre - Find Output File Directory";
            if (currentXmltvOutputPath == null)
                browseFile.SelectedPath = RunParameters.DataDirectory;
            else
                browseFile.SelectedPath = currentXmltvOutputPath;
            DialogResult result = browseFile.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            currentXmltvOutputPath = browseFile.SelectedPath;

            if (!browseFile.SelectedPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                txtOutputFile.Text = Path.Combine(browseFile.SelectedPath, "TVGuide.xml");
            else
                txtOutputFile.Text = browseFile.SelectedPath + "TVGuide.xml";
        }

        private void cbStoreStationInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStoreStationInfo.Checked)
                cbUseStoredStationInfo.Checked = false;
        }

        private void cbUseStoredStationInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUseStoredStationInfo.Checked)
                cbStoreStationInfo.Checked = false;
        }

        private void onCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (bindingList[e.RowIndex].ExcludedByUser)
                e.CellStyle.ForeColor = Color.Red;
        }

        private void dgServices_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgServices.CurrentCell.ColumnIndex == dgServices.Columns["newNameColumn"].Index)
            {
                TextBox textEdit = e.Control as TextBox;
                textEdit.KeyPress -= new KeyPressEventHandler(textEdit_KeyPressAlphaNumeric);
                textEdit.KeyPress += new KeyPressEventHandler(textEdit_KeyPressAlphaNumeric);
            }            
        }

        private void textEdit_KeyPressAlphaNumeric(object sender, KeyPressEventArgs e)
        {
            Regex alphaNumericPattern = new Regex(@"[a-zA-Z0-9!&*()-+?\s\b]");
            e.Handled = !alphaNumericPattern.IsMatch(e.KeyChar.ToString());
        }

        private void dgServices_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgServices.IsCurrentCellDirty && dgServices.Columns[dgServices.CurrentCell.ColumnIndex].Name == "excludedByUserColumn")
                dgServices.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgServices_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgServices.Columns[e.ColumnIndex].Name != "excludedByUserColumn")
                return;

            if (bindingList[e.RowIndex].ExcludedByUser)
            {
                foreach (DataGridViewCell cell in dgServices.Rows[e.RowIndex].Cells)
                {
                    cell.Style.ForeColor = Color.Red;
                    cell.Style.SelectionForeColor = Color.Red;
                }
            }
            else
            {
                foreach (DataGridViewCell cell in dgServices.Rows[e.RowIndex].Cells)
                {
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionForeColor = Color.White;
                }
            }

            dgServices.Invalidate();
        }

        private void dgServices_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgServices.EndEdit();

            if (sortedColumnName == null)
            {
                sortedAscending = true;
                sortedColumnName = dgServices.Columns[e.ColumnIndex].Name;
            }
            else
            {
                if (sortedColumnName == dgServices.Columns[e.ColumnIndex].Name)
                    sortedAscending = !sortedAscending;
                else
                    sortedColumnName = dgServices.Columns[e.ColumnIndex].Name;
            }

            Collection<TVStation> sortedStations = new Collection<TVStation>();

            foreach (TVStation station in bindingList)
            {
                switch (dgServices.Columns[e.ColumnIndex].Name)
                {
                    case "nameColumn":
                        addInOrder(sortedStations, station, sortedAscending, "Name");
                        break;
                    case "originalNetworkIDColumn":
                        addInOrder(sortedStations, station, sortedAscending, "ONID");
                        break;
                    case "transportStreamIDColumn":
                        addInOrder(sortedStations, station, sortedAscending, "TSID");
                        break;
                    case "serviceIDColumn":
                        addInOrder(sortedStations, station, sortedAscending, "SID");
                        break;
                    case "excludedByUserColumn":
                        addInOrder(sortedStations, station, sortedAscending, "ExcludedByUser");
                        break;
                    case "newNameColumn":
                        addInOrder(sortedStations, station, sortedAscending, "NewName");
                        break;
                    default:
                        return;
                }
            }

            bindingList = new BindingList<TVStation>();
            foreach (TVStation station in sortedStations)
                bindingList.Add(station);

            tVStationBindingSource.DataSource = bindingList;
        }

        private void addInOrder(Collection<TVStation> stations, TVStation newStation, bool sortedAscending, string keyName)
        {
            sortedKeyName = keyName;

            foreach (TVStation oldStation in stations)
            {
                if (sortedAscending)
                {
                    if (oldStation.CompareForSorting(newStation, keyName) > 0)
                    {
                        stations.Insert(stations.IndexOf(oldStation), newStation);
                        return;
                    }
                }
                else
                {
                    if (oldStation.CompareForSorting(newStation, keyName) < 0)
                    {
                        stations.Insert(stations.IndexOf(oldStation), newStation);
                        return;
                    }
                }
            }

            stations.Add(newStation);
        }

        private void cmdClearScan_Click(object sender, EventArgs e)
        {
            bindingList.Clear();
            runParameters.StationCollection.Clear();
        }

        private void tbcParametersDeselecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabChannels && pbarChannels.Enabled)
                e.Cancel = true;
        }

        private void cmdScan_Click(object sender, EventArgs e)
        {
            if (cmdScan.Text == "Stop Scan")
            {
                Logger.Instance.Write("Stop scan requested");
                workerScanStations.CancelAsync();
                bool reply = resetEvent.WaitOne(new TimeSpan(0, 0, 45));
                cmdClearScan.Enabled = (bindingList != null && bindingList.Count == 0);
                cmdScan.Text = "Start Scan";
                cmdSelectAll.Enabled = true;
                cmdSelectNone.Enabled = true;
                lblScanning.Visible = false;
                pbarChannels.Visible = false;
                pbarChannels.Enabled = false;

                btPlusScan.Text = "Start Scan";
                lblPlusScanning.Visible = false;
                pbarPlusScan.Enabled = false;
                pbarPlusScan.Visible = false;

                MainWindow.ChangeMenuItemAvailability(true);

                return;
            }

            Logger.Instance.Write("Scan requested");
            scanningFrequency = getPluginFrequency();
            if (scanningFrequency == null)
            {
                MessageBox.Show("No frequency selected on the Tuning tab.",
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.Hide();

            scanParameters = new ChannelScanParameters(scanningFrequency);
            DialogResult result = scanParameters.ShowDialog();

            this.Show();

            if (result == DialogResult.Cancel)
                return;

            if (!validateData())
                return;
            
            cmdClearScan.Enabled = false;
            cmdScan.Text = "Stop Scan";
            cmdSelectAll.Enabled = false;
            cmdSelectNone.Enabled = false;
            lblScanning.Visible = true;
            pbarChannels.Visible = true;
            pbarChannels.Enabled = true;            

            btPlusScan.Text = "Stop Scan";
            lblPlusScanning.Visible = true;
            pbarPlusScan.Enabled = true;
            pbarPlusScan.Visible = true;

            MainWindow.ChangeMenuItemAvailability(false);

            setRunParameters();
            RunParameters.Instance = runParameters;            

            workerScanStations = new BackgroundWorker();
            workerScanStations.WorkerReportsProgress = true;
            workerScanStations.WorkerSupportsCancellation = true;
            workerScanStations.DoWork += new DoWorkEventHandler(scanStationsDoScan);
            workerScanStations.RunWorkerCompleted += new RunWorkerCompletedEventHandler(scanStationsRunWorkerCompleted);
            workerScanStations.ProgressChanged += new ProgressChangedEventHandler(scanStationsProgressChanged);
            workerScanStations.RunWorkerAsync(scanParameters.SelectedFrequency);
        }

        private void scanStationsProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblScanning.Text = "Scanning " + scanningFrequency;
            lblPlusScanning.Text = "Scanning " + scanningFrequency;
        }

        private void scanStationsDoScan(object sender, DoWorkEventArgs e)
        {
            TuningFrequency frequency = e.Argument as TuningFrequency;
            RunParameters.Instance.CurrentFrequency = frequency;

            Logger.Instance.Write("Scanning frequency " + frequency.ToString() + " on " + frequency.TunerType);
            (sender as BackgroundWorker).ReportProgress(0);

            TunerNodeType tunerNodeType;
            TuningSpec tuningSpec;

            SatelliteFrequency satelliteFrequency = frequency as SatelliteFrequency;
            if (satelliteFrequency != null)
            {
                tunerNodeType = TunerNodeType.Satellite;
                tuningSpec = new TuningSpec((Satellite)satelliteFrequency.Provider, satelliteFrequency);
            }
            else
            {
                TerrestrialFrequency terrestrialFrequency = frequency as TerrestrialFrequency;
                if (terrestrialFrequency != null)
                {
                    tunerNodeType = TunerNodeType.Terrestrial;
                    tuningSpec = new TuningSpec(terrestrialFrequency);
                }
                else
                {
                    CableFrequency cableFrequency = frequency as CableFrequency;
                    if (cableFrequency != null)
                    {
                        tunerNodeType = TunerNodeType.Cable;
                        tuningSpec = new TuningSpec((CableFrequency)frequency);
                    }
                    else
                    {
                        AtscFrequency atscFrequency = frequency as AtscFrequency;
                        if (atscFrequency != null)
                        {
                            if (atscFrequency.TunerType == TunerType.ATSC)
                                tunerNodeType = TunerNodeType.ATSC;
                            else
                                tunerNodeType = TunerNodeType.Cable;
                            tuningSpec = new TuningSpec((AtscFrequency)frequency);
                        }
                        else
                        {
                            ClearQamFrequency clearQamFrequency = frequency as ClearQamFrequency;
                            if (clearQamFrequency != null)
                            {
                                tunerNodeType = TunerNodeType.Cable;
                                tuningSpec = new TuningSpec((ClearQamFrequency)frequency);
                            }
                            else
                                throw (new InvalidOperationException("Tuning frequency not recognized"));
                        }                    
                    }
                }
            }
            
            Tuner currentTuner = null;
            bool finished = false;

            while (!finished)
            {
                if ((sender as BackgroundWorker).CancellationPending)
                {
                    Logger.Instance.Write("Scan abandoned by user");
                    e.Cancel = true;
                    resetEvent.Set();
                    return;
                }

                ITunerDataProvider graph = BDAGraph.FindTuner(frequency.SelectedTuners, tunerNodeType, tuningSpec, currentTuner);
                if (graph == null)
                {
                    graph = SatIpController.FindReceiver(frequency.SelectedTuners, tunerNodeType, tuningSpec, currentTuner, getDiseqcSetting(tuningSpec.Frequency));
                    if (graph == null)
                    {
                        Logger.Instance.Write("<e> No tuner able to tune frequency " + frequency.ToString());

                        dgServices.Invoke(new ShowMessage(showMessage), "No tuner able to tune frequency " + frequency.ToString(),
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        finished = true;
                    }
                }
                
                if (!finished)
                {
                    string tuneReply = checkTuning(graph, frequency, sender as BackgroundWorker);
                    
                    if ((sender as BackgroundWorker).CancellationPending)
                    {
                        Logger.Instance.Write("Scan abandoned by user");
                        graph.Dispose();
                        e.Cancel = true;
                        resetEvent.Set();
                        return;
                    }

                    if (tuneReply == null)
                    {
                        getStations((ISampleDataProvider)graph, frequency, sender as BackgroundWorker);
                        graph.Dispose();
                        finished = true;
                    }
                    else
                    {
                        Logger.Instance.Write("Failed to tune frequency " + frequency.ToString());
                        graph.Dispose();
                        currentTuner = graph.Tuner;
                    }
                }
            }

            e.Cancel = true;
            resetEvent.Set();
        }

        private static int getDiseqcSetting(TuningFrequency frequency)
        {
            SatelliteFrequency satelliteFrequency = frequency as SatelliteFrequency;
            if (satelliteFrequency == null)
                return (0);

            if (satelliteFrequency.DiseqcRunParamters.DiseqcSwitch == null)
                return (0);

            switch (satelliteFrequency.DiseqcRunParamters.DiseqcSwitch)
            {
                case "A":
                    return (1);
                case "B":
                    return (2);
                case "AA":
                    return (1);
                case "AB":
                    return (2);
                case "BA":
                    return (3);
                case "BB":
                    return (4);
                default:
                    return (0);
            }
        }

        private DialogResult showMessage(string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            lblScanning.Visible = false;
            pbarChannels.Enabled = false;
            pbarChannels.Visible = false;

            lblPlusScanning.Visible = false;
            pbarPlusScan.Enabled = false;
            pbarPlusScan.Visible = false;

            DialogResult result = MessageBox.Show(message, "EPG Centre", buttons, icon);
            if (result == DialogResult.Yes)
            {
                lblScanning.Visible = true;
                pbarChannels.Enabled = true;
                pbarChannels.Visible = true;

                lblPlusScanning.Visible = true;
                pbarPlusScan.Enabled = true;
                pbarPlusScan.Visible = true;
            }

            return (result);
        }

        private string checkTuning(ITunerDataProvider graph, TuningFrequency frequency, BackgroundWorker worker)
        {
            TimeSpan timeout = new TimeSpan();
            bool done = false;
            bool locked = false;
            int frequencyRetries = 0;

            while (!done)
            {
                if (worker.CancellationPending)
                {
                    Logger.Instance.Write("Scan abandoned by user");
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
                            done = (timeout.TotalSeconds == runParameters.LockTimeout.TotalSeconds);
                        }
                    }

                    if (done)
                    {
                        done = (frequencyRetries == 2);
                        if (done)
                            Logger.Instance.Write("<e> Failed to acquire signal");
                        else
                        {
                            Logger.Instance.Write("Retrying frequency");
                            timeout = new TimeSpan();
                            frequencyRetries++;
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
                return ("<e> The tuner failed to acquire a signal for frequency " + frequency.ToString());
        }

        private bool getStations(ISampleDataProvider graph, TuningFrequency frequency, BackgroundWorker worker)
        {
            FrequencyScanner frequencyScanner;

            if (frequency.CollectionType != CollectionType.FreeSat)
                frequencyScanner = new FrequencyScanner(graph, worker, true);
            else
                frequencyScanner = new FrequencyScanner(graph, new int[] { 0xbba }, true, worker);

            Collection<TVStation> stations = frequencyScanner.FindTVStations();

            int addedCount = 0;

            if (stations != null)
            {
                foreach (TVStation tvStation in stations)
                {
                    TVStation existingStation = TVStation.FindStation(runParameters.StationCollection, 
                        tvStation.OriginalNetworkID, tvStation.TransportStreamID, tvStation.ServiceID);
                    if (existingStation == null)
                    {
                        tvStation.CollectionType = frequency.CollectionType;
                        bool added = TVStation.AddStation(runParameters.StationCollection, tvStation);
                        if (added)
                        {
                            Logger.Instance.Write("Included station: " + tvStation.FixedLengthName + " (" + tvStation.FullID + " Service type " + tvStation.ServiceType + ")");
                            addedCount++;
                        }
                    }
                    else
                        existingStation.Name = tvStation.Name;
                }

                Logger.Instance.Write("Added " + addedCount + " stations for frequency " + frequency);
            }

            return (true);
        }

        private void scanStationsRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cmdScan.Text = "Start Scan";
            btPlusScan.Text = "Start Scan";

            if (e.Error != null)
                throw new InvalidOperationException("Background worker failed - see inner exception", e.Error);

            lblScanning.Visible = false;
            pbarChannels.Enabled = false;
            pbarChannels.Visible = false;

            lblPlusScanning.Visible = false;
            pbarPlusScan.Enabled = false;
            pbarPlusScan.Visible = false;

            MainWindow.ChangeMenuItemAvailability(true);

            populateServicesGrid();
            populatePlusChannels(lbPlusSourceChannel);
            populatePlusChannels(lbPlusDestinationChannel);
        }

        private void populateServicesGrid()
        {
            if (runParameters.StationCollection.Count != 0)
            {
                Collection<TVStation> sortedStations = new Collection<TVStation>();

                foreach (TVStation station in runParameters.StationCollection)
                    addInOrder(sortedStations, station, sortedAscending, sortedKeyName);

                bindingList = new BindingList<TVStation>();
                foreach (TVStation station in sortedStations)
                    bindingList.Add(station);

                tVStationBindingSource.DataSource = bindingList;

                dgServices.DataSource = tVStationBindingSource;
                cmdSelectAll.Enabled = true;
                cmdSelectNone.Enabled = true;
                cmdClearScan.Enabled = true;

                MessageBox.Show("The scan for channels is complete." + Environment.NewLine + Environment.NewLine +
                    "There are now " + bindingList.Count + " channels in the list.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                cmdScan.Enabled = true;
                cmdSelectAll.Enabled = false;
                cmdSelectNone.Enabled = false;
                cmdClearScan.Enabled = false;
            }
        }

        private void cmdIncludeAll_Click(object sender, EventArgs e)
        {
            foreach (TVStation station in bindingList)
                station.ExcludedByUser = false;

            foreach (DataGridViewRow row in dgServices.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionForeColor = Color.White;
                }
            }
        }

        private void cmdExcludeAll_Click(object sender, EventArgs e)
        {
            foreach (TVStation station in bindingList)
                station.ExcludedByUser = true;

            foreach (DataGridViewRow row in dgServices.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.ForeColor = Color.Red;
                    cell.Style.SelectionForeColor = Color.Red;
                }
            }
        }

        private void populatePlusChannels(ListBox listBox)
        {
            listBox.Items.Clear();

            foreach (TVStation station in runParameters.StationCollection)
                listBox.Items.Add(station);
        }

        private void btPlusAdd_Click(object sender, EventArgs e)
        {
            if ((TVStation)lbPlusSourceChannel.SelectedItem == (TVStation)lbPlusDestinationChannel.SelectedItem)
            {
                showErrorMessage("The source and destination channels must be different");
                return;
            }

            TimeOffsetChannel newChannel = new TimeOffsetChannel((TVStation)lbPlusSourceChannel.SelectedItem,
                (TVStation)lbPlusDestinationChannel.SelectedItem,
                (int)nudPlusIncrement.Value);

            ListViewItem newItem = new ListViewItem(newChannel.SourceChannel.Name);
            newItem.Tag = newChannel;
            newItem.SubItems.Add(newChannel.DestinationChannel.Name);
            newItem.SubItems.Add(newChannel.Offset.ToString());

            foreach (ListViewItem oldItem in lvPlusSelectedChannels.Items)
            {
                int index = lvPlusSelectedChannels.Items.IndexOf(oldItem);

                TimeOffsetChannel oldChannel = oldItem.Tag as TimeOffsetChannel;

                if (oldChannel.SourceChannel.Name == newChannel.SourceChannel.Name &&
                    oldChannel.DestinationChannel.Name == newChannel.DestinationChannel.Name)
                {
                    if (oldChannel.Offset == newChannel.Offset)
                    {
                        MessageBox.Show("The channels have already been selected with the same offset.",
                            "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("The channels have already been selected with a different offset." + Environment.NewLine + Environment.NewLine +
                            "Do you want to overwrite the existing entry?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        switch (result)
                        {
                            case DialogResult.Yes:
                                lvPlusSelectedChannels.Items.Remove(oldItem);
                                lvPlusSelectedChannels.Items.Insert(index, newItem);
                                return;
                            default:
                                return;
                        }
                    }
                }
            }

            lvPlusSelectedChannels.Items.Add(newItem);
        }

        private void lvPlusSelectedChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            btPlusDelete.Enabled = (lvPlusSelectedChannels.SelectedItems.Count != 0);
        }

        private void btPlusDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvPlusSelectedChannels.SelectedItems)
                lvPlusSelectedChannels.Items.Remove(item);

            btPlusDelete.Enabled = (lvPlusSelectedChannels.SelectedItems.Count != 0);
        }

        private void btExcludeAdd_Click(object sender, EventArgs e)
        {
            int originalNetworkID = -1;
            int transportStreamID = -1;
            int startServiceID = -1;
            int endServiceID = -1;

            if (tbExcludeONID.Text.Length != 0)
            {
                try
                {
                    originalNetworkID = Int32.Parse(tbExcludeONID.Text.Trim());
                }
                catch (FormatException)
                {
                    MessageBox.Show("The original network ID is incorrect.",
                        "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("The original network ID is incorrect.",
                        "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (tbExcludeTSID.Text.Length != 0)
            {
                try
                {
                    transportStreamID = Int32.Parse(tbExcludeTSID.Text.Trim());
                }
                catch (FormatException)
                {
                    MessageBox.Show("The transport stream ID is incorrect.",
                        "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("The transport stream ID is incorrect.",
                        "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (tbExcludeSIDStart.Text.Length != 0)
            {
                try
                {
                    startServiceID = Int32.Parse(tbExcludeSIDStart.Text.Trim());
                }
                catch (FormatException)
                {
                    MessageBox.Show("The start service ID is incorrect.",
                        "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("The start service ID is incorrect.",
                        "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (tbExcludeSIDEnd.Text.Length != 0)
            {
                try
                {
                    endServiceID = Int32.Parse(tbExcludeSIDEnd.Text.Trim());
                }
                catch (FormatException)
                {
                    MessageBox.Show("The end service ID is incorrect.",
                        "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("The end service ID is incorrect.",
                        "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (originalNetworkID == -1 && transportStreamID == -1 && startServiceID == -1 && endServiceID == -1)
            {
                MessageBox.Show("No filter entered.",
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (startServiceID == -1 && endServiceID != -1)
            {
                MessageBox.Show("The start service ID cannot be omitted if an end service ID is entered.",
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ChannelFilterEntry newFilter = new ChannelFilterEntry(originalNetworkID, transportStreamID, startServiceID, endServiceID);

            ListViewItem newItem = null;

            if (originalNetworkID != -1)
                newItem = new ListViewItem(originalNetworkID.ToString());
            else
                newItem = new ListViewItem(string.Empty);

            newItem.Tag = newFilter;

            if (transportStreamID != -1)
                newItem.SubItems.Add(transportStreamID.ToString());
            else
                newItem.SubItems.Add("");
            if (startServiceID != -1)
                newItem.SubItems.Add(startServiceID.ToString());
            else
                newItem.SubItems.Add("");
            if (endServiceID != -1)
                newItem.SubItems.Add(endServiceID.ToString());
            else
                newItem.SubItems.Add("");

            foreach (ListViewItem oldItem in lvExcludedIdentifiers.Items)
            {
                int index = lvExcludedIdentifiers.Items.IndexOf(oldItem);

                ChannelFilterEntry oldFilter = oldItem.Tag as ChannelFilterEntry;

                if (oldFilter.Frequency == newFilter.Frequency &&
                    oldFilter.OriginalNetworkID == newFilter.OriginalNetworkID &&
                    oldFilter.TransportStreamID == newFilter.TransportStreamID &&
                    oldFilter.StartServiceID == newFilter.StartServiceID &&
                    oldFilter.EndServiceID == newFilter.EndServiceID)
                {
                    MessageBox.Show("A filter has already been created with the same parameters.",
                        "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            lvExcludedIdentifiers.Items.Add(newItem);
        }

        private void btExcludeDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvExcludedIdentifiers.SelectedItems)
                lvExcludedIdentifiers.Items.Remove(item);

            btExcludeDelete.Enabled = (lvExcludedIdentifiers.SelectedItems.Count != 0);
        }

        private void lvExcludedIdentifiers_SelectedIndexChanged(object sender, EventArgs e)
        {
            btExcludeDelete.Enabled = (lvExcludedIdentifiers.SelectedItems.Count != 0);
        }

        private void cbCheckForRepeats_CheckedChanged(object sender, EventArgs e)
        {
            cbNoSimulcastRepeats.Enabled = cbCheckForRepeats.Checked;
            cbIgnoreWMCRecordings.Enabled = cbCheckForRepeats.Checked;
        }  

        private void tbRepeatTitle_TextChanged(object sender, EventArgs e)
        {
            btRepeatAdd.Enabled = tbRepeatTitle.Text.Length != 0 || tbRepeatDescription.Text.Length != 0;
        }

        private void tbRepeatDescription_TextChanged(object sender, EventArgs e)
        {
            btRepeatAdd.Enabled = tbRepeatTitle.Text.Length != 0 || tbRepeatDescription.Text.Length != 0;
        }

        private void btRepeatAdd_Click(object sender, EventArgs e)
        {
            ListViewItem newItem = new ListViewItem(tbRepeatTitle.Text);
            newItem.SubItems.Add(tbRepeatDescription.Text);
            lvRepeatPrograms.Items.Add(newItem);

            tbRepeatTitle.Text = string.Empty;
            tbRepeatDescription.Text = string.Empty;
        }

        private void lvRepeatPrograms_SelectedIndexChanged(object sender, EventArgs e)
        {
            btRepeatDelete.Enabled = lvRepeatPrograms.Items.Count != 0;
        }

        private void btRepeatDelete_Click(object sender, EventArgs e)
        {
            lvRepeatPrograms.Items.Remove(lvRepeatPrograms.SelectedItems[0]);
            btRepeatDelete.Enabled = lvRepeatPrograms.Items.Count != 0;
        }

        private void cbMovieLookupEnabled_CheckedChanged(object sender, EventArgs e)
        {
            gpMovieLookup.Enabled = cbMovieLookupEnabled.Checked;
            gpLookupMisc.Enabled = gpMovieLookup.Enabled || gpTVLookup.Enabled;
        }

        private void cbTVLookupEnabled_CheckedChanged(object sender, EventArgs e)
        {
            gpTVLookup.Enabled = cbTVLookupEnabled.Checked;
            gpLookupMisc.Enabled = gpMovieLookup.Enabled || gpTVLookup.Enabled;
        }

        private void btTimeoutDefaults_Click(object sender, EventArgs e)
        {
            nudDataCollectionTimeout.Value = timeoutCollection;
            nudScanRetries.Value = timeoutRetries;
            nudBufferSize.Value = bufferSize;
            nudBufferFills.Value = bufferFills;
        }

        private void cbManualTime_CheckedChanged(object sender, EventArgs e)
        {
            gpTimeAdjustments.Enabled = cbManualTime.Checked;

            if (!gpTimeAdjustments.Enabled)
            {
                nudCurrentOffsetHours.Value = 0;
                nudCurrentOffsetMinutes.Value = 0;
                nudNextOffsetHours.Value = 0;
                nudNextOffsetMinutes.Value = 0;
                tbChangeDate.Text = string.Empty;
                nudChangeHours.Value = 0;
                nudChangeMinutes.Value = 0;
            }
        }

        private void btLookupChangeNotMovie_Click(object sender, EventArgs e)
        {
            Collection<string> currentList = new Collection<string>();

            foreach (string entry in cboLookupNotMovie.Items)
                currentList.Add(entry);

            ChangeNotMovieList changeNotMovieList = new ChangeNotMovieList(currentList);
            DialogResult result = changeNotMovieList.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            cboLookupNotMovie.Items.Clear();

            foreach (string entry in currentList)
                cboLookupNotMovie.Items.Add(entry);

            if (cboLookupNotMovie.Items.Count != 0)
                cboLookupNotMovie.SelectedIndex = 0;
        }

        private void btLookupBaseBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browsePath = new FolderBrowserDialog();
            browsePath.Description = "EPG Centre - Find Lookup Image Base Directory";
            if (currentLookupBasePath == null)
                browsePath.SelectedPath = RunParameters.DataDirectory;
            else
                browsePath.SelectedPath = currentLookupBasePath;
            DialogResult result = browsePath.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            currentLookupBasePath = browsePath.SelectedPath;
            tbLookupImagePath.Text = browsePath.SelectedPath;
        }

        private void tbXmltvPath_TextChanged(object sender, EventArgs e)
        {
            btXmltvAdd.Enabled = tbXmltvPath.Text.Trim().Length != 0;

            if (!tbXmltvPath.Text.Trim().ToLowerInvariant().EndsWith("mxf"))
            {
                cboXmltvLanguage.Enabled = true;
                cboXmltvIdFormat.Enabled = true;
            }
            else
            {
                cboXmltvLanguage.Enabled = false;
                cboXmltvLanguage.SelectedIndex = 0;
                cboXmltvIdFormat.Enabled = false;
                cboXmltvIdFormat.SelectedIndex = 0;
            }  
        }

        private void btXmltvBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML Files (*.xml)|*.xml";
            if (currentXmltvImportPath == null)
                openFile.InitialDirectory = RunParameters.DataDirectory;
            else
                openFile.InitialDirectory = currentXmltvImportPath;
            openFile.RestoreDirectory = true;
            openFile.Title = "Open XMLTV Import File";

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            currentXmltvImportPath = new FileInfo(openFile.FileName).DirectoryName;
            tbXmltvPath.Text = openFile.FileName;
        }

        private void btXmltvAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Uri checkPath = new Uri(tbXmltvPath.Text);
            }
            catch (UriFormatException)
            {
                MessageBox.Show("The file name is in the wrong format.",
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }  

            ImportFileSpec xmltvFileSpec = new ImportFileSpec(tbXmltvPath.Text);
            xmltvFileSpec.Precedence = (DataPrecedence)Enum.Parse(typeof(DataPrecedence), cboXmltvPrecedence.Text);

            if (cboXmltvLanguage.SelectedIndex != 0)
                xmltvFileSpec.Language = (LanguageCode)cboXmltvLanguage.SelectedItem;

            switch (cboXmltvIdFormat.SelectedIndex)
            {
                case 0:
                    xmltvFileSpec.IdFormat = XmltvIdFormat.Undefined;
                    break;
                case 1:
                    xmltvFileSpec.IdFormat = XmltvIdFormat.ServiceId;
                    break;
                case 2:
                    xmltvFileSpec.IdFormat = XmltvIdFormat.UserChannelNumber;
                    break;
                case 3:
                    xmltvFileSpec.IdFormat = XmltvIdFormat.FullChannelId;
                    break;
                case 4:
                    xmltvFileSpec.IdFormat = XmltvIdFormat.Name;
                    break;
                default:
                    xmltvFileSpec.IdFormat = XmltvIdFormat.Undefined;
                    break;
            }

            xmltvFileSpec.NoLookup = cbXmltvNoLookup.Checked;
            xmltvFileSpec.AppendOnly = cbXmltvAppendOnly.Checked;

            ListViewItem item = new ListViewItem(xmltvFileSpec.FileName);
            item.Tag = xmltvFileSpec;

            if (xmltvFileSpec.Language != null)
                item.SubItems.Add(xmltvFileSpec.Language.Description);
            else
                item.SubItems.Add(string.Empty);

            item.SubItems.Add(xmltvFileSpec.Precedence.ToString());
            item.SubItems.Add(xmltvFileSpec.NoLookup ? "Yes" : "No");
            item.SubItems.Add(xmltvFileSpec.AppendOnly ? "Yes" : "No");

            lvXmltvSelectedFiles.Items.Add(item);

            tbXmltvPath.Text = string.Empty;
            btXmltvLoadFiles.Enabled = true;
        }

        private void lvXmltvSelectedFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            btXmltvDelete.Enabled = (lvXmltvSelectedFiles.SelectedItems.Count != 0);
            btXmltvLoadFiles.Enabled = lvXmltvSelectedFiles.SelectedItems.Count != 0;
        }

        private void btXmltvDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvXmltvSelectedFiles.SelectedItems)
                lvXmltvSelectedFiles.Items.Remove(item);

            btXmltvDelete.Enabled = (lvXmltvSelectedFiles.SelectedItems.Count != 0);
            btXmltvLoadFiles.Enabled = lvXmltvSelectedFiles.SelectedItems.Count != 0;
        }

        private void btXmltvLoadFiles_Click(object sender, EventArgs e)
        {
            XmltvController.Clear();
            MxfController.Clear();

            foreach (ListViewItem item in lvXmltvSelectedFiles.Items)
            {
                ImportFileSpec fileSpec = item.Tag as ImportFileSpec;

                string actualName = ImportFileBase.GetActualFileName(fileSpec.FileName);
                ImportFileBase importFileController;

                if (!actualName.ToLowerInvariant().EndsWith("mxf"))
                    importFileController = new XmltvController();
                else
                    importFileController = new MxfController();

                string reply = importFileController.ProcessChannels(actualName, fileSpec);
                if (reply != null)
                    MessageBox.Show("The import file '" + fileSpec.FileName + "' could not be loaded." + Environment.NewLine + Environment.NewLine + reply,
                        " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);                

                ImportFileBase.DeleteTemporaryFile();
            }

            xmltvChannelBindingList = new BindingList<ImportChannelChange>();

            if (XmltvChannel.Channels != null)
            {
                foreach (XmltvChannel channel in XmltvChannel.Channels)
                    addChannelChange(xmltvChannelBindingList, new ImportChannelChange(channel.DisplayNames[0].Text));
            }

            if (MxfService.Services != null)
            {
                foreach (MxfService service in MxfService.Services)
                    addChannelChange(xmltvChannelBindingList, new ImportChannelChange(service.Name));
            }

            xmltvChannelChangeBindingSource.DataSource = xmltvChannelBindingList;
            dgXmltvChannelChanges.DataSource = xmltvChannelChangeBindingSource;

            btXmltvClear.Enabled = true;
            btXmltvIncludeAll.Enabled = true;
            btXmltvExcludeAll.Enabled = true;
        }

        private void btXmltvClear_Click(object sender, EventArgs e)
        {
            xmltvChannelBindingList.Clear();

            btXmltvClear.Enabled = false;
            btXmltvIncludeAll.Enabled = false;
            btXmltvExcludeAll.Enabled = false;
        }

        private void btXmltvIncludeAll_Click(object sender, EventArgs e)
        {
            foreach (ImportChannelChange xmltvChannelChange in xmltvChannelBindingList)
                xmltvChannelChange.Excluded = false;

            foreach (DataGridViewRow row in dgXmltvChannelChanges.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionForeColor = Color.White;
                }
            }
        }

        private void btXmltvExcludeAll_Click(object sender, EventArgs e)
        {
            foreach (ImportChannelChange xmltvChannelChange in xmltvChannelBindingList)
                xmltvChannelChange.Excluded = true;

            foreach (DataGridViewRow row in dgXmltvChannelChanges.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    cell.Style.ForeColor = Color.Red;
                    cell.Style.SelectionForeColor = Color.Red;
                }
            }
        }

        private void addChannelChange(BindingList<ImportChannelChange> channelChanges, ImportChannelChange newChannelChange)
        {
            foreach (ImportChannelChange oldChannelChange in channelChanges)
            {
                if (oldChannelChange.DisplayName.CompareTo(newChannelChange.DisplayName) > 0)
                {
                    channelChanges.Insert(channelChanges.IndexOf(oldChannelChange), newChannelChange);
                    return;
                }
            }

            channelChanges.Add(newChannelChange);
        }

        private void onXmltvCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (xmltvChannelBindingList[e.RowIndex].Excluded)
                e.CellStyle.ForeColor = Color.Red;
        }

        private void dgXmltvChannelChanges_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgXmltvChannelChanges.CurrentCell.ColumnIndex == dgXmltvChannelChanges.Columns["xmltvNewNameColumn"].Index)
            {
                TextBox textEdit = e.Control as TextBox;
                textEdit.KeyPress -= new KeyPressEventHandler(xmltvTextEdit_KeyPressAlphaNumeric);
                textEdit.KeyPress -= new KeyPressEventHandler(xmltvTextEdit_KeyPressNumeric);
                textEdit.KeyPress += new KeyPressEventHandler(xmltvTextEdit_KeyPressAlphaNumeric);
            }
            else
            {
                if (dgXmltvChannelChanges.CurrentCell.ColumnIndex == dgXmltvChannelChanges.Columns["xmltvChannelNumberColumn"].Index)
                {
                    TextBox textEdit = e.Control as TextBox;
                    textEdit.KeyPress -= new KeyPressEventHandler(xmltvTextEdit_KeyPressAlphaNumeric);
                    textEdit.KeyPress -= new KeyPressEventHandler(xmltvTextEdit_KeyPressNumeric);
                    textEdit.KeyPress += new KeyPressEventHandler(xmltvTextEdit_KeyPressNumeric);
                }
            }
        }

        private void xmltvTextEdit_KeyPressAlphaNumeric(object sender, KeyPressEventArgs e)
        {
            Regex alphaNumericPattern = new Regex(@"[a-zA-Z0-9!&*()-+?\s\b]");
            e.Handled = !alphaNumericPattern.IsMatch(e.KeyChar.ToString());
        }

        private void xmltvTextEdit_KeyPressNumeric(object sender, KeyPressEventArgs e)
        {
            if ("0123456789\b".IndexOf(e.KeyChar) == -1)
                e.Handled = true;
        }

        private void dgXmltvChannelChanges_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgXmltvChannelChanges.IsCurrentCellDirty && dgXmltvChannelChanges.Columns[dgXmltvChannelChanges.CurrentCell.ColumnIndex].Name == "xmltvExcludedColumn")
                dgXmltvChannelChanges.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgXmltvChannelChanges_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgXmltvChannelChanges.Columns[e.ColumnIndex].Name != "xmltvExcludedColumn")
                return;

            if (xmltvChannelBindingList[e.RowIndex].Excluded)
            {
                foreach (DataGridViewCell cell in dgXmltvChannelChanges.Rows[e.RowIndex].Cells)
                {
                    cell.Style.ForeColor = Color.Red;
                    cell.Style.SelectionForeColor = Color.Red;
                }
            }
            else
            {
                foreach (DataGridViewCell cell in dgXmltvChannelChanges.Rows[e.RowIndex].Cells)
                {
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.SelectionForeColor = Color.White;
                }
            }

            dgXmltvChannelChanges.Invalidate();
        }

        private void dgXmltvChannelChanges_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgXmltvChannelChanges.EndEdit();

            if (sortedColumnName == null)
            {
                sortedAscending = true;
                sortedColumnName = dgXmltvChannelChanges.Columns[e.ColumnIndex].Name;
            }
            else
            {
                if (sortedColumnName == dgXmltvChannelChanges.Columns[e.ColumnIndex].Name)
                    sortedAscending = !sortedAscending;
                else
                    sortedColumnName = dgXmltvChannelChanges.Columns[e.ColumnIndex].Name;
            }

            Collection<ImportChannelChange> sortedChanges = new Collection<ImportChannelChange>();

            foreach (ImportChannelChange channelChange in xmltvChannelBindingList)
            {
                switch (dgXmltvChannelChanges.Columns[e.ColumnIndex].Name)
                {
                    case "xmltvDisplayNameColumn":
                        addInOrder(sortedChanges, channelChange, sortedAscending, "Name");
                        break;
                    case "xmltvExcludedColumn":
                        addInOrder(sortedChanges, channelChange, sortedAscending, "Excluded");
                        break;
                    case "xmltvNewNameColumn":
                        addInOrder(sortedChanges, channelChange, sortedAscending, "NewName");
                        break;
                    case "xmltvChannelNumberColumn":
                        addInOrder(sortedChanges, channelChange, sortedAscending, "ChannelNumber");
                        break;
                    default:
                        return;
                }
            }

            xmltvChannelBindingList = new BindingList<ImportChannelChange>();
            foreach (ImportChannelChange channelChange in sortedChanges)
                xmltvChannelBindingList.Add(channelChange);

            xmltvChannelChangeBindingSource.DataSource = xmltvChannelBindingList;
        }

        private void addInOrder(Collection<ImportChannelChange> channelChanges, ImportChannelChange newChange, bool sortedAscending, string keyName)
        {
            sortedKeyName = keyName;

            foreach (ImportChannelChange oldChange in channelChanges)
            {
                if (sortedAscending)
                {
                    if (oldChange.CompareForSorting(newChange, keyName) > 0)
                    {
                        channelChanges.Insert(channelChanges.IndexOf(oldChange), newChange);
                        return;
                    }
                }
                else
                {
                    if (oldChange.CompareForSorting(newChange, keyName) < 0)
                    {
                        channelChanges.Insert(channelChanges.IndexOf(oldChange), newChange);
                        return;
                    }
                }
            }

            channelChanges.Add(newChange);
        }

        private void tbEditText_TextChanged(object sender, EventArgs e)
        {
            btEditAdd.Enabled = !string.IsNullOrWhiteSpace(tbEditText.Text);
        }

        private void btEditAdd_Click(object sender, EventArgs e)
        {
            if (tbEditText.Text.Contains(",") || tbEditText.Text.Contains("="))
            {
                MessageBox.Show("The text cannot contain commas or equal signs.",
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EditSpec editSpec = new EditSpec(tbEditText.Text, (TextLocation)Enum.Parse(typeof(TextLocation), cboEditLocation.Text, true), tbEditReplacementText.Text);
            editSpec.ApplyToTitles = cboEditApplyTo.SelectedIndex != 1;
            editSpec.ApplyToDescriptions = cboEditApplyTo.SelectedIndex != 0;
            editSpec.ReplacementMode = (TextReplacementMode)cboEditReplaceMode.SelectedIndex;

            ListViewItem item = new ListViewItem(editSpec.Text);
            item.Tag = editSpec;

            if (editSpec.ApplyToTitles)
            {
                if (editSpec.ApplyToDescriptions)
                    item.SubItems.Add("Titles and descriptions");
                else
                    item.SubItems.Add("Titles only");
            }
            else
            {
                if (editSpec.ApplyToDescriptions)
                    item.SubItems.Add("Descriptions only");
            }

            item.SubItems.Add(editSpec.Location.ToString());

            if (editSpec.ReplacementText != null)
                item.SubItems.Add(editSpec.ReplacementText);
            else
                item.SubItems.Add(string.Empty);

            lvEditSpecs.Items.Add(item);

            tbEditText.Text = null;
            tbEditReplacementText.Text = null;
        }

        private void lvEditSpecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            btEditDelete.Enabled = lvEditSpecs.SelectedItems != null && lvEditSpecs.SelectedItems.Count > 0;
        }

        private void btEditDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvEditSpecs.SelectedItems)
                lvEditSpecs.Items.Remove(item);

            enableTuningGroups();
            btEditDelete.Enabled = false;            
        }

        private void cbDVBLinkUpdateEnabled_CheckedChanged(object sender, EventArgs e)
        {
            gpDVBLink.Enabled = cbDVBLinkUpdateEnabled.Checked;
        }

        public bool PrepareToSave()
        {
            dgServices.EndEdit();

            bool reply = validateData();
            if (reply)
                setRunParameters();

            return (reply);
        }

        private bool validateData()
        {
            if (lvSelectedFrequencies.Items.Count == 0)
            {
                showErrorMessage("No frequency selected.");
                return (false);
            }

            if (cbXmltvOutputEnabled.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtOutputFile.Text))
                {                    
                    MessageBox.Show("No output path specified.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return (false);
                }
            }

            if (!string.IsNullOrWhiteSpace(tbSageTVSatelliteNumber.Text))
            {
                try
                {
                    Int32.Parse(tbSageTVSatelliteNumber.Text);
                }
                catch (FormatException)
                {
                    showErrorMessage("The SageTV satellite number on the Files tab is incorrect.");
                    return (false);
                }
                catch (OverflowException)
                {
                    showErrorMessage("The SageTV satellite number on the Files tab is incorrect.");
                    return (false);
                }
            }

            if (bindingList != null)
            {
                foreach (TVStation station in bindingList)
                {
                    if (station.ExcludedByUser && (station.NewName != null || (station.NewName != null && station.NewName.Trim() != string.Empty) || station.LogicalChannelNumber != -1))
                    {
                        showErrorMessage("Station " + station.Name + " has been both excluded and updated.");
                        return (false);
                    }
                }
            }

            if (tbExcludedMaxChannel.Text.Trim() != string.Empty)
            {
                try
                {
                    Int32.Parse(tbExcludedMaxChannel.Text);
                }
                catch (FormatException)
                {
                    showErrorMessage("The maximum service ID on the Filters tab is incorrect.");
                    return (false);
                }
                catch (OverflowException)
                {
                    showErrorMessage("The maximum channel number on the Filters tab is incorrect.");
                    return (false);
                }
            }

            if (cbManualTime.Checked)
            {
                if (tbChangeDate.Text.Trim().Length != 0)
                {
                    if (tbChangeDate.Text.Trim().Length != 6)
                    {
                        showErrorMessage("The date of change to the next time zone is incorrect (ddmmyy)");
                        return (false);
                    }

                    try
                    {
                        DateTime.ParseExact(tbChangeDate.Text.Trim().Substring(0, 2) + tbChangeDate.Text.Trim().Substring(2, 2) + tbChangeDate.Text.Trim().Substring(4, 2), "hhMMyy", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        showErrorMessage("The date of change to the next time zone is incorrect (ddmmyy)");
                        return (false);
                    }
                }

                if (nudCurrentOffsetHours.Value != 0 ||
                    nudCurrentOffsetMinutes.Value != 0 ||
                    nudNextOffsetHours.Value != 0 ||
                    nudNextOffsetMinutes.Value != 0 ||
                    nudChangeHours.Value != 0 ||
                    nudChangeMinutes.Value != 0)
                {
                    if (tbChangeDate.Text.Trim().Length == 0)
                    {
                        showErrorMessage("The time zone change data is incorrect." + Environment.NewLine + Environment.NewLine +
                            "A date of change must be entered.");
                        return (false);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(tbDebugIDs.Text))
            {
                string[] parts = tbDebugIDs.Text.Trim().Split(new char[] { ',' });

                foreach (string part in parts)
                {
                    DebugEntry debugEntry = DebugEntry.GetInstance(part);
                    if (debugEntry == null)
                    {
                        showErrorMessage("The debug ID '" + part.Trim() + "' is undefined or in the wrong format.");
                        return (false);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(tbTraceIDs.Text))
            {
                string[] parts = tbTraceIDs.Text.Trim().Split(new char[] { ',' });

                foreach (string part in parts)
                {
                    TraceEntry traceEntry = TraceEntry.GetInstance(part);
                    if (traceEntry == null)
                    {
                        showErrorMessage("The debug ID '" + part.Trim() + "' is undefined or in the wrong format.");
                        return (false);
                    }
                }
            }

            return (true);
        }

        private void showErrorMessage(string message)
        {
            MessageBox.Show(message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void setRunParameters()
        {
            runParameters.Options.Clear();

            setTuningTabData();
            setOutputTabData();
            setFilesTabData();
            setChannelsTabData();
            setTimeShiftTabData();
            setFiltersTabData();
            setRepeatsTabData();
            setAdvancedTabData();
            setLookupTabData();
            setUpdateTabData();
            setXmltvTabData();
            setEditTabData();
            setDiagnosticsTabData();
        }

        private void setTuningTabData()
        {
            runParameters.FrequencyCollection.Clear();
            
            TuningFrequency selectedFrequency = getPluginFrequency();
            if (selectedFrequency != null)
                runParameters.FrequencyCollection.Add(selectedFrequency); 
        }

        private void setOutputTabData()
        {
            if (cbAllowBreaks.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.AcceptBreaks));
            if (cbRoundTime.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.RoundTime));
            if (cbRemoveExtractedData.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.NoRemoveData));
            if (cbCreateSameData.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.DuplicateSameChannels));
            if (cbNoLogExcluded.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.NoLogExcluded));
            if (cbTcRelevantChannels.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.TcRelevantOnly));
            if (cbAddSeasonEpisodeToDesc.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.AddSeasonEpisodeToDesc));
            if (cbNoDataNoFile.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.NoDataNoFile));

            if (cbPluginOutputEnabled.Checked)
            {
                runParameters.Options.Add(new OptionEntry(OptionName.PluginImport));

                switch (cboTVSourceChannelIDFormat.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        runParameters.Options.Add(new OptionEntry(OptionName.ChannelIdSid));
                        break;
                    case 2:
                        runParameters.Options.Add(new OptionEntry(OptionName.UseChannelId));
                        break;
                    case 3:
                        runParameters.Options.Add(new OptionEntry(OptionName.ChannelIdSeqNo));
                        break;
                    default:
                        break;
                }
            }

            if (cbXmltvOutputEnabled.Checked)
            {
                runParameters.OutputFileName = txtOutputFile.Text;

                switch (cboChannelIDFormat.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        runParameters.Options.Add(new OptionEntry(OptionName.UseChannelId));
                        break;
                    case 2:
                        runParameters.Options.Add(new OptionEntry(OptionName.ChannelIdSeqNo));
                        break;
                    case 3:
                        runParameters.Options.Add(new OptionEntry(OptionName.ChannelIdFullName));
                        break;
                    default:
                        break;
                }

                switch (cboEpisodeTagFormat.SelectedIndex)
                {
                    case 0:
                        runParameters.Options.Add(new OptionEntry(OptionName.ValidEpisodeTag));
                        break;
                    case 1:
                        runParameters.Options.Add(new OptionEntry(OptionName.UseBsepg));
                        break;                    
                    case 2:
                        runParameters.Options.Add(new OptionEntry(OptionName.UseRawCrid));
                        break;
                    case 3:
                        runParameters.Options.Add(new OptionEntry(OptionName.UseNumericCrid));
                        break;
                    case 4:
                        runParameters.Options.Add(new OptionEntry(OptionName.NoEpisodeTag));
                        break;
                    default:
                        break;
                }

                if (cbUseLCN.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.UseLcn));
                if (cbCreateADTag.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.CreateAdTag));
                if (cbElementPerTag.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.ElementPerTag));
                if (cbOmitPartNumber.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.OmitPartNumber));
                if (cbPrefixDescWithAirDate.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.PrefixDescriptionWithAirDate));
                if (cbPrefixSubtitleWithSeasonEpisode.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.PrefixSubtitleWithSeasonEpisode));

                if (!string.IsNullOrWhiteSpace(tbChannelLogoPath.Text))
                    runParameters.ChannelLogoPath = tbChannelLogoPath.Text.Trim();
                else
                    runParameters.ChannelLogoPath = null;
            }
            else
                runParameters.OutputFileName = null;

            if (cbWmcOutputEnabled.Checked)
            {
                runParameters.Options.Add(new OptionEntry(OptionName.WmcImport));
                if (!string.IsNullOrWhiteSpace(txtImportName.Text))
                    runParameters.WMCImportName = txtImportName.Text.Trim();
                else
                    runParameters.WMCImportName = null;

                if (cbAutoMapEPG.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.AutoMapEpg));
                if (cbWMCFourStarSpecial.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.WmcStarSpecial));
                if (cbDisableInbandLoader.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.DisableInbandLoader));

                switch (cboWMCSeries.SelectedIndex)
                {
                    case 1:
                        runParameters.Options.Add(new OptionEntry(OptionName.UseWmcRepeatCheck));
                        break;
                    case 2:
                        runParameters.Options.Add(new OptionEntry(OptionName.UseWmcRepeatCheckBroadcast));
                        break;
                    default:
                        break;
                }
            }
        }

        private void setFilesTabData()
        {
            if (cbBladeRunnerFile.Checked)
            {
                runParameters.Options.Add(new OptionEntry(OptionName.CreateBrChannels));

                string bladeRunnerName = tbBladeRunnerFileName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(bladeRunnerName))
                    runParameters.BladeRunnerFileName = bladeRunnerName;
                else
                    runParameters.BladeRunnerFileName = null;
            }
            else
                runParameters.BladeRunnerFileName = null;

            if (cbAreaRegionFile.Checked)
            {
                runParameters.Options.Add(new OptionEntry(OptionName.CreateArChannels));

                string areaRegionName = tbAreaRegionFileName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(areaRegionName))
                    runParameters.AreaRegionFileName = areaRegionName;
                else
                    runParameters.AreaRegionFileName = null;
            }
            else
                runParameters.AreaRegionFileName = null;

            if (cbSageTVFile.Checked)
            {
                runParameters.Options.Add(new OptionEntry(OptionName.CreateSageTvFrq));

                string sageTVName = tbSageTVFileName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(sageTVName))
                    runParameters.SageTVFileName = sageTVName;
                else
                    runParameters.SageTVFileName = null;

                if (cbSageTVFileNoEPG.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.SageTvOmitNoEpg));

                if (!string.IsNullOrWhiteSpace(tbSageTVSatelliteNumber.Text))
                    runParameters.SageTVSatelliteNumber = Int32.Parse(tbSageTVSatelliteNumber.Text.Trim());
                else
                    runParameters.SageTVSatelliteNumber = -1;
            }
            else
            {
                runParameters.SageTVFileName = null;
                runParameters.SageTVSatelliteNumber = -1;
            }
        }

        private void setChannelsTabData()
        {
            if (bindingList != null)
            {
                foreach (TVStation station in bindingList)
                {
                    TVStation originalStation = TVStation.FindStation(runParameters.StationCollection,
                        station.OriginalNetworkID, station.TransportStreamID, station.ServiceID);
                    if (originalStation != null)
                    {
                        originalStation.ExcludedByUser = station.ExcludedByUser;
                        originalStation.NewName = station.NewName;
                        originalStation.LogicalChannelNumber = station.LogicalChannelNumber;
                    }
                }
            }
        }

        private void setTimeShiftTabData()
        {
            runParameters.TimeOffsetChannels.Clear();

            foreach (ListViewItem timeOffsetItem in lvPlusSelectedChannels.Items)
            {
                TimeOffsetChannel timeOffsetChannel = timeOffsetItem.Tag as TimeOffsetChannel;
                runParameters.TimeOffsetChannels.Add(timeOffsetChannel);
            }
        }

        private void setFiltersTabData()
        {
            runParameters.ChannelFilters.Clear();
            
            foreach (ListViewItem filterItem in lvExcludedIdentifiers.Items)
            {
                ChannelFilterEntry filterEntry = filterItem.Tag as ChannelFilterEntry;
                runParameters.ChannelFilters.Add(filterEntry);
            }

            if (tbExcludedMaxChannel.Text.Trim().Length != 0)
                runParameters.MaxService = Int32.Parse(tbExcludedMaxChannel.Text.Trim());
            else
                runParameters.MaxService = -1;
        }

        private void setRepeatsTabData()
        {
            if (cbCheckForRepeats.Checked)
            {
                runParameters.Options.Add(new OptionEntry(OptionName.CheckForRepeats));
                if (cbNoSimulcastRepeats.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.NoSimulcastRepeats));
                if (cbIgnoreWMCRecordings.Checked)
                    runParameters.Options.Add(new OptionEntry(OptionName.IgnoreWmcRecordings));
            }

            runParameters.Exclusions.Clear();

            foreach (ListViewItem exclusionEntry in lvRepeatPrograms.Items)
            {
                RepeatExclusion exclusion = new RepeatExclusion(exclusionEntry.SubItems[0].Text, exclusionEntry.SubItems[1].Text);
                runParameters.Exclusions.Add(exclusion);
            }

            runParameters.PhrasesToIgnore.Clear();

            if (!string.IsNullOrWhiteSpace(tbPhrasesToIgnore.Text))
            {
                string[] phrases = tbPhrasesToIgnore.Text.Split(new char[] { ',' });

                foreach (string phrase in phrases)
                    runParameters.PhrasesToIgnore.Add(phrase);
            }
        }

        private void setAdvancedTabData()
        {
            if (cbStoreStationInfo.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.StoreStationInfo));
            if (cbUseStoredStationInfo.Checked)
                runParameters.Options.Add(new OptionEntry(OptionName.UseStoredStationInfo));
            
            runParameters.FrequencyTimeout = new TimeSpan((long)(nudDataCollectionTimeout.Value * 10000000));
            runParameters.Repeats = (int)nudScanRetries.Value;
            runParameters.BufferSize = (int)nudBufferSize.Value;
            runParameters.BufferFills = (int)nudBufferFills.Value;

            if (cbManualTime.Checked)
            {
                if (tbChangeDate.Text.Trim() != string.Empty)
                {
                    runParameters.TimeZone = new TimeSpan((int)nudCurrentOffsetHours.Value, (int)nudCurrentOffsetMinutes.Value, 0);
                    runParameters.NextTimeZone = new TimeSpan((int)nudNextOffsetHours.Value, (int)nudNextOffsetMinutes.Value, 0);

                    try
                    {
                        runParameters.NextTimeZoneChange = DateTime.ParseExact(tbChangeDate.Text.Trim().Substring(0, 2) + tbChangeDate.Text.Trim().Substring(2, 2) + tbChangeDate.Text.Trim().Substring(4, 2) + " 000000", "ddMMyy HHmmss", CultureInfo.InvariantCulture) +
                            new TimeSpan((int)nudChangeHours.Value, (int)nudChangeMinutes.Value, 0);
                    }
                    catch (FormatException) { runParameters.NextTimeZoneChange = DateTime.MaxValue; }
                    catch (ArgumentOutOfRangeException) { runParameters.NextTimeZoneChange = DateTime.MaxValue; }

                    runParameters.TimeZoneSet = true;
                }
                else
                    runParameters.TimeZoneSet = false;
            }
            else
            {
                runParameters.TimeZone = new TimeSpan();
                runParameters.NextTimeZone = new TimeSpan();
                runParameters.NextTimeZoneChange = new DateTime();
                runParameters.TimeZoneSet = false;
            }
        }

        private void setLookupTabData()
        {
            runParameters.MovieLookupEnabled = cbMovieLookupEnabled.Checked;
            
            switch (cboxMovieLookupImageType.SelectedIndex)
            {
                case 0:
                    runParameters.DownloadMovieThumbnail = LookupImageType.Thumbnail;
                    break;
                case 1:
                    runParameters.DownloadMovieThumbnail = LookupImageType.Poster;
                    break;
                case 2:
                    runParameters.DownloadMovieThumbnail = LookupImageType.None;
                    break;
                default:
                    runParameters.DownloadMovieThumbnail = LookupImageType.None;
                    break;
            }

            runParameters.MovieLowTime = (int)nudLookupMovieLowDuration.Value;
            runParameters.MovieHighTime = (int)nudLookupMovieHighDuration.Value;
            
            runParameters.LookupMoviePhrases.Clear();
            if (tbLookupMoviePhrases.Text != string.Empty)
            {
                runParameters.MoviePhraseSeparator = udMoviePhraseSeparator.Text;

                string[] parts = tbLookupMoviePhrases.Text.Split(new string[] { udMoviePhraseSeparator.Text }, StringSplitOptions.None);

                foreach (string part in parts)
                    runParameters.LookupMoviePhrases.Add(part);
            }

            if (cboLookupNotMovie.Items.Count != 0)
            {
                runParameters.LookupNotMovie = new Collection<string>();
                foreach (string entry in cboLookupNotMovie.Items)
                    runParameters.LookupNotMovie.Add(entry);
            }
            else
                runParameters.LookupNotMovie = null;

            runParameters.TVLookupEnabled = cbTVLookupEnabled.Checked;

            switch (cboxTVLookupImageType.SelectedIndex)
            {
                case 0:
                    runParameters.DownloadTVThumbnail = LookupImageType.Poster;
                    break;
                case 1:
                    runParameters.DownloadTVThumbnail = LookupImageType.Banner;
                    break;
                case 2:
                    runParameters.DownloadTVThumbnail = LookupImageType.Fanart;
                    break;
                case 3:
                    runParameters.DownloadTVThumbnail = LookupImageType.SmallPoster;
                    break;
                case 4:
                    runParameters.DownloadTVThumbnail = LookupImageType.SmallFanart;
                    break;
                case 5:
                    runParameters.DownloadTVThumbnail = LookupImageType.None;
                    break;
                default:
                    runParameters.DownloadTVThumbnail = LookupImageType.None;
                    break;
            }

            runParameters.LookupMatching = (MatchMethod)Enum.Parse(typeof(MatchMethod), cbxLookupMatching.Text);
            runParameters.LookupNotFound = cbLookupNotFound.Checked;
            runParameters.LookupReload = cbLookupReload.Checked;
            runParameters.LookupIgnoreCategories = cbLookupIgnoreCategories.Checked;
            runParameters.LookupProcessAsTVSeries = cbLookupProcessAsTVSeries.Checked;
            runParameters.LookupTimeLimit = (int)nudLookupTime.Value;
            runParameters.LookupErrorLimit = (int)nudLookupErrors.Value;

            runParameters.LookupIgnoredPhrases.Clear();
            if (tbLookupIgnoredPhrases.Text != string.Empty)
            {
                runParameters.LookupIgnoredPhraseSeparator = udIgnorePhraseSeparator.Text;

                string[] parts = tbLookupIgnoredPhrases.Text.Split(new string[] { udIgnorePhraseSeparator.Text }, StringSplitOptions.None);

                foreach (string part in parts)
                    runParameters.LookupIgnoredPhrases.Add(part);
            }

            if (string.IsNullOrWhiteSpace(tbLookupImagePath.Text))
                runParameters.LookupImagePath = null;
            else
                runParameters.LookupImagePath = tbLookupImagePath.Text.Trim();            
        }

        private void setUpdateTabData()
        {
            runParameters.ChannelUpdateEnabled = cbDVBLinkUpdateEnabled.Checked;

            switch (cboMergeMethod.SelectedIndex)
            {
                case 0:
                    runParameters.ChannelMergeMethod = ChannelMergeMethod.None;
                    break;
                case 1:
                    runParameters.ChannelMergeMethod = ChannelMergeMethod.Name;
                    break;
                case 2:
                    runParameters.ChannelMergeMethod = ChannelMergeMethod.Number;
                    break;
                case 3:
                    runParameters.ChannelMergeMethod = ChannelMergeMethod.NameNumber;
                    break;
                default:
                    runParameters.ChannelMergeMethod = ChannelMergeMethod.None;
                    break;
            }

            switch (cboEPGScanner.SelectedIndex)
            {
                case 0:
                    runParameters.ChannelEPGScanner = ChannelEPGScanner.None;
                    break;
                case 1:
                    runParameters.ChannelEPGScanner = ChannelEPGScanner.Default;
                    break;
                case 2:
                    runParameters.ChannelEPGScanner = ChannelEPGScanner.EPGCollector;
                    break;
                case 3:
                    runParameters.ChannelEPGScanner = ChannelEPGScanner.EITScanner;
                    break;
                case 4:
                    runParameters.ChannelEPGScanner = ChannelEPGScanner.Xmltv;
                    break;
                default:
                    runParameters.ChannelEPGScanner = ChannelEPGScanner.None;
                    break;
            }

            runParameters.ChannelChildLock = cbChildLock.Checked;
            runParameters.ChannelLogNetworkMap = cbLogNetworkMap.Checked;
        }

        private void setXmltvTabData()
        {
            if (lvXmltvSelectedFiles.Items.Count != 0)
            {
                runParameters.ImportFiles = new Collection<ImportFileSpec>();
                foreach (ListViewItem item in lvXmltvSelectedFiles.Items)
                    runParameters.ImportFiles.Add(item.Tag as ImportFileSpec);

                if (xmltvChannelBindingList != null)
                {
                    runParameters.ImportChannelChanges = new Collection<ImportChannelChange>();
                    foreach (ImportChannelChange xmltvChannelChange in xmltvChannelBindingList)
                        runParameters.ImportChannelChanges.Add(xmltvChannelChange);
                }
            }
            else
            {
                runParameters.ImportFiles = null;
                runParameters.ImportChannelChanges = null;
            }
        }

        private void setEditTabData()
        {
            if (lvEditSpecs.Items.Count != 0)
            {
                runParameters.EditSpecs = new Collection<EditSpec>();
                foreach (ListViewItem item in lvEditSpecs.Items)
                    runParameters.EditSpecs.Add(item.Tag as EditSpec);
            }
            else
                runParameters.EditSpecs = null;
        }

        private void setDiagnosticsTabData()
        {
            runParameters.DebugIDs.Clear();
            if (!string.IsNullOrWhiteSpace(tbDebugIDs.Text))
            {
                string[] parts = tbDebugIDs.Text.Trim().Split(new char[] { ',' });

                foreach (string part in parts)
                    runParameters.DebugIDs.Add(DebugEntry.GetInstance(part));
            }

            runParameters.TraceIDs.Clear();
            if (!string.IsNullOrWhiteSpace(tbTraceIDs.Text))
            {
                string[] parts = tbTraceIDs.Text.Trim().Split(new char[] { ',' });

                foreach (string part in parts)
                    runParameters.TraceIDs.Add(TraceEntry.GetInstance(part));
            }
        }

        private TuningFrequency getPluginFrequency()
        {
            if (lvSelectedFrequencies.Items.Count == 0)
                return (null);

            TuningFrequency selectedFrequency = (TuningFrequency)((TuningFrequency)lvSelectedFrequencies.Items[0].Tag).Clone();
            return(selectedFrequency);
        }

        private DataState hasDataChanged()
        {
            setRunParameters();

            if (originalData == null || newFile)
                return (DomainObjects.DataState.Changed);

            return (runParameters.HasDataChanged(originalData));
        }

        /// <summary>
        /// Save the data to the original file.
        /// </summary>
        /// <returns>True if the file has been saved; false otherwise.</returns>
        public bool Save()
        {
            return (Save(currentFileName));
        }

        /// <summary>
        /// Save the current data set to a specified file.
        /// </summary>
        /// <param name="fileName">The name of the file to be saved.</param>
        /// <returns>True if the data has been saved; false otherwise.</returns>
        public bool Save(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            if (!fileInfo.DirectoryName.ToUpper().EndsWith(Path.DirectorySeparatorChar + "EPG"))
            {
                DialogResult result = MessageBox.Show("The path does not reference an 'EPG' directory." +
                    Environment.NewLine + Environment.NewLine + "Is the path correct?", "EPG Centre",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return (false);
            }

            Cursor.Current = Cursors.WaitCursor;
            string message = runParameters.Save(fileName);
            Cursor.Current = Cursors.Arrow;

            if (message == null)
            {
                MessageBox.Show("The parameters have been saved to '" + fileName + "'", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                newFile = false;
                originalData = runParameters.Clone();
                currentFileName = fileName;
                copyPluginModule(fileName);
            }
            else
                MessageBox.Show("An error has occurred while writing the parameters." + Environment.NewLine + Environment.NewLine + message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return (message == null);
        }

        private string getSelectedFrequency()
        {
            return (((TuningFrequency)lvSelectedFrequencies.Items[0].Tag).Frequency.ToString());
        }

        private void copyPluginModule(string parameterFileName)
        {
            FileInfo fileInfo = new FileInfo(parameterFileName);

            if (!fileInfo.DirectoryName.ToUpper().EndsWith(Path.DirectorySeparatorChar + "EPG"))
                return;

            string oldPluginPath = Path.Combine(fileInfo.DirectoryName, "DVBLogicCPPPlugin.dll");
            string newPluginPath = Path.Combine(RunParameters.BaseDirectory, "DVBLogicCPPPlugin.dll");

            if (File.Exists(oldPluginPath))
            {
                Logger.Instance.Write("Existing plugin located");

                DateTime newWriteTime = File.GetLastWriteTime(newPluginPath);
                DateTime existingWriteTime = File.GetLastWriteTime(oldPluginPath);

                if (newWriteTime <= existingWriteTime)
                {
                    Logger.Instance.Write("Existing plugin is up to date");
                    return;
                }
                else
                    Logger.Instance.Write("Newer version available to install");
            }

            try
            {
                File.Copy(newPluginPath, oldPluginPath, true);
                Logger.Instance.Write("Plugin installed - version now " + File.GetLastWriteTime(oldPluginPath));
                MessageBox.Show("The plugin module has been updated.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException e1)
            {
                Logger.Instance.Write("<e> Plugin install failed - " + e1.Message);
                MessageBox.Show("The plugin could not be updated." + Environment.NewLine + Environment.NewLine + e1.Message,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (UnauthorizedAccessException e2)
            {
                Logger.Instance.Write("<e> Plugin install failed - " + e2.Message);
                MessageBox.Show("The plugin could not be updated." + Environment.NewLine + Environment.NewLine + e2.Message,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string locationPath = Path.Combine(fileInfo.DirectoryName, "EPG Collector Gateway.cfg");

                if (File.Exists(locationPath))
                {
                    File.SetAttributes(locationPath, FileAttributes.Normal);
                    File.Delete(locationPath);
                }

                FileStream fileStream = new FileStream(locationPath, FileMode.Create, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);

                streamWriter.WriteLine("Location=" + RunParameters.BaseDirectory + Path.DirectorySeparatorChar + "DVBLogicPlugin.dll");

                streamWriter.Close();
                fileStream.Close();

                File.SetAttributes(locationPath, FileAttributes.ReadOnly);
                Logger.Instance.Write("Software location file written successfully");
                Logger.Instance.Write("Software location set to " + RunParameters.BaseDirectory);
            }
            catch (IOException e1)
            {
                Logger.Instance.Write("<e> The software location file could not be written - " + e1.Message);
                MessageBox.Show("The software location file could not be written." + Environment.NewLine + Environment.NewLine + e1.Message,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (UnauthorizedAccessException e2)
            {
                Logger.Instance.Write("<e> The software location file could not be written - " + e2.Message);
                MessageBox.Show("The software location file could not be written." + Environment.NewLine + Environment.NewLine + e2.Message,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cbAreaRegionFile_CheckedChanged(object sender, EventArgs e)
        {
            gpAreaRegionFile.Enabled = cbAreaRegionFile.Checked;
        }

        private void btBrowseAreaRegionFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseFile = new FolderBrowserDialog();
            browseFile.Description = "EPG Centre - Find Area/Region Channel File Directory";
            if (currentAreaChannelOutputPath == null)
                browseFile.SelectedPath = RunParameters.DataDirectory;
            else
                browseFile.SelectedPath = currentAreaChannelOutputPath;
            DialogResult result = browseFile.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            currentAreaChannelOutputPath = browseFile.SelectedPath + @"\";
            tbAreaRegionFileName.Text = currentAreaChannelOutputPath;
        }

        private void cbBladeRunnerFile_CheckedChanged(object sender, EventArgs e)
        {
            gpBladeRunnerFile.Enabled = cbBladeRunnerFile.Checked;
        }

        private void btBrowseBladeRunnerFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseFile = new FolderBrowserDialog();
            browseFile.Description = "EPG Centre - Find BladeRunner Channel File Directory";
            if (currentBladeRunnerOutputPath == null)
                browseFile.SelectedPath = RunParameters.DataDirectory;
            else
                browseFile.SelectedPath = currentBladeRunnerOutputPath;
            DialogResult result = browseFile.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            currentBladeRunnerOutputPath = browseFile.SelectedPath + @"\";
            tbBladeRunnerFileName.Text = currentBladeRunnerOutputPath;
        }

        private void cbSageTVFile_CheckedChanged(object sender, EventArgs e)
        {
            gpSageTVFile.Enabled = cbSageTVFile.Checked;
        }

        private void btBrowseSageTVFile_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseFile = new FolderBrowserDialog();
            browseFile.Description = "EPG Centre - Find SageTV Frequency File Directory";
            if (currentSageTVOutputPath == null)
                browseFile.SelectedPath = RunParameters.DataDirectory;
            else
                browseFile.SelectedPath = currentSageTVOutputPath;
            DialogResult result = browseFile.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            currentSageTVOutputPath = browseFile.SelectedPath + @"\";
            tbSageTVFileName.Text = currentSageTVOutputPath;
        }

        private void cbCheckForRepeats_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cboWMCSeries.SelectedIndex != 0 && cbCheckForRepeats.Checked)
            {
                MessageBox.Show("EPG Collector repeat checking and Windows Media Center series/repeat checking cannot be enabled at the same time." +
                    Environment.NewLine + Environment.NewLine +
                    "Windows Media Center repeat checking will be disabled.",
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                cboWMCSeries.SelectedIndex = 0;
            }

            cbNoSimulcastRepeats.Enabled = cbCheckForRepeats.Checked;
            if (!cbNoSimulcastRepeats.Enabled)
                cbNoSimulcastRepeats.Checked = false;

            cbIgnoreWMCRecordings.Enabled = cbCheckForRepeats.Checked;
            if (!cbIgnoreWMCRecordings.Enabled)
                cbIgnoreWMCRecordings.Checked = false;

            gpRepeatExclusions.Enabled = cbCheckForRepeats.Checked;
            if (!gpRepeatExclusions.Enabled)
            {
                lvRepeatPrograms.Items.Clear();
                tbPhrasesToIgnore.Text = null;
            }
        }

        private void btAddSatellite_Click(object sender, EventArgs e)
        {
            SatelliteFrequency satelliteFrequency = (SatelliteFrequency)(cboDVBSScanningFrequency.SelectedItem as SatelliteFrequency).Clone();
            satelliteFrequency.CollectionType = (CollectionType)cboDVBSCollectionType.SelectedItem;

            satelliteFrequency.SatelliteDish = new SatelliteDish();

            bool advancedResult = getAdvancedParameters(satelliteFrequency);
            if (!advancedResult)
                return;

            ListViewItem newItem = new ListViewItem(satelliteFrequency.ToString());
            newItem.Tag = satelliteFrequency;
            newItem.SubItems.Add(((Satellite)cboSatellite.SelectedItem).Name);
            newItem.SubItems.Add("Satellite");
            newItem.SubItems.Add(satelliteFrequency.CollectionType.ToString());            

            foreach (ListViewItem oldItem in lvSelectedFrequencies.Items)
            {
                SatelliteFrequency oldFrequency = oldItem.Tag as SatelliteFrequency;
                if (oldFrequency != null && oldFrequency.EqualTo(satelliteFrequency, EqualityLevel.Identity))
                {
                    if (!oldFrequency.EqualTo(satelliteFrequency, EqualityLevel.Entirely))
                    {
                        DialogResult result = MessageBox.Show("The frequency has already been selected." + Environment.NewLine + Environment.NewLine +
                            "Do you want to overwrite it?", "EPG Centre", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        switch (result)
                        {
                            case DialogResult.Cancel:
                                return;
                            case DialogResult.Yes:
                                lvSelectedFrequencies.Items.RemoveAt(0);
                                lvSelectedFrequencies.Items.Add(newItem);
                                return;
                            default:
                                lvSelectedFrequencies.Items.Add(newItem);
                                return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The frequency has already been selected with the same parameters.",
                            "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (lvSelectedFrequencies.Items.Count != 0)
            {
                DialogResult replace = MessageBox.Show("Only one frequency can be selected." + Environment.NewLine + Environment.NewLine +
                    "Do you want to overwrite the existing frequency?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (replace)
                {
                    case DialogResult.Yes:
                        lvSelectedFrequencies.Items.RemoveAt(0);
                        lvSelectedFrequencies.Items.Add(newItem);
                        return;
                    default:
                        return;
                }
            }
            else
                lvSelectedFrequencies.Items.Add(newItem);
        }

        private void btAddTerrestrial_Click(object sender, EventArgs e)
        {
            TerrestrialFrequency terrestrialFrequency = (TerrestrialFrequency)(cboDVBTScanningFrequency.SelectedItem as TerrestrialFrequency).Clone();
            terrestrialFrequency.CollectionType = (CollectionType)cboDVBTCollectionType.SelectedItem;

            ListViewItem newItem = new ListViewItem(terrestrialFrequency.ToString());
            newItem.Tag = terrestrialFrequency;
            newItem.SubItems.Add(cboCountry.Text + " " + cboArea.Text);
            newItem.SubItems.Add("Terrestrial");
            newItem.SubItems.Add(terrestrialFrequency.CollectionType.ToString());

            bool advancedResult = getAdvancedParameters(terrestrialFrequency);
            if (!advancedResult)
                return;

            foreach (ListViewItem oldItem in lvSelectedFrequencies.Items)
            {
                TerrestrialFrequency oldFrequency = oldItem.Tag as TerrestrialFrequency;
                if (oldFrequency != null && oldFrequency.EqualTo(terrestrialFrequency, EqualityLevel.Identity))
                {
                    if (!oldFrequency.EqualTo(terrestrialFrequency, EqualityLevel.Identity))
                    {
                        DialogResult result = MessageBox.Show("The frequency has already been selected." + Environment.NewLine + Environment.NewLine +
                            "Do you want to overwrite it?", "EPG Centre", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        switch (result)
                        {
                            case DialogResult.Cancel:
                                return;
                            case DialogResult.Yes:
                                lvSelectedFrequencies.Items.RemoveAt(0);
                                lvSelectedFrequencies.Items.Add(newItem);
                                return;
                            default:
                                lvSelectedFrequencies.Items.Add(newItem);
                                return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The frequency has already been selected with the same parameters.",
                            "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (lvSelectedFrequencies.Items.Count != 0)
            {
                DialogResult replace = MessageBox.Show("Only one frequency can be selected." + Environment.NewLine + Environment.NewLine +
                    "Do you want to overwrite the existing frequency?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (replace)
                {
                    case DialogResult.Yes:
                        lvSelectedFrequencies.Items.RemoveAt(0);
                        lvSelectedFrequencies.Items.Add(newItem);
                        return;
                    default:
                        return;
                }
            }
            else
                lvSelectedFrequencies.Items.Add(newItem);
        }

        private void btAddCable_Click(object sender, EventArgs e)
        {
            CableFrequency cableFrequency = (CableFrequency)(cboCableScanningFrequency.SelectedItem as CableFrequency).Clone();
            cableFrequency.CollectionType = (CollectionType)cboCableCollectionType.SelectedItem;
            
            ListViewItem newItem = new ListViewItem(cableFrequency.ToString());
            newItem.Tag = cableFrequency;
            newItem.SubItems.Add(((CableProvider)cboCable.SelectedItem).Name);
            newItem.SubItems.Add("Cable");
            newItem.SubItems.Add(cableFrequency.CollectionType.ToString());

            bool advancedResult = getAdvancedParameters(cableFrequency);
            if (!advancedResult)
                return;

            foreach (ListViewItem oldItem in lvSelectedFrequencies.Items)
            {
                CableFrequency oldFrequency = oldItem.Tag as CableFrequency;
                if (oldFrequency != null && oldFrequency.EqualTo(cableFrequency, EqualityLevel.Identity))
                {
                    if (!oldFrequency.EqualTo(cableFrequency, EqualityLevel.Entirely))
                    {
                        DialogResult result = MessageBox.Show("The frequency has already been selected." + Environment.NewLine + Environment.NewLine +
                            "Do you want to overwrite it?", "EPG Centre", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        switch (result)
                        {
                            case DialogResult.Cancel:
                                return;
                            case DialogResult.Yes:
                                lvSelectedFrequencies.Items.Remove(oldItem);
                                lvSelectedFrequencies.Items.Add(newItem);
                                return;
                            default:
                                lvSelectedFrequencies.Items.Add(newItem);
                                return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The frequency has already been selected with the same parameters.",
                            "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (lvSelectedFrequencies.Items.Count != 0)
            {
                DialogResult replace = MessageBox.Show("Only one frequency can be selected." + Environment.NewLine + Environment.NewLine +
                    "Do you want to overwrite the existing frequency?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (replace)
                {
                    case DialogResult.Yes:
                        lvSelectedFrequencies.Items.RemoveAt(0);
                        lvSelectedFrequencies.Items.Add(newItem);
                        return;
                    default:
                        return;
                }
            }
            else
                lvSelectedFrequencies.Items.Add(newItem);
        }

        private void btAddAtsc_Click(object sender, EventArgs e)
        {
            AtscFrequency atscFrequency = (AtscFrequency)(cboAtscScanningFrequency.SelectedItem as AtscFrequency).Clone();
            atscFrequency.CollectionType = (CollectionType)cboAtscCollectionType.SelectedItem;

            ListViewItem newItem = new ListViewItem(atscFrequency.ToString());
            newItem.Tag = atscFrequency;
            newItem.SubItems.Add(((AtscProvider)cboAtsc.SelectedItem).Name);
            newItem.SubItems.Add("ATSC");
            newItem.SubItems.Add(atscFrequency.CollectionType.ToString());

            bool advancedResult = getAdvancedParameters(atscFrequency);
            if (!advancedResult)
                return;

            foreach (ListViewItem oldItem in lvSelectedFrequencies.Items)
            {
                AtscFrequency oldFrequency = oldItem.Tag as AtscFrequency;
                if (oldFrequency != null && oldFrequency.EqualTo(atscFrequency, EqualityLevel.Identity))
                {
                    if (!oldFrequency.EqualTo(atscFrequency, EqualityLevel.Entirely))
                    {
                        DialogResult result = MessageBox.Show("The frequency has already been selected." + Environment.NewLine + Environment.NewLine +
                            "Do you want to overwrite it?", "EPG Centre", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        switch (result)
                        {
                            case DialogResult.Cancel:
                                return;
                            case DialogResult.Yes:
                                lvSelectedFrequencies.Items.Remove(oldItem);
                                lvSelectedFrequencies.Items.Add(newItem);
                                return;
                            default:
                                lvSelectedFrequencies.Items.Add(newItem);
                                return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The frequency has already been selected with the same parameters.",
                            "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (lvSelectedFrequencies.Items.Count != 0)
            {
                DialogResult replace = MessageBox.Show("Only one frequency can be selected." + Environment.NewLine + Environment.NewLine +
                    "Do you want to overwrite the existing frequency?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (replace)
                {
                    case DialogResult.Yes:
                        lvSelectedFrequencies.Items.RemoveAt(0);
                        lvSelectedFrequencies.Items.Add(newItem);
                        return;
                    default:
                        return;
                }
            }
            else
                lvSelectedFrequencies.Items.Add(newItem);
        }

        private void btAddClearQam_Click(object sender, EventArgs e)
        {
            ClearQamFrequency clearQamFrequency = (ClearQamFrequency)(cboClearQamScanningFrequency.SelectedItem as ClearQamFrequency).Clone();
            clearQamFrequency.CollectionType = (CollectionType)cboClearQamCollectionType.SelectedItem;

            ListViewItem newItem = new ListViewItem(clearQamFrequency.ToString());
            newItem.Tag = clearQamFrequency;
            newItem.SubItems.Add(((ClearQamProvider)cboClearQam.SelectedItem).Name);
            newItem.SubItems.Add("Clear QAM");
            newItem.SubItems.Add(clearQamFrequency.CollectionType.ToString());

            bool advancedResult = getAdvancedParameters(clearQamFrequency);
            if (!advancedResult)
                return;

            foreach (ListViewItem oldItem in lvSelectedFrequencies.Items)
            {
                ClearQamFrequency oldFrequency = oldItem.Tag as ClearQamFrequency;
                if (oldFrequency != null && oldFrequency.EqualTo(clearQamFrequency, EqualityLevel.Identity))
                {
                    if (!oldFrequency.EqualTo(clearQamFrequency, EqualityLevel.Entirely))
                    {
                        DialogResult result = MessageBox.Show("The frequency has already been selected." + Environment.NewLine + Environment.NewLine +
                            "Do you want to overwrite it?", "EPG Centre", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        switch (result)
                        {
                            case DialogResult.Cancel:
                                return;
                            case DialogResult.Yes:
                                lvSelectedFrequencies.Items.Remove(oldItem);
                                lvSelectedFrequencies.Items.Add(newItem);
                                return;
                            default:
                                lvSelectedFrequencies.Items.Add(newItem);
                                return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The frequency has already been selected with the same parameters.",
                            "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            if (lvSelectedFrequencies.Items.Count != 0)
            {
                DialogResult replace = MessageBox.Show("Only one frequency can be selected." + Environment.NewLine + Environment.NewLine +
                    "Do you want to overwrite the existing frequency?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (replace)
                {
                    case DialogResult.Yes:
                        lvSelectedFrequencies.Items.RemoveAt(0);
                        lvSelectedFrequencies.Items.Add(newItem);
                        return;
                    default:
                        return;
                }
            }
            else
                lvSelectedFrequencies.Items.Add(newItem);
        }

        private void btTuningParameters_Click(object sender, EventArgs e)
        {
            TuningFrequency tuningFrequency = lvSelectedFrequencies.SelectedItems[0].Tag as TuningFrequency;

            AdvancedParameters advancedParameters = new AdvancedParameters();
            advancedParameters.Initialize(tuningFrequency);
            advancedParameters.ShowDialog();

            btDelete.Enabled = false;
            btTuningParameters.Enabled = false;
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            lvSelectedFrequencies.Items.RemoveAt(0);
            btDelete.Enabled = false;
        }

        private bool getAdvancedParameters(TuningFrequency tuningFrequency)
        {
            AdvancedParameters advancedParameters = new AdvancedParameters();
            advancedParameters.Initialize(tuningFrequency);
            DialogResult advancedResult = advancedParameters.ShowDialog();
            if (advancedResult == DialogResult.Cancel)
            {
                MessageBox.Show("The frequency has NOT been selected.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return (false); ;
            }

            return (true);
        }

        private void lvSelectedFrequencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            btDelete.Enabled = (lvSelectedFrequencies.SelectedItems.Count != 0);            
            btTuningParameters.Enabled = (lvSelectedFrequencies.SelectedItems.Count != 0);
        }

        private void cboWMCSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboWMCSeries.SelectedIndex != 0 && cbCheckForRepeats.Checked)
            {
                MessageBox.Show("Windows Media Center series/repeat checking and EPG Collector repeat checking cannot be enabled at the same time." +
                    Environment.NewLine + Environment.NewLine +
                    "EPG Collector repeat checking will be disabled.",
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                cbCheckForRepeats.Checked = false;
            }
        }

        private void btBrowseLogoPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browsePath = new FolderBrowserDialog();
            browsePath.Description = "EPG Centre - Find Channel Logo Directory";
            if (currentChannelLogoPath == null)
                browsePath.SelectedPath = Path.Combine(RunParameters.DataDirectory, "Images") + Path.DirectorySeparatorChar;
            else
                browsePath.SelectedPath = currentLookupBasePath;
            DialogResult result = browsePath.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            currentChannelLogoPath = browsePath.SelectedPath + @"\";
            tbChannelLogoPath.Text = currentChannelLogoPath;
        }
    }
}

