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
using System.Windows.Forms;

using DirectShow;
using DomainObjects;

namespace EPGCentre
{
    internal partial class FrequencySelectionControl : UserControl
    {
        internal TuningFrequency SelectedFrequency
        {
            get
            {
                if (tbcDeliverySystem.SelectedTab.Name == "tbpSatellite")
                    return (getSatelliteFrequency());
                else
                {
                    if (tbcDeliverySystem.SelectedTab.Name == "tbpTerrestrial")
                        return (getTerrestrialFrequency());
                    else
                    {
                        if (tbcDeliverySystem.SelectedTab.Name == "tbpCable")
                            return (getCableFrequency());
                        else
                        {
                            if (tbcDeliverySystem.SelectedTab.Name == "tbpAtsc")
                                return (getAtscFrequency());
                            else
                            {
                                if (tbcDeliverySystem.SelectedTab.Name == "tbpClearQAM")
                                    return (getClearQamFrequency());
                                else
                                {
                                    if (tbcDeliverySystem.SelectedTab.Name == "tbpISDBSatellite")
                                        return (getISDBSatelliteFrequency());
                                    else
                                    {
                                        if (tbcDeliverySystem.SelectedTab.Name == "tbpISDBTerrestrial")
                                            return (getISDBTerrestrialFrequency());
                                        else
                                        {
                                            if (tbcDeliverySystem.SelectedTab.Name == "tbpFile")
                                                return (getFileFrequency());
                                            else
                                                return (getStreamFrequency());
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
        }

        private bool loaded;

        private static SatelliteFrequency currentSatelliteFrequency;
        private static TerrestrialFrequency currentTerrestrialFrequency;
        private static CableFrequency currentCableFrequency;
        private static AtscFrequency currentAtscFrequency;
        private static ClearQamFrequency currentClearQamFrequency;
        private static ISDBSatelliteFrequency currentISDBSatelliteFrequency;
        private static ISDBTerrestrialFrequency currentISDBTerrestrialFrequency;
        private static FileFrequency currentFileFrequency;
        private static StreamFrequency currentStreamFrequency;
        
        internal FrequencySelectionControl()
        {
            InitializeComponent();
        }

        internal void Process(bool hideFileTab)
        {
            Process(hideFileTab, false, false);
        }

        internal void Process(bool hideFileTab, bool hideStreamTab, bool providersOnly)
        {
            if (loaded)
                return;

            Satellite.Load();
            TerrestrialProvider.Load();
            CableProvider.Load();
            AtscProvider.Load();
            ClearQamProvider.Load();
            ISDBSatelliteProvider.Load();
            ISDBTerrestrialProvider.Load();

            tbcDeliverySystem.TabPages.RemoveByKey("tbpISDBSatellite");
            tbcDeliverySystem.TabPages.RemoveByKey("tbpISDBTerrestrial");
        
            initializeSatelliteTab(providersOnly);
            initializeTerrestrialTab(providersOnly);
            initializeCableTab(providersOnly);
            initializeAtscTab(providersOnly);
            initializeClearQamTab(providersOnly);
            initializeISDBSTab(providersOnly);
            initializeISDBTTab(providersOnly);
            initializeFileTab();
            initializeStreamTab();

            if (hideFileTab)
                tbcDeliverySystem.TabPages.RemoveByKey("tbpFile");
            if (hideStreamTab)
                tbcDeliverySystem.TabPages.RemoveByKey("tbpStream");

            loaded = true;
        }

        private void initializeSatelliteTab(bool providersOnly)
        {
            bool satelliteUsed = false;

            foreach (Tuner tuner in Tuner.TunerCollection)
            {
                if (!tuner.Name.ToUpper().Contains("DVBLINK") && tuner.Supports(TunerNodeType.Satellite))
                    satelliteUsed = true;
            }

            if (!satelliteUsed)
            {
                tbcDeliverySystem.TabPages.RemoveByKey("tbpSatellite");
                return;
            }

            if (cboSatellite.Items.Count == 0)
            {
                foreach (Satellite satellite in Satellite.Providers)
                    cboSatellite.Items.Add(satellite);
            }
            if (currentSatelliteFrequency != null)
            {
                cboSatellite.Text = currentSatelliteFrequency.Provider.Name;
                cboDVBSScanningFrequency.Text = currentSatelliteFrequency.ToString();

                if (currentSatelliteFrequency.SatIpFrontend != -1)
                    udDvbsSatIpFrontend.SelectedItem = udDvbsSatIpFrontend.Items[currentSatelliteFrequency.SatIpFrontend].ToString();
                else
                    udDvbsSatIpFrontend.SelectedIndex = 0;
            }
            else
            {
                cboSatellite.SelectedIndex = 0;
                udDvbsSatIpFrontend.SelectedIndex = 0;
            }

            if (cboLNBType.Items.Count == 0)
            {
                foreach (LNBType lnbType in LNBType.LNBTypes)
                    cboLNBType.Items.Add(lnbType);
            }

            if (currentSatelliteFrequency != null && currentSatelliteFrequency.SatelliteDish != null)
            {
                txtLNBLow.Text = currentSatelliteFrequency.SatelliteDish.LNBLowBandFrequency.ToString();
                txtLNBHigh.Text = currentSatelliteFrequency.SatelliteDish.LNBHighBandFrequency.ToString();
                txtLNBSwitch.Text = currentSatelliteFrequency.SatelliteDish.LNBSwitchFrequency.ToString();
                cboLNBType.Text = currentSatelliteFrequency.SatelliteDish.LNBType.ToString();
            }
            else
            {
                SatelliteDish satelliteDish = SatelliteDish.FirstDefault;

                txtLNBLow.Text = satelliteDish.LNBLowBandFrequency.ToString();
                txtLNBHigh.Text = satelliteDish.LNBHighBandFrequency.ToString();
                txtLNBSwitch.Text = satelliteDish.LNBSwitchFrequency.ToString();
                cboLNBType.SelectedIndex = 0;
            }

            fillTunersList(TunerNodeType.Satellite, clbSatelliteTuners, currentSatelliteFrequency);

            if (cboDiseqc.Items.Count == 0)
            {
                foreach (DiseqcSettings diseqcSetting in Enum.GetValues(typeof(DiseqcSettings)))
                    cboDiseqc.Items.Add(diseqcSetting);
            }

            if (currentSatelliteFrequency != null && currentSatelliteFrequency.DiseqcRunParamters.DiseqcSwitch != null)
                cboDiseqc.Text = currentSatelliteFrequency.DiseqcRunParamters.DiseqcSwitch;
            else
                cboDiseqc.SelectedIndex = 0;

            Collection<string> diseqcHandlers = DiseqcHandlerBase.Handlers;
            cboDiseqcHandler.Items.Clear();
            foreach (string diseqcHandler in diseqcHandlers)
                cboDiseqcHandler.Items.Add(diseqcHandler);

            if (currentSatelliteFrequency != null && currentSatelliteFrequency.DiseqcRunParamters.DiseqcHandler != null)
            {
                foreach (string diseqcHandler in cboDiseqcHandler.Items)
                {
                    if (diseqcHandler.ToUpperInvariant() == currentSatelliteFrequency.DiseqcRunParamters.DiseqcHandler.ToUpperInvariant())
                        cboDiseqcHandler.SelectedItem = diseqcHandler;
                }
            }
            else
            {
                cboDiseqcHandler.SelectedItem = diseqcHandlers[0];
                cboDiseqcHandler.Text = diseqcHandlers[0];
            }

            if (currentSatelliteFrequency != null)
            {
                cbUseSafeDiseqc.Checked = OptionEntry.IsDefined(currentSatelliteFrequency.DiseqcRunParamters.Options, OptionName.UseSafeDiseqc);
                cbSwitchAfterPlay.Checked = OptionEntry.IsDefined(currentSatelliteFrequency.DiseqcRunParamters.Options, OptionName.SwitchAfterPlay);
                cbSwitchAfterTune.Checked = OptionEntry.IsDefined(currentSatelliteFrequency.DiseqcRunParamters.Options, OptionName.SwitchAfterTune);
                cbRepeatDiseqc.Checked = OptionEntry.IsDefined(currentSatelliteFrequency.DiseqcRunParamters.Options, OptionName.RepeatDiseqc);
                cbDisableDriverDiseqc.Checked = OptionEntry.IsDefined(currentSatelliteFrequency.DiseqcRunParamters.Options, OptionName.DisableDriverDiseqc);
                cbUseDiseqcCommands.Checked = OptionEntry.IsDefined(currentSatelliteFrequency.DiseqcRunParamters.Options, OptionName.UseDiseqcCommand);
            }

            lblSatelliteFrequency.Enabled = !providersOnly;
            cboDVBSScanningFrequency.Enabled = !providersOnly;
        }

        private void initializeTerrestrialTab(bool providersOnly)
        {
            bool terrestrialUsed = false;

            foreach (Tuner tuner in Tuner.TunerCollection)
            {
                if (!tuner.Name.ToUpper().Contains("DVBLINK") && tuner.Supports(TunerNodeType.Terrestrial))
                    terrestrialUsed = true;
            }

            if (!terrestrialUsed)
            {
                tbcDeliverySystem.TabPages.RemoveByKey("tbpTerrestrial");
                return;
            }

            if (cboCountry.Items.Count == 0)
            {
                foreach (Country country in TerrestrialProvider.Countries)
                    cboCountry.Items.Add(country);
            }

            if (currentTerrestrialFrequency != null)
            {
                cboCountry.Text = ((TerrestrialProvider)currentTerrestrialFrequency.Provider).Country.Name;
                cboArea.Text = ((TerrestrialProvider)currentTerrestrialFrequency.Provider).Area.Name;
                cboDVBTScanningFrequency.Text = currentTerrestrialFrequency.ToString();

                if (currentTerrestrialFrequency.SatIpFrontend != -1)
                    udDvbtSatIpFrontend.SelectedItem = udDvbtSatIpFrontend.Items[currentTerrestrialFrequency.SatIpFrontend].ToString();
                else
                    udDvbtSatIpFrontend.SelectedIndex = 0;
            }
            else
            {
                cboCountry.SelectedIndex = 0;
                udDvbtSatIpFrontend.SelectedIndex = 0;
            }                

            fillTunersList(TunerNodeType.Terrestrial, clbTerrestrialTuners, currentTerrestrialFrequency);

            lblTerrestrialFrequency.Enabled = !providersOnly;
            cboDVBTScanningFrequency.Enabled = !providersOnly;
        }

        private void initializeCableTab(bool providersOnly)
        {
            bool cableUsed = false;

            foreach (Tuner tuner in Tuner.TunerCollection)
            {
                if (!tuner.Name.ToUpper().Contains("DVBLINK") && tuner.Supports(TunerNodeType.Cable))
                    cableUsed = true;
            }

            if (!cableUsed)
            {
                tbcDeliverySystem.TabPages.RemoveByKey("tbpCable");
                return;
            }

            if (cboCable.Items.Count == 0)
            {
                foreach (CableProvider cableProvider in CableProvider.Providers)
                    cboCable.Items.Add(cableProvider);
            }

            if (currentCableFrequency != null)
            {
                cboCable.Text = currentCableFrequency.Provider.Name;
                cboCableScanningFrequency.Text = currentCableFrequency.ToString();

                if (currentCableFrequency.SatIpFrontend != -1)
                    udDvbcSatIpFrontend.SelectedItem = udDvbcSatIpFrontend.Items[currentCableFrequency.SatIpFrontend].ToString();
                else
                    udDvbcSatIpFrontend.SelectedIndex = 0;
            }
            else
            {
                cboCable.SelectedIndex = 0;
                udDvbcSatIpFrontend.SelectedIndex = 0;
            }

            fillTunersList(TunerNodeType.Cable, clbCableTuners, currentCableFrequency);

            lblCableFrequency.Enabled = !providersOnly;
            cboCableScanningFrequency.Enabled = !providersOnly;
        }

        private void initializeAtscTab(bool providersOnly)
        {
            bool atscUsed = false;

            foreach (Tuner tuner in Tuner.TunerCollection)
            {
                if (!tuner.Name.ToUpper().Contains("DVBLINK") && tuner.Supports(TunerNodeType.ATSC))
                    atscUsed = true;
            }

            if (!atscUsed)
            {
                tbcDeliverySystem.TabPages.RemoveByKey("tbpAtsc");
                return;
            }

            if (cboAtscProvider.Items.Count == 0)
            {
                foreach (AtscProvider atscProvider in AtscProvider.Providers)
                    cboAtscProvider.Items.Add(atscProvider);
            }

            if (currentAtscFrequency != null)
            {
                cboAtscProvider.Text = currentAtscFrequency.Provider.Name;
                cboAtscScanningFrequency.Text = currentAtscFrequency.ToString();
            }
            else
                cboAtscProvider.SelectedIndex = 0;

            fillTunersList(TunerNodeType.ATSC, clbAtscTuners, currentAtscFrequency);

            lblAtscChannel.Enabled = !providersOnly;
            cboAtscScanningFrequency.Enabled = !providersOnly;
        }

        private void initializeClearQamTab(bool providersOnly)
        {
            bool clearQamUsed = false;

            foreach (Tuner tuner in Tuner.TunerCollection)
            {
                if (!tuner.Name.ToUpper().Contains("DVBLINK") && tuner.Supports(TunerNodeType.ATSC))
                    clearQamUsed = true;
            }

            if (!clearQamUsed)
            {
                tbcDeliverySystem.TabPages.RemoveByKey("tbpClearQAM");
                return;
            }

            if (cboClearQamProvider.Items.Count == 0)
            {
                foreach (ClearQamProvider clearQamProvider in ClearQamProvider.Providers)
                    cboClearQamProvider.Items.Add(clearQamProvider);
            }

            if (currentClearQamFrequency != null)
            {
                cboClearQamProvider.Text = currentClearQamFrequency.Provider.Name;
                cboClearQamScanningFrequency.Text = currentClearQamFrequency.ToString();
            }
            else
                cboClearQamProvider.SelectedIndex = 0;

            fillTunersList(TunerNodeType.Cable, clbClearQamTuners, currentClearQamFrequency);

            lblClearQamChannel.Enabled = !providersOnly;
            cboClearQamScanningFrequency.Enabled = !providersOnly;
        }

        private void initializeISDBSTab(bool providersOnly)
        {
            bool satelliteUsed = false;

            foreach (Tuner tuner in Tuner.TunerCollection)
            {
                if (!tuner.Name.ToUpper().Contains("DVBLINK") && tuner.Supports(TunerNodeType.ISDBS))
                    satelliteUsed = true;
            }

            if (!satelliteUsed)
            {
                tbcDeliverySystem.TabPages.RemoveByKey("tbpISDBSatellite");
                return;
            }

            if (cboISDBSatellite.Items.Count == 0)
            {
                foreach (ISDBSatelliteProvider provider in ISDBSatelliteProvider.Providers)
                    cboISDBSatellite.Items.Add(provider);
            }

            if (currentISDBSatelliteFrequency != null)
            {
                cboISDBSatellite.Text = currentISDBSatelliteFrequency.Provider.Name;
                cboISDBSScanningFrequency.Text = currentISDBSatelliteFrequency.ToString();
            }
            else
                cboISDBSatellite.SelectedIndex = 0;
            
            if (currentISDBSatelliteFrequency != null && currentISDBSatelliteFrequency.SatelliteDish != null)
            {
                txtISDBLNBLow.Text = currentSatelliteFrequency.SatelliteDish.LNBLowBandFrequency.ToString();
                txtISDBLNBHigh.Text = currentSatelliteFrequency.SatelliteDish.LNBHighBandFrequency.ToString();
                txtISDBLNBSwitch.Text = currentSatelliteFrequency.SatelliteDish.LNBSwitchFrequency.ToString();                
            }
            else
            {
                SatelliteDish satelliteDish = SatelliteDish.FirstDefault;

                txtISDBLNBLow.Text = satelliteDish.LNBLowBandFrequency.ToString();
                txtISDBLNBHigh.Text = satelliteDish.LNBHighBandFrequency.ToString();
                txtISDBLNBSwitch.Text = satelliteDish.LNBSwitchFrequency.ToString();                
            }

            fillTunersList(TunerNodeType.ISDBS, clbISDBSatelliteTuners, currentISDBSatelliteFrequency);

            lblISDBSFrequency.Enabled = !providersOnly;
            cboISDBSScanningFrequency.Enabled = !providersOnly;
        }

        private void initializeISDBTTab(bool providersOnly)
        {
            bool terrestrialUsed = false;

            foreach (Tuner tuner in Tuner.TunerCollection)
            {
                if (!tuner.Name.ToUpper().Contains("DVBLINK") && tuner.Supports(TunerNodeType.ISDBT))
                    terrestrialUsed = true;
            }

            if (!terrestrialUsed)
            {
                tbcDeliverySystem.TabPages.RemoveByKey("tbpISDBTerrestrial");
                return;
            }

            if (cboISDBTProvider.Items.Count == 0)
            {
                foreach (ISDBTerrestrialProvider provider in ISDBTerrestrialProvider.Providers)
                    cboISDBTProvider.Items.Add(provider);
            }

            if (currentISDBTerrestrialFrequency != null)
            {
                cboISDBTProvider.Text = currentISDBTerrestrialFrequency.Provider.Name;
                cboISDBTScanningFrequency.Text = currentISDBTerrestrialFrequency.ToString();
            }
            else
                cboISDBTProvider.SelectedIndex = 0;

            fillTunersList(TunerNodeType.ISDBT, clbISDBTerrestrialTuners, currentISDBTerrestrialFrequency);

            lblISDBTChannel.Enabled = !providersOnly;
            cboISDBTScanningFrequency.Enabled = !providersOnly;
        }

        private void initializeFileTab()
        {
            if (currentFileFrequency != null)
                tbDeliveryFilePath.Text = currentFileFrequency.Path;
            else
                tbDeliveryFilePath.Text = null;
        }

        private void initializeStreamTab()
        {
            if (currentStreamFrequency != null)
            {
                if (currentStreamFrequency.IPAddress != "0.0.0.0")
                    tbStreamIpAddress.Text = currentStreamFrequency.IPAddress;
                else
                    tbStreamIpAddress.Text = string.Empty;
                nudStreamPortNumber.Value = currentStreamFrequency.PortNumber;

                if (!string.IsNullOrWhiteSpace(currentStreamFrequency.MulticastSource))
                {
                    tbStreamMulticastSourceIP.Text = currentStreamFrequency.MulticastSource;
                    nudStreamMulticastSourcePort.Value = currentStreamFrequency.MulticastPort;
                }
                else
                {
                    tbStreamMulticastSourceIP.Text = null;
                    nudStreamMulticastSourcePort.Value = 0;
                }

                cboStreamProtocol.Text = currentStreamFrequency.Protocol.ToString();
                tbStreamPath.Text = currentStreamFrequency.Path;                
            }
            else
            {
                tbStreamIpAddress.Text = null;
                nudStreamPortNumber.Value = 80;
                tbStreamMulticastSourceIP.Text = null;
                nudStreamMulticastSourcePort.Value = 0;
                cboStreamProtocol.SelectedIndex = 0;
                tbStreamPath.Text = null;                
            }
        }

        private void fillTunersList(TunerNodeType nodeType, CheckedListBox tunerListBox, TuningFrequency tuningFrequency)
        {
            tunerListBox.Items.Clear();
            tunerListBox.Items.Add("Any available Tuner");

            bool found = false;

            foreach (Tuner tuner in Tuner.TunerCollection)
            {
                if (!tuner.Name.ToUpper().Contains("DVBLINK") && tuner.Supports(nodeType))
                {
                    tunerListBox.Items.Add(tuner);

                    if (tuningFrequency != null && SelectedTuner.Selected(tuningFrequency.SelectedTuners, Tuner.TunerCollection.IndexOf(tuner) + 1))
                    {
                        tunerListBox.SetItemChecked(tunerListBox.Items.Count - 1, true);
                        found = true;
                    }
                }
            }

            if (!found)
                tunerListBox.SetItemChecked(0, true);
        }

        private void btLNBDefaults_Click(object sender, EventArgs e)
        {
            SatelliteDish defaultSatellite = SatelliteDish.Default;

            txtLNBLow.Text = defaultSatellite.LNBLowBandFrequency.ToString();
            txtLNBHigh.Text = defaultSatellite.LNBHighBandFrequency.ToString();
            txtLNBSwitch.Text = defaultSatellite.LNBSwitchFrequency.ToString();

            cboLNBType.SelectedIndex = 0;
        }

        private void cboSatellite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSatellite.SelectedItem != null)
            {
                cboDVBSScanningFrequency.Items.Clear();
                foreach (TuningFrequency tuningFrequency in ((Satellite)cboSatellite.SelectedItem).Frequencies)
                    cboDVBSScanningFrequency.Items.Add(tuningFrequency);
                cboDVBSScanningFrequency.SelectedIndex = 0;

                if (cboDiseqc.Items.Count > 0)
                    cboDiseqc.SelectedIndex = 0;
                if (cboDiseqcHandler.Items.Count > 0)
                    cboDiseqcHandler.SelectedIndex = 0;
                cboDiseqcHandler.Enabled = false;
                cbUseSafeDiseqc.Checked = false;
                cbUseSafeDiseqc.Enabled = false;
                cbSwitchAfterPlay.Checked = false;
                cbSwitchAfterPlay.Enabled = false;
                cbSwitchAfterTune.Checked = false;
                cbSwitchAfterTune.Enabled = false;
                cbRepeatDiseqc.Checked = false;
                cbRepeatDiseqc.Enabled = false;
                cbDisableDriverDiseqc.Checked = false;
                cbDisableDriverDiseqc.Enabled = false;
                cbUseDiseqcCommands.Checked = false;
                cbUseDiseqcCommands.Enabled = false;
            }
        }

        private void cboDVBSScanningFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDVBSScanningFrequency.SelectedItem != null)
            {
                cboLNBType.Enabled = ((SatelliteFrequency)cboDVBSScanningFrequency.SelectedItem).LNBConversion;
                cboLNBType.Text = LNBType.Legacy;
            }
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

        private void cboAtscProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAtscProvider.SelectedItem != null)
            {
                cboAtscScanningFrequency.Items.Clear();
                foreach (TuningFrequency tuningFrequency in ((AtscProvider)cboAtscProvider.SelectedItem).Frequencies)
                    cboAtscScanningFrequency.Items.Add(tuningFrequency);
                cboAtscScanningFrequency.SelectedIndex = 0;
            }
        }

        private void cboClearQamProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboClearQamProvider.SelectedItem != null)
            {
                cboClearQamScanningFrequency.Items.Clear();
                foreach (TuningFrequency tuningFrequency in ((ClearQamProvider)cboClearQamProvider.SelectedItem).Frequencies)
                    cboClearQamScanningFrequency.Items.Add(tuningFrequency);
                cboClearQamScanningFrequency.SelectedIndex = 0;
            }
        }

        private void btISDBLNBDefaults_Click(object sender, EventArgs e)
        {
            SatelliteDish defaultSatellite = SatelliteDish.Default;

            txtISDBLNBLow.Text = defaultSatellite.LNBLowBandFrequency.ToString();
            txtISDBLNBHigh.Text = defaultSatellite.LNBHighBandFrequency.ToString();
            txtISDBLNBSwitch.Text = defaultSatellite.LNBSwitchFrequency.ToString();
        }

        private void cboISDBSatellite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboISDBSatellite.SelectedItem != null)
            {
                cboISDBSScanningFrequency.Items.Clear();
                foreach (TuningFrequency tuningFrequency in ((ISDBSatelliteProvider)cboISDBSatellite.SelectedItem).Frequencies)
                    cboISDBSScanningFrequency.Items.Add(tuningFrequency);
                cboISDBSScanningFrequency.SelectedIndex = 0;
            }
        }

        private void cboISDBTProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboISDBTProvider.SelectedItem != null)
            {
                cboISDBTScanningFrequency.Items.Clear();
                foreach (TuningFrequency tuningFrequency in ((ISDBTerrestrialProvider)cboISDBTProvider.SelectedItem).Channels)
                    cboISDBTScanningFrequency.Items.Add(tuningFrequency);
                cboISDBTScanningFrequency.SelectedIndex = 0;
            }
        }

        internal string ValidateForm()
        {
            if (tbcDeliverySystem.SelectedTab.Name == "tbpSatellite")
            {
                try
                {
                    int lnbLowBandFrequency = Int32.Parse(txtLNBLow.Text.Trim());
                    int lnbHighBandFrequency = Int32.Parse(txtLNBHigh.Text.Trim());
                    int lnbSwitchFrequency = Int32.Parse(txtLNBSwitch.Text.Trim());
                }
                catch (FormatException)
                {
                    return ("A dish parameter is incorrect.");
                }
                catch (OverflowException)
                {
                    return ("A dish parameter is incorrect.");
                }
            }

            if (tbcDeliverySystem.SelectedTab.Name == "tbpFile")
            {
                if (string.IsNullOrWhiteSpace(tbDeliveryFilePath.Text))
                    return ("No path entered.");
            }

            if (tbcDeliverySystem.SelectedTab.Name == "tbpStream")
            {
                string addressReply = StreamFrequency.ValidateIPAddress(tbStreamIpAddress.Text.Trim(), cboStreamProtocol.Text);
                if (addressReply != null)
                    return (addressReply);
            }

            return (null);
        }

        private void cboDiseqc_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeTuningOptions(cboDiseqc.SelectedIndex != 0);
        }

        private void changeTuningOptions(bool enabled)
        {
            if (enabled)
            {
                cboDiseqcHandler.Enabled = true;

                cbUseSafeDiseqc.Enabled = true;
                cbRepeatDiseqc.Enabled = true;
                cbSwitchAfterPlay.Enabled = true;
                cbSwitchAfterTune.Enabled = true;

                if (DiseqcHandlerBase.IsGeneric(cboDiseqcHandler.Text))
                {
                    cbUseDiseqcCommands.Enabled = true;
                    cbDisableDriverDiseqc.Enabled = true;
                }
            }
            else
            {
                cboDiseqcHandler.Enabled = false;
                if (cboDiseqcHandler.Items.Count != 0)
                    cboDiseqcHandler.SelectedIndex = 0;

                cbUseSafeDiseqc.Enabled = false;
                cbUseSafeDiseqc.Checked = false;
                cbRepeatDiseqc.Enabled = false;
                cbRepeatDiseqc.Checked = false;
                cbSwitchAfterPlay.Enabled = false;
                cbSwitchAfterPlay.Checked = false;
                cbSwitchAfterTune.Enabled = false;
                cbSwitchAfterTune.Checked = false;
                cbUseDiseqcCommands.Enabled = false;
                cbUseDiseqcCommands.Checked = false;
                cbDisableDriverDiseqc.Enabled = false;
                cbDisableDriverDiseqc.Checked = false;
            }
        }

        private void cboDiseqcHandler_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DiseqcHandlerBase.IsGeneric(cboDiseqcHandler.Text))
            {
                cbDisableDriverDiseqc.Enabled = true;
                cbUseDiseqcCommands.Enabled = true;
            }
            else
            {
                cbDisableDriverDiseqc.Enabled = false;
                cbDisableDriverDiseqc.Checked = false;
                cbUseDiseqcCommands.Enabled = false;
                cbUseDiseqcCommands.Checked = false;
            }
        }

        private void tbDeliveryFileBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Transport Stream Dump Files (*.ts)|*.ts";

            openFile.InitialDirectory = MainWindow.CurrentTSPath;

            openFile.RestoreDirectory = true;
            openFile.Title = "Find Transport Stream Dump File";

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            tbDeliveryFilePath.Text = openFile.FileName;
        }

        private TuningFrequency getSatelliteFrequency()
        {
            currentSatelliteFrequency = (SatelliteFrequency)((SatelliteFrequency)cboDVBSScanningFrequency.SelectedItem).Clone();
            
            currentSatelliteFrequency.SatelliteDish = new SatelliteDish();
            currentSatelliteFrequency.SatelliteDish.LNBLowBandFrequency = Int32.Parse(txtLNBLow.Text.Trim());
            currentSatelliteFrequency.SatelliteDish.LNBHighBandFrequency = Int32.Parse(txtLNBHigh.Text.Trim());
            currentSatelliteFrequency.SatelliteDish.LNBSwitchFrequency = Int32.Parse(txtLNBSwitch.Text.Trim());
            currentSatelliteFrequency.SatelliteDish.LNBType = LNBType.GetInstance(cboLNBType.Text);

            if (udDvbsSatIpFrontend.SelectedIndex != 0)
                currentSatelliteFrequency.SatIpFrontend = udDvbsSatIpFrontend.SelectedIndex;
            else
                currentSatelliteFrequency.SatIpFrontend = -1;

            setTuner(currentSatelliteFrequency, clbSatelliteTuners);

            if (cboDiseqc.SelectedIndex != 0)
            {
                currentSatelliteFrequency.DiseqcRunParamters.DiseqcSwitch = cboDiseqc.Text;

                if (cbUseSafeDiseqc.Checked)
                    currentSatelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.UseSafeDiseqc));
                if (cbSwitchAfterPlay.Checked)
                    currentSatelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.SwitchAfterPlay));
                if (cbSwitchAfterTune.Checked)
                    currentSatelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.SwitchAfterTune));
                if (cbRepeatDiseqc.Checked)
                    currentSatelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.RepeatDiseqc));

                if (DiseqcHandlerBase.IsGeneric(cboDiseqcHandler.Text))
                {
                    if (cbDisableDriverDiseqc.Checked)
                        currentSatelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.DisableDriverDiseqc));
                    if (cbUseDiseqcCommands.Checked)
                        currentSatelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.UseDiseqcCommand));
                }

                currentSatelliteFrequency.DiseqcRunParamters.DiseqcHandler = cboDiseqcHandler.Text;
            }

            return (currentSatelliteFrequency);            
        }

        private TuningFrequency getTerrestrialFrequency()
        {
            currentTerrestrialFrequency = (TerrestrialFrequency)((TerrestrialFrequency)cboDVBTScanningFrequency.SelectedItem).Clone();
            
            if (udDvbtSatIpFrontend.SelectedIndex != 0)
                currentTerrestrialFrequency.SatIpFrontend = udDvbtSatIpFrontend.SelectedIndex;
            else
                currentTerrestrialFrequency.SatIpFrontend = -1;

            setTuner(currentTerrestrialFrequency, clbTerrestrialTuners);
            
            return (currentTerrestrialFrequency);
        }

        private TuningFrequency getCableFrequency()
        {
            currentCableFrequency = (CableFrequency)((CableFrequency)cboCableScanningFrequency.SelectedItem).Clone();
            
            if (udDvbtSatIpFrontend.SelectedIndex != 0)
                currentCableFrequency.SatIpFrontend = udDvbcSatIpFrontend.SelectedIndex;
            else
                currentCableFrequency.SatIpFrontend = -1;

            setTuner(currentCableFrequency, clbCableTuners);

            return (currentCableFrequency);
        }

        private TuningFrequency getAtscFrequency()
        {
            currentAtscFrequency = (AtscFrequency)((AtscFrequency)cboAtscScanningFrequency.SelectedItem).Clone();            
            setTuner(currentAtscFrequency, clbAtscTuners);

            return (currentAtscFrequency);
        }

        private TuningFrequency getClearQamFrequency()
        {
            currentClearQamFrequency = (ClearQamFrequency)((ClearQamFrequency)cboClearQamScanningFrequency.SelectedItem).Clone();
            setTuner(currentClearQamFrequency, clbClearQamTuners);

            return (currentClearQamFrequency);
        }

        private TuningFrequency getISDBSatelliteFrequency()
        {
            currentISDBSatelliteFrequency = (ISDBSatelliteFrequency)((ISDBSatelliteFrequency)cboISDBSScanningFrequency.SelectedItem).Clone();
            
            currentISDBSatelliteFrequency.SatelliteDish = new SatelliteDish();
            currentISDBSatelliteFrequency.SatelliteDish.LNBLowBandFrequency = Int32.Parse(txtLNBLow.Text.Trim());
            currentISDBSatelliteFrequency.SatelliteDish.LNBHighBandFrequency = Int32.Parse(txtLNBHigh.Text.Trim());
            currentISDBSatelliteFrequency.SatelliteDish.LNBSwitchFrequency = Int32.Parse(txtLNBSwitch.Text.Trim());
            currentISDBSatelliteFrequency.SatelliteDish.LNBType = LNBType.GetInstance(cboLNBType.Text);

            setTuner(currentISDBSatelliteFrequency, clbISDBSatelliteTuners);

            return (currentISDBSatelliteFrequency);
        }

        private TuningFrequency getISDBTerrestrialFrequency()
        {
            currentISDBTerrestrialFrequency = (ISDBTerrestrialFrequency)((ISDBTerrestrialFrequency)cboISDBTScanningFrequency.SelectedItem).Clone();
            setTuner(currentISDBTerrestrialFrequency, clbISDBTerrestrialTuners);

            return (currentISDBTerrestrialFrequency);
        }

        private void setTuner(TuningFrequency tuningFrequency, CheckedListBox clbTuners)
        {
            for (int index = 1; index < clbTuners.Items.Count; index++)
            {
                if (clbTuners.GetItemChecked(index))
                {
                    Tuner tuner = (Tuner)clbTuners.Items[index];

                    if (!tuner.IsServerTuner)
                        tuningFrequency.SelectedTuners.Add(new SelectedTuner(Tuner.TunerCollection.IndexOf(tuner) + 1));
                    else
                        tuningFrequency.SelectedTuners.Add(new SelectedTuner(tuner.UniqueIdentity));
                }
            }
        }

        private TuningFrequency getFileFrequency()
        {
            currentFileFrequency = new FileFrequency();
            currentFileFrequency.Path = tbDeliveryFilePath.Text;

            return (currentFileFrequency);
        }

        private TuningFrequency getStreamFrequency()
        {
            currentStreamFrequency = new StreamFrequency();

            if (!string.IsNullOrWhiteSpace(tbStreamIpAddress.Text))
                currentStreamFrequency.IPAddress = tbStreamIpAddress.Text.Trim();
            else
                currentStreamFrequency.IPAddress = "0.0.0.0";

            currentStreamFrequency.PortNumber = (int)nudStreamPortNumber.Value;
            currentStreamFrequency.Protocol = (StreamProtocol)Enum.Parse(typeof(StreamProtocol), cboStreamProtocol.Text);

            if (!string.IsNullOrWhiteSpace(tbStreamPath.Text))
                currentStreamFrequency.Path = tbStreamPath.Text.Trim();

            return (currentStreamFrequency);
        }

        private void btFindIPAdress_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            FindIPAddress findAddress = new FindIPAddress();
            DialogResult result = findAddress.ShowDialog();

            Cursor.Current = Cursors.Default;

            if (result == DialogResult.Cancel)
                return;

            tbStreamIpAddress.Text = findAddress.SelectedAddress.ToString();
        }

        private void clbSatelliteTuners_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeTunerListBox(clbSatelliteTuners); 
        }

        private void clbTerrestrialTuners_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeTunerListBox(clbTerrestrialTuners);  
        }

        private void clbCableTuners_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeTunerListBox(clbCableTuners);  
        }

        private void clbAtscTuners_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeTunerListBox(clbAtscTuners);  
        }

        private void clbClearQamTuners_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeTunerListBox(clbClearQamTuners);  
        }

        private void clbISDBSatelliteTuners_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeTunerListBox(clbISDBSatelliteTuners);  
        }

        private void clbISDBTerrestrialTuners_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeTunerListBox(clbISDBTerrestrialTuners);  
        }

        private void changeTunerListBox(CheckedListBox listBox)
        {
            if (listBox.SelectedIndices.Count == 0)
                return;

            if (listBox.SelectedIndices[0] == 0)
            {
                for (int index = 1; index < listBox.Items.Count; index++)
                    listBox.SetItemChecked(index, false);
            }
            else
                listBox.SetItemChecked(0, false);
        }
    }
}

