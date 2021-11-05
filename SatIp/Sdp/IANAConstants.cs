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

namespace Sdp
{
    /// <summary>
    /// Definitions of IANA constant values.
    /// </summary>
    public sealed class IANAConstants
    {
        /// <summary>
        /// Check that a value is valid.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="validValues">The list of valid values.</param>
        /// <returns>True if the value is in the list; false otherwise.</returns>
        public static bool IsValid(string value, string[] validValues)
        {
            foreach (string validValue in validValues)
            {
                if (value == validValue)
                    return (true);
            }

            return (false);
        }

        /// <summary>
        /// The medis values.
        /// </summary>
        public static string[] Media = new string[] 
        {
            "audio", "video", "text", "application", "message", "image"
        };

        /// <summary>
        /// The protocol values.
        /// </summary>
        public static string[] Protocol = new string[] 
        {
            "RTP/AVP", "udp", "vat", "rtp", "udptl", "TCP", "RTP/AVPF", "TCP/RTP/AVP", "RTP/SAVP",
            "TCP/BFCP", "TCP/TLS/BFCP", "TCP/TLS", "FLUTE/UDP", "TCP/MSRP", "TCP/TLS/MSRP",
            "DCCP", "DCCP/RTP/AVP", "DCCP/RTP/SAVP", "DCCP/RTP/AVPF", "DCCP/RTP/SAVPF",
            "RTP/SAVPF", "UDP/TLS/RTP/SAVP", "DCCP/TLS/RTP/SAVP", "UDP/TLS/RTP/SAVPF", "DCCP/TLS/RTP/SAVPF",
            "UDP/MBMS-FEC/RTP/AVP", "UDP/MBMS-FEC/RTP/SAVP", "UDP/MBMS-REPAIR", "FEC/UDP", "UDP/FEC",
            "TCP/MRCPv2", "TCP/TLS/MRCPv2", "PSTN"
        };

        /// <summary>
        /// The bandwidth type values.
        /// </summary>
        public static string[] BandwidthType = new string[] 
        {
            "CT", "AS", "RS", "RR", "TIAS"
        };

        /// <summary>
        /// The network type values.
        /// </summary>
        public static string[] NetType = new string[] 
        {
            "IN", "PSTN"
        };

        /// <summary>
        /// The address type values.
        /// </summary>
        public static string[] AddressType = new string[] 
        {
            "IP4", "IP6", "E164"
        };

        /// <summary>
        /// The encryption type values.
        /// </summary>
        public static string[] EncryptionType = new string[] 
        {
            "clear", "base64", "uri", "prompt"
        };
    }
}

