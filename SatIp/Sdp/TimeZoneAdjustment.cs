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

using SatIpDomainObjects;

namespace Sdp
{
    /// <summary>
    /// The class that describes a time zone adjustment.
    /// </summary>
    public class TimeZoneAdjustment
    {
        /// <summary>
        /// Get the adjustment time.
        /// </summary>
        public TimeSpan AdjustmentTime { get; private set; }
        /// <summary>
        /// Get the offset time.
        /// </summary>
        public TimeSpan Offset { get; private set; }

        /// <summary>
        /// Initialize a new instance of the TimeZoneAdjustment class.
        /// </summary>
        public TimeZoneAdjustment() { }

        /// <summary>
        /// Parse the tiem zone adjustment data.
        /// </summary>
        /// <param name="part1">The first part of the data to be parsed.</param>
        /// <param name="part2">The second part of the data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string part1, string part2)
        {
            return (null);
        }
    }
}

