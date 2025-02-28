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
using System.Xml.Serialization;

namespace WMCUtility
{
    /// <summary>
    /// The class that describes a lineup backup file top level node.
    /// </summary>
    [XmlRoot(ElementName = "MXF", Namespace = "")]
    public class BackupLineupRoot
    {
        /// <summary>
        /// Get or set the list of assemblies.
        /// </summary>
        [XmlElement(ElementName = "Assembly")]
        public Collection<BackupAssembly> Assemblies { get; set; }

        /// <summary>
        /// Get or set the list of lineups.
        /// </summary>
        [XmlElement(ElementName = "Lineup")]
        public Collection<BackupLineup> Lineups { get; set; }

        /// <summary>
        /// Get or set the list of device groups.
        /// </summary>
        [XmlElement(ElementName = "DeviceGroup")]
        public Collection<BackupDeviceGroup> DeviceGroups { get; set; }

        /// <summary>
        /// Initialize a new instance of the BackupLineupRoot class.
        /// </summary>
        public BackupLineupRoot() { }
    }
}
