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
    /// The class the describes a Schedules Direct image entry.
    /// </summary>
    [DataContract]
    public class SchedulesDirectImageEntry
    {
        /// <summary>
        /// Get or set the width of the image.
        /// </summary>
        [DataMember(Name = "width")]
        public string Width { get; set; }

        /// <summary>
        /// Get or set the height of the entry.
        /// </summary>
        [DataMember(Name = "height")]
        public string Height { get; set; }

        /// <summary>
        /// Get or set the URL of the image.
        /// </summary>
        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Get or set the size of the image.
        /// </summary>
        [DataMember(Name = "size")]
        public string Size { get; set; }

        /// <summary>
        /// Get or set the aspect ratio of the image.
        /// </summary>
        [DataMember(Name = "aspect")]
        public string Aspect { get; set; }

        /// <summary>
        /// Get or set the category of the image.
        /// </summary>
        [DataMember(Name = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Get or set the text associated with the image.
        /// </summary>
        [DataMember(Name = "text")]
        public string Text { get; set; }

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
        /// Initialize a new instance of the SchedulesDirectImageEntry class.
        /// </summary>
        public SchedulesDirectImageEntry() { }
    }
}

