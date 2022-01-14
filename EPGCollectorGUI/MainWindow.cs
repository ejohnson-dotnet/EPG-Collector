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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;

using DomainObjects;
using DirectShow;
using DVBServices;
using Lookups;
using XmltvParser;
using MxfParser;
using ChannelUpdate;
using SatIp;
using VBox;
using NetReceiver;
using NetworkProtocols;
using SchedulesDirect;

namespace EPGCentre
{
    internal partial class MainWindow : Form
    {
        internal static string AssemblyVersion
        {
            get
            {
                System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
                return (version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision);
            }
        }

        internal static string CurrentTSPath { get; set; }

        private CollectorParametersControl collectorParametersControl;
        private DVBLogicParametersControl pluginParametersControl;        
        private ChangeProgramContentControl changeProgramContentControl;
        private ChangeProgramCategoryControl changeProgramCategoryControl;
        private ChangeMHWCategoriesControl changeMHWCategoriesControl;
        private ChangePSIPCategoriesControl changePSIPCategoriesControl;
        private ChangeDishNetworkCategoriesControl changeDishNetworkCategoriesControl;
        private ChangeBellTVCategoriesControl changeBellTVCategoriesControl;
        private ChangeCustomCategoriesControl changeCustomCategoriesControl;
        private ChangeXMLTVCategoriesControl changeXMLTVCategoriesControl;
        private ChangeSdCategoriesControl changeSdCategoriesControl;

        private ViewHistoryControl viewHistoryControl;
        private ViewLogControl viewLogControl;
        private ViewSatIPControl viewSatIPControl;

        private OutputFileUnformattedControl outputFileUnformattedControl;
        private RunCollectionControl runCollectionControl;
        private FindEPGControl findEPGControl;
        private TransportStreamDumpControl tsDumpControl;
        private TransportStreamAnalyzeControl tsAnalyzeControl;
        private ChannelScanControl channelScanControl;
        private RunWMCMaintenanceControl runWMCMaintenanceControl;

        private IUpdateControl currentControl;        
        private string collectionParameters;

        private IView viewControl;

        private static MainWindow mainWindow;

        private bool updateChecksDone;

        private string currentStandaloneIniPath;
        private string currentPluginIniPath;
        private string currentXmltvViewPath;
        private string currentRunPath;

        private int standardHeight;
        private int standardWidth;

        private Control latestControl;
        private bool latestParent;

        internal MainWindow(string[] args)
        {
            InitializeComponent();

            Logger.Instance.WriteSeparator("EPG Centre (Version " + RunParameters.SystemVersion + ")");

            Logger.Instance.Write("");
            Logger.Instance.Write("OS version: " + Environment.OSVersion.Version + (RunParameters.Is64Bit ? " 64-bit" : " 32-bit"));
            Logger.Instance.Write("");
            Logger.Instance.Write("Executable build: " + AssemblyVersion);
            Logger.Instance.Write("DirectShow build: " + DirectShowGraph.AssemblyVersion);
            Logger.Instance.Write("DomainObjects build: " + RunParameters.AssemblyVersion);
            Logger.Instance.Write("DVBServices build: " + DVBServices.Utils.AssemblyVersion);
            Logger.Instance.Write("Lookups build: " + LookupController.AssemblyVersion);
            Logger.Instance.Write("ChannelUpdate build: " + DVBLinkController.AssemblyVersion);
            Logger.Instance.Write("XmltvParser build: " + XmltvController.AssemblyVersion);
            Logger.Instance.Write("MxfParser build: " + MxfController.AssemblyVersion);
            Logger.Instance.Write("NetReceiver build: " + ReceiverBase.AssemblyVersion);
            Logger.Instance.Write("NetworkProtocols build: " + NetworkConfiguration.AssemblyVersion);
            Logger.Instance.Write("SatIp build: " + SatIpController.AssemblyVersion);
            Logger.Instance.Write("VBox build: " + VBoxController.AssemblyVersion);
            Logger.Instance.Write("SchedulesDirect build: " + SchedulesDirectController.AssemblyVersion);
            Logger.Instance.Write("");            
            Logger.Instance.Write("Privilege level: " + RunParameters.Role);
            Logger.Instance.Write("");
            Logger.Instance.Write("Base directory: " + RunParameters.BaseDirectory);
            Logger.Instance.Write("Data directory: " + RunParameters.DataDirectory);
            Logger.Instance.Write("");

            bool reply = CommandLine.Process(args);
            if (!reply)
            {
                Logger.Instance.Write("<e> Incorrect command line");
                Logger.Instance.Write("<e> Exiting with code = 4");
                MessageBox.Show("The command line is incorrect.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit((int)ExitCode.CommandLineWrong);
            }

            if (RunParameters.IsMono)
            {
                Logger.Instance.Write("Mono version: " + RunParameters.MonoVersion);
                Logger.Instance.Write("");
            }

            if (RunParameters.IsWine)
            {
                Logger.Instance.Write("Running in the Wine environment");
                Logger.Instance.Write("");
            }

            mainWindow = this;
            
            standardHeight = mainPanel.Height;
            standardWidth = mainPanel.Width;

            if (mainWindow.Height > Screen.PrimaryScreen.WorkingArea.Height)
                mainWindow.Height = (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.90);
            if (mainWindow.Width > Screen.PrimaryScreen.WorkingArea.Width)
                mainWindow.Width = (int)(Screen.PrimaryScreen.WorkingArea.Width * 0.90);

            BDAGraph.LoadTuners();
            int bdaTunerCount = Tuner.TunerCollection.Count;

            if (SatIpConfiguration.SatIpEnabled)
            {
                Logger.Instance.Write("Sat>IP servers enabled");
                SatIpServer.LoadServers();                
            }

            if (VBoxConfiguration.VBoxEnabled)
            {
                Logger.Instance.Write("VBox servers enabled");
                VBoxTuner.LoadServers();
            }

            /*if (Tuner.TunerCollection.Count == 0)
            {
                DialogResult result = MessageBox.Show("There are no tuners installed on this machine." +
                    Environment.NewLine + Environment.NewLine + 
                    "Do you want to continue?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    Environment.Exit((int)ExitCode.NoDVBTuners);
            }*/

            if (bdaTunerCount == Tuner.TunerCollection.Count)
            {
                clearSatIPLogToolStripMenuItem.Visible = false;
                satIPLogToolStripMenuItem.Visible = false;
            }

            historyToolStripMenuItem_Click(null, null);

            Logger.Instance.Write("EPG Centre is loaded");
        }

        internal static void ChangeMenuItemAvailability(bool availability)
        {
            Logger.Instance.Write("Changing menu availability to " + availability);

            mainWindow.fileToolStripMenuItem.Enabled = availability;
            mainWindow.viewToolStripMenuItem.Enabled = availability;
            mainWindow.windowToolStripMenuItem.Enabled = availability;
            mainWindow.runToolStripMenuItem.Enabled = availability;

            foreach (ToolStripItem item in mainWindow.toolStrip.Items)
                item.Enabled = availability;

            if (availability)
            {
                mainWindow.changeSaveAvailability(mainWindow.saveToolStripMenuItem.Enabled, mainWindow.saveAsToolStripMenuItem.Enabled);
                mainWindow.changeFindFilterAvailability(mainWindow.filterTextToolStripMenuItem.Enabled);
            }
        }

        internal static void ChangeFindFilterAvailability(bool availability)
        {
            Logger.Instance.Write("Changing Find/Filter availability to " + availability);
            mainWindow.changeFindFilterAvailability(availability);
        }

        internal static void SetLoadFromScanState(bool state)
        {
            if (mainWindow.collectorParametersControl != null)
                mainWindow.collectorParametersControl.SetLoadFromScanState(state);
        }

        private void createNewCollectorMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Create collection parameters menu option selected");

            bool proceed = checkSaveFile(collectorParametersControl, "Collection Parameters");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            SelectTask selectTask = new SelectTask(false);
            DialogResult result = selectTask.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Logger.Instance.Write("Select task aborted by user");
                return;
            }

            if (collectorParametersControl == null)
            {
                collectorParametersControl = new CollectorParametersControl();
                positionControl(collectorParametersControl, false);
            }

            if (selectTask.Task != null)
            {
                this.Text = collectorParametersControl.Heading + selectTask.Task;
                Logger.Instance.Write("Creating parameters using task '" + selectTask.Task + "'");
            }
            else
            {
                this.Text = collectorParametersControl.Heading + "Custom Parameters";
                Logger.Instance.Write("Creating custom parameters");
            }

            collectorParametersControl.Tag = new ControlStatus(this.Text);            

            saveAsToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = false;

            if (selectTask.Task != null)
            {
                string fileName = Path.Combine(RunParameters.BaseDirectory, Path.Combine("Samples", Path.Combine("Collector", selectTask.Task))) + ".ini";                
                collectionParameters = fileName;
                collectorParametersControl.Process(fileName, true);
            }
            else
                collectorParametersControl.Process();

            changeSaveAvailability(false, true);
            changeFindFilterAvailability(false);
            
            hideAllControls(collectorParametersControl);
            currentControl = collectorParametersControl;

            bar1ToolStripMenuItem.Visible = true;
            collectorParametersToolStripMenuItem.Visible = true;
        }

        private void openCollectorParametersMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update collection parameters menu option selected");

            bool proceed = checkSaveFile(collectorParametersControl, "Collection Parameters");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "INI Files (*.ini)|*.ini";
            if (currentStandaloneIniPath == null)
                openFile.InitialDirectory = RunParameters.DataDirectory;
            else
                openFile.InitialDirectory = currentStandaloneIniPath;
            openFile.RestoreDirectory = true;
            openFile.Title = "Open EPG Collection Parameter File";
            
            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Logger.Instance.Write("Select parameters aborted by user");
                return;
            }

            Logger.Instance.Write("Updating collection parameter file - " + openFile.FileName);

            if (collectorParametersControl == null)
            {
                collectorParametersControl = new CollectorParametersControl();
                positionControl(collectorParametersControl, false);
            }

            this.Text = collectorParametersControl.Heading + openFile.FileName;
            collectorParametersControl.Tag = new ControlStatus(this.Text);

            collectionParameters = openFile.FileName;
            currentStandaloneIniPath = new FileInfo(collectionParameters).DirectoryName;
            
            collectorParametersControl.Process(openFile.FileName, false);

            changeSaveAvailability(true, true);
            changeFindFilterAvailability(false); 

            hideAllControls(collectorParametersControl);
            currentControl = collectorParametersControl;

            bar1ToolStripMenuItem.Visible = true;
            collectorParametersToolStripMenuItem.Visible = true;
        }

        private void pluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Create plugin parameters menu option selected");

            bool proceed = checkSaveFile(pluginParametersControl, "Plugin Parameters");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            SelectTask selectTask = new SelectTask(true);
            DialogResult result = selectTask.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Logger.Instance.Write("Select task aborted by user");
                return;
            }

            if (pluginParametersControl == null)
            {
                pluginParametersControl = new DVBLogicParametersControl();
                positionControl(pluginParametersControl, false);
            }

            if (selectTask.Task != null)
            {
                this.Text = pluginParametersControl.Heading + selectTask.Task;
                Logger.Instance.Write("Creating parameters using task '" + selectTask.Task + "'");
            }
            else
            {
                this.Text = pluginParametersControl.Heading + "Custom Parameters";
                Logger.Instance.Write("Creating custom parameters");
            }

            pluginParametersControl.Tag = new ControlStatus(this.Text);

            saveAsToolStripMenuItem.Enabled = true;
            saveToolStripMenuItem.Enabled = false;

            if (selectTask.Task != null)
            {
                string fileName = Path.Combine(RunParameters.BaseDirectory, Path.Combine("Samples", Path.Combine("DVBLogic Plugin", selectTask.Task))) + ".ini";
                collectionParameters = fileName;
                pluginParametersControl.Process(fileName, true);
            }
            else
                pluginParametersControl.Process();
            
            changeSaveAvailability(false, true);
            changeFindFilterAvailability(false);

            hideAllControls(pluginParametersControl);
            currentControl = pluginParametersControl;

            bar1ToolStripMenuItem.Visible = true;
            pluginParametersToolStripMenuItem.Visible = true;
        }

        private void openPluginParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update plugin parameters menu option selected");

            bool proceed = checkSaveFile(pluginParametersControl, "Plugin Parameters");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "INI Files (*.ini)|*.ini";
            if (currentPluginIniPath == null)
            {
                openFile.InitialDirectory = getDVBLinkPluginPath();
                if (openFile.InitialDirectory == null)
                    openFile.InitialDirectory = RunParameters.DataDirectory;
            }
            else
                openFile.InitialDirectory = currentPluginIniPath;
            openFile.RestoreDirectory = true;
            openFile.Title = "Open EPG Plugin Parameter File";

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Logger.Instance.Write("Select parameters aborted by user");
                return;
            }

            Logger.Instance.Write("Updating plugin parameter file - " + openFile.FileName);

            if (pluginParametersControl == null)
            {
                pluginParametersControl = new DVBLogicParametersControl();
                positionControl(pluginParametersControl, false);
            }

            this.Text = pluginParametersControl.Heading + openFile.FileName;
            pluginParametersControl.Tag = new ControlStatus(this.Text);

            collectionParameters = openFile.FileName;
            currentPluginIniPath = new FileInfo(collectionParameters).DirectoryName;
            
            pluginParametersControl.Process(openFile.FileName, false);

            changeSaveAvailability(true, true);
            changeFindFilterAvailability(false);

            hideAllControls(pluginParametersControl);
            currentControl = pluginParametersControl;

            bar1ToolStripMenuItem.Visible = true;
            pluginParametersToolStripMenuItem.Visible = true;
        }

        private void updateDVBLogicPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update DVBLogic Plugin menu option selected");

            FolderBrowserDialog browsePath = new FolderBrowserDialog();
            browsePath.Description = "EPG Centre - Find DVBLogic EPG Directory";
            browsePath.SelectedPath = getDVBLinkPluginPath();

            DialogResult result = browsePath.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Logger.Instance.Write("Cancelled by user");
                return;
            }

            currentPluginIniPath = browsePath.SelectedPath;

            if (!browsePath.SelectedPath.ToUpper().EndsWith(Path.DirectorySeparatorChar + "EPG"))
            {
                Logger.Instance.Write("Querying path selected.");
                result = MessageBox.Show("The path selected does not reference an 'EPG' directory." + Environment.NewLine + Environment.NewLine +
                    "Is the path correct?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    Logger.Instance.Write("Cancelled by user - path incorrect");
                    return;
                }
            }

            string pluginPath = Path.Combine(browsePath.SelectedPath, "DVBLogicCPPPlugin.dll");

            if (File.Exists(Path.Combine(browsePath.SelectedPath, "DVBLogicCPPPlugin.dll")))
            {
                Logger.Instance.Write("Existing plugin located");

                DateTime newWriteTime = File.GetLastWriteTime(Path.Combine(RunParameters.BaseDirectory, "DVBLogicCPPPlugin.dll"));
                DateTime existingWriteTime = File.GetLastWriteTime(pluginPath);

                if (newWriteTime <= existingWriteTime)
                {
                    Logger.Instance.Write("Latest version is installed - " + newWriteTime + ":" + existingWriteTime);
                    result = MessageBox.Show("The plugin software is up to date." + Environment.NewLine + Environment.NewLine +
                    "Do you still want to update it?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        Logger.Instance.Write("Cancelled by user - existing plugin up to date");
                        return;
                    }
                }
                else
                    Logger.Instance.Write("Newer version available to install");
            }

            Exception updateException = updatePlugin(pluginPath);
            if (updateException != null)
            {
                IOException ioException = updateException as IOException;
                if (ioException == null)
                    return;

                result = MessageBox.Show("Do you want EPG Centre to try stopping the DVBLink server process and updating the plugin again?" + Environment.NewLine + Environment.NewLine +
                    "EPG Centre will restart the server process afterwards.", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                ServiceController controller = stopDVBLinkServer();
                if (controller == null)
                    return;
                
                updateException = updatePlugin(pluginPath);
                if (updateException != null)
                    return;

                updateException = startDVBLinkServer(controller);
                if (updateException != null)
                    return;                
            }

            try
            {
                string locationPath = Path.Combine(browsePath.SelectedPath, "EPG Collector Gateway.cfg");

                if (File.Exists(locationPath))
                {
                    File.SetAttributes(locationPath, FileAttributes.Normal);
                    File.Delete(locationPath);                    
                }

                FileStream fileStream = new FileStream(locationPath, FileMode.Create, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fileStream);

                streamWriter.WriteLine("Location=" + Path.Combine(RunParameters.BaseDirectory, "DVBLogicPlugin.dll"));

                streamWriter.Close();
                fileStream.Close();

                File.SetAttributes(locationPath, FileAttributes.ReadOnly);
                Logger.Instance.Write("Software location file written successfully");
                Logger.Instance.Write("Software location set to " + RunParameters.BaseDirectory);
            }
            catch (IOException e1)
            {
                Logger.Instance.Write("<e> The software location file could not be written - " + e1.Message);
                MessageBox.Show("The software location file could not be written." + Environment.NewLine + Environment.NewLine + e1.Message,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (UnauthorizedAccessException e2)
            {
                Logger.Instance.Write("<e> The software location file could not be written - " + e2.Message);
                MessageBox.Show("The software location file could not be written." + Environment.NewLine + Environment.NewLine + e2.Message,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private Exception updatePlugin(string pluginPath)
        {
            try
            {
                File.Copy(Path.Combine(RunParameters.BaseDirectory, "DVBLogicCPPPlugin.dll"), pluginPath, true);
                Logger.Instance.Write("Plugin installed - version now " + File.GetLastWriteTime(pluginPath));
                MessageBox.Show("The plugin module has been updated.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return (null);
            }
            catch (IOException e1)
            {
                Logger.Instance.Write("<e> Plugin install failed - " + e1.Message);
                MessageBox.Show("The plugin could not be updated." + Environment.NewLine + Environment.NewLine + e1.Message,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (e1);
            }
            catch (UnauthorizedAccessException e2)
            {
                Logger.Instance.Write("<e> Plugin install failed - " + e2.Message);
                MessageBox.Show("The plugin could not be updated." + Environment.NewLine + Environment.NewLine + e2.Message,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (e2);
            }
        }

        private ServiceController stopDVBLinkServer()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                ServiceController controller = new ServiceController("dvblink_server");

                Logger.Instance.Write("Stopping the DVBLink server process");
                controller.Stop();

                controller.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 30));

                Logger.Instance.Write("The DVBLink server process has been stopped");
                Cursor.Current = Cursors.Default;

                return (controller);
            }
            catch (System.ServiceProcess.TimeoutException)
            {
                Cursor.Current = Cursors.Default;

                Logger.Instance.Write("<E> The DVBLink server process did not stop after 30 seconds");
                MessageBox.Show("The DVBLink server process could not be stopped." + Environment.NewLine + Environment.NewLine +
                    "The plugin cannot be updated.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (null);

            }
            catch (Exception e2)
            {
                Cursor.Current = Cursors.Default;

                Logger.Instance.Write("<E> An exception of type " + e2.GetType().Name + " has occurred");
                Logger.Instance.Write("<E> " + e2.Message);
                Logger.Instance.Write("<E> Failed to stop the DVBLink server process");
                return (null);
            }
        }

        private Exception startDVBLinkServer(ServiceController controller)
        {
            try
            {
                Logger.Instance.Write("Restarting the DVBLink server process");
                controller.Start();
                Logger.Instance.Write("The DVBLink server process has been restarted");
                MessageBox.Show("The server process has been restarted.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return (null);
            }
            catch (Exception e)
            {
                Logger.Instance.Write("<E> An exception of type " + e.GetType().Name + " has occurred");
                Logger.Instance.Write("<E> Failed to restart the DVBLink server process");
                return (e);
            }
        }

        private void editProgramContentEITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update EIT Program Contents menu option selected");

            bool proceed = checkSaveFile(changeProgramContentControl, "EIT Program Categories");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            if (changeProgramContentControl == null)
            {
                changeProgramContentControl = new ChangeProgramContentControl();
                positionControl(changeProgramContentControl, true);
            }

            this.Text = changeProgramContentControl.Heading + EITProgramCategory.FileName + ".cfg";
            changeProgramContentControl.Tag = new ControlStatus(this.Text);

            string fullPath = Path.Combine(RunParameters.DataDirectory, EITProgramCategory.FileName + ".cfg");
            if (!File.Exists(fullPath))
                fullPath = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", EITProgramCategory.FileName + ".cfg"));

            Cursor.Current = Cursors.WaitCursor;
            changeProgramContentControl.Process(fullPath);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false); 

            hideAllControls(changeProgramContentControl);
            currentControl = changeProgramContentControl;

            bar1ToolStripMenuItem.Visible = true;
            programContentsToolStripMenuItem.Visible = true;
        }

        private void editProgramCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update OpenTV Program Categories menu option selected");

            bool proceed = checkSaveFile(changeProgramCategoryControl, "OpenTV Program Categories");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            SelectCategoryFile selectCategoryFile = new SelectCategoryFile();
            DialogResult result = selectCategoryFile.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Logger.Instance.Write("Select categories aborted by user");
                return;
            }

            Logger.Instance.Write("Updating OpenTV Program Categories file - " + selectCategoryFile.File);

            if (changeProgramCategoryControl == null)
            {
                changeProgramCategoryControl = new ChangeProgramCategoryControl();
                positionControl(changeProgramCategoryControl, true);
            }

            this.Text = changeProgramCategoryControl.Heading + selectCategoryFile.File;
            changeProgramCategoryControl.Tag = new ControlStatus(this.Text);

            string fullPath = Path.Combine(RunParameters.DataDirectory, selectCategoryFile.File);
            if (!File.Exists(fullPath))
                fullPath = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", selectCategoryFile.File));

            Cursor.Current = Cursors.WaitCursor;
            changeProgramCategoryControl.Process(fullPath);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeProgramCategoryControl);
            currentControl = changeProgramCategoryControl;

            bar1ToolStripMenuItem.Visible = true;
            programCategoriesToolStripMenuItem.Visible = true;
        }

        private void changeMediaHighwayProgramCategoryDescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update MHW Program Categories menu option selected");

            bool proceed = checkSaveFile(changeMHWCategoriesControl, "MediaHighway Program Categories");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            SelectMHWCategoryFile selectCategoryFile = new SelectMHWCategoryFile();
            DialogResult result = selectCategoryFile.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Logger.Instance.Write("Select categories aborted by user");
                return;
            }

            string selectedFile;

            if (selectCategoryFile.File[0] == ' ')
                selectedFile = "New File";
            else
                selectedFile = selectCategoryFile.File;

            Logger.Instance.Write("Updating MHW Program Categories file - " + selectedFile);

            if (changeMHWCategoriesControl == null)
            {
                changeMHWCategoriesControl = new ChangeMHWCategoriesControl();
                positionControl(changeMHWCategoriesControl, true);
            }

            this.Text = changeMHWCategoriesControl.Heading + selectedFile;
            changeMHWCategoriesControl.Tag = new ControlStatus(this.Text);

            string fullPath;
            
            if (selectedFile != "New File")
            {
                fullPath = Path.Combine(RunParameters.DataDirectory, selectCategoryFile.File);
                if (!File.Exists(fullPath))
                    fullPath = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", selectCategoryFile.File));
            }
            else
                fullPath = null;

            Cursor.Current = Cursors.WaitCursor;
            changeMHWCategoriesControl.Process(fullPath);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(selectedFile != "New File", selectedFile == "New File");
            changeFindFilterAvailability(false);

            hideAllControls(changeMHWCategoriesControl);
            currentControl = changeMHWCategoriesControl;

            bar1ToolStripMenuItem.Visible = true;
            mhwCategoriesToolStripMenuItem.Visible = true;
        }

        private void changePSIPProgramCategoryDescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update PSIP Program Contents menu option selected");

            bool proceed = checkSaveFile(changePSIPCategoriesControl, "PSIP Program Categories");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            if (changePSIPCategoriesControl == null)
            {
                changePSIPCategoriesControl = new ChangePSIPCategoriesControl();
                positionControl(changePSIPCategoriesControl, true);
            }

            this.Text = changePSIPCategoriesControl.Heading + AtscPsipProgramCategory.FileName + ".cfg";
            changePSIPCategoriesControl.Tag = new ControlStatus(this.Text);

            string fullPath = Path.Combine(RunParameters.DataDirectory, AtscPsipProgramCategory.FileName + ".cfg");
            if (!File.Exists(fullPath))
                fullPath = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", AtscPsipProgramCategory.FileName + ".cfg"));

            Cursor.Current = Cursors.WaitCursor;
            changePSIPCategoriesControl.Process(fullPath);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changePSIPCategoriesControl);
            currentControl = changePSIPCategoriesControl;

            bar1ToolStripMenuItem.Visible = true;
            psipCategoriesToolStripMenuItem.Visible = true;
        }

        private void changeDishNetworkProgramCategoryDescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update Dish Network Program Categories menu option selected");

            bool proceed = checkSaveFile(changeDishNetworkCategoriesControl, "Dish Network Program Categories");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            if (changeDishNetworkCategoriesControl == null)
            {
                changeDishNetworkCategoriesControl = new ChangeDishNetworkCategoriesControl();
                positionControl(changeDishNetworkCategoriesControl, true);
            }

            this.Text = changeDishNetworkCategoriesControl.Heading + DishNetworkProgramCategory.FileName + ".cfg";
            changeDishNetworkCategoriesControl.Tag = new ControlStatus(this.Text);

            string fullPath = Path.Combine(RunParameters.DataDirectory, DishNetworkProgramCategory.FileName + ".cfg");
            if (!File.Exists(fullPath))
                fullPath = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", DishNetworkProgramCategory.FileName + ".cfg"));

            Cursor.Current = Cursors.WaitCursor;
            changeDishNetworkCategoriesControl.Process(fullPath);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeDishNetworkCategoriesControl);
            currentControl = changeDishNetworkCategoriesControl;

            bar1ToolStripMenuItem.Visible = true;
            dishNetworkCategoriesToolStripMenuItem.Visible = true;
        }

        private void changeBellTVProgramCategoryDescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update Bell TV Program Categories menu option selected");

            bool proceed = checkSaveFile(changeBellTVCategoriesControl, "Bell TV Program Categories");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            if (changeBellTVCategoriesControl == null)
            {
                changeBellTVCategoriesControl = new ChangeBellTVCategoriesControl();
                positionControl(changeBellTVCategoriesControl, true);
            }

            this.Text = changeBellTVCategoriesControl.Heading + BellTVProgramCategory.FileName + ".cfg";
            changeBellTVCategoriesControl.Tag = new ControlStatus(this.Text);

            string fullPath = Path.Combine(RunParameters.DataDirectory, BellTVProgramCategory.FileName + ".cfg");
            if (!File.Exists(fullPath))
                fullPath = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", BellTVProgramCategory.FileName + ".cfg"));

            Cursor.Current = Cursors.WaitCursor;
            changeBellTVCategoriesControl.Process(fullPath);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeBellTVCategoriesControl);
            currentControl = changeBellTVCategoriesControl;

            bar1ToolStripMenuItem.Visible = true;
            bellTVCategoriesToolStripMenuItem.Visible = true;
        }

        private void changeCustomProgramCategoryDescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update Custom Program Contents menu option selected");

            bool proceed = checkSaveFile(changeCustomCategoriesControl, "Custom Program Categories");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            if (changeCustomCategoriesControl == null)
            {
                changeCustomCategoriesControl = new ChangeCustomCategoriesControl();
                positionControl(changeCustomCategoriesControl, true);
            }

            this.Text = changeCustomCategoriesControl.Heading + CustomProgramCategory.FileName + ".cfg";
            changeCustomCategoriesControl.Tag = new ControlStatus(this.Text);

            string fullPath = Path.Combine(RunParameters.DataDirectory, CustomProgramCategory.FileName + ".cfg");
            if (!File.Exists(fullPath))
                fullPath = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", CustomProgramCategory.FileName + ".cfg"));                

            Cursor.Current = Cursors.WaitCursor;
            changeCustomCategoriesControl.Process(fullPath);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeCustomCategoriesControl);
            currentControl = changeCustomCategoriesControl;

            bar1ToolStripMenuItem.Visible = true;
            customCategoriesToolStripMenuItem.Visible = true;
        }

        private void changeXMLTVProgramCategoryDescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update XMLTV Program Contents menu option selected");

            bool proceed = checkSaveFile(changeXMLTVCategoriesControl, "XMLTV Program Categories");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            if (changeXMLTVCategoriesControl == null)
            {
                changeXMLTVCategoriesControl = new ChangeXMLTVCategoriesControl();
                positionControl(changeXMLTVCategoriesControl, true);
            }

            this.Text = changeXMLTVCategoriesControl.Heading + XmltvProgramCategory.FileName + ".cfg";
            changeXMLTVCategoriesControl.Tag = new ControlStatus(this.Text);

            string fullPath = Path.Combine(RunParameters.DataDirectory, XmltvProgramCategory.FileName + ".cfg");
            if (!File.Exists(fullPath))
            {
                fullPath = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", XmltvProgramCategory.FileName + ".cfg"));
                if (!File.Exists(fullPath))
                    fullPath = null;
            }

            Cursor.Current = Cursors.WaitCursor;
            changeXMLTVCategoriesControl.Process(fullPath);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeXMLTVCategoriesControl);
            currentControl = changeXMLTVCategoriesControl;

            bar1ToolStripMenuItem.Visible = true;
            xmltvCategoriesToolStripMenuItem.Visible = true;
        }

        private void changeSchedulesDirectProgramCategoryDescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Update Schedules Direct Program Contents menu option selected");

            bool proceed = checkSaveFile(changeSdCategoriesControl, "Schedules Direct Program Categories");
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            if (changeSdCategoriesControl == null)
            {
                changeSdCategoriesControl = new ChangeSdCategoriesControl();
                positionControl(changeSdCategoriesControl, true);
            }

            this.Text = changeSdCategoriesControl.Heading + SchedulesDirectProgramCategory.FileName + ".cfg";
            changeSdCategoriesControl.Tag = new ControlStatus(this.Text);

            string fullPath = Path.Combine(RunParameters.DataDirectory, SchedulesDirectProgramCategory.FileName + ".cfg");
            if (!File.Exists(fullPath))
            {
                fullPath = Path.Combine(RunParameters.ConfigDirectory, Path.Combine("Program Categories", SchedulesDirectProgramCategory.FileName + ".cfg"));
                if (!File.Exists(fullPath))
                    fullPath = null;
            }

            Cursor.Current = Cursors.WaitCursor;
            changeSdCategoriesControl.Process(fullPath);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeSdCategoriesControl);
            currentControl = changeSdCategoriesControl;

            bar1ToolStripMenuItem.Visible = true;
            sdCategoriesToolStripMenuItem.Visible = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Save menu option selected");

            bool result = currentControl.PrepareToSave();
            if (!result)
            {
                Logger.Instance.Write("Aborted due to incorrect data");
                return;
            }

            currentControl.Save();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Save As menu option selected");

            bool prepareResult = currentControl.PrepareToSave();
            if (!prepareResult)
            {
                Logger.Instance.Write("Aborted due to incorrect data");
                return;
            }

            string actualName;

            if (currentControl as ChangeMHWCategoriesControl != null)
            {
                SaveMHWCategoryFile saveMHWCategoryFile = new SaveMHWCategoryFile();
                DialogResult result = saveMHWCategoryFile.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    Logger.Instance.Write("Specify saved file aborted by user");
                    return;
                }
                else
                    actualName = Path.Combine(RunParameters.DataDirectory, "MHW" +  saveMHWCategoryFile.SelectedType + " Categories " + saveMHWCategoryFile.SelectedFrequency + ".cfg");
            }
            else
            {
                if (currentControl as DVBLogicParametersControl == null)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = currentControl.SaveFileFilter;
                    saveFileDialog.Title = currentControl.SaveFileTitle;

                    if (currentStandaloneIniPath != null)
                        saveFileDialog.InitialDirectory = currentStandaloneIniPath;
                    else
                         saveFileDialog.InitialDirectory = RunParameters.DataDirectory;

                    saveFileDialog.FileName = currentControl.DefaultFileName;
                    saveFileDialog.OverwritePrompt = false;
                    if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
                    {
                        Logger.Instance.Write("Specify saved file aborted by user");
                        return;
                    }

                    currentStandaloneIniPath = new FileInfo(saveFileDialog.FileName).DirectoryName;

                    if (saveFileDialog.FileName.Trim().EndsWith(currentControl.SaveFileSuffix))
                        actualName = saveFileDialog.FileName.Trim();
                    else
                        actualName = saveFileDialog.FileName.Trim() + currentControl.SaveFileSuffix;                    
                }
                else
                {
                    FolderBrowserDialog browsePath = new FolderBrowserDialog();
                    browsePath.Description = "EPG Centre - Find DVBLogic EPG Directory";
                    browsePath.SelectedPath = getDVBLinkPluginPath();
                    if (browsePath.SelectedPath == null)
                        browsePath.SelectedPath = RunParameters.DataDirectory;
                    
                    DialogResult result = browsePath.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        Logger.Instance.Write("Cancelled by user");
                        return;
                    }

                    currentPluginIniPath = browsePath.SelectedPath;
                    actualName = Path.Combine(browsePath.SelectedPath, currentControl.DefaultFileName) + "." + currentControl.SaveFileSuffix;
                }
            }

            Logger.Instance.Write("Save file selected as " + actualName);

            if (File.Exists(actualName))
            {
                Logger.Instance.Write("File exists - asking for authority to overwrite");
                DialogResult questionResult = MessageBox.Show("The file '" + actualName + "' already exists." + Environment.NewLine + Environment.NewLine +
                    "Do you want to overwrite it?",
                    "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (questionResult == DialogResult.No)
                {
                    Logger.Instance.Write("User refused permission to overwrite - save aborted");
                    return;
                }
                else
                    Logger.Instance.Write("User authorised overwrite");
            }

            bool reply = currentControl.Save(actualName);
            if (!reply)
                return;

            if (currentControl as CollectorParametersControl != null)
            {
                collectionParameters = actualName;
                this.Text = currentControl.Heading + actualName;
            }
            else
            {
                if (currentControl as DVBLogicParametersControl != null)
                {
                    collectionParameters = actualName;
                    this.Text = currentControl.Heading + " - " + actualName;
                }
                else
                {
                    string[] nameParts = actualName.Split(new char[] { '\\' });
                    this.Text = currentControl.Heading + nameParts[nameParts.Length - 1];
                }
            }

            changeSaveAvailability(true, true);
        }

        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Clear History menu option selected");

            DialogResult result = MessageBox.Show("Please confirm that the History is to be cleared.",
                "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            string reply = Logger.Clear(Logger.HistoryFilePath);

            if (reply != null)
            {
                Logger.Instance.Write("History could not be cleared");
                Logger.Instance.Write(reply);
                MessageBox.Show("The history could not be cleared because an error occured." + Environment.NewLine + Environment.NewLine + reply,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Logger.Instance.Write("The history has been cleared.");
            MessageBox.Show("The History has been cleared.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (viewHistoryControl != null)
                viewHistoryControl.Clear();  
        }

        private void clearCollectorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Clear General Log menu option selected");

            DialogResult result = MessageBox.Show("Please confirm that the General log is to be cleared.", 
                "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            string reply = Logger.Clear();

            if (reply != null)
            {
                Logger.Instance.Write("Log could not be cleared");
                Logger.Instance.Write(reply);
                MessageBox.Show("The log could not be cleared because an error occured." + Environment.NewLine + Environment.NewLine + reply,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Logger.Instance.Write("The general log has been cleared.");
            MessageBox.Show("The General log has been cleared.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            if (viewLogControl != null)
                viewLogControl.Clear();    
        }

        private void clearSatIPLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Clear Network Log menu option selected");

            DialogResult result = MessageBox.Show("Please confirm that the Network log is to be cleared.",
                "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            string reply = Logger.Clear(Logger.NetworkFilePath);

            if (reply != null)
            {
                Logger.Instance.Write("Log could not be cleared");
                Logger.Instance.Write(reply);
                MessageBox.Show("The log could not be cleared because an error occured." + Environment.NewLine + Environment.NewLine + reply,
                    "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Logger.Instance.Write("The Network log has been cleared.");
            MessageBox.Show("The Network log has been cleared.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (viewSatIPControl != null)
                viewSatIPControl.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Exit menu option selected");

            bool proceed = checkSaveAllFiles();
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            updateChecksDone = true;

            Logger.Instance.Write("EPG Centre closing down");
            this.Close();
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("View history menu option selected");

            Logger logger = new Logger(false);
            bool opened = logger.Open(Path.Combine(RunParameters.DataDirectory, "EPG Collector.hst"));
            if (!opened)
            {
                Logger.Instance.Write("Failed to open history file - view history aborted");
                MessageBox.Show("The history file is not available.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (viewHistoryControl == null)
            {
                viewHistoryControl = new ViewHistoryControl();
                positionControl(viewHistoryControl, true);
            }

            this.Text = "EPG Centre - View History";
            viewHistoryControl.Tag = new ControlStatus(this.Text);

            Cursor.Current = Cursors.WaitCursor;
            viewHistoryControl.Process(logger);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            hideAllControls(viewHistoryControl);
            viewControl = viewHistoryControl;
        }

        private void collectorLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("View general log menu option selected");

            Logger logger = new Logger(true);
            bool opened = logger.Open();
            if (!opened)
            {
                Logger.Instance.Write("Failed to open general log file - view general log aborted");
                MessageBox.Show("The general log file is not available.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (viewLogControl == null)
            {
                viewLogControl = new ViewLogControl();
                positionControl(viewLogControl, true);
            }

            this.Text = "EPG Centre - View General Log";
            viewLogControl.Tag = new ControlStatus(this.Text);

            Cursor.Current = Cursors.WaitCursor;
            viewLogControl.Process(null, logger, true);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(viewLogControl.FindFilterAvailability);  

            hideAllControls(viewLogControl);
            viewControl = viewLogControl;

            bar0ToolStripMenuItem.Visible = true;
            logViewToolStripMenuItem.Visible = true;
        }

        private void satIPLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("View Network menu option selected");

            Logger logger = new Logger(false);
            bool opened = logger.Open(Logger.NetworkFilePath);
            if (!opened)
            {
                Logger.Instance.Write("Failed to open Network log file - view history aborted");
                MessageBox.Show("The Network log file is not available.", "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (viewSatIPControl == null)
            {
                viewSatIPControl = new ViewSatIPControl();
                positionControl(viewSatIPControl, true);
            }

            this.Text = "EPG Centre - View Network Log";
            viewSatIPControl.Tag = new ControlStatus(this.Text);

            Cursor.Current = Cursors.WaitCursor;
            viewSatIPControl.Process(Logger.NetworkFilePath, logger);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            hideAllControls(viewSatIPControl);
            viewControl = viewSatIPControl;

            satIPLogViewToolStripMenuItem.Visible = true;
        }

        private void otherLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("View other log menu option selected");

            SelectLog selectLog = new SelectLog();
            DialogResult result = selectLog.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            string actualName = Path.Combine(RunParameters.DataDirectory, selectLog.File) + ".log";

            Logger.Instance.Write("Log file selected as " + actualName);

            Logger logger = new Logger(true);

            bool opened = logger.Open(actualName);
            if (!opened)
            {
                Logger.Instance.Write("Failed to open log file - view other log aborted");
                MessageBox.Show("The log file '" + actualName + "' is not available.", "EPG Collector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (viewLogControl == null)
            {
                viewLogControl = new ViewLogControl();
                positionControl(viewLogControl, true);
            }

            this.Text = "EPG Centre - View Other Logs - " + selectLog.File;
            viewLogControl.Tag = new ControlStatus(this.Text);

            Cursor.Current = Cursors.WaitCursor;
            viewLogControl.Process(actualName, logger, false);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(viewLogControl.FindFilterAvailability); 

            hideAllControls(viewLogControl);
            viewControl = viewLogControl;
        }

        private void outputFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("View output file unformatted menu option selected");

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XMLTV Files (*.xml)|*.xml";
            openFile.RestoreDirectory = true;
            openFile.Title = "Open EPG Collection Output File";

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            Logger.Instance.Write("File selected as " + openFile.FileName);

            if (outputFileUnformattedControl == null)
            {
                outputFileUnformattedControl = new OutputFileUnformattedControl();
                positionControl(outputFileUnformattedControl, true);
            }

            this.Text = "EPG Centre - View Output File Unformatted - " + openFile.FileName;
            outputFileUnformattedControl.Tag = new ControlStatus(this.Text);

            Cursor.Current = Cursors.WaitCursor;
            outputFileUnformattedControl.Process(openFile.FileName);
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(outputFileUnformattedControl.FindFilterAvailability); 

            hideAllControls(outputFileUnformattedControl);
            viewControl = outputFileUnformattedControl;

            bar2ToolStripMenuItem.Visible = true;
            outputFileUnformattedToolStripMenuItem.Visible = true;
        }

        private void outputFileFormattedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("View output file formatted menu option selected");

            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Output Files (*.xml)|*.xml";
            if (currentXmltvViewPath == null)
                openFile.InitialDirectory = RunParameters.DataDirectory;
            else
                openFile.InitialDirectory = currentXmltvViewPath;
            openFile.RestoreDirectory = true;
            openFile.Title = "Open EPG Collection Output File";

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            Logger.Instance.Write("File selected as " + openFile.FileName);

            currentXmltvViewPath = new FileInfo(openFile.FileName).DirectoryName;

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(true);

            bar2ToolStripMenuItem.Visible = true;
            outputFileFormattedToolStripMenuItem.Visible = true;
        }

        private void findTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Find text menu option selected");

            if (viewControl == null)
            {
                Logger.Instance.Write("No view control available - aborted");
                return;
            }

            viewControl.FindText();
        }

        private void filterTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Filter text menu option selected");

            if (viewControl == null)
            {
                Logger.Instance.Write("No view control available - aborted");
                return;
            }

            viewControl.FilterText();
        }

        private void collectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Run collection menu option selected");

            bool proceed = checkSaveAllFiles();
            if (!proceed)
            {
                Logger.Instance.Write("Aborted by user");
                return;
            }

            string fileName;

            if (collectionParameters != null)
            {
                Logger.Instance.Write("Asking if current parameters are to be used");
                DialogResult result = MessageBox.Show("Do you want to use the parameters currently loaded to run the collection?", "EPG Centre", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                {
                    Logger.Instance.Write("Aborted by user");
                    return;
                }

                if (result == DialogResult.No)
                {
                    Logger.Instance.Write("User has requested to use different parameters");
                    fileName = getParameterFile("Collecting EPG");
                    if (fileName == null)
                    {
                        Logger.Instance.Write("Aborted by user");
                        return;
                    }
                }
                else
                {
                    Logger.Instance.Write("Using current parameters");
                    fileName = collectionParameters;
                }
            }
            else
            {
                Logger.Instance.Write("No current parameters - getting parameter file");
                fileName = getParameterFile("Collecting EPG");
                if (fileName == null)
                {
                    Logger.Instance.Write("Aborted by user");
                    return;
                }
            }

            RunParameters runParameters = new RunParameters(ParameterSet.Collector, RunType.Collection);
            ExitCode exitCode = runParameters.Process(fileName);
            if (exitCode != ExitCode.OK)
            {
                MessageBox.Show(runParameters.LastError + Environment.NewLine + Environment.NewLine + 
                    CommandLine.GetCompletionCodeDescription(exitCode),
                    "EPG Collector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (runParameters.PluginParameters)
            {
                MessageBox.Show("Plugin parameters cannot be used to run a collection from within EPG Centre" +
                    Environment.NewLine + Environment.NewLine +
                    "They can only be used in a TVSource environment.",
                    "EPG Collector", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (runCollectionControl == null)
                runCollectionControl = new RunCollectionControl();

            hideAllControls(runCollectionControl);

            Logger.Instance.Write("Running collection with " + fileName);

            this.Text = "EPG Centre - Collect EPG using " + fileName;
            runCollectionControl.Tag = new ControlStatus(this.Text);

            runCollectionControl.Process(fileName);

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            bar3ToolStripMenuItem.Visible = true;
            runCollectorToolStripMenuItem.Visible = true;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Find EPG menu option selected");

            if (findEPGControl == null)
            {
                findEPGControl = new FindEPGControl();
                positionControl(findEPGControl, false);
            }

            this.Text = findEPGControl.Heading;
            findEPGControl.Tag = new ControlStatus(this.Text);

            Cursor.Current = Cursors.WaitCursor;
            findEPGControl.Process();
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            hideAllControls(findEPGControl);
            currentControl = findEPGControl;

            bar3ToolStripMenuItem.Visible = true;
            findEPGToolStripMenuItem.Visible = true;
        }

        private void dumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Dump Transport Stream menu option selected");

            if (tsDumpControl == null)
            {
                tsDumpControl = new TransportStreamDumpControl();
                positionControl(tsDumpControl, false);
            }

            this.Text = tsDumpControl.Heading;
            tsDumpControl.Tag = new ControlStatus(this.Text);

            Cursor.Current = Cursors.WaitCursor;
            tsDumpControl.Process();
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            hideAllControls(tsDumpControl);
            currentControl = tsDumpControl;          
        }

        private void analyzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Analyze Transport Stream menu option selected");

            if (tsAnalyzeControl == null)
            {
                tsAnalyzeControl = new TransportStreamAnalyzeControl();
                positionControl(tsAnalyzeControl, false);
            }

            this.Text = tsAnalyzeControl.Heading;
            tsAnalyzeControl.Tag = new ControlStatus(this.Text);

            Cursor.Current = Cursors.WaitCursor;
            tsAnalyzeControl.Process();
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            hideAllControls(tsAnalyzeControl);
            currentControl = tsAnalyzeControl;

            bar4ToolStripMenuItem.Visible = true;
            analyzeTransportStreamToolStripMenuItem.Visible = true;
        }

        private void channelScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Channel scan menu option selected");

            if (channelScanControl == null)
            {
                channelScanControl = new ChannelScanControl();
                positionControl(channelScanControl, false);
            }

            this.Text = channelScanControl.Heading;
            channelScanControl.Tag = new ControlStatus(this.Text);

            Cursor.Current = Cursors.WaitCursor;
            channelScanControl.Process();
            Cursor.Current = Cursors.Arrow;

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            hideAllControls(channelScanControl);
            currentControl = channelScanControl;

            bar5ToolStripMenuItem.Visible = true;
            channelScanResultsToolStripMenuItem.Visible = true;
        }

        private void generalHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("General help menu option selected");

            Process process = new Process();

            string fileName = Path.Combine(RunParameters.BaseDirectory, "EPG Collector.chm");

            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = fileName;
            try
            {
                process.Start();
                Logger.Instance.Write("Help process started with file " + fileName);
            }
            catch (Exception ex)
            {
                Logger.Instance.Write("Help process failed to start for file " + fileName);
                Logger.Instance.Write(ex.Message);
                MessageBox.Show("Unable to open " + fileName + Environment.NewLine + Environment.NewLine + ex.Message);
            }
        }

        private void aboutEPGCollectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("About menu option selected");

            About about = new About();
            about.ShowDialog();
        }

        private void historyViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to History view");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            hideAllControls(viewHistoryControl);
            viewControl = viewHistoryControl;

            this.Text = (viewHistoryControl.Tag as ControlStatus).Heading;
        }

        private void logViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to General log view");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(viewLogControl.FindFilterAvailability);            

            hideAllControls(viewLogControl);
            viewControl = viewLogControl;

            this.Text = (viewLogControl.Tag as ControlStatus).Heading;
        }

        private void satIPViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Sat>IP log view");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            hideAllControls(viewSatIPControl);
            viewControl = viewSatIPControl;

            this.Text = (viewSatIPControl.Tag as ControlStatus).Heading;
        }

        private void collectorParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Collection Parameters view");

            changeSaveAvailability(!collectorParametersControl.NewFile, true);
            changeFindFilterAvailability(false); 

            hideAllControls(collectorParametersControl);
            currentControl = collectorParametersControl;

            this.Text = (collectorParametersControl.Tag as ControlStatus).Heading;
        }

        private void pluginParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Plugin Parameters view");

            changeSaveAvailability(!pluginParametersControl.NewFile, true);
            changeFindFilterAvailability(false);

            hideAllControls(pluginParametersControl);
            currentControl = pluginParametersControl;

            this.Text = (pluginParametersControl.Tag as ControlStatus).Heading;
        }

        private void programContentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to EIT Program Categories view");

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false); 

            hideAllControls(changeProgramContentControl);
            currentControl = changeProgramContentControl;

            this.Text = (changeProgramContentControl.Tag as ControlStatus).Heading;
        }

        private void programCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to OpenTV Program Categories view");

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeProgramCategoryControl);
            currentControl = changeProgramCategoryControl;

            this.Text = (changeProgramCategoryControl.Tag as ControlStatus).Heading;
        }

        private void mediaHighwayCategoryDescriptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to MediaHighway Program Categories view");

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeMHWCategoriesControl);
            currentControl = changeMHWCategoriesControl;

            this.Text = (changeMHWCategoriesControl.Tag as ControlStatus).Heading;
        }

        private void psipCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to PSIP Program Categories view");

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changePSIPCategoriesControl);
            currentControl = changePSIPCategoriesControl;

            this.Text = (changePSIPCategoriesControl.Tag as ControlStatus).Heading;
        }

        private void dishNetworkCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Dish Network Program Categories view");

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeDishNetworkCategoriesControl);
            currentControl = changeDishNetworkCategoriesControl;

            this.Text = (changeDishNetworkCategoriesControl.Tag as ControlStatus).Heading;
        }

        private void bellTVCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Bell TV Program Categories view");

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeBellTVCategoriesControl);
            currentControl = changeBellTVCategoriesControl;

            this.Text = (changeBellTVCategoriesControl.Tag as ControlStatus).Heading;
        }   

        private void customCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Custom Program Categories view");

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeCustomCategoriesControl);
            currentControl = changeCustomCategoriesControl;

            this.Text = (changeCustomCategoriesControl.Tag as ControlStatus).Heading;
        }

        private void xmltvCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to XMLTV Program Categories view");

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeXMLTVCategoriesControl);
            currentControl = changeXMLTVCategoriesControl;

            this.Text = (changeXMLTVCategoriesControl.Tag as ControlStatus).Heading;
        }

        private void sdCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Schedules Direct Program Categories view");

            changeSaveAvailability(true, false);
            changeFindFilterAvailability(false);

            hideAllControls(changeSdCategoriesControl);
            currentControl = changeSdCategoriesControl;

            this.Text = (changeSdCategoriesControl.Tag as ControlStatus).Heading;
        } 

        private void outputFileUnformattedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Output File Unformatted view");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(outputFileUnformattedControl.FindFilterAvailability); 

            hideAllControls(outputFileUnformattedControl);
            viewControl = outputFileUnformattedControl;

            this.Text = (outputFileUnformattedControl.Tag as ControlStatus).Heading;
        }

        private void outputFileFormattedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Output File Formatted view");
        }

        private void runCollectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Run Collection view");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false); 

            hideAllControls(runCollectionControl);

            this.Text = (runCollectionControl.Tag as ControlStatus).Heading;
        }

        private void findEPGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Find EPG view");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            findEPGControl.ViewResults();
            hideAllControls(findEPGControl);

            this.Text = (findEPGControl.Tag as ControlStatus).Heading;
        }

        private void tsDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Transport Stream Dump view");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            hideAllControls(tsDumpControl);

            this.Text = (tsDumpControl.Tag as ControlStatus).Heading;
        }

        private void tsAnalyzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Transport Stream Analysis view");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            tsAnalyzeControl.ViewResults();
            hideAllControls(tsAnalyzeControl);

            this.Text = (tsAnalyzeControl.Tag as ControlStatus).Heading;
        }

        private void channelScanResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Channel Scan Results view");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            channelScanControl.ViewResults();
            hideAllControls(channelScanControl);

            this.Text = (channelScanControl.Tag as ControlStatus).Heading;
        }

        private void windowsMediaCenterMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logger.Instance.Write("Changing to Windows Media Center Maintenance view");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            hideAllControls(runWMCMaintenanceControl);

            this.Text = (runWMCMaintenanceControl.Tag as ControlStatus).Heading;
        }

        private void sizeChanged(object sender, EventArgs e)
        {
            if (latestControl != null)
                positionControl(latestControl, latestParent);            
        }

        private void positionControl(Control control, bool parentToMainWindow)
        {
            if (parentToMainWindow)
            {
                control.Parent = mainWindow;
                mainPanel.Hide();

                control.Location = new Point(0, menuStrip1.Height + toolStrip.Height);
                control.Height = this.ClientSize.Height - (menuStrip1.Height + toolStrip.Height);
                control.Width = this.ClientSize.Width;
            }
            else
            {
                control.Parent = mainPanel;
                mainPanel.Show();

                control.Location = new Point(0, 0);
                control.Height = standardHeight;
                control.Width = standardWidth;
            }

            control.Visible = true;

            latestControl = control;
            latestParent = parentToMainWindow;
        }

        private void hideAllControls(Control control)
        {
            if (collectorParametersControl != null)
            {
                if (collectorParametersControl == control)
                    positionControl(control, false);
                else
                    collectorParametersControl.Visible = false;
            }

            if (pluginParametersControl != null)
            {
                if (pluginParametersControl == control)
                    positionControl(control, false);
                else
                    pluginParametersControl.Visible = false;
            }

            if (changeProgramContentControl != null)
            {
                if (changeProgramContentControl == control)
                    positionControl(control, true);
                else
                    changeProgramContentControl.Visible = false;
            }

            if (changeProgramCategoryControl != null)
            {
                if (changeProgramCategoryControl == control)
                    positionControl(control, true);
                else
                    changeProgramCategoryControl.Visible = false;
            }

            if (changeMHWCategoriesControl != null)
            {
                if (changeMHWCategoriesControl == control)
                    positionControl(control, true);
                else
                    changeMHWCategoriesControl.Visible = false;
            }

            if (changePSIPCategoriesControl != null)
            {
                if (changePSIPCategoriesControl == control)
                    positionControl(control, true);
                else
                    changePSIPCategoriesControl.Visible = false;
            }

            if (changeDishNetworkCategoriesControl != null)
            {
                if (changeDishNetworkCategoriesControl == control)
                    positionControl(control, true);
                else
                    changeDishNetworkCategoriesControl.Visible = false;
            }

            if (changeBellTVCategoriesControl != null)
            {
                if (changeBellTVCategoriesControl == control)
                    positionControl(control, true);
                else
                    changeBellTVCategoriesControl.Visible = false;
            }

            if (changeCustomCategoriesControl != null)
            {
                if (changeCustomCategoriesControl == control)
                    positionControl(control, true);
                else
                    changeCustomCategoriesControl.Visible = false;
            }

            if (changeXMLTVCategoriesControl != null)
            {
                if (changeXMLTVCategoriesControl == control)
                    positionControl(control, true);
                else
                    changeXMLTVCategoriesControl.Visible = false;
            }

            if (changeSdCategoriesControl != null)
            {
                if (changeSdCategoriesControl == control)
                    positionControl(control, true);
                else
                    changeSdCategoriesControl.Visible = false;
            }

            if (viewHistoryControl != null)
            {
                if (viewHistoryControl == control)
                    positionControl(control, true);
                else
                    viewHistoryControl.Visible = false;
            }

            if (viewLogControl != null)
            {
                if (viewLogControl == control)
                    positionControl(control, true);
                else
                    viewLogControl.Visible = false;
            }

            if (viewSatIPControl != null)
            {
                if (viewSatIPControl == control)
                    positionControl(control, true);
                else
                    viewSatIPControl.Visible = false;
            }

            if (outputFileUnformattedControl != null)
            {
                if (outputFileUnformattedControl == control)
                    positionControl(control, true);
                else
                    outputFileUnformattedControl.Visible = false;
            }

            if (runCollectionControl != null)
            {
                if (runCollectionControl == control)
                    positionControl(control, true);
                else
                    runCollectionControl.Visible = false;
            }

            if (findEPGControl != null)
            {
                if (findEPGControl == control)
                    positionControl(control, false);
                else
                    findEPGControl.Visible = false;
            }

            if (tsDumpControl != null)
            {
                if (tsDumpControl == control)
                    positionControl(control, false);
                else
                    tsDumpControl.Visible = false;
            }

            if (tsAnalyzeControl != null)
            {
                if (tsAnalyzeControl == control)
                    positionControl(control, false);
                else
                    tsAnalyzeControl.Visible = false;
            }

            if (channelScanControl != null)
            {
                if (channelScanControl == control)
                    positionControl(control, false);
                else
                    channelScanControl.Visible = false;
            }

            if (runWMCMaintenanceControl != null)
            {
                if (runWMCMaintenanceControl == control)
                    positionControl(control, true);
                else
                    runWMCMaintenanceControl.Visible = false;
            }
        }

        private void changeSaveAvailability(bool saveAvailability, bool saveAsAvailability)
        {
            Logger.Instance.Write("Save availability changed to " + saveAvailability + ":" + saveAsAvailability);

            saveToolStripMenuItem.Enabled = saveAvailability;
            saveAsToolStripMenuItem.Enabled = saveAsAvailability;

            saveToolStripButton.Enabled = saveAvailability;
            saveAsToolStripButton.Enabled = saveAsAvailability;
        }

        private void changeFindFilterAvailability(bool availability)
        {
            findTextToolStripMenuItem.Enabled = availability;
            filterTextToolStripMenuItem.Enabled = availability;

            findTextToolStripButton.Enabled = availability;
            filterTextToolStripButton.Enabled = availability;
        }

        private bool checkSaveAllFiles()
        {
            bool proceed = checkSaveFile(collectorParametersControl, "Collection Parameters");
            if (!proceed)
                return(false);

            proceed = checkSaveFile(pluginParametersControl, "Plugin Parameters");
            if (!proceed)
                return (false);            

            proceed = checkSaveFile(changeProgramContentControl, "EIT Program Categories");
            if (!proceed)
                return(false);

            proceed = checkSaveFile(changeProgramCategoryControl, "OpenTV Program Categories");
            if (!proceed)
                return (false);

            proceed = checkSaveFile(changeMHWCategoriesControl, "MediaHighway Program Categories");
            if (!proceed)
                return (false);

            proceed = checkSaveFile(changePSIPCategoriesControl, "PSIP Program Categories");
            if (!proceed)
                return (false);

            proceed = checkSaveFile(changeDishNetworkCategoriesControl, "Dish Network Program Categories");
            if (!proceed)
                return (false);

            proceed = checkSaveFile(changeBellTVCategoriesControl, "Bell TV Program Categories");
            if (!proceed)
                return (false);

            proceed = checkSaveFile(changeCustomCategoriesControl, "Custom Program Categories");
            if (!proceed)
                return (false);

            proceed = checkSaveFile(changeXMLTVCategoriesControl, "XMLTV Program Categories");
            if (!proceed)
                return (false);

            proceed = checkSaveFile(changeSdCategoriesControl, "Schedules Direct Program Categories");
            if (!proceed)
                return (false);

            return (true);
        }

        private bool checkSaveFile(IUpdateControl updateControl, string description)
        {
            Logger.Instance.Write("Checking if save needed for " + description);

            if (updateControl == null)
            {
                Logger.Instance.Write("No update control - ok to proceed");
                return (true);
            }

            switch (updateControl.DataState)
            {
                case DataState.HasErrors:
                    Logger.Instance.Write("Data errors - do not proceed");
                    return (false);
                case DataState.Changed:
                    Logger.Instance.Write("Data has changed - asking user for action");
                    DialogResult result = MessageBox.Show("Any changes to the " + description + " will be lost if they are not saved." + Environment.NewLine + Environment.NewLine +
                        "Do you want to cancel the current action and save them?", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    Logger.Instance.Write(result == DialogResult.Yes ? "Do not proceed - save requested" : "OK to proceed - no save requested" );
                    return (result != DialogResult.Yes);
                case DataState.NotChanged:
                    Logger.Instance.Write("Data has not changed - ok to proceed");
                    return (true);
                default:
                    Logger.Instance.Write("Data state unknown - ok to proceed");
                    return (true);
            }
        }

        private string getParameterFile(string description)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "INI Files (*.ini)|*.ini";
            if (currentRunPath == null)
                openFile.InitialDirectory = RunParameters.DataDirectory;
            else
                openFile.InitialDirectory = currentRunPath;
            openFile.RestoreDirectory = true;
            openFile.Title = "Locate EPG Collection Parameter File For " + description;

            DialogResult result = openFile.ShowDialog();
            if (result == DialogResult.Cancel)
                return (null);
            else
            {
                currentRunPath = new FileInfo(openFile.FileName).DirectoryName;
                return (openFile.FileName);
            }
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            if (updateChecksDone)
                return;

            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = !mainWindow.fileToolStripMenuItem.Enabled;

            if (!e.Cancel)
                e.Cancel = !checkSaveAllFiles();

            if (!e.Cancel)
                RunParameters.Instance.CloseHttpProxy();

            if (!e.Cancel)
                Logger.Instance.Write("EPG Centre closing down");
        }

        internal static void ParentToPanel(Control control)
        {
            mainWindow.positionControl(control, false);
        }

        internal static void ParentToWindow(Control control)
        {
            mainWindow.positionControl(control, true);
        }

        private string getDVBLinkPluginPath()
        {
            if (currentPluginIniPath != null)
                return (currentPluginIniPath);

            string dvblinkPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\DVBLogic\DVBLink\Sources";
            if (Directory.Exists(dvblinkPath))
            {
                string[] directories = Directory.GetDirectories(dvblinkPath);
                if (directories.Length == 1)
                    return (directories[0]);
                else
                    return (dvblinkPath);
            }
            else
                return (null);
        }

        private void dVBSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDvbs selectDvbs = new SelectDvbs();
            DialogResult result = selectDvbs.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            ChangeDvbsDetails changeDvbs = new ChangeDvbsDetails();

            changeDvbs.Initialize(selectDvbs.Satellite, selectDvbs.Frequency);

            result = changeDvbs.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            if (collectorParametersControl != null)
                collectorParametersControl.UpdateSelectedFrequency(selectDvbs.Frequency);
        }

        private void dVBtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDvbt selectDvbt = new SelectDvbt();
            DialogResult result = selectDvbt.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            ChangeDvbtDetails changeDvbt = new ChangeDvbtDetails();

            changeDvbt.Initialize(selectDvbt.Country, selectDvbt.Area, selectDvbt.Frequency);

            result = changeDvbt.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            if (collectorParametersControl != null)
                collectorParametersControl.UpdateSelectedFrequency(selectDvbt.Frequency);
        }

        private void dVBCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectDvbc selectDvbc = new SelectDvbc();
            DialogResult result = selectDvbc.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            ChangeDvbcDetails changeDvbc = new ChangeDvbcDetails();

            changeDvbc.Initialize(selectDvbc.Provider, selectDvbc.Frequency);

            result = changeDvbc.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            if (collectorParametersControl != null)
                collectorParametersControl.UpdateSelectedFrequency(selectDvbc.Frequency);
        }

        private void aTSCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAtsc selectAtsc = new SelectAtsc();
            DialogResult result = selectAtsc.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            ChangeAtscDetails changeAtsc = new ChangeAtscDetails();

            changeAtsc.Initialize(selectAtsc.Provider, selectAtsc.Frequency);

            result = changeAtsc.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            if (collectorParametersControl != null)
                collectorParametersControl.UpdateSelectedFrequency(selectAtsc.Frequency);
        }

        private void clearQAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectClearQam selectClearQam = new SelectClearQam();
            DialogResult result = selectClearQam.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            ChangeClearQamDetails changeClearQam = new ChangeClearQamDetails();

            changeClearQam.Initialize(selectClearQam.Provider, selectClearQam.Frequency);

            result = changeClearQam.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            if (collectorParametersControl != null)
                collectorParametersControl.UpdateSelectedFrequency(selectClearQam.Frequency);
        }

        private void iSDBSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIsdbs selectIsdbs = new SelectIsdbs();
            DialogResult result = selectIsdbs.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            ChangeIsdbsDetails changeIsdbs = new ChangeIsdbsDetails();

            changeIsdbs.Initialize(selectIsdbs.Satellite, selectIsdbs.Frequency);

            result = changeIsdbs.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            if (collectorParametersControl != null)
                collectorParametersControl.UpdateSelectedFrequency(selectIsdbs.Frequency);
        }

        private void iSDBTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectIsdbt selectIsdbt = new SelectIsdbt();
            DialogResult result = selectIsdbt.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            ChangeIsdbtDetails changeIsdbt = new ChangeIsdbtDetails();

            changeIsdbt.Initialize(selectIsdbt.Provider, selectIsdbt.Frequency);

            result = changeIsdbt.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            if (collectorParametersControl != null)
                collectorParametersControl.UpdateSelectedFrequency(selectIsdbt.Frequency);
        }

        private void useSatIPTunersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool wasEnabled = SatIpConfiguration.SatIpEnabled;

            SatIpConfiguration configuration = new SatIpConfiguration();
            configuration.Load();

            ConfigureSatIp configureSatIp = new ConfigureSatIp(configuration);
            DialogResult result = configureSatIp.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            if (!wasEnabled)
            {
                if (!SatIpConfiguration.SatIpEnabled)
                    return;
                else
                {
                    Cursor.Current = Cursors.WaitCursor;

                    clearSatIPLogToolStripMenuItem.Visible = true;
                    satIPLogToolStripMenuItem.Visible = true;

                    SatIpServer.LoadServers();

                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                if (SatIpConfiguration.SatIpEnabled)
                    return;
                else
                {
                    if (!VBoxConfiguration.VBoxEnabled)
                    {
                        clearSatIPLogToolStripMenuItem.Visible = false;
                        satIPLogToolStripMenuItem.Visible = false;
                    }

                    Collection<Tuner> deletedTuners = new Collection<Tuner>();

                    foreach (Tuner tuner in Tuner.TunerCollection)
                    {
                        if (tuner.IsSatIpTuner)
                            deletedTuners.Add(tuner);
                    }

                    foreach (Tuner deletedTuner in deletedTuners)
                        Tuner.TunerCollection.Remove(deletedTuner);
                }
            }
        }

        private void useVBoxTunersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool wasEnabled = VBoxConfiguration.VBoxEnabled;

            VBoxConfiguration configuration = new VBoxConfiguration();
            configuration.Load();

            ConfigureVBox configureVBox = new ConfigureVBox(configuration);
            DialogResult result = configureVBox.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            if (!wasEnabled)
            {
                if (!VBoxConfiguration.VBoxEnabled)
                    return;
                else
                {
                    Cursor.Current = Cursors.WaitCursor;

                    clearSatIPLogToolStripMenuItem.Visible = true;
                    satIPLogToolStripMenuItem.Visible = true;

                    VBoxTuner.LoadServers();

                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                if (VBoxConfiguration.VBoxEnabled)
                    return;
                else
                {
                    if (!SatIpConfiguration.SatIpEnabled)
                    {
                        clearSatIPLogToolStripMenuItem.Visible = false;
                        satIPLogToolStripMenuItem.Visible = false;
                    }

                    Collection<Tuner> deletedTuners = new Collection<Tuner>();

                    foreach (Tuner tuner in Tuner.TunerCollection)
                    {
                        if (tuner.IsVBoxTuner)
                            deletedTuners.Add(tuner);
                    }

                    foreach (Tuner deletedTuner in deletedTuners)
                        Tuner.TunerCollection.Remove(deletedTuner);
                }
            }
        }

        private void configureSchedulesDirectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeSdLineups changeSdLineups = new ChangeSdLineups();
            ReplyBase reply = changeSdLineups.Initialize();
            if (reply.Message != null)
                MessageBox.Show("Failed to initialize Schedules Direct service." + Environment.NewLine + Environment.NewLine +
                    reply.Message, "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Error);

            changeSdLineups.ShowDialog();
        }

        private void removeChannelLogosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Please confirm that all channel logos are to be removed.", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            if (runWMCMaintenanceControl == null)
                runWMCMaintenanceControl = new RunWMCMaintenanceControl();

            hideAllControls(runWMCMaintenanceControl);

            Logger.Instance.Write("Running WMC maintenance control");

            this.Text = "EPG Centre - Windows Media Center Maintenance - Remove Channel Logos";
            runWMCMaintenanceControl.Tag = new ControlStatus(this.Text);

            runWMCMaintenanceControl.Process("REMOVECHANNELLOGOS");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            bar6ToolStripMenuItem.Visible = true;
            windowsMediaCenterMaintenanceToolStripMenuItem.Visible = true;
        }

        private void removeAffiliatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Please confirm that all affiliates are to be removed.", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            if (runWMCMaintenanceControl == null)
                runWMCMaintenanceControl = new RunWMCMaintenanceControl();

            hideAllControls(runWMCMaintenanceControl);

            Logger.Instance.Write("Running WMC maintenance control");

            this.Text = "EPG Centre - Windows Media Center Maintenance - Remove Affiliates";
            runWMCMaintenanceControl.Tag = new ControlStatus(this.Text);

            runWMCMaintenanceControl.Process("REMOVEAFFILIATES=");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            bar6ToolStripMenuItem.Visible = true;
            windowsMediaCenterMaintenanceToolStripMenuItem.Visible = true;
        }

        private void clearProgrammeGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Please confirm that the programme guide is to be cleared." + Environment.NewLine + Environment.NewLine +
                "All channels will remain but they will have no data available.", "EPG Centre", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            result = MessageBox.Show("This process deletes and recreates the Windows Media Center database." + Environment.NewLine + Environment.NewLine +
                "Please ensure that Windows Media Center is unloaded and any Media Center tasks terminated using Task Manager.", "EPG Centre", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.Cancel)
                return;

            string wmcProcesses = string.Empty;
            while (wmcProcesses != null)
            {
                wmcProcesses = getWmcProcesses();
                if (wmcProcesses != null)
                    MessageBox.Show("The following Windows Media Center processes are still running." + 
                        Environment.NewLine + Environment.NewLine + 
                        wmcProcesses +
                        Environment.NewLine + Environment.NewLine +
                        "Please unload Windows Media Center or terminate them using Task Manager.", "EPG Centre", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

            if (runWMCMaintenanceControl == null)
                runWMCMaintenanceControl = new RunWMCMaintenanceControl();

            hideAllControls(runWMCMaintenanceControl);

            Logger.Instance.Write("Running WMC maintenance control");

            this.Text = "EPG Centre - Windows Media Center Maintenance - Clear Programme Guide";
            runWMCMaintenanceControl.Tag = new ControlStatus(this.Text);

            runWMCMaintenanceControl.Process("CLEARGUIDE");

            changeSaveAvailability(false, false);
            changeFindFilterAvailability(false);

            bar6ToolStripMenuItem.Visible = true;
            windowsMediaCenterMaintenanceToolStripMenuItem.Visible = true;
        }

        private string getWmcProcesses()
        {
            string reply = null;

            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    if (process.MainModule != null && process.MainModule.FileVersionInfo != null && process.MainModule.FileVersionInfo.FileDescription != null)
                    {
                        Logger.Instance.Write("Process running: " + process.ToString() + " : " + process.MainModule.FileVersionInfo.FileDescription);

                    
                            if (process.MainModule.FileVersionInfo.FileDescription.Contains("Windows Media Center"))
                            {
                                if (reply == null)
                                    reply = process.MainModule.FileVersionInfo.FileDescription + "(" + process.MainModule.ModuleName + ")";
                                else
                                {
                                    reply += Environment.NewLine;
                                    reply += process.MainModule.FileVersionInfo.FileDescription + "(" + process.MainModule.ModuleName + ")";
                                }
                            }
                    }
                }
                catch (Exception e)
                {
                    Logger.Instance.Write("<e> Cannot check process  " + process.ToString());
                    Logger.Instance.Write("<e> " + e.Message);
                }
            }

            return reply;
        }

        private void createDebugPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDebugPackage createDebugPackage = new CreateDebugPackage();
            createDebugPackage.ShowDialog();
        }
    }
}

