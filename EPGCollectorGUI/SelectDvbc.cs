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
    internal partial class SelectDvbc : Form
    {
        /// <summary>
        /// Get the selected provide.
        /// </summary>
        public CableProvider Provider 
        { 
            get 
            {
                if (cboProvider.SelectedIndex == 0)
                    return (null);
                else
                    return (cboProvider.SelectedItem as CableProvider); 
            } 
        }

        /// <summary>
        /// Get the selected frequency.
        /// </summary>
        public CableFrequency Frequency { get { return (lbFrequency.SelectedItem as CableFrequency); } }

        internal SelectDvbc()
        {
            InitializeComponent();

            CableProvider.Load();

            cboProvider.Items.Add(" -- New --");

            foreach (CableProvider provider in CableProvider.Providers)
                cboProvider.Items.Add(provider);

            if (cboProvider.Items.Count != 0)
                cboProvider.SelectedIndex = 1;
            else
                cboProvider.SelectedIndex = 0;
        }

        private void cboProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbFrequency.Items.Clear();
            
            lbFrequency.Items.Add(" -- New --");
            lbFrequency.SelectedIndex = 0;

            if (cboProvider.SelectedIndex == 0)
                return;

            foreach (CableFrequency frequency in ((CableProvider)cboProvider.SelectedItem).Frequencies)
                lbFrequency.Items.Add(frequency);
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

