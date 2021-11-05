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
    /// The class that describes a Schedules Direct cast member.
    /// </summary>
    [DataContract]
    public class SchedulesDirectCastMember
    {
        /// <summary>
        /// Get or set the identification code.
        /// </summary>
        [DataMember(Name = "personId")]
        public string PersonId { get; set; }

        /// <summary>
        /// Get or set the name identification.
        /// </summary>
        [DataMember(Name = "nameId")]
        public string NameId { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the role.
        /// </summary>
        [DataMember(Name = "role")]
        public string Role { get; set; }

        /// <summary>
        /// Get or set the name of the character.
        /// </summary>
        [DataMember(Name = "characterName")]
        public string CharacterName { get; set; }

        /// <summary>
        /// Get or set the billing order.
        /// </summary>
        [DataMember(Name = "billingOrder")]
        public string BillingOrder { get; set; }

        /// <summary>
        /// Get the billing order.
        /// </summary>
        public int BillingOrderNumber
        {
            get
            {
                if (string.IsNullOrWhiteSpace(BillingOrder))
                    return 0;

                try
                {
                    return Int32.Parse(BillingOrder);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectCastMember class.
        /// </summary>
        public SchedulesDirectCastMember() { }
    }
}

