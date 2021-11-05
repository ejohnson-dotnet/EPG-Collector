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

using DomainObjects;
using SchedulesDirect;

namespace EPGCentre
{
    /// <summary>
    /// The class that changes Schedules Direct lineups.
    /// </summary>
    public partial class ChangeSdLineups : Form
    {
        private Collection<SchedulesDirectLineup> lineupAdditions = new Collection<SchedulesDirectLineup>();
        private Collection<SchedulesDirectLineup> lineupDeletions = new Collection<SchedulesDirectLineup>();

        private Collection<SchedulesDirectSatellite> satelliteAdditions = new Collection<SchedulesDirectSatellite>();
        private Collection<SchedulesDirectSatellite> satelliteDeletions = new Collection<SchedulesDirectSatellite>();

        private Collection<SchedulesDirectTransmitter> transmitterAdditions = new Collection<SchedulesDirectTransmitter>();
        private Collection<SchedulesDirectTransmitter> transmitterDeletions = new Collection<SchedulesDirectTransmitter>();

        /// <summary>
        /// Initialize a new instance of the ChangeSdLineups class.
        /// </summary>
        public ChangeSdLineups()
        {
            InitializeComponent();

            cboMethod.SelectedIndex = 0;  
        }

        /// <summary>
        /// Initialize the dialog.
        /// </summary>
        /// <returns>ReplyBase instance.</returns>
        public ReplyBase Initialize()
        {
            string configReply = SchedulesDirectConfig.Instance.Load();
            if (configReply != null)
                return ReplyBase.ErrorReply(configReply);

            if (!string.IsNullOrWhiteSpace(SchedulesDirectConfig.Instance.UserName))
            {
                tbSdUserName.Text = SchedulesDirectConfig.Instance.UserName;
                tbSdPassword.Text = SchedulesDirectConfig.Instance.Password;
                
                Cursor.Current = Cursors.WaitCursor;
                ReplyBase reply = SchedulesDirectController.Instance.Initialize(SchedulesDirectConfig.Instance.UserName, SchedulesDirectConfig.Instance.Password);
                Cursor.Current = Cursors.Default;
                if (reply.Message != null)
                    return reply;

                cboMethod_SelectedIndexChanged(null, null);
                getLineups();
            }
            else
            {
                btSdVerify.Enabled = false;
                gpLineups.Enabled = false;
            }

            return ReplyBase.NoReply();
        }            

        private void tbSdUserName_TextChanged(object sender, EventArgs e)
        {
            btSdVerify.Enabled = tbSdUserName.Text.Trim().Length != 0 && tbSdPassword.Text.Trim().Length != 0;
        }

        private void tbSdPassword_TextChanged(object sender, EventArgs e)
        {
            btSdVerify.Enabled = tbSdUserName.Text.Trim().Length != 0 && tbSdPassword.Text.Trim().Length != 0;
        }

        private void btSdVerify_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ReplyBase reply = SchedulesDirectController.Instance.Initialize(tbSdUserName.Text, tbSdPassword.Text);
            Cursor.Current = Cursors.Default;
            if (reply.Message != null)
            {
                MessageBox.Show("Failed to initialize the Schedules Direct service." + Environment.NewLine + Environment.NewLine +
                    reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SchedulesDirectConfig.Instance.UserName = tbSdUserName.Text.Trim();
            SchedulesDirectConfig.Instance.Password = tbSdPassword.Text.Trim();

            getLineups();
            gpLineups.Enabled = true;

            cboMethod.SelectedIndex = 0;            
        }

        private void getLineups()
        {
            ReplyBase reply = SchedulesDirectController.Instance.GetLineups();
            if (reply.Message != null)
            {
                MessageBox.Show("Failed to load users lineups." + Environment.NewLine + Environment.NewLine +
                    reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            lvSelectedLineups.Items.Clear();

            foreach (SchedulesDirectLineup lineup in reply.ResponseData as Collection<SchedulesDirectLineup>)
            {
                ListViewItem item = new ListViewItem(lineup.FullName);
                item.SubItems.Add(!string.IsNullOrWhiteSpace(lineup.Location) ? lineup.Location : lineup.Identity);
                item.SubItems.Add(lineup.Transport);
                item.Tag = lineup;

                lvSelectedLineups.Items.Add(item);
            }
        }

        private void cboMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!SchedulesDirectController.Instance.IsInitialized)
                return;

            ReplyBase reply;

            switch (cboMethod.SelectedIndex)
            {
                case 0:
                    if (cboCountry.Items.Count == 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        reply = SchedulesDirectController.Instance.LoadCountries();
                        Cursor.Current = Cursors.Default;

                        if (reply.Message != null)
                        {
                            MessageBox.Show("Failed to load country information." + Environment.NewLine + Environment.NewLine +
                                reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        foreach (SchedulesDirectCountry country in reply.ResponseData as Collection<SchedulesDirectCountry>)
                            cboCountry.Items.Add(country);
                    }

                    if (cboCountry.Items.Count != 0)
                        cboCountry.SelectedIndex = 0;

                    cboCountry.Enabled = true;
                    lblMethod.Text = "Zip code/postcode";
                    tbZipPostCode.Visible = true;
                    cboSatelliteTransmitter.Visible = false;
                    btRefresh.Enabled = true;
                    break;
                case 1:
                    cboCountry.Enabled = false;
                    lblMethod.Text = "Satellites";
                    tbZipPostCode.Visible = false;
                    cboSatelliteTransmitter.Visible = true;
                    btRefresh.Enabled = false;

                    cboSatelliteTransmitter.Items.Clear();

                    Cursor.Current = Cursors.WaitCursor;
                    reply = SchedulesDirectController.Instance.LoadSatellites();
                    Cursor.Current = Cursors.Default;
                    
                    if (reply.Message != null)
                    {
                        MessageBox.Show("Failed to load satellite information." + Environment.NewLine + Environment.NewLine +
                            reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    foreach (SchedulesDirectSatellite satellite in reply.ResponseData as Collection<SchedulesDirectSatellite>)
                        cboSatelliteTransmitter.Items.Add(satellite);

                    if (cboSatelliteTransmitter.Items.Count != 0)
                        cboSatelliteTransmitter.SelectedIndex = 0;
                    else
                        MessageBox.Show("No satellites available.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                case 2:
                    cboCountry.Enabled = false;
                    lblMethod.Text = "Transmitters";
                    tbZipPostCode.Visible = false;
                    cboSatelliteTransmitter.Visible = true;
                    btRefresh.Enabled = false;

                    cboSatelliteTransmitter.Items.Clear();

                    Cursor.Current = Cursors.WaitCursor;
                    reply = SchedulesDirectController.Instance.LoadTransmitters("GBR");
                    Cursor.Current = Cursors.Default;

                    if (reply.Message != null)
                    {
                        MessageBox.Show("Failed to load transmitter information." + Environment.NewLine + Environment.NewLine +
                            reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    foreach (SchedulesDirectTransmitter transmitter in reply.ResponseData as Collection<SchedulesDirectTransmitter>)
                        cboSatelliteTransmitter.Items.Add(transmitter);

                    if (cboSatelliteTransmitter.Items.Count != 0)
                        cboSatelliteTransmitter.SelectedIndex = 0;
                    else
                        MessageBox.Show("No transmitters available.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    break;
                default:
                    MessageBox.Show("Method " + cboMethod.SelectedIndex + " not implemented", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
        }

        private void cboSatelliteTransmitter_SelectedIndexChanged(object sender, EventArgs e)
        {
            tvLineups.Nodes.Clear();

            SchedulesDirectSatellite satellite = cboSatelliteTransmitter.SelectedItem as SchedulesDirectSatellite;
            if (satellite != null)
            {
                TreeNode node = new TreeNode(satellite.ToString());
                node.Tag = satellite;
                node.Nodes.Add(new TreeNode("Dummy"));

                tvLineups.Nodes.Add(node);
            }
            else
            {
                SchedulesDirectTransmitter transmitter = cboSatelliteTransmitter.SelectedItem as SchedulesDirectTransmitter;
                if (transmitter != null)
                {
                    TreeNode node = new TreeNode(transmitter.ToString());
                    node.Tag = transmitter;
                    node.Nodes.Add(new TreeNode("Dummy"));

                    tvLineups.Nodes.Add(node);
                }
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {            
            switch (cboMethod.SelectedIndex)
            {
                case 0:
                    Cursor.Current = Cursors.WaitCursor;
                    ReplyBase reply = SchedulesDirectController.Instance.LoadLineups((cboCountry.SelectedItem as SchedulesDirectCountry).ShortName, tbZipPostCode.Text);
                    Cursor.Current = Cursors.Default;

                    if (reply.Message != null)
                    {
                        MessageBox.Show("Failed to load lineup information." + Environment.NewLine + Environment.NewLine +
                            reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    tvLineups.Nodes.Clear();

                    Collection<SchedulesDirectLineup> sortedLineups = new Collection<SchedulesDirectLineup>();

                    foreach (SchedulesDirectLineup lineup in reply.ResponseData as Collection<SchedulesDirectLineup>)
                        addToSortedLineups(sortedLineups, lineup);

                    foreach (SchedulesDirectLineup lineup in sortedLineups)
                    {
                        if (!lineup.IsDeleted)
                        {
                            TreeNode newNode = new TreeNode(lineup.FullName);
                            newNode.Tag = lineup;
                            newNode.Nodes.Add(new TreeNode("Dummy"));
                            tvLineups.Nodes.Add(newNode);
                        }
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

        private void addToSortedLineups(Collection<SchedulesDirectLineup> sortedLineups, SchedulesDirectLineup newLineup)
        {
            foreach (SchedulesDirectLineup oldLineup in sortedLineups)
            {
                if (oldLineup.FullName.CompareTo(newLineup.FullName) > 0)
                {
                    sortedLineups.Insert(sortedLineups.IndexOf(oldLineup), newLineup);
                    return;
                }
            }

            sortedLineups.Add(newLineup);
        }

        private void tvLineups_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count != 1 || e.Node.Nodes[0].Text != "Dummy")
                return;

            string identity = "?";

            SchedulesDirectLineup lineup = e.Node.Tag as SchedulesDirectLineup;
            if (lineup != null)
                identity = lineup.Identity;
            else
            {
                SchedulesDirectSatellite satellite = e.Node.Tag as SchedulesDirectSatellite;
                if (satellite != null)
                    identity = satellite.Name;
                else
                {
                    SchedulesDirectTransmitter transmitter = e.Node.Tag as SchedulesDirectTransmitter;
                    if (transmitter != null)
                        identity = transmitter.Identity;
                }
            }
            
            Cursor.Current = Cursors.WaitCursor;
            ReplyBase reply = SchedulesDirectController.Instance.LoadPreview(identity);
            Cursor.Current = Cursors.Default;

            if (reply.Message != null)
            {
                MessageBox.Show("Failed to load channel information." + Environment.NewLine + Environment.NewLine +
                    reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }

            e.Node.Nodes.Clear();

            Collection<SchedulesDirectChannel> sortedChannels = new Collection<SchedulesDirectChannel>();

            foreach (SchedulesDirectChannel channel in reply.ResponseData as Collection<SchedulesDirectChannel>)
                addToSortedChannels(sortedChannels, channel);

            foreach (SchedulesDirectChannel channel in sortedChannels)
            {
                TreeNode channelNode = new TreeNode(channel.FullName);
                e.Node.Nodes.Add(channelNode);
            }  
        }

        private void addToSortedChannels(Collection<SchedulesDirectChannel> sortedChannels, SchedulesDirectChannel newChannel)
        {
            foreach (SchedulesDirectChannel oldChannel in sortedChannels)
            {
                if (oldChannel.FullName.CompareTo(newChannel.FullName) > 0)
                {
                    sortedChannels.Insert(sortedChannels.IndexOf(oldChannel), newChannel);
                    return;
                }
            }

            sortedChannels.Add(newChannel);
        }

        private void tvLineups_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btAdd.Enabled = true;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            SchedulesDirectLineup selectedLineup = tvLineups.SelectedNode.Tag as SchedulesDirectLineup;
            if (selectedLineup != null)
                addLineup(selectedLineup);
            else
            {
                SchedulesDirectSatellite selectedSatellite = tvLineups.SelectedNode.Tag as SchedulesDirectSatellite;
                if (selectedSatellite != null)
                    addSatellite(selectedSatellite);
                else
                {
                    SchedulesDirectTransmitter selectedTransmitter = tvLineups.SelectedNode.Tag as SchedulesDirectTransmitter;
                    if (selectedTransmitter != null)
                        addTransmitter(selectedTransmitter);
                }
            }
        }

        private void addLineup(SchedulesDirectLineup selectedLineup)
        {
            foreach (ListViewItem item in lvSelectedLineups.Items)
            {
                SchedulesDirectLineup lineup = item.Tag as SchedulesDirectLineup;

                if (lineup != null && lineup.Identity == selectedLineup.Identity)
                {
                    MessageBox.Show("The lineup has already been selected.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }                
            }

            if (lvSelectedLineups.Items.Count == 4)
            {
                MessageBox.Show("The Schedules Direct service only allows a maximum of 4 lineups.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lineupAdditions.Add(selectedLineup);
            lineupDeletions.Remove(selectedLineup);

            ListViewItem newItem = new ListViewItem(selectedLineup.Name);
            newItem.Tag = selectedLineup;
            newItem.SubItems.Add(!string.IsNullOrWhiteSpace(selectedLineup.Location) ? selectedLineup.Location : selectedLineup.Identity);
            newItem.SubItems.Add(selectedLineup.Transport);

            lvSelectedLineups.Items.Add(newItem);
        }

        private void addSatellite(SchedulesDirectSatellite selectedSatellite)
        {
            foreach (ListViewItem item in lvSelectedLineups.Items)
            {
                SchedulesDirectSatellite satellite = item.Tag as SchedulesDirectSatellite;

                if (satellite != null && satellite.Name == selectedSatellite.Name)
                {
                    MessageBox.Show("The satellite has already been selected.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (lvSelectedLineups.Items.Count == 4)
            {
                MessageBox.Show("The Schedules Direct service only allows a maximum of 4 lineups.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            satelliteAdditions.Add(selectedSatellite);
            satelliteDeletions.Remove(selectedSatellite);

            ListViewItem newItem = new ListViewItem(selectedSatellite.ToString());
            newItem.Tag = selectedSatellite;
            newItem.SubItems.Add("");
            newItem.SubItems.Add("DVB-S");

            lvSelectedLineups.Items.Add(newItem);
        }

        private void addTransmitter(SchedulesDirectTransmitter selectedTransmitter)
        {
            foreach (ListViewItem item in lvSelectedLineups.Items)
            {
                SchedulesDirectTransmitter transmitter = item.Tag as SchedulesDirectTransmitter;

                if (transmitter != null && transmitter.Identity == selectedTransmitter.Identity)
                {
                    MessageBox.Show("The transmitter has already been selected.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (lvSelectedLineups.Items.Count == 4)
            {
                MessageBox.Show("The Schedules Direct service only allows a maximum of 4 lineups.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            transmitterAdditions.Add(selectedTransmitter);
            transmitterDeletions.Remove(selectedTransmitter);

            ListViewItem newItem = new ListViewItem(selectedTransmitter.Name);
            newItem.Tag = selectedTransmitter;
            newItem.SubItems.Add("");
            newItem.SubItems.Add("DVB-T");

            lvSelectedLineups.Items.Add(newItem);
        }

        private void lvSelectedLineups_SelectedIndexChanged(object sender, EventArgs e)
        {
            btDelete.Enabled = lvSelectedLineups.SelectedItems.Count > 0;
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            string lineupList = string.Empty;

            foreach (ListViewItem item in lvSelectedLineups.SelectedItems)
            {
                SchedulesDirectLineup lineup = item.Tag as SchedulesDirectLineup;
                if (lineup != null)
                {
                    if (lineupList.Length != 0)
                        lineupList += ", ";
                    lineupList += lineup.FullName;
                }
                else
                {
                    SchedulesDirectSatellite satellite = item.Tag as SchedulesDirectSatellite;
                    if (satellite != null)
                    {
                        if (lineupList.Length != 0)
                            lineupList += ", ";
                        lineupList += satellite.ToString();
                    }
                    else
                    {
                        SchedulesDirectTransmitter transmitter = item.Tag as SchedulesDirectTransmitter;
                        if (transmitter != null)
                        {
                            if (lineupList.Length != 0)
                                lineupList += ", ";
                            lineupList += transmitter.Name;
                        }
                    }
                }
            }

            DialogResult result = MessageBox.Show("Please confirm that the following lineups are to be deleted:" + Environment.NewLine + Environment.NewLine +
                lineupList, "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            foreach (ListViewItem item in lvSelectedLineups.SelectedItems)
            {
                SchedulesDirectLineup lineup = item.Tag as SchedulesDirectLineup;
                if (lineup != null)
                {
                    lineupDeletions.Add(lineup);
                    lineupAdditions.Remove(lineup);
                }
                else
                {
                    SchedulesDirectSatellite satellite = item.Tag as SchedulesDirectSatellite;
                    if (lineup != null)
                    {
                        lineupDeletions.Add(lineup);
                        lineupAdditions.Remove(lineup);
                    }
                }
            }

            foreach (ListViewItem item in lvSelectedLineups.SelectedItems)
                lvSelectedLineups.Items.Remove(item);            

            lvSelectedLineups.SelectedItems.Clear();
            btDelete.Enabled = false;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            int added = 0;
            int deleted = 0;

            foreach (SchedulesDirectLineup lineup in lineupDeletions)
            {
                ReplyBase reply = SchedulesDirectController.Instance.DeleteLineup(lineup.Identity);
                if (reply.Message != null)
                {
                    MessageBox.Show("Failed to delete lineup '" + lineup.FullName + "'." + Environment.NewLine + Environment.NewLine +
                        reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    deleted++;
            }

            foreach (SchedulesDirectLineup lineup in lineupAdditions)
            {
                ReplyBase reply = SchedulesDirectController.Instance.AddLineup(lineup.Identity);
                if (reply.Message != null)
                {
                    MessageBox.Show("Failed to add lineup '" + lineup.FullName + "'" + Environment.NewLine + Environment.NewLine +
                        reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    removeSelectedLineup(lineup);
                }
                else
                    added++;
            }

            foreach (SchedulesDirectSatellite satellite in satelliteDeletions)
            {
                ReplyBase reply = SchedulesDirectController.Instance.DeleteLineup(satellite.Name);
                if (reply.Message != null)
                {
                    MessageBox.Show("Failed to delete lineup '" + satellite.Name + "'." + Environment.NewLine + Environment.NewLine +
                        reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    deleted++;
            }

            foreach (SchedulesDirectSatellite satellite in satelliteAdditions)
            {
                ReplyBase reply = SchedulesDirectController.Instance.AddLineup(satellite.Name);
                if (reply.Message != null)
                {
                    MessageBox.Show("Failed to add lineup '" + satellite.Name + "'" + Environment.NewLine + Environment.NewLine +
                        reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    removeSelectedLineup(satellite);
                }
                else
                    added++;
            }

            foreach (SchedulesDirectTransmitter transmitter in transmitterDeletions)
            {
                ReplyBase reply = SchedulesDirectController.Instance.DeleteLineup(transmitter.Identity);
                if (reply.Message != null)
                {
                    MessageBox.Show("Failed to delete lineup '" + transmitter.Name + "'." + Environment.NewLine + Environment.NewLine +
                        reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    deleted++;
            }

            foreach (SchedulesDirectTransmitter transmitter in transmitterAdditions)
            {
                ReplyBase reply = SchedulesDirectController.Instance.AddLineup(transmitter.Identity);
                if (reply.Message != null)
                {
                    MessageBox.Show("Failed to add lineup '" + transmitter.Name + "'" + Environment.NewLine + Environment.NewLine +
                        reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    removeSelectedLineup(transmitter);
                }
                else
                    added++;
            }

            SchedulesDirectConfig.Instance.Unload();
            MessageBox.Show(added + " lineups added, " + deleted + " lineups deleted", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Cursor.Current = Cursors.Default;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void removeSelectedLineup(SchedulesDirectLineup deletedlineup)
        {
            foreach (ListViewItem item in lvSelectedLineups.Items)
            {
                SchedulesDirectLineup lineup = item.Tag as SchedulesDirectLineup;
                if (lineup != null && lineup.Identity == deletedlineup.Identity)
                {
                    lvSelectedLineups.Items.Remove(item);
                    return;
                }
            }
        }

        private void removeSelectedLineup(SchedulesDirectSatellite deletedSatellite)
        {
            foreach (ListViewItem item in lvSelectedLineups.Items)
            {
                SchedulesDirectSatellite satellite = item.Tag as SchedulesDirectSatellite;
                if (satellite != null && satellite.Name == deletedSatellite.Name)
                {
                    lvSelectedLineups.Items.Remove(item);
                    return;
                }
            }
        }

        private void removeSelectedLineup(SchedulesDirectTransmitter deletedTransmitter)
        {
            foreach (ListViewItem item in lvSelectedLineups.Items)
            {
                SchedulesDirectTransmitter transmitter = item.Tag as SchedulesDirectTransmitter;
                if (transmitter != null && transmitter.Identity == deletedTransmitter.Identity)
                {
                    lvSelectedLineups.Items.Remove(item);
                    return;
                }
            }
        }
    }
}

