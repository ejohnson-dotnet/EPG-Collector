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
using System.Text;
using System.Windows.Forms;

using DomainObjects;

namespace EPGCentre
{
    internal partial class ChangeDvbsDetails : Form
    {        
        private SignalPolarization polarization { get { return(new SignalPolarization(cboPolarization.Text)); } }
        private FECRate fec { get { return(new FECRate(cboFec.Text)); } }
        private SignalModulation.Modulation modulation { get { return ((SignalModulation.Modulation)Enum.Parse(typeof(SignalModulation.Modulation), cboModulation.Text)); } }
        private SignalPilot.Pilot pilot { get { return ((SignalPilot.Pilot)Enum.Parse(typeof(SignalPilot.Pilot), cboPilot.Text.Replace(" ", ""))); } }
        private SignalRollOff.RollOff rollOff { get { return ((SignalRollOff.GetRollOff(cboRolloff.SelectedIndex))); } }
        
        private int frequency;
        private int symbolRate;
        
        private bool newSatellite;
        private bool newFrequency;

        private SatelliteFrequency originalFrequency;
        
        internal ChangeDvbsDetails()
        {
            InitializeComponent();
        }

        internal void Initialize(Satellite satellite, SatelliteFrequency frequency)
        {
            Initialize(satellite, frequency, true);
        }

        internal void Initialize(Satellite satellite, SatelliteFrequency frequency, bool canChangeFrequency)
        {
            originalFrequency = frequency;

            initializeLists();

            if (satellite == null)
            {
                tbSatellite.ReadOnly = false;
                tbSatellite.TabStop = true;

                newSatellite = true;
                newFrequency = true;
            }
            else
                tbSatellite.Text = satellite.Name;

            if (frequency == null)
            {
                newFrequency = true;
                return;
            }

            if (!canChangeFrequency)
            {
                tbFrequency.ReadOnly = true;
                tbFrequency.TabStop = false;
            }

            tbFrequency.Text = frequency.Frequency.ToString();
            tbSymbolRate.Text = frequency.SymbolRate.ToString(); 
            cboPolarization.SelectedIndex = SignalPolarization.GetIndex(frequency.Polarization);
            cboFec.SelectedIndex = FECRate.GetIndex(frequency.FEC);            
            cboModulation.SelectedIndex = SignalModulation.GetDvbsIndex(frequency.Modulation);            
            cboPilot.SelectedIndex = SignalPilot.GetIndex(frequency.Pilot);            
            cboRolloff.SelectedIndex = SignalRollOff.GetIndex(frequency.RollOff);            
        }

        private void initializeLists()
        {
            cboPolarization.DataSource = SignalPolarization.Polarizations;
            cboFec.DataSource = FECRate.FECRates;
            cboModulation.DataSource = SignalModulation.GetDvbsModulations();
            cboPilot.DataSource = SignalPilot.GetPilots();
            cboRolloff.DataSource = SignalRollOff.GetRollOffs();            
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            string[] satelliteParts = null;
            string degree = "°";
            decimal location = 0;

            if (newSatellite)
            {
                if (string.IsNullOrWhiteSpace(tbSatellite.Text))
                {
                    MessageBox.Show("No satellite entered.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                satelliteParts = tbSatellite.Text.Trim().Split(new char[] { ' ' });
                if (satelliteParts.Length < 2)
                {
                    MessageBox.Show("The satellite name is in the wrong format.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string eastWest = satelliteParts[0].Trim().Substring(satelliteParts[0].Trim().Length - 1, 1).ToUpperInvariant();
                if (eastWest != "E" && eastWest != "W")
                {
                    MessageBox.Show("The satellite name is in the wrong format.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    location = decimal.Parse(satelliteParts[0].Trim().Substring(0, satelliteParts[0].Trim().Length - 1));
                }
                catch (FormatException)
                {
                    MessageBox.Show("The satellite name is in the wrong format.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (ArithmeticException)
                {
                    MessageBox.Show("The satellite name is in the wrong format.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(tbFrequency.Text))
            {
                MessageBox.Show("No frequency entered.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            try
            {
                frequency = Int32.Parse(tbFrequency.Text.Trim());
            }
            catch (FormatException)
            {
                MessageBox.Show("The frequency is incorrect.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;                
            }
            catch (ArithmeticException)
            {
                MessageBox.Show("The frequency is incorrect.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbSymbolRate.Text))
            {
                MessageBox.Show("No symbol rate entered.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                symbolRate = Int32.Parse(tbSymbolRate.Text.Trim());
            }
            catch (FormatException)
            {
                MessageBox.Show("The symbol rate is incorrect.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Satellite satellite;

            if (!newSatellite)
            {
                satellite = Satellite.FindSatellite(tbSatellite.Text);
                if (satellite == null)
                {
                    MessageBox.Show("The satellite cannot be located.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                StringBuilder restName = new StringBuilder();

                for (int index = 1; index < satelliteParts.Length; index++)
                    restName.Append(" " + satelliteParts[index]);

                string realName = location.ToString("0.0" + 
                    degree + 
                    satelliteParts[0].Trim().Substring(satelliteParts[0].Length - 1, 1).ToUpperInvariant() +
                    restName);

                satellite = new Satellite(realName);
                Satellite.AddProvider(satellite);
            }

            if (!newFrequency)
            {
                SatelliteFrequency satelliteFrequency = satellite.FindFrequency(originalFrequency.Frequency) as SatelliteFrequency;
                if (satelliteFrequency == null)
                {
                    MessageBox.Show("The frequency cannot be located.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (originalFrequency.Frequency == frequency && originalFrequency.Polarization.Polarization == polarization.Polarization)
                    updateFrequency(satelliteFrequency);
                else
                {
                    satellite.Frequencies.Remove(originalFrequency);
                    SatelliteFrequency addFrequency = (SatelliteFrequency)originalFrequency.Clone();
                    addFrequency.Frequency = frequency;
                    updateFrequency(addFrequency);
                    satellite.AddFrequency(addFrequency);
                }
            }
            else
            {
                SatelliteFrequency satelliteFrequency = new SatelliteFrequency();
                satelliteFrequency.Frequency = frequency;
                updateFrequency(satelliteFrequency);
                satellite.AddFrequency(satelliteFrequency);                
            }
            
            Close();

            string reply = satellite.Unload();

            if (reply != null)
            {
                MessageBox.Show(reply + Environment.NewLine + Environment.NewLine + "See the EPG Collector log for details.",
                    " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Ignore;
            }
            else
            {
                MessageBox.Show("The DVB Satellite tuning parameters have been updated.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK; 
            }
        }

        private void updateFrequency(SatelliteFrequency frequency)
        {
            frequency.Polarization = polarization;
            frequency.SymbolRate = symbolRate;
            frequency.FEC = fec;
            frequency.Modulation = modulation;
            frequency.Pilot = pilot;
            frequency.RollOff = rollOff;           
        }
    }
}

