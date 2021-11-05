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

using System.IO;

namespace SatIpDomainObjects
{
    /// <summary>
    /// The class that describes the status code in a server response.
    /// </summary>
    public class StatusCode
    {
        /// <summary>
        /// Process a server response status code.
        /// </summary>
        /// <param name="streamReader">The stream that contains the status code line as the first line.</param>
        /// <param name="protocol">The protocol expected.</param>
        /// <param name="protocolVersion">The protocol version expected.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public static ErrorSpec Process(StreamReader streamReader, string protocol, string protocolVersion)
        {
            if (streamReader.EndOfStream)
                return (new ErrorSpec(protocol, ErrorCode.UnexpectedEndOfMessage, 0, "Unexpected end of message", null));

            string line = streamReader.ReadLine();

            string[] parts = line.Split(new char[] { ' ' });
            if (parts.Length != 3)
                return (new ErrorSpec(protocol, ErrorCode.FormatError, 0, "Status response format incorrect", line));

            if (parts[0].Trim() != protocol + "/" + protocolVersion)
                return (new ErrorSpec(protocol, ErrorCode.FormatError, 0, "Status response protocol wrong", line));

            switch (parts[1].Trim())
            {
                case "200":
                    return (null);
                case "404":
                    return (new ErrorSpec(protocol, ErrorCode.NotFound, 404, parts[2].Trim(), line));
                case "408":
                    return (new ErrorSpec(protocol, ErrorCode.TimedOut, 408, parts[2].Trim(), line));
                case "453":
                    return (new ErrorSpec(protocol, ErrorCode.InsufficientBandwidth, 453, parts[2].Trim(), line));
                case "454":
                    return (new ErrorSpec(protocol, ErrorCode.SessionNotFound, 454, parts[2].Trim(), line));
                case "500":
                    return (new ErrorSpec(protocol, ErrorCode.ServerError, 500, parts[2].Trim(), line));
                case "503":
                    return (new ErrorSpec(protocol, ErrorCode.ServiceUnavailable, 503, parts[2].Trim(), line));
                default:
                    return (new ErrorSpec(protocol, ErrorCode.ResponseError, parts[1].Trim(), parts[2].Trim(), line));
            }
        }
    }
}

