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

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes a satellite.
    /// </summary>
    public class SchedulesDirectSatellite
    {
        /// <summary>
        /// Get the satellite name.
        /// </summary>
        public string Name { get; private set; }

        private SchedulesDirectSatellite() { }
        
        /// <summary>
        /// Initialize a new instance of the SchedulesDirectSatellite class.
        /// </summary>
        /// <param name="name">The name of the satellite.</param>
        public SchedulesDirectSatellite(string name) 
        {
            Name = name;
        }

        /// <summary>
        /// Get a description of this instance.
        /// </summary>
        /// <returns>The description of this instance.</returns>
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return string.Empty;

            string[] parts = Name.Split(new char[] { '-' });
            if (parts.Length != 3)
                return Name;

            return parts[1];
        }
    }
}

