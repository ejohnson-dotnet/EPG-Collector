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
    /// The class that describes a Schedules Direct country.
    /// </summary>
    [DataContract]
    public class SchedulesDirectCountry
    {
        /// <summary>
        /// Get or set the full name.
        /// </summary>
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }

        /// <summary>
        /// Get or set the short name.
        /// </summary>
        [DataMember(Name = "shortName")]
        public string ShortName { get; set; }

        /// <summary>
        /// Get or set the postcode example.
        /// </summary>
        [DataMember(Name = "postalCodeExample")]
        public string PostalCodeExample { get; set; }

        /// <summary>
        /// Get or set the single postcode.
        /// </summary>
        [DataMember(Name = "postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// True if the country only has a single postcode; false otherwise.
        /// </summary>
        [DataMember(Name = "onePostalCode")]
        public bool OnePostalCode { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectCountry class.
        /// </summary>
        public SchedulesDirectCountry() { }

        /// <summary>
        /// Get the description of this instance.
        /// </summary>
        /// <returns>The description of this instance.</returns>
        public override string ToString()
        {
            return FullName;
        }
    }
}

