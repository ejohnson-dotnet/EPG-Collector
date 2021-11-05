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
    /// The class that describes the bandwidth.
    /// </summary>
    public class BandwidthData
    {
        /// <summary>
        /// Get the type of bandwidth.
        /// </summary>
        public string BandwidthType { get; private set; }
        /// <summary>
        /// Get the bandwidth.
        /// </summary>
        public int Bandwidth { get; private set; }

        /// <summary>
        /// Initialize a new instance of the BandwidthData class.
        /// </summary>
        public BandwidthData() { }

        /// <summary>
        /// Parse the bandwidth line.
        /// </summary>
        /// <param name="part">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string part)
        {
            string[] parts = part.Split(new char[] { ':' });

            if (parts.Length != 2)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'b' attribute is in the wrong format", part));                

            BandwidthType = parts[0].Trim();
            if (!IANAConstants.IsValid(BandwidthType, IANAConstants.BandwidthType))
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'b' attribute type field is not recognized", part));                

            try
            {
                Bandwidth = Int32.Parse(parts[1].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'b' attribute bandwidth field is in the wrong format", part));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'b' attribute bandwidth field is out of range", part));                
            }

            return (null);
        }
    }
}

