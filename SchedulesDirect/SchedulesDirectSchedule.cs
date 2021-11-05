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

using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes a Schedules Direct programme schedule.
    /// </summary>
    [DataContract]
    public class SchedulesDirectSchedule
    {
        /// <summary>
        /// Get or set the station identifier.
        /// </summary>
        [DataMember(Name = "stationID")]
        public string StationId { get; set; }

        /// <summary>
        /// Get or set the list of programmes in the schedule.
        /// </summary>
        [DataMember(Name = "programs")]
        public Collection<SchedulesDirectProgram> Programmes { get; set; }

        /// <summary>
        /// Get or set the schedule metadata.
        /// </summary>
        [DataMember(Name = "metadata")]
        public SchedulesDirectScheduleMetadata Metadata { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectSchedule class.
        /// </summary>
        public SchedulesDirectSchedule() { }
    }
}

