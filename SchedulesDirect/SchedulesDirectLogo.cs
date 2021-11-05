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
    /// The class that describes a Schedules Direct logo.
    /// </summary>
    [DataContract]
    public class SchedulesDirectLogo
    {
        /// <summary>
        /// Get or set the URI.
        /// </summary>
        [DataMember(Name = "uri")]
        public string Uri 
        {
            get { return uri; } 
            set { uri = value; } 
        }

        /// <summary>
        /// Get or set the URL.
        /// </summary>
        [DataMember(Name = "URL")]
        public string Url 
        {
            get { return uri; } 
            set { uri = value; } 
        }

        /// <summary>
        /// Get or set the logo height.
        /// </summary>
        [DataMember(Name = "height")]
        public int Height { get; set; }

        /// <summary>
        /// Get or set the logo width.
        /// </summary>
        [DataMember(Name = "width")]
        public int Width { get; set; }

        /// <summary>
        /// Get or set the MD5 checksum for the logo.
        /// </summary>
        [DataMember(Name = "md5")]
        public string Md5 { get; set; }

        /// <summary>
        /// Get or set the source of the logo.
        /// </summary>
        [DataMember(Name = "source")]
        public string Source { get; set; }

        private string uri;

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectLogo class.
        /// </summary>
        public SchedulesDirectLogo() { }
    }
}

