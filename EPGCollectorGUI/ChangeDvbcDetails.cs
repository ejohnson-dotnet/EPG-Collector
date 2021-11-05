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
    internal partial class ChangeDvbcDetails : Form
    {        
        private SignalModulation.Modulation modulation { get { return ((SignalModulation.Modulation)Enum.Parse(typeof(SignalModulation.Modulation), cboModulation.Text)); } }
        
        private int frequency;
        private int symbolRate;
        
        private bool newProvider;
        private bool newFrequency;

        private CableFrequency originalFrequency;
        
        internal ChangeDvbcDetails()
        {
            InitializeComponent();
        }

        internal void Initialize(CableProvider provider, CableFrequency frequency)
        {
            Initialize(provider, frequency, true);
        }

        internal void Initialize(CableProvider provider, CableFrequency frequency, bool canChangeFrequency)
        {
            originalFrequency = frequency;

            initializeLists();

            if (provider == null)
            {
                tbProvider.ReadOnly = false;
                tbProvider.TabStop = true;

                newProvider = true;
                newFrequency = true;
            }
            else
                tbProvider.Text = provider.Name;

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
            cboModulation.SelectedIndex = SignalModulation.GetDvbcIndex(frequency.Modulation);
        }

        private void initializeLists()
        {
            cboModulation.DataSource = SignalModulation.GetDvbcModulations();            
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (newProvider)
            {
                if (string.IsNullOrWhiteSpace(tbProvider.Text))
                {
                    MessageBox.Show("No provider entered.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            CableProvider provider;

            if (!newProvider)
            {
                provider = CableProvider.FindProvider(tbProvider.Text.Trim());
                if (provider == null)
                {
                    MessageBox.Show("The provider cannot be located.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                provider = new CableProvider(tbProvider.Text.Trim());
                CableProvider.AddProvider(provider);
            }

            if (!newFrequency)
            {
                CableFrequency cableFrequency = provider.FindFrequency(originalFrequency.Frequency) as CableFrequency;
                if (cableFrequency == null)
                {
                    MessageBox.Show("The frequency cannot be located.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (originalFrequency.Frequency == frequency)
                    updateFrequency(cableFrequency);
                else
                {
                    provider.Frequencies.Remove(originalFrequency);
                    CableFrequency addFrequency = (CableFrequency)originalFrequency.Clone();
                    addFrequency.Frequency = frequency;
                    updateFrequency(addFrequency);
                    provider.AddFrequency(addFrequency);
                }
            }
            else
            {
                CableFrequency cableFrequency = new CableFrequency();
                cableFrequency.Frequency = frequency;
                updateFrequency(cableFrequency);
                provider.AddFrequency(cableFrequency);                
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
                MessageBox.Show("The DVB Cable tuning parameters have been updated.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK; 
            }
        }

        private void updateFrequency(CableFrequency frequency)
        {
            frequency.SymbolRate = symbolRate;
            frequency.Modulation = modulation;
        }
    }
}

