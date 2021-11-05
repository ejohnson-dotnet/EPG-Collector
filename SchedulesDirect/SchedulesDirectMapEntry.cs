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

using System.Runtime.Serialization;

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes a Schedules Direct map entry.
    /// </summary>
    [DataContract]
    public class SchedulesDirectMapEntry
    {
        /// <summary>
        /// Get or set the station identity.
        /// </summary>
        [DataMember(Name = "stationID")]
        public string StationId { get; set; }

        /// <summary>
        /// Get or set the UHF/VHF flag.
        /// </summary>
        [DataMember(Name = "uhfVhf")]
        public int UhfVhf { get; set; }

        /// <summary>
        /// Get or set the ATSC major channel number.
        /// </summary>
        [DataMember(Name = "atscMajor")]
        public int AtscMajor { get; set; }

        /// <summary>
        /// Get or set the minor ATSC channel number.
        /// </summary>
        [DataMember(Name = "atscMinor")]
        public int AtscMinor { get; set; }

        /// <summary>
        /// Get or set the channel number.
        /// </summary>
        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        /// <summary>
        /// Get or set the call sign.
        /// </summary>
        [DataMember(Name = "providerCallsign")]
        public string ProviderCallSign { get; set; }

        /// <summary>
        /// Get or set the logical channel number.
        /// </summary>
        [DataMember(Name = "logicalChannelNumber")]
        public string LogicalChannelNumber { get; set; }

        /// <summary>
        /// Get or set the match type.
        /// </summary>
        [DataMember(Name = "matchType")]
        public string MatchType { get; set; }

        /// <summary>
        /// Get or set the frequency.
        /// </summary>
        [DataMember(Name = "frequencyHz")]
        public long Frequency { get; set; }

        /// <summary>
        /// Get or set the DVB service ID.
        /// </summary>
        [DataMember(Name = "serviceID")]
        public int ServiceId { get; set; }

        /// <summary>
        /// Get or set the DVB network ID.
        /// </summary>
        [DataMember(Name = "networkID")]
        public int NetworkId { get; set; }

        /// <summary>
        /// Get or set the DVB transport ID.
        /// </summary>
        [DataMember(Name = "transportID")]
        public int TransportId { get; set; }

        /// <summary>
        /// Get or set the DVB delivery system.
        /// </summary>
        [DataMember(Name = "deliverySystem")]
        public string DeliverySystem { get; set; }

        /// <summary>
        /// Get or set the DVB modulation system.
        /// </summary>
        [DataMember(Name = "modulationSystem")]
        public string ModulationSystem { get; set; }

        /// <summary>
        /// Get or set the DVB symbol rate.
        /// </summary>
        [DataMember(Name = "symbolrate")]
        public int SymbolRate { get; set; }

        /// <summary>
        /// Get or set the DVB polarization.
        /// </summary>
        [DataMember(Name = "polarization")]
        public string Polarization { get; set; }

        /// <summary>
        /// Get or set the DVB FEC.
        /// </summary>
        [DataMember(Name = "fec")]
        public string Fec { get; set; }

        /// <summary>
        /// Get or set the virtual channel number.
        /// </summary>
        [DataMember(Name = "virtualChannel")]
        public string VirtualChannel { get; set; }

        /// <summary>
        /// Get or set the major channel number.
        /// </summary>
        [DataMember(Name = "channelMajor")]
        public int ChannelMajor { get; set; }

        /// <summary>
        /// Get or set the minor channel number.
        /// </summary>
        [DataMember(Name = "channelMinor")]
        public int ChannelMinor { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectMapEntry class.
        /// </summary>
        public SchedulesDirectMapEntry() { }
    }
}

