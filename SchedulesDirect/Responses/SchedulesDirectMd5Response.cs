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
using System.Runtime.Serialization;

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes a Schedules Direct MD5 response.
    /// </summary>
    [DataContract]
    public class SchedulesDirectMd5Response : SchedulesDirectResponse
    {
        /// <summary>
        /// The last modified date.
        /// </summary>
        [DataMember(Name = "lastModified")]
        public DateTime LastModified { get; set; }

        /// <summary>
        /// The MD5 checksum.
        /// </summary>
        [DataMember(Name = "md5")]
        public string Md5 { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectMd5Response class.
        /// </summary>
        public SchedulesDirectMd5Response() { }
    }
}

