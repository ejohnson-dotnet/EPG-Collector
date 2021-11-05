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
using System.IO;
using System.Net.Sockets;

namespace NetworkProtocols.Rtsp
{
    /// <summary>
    /// The base class for RTSP messages.
    /// </summary>
    public abstract class RtspMessageBase
    {
        /// <summary>
        /// Get and set the session identity.
        /// </summary>
        public string SessionId { get; protected set; }
        /// <summary>
        /// Get and set the session timeout.
        /// </summary>
        public TimeSpan? SessionTimeout { get; protected set; }
        /// <summary>
        /// Get the stream identifier.
        /// </summary>
        public int StreamId { get; private set; }
        /// <summary>
        /// Get the cache control method.
        /// </summary>
        public string CacheControl { get; private set; }
        /// <summary>
        /// Get the content length.
        /// </summary>
        public int ContentLength { get; private set; }
        /// <summary>
        /// Get the content type.
        /// </summary>
        public string ContentType { get; private set; }
        /// <summary>
        /// Get the content base.
        /// </summary>
        public string ContentBase { get; private set; }
        /// <summary>
        /// Get the content encoding.
        /// </summary>
        public string ContentEncoding { get; private set; }
        /// <summary>
        /// Get the list of content languages.
        /// </summary>
        public Collection<string> ContentLanguages { get; private set; }
        /// <summary>
        /// Get the content location.
        /// </summary>
        public string ContentLocation { get; private set; }
        /// <summary>
        /// Get the list of allowed methods.
        /// </summary>
        public Collection<string> AllowedMethods { get; private set; }        
        /// <summary>
        /// Get the connection string.
        /// </summary>
        public string Connection { get; private set; }
        /// <summary>
        /// Get the date of the message.
        /// </summary>
        public string Date { get; private set; }
        /// <summary>
        /// Get the expiry date of the message.
        /// </summary>
        public string Expires { get; private set; }
        /// <summary>
        /// Get the last modified date of the message.
        /// </summary>
        public string LastModified { get; private set; }
        /// <summary>
        /// Get the list of public methods.
        /// </summary>
        public Collection<string> PublicMethods { get; private set; }
        /// <summary>
        /// Get the retry date.
        /// </summary>
        public string RetryAfterDate { get; private set; }
        /// <summary>
        /// Get the retry time.
        /// </summary>
        public TimeSpan? RetryAfterTime { get; private set; }        
        /// <summary>
        /// Get the scale.
        /// </summary>
        public decimal Scale { get; private set; }        
        /// <summary>
        /// Get the server name.
        /// </summary>
        public string Server { get; private set; }
        /// <summary>
        /// Get or the unsupported functions.
        /// </summary>
        public string Unsupported { get; private set; }
        /// <summary>
        /// Get the indirection string.
        /// </summary>
        public string Via { get; private set; }
        /// <summary>
        /// Get the challenge data.
        /// </summary>
        public string Challenge { get; private set; }

        internal static string RtspProtocolId = "RTSP";
        internal static string RtspProtocolVersion = "1.0";

        protected RtspMessageBase()
        {
            StreamId = -1;
        }

        /// <summary>
        /// Send an RTSP message and receive the response.
        /// </summary>
        /// <param name="description">A textual description of the message for logging purposes.</param>
        /// <param name="requestStream">The request as a byte stream.</param>
        /// <param name="hostAddress">The server address.</param>
        /// <param name="hostPort">The server port.</param>
        /// <returns>A StreamReader instance containing the response.</returns>
        protected StreamReader ProcessRequest(string description, MemoryStream requestStream, string hostAddress, int hostPort)
        {
            NetworkLogger.Instance.Write("Sending " + description.ToUpperInvariant() + " message to " + hostAddress + " port " + hostPort);

            TcpClient tcpClient = new TcpClient(hostAddress, hostPort);
            NetworkStream networkStream = tcpClient.GetStream();

            byte[] requestMessage = requestStream.ToArray();
            NetworkLogger.Instance.LogReply("RTSP " + description.ToUpperInvariant() + " request: ", requestMessage, requestMessage.Length);
            networkStream.Write(requestMessage, 0, requestMessage.Length);

            byte[] responseMessage = new byte[8192];
            int byteCount = networkStream.Read(responseMessage, 0, 8192);

            networkStream.Close();
            tcpClient.Close();

            NetworkLogger.Instance.LogReply("RTSP " + description.ToUpperInvariant() + " response: ", responseMessage, byteCount);

            MemoryStream replyStream = new MemoryStream(responseMessage, 0, byteCount);
            StreamReader streamReader = new StreamReader(replyStream);

            return (streamReader);
        }

        /// <summary>
        /// Process an RTSP message response line.
        /// </summary>
        /// <param name="line">The line to process.</param>
        /// <param name="cseq">The expected sequence number.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        protected ErrorSpec ProcessLine (string line, int cseq)
        {
            if (string.IsNullOrWhiteSpace(line))
                return (null);

            ErrorSpec error = null;

            int index = line.IndexOf(':');
            if (index == -1)
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Line format incorrect", line));

            string identity = line.Substring(0, index);
            string parameters = line.Substring(index + 1).Trim();

            switch (identity.ToLowerInvariant())
            {
                case "allow":
                    error = processAllow(parameters);
                    break;
                case "cache-control":
                    error = processCacheControl(parameters);
                    break;
                case "connection":
                    error = processConnection(parameters);
                    break;
                case "content-base":
                    error = processContentBase(parameters);
                    break;
                case "content-encoding":
                    error = processContentEncoding(parameters);
                    break;
                case "content-length":
                    error = processContentLength(parameters);
                    break;
                case "content-type":
                    error = processContentType(parameters);
                    break;
                case "content-language":
                    error = processContentLanguage(parameters);
                    break;
                case "content-location":
                    error = processContentLocation(parameters);
                    break;
                case "cseq":
                    error = processCseq(parameters, cseq);
                    break;
                case "date":
                    error = processDate(parameters);
                    break;
                case "expires":
                    error = processExpires(parameters);
                    break;
                case "last-modified":
                    error = processLastModified(parameters);
                    break;
                case "public":
                    error = processPublic(parameters);
                    break;
                case "range":
                    error = processRange(parameters);
                    break;
                case "retry-after":
                    error = processRetryAfter(parameters);
                    break;                
                case "scale":
                    error = processScale(parameters);
                    break;
                case "server":
                    error = processServer(parameters);
                    break;
                case "session":
                    error = processSession(parameters);
                    break;                
                case "com.ses.streamid":
                    error = processStreamId(parameters);
                    break;
                case "unsupported":
                    error = processUnsupported(parameters);
                    break;
                case "via":
                    error = processVia(parameters);
                    break;
                case "www-authenticate":
                    error = processAuthenticate(parameters);
                    break;
                default:
                    break;
            }

            return (error);
        }

        private ErrorSpec processAllow(string parameters)
        {
            AllowedMethods = new Collection<string>();

            foreach (string method in parameters.Split(new char[] { ',' }))
                AllowedMethods.Add(method.Trim());

            return (null);
        }

        private ErrorSpec processCacheControl(string parameters)
        {
            CacheControl = parameters;
            return (null);
        }

        private ErrorSpec processConnection(string parameters)
        {
            Connection = parameters;
            return (null);
        }

        private ErrorSpec processCseq(string parameters, int expectedCseq)
        {
            try
            {
                int inputCseq = Int32.Parse(parameters);

                if (inputCseq == expectedCseq)
                    return (null);
                else
                    return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "CSeq expected " + expectedCseq + " got " + inputCseq, parameters));                    
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "CSeq format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "CSeq out of range", parameters));
            }
        }

        private ErrorSpec processSession(string parameters)
        {
            string[] restParts = parameters.Split(new char[] { ';' });
            if (restParts.Length > 2)
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Session format incorrect", parameters));

            SessionId = restParts[0];

            if (restParts.Length == 1)
                return (null);

            string[] lastParts = restParts[1].Trim().Split(new char[] { '=' });
            if (lastParts.Length != 2 || lastParts[0].Trim() != "timeout")
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Session format incorrect", parameters));

            try
            {
                SessionTimeout = new TimeSpan(Int32.Parse(lastParts[1].Trim()) * TimeSpan.TicksPerSecond);
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Session timeout format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Session timeout out of range", parameters));
            }
        }

        private ErrorSpec processStreamId(string parameters)
        {
            try
            {
                StreamId = Int32.Parse(parameters);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Stream ID format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Stream ID out of range", parameters));
            }

            return (null);
        }

        private ErrorSpec processContentLength(string parameters)
        {
            try
            {
                ContentLength = Int32.Parse(parameters);
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Content-length format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Content-length out of range", parameters));
            }
        }

        private ErrorSpec processContentEncoding(string parameters)
        {
            ContentEncoding = parameters;
            return (null);
        }

        private ErrorSpec processContentType(string parameters)
        {
            ContentType = parameters;
            return (null);
        }

        private ErrorSpec processContentBase(string parameters)
        {
            ContentBase = parameters;
            return (null);
        }

        private ErrorSpec processContentLanguage(string parameters)
        {
            ContentLanguages = new Collection<string>();

            foreach (string language in parameters.Split(new char[] { ',' }))
                ContentLanguages.Add(language.Trim());

            return (null);
        }

        private ErrorSpec processContentLocation(string parameters)
        {
            ContentLocation = parameters;
            return (null);
        }

        private ErrorSpec processDate(string parameters)
        {
            Date = parameters;
            return (null);
        }

        private ErrorSpec processExpires(string parameters)
        {
            Expires = parameters;
            return (null);
        }

        private ErrorSpec processLastModified(string parameters)
        {
            LastModified = parameters;
            return (null);            
        }

        private ErrorSpec processPublic(string parameters)
        {
            PublicMethods = new Collection<string>();

            foreach (string method in parameters.Split(new char[] { ',' }))
                PublicMethods.Add(method.Trim());

            return (null);
        }

        private ErrorSpec processRange(string parameters)
        {
            return (null);
        }

        private ErrorSpec processRetryAfter(string parameters)
        {
            RetryAfterDate = parameters;
            return (null);
        }

        private ErrorSpec processScale(string parameters)
        {
            try
            {
                Scale = decimal.Parse(parameters);
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Scale format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Scale out of range", parameters));
            }
        }

        private ErrorSpec processServer(string parameters)
        {
            Server = parameters;
            return (null);
        }

        private ErrorSpec processUnsupported(string parameters)
        {
            Unsupported = parameters;
            return (null);
        }

        private ErrorSpec processVia(string parameters)
        {
            Via = parameters;
            return (null);
        }

        private ErrorSpec processAuthenticate(string parameters)
        {
            Challenge = parameters;
            return (null);
        }
    }
}

