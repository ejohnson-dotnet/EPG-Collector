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
    /// The base class for Schedules Direct response messages.
    /// </summary>
    [DataContract]
    public class SchedulesDirectResponse
    {
        /// <summary>
        /// Get or set the response description.
        /// </summary>
        [DataMember(Name = "response")]
        public string Response { get; set; }

        /// <summary>
        /// Get or set the response code.
        /// </summary>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        /// <summary>
        /// Get or set the response message.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Get or set the server identification.
        /// </summary>
        [DataMember(Name = "serverID")]
        public string ServerId { get; set; }

        /// <summary>
        /// Get or set the timestamp.
        /// </summary>
        [DataMember(Name = "datetime")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Get or set the lineup changes still available.
        /// </summary>
        [DataMember(Name = "changesRemaining")]
        public int ChangesRemaining { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectResponse class.
        /// </summary>
        public SchedulesDirectResponse() { }
    }
}

