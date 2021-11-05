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

namespace Rtsp
{
    /// <summary>
    /// A utility class for time conversion.
    /// </summary>
    public sealed class Time
    {
        private Time() { }

        /// <summary>
        /// Convert a string to the date and time.
        /// </summary>
        /// <param name="parameter">The number of seconds since midnight on the 1/1/1900</param>
        /// <returns>The converted date and time.</returns>
        public static DateTime GetTime(string parameter)
        {
            long seconds = long.Parse(parameter.Trim());
            return (new DateTime(1900, 1, 1) + new TimeSpan(seconds * TimeSpan.TicksPerSecond));
        }
    }
}

