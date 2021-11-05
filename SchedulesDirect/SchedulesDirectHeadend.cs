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

using DomainObjects;

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes a Schedules Direct headend.
    /// </summary>
    [DataContract]
    public class SchedulesDirectHeadend
    {
        /// <summary>
        /// Get or set the headend identification.
        /// </summary>
        [DataMember(Name = "headend")]
        public string Headend { get; set; }

        /// <summary>
        /// Get or set the transport.
        /// </summary>
        [DataMember(Name = "transport")]
        public string Transport { get; set; }

        /// <summary>
        /// Get or set the location.
        /// </summary>
        [DataMember(Name = "location")]
        public string Location { get; set; }

        /// <summary>
        /// Get or set the lineups in the headend.
        /// </summary>
        [DataMember(Name = "lineups")]
        public Collection<SchedulesDirectLineup> Lineups { get; set; }

        /// <summary>
        /// Get or set the internal identity of the lineup.
        /// </summary>
        [DataMember(Name = "lineupID")]
        public string LineupID { get; set; }

        /// <summary>
        /// Get or set the name of the lineup.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or ser the URL associated with the lineup.
        /// </summary>
        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirect headend class.
        /// </summary>
        public SchedulesDirectHeadend() { }
    }
}

