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
    /// The class that describes a backup device type.
    /// </summary>
    [XmlRoot(Namespace = "")]
    public class BackupDeviceType
    {
        /// <summary>
        /// Get or set the uid.
        /// </summary>
        [XmlAttribute("uid")]
        public string Uid { get; set; }

        /// <summary>
        /// Get or set the network type.
        /// </summary>
        [XmlAttribute("networkType")]
        public string NetworkType { get; set; }

        /// <summary>
        /// Get or set the headend type.
        /// </summary>
        [XmlAttribute("headendType")]
        public string HeadendType { get; set; }

        /// <summary>
        /// Get or set the video source.
        /// </summary>
        [XmlAttribute("videoSource")]
        public string VideoSource { get; set; }

        /// <summary>
        /// Get or set the set top box flag.
        /// </summary>
        [XmlAttribute("isSetTopBox")]
        public bool IsSetTopBox { get; set; }

        /// <summary>
        /// Get or set the tuning space name.
        /// </summary>
        [XmlAttribute("tuningSpaceName")]
        public string TuningSpaceName { get; set; }

        /// <summary>
        /// Get or set the display name.
        /// </summary>
        [XmlAttribute("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the sqmid.
        /// </summary>
        [XmlAttribute("sqmId")]
        public int SqmId { get; set; }

        /// <summary>
        /// Get or set the view priority.
        /// </summary>
        [XmlAttribute("viewPriority")]
        public long ViewPriority { get; set; }

        /// <summary>
        /// Initialize a new instance of the BackupDeviceType class.
        /// </summary>
        public BackupDeviceType() { }
    }
}
