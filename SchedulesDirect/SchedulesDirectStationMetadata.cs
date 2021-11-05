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
using System.Runtime.Serialization;

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes the Schedules Direct station metadata.
    /// </summary>
    [DataContract]
    public class SchedulesDirectStationMetadata
    {
        /// <summary>
        /// Get or set the lineup containing the station.
        /// </summary>
        [DataMember(Name = "lineup")]
        public string Lineup { get; set; }

        /// <summary>
        /// Get the date of the last modification.
        /// </summary>
        [DataMember(Name = "modified")]
        public DateTime Modified { get; set; }

        /// <summary>
        /// Get the transport description.
        /// </summary>
        [DataMember(Name = "transport")]
        public string Transport { get; set; }

        /// <summary>
        /// Get the modulation used by the station.
        /// </summary>
        [DataMember(Name = "modulation")]
        public string Modulation { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectStationMetadata class.
        /// </summary>
        public SchedulesDirectStationMetadata() { }
    }
}

