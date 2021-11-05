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
    /// The class that describes a SchedulesDirect episode image.
    /// </summary>
    [DataContract]
    public class SchedulesDirectEpisodeImage
    {
        /// <summary>
        /// Get or set the width of the image.
        /// </summary>
        [DataMember(Name = "width")]
        public string Width { get; set; }

        /// <summary>
        /// Get or set the height of the image.
        /// </summary>
        [DataMember(Name = "height")]
        public string Height { get; set; }

        /// <summary>
        /// Get or set the URL of the image.
        /// </summary>
        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Get or set the category of the image.
        /// </summary>
        [DataMember(Name = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Get or set the primary setting for the image.
        /// </summary>
        [DataMember(Name = "primary")]
        public string Primary { get; set; }

        /// <summary>
        /// Get or set the level of the image.
        /// </summary>
        [DataMember(Name = "tier")]
        public string Tier { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectEpisodeImage class.
        /// </summary>
        public SchedulesDirectEpisodeImage() { }
    }
}

