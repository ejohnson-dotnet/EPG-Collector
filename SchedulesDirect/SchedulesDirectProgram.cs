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
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes a Schedules Direct programme.
    /// </summary>
    [DataContract]
    public class SchedulesDirectProgram
    {
        /// <summary>
        /// Get or set the programme identifier.
        /// </summary>
        [DataMember(Name = "programID")]
        public string ProgramId { get; set; }

        /// <summary>
        /// Get or set the air date and time.
        /// </summary>
        [DataMember(Name = "airDateTime")]
        public DateTime AirDateTime { get; set; }

        /// <summary>
        /// Get or set the duration of the programme.
        /// </summary>
        [DataMember(Name = "duration")]
        public int Duration { get; set; }

        /// <summary>
        /// Get or set the MD5 checksum for the programme.
        /// </summary>
        [DataMember(Name = "md5")]
        public string Md5 { get; set; }

        /// <summary>
        /// Returns true if the programme is new; false otherwise.
        /// </summary>
        [DataMember(Name = "new")]
        public bool New { get; set; }

        /// <summary>
        /// Returns true if the programme is from cable in the classroom; false otherwise. 
        /// </summary>
        [DataMember(Name = "cableInTheClassroom")]
        public bool CableInTheClassroom { get; set; }

        /// <summary>
        /// Returns true if the programme is a catchup; false otherwise.
        /// </summary>
        [DataMember(Name = "catchup")]
        public bool Catchup { get; set; }

        /// <summary>
        /// Returns true if the programme is a continuation; false otherwise.
        /// </summary>
        [DataMember(Name = "continued")]
        public bool Continued { get; set; }

        /// <summary>
        /// Returns true if the programme is educational; false otherwise.
        /// </summary>
        [DataMember(Name = "educational")]
        public bool Educational { get; set; }

        /// <summary>
        /// Returns true if the programme is joined while in progress; false otherwise.
        /// </summary>
        [DataMember(Name = "joinedInProgress")]
        public bool JoinedInProgress { get; set; }

        /// <summary>
        /// Returns true if the programme has been left in progress; false otherwise.
        /// </summary>
        [DataMember(Name = "leftInProgress")]
        public bool LeftInProgress { get; set; }

        /// <summary>
        /// Returns true if the programme is a premiere showing; false otherwise.
        /// </summary>
        [DataMember(Name = "premiere")]
        public bool Premiere { get; set; }

        /// <summary>
        /// Returnsd true if the programme is a break; false otherwise.
        /// </summary>
        [DataMember(Name = "programBreak")]
        public bool ProgramBreak { get; set; }

        /// <summary>
        /// Returns true if the programme is a repeat; false otherwise.
        /// </summary>
        [DataMember(Name = "repeat")]
        public bool Repeat { get; set; }

        /// <summary>
        /// Returns true if the programme is signed; false otherwise.
        /// </summary>
        [DataMember(Name = "signed")]
        public bool Signed { get; set; }

        /// <summary>
        /// Returns true if the programme is subject to blackout; false otherwise.
        /// </summary>
        [DataMember(Name = "subjectToBlackout")]
        public bool SubjectToBlackout { get; set; }

        /// <summary>
        /// Returns true if the duration is approximate; false otherwise.
        /// </summary>
        [DataMember(Name = "timeApproximate")]
        public bool TimeApproximate { get; set; }

        /// <summary>
        /// Returns true if the programme is free on a paid for service; false otherwise.
        /// </summary>
        [DataMember(Name = "free")]
        public bool Free { get; set; }

        /// <summary>
        /// Get or set the collection of audio properties.
        /// </summary>
        [DataMember(Name = "audioProperties")]
        public Collection<string> AudioProperties { get; set; }

        /// <summary>
        /// Get the collection of video properties.
        /// </summary>
        [DataMember(Name = "videoProperties")]
        public Collection<string> VideoProperties { get; set; }

        /// <summary>
        /// Get or set if the programme is from a live tape delay; false otherwise.
        /// </summary>
        [DataMember(Name = "liveTapeDelay")]
        public string LiveTapeDelay { get; set; }

        /// <summary>
        /// Get or set if the programme is a premiere or finale.
        /// </summary>
        [DataMember(Name = "isPremiereOrFinale")]
        public string IsPremiereOrFinale { get; set; }

        /// <summary>
        /// Get or set the list of content ratings.
        /// </summary>
        [DataMember(Name = "contentRating")]
        public Collection<SchedulesDirectContentRating> ContentRatings { get; set; }

        /// <summary>
        /// Get or set the multipart information.
        /// </summary>
        [DataMember(Name = "multipart")]
        public SchedulesDirectMultipart Multipart { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectProgram class.
        /// </summary>
        public SchedulesDirectProgram() { }
    }
}

