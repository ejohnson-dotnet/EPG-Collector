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

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes a terrestrial transmitter.
    /// </summary>
    public class SchedulesDirectTransmitter
    {
        /// <summary>
        /// Get the transmitter name.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Get the identifier for the transmitter.
        /// </summary>
        public string Identity { get; private set; }

        private SchedulesDirectTransmitter() { }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectTransmitter class.
        /// </summary>
        /// <param name="name">The name of the transmitter.</param>
        /// <param name="identity">The identifier for the transmitter.</param>
        public SchedulesDirectTransmitter(string name, string identity)
        {
            Name = name;
            Identity = identity;
        }

        /// <summary>
        /// Get a description of this instance.
        /// </summary>
        /// <returns>The description of the instance.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}

