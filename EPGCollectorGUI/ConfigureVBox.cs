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
using System.Net;
using System.IO;
using System.Collections.ObjectModel;

using DomainObjects;
using VBox;
using NetworkProtocols;

namespace EPGCentre
{
    /// <summary>
    /// The dialog for configuring Sat>IP.
    /// </summary>
    public partial class ConfigureVBox : Form
    {
        /// <summary>
        /// Get the enabled option.
        /// </summary>
        public bool VBoxEnabled { get { return (cbEnable.Checked); } }        

        private VBoxConfiguration configuration;

        private string defaultLine = "-- Default --";
        
        /// <summary>
        /// Initialize a new instance of the ConfigureSatIp class.
        /// </summary>
        public ConfigureVBox(VBoxConfiguration currentConfiguration)
        {
            InitializeComponent();

            configuration = currentConfiguration;

            cbEnable.Checked = VBoxConfiguration.VBoxEnabled;
            gpSatIp.Enabled = cbEnable.Checked;

            Collection<IPAddress> localAddresses = Utils.GetLocalIpAddresses();
            cboLocalAddress.Items.Add(defaultLine);

            foreach (IPAddress address in localAddresses)
                cboLocalAddress.Items.Add(address.ToString());

            if (cboLocalAddress.Items.Count != 0)
                cboLocalAddress.SelectedItem = cboLocalAddress.Items[0];

            if (cbEnable.Checked)
            {
                if (configuration.LocalAddress != null)
                {
                    foreach (string listAddress in cboLocalAddress.Items)
                    {
                        if (listAddress == configuration.LocalAddress)
                            cboLocalAddress.SelectedItem = listAddress;                        
                    }
                }              
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (!cbEnable.Checked)
            {
                if (File.Exists(VBoxConfiguration.VBoxEnabledFile))
                    File.Delete(VBoxConfiguration.VBoxEnabledFile);

                configuration.LocalAddress = null;                

                Logger.Instance.Write("VBox servers disabled");
            }
            else
            {
                configuration.LocalAddress =  cboLocalAddress.Text == defaultLine ? null : cboLocalAddress.Text.Trim();
                
                configuration.Unload();
                Logger.Instance.Write("VBox servers enabled");
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cbEnable_CheckedChanged(object sender, EventArgs e)
        {
            gpSatIp.Enabled = cbEnable.Checked;
        }
    }
}

