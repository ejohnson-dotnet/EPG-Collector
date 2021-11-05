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
using System.Runtime.Serialization;

using DomainObjects;

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes the Schedules Direct status response.
    /// </summary>
    [DataContract]
    public class SchedulesDirectStatusResponse : SchedulesDirectResponse
    { 
        /// <summary>
        /// Get or set the account information.
        /// </summary>
        [DataMember(Name = "account")]
        public SchedulesDirectAccount Account { get; set; }

        /// <summary>
        /// Get or set the list of lineups associated with the account.
        /// </summary>
        [DataMember(Name = "lineups")]
        public Collection<SchedulesDirectLineup> Lineups { get; set; }

        /// <summary>
        /// Get or set the number of lineup changes still available.
        /// </summary>
        [DataMember(Name = "lineupChangesRemaining")]
        public int LineupChangesRemaining { get; set; }

        /// <summary>
        /// Get the system status information.
        /// </summary>
        [DataMember(Name = "systemStatus")]
        public Collection<SchedulesDirectSystemStatus> SystemStatuses { get; set; }
        
        /// <summary>
        /// Get or set the date of the last modification.
        /// </summary>
        [DataMember(Name = "lastDataUpdate")]
        public DateTime LastDataUpdate { get; set; }
        
        /// <summary>
        /// Initialize a new instance of the SchedulesDirectStatusResponse class.
        /// </summary>
        public SchedulesDirectStatusResponse() { }
    }
}

