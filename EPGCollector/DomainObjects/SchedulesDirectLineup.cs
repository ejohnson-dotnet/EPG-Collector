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

namespace DomainObjects
{
    /// <summary>
    /// The class that defines a Schedules Direct lineup.
    /// </summary>
    [DataContract]
    public class SchedulesDirectLineup
    {
        /// <summary>
        /// Get or set the internal identity of the lineup.
        /// </summary>
        [DataMember(Name = "lineup")]
        public string Identity { get; set; }

        /// <summary>
        /// Get or set the internal identity of the lineup.
        /// </summary>
        [DataMember(Name = "lineupID")]
        public string LineupID 
        {
            get { return Identity; }
            set { Identity = value; } 
        }
        
        /// <summary>
        /// Get or set the name of the lineup.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        /// <summary>
        /// Get or set the last modified date.
        /// </summary>
        [DataMember(Name = "modified")]
        public DateTime Modified { get; set; }
        
        /// <summary>
        /// Get or set the location of the lineup.
        /// </summary>
        [DataMember(Name = "location")]
        public string Location { get; set; }
        
        /// <summary>
        /// Get or set the transport type of the lineup.
        /// </summary>
        [DataMember(Name = "transport")]
        public string Transport { get; set; }
        
        /// <summary>
        /// Get or ser the URI associated with the lineup.
        /// </summary>
        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Get or set the flag indicating that the lineup has been deleted.
        /// </summary>
        [DataMember(Name = "isDeleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Get the full name of the lineup.
        /// </summary>
        public string FullName 
        { 
            get 
            {
                string reply = Name;

                if (!string.IsNullOrWhiteSpace(Name))
                    reply += " - ";

                if (!string.IsNullOrWhiteSpace(Location))
                    reply += Location;
                else
                    reply += Identity;
                                
                if (!string.IsNullOrWhiteSpace(Transport))
                    reply += " - " + Transport;

                return reply;
            } 
        }

        /// <summary>
        /// Create a new instance of the SdLineup class.
        /// </summary>
        public SchedulesDirectLineup() { }

        /// <summary>
        /// Create a copy of the current instance.
        /// </summary>
        /// <returns>A copy of the current instance.</returns>
        public SchedulesDirectLineup Clone()
        {
            SchedulesDirectLineup newLineup = new SchedulesDirectLineup();
            newLineup.Identity = Identity;
            newLineup.Name = Name;
            newLineup.Modified = Modified;
            newLineup.Location = Location;
            newLineup.Transport = Transport;
            newLineup.Uri = Uri;
            newLineup.IsDeleted = IsDeleted;

            return newLineup;
        }

        /// <summary>
        /// Compare this instance with another.
        /// </summary>
        /// <param name="otherLineup">The other lineup.</param>
        /// <returns>True if the instances are equal; false otherwise.</returns>
        public bool CompareTo(SchedulesDirectLineup otherLineup)
        {
            if (Identity != otherLineup.Identity)
                return false;
            if (Name != otherLineup.Name)
                return false;
            if (Location != otherLineup.Location)
                return false;
            if (Transport != otherLineup.Transport)
                return false;
            if (Uri != otherLineup.Uri)
                return false;

            return true;
        }

        /// <summary>
        /// Get a string representation of this instance.
        /// </summary>
        /// <returns>A string representation of this instance.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}

