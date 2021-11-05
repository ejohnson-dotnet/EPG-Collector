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
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

using DomainObjects;

namespace EPGCentre
{
    internal partial class RunWMCMaintenanceControl : UserControl
    {
        private delegate void SetListViewEntry(string entryType, string detail);
        private delegate void DisableControls(int exitCode);

        private Process maintenanceProcess;
        
        private string informationText = "Information";
        private string exceptionText = "Exception";
        private string errorText = "Error";
        private string completedText = "Completed";
        private string summaryText = "Summary";
        private string warningText = "Warning";

        internal RunWMCMaintenanceControl()
        {
            InitializeComponent();
        }

        internal void Process(string action)
        {
            MainWindow.ChangeMenuItemAvailability(false);            

            dgViewLog.Rows.Clear();

            maintenanceProcess = new Process();

            maintenanceProcess.StartInfo.FileName = Path.Combine(RunParameters.BaseDirectory, "WMCUtility.exe");
            maintenanceProcess.StartInfo.WorkingDirectory = RunParameters.BaseDirectory;

            maintenanceProcess.StartInfo.Arguments = action;
            
            maintenanceProcess.StartInfo.UseShellExecute = false;
            maintenanceProcess.StartInfo.CreateNoWindow = true;
            maintenanceProcess.StartInfo.RedirectStandardOutput = true;
            maintenanceProcess.EnableRaisingEvents = true;
            maintenanceProcess.OutputDataReceived += new DataReceivedEventHandler(maintenanceProcessOutputDataReceived);
            maintenanceProcess.Exited += new EventHandler(maintenanceProcessExited);

            maintenanceProcess.Start();

            maintenanceProcess.BeginOutputReadLine();
        }     

        private void maintenanceProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null || e.Data.Length < 14)
                return;

            string editedLine = e.Data.Replace("\u0009", "    ");

            string entryType;
            int detailOffset = 0;

            if (editedLine[0] == '<' && editedLine[2] == '>')
            {
                detailOffset = 4;

                switch (editedLine[1])
                {
                    case 'e':
                        entryType = errorText;
                        break;
                    case 'E':
                        entryType = exceptionText;
                        break;
                    case 'I':
                        entryType = informationText;
                        break;
                    case 'C':
                        entryType = completedText;
                        break;
                    case 'S':
                        entryType = summaryText;
                        break;
                    case 'w':
                        entryType = warningText;
                        break;
                    default:
                        entryType = informationText;
                        break;
                }
            }
            else
                entryType = "Information";

            string detail = editedLine.Substring(detailOffset);

            if (!dgViewLog.InvokeRequired)
                setListEntry(entryType, detail);
            else
                dgViewLog.Invoke(new SetListViewEntry(setListEntry), entryType, detail);                                
        }

        private void setListEntry(string entryType, string detail)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.Height = 16;

            DataGridViewCell typeCell = new DataGridViewTextBoxCell();
            typeCell.Value = entryType;
            row.Cells.Add(typeCell);

            DataGridViewCell detailCell = new DataGridViewTextBoxCell();
            detailCell.Value = detail;
            row.Cells.Add(detailCell);
            
            dgViewLog.Rows.Add(row);

            dgViewLog.FirstDisplayedScrollingRowIndex = dgViewLog.Rows.Count - 1;
        }

        private void maintenanceProcessExited(object sender, EventArgs e)
        {
            int exitCode = maintenanceProcess.ExitCode;            
            maintenanceProcess.Close();

            if (!dgViewLog.InvokeRequired)
                disableControls(exitCode);
            else
                dgViewLog.Invoke(new DisableControls(disableControls), exitCode);
        }

        private void disableControls(int exitCode)
        {
            MainWindow.ChangeMenuItemAvailability(true);
            
            MessageBox.Show("The maintenance process has completed with exit code " + exitCode + Environment.NewLine + Environment.NewLine +
                CommandLine.GetCompletionCodeDescription((ExitCode)exitCode),
                "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

