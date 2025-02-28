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
using System.Xml.Serialization;

namespace WMCUtility
{
    /// <summary>
    /// The class that describes a backup device group element.
    /// </summary>
    [XmlRoot(Namespace = "")]
    public class BackupDeviceGroup
    {
        /// <summary>
        /// Get or set the uid.
        /// </summary>
        [XmlAttribute("uid")]
        public string Uid { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Get or set the last configuration change.
        /// </summary>
        [XmlAttribute("lastConfigurationChange")]
        public DateTime LastConfigurationChange { get; set; }
        
        /// <summary>
        /// Get or set the rank.
        /// </summary>
        [XmlAttribute("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// Get or set the permit any device type falg.
        /// </summary>
        [XmlAttribute("permitAnyDeviceType")]
        public bool PermitAnyDeviceType { get; set; }

        /// <summary>
        /// Get or set the enabled flag.
        /// </summary>
        [XmlAttribute("isEnabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Get or set the first run process ID.
        /// </summary>
        [XmlAttribute("firstRunProcessId")]
        public int FirstRunProcessId { get; set; }

        /// <summary>
        /// Get or set the only show dynamic lineups flag.
        /// </summary>
        [XmlAttribute("onlyShowDynamicLineups")]
        public bool OnlyShowDynamicLineups { get; set; }
        
        /// <summary>
        /// Get or set the exclusive device types.
        /// </summary>
        [XmlElement("exclusiveDeviceTypes")]
        public BackupDeviceTypes ExclusiveDeviceTypes { get; set; }

        /// <summary>
        /// Get or set the permitted device types.
        /// </summary>
        [XmlElement("permittedDeviceTypes")]
        public BackupDeviceTypes PermittedDeviceTypes { get; set; }

        /// <summary>
        /// Get or set the dynamic lineups.
        /// </summary>
        [XmlElement("dynamicLineups")]
        public BackupDynamicLineups DynamicLineups { get; set; }

        /// <summary>
        /// Get or set the guide image.
        /// </summary>
        [XmlElement("guideImage")]
        public BackupGuideImage GuideImage { get; set; }

        /// <summary>
        /// Get or set the list of devices.
        /// </summary>
        [XmlElement("devices")]
        public BackupDevices Devices { get; set; }

        /// <summary>
        /// Initialize a new instance of the BackupDeviceGroup class.
        /// </summary>
        
        public BackupDeviceGroup() { }
    }
}
