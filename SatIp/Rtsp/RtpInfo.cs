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

namespace Rtsp
{
    /// <summary>
    /// The class that describes the RTP information parameters.
    /// </summary>
    public class RtpInfo
    {
        /// <summary>
        /// Get the URL.
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// Get the stream identifier.
        /// </summary>
        public int StreamId { get; private set; }
        /// <summary>
        /// Get the first packet number.
        /// </summary>
        public int FirstPacketNumber { get; private set; }
        /// <summary>
        /// Get the RTP time
        /// </summary>
        public DateTime? RtpTime { get; private set; }

        /// <summary>
        /// Initialize a new instance of the RtpInfo class.
        /// </summary>
        public RtpInfo() { }

        /// <summary>
        /// Process the RTPInfo line.
        /// </summary>
        /// <param name="parameters">The line parameters.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string parameters)
        {
            int index = parameters.IndexOf('=');
            if (index == -1)
                return (new ErrorSpec(RtspMessageBase.RtspProtocolId, ErrorCode.FormatError, 0, "RTP-Info format incorrect", parameters));

            string identity = parameters.Substring(0, index).Trim();
            string rest = parameters.Substring(index + 1).Trim();

            if (identity.ToLowerInvariant() != "url")
                return (new ErrorSpec(RtspMessageBase.RtspProtocolId, ErrorCode.FormatError, 0, "RTP-Info format incorrect", parameters));

            char restSeparator;
            if (rest.IndexOf(";") != -1)
                restSeparator = ';';
            else
                restSeparator = ' ';

            string[] restParts =  rest.Split(restSeparator);

            int index1 = restParts[0].IndexOf("stream=");
            if (index1 == -1)
                Url = restParts[0];
            else
            {
                Url = restParts[0].Substring(0, index1);

                try
                {
                    StreamId = Int32.Parse(restParts[0].Substring(index1 + "stream=".Length));
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(RtspMessageBase.RtspProtocolId, ErrorCode.FormatError, 0, "RTP-Info stream ID format incorrect", parameters));
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(RtspMessageBase.RtspProtocolId, ErrorCode.FormatError, 0, "RTP-Info stream ID out of range", parameters));
                }
            }

            if (restSeparator == ' ')
                return (null);

            for (int restIndex = 1; restIndex < restParts.Length; restIndex++)
            {
                if (!string.IsNullOrWhiteSpace(restParts[restIndex]))
                {
                    int restIdentityIndex = restParts[restIndex].IndexOf('=');
                    if (restIdentityIndex == -1)
                        return (new ErrorSpec(RtspMessageBase.RtspProtocolId, ErrorCode.FormatError, 0, "RTP-Info format incorrect", parameters));

                    string restIdentity = restParts[restIndex].Substring(0, restIdentityIndex);
                    string restParameters = restParts[restIndex].Substring(restIdentityIndex + 1);

                    switch (restIdentity.ToLowerInvariant())
                    {
                        case "seq":
                            try
                            {
                                FirstPacketNumber = Int32.Parse(restParameters.Trim());
                            }
                            catch (FormatException)
                            {
                                return (new ErrorSpec(RtspMessageBase.RtspProtocolId, ErrorCode.FormatError, 0, "RTP-Info sequence format incorrect", parameters));
                            }
                            catch (OverflowException)
                            {
                                return (new ErrorSpec(RtspMessageBase.RtspProtocolId, ErrorCode.FormatError, 0, "RTP-Info sequence out of range", parameters));
                            }
                            break;
                        case "rtptime":
                            try
                            {
                                RtpTime = Time.GetTime(restParameters);
                            }
                            catch (FormatException)
                            {
                                return (new ErrorSpec(RtspMessageBase.RtspProtocolId, ErrorCode.FormatError, 0, "RTP-Info time format incorrect", parameters));
                            }
                            catch (OverflowException)
                            {
                                return (new ErrorSpec(RtspMessageBase.RtspProtocolId, ErrorCode.FormatError, 0, "RTP-Info time out of range", parameters));
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return (null);
        }
    }
}

