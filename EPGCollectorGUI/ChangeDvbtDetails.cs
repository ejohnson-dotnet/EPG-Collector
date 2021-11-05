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

namespace EPGCentre
{
    internal partial class ChangeDvbtDetails : Form
    {        
        private int frequency;
        private int bandwidth;
        private int plpNumber = -1;
        
        private bool newCountry;
        private bool newArea;
        private bool newFrequency;

        private TerrestrialFrequency originalFrequency;

        internal ChangeDvbtDetails()
        {
            InitializeComponent();
        }

        internal void Initialize(string country, string area, TerrestrialFrequency frequency)
        {
            Initialize(country, area, frequency, true);
        }

        internal void Initialize(string country, string area, TerrestrialFrequency frequency, bool canChangeFrequency)
        {
            originalFrequency = frequency;

            if (country == null)
            {
                tbCountry.ReadOnly = false;
                tbCountry.TabStop = true;

                tbArea.ReadOnly = false;
                tbArea.TabStop = true;

                newCountry = true;
                newArea = true;
                newFrequency = true;
            }
            else
                tbCountry.Text = country;

            if (area == null)
            {
                tbArea.ReadOnly = false;
                tbArea.TabStop = true;

                newArea = true;
                newFrequency = true;
            }
            else
                tbArea.Text = area;

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
            tbBandwidth.Text = frequency.Bandwidth.ToString();

            if (frequency.PlpNumber != -1)
                tbPlpNumber.Text = frequency.PlpNumber.ToString();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (newCountry)
            {
                if (string.IsNullOrWhiteSpace(tbCountry.Text))
                {
                    MessageBox.Show("No country entered.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }                
            }

            if (newArea)
            {
                if (string.IsNullOrWhiteSpace(tbArea.Text))
                {
                    MessageBox.Show("No area entered.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (string.IsNullOrWhiteSpace(tbBandwidth.Text))
            {
                MessageBox.Show("No bandwidth entered.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                bandwidth = Int32.Parse(tbBandwidth.Text.Trim());
            }
            catch (FormatException)
            {
                MessageBox.Show("The bandwidth is incorrect.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ArithmeticException)
            {
                MessageBox.Show("The bandwidth is incorrect.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(tbPlpNumber.Text))
            {
                try
                {
                    plpNumber = Int32.Parse(tbPlpNumber.Text.Trim());
                }
                catch (FormatException)
                {
                    MessageBox.Show("The PLP id is incorrect.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (ArithmeticException)
                {
                    MessageBox.Show("The PLP id is incorrect.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            TerrestrialProvider provider;

            if (!newCountry && !newArea)
            {
                provider = TerrestrialProvider.FindProvider(tbCountry.Text, tbArea.Text);
                if (provider == null)
                {
                    MessageBox.Show("The provider cannot be located.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                provider = new TerrestrialProvider(tbCountry.Text + "." + tbArea.Text);
                TerrestrialProvider.AddProvider(provider);
            }

            if (!newFrequency)
            {
                TerrestrialFrequency terrestrialFrequency = provider.FindFrequency(originalFrequency.Frequency) as TerrestrialFrequency;
                if (terrestrialFrequency == null)
                {
                    MessageBox.Show("The frequency cannot be located.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (originalFrequency.Frequency == frequency)
                    updateFrequency(terrestrialFrequency);
                else
                {
                    provider.Frequencies.Remove(originalFrequency);
                    TerrestrialFrequency addFrequency = (TerrestrialFrequency)originalFrequency.Clone();
                    addFrequency.Frequency = frequency;
                    updateFrequency(addFrequency);
                    provider.AddFrequency(addFrequency);
                }
            }
            else
            {
                TerrestrialFrequency terrestrialFrequency = new TerrestrialFrequency();
                terrestrialFrequency.Frequency = frequency;
                updateFrequency(terrestrialFrequency);
                provider.AddFrequency(terrestrialFrequency);                
            }
            
            Close();

            string reply = provider.Unload();

            if (reply != null)
            {
                MessageBox.Show(reply + Environment.NewLine + Environment.NewLine + "See the EPG Collector log for details.",
                    " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Ignore;
            }
            else
            {
                MessageBox.Show("The DVB Terrestrial tuning parameters have been updated.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK; 
            }
        }

        private void updateFrequency(TerrestrialFrequency frequency)
        {
            frequency.Bandwidth = bandwidth;
            frequency.PlpNumber = plpNumber;          
        }
    }
}

