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

using NetworkProtocols;
using NetworkProtocols.Sdp;

namespace SatIp
{
    /// <summary>
    /// The class that describes a SAT>IP control attribute.
    /// </summary>
    public class SatIpControlAttribute : ISdpControl, ICreateControl
    {
        /// <summary>
        /// Get the stream identifier.
        /// </summary>
        public int StreamId { get; private set; }

        /// <summary>
        /// Initialize a new instance of the SatIpControlAttribute class.
        /// </summary>
        public SatIpControlAttribute() { }

        /// <summary>
        /// Create an instance of the processor.
        /// </summary>
        /// <returns>An instance of the processor.</returns>
        public ISdpControl CreateControl()
        {
            return (new SatIpControlAttribute());
        }

        /// <summary>
        /// Parse the attribute data.
        /// </summary>
        /// <param name="parameters">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string parameters)
        {
            string[] parts = parameters.Split(new char[] { '=' });
            if (parts.Length != 2 || parts[0].Trim() != "stream")
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute control field is in the wrong format", parameters));                

            try
            {
                StreamId = Int32.Parse(parts[1].Trim());
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute control field stream ID is in the wrong format", parameters));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute control field stream ID is out of range", parameters));                
            }
        }
    }
}

