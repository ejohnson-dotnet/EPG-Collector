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
    internal partial class RunCollectionControl : UserControl
    {
        private delegate void SetListViewEntry(string entryType, string detail);
        private delegate void DisableControls(int exitCode);

        private Process collectionProcess;
        
        private string runReference;
        private Mutex cancelMutex;

        private string informationText = "Information";
        private string exceptionText = "Exception";
        private string errorText = "Error";
        private string completedText = "Completed";
        private string summaryText = "Summary";
        private string warningText = "Warning";

        private InProgress inProgress;

        internal RunCollectionControl()
        {
            InitializeComponent();
        }

        internal void Process(string fileName)
        {
            MainWindow.ChangeMenuItemAvailability(false);            

            DateTime now = DateTime.Now;
            runReference = now.DayOfYear.ToString() + now.TimeOfDay.Hours.ToString() + now.TimeOfDay.Minutes.ToString() + now.TimeOfDay.Seconds.ToString();
            cancelMutex = new Mutex(true, "EPG Collector Cancel Mutex " + runReference);
            
            dgViewLog.Rows.Clear();

            collectionProcess = new Process();

            collectionProcess.StartInfo.FileName = Path.Combine(RunParameters.BaseDirectory, "EPGCollector.exe");
            collectionProcess.StartInfo.WorkingDirectory = RunParameters.BaseDirectory;

            string monoString = RunParameters.IsMono ? @" /mono" : string.Empty;
            string dummyTunersString = CommandLine.DummyTuners ? @" /dummytuners" : string.Empty;
            collectionProcess.StartInfo.Arguments = @"/ini=" + '"' + fileName + '"' + 
                " /background=" + runReference + 
                monoString + 
                dummyTunersString;
            
            collectionProcess.StartInfo.UseShellExecute = false;
            collectionProcess.StartInfo.CreateNoWindow = true;
            collectionProcess.StartInfo.RedirectStandardOutput = true;
            collectionProcess.EnableRaisingEvents = true;
            collectionProcess.OutputDataReceived += new DataReceivedEventHandler(collectionProcessOutputDataReceived);
            collectionProcess.Exited += new EventHandler(collectionProcessExited);

            collectionProcess.Start();

            collectionProcess.BeginOutputReadLine();

            inProgress = new InProgress("Collecting EPG");
            inProgress.Cancelled += new InProgress.CancelledHandler(inProgressCancelled);
            inProgress.Show();            
        }     

        private void collectionProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
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

        private void collectionProcessExited(object sender, EventArgs e)
        {
            int exitCode = collectionProcess.ExitCode;            
            collectionProcess.Close();

            if (!dgViewLog.InvokeRequired)
                disableControls(exitCode);
            else
                dgViewLog.Invoke(new DisableControls(disableControls), exitCode);

            cancelMutex.Close();
            cancelMutex = null;
        }

        private void inProgressCancelled(object sender, EventArgs e)
        {
            Logger.Instance.Write("Stop collection requested");

            inProgress = null;
            if (cancelMutex != null)
                cancelMutex.ReleaseMutex();            
        }
        
        private void disableControls(int exitCode)
        {
            MainWindow.ChangeMenuItemAvailability(true);
            
            if (inProgress != null)
                inProgress.Hide();

            MessageBox.Show("The collection process has completed with exit code " + exitCode + Environment.NewLine + Environment.NewLine +
                CommandLine.GetCompletionCodeDescription((ExitCode)exitCode),
                "EPG Centre", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (inProgress != null)
                inProgress.CloseWithNoEvents();     
        }
    }
}

