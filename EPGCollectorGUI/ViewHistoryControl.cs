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
using System.Windows.Forms;

using DomainObjects;

namespace EPGCentre
{
    internal partial class ViewHistoryControl : UserControl, IView
    {
        private Collection<HistoryRecord> records;

        private Logger logger;

        private string sortedColumnName;
        private bool sortedAscending;

        internal ViewHistoryControl()
        {
            InitializeComponent();
        }

        internal void Process(Logger logger)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.logger = logger;

            records = new Collection<HistoryRecord>();

            string line;

            do
            {
                line = logger.Read();
                if (line != null)
                    records.Insert(0, processLine(line));
            }
            while (line != null);

            logger.Close();

            dgViewHistory.Rows.Clear();
            dgViewHistory.VirtualMode = true;
            dgViewHistory.CellValueNeeded += new DataGridViewCellValueEventHandler(cellValueNeeded);
            dgViewHistory.RowCount = records.Count;

            Cursor.Current = Cursors.Arrow;
        }

        private void cellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= records.Count)
            {
                e.Value = string.Empty;
                return;
            }

            switch (dgViewHistory.Columns[e.ColumnIndex].Name)
            {
                case "dateColumn":
                    e.Value = records[e.RowIndex].StartDate.ToShortDateString();
                    break;
                case "timeColumn":
                    e.Value = records[e.RowIndex].StartDate.ToShortTimeString();
                    break;
                case "collectionResultColumn":
                    e.Value = records[e.RowIndex].CollectionResult;

                    if ((string)e.Value != "OK")
                    {
                        dgViewHistory.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                        dgViewHistory.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
                    }
                    else
                    {
                        dgViewHistory.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        dgViewHistory.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    break;
                case "collectionTimeColumn":
                    if (records[e.RowIndex].CollectionDuration != null)
                    {
                        TimeSpan duration = records[e.RowIndex].CollectionDuration.Value;
                        TimeSpan timeSpan = new TimeSpan(duration.Hours, duration.Minutes, duration.Seconds);
                        e.Value = timeSpan.ToString();
                    }
                    break;
                case "collectionCountColumn":
                    if (records[e.RowIndex].CollectionCount != 0)
                        e.Value = records[e.RowIndex].CollectionCount.ToString();                    
                    break;
                case "lookupResultColumn":
                    if (!string.IsNullOrWhiteSpace(records[e.RowIndex].LookupResult))
                        e.Value = records[e.RowIndex].LookupResult;
                    break;
                case "lookupTimeColumn":
                    if (records[e.RowIndex].LookupDuration != null)
                    {
                        TimeSpan duration = records[e.RowIndex].LookupDuration.Value;
                        TimeSpan timeSpan = new TimeSpan(duration.Hours, duration.Minutes, duration.Seconds);
                        e.Value = timeSpan.ToString();
                    }
                    break;
                case "lookupRateColumn":
                    if (records[e.RowIndex].LookupRate != -1)
                        e.Value = records[e.RowIndex].LookupRate.ToString();
                    break;
                case "softwareVersionColumn":
                    e.Value = records[e.RowIndex].SoftwareVersion;
                    break;
                default:
                    break;
            }

            dgViewHistory.Rows[e.RowIndex].Height = 16;
        }

        private HistoryRecord processLine(string line)
        {
            HistoryRecord historyRecord = new HistoryRecord(line);
            return (historyRecord);
        }

        private void columnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sortedColumnName == null)
            {
                sortedAscending = false;
                sortedColumnName = dgViewHistory.Columns[e.ColumnIndex].Name;
            }
            else
            {
                if (sortedColumnName == dgViewHistory.Columns[e.ColumnIndex].Name)
                    sortedAscending = !sortedAscending;
                else
                    sortedColumnName = dgViewHistory.Columns[e.ColumnIndex].Name;
            }

            Collection<HistoryRecord> sortedContents = new Collection<HistoryRecord>();

            foreach (HistoryRecord historyRecord in records)
            {
                switch (dgViewHistory.Columns[e.ColumnIndex].Name)
                {
                    case "dateColumn":
                        addInOrder(sortedContents, historyRecord, sortedAscending, "Date");
                        break;
                    case "timeColumn":
                        addInOrder(sortedContents, historyRecord, sortedAscending, "Time");
                        break;
                    case "collectionResultColumn":
                        addInOrder(sortedContents, historyRecord, sortedAscending, "CollectionResult");
                        break;
                    case "collectionCountColumn":
                        addInOrder(sortedContents, historyRecord, sortedAscending, "CollectionCount");
                        break;
                    case "collectionTimeColumn":
                        addInOrder(sortedContents, historyRecord, sortedAscending, "CollectionTime");
                        break;
                    case "lookupResultColumn":
                        addInOrder(sortedContents, historyRecord, sortedAscending, "LookupResult");
                        break;
                    case "lookupRateColumn":
                        addInOrder(sortedContents, historyRecord, sortedAscending, "LookupRate");
                        break;
                    case "lookupTimeColumn":
                        addInOrder(sortedContents, historyRecord, sortedAscending, "LookupTime");
                        break;
                    case "softwareVersionColumn":
                        addInOrder(sortedContents, historyRecord, sortedAscending, "SoftwareVersion");
                        break;
                    default:
                        return;
                }
            }

            records = sortedContents;
            dgViewHistory.Refresh();
        }

        private void addInOrder(Collection<HistoryRecord> sortedContents, HistoryRecord historyRecord, bool sortedAscending, string columnName)
        {
            foreach (HistoryRecord oldRecord in sortedContents)
            {
                int condition = checkKeys(oldRecord, historyRecord, sortedAscending, columnName);

                if (condition > 0)
                {
                    sortedContents.Insert(sortedContents.IndexOf(oldRecord), historyRecord);
                    return;
                }
            }

            sortedContents.Add(historyRecord);
        }

        private int checkKeys(HistoryRecord oldRecord, HistoryRecord newRecord, bool sortedAscending, string columnName)
        {
            switch (columnName)
            {
                case "Date":
                    if (sortedAscending)
                        return (oldRecord.StartDate.CompareTo(newRecord.StartDate));
                    else
                        return (newRecord.StartDate.CompareTo(oldRecord.StartDate));
                case "Time":
                    if (sortedAscending)
                        return (oldRecord.StartDate.TimeOfDay.CompareTo(newRecord.StartDate.TimeOfDay));
                    else
                        return (newRecord.StartDate.TimeOfDay.CompareTo(oldRecord.StartDate.TimeOfDay));
                case "CollectionResult":
                    if (sortedAscending)
                        return (oldRecord.CollectionResult.CompareTo(newRecord.CollectionResult));
                    else
                        return (newRecord.CollectionResult.CompareTo(oldRecord.CollectionResult));
                case "CollectionCount":
                    if (sortedAscending)
                        return (oldRecord.CollectionCount.CompareTo(newRecord.CollectionCount));
                    else
                        return (newRecord.CollectionCount.CompareTo(oldRecord.CollectionCount));
                case "CollectionTime":
                    if (sortedAscending)
                    {
                        if (oldRecord.CollectionDuration != null)
                        {
                            if (newRecord.CollectionDuration != null)
                                return (oldRecord.CollectionDuration.Value.CompareTo(newRecord.CollectionDuration.Value));
                            else
                                return (-1);
                        }
                        else
                        {
                            if (newRecord.CollectionDuration != null)
                                return (1);
                            else
                                return (0);
                        }

                    }
                    else
                    {
                        if (newRecord.CollectionDuration != null)
                        {
                            if (oldRecord.CollectionDuration != null)
                                return (newRecord.CollectionDuration.Value.CompareTo(oldRecord.CollectionDuration.Value));
                            else
                                return (-1);
                        }
                        else
                        {
                            if (oldRecord.CollectionDuration != null)
                                return (1);
                            else
                                return (0);
                        }
                    }
                case "LookupResult":
                    if (sortedAscending)
                        return (oldRecord.LookupResult.CompareTo(newRecord.LookupResult));
                    else
                        return (newRecord.LookupResult.CompareTo(oldRecord.LookupResult));
                case "LookupRate":
                    if (sortedAscending)
                        return (oldRecord.LookupRate.CompareTo(newRecord.LookupRate));
                    else
                        return (newRecord.LookupRate.CompareTo(oldRecord.LookupRate));
                case "LookupTime":
                    if (sortedAscending)
                    {
                        if (oldRecord.LookupDuration != null)
                        {
                            if (newRecord.LookupDuration != null)
                                return (oldRecord.LookupDuration.Value.CompareTo(newRecord.LookupDuration.Value));
                            else
                                return (-1);
                        }
                        else
                        {
                            if (newRecord.LookupDuration != null)
                                return (1);
                            else
                                return (0);
                        }

                    }
                    else
                    {
                        if (newRecord.LookupDuration != null)
                        {
                            if (oldRecord.LookupDuration != null)
                                return (newRecord.LookupDuration.Value.CompareTo(oldRecord.LookupDuration.Value));
                            else
                                return (-1);
                        }
                        else
                        {
                            if (oldRecord.LookupDuration != null)
                                return (1);
                            else
                                return (0);
                        }
                    }
                case "SoftwareVersion":
                    if (sortedAscending)
                        return (oldRecord.SoftwareVersion.CompareTo(newRecord.SoftwareVersion));
                    else
                        return (newRecord.SoftwareVersion.CompareTo(oldRecord.SoftwareVersion));
                default:
                    return (0);
            }
        }

        /// <summary>
        /// Filter the data.
        /// </summary>
        public void FilterText() { }

        /// <summary>
        /// Find a specific line.
        /// </summary>
        public void FindText() { }

        /// <summary>
        /// Clear the view.
        /// </summary>
        public void Clear()
        {
            dgViewHistory.Rows.Clear();
        }
    }
}

