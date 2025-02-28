///////////////////////////////////////////////////////////////////////////////// 
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
    /// The class that describes a wmis lineup.
    /// </summary>
    [XmlRoot(Namespace = "")]
    public class BackupWmisLineup
    {
        /// <summary>
        /// Get or set the uid.
        /// </summary>
        [XmlAttribute("uid")]
        public string Uid { get; set; }

        /// <summary>
        /// Get or set the guid.
        /// </summary>
        [XmlAttribute("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the language.
        /// </summary>
        [XmlAttribute("language")]
        public string Language { get; set; }

        /// <summary>
        /// Get or set the state.
        /// </summary>
        [XmlAttribute("state")]
        public int State { get; set; }

        /// <summary>
        /// Get or set the lineup types.
        /// </summary>
        [XmlAttribute("lineupTypes")]
        public string LineupTypes { get; set; }

        /// <summary>
        /// Get or set the alternate lineup types.
        /// </summary>
        [XmlAttribute("altLineupTypes")]
        public string AlternateLineupTypes { get; set; }

        /// <summary>
        /// Initialize a new instance of the BackupWmisLineup class.
        /// </summary>
        public BackupWmisLineup() { }
    }
}
