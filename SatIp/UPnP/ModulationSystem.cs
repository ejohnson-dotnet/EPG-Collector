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

namespace UPnP
{
    /// <summary>
    /// The class that describes a modulation system.
    /// </summary>
    public class ModulationSystem
    {
        /// <summary>
        /// Get the type.
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// Get the count.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Initialize a new instance of the ModulationSystem class.
        /// </summary>
        public ModulationSystem() { }

        /// <summary>
        /// Parse a modulation.
        /// </summary>
        /// <param name="entry">The data to parse.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Load(string entry)
        {
            string[] parts = entry.Split(new char[] { '-' } );
            if (parts.Length > 2)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The modulation system field is in the wrong format", entry));
            }

            Type = parts[0].Trim();

            if (parts.Length > 1)
            {
                try
                {
                    Count = Int32.Parse(parts[1].Trim());
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The modulation system count field is in the wrong format", entry));
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The modulation system count field is out of range", entry));
                }
            }
            else
                Count = 1;
            
            return (null);
        }
    }
}

