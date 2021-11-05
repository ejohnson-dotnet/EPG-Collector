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
    /// The class that describes the connection data.
    /// </summary>
    public class ConnectionData
    {
        /// <summary>
        /// Get the network type.
        /// </summary>
        public string NetworkType { get; private set; }
        /// <summary>
        /// Get the address type.
        /// </summary>
        public string AddressType { get; private set; }
        /// <summary>
        /// Get the connection address.
        /// </summary>
        public string ConnectionAddress { get; private set; }
        /// <summary>
        /// Get the time to live.
        /// </summary>
        public int TimeToLive { get; private set; }
        /// <summary>
        /// Get the number of addresses.
        /// </summary>
        public int NumberOfAddresses { get; private set; }

        /// <summary>
        /// Initialize a new instance of the ConnectionData CLASS.
        /// </summary>
        public ConnectionData() { }

        /// <summary>
        /// Parse the connection line.
        /// </summary>
        /// <param name="part">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string part)
        {
            string[] parts = part.Split(new char[] { ' ' });

            if (parts.Length != 3)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'c' attribute is in the wrong format", part));                

            NetworkType = parts[0].Trim();
            if (!IANAConstants.IsValid(NetworkType, IANAConstants.NetType))
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'c' attribute network type is not recognized", part));                

            AddressType = parts[1].Trim();
            if (!IANAConstants.IsValid(AddressType, IANAConstants.AddressType))
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'c' attribute address type is not recognized", part));                

            string[] addressParts = parts[2].Trim().Split(new char[] { '/' });
            if (addressParts.Length > 3)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'c' attribute is in the wrong format", part));                

            ConnectionAddress = addressParts[0];
            
            if (addressParts.Length > 1)
            {
                try
                {
                    TimeToLive = Int32.Parse(addressParts[1].Trim());
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'c' attribute TTL field is in the wrong format", part));                
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'c' attribute TTL field is out of range", part));                
                }
            }
            else
                TimeToLive = 0;

            if (addressParts.Length > 2)
            {
                try
                {
                    NumberOfAddresses = Int32.Parse(addressParts[2].Trim());
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'c' attribute number of addresses is in the wrong format", part));                
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'c' attribute number of addresses is out of range", part));                
                }
            }
            else
                NumberOfAddresses  = 1;

            return (null);
        }
    }
}

