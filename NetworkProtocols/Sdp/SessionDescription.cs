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

using System.Collections.ObjectModel;
using System.IO;

namespace NetworkProtocols.Sdp
{
    /// <summary>
    /// The class that describes a session description.
    /// </summary>
    public class SessionDescription
    {
        /// <summary>
        /// Get the protocol identity.
        /// </summary>
        public static string SdpProtocolId { get { return ("SDP"); } }

        /// <summary>
        /// Get the protocol version.
        /// </summary>
        public string ProtocolVersion { get; private set; }
        /// <summary>
        /// Get the origin.
        /// </summary>
        public Origin Origin { get; private set; }
        /// <summary>
        /// Get the session name.
        /// </summary>
        public SessionName SessionName { get; private set; }

        /// <summary>
        /// Get the description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Get the Url.
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// Get the email address.
        /// </summary>
        public string EmailAddress { get; private set; }
        /// <summary>
        /// Get the phone number.
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// Get the connection data.
        /// </summary>
        public ConnectionData ConnectionData { get; private set; }
        /// <summary>
        /// Get the bandwidth data.
        /// </summary>
        public BandwidthData BandwidthData { get; private set; }
        /// <summary>
        /// Get the encryption key.
        /// </summary>
        public EncryptionKey EncryptionKey { get; private set; }

        /// <summary>
        /// Get the list of media descriptions.
        /// </summary>
        public Collection<MediaDescription> MediaDescriptions { get; private set; }
        /// <summary>
        /// Get the list of time descriptions.
        /// </summary>
        public Collection<TimeDescription> TimeDescriptions { get; private set; }
        /// <summary>
        /// Get the list of time zome adjustments.
        /// </summary>
        public Collection<TimeZoneAdjustment> TimeZoneAdjustments { get; private set; }
        /// <summary>
        /// Get the list of attributes.
        /// </summary>
        public Collection<SDPAttribute> Attributes { get; private set; }

        private MediaDescription mediaDescription;
        private TimeDescription timeDescription;

        /// <summary>
        /// Initialize a new instance of the SessionDescription class.
        /// </summary>
        public SessionDescription() { }

        /// <summary>
        /// Parse the session description data.
        /// </summary>
        /// <param name="part">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(StreamReader streamReader)
        {
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                string identity = line.Substring(0, 2);
                string parameters = line.Substring(2).TrimEnd(); ;

                ErrorSpec error = null;

                switch (identity)
                {
                    case "v=":
                        error = processVersion(parameters);
                        break;
                    case "o=":
                        Origin = new Origin();
                        error = Origin.Process(parameters);
                        break;
                    case "s=":
                        SessionName = new SessionName();
                        error = SessionName.Process(parameters);
                        break;
                    case "i=":
                        error = processSessionInformation(parameters);
                        break;
                    case "u=":
                        error = processUrl(parameters);
                        break;
                    case "e=":
                        error = processEmailAddress(parameters);
                        break;
                    case "p=":
                        error = processPhoneNumber(parameters);
                        break;
                    case "c=":
                        error = processConnectionData(parameters);
                        break;
                    case "b=":
                        error = processBandwidth(parameters);
                        break;
                    case "t=":
                        error = processTimeDescription(parameters);
                        break;
                    case "r=":
                        error = processRepeatDescription(parameters);
                        break;
                    case "z=":
                        error = processTimeZones(parameters);
                        break;
                    case "k=":
                        EncryptionKey = new EncryptionKey();
                        error = EncryptionKey.Process(parameters);
                        break;
                    case "m=":
                        error = processMediaDescription(parameters);                        
                        break;
                    case "a=":
                        error = processAttribute(parameters);
                        break;
                    default:
                        break;
                }

                if (error != null)
                    return (error);
            }

            return (null);
        }

        private ErrorSpec processVersion(string part)
        {
            ProtocolVersion = part;
            return (null);
        }

        private ErrorSpec processSessionInformation(string part)
        {
            Description = part;
            return(null);
        }

        private ErrorSpec processUrl(string part)
        {
            Url = part;
            return (null);
        }

        private ErrorSpec processEmailAddress(string part)
        {
            EmailAddress = part;
            return (null);
        }

        private ErrorSpec processPhoneNumber(string part)
        {
            PhoneNumber = part;
            return (null);
        }

        private ErrorSpec processConnectionData(string parameters)
        {
            ConnectionData connectionData = new ConnectionData();
            ErrorSpec error = connectionData.Process(parameters);
            if (error == null)
            {
                if (mediaDescription == null)
                    ConnectionData = connectionData;
                else
                    mediaDescription.ConnectionData = connectionData;
            }

            return (error);
        }

        private ErrorSpec processBandwidth(string parameters)
        {
            BandwidthData bandwidthData = new BandwidthData();
            ErrorSpec error = bandwidthData.Process(parameters);
            if (error == null)
            {
                if (mediaDescription == null)
                    BandwidthData = bandwidthData;
                else
                    mediaDescription.BandwidthData = bandwidthData;
            }

            return (error);
        }

        private ErrorSpec processTimeDescription(string part)
        {
            TimeDescription time = new TimeDescription();
            ErrorSpec reply = time.Process(part);
            if (reply == null)
            {
                if (TimeDescriptions == null)
                    TimeDescriptions = new Collection<TimeDescription>();
                TimeDescriptions.Add(time);
                timeDescription = time;
            }
            else
                timeDescription = null;

            return (reply);
        }

        private ErrorSpec processRepeatDescription(string part)
        {
            if (timeDescription == null)
                return (null);

            RepeatDescription repeatDescription = new RepeatDescription();
            ErrorSpec reply = repeatDescription.Process(part);
            if (reply == null)
            {
                if (timeDescription.Repeats == null)
                    timeDescription.Repeats = new Collection<RepeatDescription>();
                timeDescription.Repeats.Add(repeatDescription);
            }

            return (reply);
        }

        private ErrorSpec processTimeZones(string part)
        {
            string[] parts = part.Split(new char[] { ' ' });

            if (parts.Length < 2 || parts.Length % 2 != 0)
                return (new ErrorSpec(SdpProtocolId, ErrorCode.FormatError, 0, "The 'z' attribute is in the wrong format", part));                

            for (int index = 0; index < parts.Length; index += 2)
            {
                TimeZoneAdjustment timeZoneAdjustment = new TimeZoneAdjustment();
                ErrorSpec reply = timeZoneAdjustment.Process(parts[index], parts[index + 1]);
                if (reply == null)
                {
                    if (TimeZoneAdjustments == null)
                        TimeZoneAdjustments = new Collection<TimeZoneAdjustment>();
                    TimeZoneAdjustments.Add(timeZoneAdjustment);
                }
                else
                    return (reply);
            }

            return (null);
        }

        private ErrorSpec processMediaDescription(string part)
        {
            MediaDescription media = new MediaDescription();
            ErrorSpec reply = media.Process(part);
            if (reply == null)
            {
                if (MediaDescriptions == null)
                    MediaDescriptions = new Collection<MediaDescription>();
                MediaDescriptions.Add(mediaDescription);
                mediaDescription = media;
            }
            else
                mediaDescription = null;

            return (reply);
        }

        private ErrorSpec processAttribute(string parameters)
        {
            SDPAttribute attribute = new SDPAttribute();
            ErrorSpec error = attribute.Process(parameters);
            if (error == null)
            {
                if (mediaDescription == null)
                {
                    if (Attributes == null)
                        Attributes = new Collection<SDPAttribute>();
                    Attributes.Add(attribute);
                }
                else
                {
                    if (mediaDescription.Attributes == null)
                        mediaDescription.Attributes = new Collection<SDPAttribute>();
                    mediaDescription.Attributes.Add(attribute);
                }
            }

            return (error);
        }
    }
}

