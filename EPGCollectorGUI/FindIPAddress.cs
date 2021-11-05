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
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

using DomainObjects;

namespace EPGCentre
{
    internal partial class FindIPAddress : Form
    {
        internal IPAddress SelectedAddress { get { return(((NetworkSpec)lvNames.SelectedItems[0].Tag).Address); } }

        public FindIPAddress()
        {
            InitializeComponent();

            Collection<NetworkSpec> networkSpecs = StreamFrequency.GetNetworkComputers("192.168.0.40");

            foreach (NetworkSpec networkSpec in networkSpecs)
            {
                try
                {
                    if (networkSpec.Address != null)
                    {
                        ListViewItem item = new ListViewItem(networkSpec.Name == null ? "Name not available" : networkSpec.Name);
                        item.Tag = networkSpec;

                        ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();
                        subItem.Text = networkSpec.Address.ToString();

                        item.SubItems.Add(subItem);

                        lvNames.Items.Add(item);
                    }
                }
                catch (SocketException e) 
                {
                    string message = e.Message;
                }
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (lvNames.SelectedItems.Count == 0)
            {
                MessageBox.Show("No address selected", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Close();
            DialogResult = DialogResult.OK;
        }
    }
}

