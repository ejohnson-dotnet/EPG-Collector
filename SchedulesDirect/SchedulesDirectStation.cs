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
    /// The class that describes a Schedules Direct station.
    /// </summary>
    [DataContract]
    public class SchedulesDirectStation
    {
        /// <summary>
        /// Get or set the station identifier.
        /// </summary>
        [DataMember(Name = "stationID")]
        public string StationId { get; set; }

        /// <summary>
        /// Get or set the name of the station.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the station call sign.
        /// </summary>
        [DataMember(Name = "callsign")]
        public string CallSign { get; set; }

        /// <summary>
        /// Get or set the affiliate.
        /// </summary>
        [DataMember(Name = "affiliate")]
        public string Affiliate { get; set; }

        /// <summary>
        /// Get or set the list of languages broadcast by the station.
        /// </summary>
        [DataMember(Name = "broadcastLanguage")]
        public Collection<string> BroadcastLanguages { get; set; }

        /// <summary>
        /// Get or set the list of languages used for descriptions from the station.
        /// </summary>
        [DataMember(Name = "descriptionLanguage")]
        public Collection<string> DescriptionLanguages { get; set; }

        /// <summary>
        /// Get or set the broadcaster information.
        /// </summary>
        [DataMember(Name = "broadcaster")]
        public SchedulesDirectBroadcaster Broadcaster { get; set; }

        /// <summary>
        /// Get or set the list of station logos.
        /// </summary>
        [DataMember(Name = "stationLogo")]
        public Collection<SchedulesDirectLogo> StationLogos { get; set; }

        /// <summary>
        /// Returns true if the station is commercial free; false otherwise.
        /// </summary>
        [DataMember(Name = "isCommericalFree")]
        public bool IsCommercialFree { get; set; }

        /// <summary>
        /// Get or set the metadata for the station.
        /// </summary>
        [DataMember(Name = "metadata")]
        public SchedulesDirectStationMetadata Metadata { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectStation class.
        /// </summary>
        public SchedulesDirectStation() { }
    }
}

