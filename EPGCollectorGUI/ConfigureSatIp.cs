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
using SatIp;
using NetworkProtocols;

namespace EPGCentre
{
    /// <summary>
    /// The dialog for configuring Sat>IP.
    /// </summary>
    public partial class ConfigureSatIp : Form
    {
        /// <summary>
        /// Get the enabled option.
        /// </summary>
        public bool SatIpEnabled { get { return (cbEnable.Checked); } }        

        private string defaultLine = "-- Default --";
        private string dynamicLine = "-- Dynamic --";

        private SatIpConfiguration configuration;
        
        /// <summary>
        /// Initialize a new instance of the ConfigureSatIp class.
        /// </summary>
        public ConfigureSatIp(SatIpConfiguration currentConfiguration)
        {
            InitializeComponent();

            configuration = currentConfiguration;

            cbEnable.Checked = SatIpConfiguration.SatIpEnabled;
            gpSatIp.Enabled = cbEnable.Checked;

            Collection<IPAddress> localAddresses = Utils.GetLocalIpAddresses();
            cboLocalAddress.Items.Add(defaultLine);

            foreach (IPAddress address in localAddresses)
                cboLocalAddress.Items.Add(address.ToString());

            if (cboLocalAddress.Items.Count != 0)
                cboLocalAddress.SelectedItem = cboLocalAddress.Items[0];

            cboFrontend.Items.Add(dynamicLine);
            for (int index = 0; index < 7; index++)
                cboFrontend.Items.Add((index + 1).ToString());
            cboFrontend.SelectedItem = cboFrontend.Items[0];

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
                    
                cboFrontend.SelectedIndex = configuration.Frontend;                

                if (configuration.RtspPort != 554)
                    tbRtspPort.Text = configuration.RtspPort.ToString();

                cbNoAnnounce.Checked = configuration.NoAnnouncements;
                cbNoSendRtspPort.Checked = configuration.NoSendRtspPort;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (!cbEnable.Checked)
            {
                if (File.Exists(SatIpConfiguration.SatIpEnabledFile))
                    File.Delete(SatIpConfiguration.SatIpEnabledFile);

                configuration.LocalAddress = null;
                configuration.Frontend = 0;
                configuration.NoAnnouncements = false;
                configuration.NoSendRtspPort = false;

                Logger.Instance.Write("Sat>IP servers disabled");
            }
            else
            {
                int rtspPort = 554;

                if (!string.IsNullOrWhiteSpace(tbRtspPort.Text))
                {
                    try
                    {
                        rtspPort = Int32.Parse(tbRtspPort.Text.Trim());
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("The RTSP port is incorrect (valid values are 1-65535).");
                        return;
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("The RTSP port is incorrect (valid values are 1-65535).");
                        return;
                    }
                }

                configuration.LocalAddress =  cboLocalAddress.Text == defaultLine ? null : cboLocalAddress.Text.Trim();
                configuration.Frontend = cboFrontend.SelectedIndex;
                configuration.NoAnnouncements = cbNoAnnounce.Checked;
                configuration.RtspPort = rtspPort;
                configuration.NoSendRtspPort = cbNoSendRtspPort.Checked;

                configuration.Unload();
                Logger.Instance.Write("Sat>IP servers enabled");
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

