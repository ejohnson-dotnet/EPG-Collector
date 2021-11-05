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

namespace NetworkProtocols.Sdp
{
    /// <summary>
    /// The class that describes an SDP attribute.
    /// </summary>
    public class SDPAttribute
    {
        /// <summary>
        /// Get the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Get the parameters.
        /// </summary>
        public string Parameters { get; private set; }

        /// <summary>
        /// Get the category.
        /// </summary>
        public string Category { get; private set; }
        /// <summary>
        /// Get the list of keywords.
        /// </summary>
        public string[] Keywords { get; private set; }
        /// <summary>
        /// Get the tool description.
        /// </summary>
        public string Tool { get; private set; }
        /// <summary>
        /// Get the packet time.
        /// </summary>
        public TimeSpan PacketTime { get; private set; }
        /// <summary>
        /// Get the maximum packet time.
        /// </summary>
        public TimeSpan MaxPacketTime { get; private set; }
        /// <summary>
        /// Returns true if receive only; false otherwise.
        /// </summary>
        public bool ReceiveOnly { get; private set; }
        /// <summary>
        /// Returns true if send and receive; false otherwise.
        /// </summary>
        public bool SendReceive { get; private set; }
        /// <summary>
        /// Returns true if send only; false otherwise.
        /// </summary>
        public bool SendOnly { get; private set; }
        /// <summary>
        /// Returns true if inactive; false otherwise.
        /// </summary>
        public bool Inactive { get; private set; }
        /// <summary>
        /// Get the orientation.
        /// </summary>
        public string Orientation { get; private set; }
        /// <summary>
        /// Get the conference type.
        /// </summary>
        public string ConferenceType { get; private set; }
        /// <summary>
        /// Get the character set.
        /// </summary>
        public string CharacterSet { get; private set; }
        /// <summary>
        /// Get the SDP language code.
        /// </summary>
        public string SDPLanguage { get; private set; }
        /// <summary>
        /// Get the language.
        /// </summary>
        public string Language { get; private set; }
        /// <summary>
        /// Get the frame rate.
        /// </summary>
        public int FrameRate { get; private set; }
        /// <summary>
        /// Get the quality.
        /// </summary>
        public int Quality { get; private set; }        
        /// <summary>
        /// Get the format processor.
        /// </summary>
        public ISdpFormat Format { get; private set; }
        /// <summary>
        /// Get the control processor.
        /// </summary>
        public ISdpControl Control { get; private set; }
        /// <summary>
        /// Get the range units.
        /// </summary>
        public string RangeUnits { get; private set; }
        /// <summary>
        /// Get the start of the range.
        /// </summary>
        public int RangeStart { get; private set; }
        /// <summary>
        /// Get the end of the range.
        /// </summary>
        public int RangeEnd { get; private set; }
        /// <summary>
        /// Get the start date of the range.
        /// </summary>
        public string RangeDateStart { get; private set; }
        /// <summary>
        /// Get the end date of the range.
        /// </summary>
        public string RangeDateEnd { get; private set; }
        /// <summary>
        /// Get the Etag.
        /// </summary>
        public string Etag { get; private set; }

        /// <summary>
        /// Initialize a new instance of the SDPAttribute class.
        /// </summary>
        public SDPAttribute() { }

        /// <summary>
        /// Parse the attribute data.
        /// </summary>
        /// <param name="part">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string part)
        {
            int index = part.IndexOf(':');
            if (index == -1)
                Name = part.Trim();
            else
            {
                Name = part.Substring(0, index);
                Parameters = part.Substring(index + 1);
            }

            switch (Name)
            {
                case "cat":
                    Category = Parameters;
                    break;
                case "keywds":
                    Keywords = Parameters.Split(new char[] { ' ' } );
                    break;
                case "tool":
                    Tool = Parameters;
                    break;
                case "ptime":
                    try
                    {
                        PacketTime = new TimeSpan(Int32.Parse(Parameters) * TimeSpan.TicksPerMillisecond);
                    }
                    catch (FormatException)
                    {
                        return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute ptime field is in the wrong format", part));                
                    }
                    catch (OverflowException)
                    {
                        return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute ptime field is out of range", part));                
                    }
                    break;
                case "maxptime":
                    try
                    {
                        MaxPacketTime = new TimeSpan(Int32.Parse(Parameters) * TimeSpan.TicksPerMillisecond);
                    }
                    catch (FormatException)
                    {
                        return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute maxptime field is in the wrong format", part));                
                    }
                    catch (OverflowException)
                    {
                        return(new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute maxptime field is out of range", part));                
                    }
                    break;
                case "recvonly":
                    ReceiveOnly = true;
                    break;
                case "sendrecv":
                    SendReceive = true;
                    break;
                case "sendonly":
                    SendOnly = true;
                    break;
                case "inactive":
                    Inactive = true;
                    break;
                case "orient":
                    Orientation = Parameters;
                    break;
                case "type":
                    ConferenceType = Parameters;
                    break;
                case "charset":
                    CharacterSet = Parameters;
                    break;
                case "sdplang":
                    SDPLanguage = Parameters;
                    break;
                case "lang":
                    Language = Parameters;
                    break;
                case "framerate":
                    try
                    {
                        FrameRate = Int32.Parse(Parameters);
                    }
                    catch (FormatException)
                    {
                        return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute framerate field is in the wrong format", part));                
                    }
                    catch (OverflowException)
                    {
                        return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute framerate field is out of range", part));                
                    }
                    break;
                case "quality":
                    try
                    {
                        Quality = Int32.Parse(Parameters);
                    }
                    catch (FormatException)
                    {
                        return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute quality field is in the wrong format", part));                
                    }
                    catch (OverflowException)
                    {
                        return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute quality field is out of range", part));                
                    }
                    break;
                case "fmtp":
                    int formatIndex = Parameters.IndexOf(' ');
                    if (formatIndex == -1)
                        return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute fmtp field is in the wrong format", part));                
                    
                    string identity = Parameters.Substring(0, formatIndex);
                    Format = MediaFormat.GetInstance(identity);
                    
                    ErrorSpec formatReply = Format.Process(Parameters);
                    if (formatReply != null)
                        return (formatReply);
                    break;
                case "control":
                    Control = ControlAttribute.GetInstance();
                    ErrorSpec controlReply = Control.Process(Parameters);
                    if (controlReply != null)
                        return (controlReply);
                    break;
                case "range":
                    ErrorSpec rangeReply = processRange(Parameters);
                    if (rangeReply != null)
                        return (rangeReply);
                    break;
                case "etag":
                    Etag = Parameters;
                    break;
                default:
                    break;
            }

            return (null);
        }

        private ErrorSpec processRange(string parameters)
        {
            int rangeIndex = Parameters.IndexOf("=");
            if (rangeIndex == -1)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute range field is in the wrong format", parameters));                

            RangeUnits = parameters.Substring(0, rangeIndex);

            string[] rangeParts = parameters.Substring(rangeIndex + 1).Split(new char[] { '-' });
            if (rangeParts.Length != 2)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute range field is in the wrong format", parameters));                

            if (RangeUnits == "npt")
            {
                try
                {
                    RangeStart = Int32.Parse(rangeParts[0]);
                    RangeEnd = Int32.Parse(rangeParts[1]);
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute range field is in the wrong format", parameters));                
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute range field is out of range", parameters));                
                }
            }
            else
            {
                if (RangeUnits == "clock")
                {
                    RangeDateStart = rangeParts[0];
                    RangeDateEnd = rangeParts[1];                    
                }
                else
                    return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute range field is in the wrong format", parameters));                

            }

            return (null);
        }
    }
}

