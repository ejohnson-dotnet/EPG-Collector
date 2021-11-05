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
    internal partial class SelectIsdbs : Form
    {
        /// <summary>
        /// Get the selected satellite.
        /// </summary>
        public ISDBSatelliteProvider Satellite { get { return (cboSatellite.SelectedItem as ISDBSatelliteProvider); } }

        /// <summary>
        /// Get the selected frequency.
        /// </summary>
        public ISDBSatelliteFrequency Frequency { get { return (lbFrequency.SelectedItem as ISDBSatelliteFrequency); } }

        internal SelectIsdbs()
        {
            InitializeComponent();

            ISDBSatelliteProvider.Load();

            cboSatellite.Items.Add(" -- New --");

            foreach (ISDBSatelliteProvider satellite in ISDBSatelliteProvider.Providers)
                cboSatellite.Items.Add(satellite);

            if (cboSatellite.Items.Count != 0)
                cboSatellite.SelectedIndex = 1;
            else
                cboSatellite.SelectedIndex = 0;
        }

        private void cboSatellite_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbFrequency.Items.Clear();
            
            lbFrequency.Items.Add(" -- New --");
            lbFrequency.SelectedIndex = 0;

            if (cboSatellite.SelectedIndex == 0)
                return;

            foreach (ISDBSatelliteFrequency frequency in ((ISDBSatelliteProvider)cboSatellite.SelectedItem).Frequencies)
                lbFrequency.Items.Add(frequency);
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

