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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjects
{
    /// <summary>
    /// The class that describes a cast or crew member.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Get the name of the person.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Get the role the person plays eg director.
        /// </summary>
        public string Role { get; private set; }
        
        /// <summary>
        /// Get the character the person plays.
        /// </summary>
        public string Character { get; private set; }
        
        /// <summary>
        /// Get the billing order for the person.
        /// </summary>
        public int Rank { get; private set; }

        private Person() { }

        /// <summary>
        /// Initialize a new instance of the Person class.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        /// <param name="role">The role the person plays.</param>
        public Person(string name, string role)
        {
            Name = name.Trim();            
            if (!string.IsNullOrWhiteSpace(role))
                Role = role.Trim();
        }

        /// <summary>
        /// Initialize a new instance of the Person class.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        /// <param name="role">The role the person plays.</param>
        /// <param name="character">The character the person plays.</param>
        /// <param name="rank">The billing order for the person.</param>
        public Person(string name, string role, string character, int rank) : this(name, role)
        {
            if (!string.IsNullOrWhiteSpace(character))
                Character = character.Trim();
            Rank = rank;
        }

        /// <summary>
        /// Get a description of this instance.
        /// </summary>
        /// <returns>The name of the person.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}

