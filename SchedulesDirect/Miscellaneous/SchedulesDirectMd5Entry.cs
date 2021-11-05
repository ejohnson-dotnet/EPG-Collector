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

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes a Schedules Direct MD5 entry.
    /// </summary>
    public class SchedulesDirectMd5Entry
    {
        /// <summary>
        /// Get the station ID.
        /// </summary>
        public string StationId { get; private set; }
        
        /// <summary>
        /// Get the date.
        /// </summary>
        public DateTime Date { get; private set; }
        
        /// <summary>
        /// Get the MD5 checksum.
        /// </summary>
        public string Md5 { get; private set; }
        
        /// <summary>
        /// Get the last modified date.
        /// </summary>
        public DateTime LastModifiedDate { get; private set; }

        private SchedulesDirectMd5Entry() { }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectMd5Entry class.
        /// </summary>
        /// <param name="stationId">The station ID.</param>
        /// <param name="date">The date.</param>
        /// <param name="md5">The MD5 checksum.</param>
        /// <param name="lastModifiedDate">The last modified date.</param>
        public SchedulesDirectMd5Entry(string stationId, DateTime date, string md5, DateTime lastModifiedDate)
        {
            StationId = stationId;
            Date = date;
            Md5 = md5;
            LastModifiedDate = lastModifiedDate;
        }

        /// <summary>
        /// Determine if a specified MD5 entry is current.
        /// </summary>
        /// <param name="stationId">The station ID.</param>
        /// <param name="date">The date.</param>
        /// <param name="md5">The MD5 checksum.</param>
        /// <param name="lastModifiedDate">The last modified date.</param>
        /// <param name="md5Entries">The list of MD5 entries to check against.</param>
        /// <returns>True if the entry is current; false otherwise.</returns>
        public static bool IsCurrent(string stationId, DateTime date, string md5, DateTime lastModifiedDate, Collection<SchedulesDirectMd5Entry> md5Entries)
        {
            if (md5Entries == null)
                return false;

            foreach (SchedulesDirectMd5Entry md5Entry in md5Entries)
            {
                if (md5Entry.StationId == stationId && md5Entry.Date == date && md5Entry.Md5 == md5 && md5Entry.LastModifiedDate == lastModifiedDate)
                    return true;
            }

            return false;
        }
    }
}

