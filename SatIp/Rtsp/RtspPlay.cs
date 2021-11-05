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

using SatIpDomainObjects;

namespace Rtsp
{
    /// <summary>
    /// The class that describes an RTSP Play message.
    /// </summary>
    public class RtspPlay : RtspMessageBase
    {
        /// <summary>
        /// Get the list of RTPInfo instances.
        /// </summary>
        public Collection<RtpInfo> RtpInfos { get; private set; }
        /// <summary>
        /// Get the speed.
        /// </summary>
        public decimal Speed { get; private set; }

        /// <summary>
        /// Initialize a new instance of the RtspPlay class.
        /// </summary>
        public RtspPlay() { }

        /// <summary>
        /// Send a Play request and receive and process the response. 
        /// </summary>
        /// <param name="hostAddress">The server address.</param>
        /// <param name="hostPort">The server port.</param>
        /// <param name="cseq">The sequence number of the message.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="streamId">The stream identifier.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string hostAddress, int hostPort, int cseq, int streamId, string sessionId)
        {
            return (Process(hostAddress, hostPort, cseq, streamId, sessionId, null));
        }

        /// <summary>
        /// Send an Play request with tuning parameters and receive and process the response. 
        /// </summary>
        /// <param name="hostAddress">The server address.</param>
        /// <param name="hostPort">The server port.</param>
        /// <param name="cseq">The sequence number of the message.</param>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="streamId">The stream identifier.</param>
        /// <param name="tuningParameters">The tuning parameters.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string hostAddress, int hostPort, int cseq, int streamId, string sessionId, TuningParameters tuningParameters)
        {
            MemoryStream outputStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(outputStream);

            if (streamId != -1)
                streamWriter.Write("PLAY rtsp://" + hostAddress + ":" + hostPort + "/stream=" + streamId);
            else
                streamWriter.Write("PLAY rtsp://" + hostAddress + ":" + hostPort + "/EPGCollector");

            if (tuningParameters != null)
                streamWriter.Write("?" + tuningParameters.ToString());

            streamWriter.WriteLine(" RTSP/1.0");
 
            streamWriter.WriteLine("Cseq:" + cseq);
            streamWriter.WriteLine("Session:" + sessionId);
            streamWriter.WriteLine("Connection:close");
            streamWriter.WriteLine(string.Empty);
            streamWriter.Close();

            TcpClient tcpClient = new TcpClient(hostAddress, hostPort);
            NetworkStream networkStream = tcpClient.GetStream();

            byte[] requestMessage = outputStream.ToArray();
            SatIpLogger.Instance.LogReply("RTSP PLAY request: ", requestMessage, requestMessage.Length);
            networkStream.Write(requestMessage, 0, requestMessage.Length);

            byte[] responseMessage = new byte[1024];
            int byteCount = networkStream.Read(responseMessage, 0, 1024);

            networkStream.Close();
            tcpClient.Close();

            SatIpLogger.Instance.LogReply("RTSP PLAY response: ", responseMessage, byteCount); 

            MemoryStream replyStream = new MemoryStream(responseMessage, 0, byteCount);
            StreamReader streamReader = new StreamReader(replyStream);

            ErrorSpec error = StatusCode.Process(streamReader, RtspProtocolId, RtspProtocolVersion);
            if (error != null)
                return (error);

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                if (!string.IsNullOrWhiteSpace(line))
                {
                    int index = line.IndexOf(':');
                    if (index == -1)
                        return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Response format incorrect", line));

                    string identity = line.Substring(0, index);
                    string parameters;

                    if (index + 1 < line.Length)
                        parameters = line.Substring(index + 1).Trim();
                    else
                        parameters = string.Empty;

                    switch (identity.ToLowerInvariant())
                    {
                        case "rtp-info":
                            error = processRtpInfo(parameters);
                            break;
                        case "speed":
                            error = processSpeed(parameters);
                            break;
                        default:
                            error = base.ProcessLine(line, cseq);
                            break;
                    }

                    if (error != null)
                    {
                        streamReader.Close();
                        return (error);
                    }
                }
            }

            streamReader.Close();

            return (null);
        }

        private ErrorSpec processRtpInfo(string parameters)
        {
            RtpInfos = new Collection<RtpInfo>();

            string[] parts = parameters.Split(new char[] { ',' } );

            foreach (string part in parts)
            {
                RtpInfo rtpInfo = new RtpInfo();
                ErrorSpec error = rtpInfo.Process(part);
                if (error != null)
                    return (error);
                else
                    RtpInfos.Add(rtpInfo);
            }

            return (null);
        }

        private ErrorSpec processSpeed(string parameters)
        {
            try
            {
                Speed = decimal.Parse(parameters);
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Speed format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Speed out of range", parameters));
            }
        }
    }
}

