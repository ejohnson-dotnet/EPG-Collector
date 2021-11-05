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

using SatIpDomainObjects;

namespace Sdp
{
    /// <summary>
    /// The class that describes an origin.
    /// </summary>
    public class Origin
    {
        /// <summary>
        /// Get the session identity.
        /// </summary>
        public string SessionId { get; private set; }
        /// <summary>
        /// Get the version.
        /// </summary>
        public string SessionVersion { get; private set; }
        /// <summary>
        /// Get the host address.
        /// </summary>
        public string HostAddress { get; private set; }

        /// <summary>
        /// Initialize a new instance of the Origin class.
        /// </summary>
        public Origin() { }

        /// <summary>
        /// Parse the origin data.
        /// </summary>
        /// <param name="part">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string part)
        {
            string[] parts = part.Split(new char[] { ' ' });

            if (parts.Length != 6)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'o' attribute is in the wrong format", part));                

            if (parts[0].Trim() != "-")
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'o' attribute is in the wrong format", part));                

            SessionId = parts[1].Trim();
            SessionVersion = parts[2].Trim();

            if (parts[3].Trim() != "IN")
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'o' attribute is in the wrong format", part));                

            if (parts[4].Trim() != "IP4")
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'o' attribute is in the wrong format", part));                

            HostAddress = parts[5].Trim();

            return (null);
        }
    }
}

