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
using System.Windows.Forms;

using DomainObjects;
using DirectShow;

namespace EPGCentre
{
    internal partial class ChannelScanParameters : Form
    {
        internal TuningFrequency SelectedFrequency { get { return (getFrequencyParameters(tuningFrequency)); } }

        private TuningFrequency tuningFrequency;
        private TuningFrequency currentFrequency;

        private static SatelliteFrequency currentSatelliteFrequency;
        private static TerrestrialFrequency currentTerrestrialFrequency;
        private static CableFrequency currentCableFrequency;
        private static AtscFrequency currentAtscFrequency;
        private static ClearQamFrequency currentClearQamFrequency;

        private ChannelScanParameters() { }

        internal ChannelScanParameters(TuningFrequency tuningFrequency) : base()
        {
            InitializeComponent();

            this.tuningFrequency = tuningFrequency;
            currentFrequency = getCurrentFrequency(tuningFrequency);

            fillTunersList(tuningFrequency.TunerType, clbTuners, currentFrequency);

            foreach (LNBType lnbType in LNBType.LNBTypes)
                cboLNBType.Items.Add(lnbType);

            foreach (DiseqcSettings diseqcSetting in Enum.GetValues(typeof(DiseqcSettings)))
                cboDiseqc.Items.Add(diseqcSetting);

            foreach (string diseqcHandler in DiseqcHandlerBase.Handlers)
                cboDiseqcHandler.Items.Add(diseqcHandler);

            if (tuningFrequency.TunerType != TunerType.Satellite)
            {
                cboLNBType.SelectedIndex = 0;
                gpDish.Enabled = false;
                cboDiseqc.SelectedIndex = 0;
                cboDiseqcHandler.SelectedIndex = 0;
                gpDiseqc.Enabled = false;

                if (tuningFrequency.TunerType != TunerType.Terrestrial && tuningFrequency.TunerType != TunerType.Cable)
                {
                    udDvbsSatIpFrontend.SelectedIndex = 0;
                    gpSatIp.Enabled = false;
                }
                else
                {
                    if (currentFrequency != null && currentFrequency.SatIpFrontend != -1)
                        udDvbsSatIpFrontend.SelectedItem = udDvbsSatIpFrontend.Items[currentFrequency.SatIpFrontend].ToString();
                    else
                        udDvbsSatIpFrontend.SelectedIndex = 0;
                }
                
                return;
            }

            if (currentFrequency == null)
            {
                SatelliteDish satelliteDish = SatelliteDish.FirstDefault;
                txtLNBLow.Text = satelliteDish.LNBLowBandFrequency.ToString();
                txtLNBHigh.Text = satelliteDish.LNBHighBandFrequency.ToString();
                txtLNBSwitch.Text = satelliteDish.LNBSwitchFrequency.ToString();
                cboLNBType.Text = satelliteDish.LNBType.ToString();
                gpDish.Enabled = true;

                cboDiseqc.SelectedIndex = 0;
                cboDiseqcHandler.SelectedIndex = 0;
                gpDiseqc.Enabled = true;

                udDvbsSatIpFrontend.SelectedIndex = 0;
                gpSatIp.Enabled = true;

                return;
            }
            else
            {
                SatelliteFrequency satelliteFrequency = currentFrequency as SatelliteFrequency;

                if (satelliteFrequency.SatelliteDish != null)
                {
                    txtLNBLow.Text = satelliteFrequency.SatelliteDish.LNBLowBandFrequency.ToString();
                    txtLNBHigh.Text = satelliteFrequency.SatelliteDish.LNBHighBandFrequency.ToString();
                    txtLNBSwitch.Text = satelliteFrequency.SatelliteDish.LNBSwitchFrequency.ToString();
                    cboLNBType.Text = satelliteFrequency.SatelliteDish.LNBType.ToString();
                }
                else
                {
                    SatelliteDish satelliteDish = SatelliteDish.FirstDefault;

                    txtLNBLow.Text = satelliteDish.LNBLowBandFrequency.ToString();
                    txtLNBHigh.Text = satelliteDish.LNBHighBandFrequency.ToString();
                    txtLNBSwitch.Text = satelliteDish.LNBSwitchFrequency.ToString();
                    cboLNBType.SelectedIndex = 0;
                }

                if (satelliteFrequency.DiseqcRunParamters.DiseqcSwitch != null)
                    cboDiseqc.Text = satelliteFrequency.DiseqcRunParamters.DiseqcSwitch;
                else
                    cboDiseqc.SelectedIndex = 0;

                if (satelliteFrequency.DiseqcRunParamters.DiseqcHandler != null)
                {
                    foreach (string diseqcHandler in cboDiseqcHandler.Items)
                    {
                        if (diseqcHandler.ToUpperInvariant() == satelliteFrequency.DiseqcRunParamters.DiseqcHandler.ToUpperInvariant())
                            cboDiseqcHandler.SelectedItem = diseqcHandler;
                    }
                }
                else
                    cboDiseqcHandler.SelectedIndex = 0;

                cbUseSafeDiseqc.Checked = OptionEntry.IsDefined(satelliteFrequency.DiseqcRunParamters.Options, OptionName.UseSafeDiseqc);
                cbSwitchAfterPlay.Checked = OptionEntry.IsDefined(satelliteFrequency.DiseqcRunParamters.Options, OptionName.SwitchAfterPlay);
                cbSwitchAfterTune.Checked = OptionEntry.IsDefined(satelliteFrequency.DiseqcRunParamters.Options, OptionName.SwitchAfterTune);
                cbRepeatDiseqc.Checked = OptionEntry.IsDefined(satelliteFrequency.DiseqcRunParamters.Options, OptionName.RepeatDiseqc);
                cbDisableDriverDiseqc.Checked = OptionEntry.IsDefined(satelliteFrequency.DiseqcRunParamters.Options, OptionName.DisableDriverDiseqc);
                cbUseDiseqcCommands.Checked = OptionEntry.IsDefined(satelliteFrequency.DiseqcRunParamters.Options, OptionName.UseDiseqcCommand);
                
                if (satelliteFrequency.SatIpFrontend != -1)
                    udDvbsSatIpFrontend.SelectedItem = udDvbsSatIpFrontend.Items[satelliteFrequency.SatIpFrontend].ToString();
                else
                    udDvbsSatIpFrontend.SelectedIndex = 0;
            }
        }

        private TuningFrequency getCurrentFrequency(TuningFrequency tuningFrequency)
        {
            switch (tuningFrequency.TunerType)
            {
                case TunerType.Satellite:
                    return (currentSatelliteFrequency);
                case TunerType.Terrestrial:
                    return (currentTerrestrialFrequency);
                case TunerType.Cable:
                    return (currentCableFrequency);
                case TunerType.ATSC:
                case TunerType.ATSCCable:
                    return (currentAtscFrequency);
                case TunerType.ClearQAM:
                    return (currentClearQamFrequency);
                default:
                    return (null);
            }
        }

        private void fillTunersList(TunerType tunerType, CheckedListBox tunerListBox, TuningFrequency tuningFrequency)
        {
            tunerListBox.Items.Clear();
            tunerListBox.Items.Add("Any available Tuner");

            bool found = false;

            foreach (Tuner tuner in Tuner.TunerCollection)
            {
                if (!tuner.Name.ToUpper().Contains("DVBLINK") && tuner.Supports(tunerType))
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

        private void clbTuners_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbTuners.SelectedIndices[0] == 0)
            {
                for (int index = 1; index < clbTuners.Items.Count; index++)
                    clbTuners.SetItemChecked(index, false);
            }
            else
                clbTuners.SetItemChecked(0, false);
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (gpDish.Enabled)
            {
                try
                {
                    Int32.Parse(txtLNBLow.Text.Trim());
                    Int32.Parse(txtLNBHigh.Text.Trim());
                    Int32.Parse(txtLNBSwitch.Text.Trim());
                }
                catch (FormatException)
                {
                    MessageBox.Show("A dish parameter is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("A dish parameter is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btLNBDefaults_Click(object sender, EventArgs e)
        {
            SatelliteDish defaultSatellite = SatelliteDish.Default;

            txtLNBLow.Text = defaultSatellite.LNBLowBandFrequency.ToString();
            txtLNBHigh.Text = defaultSatellite.LNBHighBandFrequency.ToString();
            txtLNBSwitch.Text = defaultSatellite.LNBSwitchFrequency.ToString();

            cboLNBType.SelectedIndex = 0;
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

        private TuningFrequency getFrequencyParameters(TuningFrequency tuningFrequency)
        {
            SatelliteFrequency satelliteFrequency = tuningFrequency as SatelliteFrequency;
            if (satelliteFrequency == null)
            {
                getTuner(tuningFrequency, clbTuners);

                if (tuningFrequency.TunerType == TunerType.Terrestrial || tuningFrequency.TunerType == TunerType.Cable)
                {
                    if (udDvbsSatIpFrontend.SelectedIndex != 0)
                        tuningFrequency.SatIpFrontend = udDvbsSatIpFrontend.SelectedIndex;
                    else
                        tuningFrequency.SatIpFrontend = -1;
                }

                setCurrentFrequency(tuningFrequency);
                return (tuningFrequency);
            }

            getTuner(satelliteFrequency, clbTuners);

            satelliteFrequency.SatelliteDish = new SatelliteDish();
            satelliteFrequency.SatelliteDish.LNBLowBandFrequency = Int32.Parse(txtLNBLow.Text.Trim());
            satelliteFrequency.SatelliteDish.LNBHighBandFrequency = Int32.Parse(txtLNBHigh.Text.Trim());
            satelliteFrequency.SatelliteDish.LNBSwitchFrequency = Int32.Parse(txtLNBSwitch.Text.Trim());
            satelliteFrequency.SatelliteDish.LNBType = LNBType.GetInstance(cboLNBType.Text);
            
            if (cboDiseqc.SelectedIndex != 0)
            {
                satelliteFrequency.DiseqcRunParamters.DiseqcSwitch = cboDiseqc.Text;

                if (cbUseSafeDiseqc.Checked)
                    satelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.UseSafeDiseqc));
                if (cbSwitchAfterPlay.Checked)
                    satelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.SwitchAfterPlay));
                if (cbSwitchAfterTune.Checked)
                    satelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.SwitchAfterTune));
                if (cbRepeatDiseqc.Checked)
                    satelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.RepeatDiseqc));

                if (DiseqcHandlerBase.IsGeneric(cboDiseqcHandler.Text))
                {
                    if (cbDisableDriverDiseqc.Checked)
                        satelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.DisableDriverDiseqc));
                    if (cbUseDiseqcCommands.Checked)
                        satelliteFrequency.DiseqcRunParamters.Options.Add(new OptionEntry(OptionName.UseDiseqcCommand));
                }

                satelliteFrequency.DiseqcRunParamters.DiseqcHandler = cboDiseqcHandler.Text;
            }

            if (udDvbsSatIpFrontend.SelectedIndex != 0)
                satelliteFrequency.SatIpFrontend = udDvbsSatIpFrontend.SelectedIndex;
            else
                satelliteFrequency.SatIpFrontend = -1;

            setCurrentFrequency(satelliteFrequency);
            return (satelliteFrequency);
        }

        private void getTuner(TuningFrequency tuningFrequency, CheckedListBox clbTuners)
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

        private void setCurrentFrequency(TuningFrequency tuningFrequency)
        {
            switch (tuningFrequency.TunerType)
            {
                case TunerType.Satellite:
                    currentSatelliteFrequency = tuningFrequency as SatelliteFrequency;
                    break;
                case TunerType.Terrestrial:
                    currentTerrestrialFrequency = tuningFrequency as TerrestrialFrequency;
                    break;
                case TunerType.Cable:
                    currentCableFrequency = tuningFrequency as CableFrequency;
                    break;
                case TunerType.ATSC:
                case TunerType.ATSCCable:
                    currentAtscFrequency = tuningFrequency as AtscFrequency;
                    break;
                case TunerType.ClearQAM:
                    currentClearQamFrequency = tuningFrequency as ClearQamFrequency;
                    break;
                default:
                    break;
            }
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
    }
}

