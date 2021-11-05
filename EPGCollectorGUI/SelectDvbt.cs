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
    internal partial class SelectDvbt : Form
    {
        /// <summary>
        /// Get the selected country.
        /// </summary>
        public string Country 
        { 
            get 
            {
                if (cboCountry.SelectedIndex == 0)
                    return (null);
                else
                    return (cboCountry.Text); 
            } 
        }

        /// <summary>
        /// Get the selected area.
        /// </summary>
        public string Area 
        { 
            get 
            {
                if (cboArea.SelectedIndex == 0)
                    return (null);
                else
                    return (cboArea.Text); 
            } 
        }

        /// <summary>
        /// Get the selected frequency.
        /// </summary>
        public TerrestrialFrequency Frequency { get { return (lbFrequency.SelectedItem as TerrestrialFrequency); } }

        internal SelectDvbt()
        {
            InitializeComponent();

            TerrestrialProvider.Load();

            foreach (TerrestrialProvider provider in TerrestrialProvider.Providers)
            {
                bool add = true;

                foreach(Country country in cboCountry.Items)
                {
                    if (country.Name == provider.Country.Name)
                        add = false;
                }

                if (add)
                    cboCountry.Items.Add(provider.Country);
            }

            cboCountry.Items.Insert(0, " -- New --");

            if (cboCountry.Items.Count != 0)
                cboCountry.SelectedIndex = 1;
            else
                cboCountry.SelectedIndex = 0;
        }

        private void cboCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboArea.Items.Clear();
            
            cboArea.Items.Add(" -- New --");
            cboArea.SelectedIndex = 0;

            if (cboCountry.SelectedIndex == 0)
                return;

            Country country = cboCountry.SelectedItem as Country;

            foreach (TerrestrialProvider provider in TerrestrialProvider.Providers)
            {
                if (provider.Country.Name == country.Name)
                    cboArea.Items.Add(provider.Area);
            }

            if (cboArea.Items.Count != 0)
                cboArea.SelectedIndex = 1;
            else
                cboArea.SelectedIndex = 0;
        }

        private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbFrequency.Items.Clear();

            lbFrequency.Items.Add(" -- New --");
            lbFrequency.SelectedIndex = 0;

            if (cboArea.SelectedIndex == 0)
                return;

            Country country = cboCountry.SelectedItem as Country;
            Area area = cboArea.SelectedItem as Area;

            TerrestrialProvider selectedProvider = null;

            foreach (TerrestrialProvider provider in TerrestrialProvider.Providers)
            {
                if (provider.Country.Name == country.Name && provider.Area.Name == area.Name)
                    selectedProvider = provider;
            }

            if (selectedProvider == null)
                return;

            foreach (TerrestrialFrequency frequency in selectedProvider.Frequencies)
                lbFrequency.Items.Add(frequency);
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

