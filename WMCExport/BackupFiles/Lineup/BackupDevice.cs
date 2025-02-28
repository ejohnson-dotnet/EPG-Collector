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

using System.Xml.Serialization;

namespace WMCUtility
{
    /// <summary>
    /// The class that describes a backup device.
    /// </summary>
    [XmlRoot(Namespace = "")]
    public class BackupDevice
    {
        /// <summary>
        /// Get or set the ID.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Get or set the ID reference.
        /// </summary>
        [XmlAttribute("idref")]
        public string IdRef { get; set; }

        /// <summary>
        /// Get or set the device type.
        /// </summary>
        [XmlAttribute("deviceType")]
        public string DeviceType { get; set; }

        /// <summary>
        /// Get or set the guid.
        /// </summary>
        [XmlAttribute("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Get or set the country code.
        /// </summary>
        [XmlAttribute("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the recorder ID.
        /// </summary>
        [XmlAttribute("recorderId")]
        public string RecorderId { get; set; }

        /// <summary>
        /// Get or set the preferred order.
        /// </summary>
        [XmlAttribute("preferredOrder")]
        public int PreferredOrder { get; set; }

        /// <summary>
        /// Get or set the channel tuning info supported flag.
        /// </summary>
        [XmlAttribute("isChannelTuninfoSupported")]
        public bool IsChannelTuningInfoSupported { get; set; }

        /// <summary>
        /// Get or set the DVB tuning info supported flag.
        /// </summary>
        [XmlAttribute("isDVBTuninfoSupported")]
        public bool IsDVBTuningInfoSupported { get; set; }

        /// <summary>
        /// Get or set the string tuning info supported flag.
        /// </summary>
        [XmlAttribute("isStringTuninfoSupported")]
        public bool IsStringTuningInfoSupported { get; set; }

        /// <summary>
        /// Get or set the wmis lineups.
        /// </summary>
        [XmlElement("wmisLineups")]
        public BackupWmisLineups WmisLineups { get; set; }

        /// <summary>
        /// Initialize a new instance of the BackupDevice class.
        /// </summary>
        public BackupDevice() { }
    }
}
