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
using System.Collections.ObjectModel;

namespace NetworkProtocols.Sdp
{
    /// <summary>
    /// The class that describes a media description.
    /// </summary>
    public class MediaDescription
    {
        /// <summary>
        /// Get the media type.
        /// </summary>
        public string MediaType { get; private set; }
        /// <summary>
        /// Get the port number.
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// Get the number of ports.
        /// </summary>
        public int NumberOfPorts { get; private set; }
        /// <summary>
        /// Get the protocol.
        /// </summary>
        public string Protocol { get; private set; }
        /// <summary>
        /// Get the list of formats.
        /// </summary>
        public Collection<string> Formats { get; private set; }

        /// <summary>
        /// Get the connection data.
        /// </summary>
        public ConnectionData ConnectionData { get; set; }        
        /// <summary>
        /// Get the bandwidth data.
        /// </summary>
        public BandwidthData BandwidthData { get; set; }
        /// <summary>
        /// Get the list of attributes.
        /// </summary>
        public Collection<SDPAttribute> Attributes { get; set; }

        /// <summary>
        /// Initialize a new instance of the MediaDescription class.
        /// </summary>
        public MediaDescription() { }

        /// <summary>
        /// Parse the media description.
        /// </summary>
        /// <param name="part">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string part)
        {
            string[] parts = part.Split(new char[] { ' ' });

            if (parts.Length < 4)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'm' attribute is in the wrong format", part));                

            MediaType = parts[0].Trim();
            if (!IANAConstants.IsValid(MediaType, IANAConstants.Media))
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'm' attribute type field is not recognized", part));                        

            string[] portParts = parts[1].Trim().Split(new char[] { '/' });
            if (portParts.Length > 2)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'm' attribute ports field is in the wrong format", part));                

            try
            {
                Port = Int32.Parse(portParts[0].Trim());
                if (portParts.Length == 2)
                    NumberOfPorts = Int32.Parse(portParts[1].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'm' attribute port field is in the wrong format", part));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'm' attribute port field is out of range", part));                
            }

            Protocol = parts[2].Trim();
            if (!IANAConstants.IsValid(Protocol, IANAConstants.Protocol))
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'm' attribute protocol field is not recognized", part));                

            if (parts.Length > 3)
            {
                Formats = new Collection<string>();

                for (int index = 3; index < parts.Length; index++)
                    Formats.Add(parts[index].Trim());
            }
            
            return (null);
        }
    }
}

