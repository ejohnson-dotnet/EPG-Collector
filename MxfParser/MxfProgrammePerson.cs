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

namespace MxfParser
{
    /// <summary>
    /// The class that describes a programme person entry.
    /// </summary>
    public class MxfProgrammePerson
    {
        /// <summary>
        /// Get or set the person ID.
        /// </summary>
        public string Person { get; set; }
        
        /// <summary>
        /// Get or set the rank.
        /// </summary>
        public string Rank { get; set; }
        
        /// <summary>
        /// Get or set the chartacter.
        /// </summary>
        public string Character { get; set; }

        /// <summary>
        /// Get the rank as a number.
        /// </summary>
        public int RankNumber
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Rank))
                    return 0;

                try
                {
                    return Int32.Parse(Rank);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        private MxfProgrammePerson() { }

        /// <summary>
        /// Initialize a new instance of the MxfProgrammePerson class.
        /// </summary>
        /// <param name="person">The person ID.</param>
        /// <param name="rank">The rank.</param>
        /// <param name="character">The character.</param>
        public MxfProgrammePerson(string person, string rank, string character)
        {
            Person = person;
            Rank = rank;
            Character = character;
        }
    }
}

