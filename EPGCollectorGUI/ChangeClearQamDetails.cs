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
    internal partial class ChangeClearQamDetails : Form
    {        
        private FECRate fec { get { return (new FECRate(cboFec.Text)); } }
        private SignalModulation.Modulation modulation { get { return ((SignalModulation.Modulation)Enum.Parse(typeof(SignalModulation.Modulation), cboModulation.Text)); } }
        
        private int channelNumber;
        private int frequency;
        private int symbolRate;
        
        private bool newProvider;
        private bool newFrequency;

        private ClearQamFrequency originalFrequency;
        
        internal ChangeClearQamDetails()
        {
            InitializeComponent();
        }

        internal void Initialize(ClearQamProvider provider, ClearQamFrequency frequency)
        {
            Initialize(provider, frequency, true);
        }

        internal void Initialize(ClearQamProvider provider, ClearQamFrequency frequency, bool canChangeFrequency)
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
                tbChannelNumber.ReadOnly = true;
                tbChannelNumber.TabStop = false;

                tbFrequency.ReadOnly = true;
                tbFrequency.TabStop = false;
            }

            tbChannelNumber.Text = frequency.ChannelNumber.ToString();
            tbFrequency.Text = frequency.Frequency.ToString();
            tbSymbolRate.Text = frequency.SymbolRate.ToString();
            cboFec.SelectedIndex = FECRate.GetIndex(frequency.FEC);  
            cboModulation.SelectedIndex = SignalModulation.GetClearQamIndex(frequency.Modulation);
        }

        private void initializeLists()
        {
            cboFec.DataSource = FECRate.FECRates;
            cboModulation.DataSource = SignalModulation.GetClearQamModulations();
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

            if (string.IsNullOrWhiteSpace(tbChannelNumber.Text))
            {
                MessageBox.Show("No channel number entered.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                channelNumber = Int32.Parse(tbChannelNumber.Text.Trim());
            }
            catch (FormatException)
            {
                MessageBox.Show("The channel number is incorrect.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ArithmeticException)
            {
                MessageBox.Show("The channel number is incorrect.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

            ClearQamProvider provider;

            if (!newProvider)
            {
                provider = ClearQamProvider.FindProvider(tbProvider.Text.Trim());
                if (provider == null)
                {
                    MessageBox.Show("The provider cannot be located.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                provider = new ClearQamProvider(tbProvider.Text.Trim());
                ClearQamProvider.AddProvider(provider);
            }

            if (!newFrequency)
            {
                ClearQamFrequency clearQamFrequency = provider.FindFrequency(originalFrequency.Frequency) as ClearQamFrequency;
                if (clearQamFrequency == null)
                {
                    MessageBox.Show("The frequency cannot be located.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (originalFrequency.Frequency == frequency)
                    updateFrequency(clearQamFrequency);
                else
                {
                    provider.Frequencies.Remove(originalFrequency);
                    ClearQamFrequency addFrequency = (ClearQamFrequency)originalFrequency.Clone();
                    addFrequency.Frequency = frequency;
                    updateFrequency(addFrequency);
                    provider.AddFrequency(addFrequency);
                }
            }
            else
            {
                ClearQamFrequency clearQamFrequency = new ClearQamFrequency();
                clearQamFrequency.Frequency = frequency;
                updateFrequency(clearQamFrequency);
                provider.AddFrequency(clearQamFrequency);                
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
                MessageBox.Show("The ClearQAM tuning parameters have been updated.", " EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK; 
            }
        }

        private void updateFrequency(ClearQamFrequency frequency)
        {
            frequency.ChannelNumber = channelNumber;            
            frequency.FEC = fec;
            frequency.Modulation = modulation;
            frequency.SymbolRate = symbolRate;          
        }
    }
}

