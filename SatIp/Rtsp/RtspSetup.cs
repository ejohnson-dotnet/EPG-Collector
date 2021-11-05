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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using SatIpDomainObjects;

namespace Rtsp
{
    /// <summary>
    /// The class that describes an RTSP Setup message.
    /// </summary>
    public class RtspSetup : RtspMessageBase
    {
        /// <summary>
        /// Get the destination address.
        /// </summary>
        public string Destination { get; private set; }
        /// <summary>
        /// Get the source address.
        /// </summary>
        public string Source { get; private set; }
        /// <summary>
        /// Get the number of layers.
        /// </summary>
        public int Layers { get; private set; }
        /// <summary>
        /// Get the mode.
        /// </summary>
        public string Mode { get; private set; }
        /// <summary>
        /// Get the append setting.
        /// </summary>
        public bool Append { get; private set; }
        /// <summary>
        /// Get the value for interleaved channel 1.
        /// </summary>
        public int InterleavedChannel1 { get; private set; }
        /// <summary>
        /// Get the value for interleaved channel 2.
        /// </summary>
        public int InterleavedChannel2 { get; private set; }
        /// <summary>
        /// Get the multicast time to live value.
        /// </summary>
        public int MulticastTimeToLive { get; private set; }
        /// <summary>
        /// Get the multicast data port.
        /// </summary>
        public int MulticastDataPort { get; private set; }
        /// <summary>
        /// Get the multicast control port.
        /// </summary>
        public int MulticastControlPort { get; private set; } 
        /// <summary>
        /// Get the unicast client data port.
        /// </summary>
        public int UnicastClientDataPort { get; private set; }
        /// <summary>
        /// Get the unicast client control port.
        /// </summary>
        public int UnicastClientControlPort { get; private set; }        
        /// <summary>
        /// Get the unicast server data port.
        /// </summary>
        public int UnicastServerDataPort { get; private set; }
        /// <summary>
        /// Get the unicast server control port.
        /// </summary>
        public int UnicastServerControlPort { get; private set; }
        /// <summary>
        /// Get the synchronizing source.
        /// </summary>
        public string Ssrc { get; private set; }

        /// <summary>
        /// Initialize a new instance of the RtspSetup class.
        /// </summary>
        public RtspSetup() { }

        /// <summary>
        /// Send a Setup request and receive and process the response.
        /// </summary>
        /// <param name="hostAddress">The server address.</param>
        /// <param name="hostPort">The server port.</param>
        /// <param name="cseq">The sequence number to be used for the message.</param>
        /// <param name="useMulticast">True to use multicasting, false for unicasting.</param>
        /// <param name="multicastAddress">The multicast address.</param>
        /// <param name="rtpPort">The client data port.</param>
        /// <param name="rtcpPort">The client control port.</param>
        /// <param name="ttl">The time to live value.</param>
        /// <param name="tuningParameters">The tuning parameters.</param>
        /// <param name="path">The setup path.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string hostAddress, int hostPort, int cseq, bool useMulticast, IPAddress multicastAddress, int rtpPort, int rtcpPort, int ttl, TuningParameters tuningParameters, string path)
        {
            MemoryStream outputStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(outputStream);

            streamWriter.Write("SETUP rtsp://" + hostAddress + ":" + hostPort + "/");

            if (tuningParameters != null)
                streamWriter.Write("?" + tuningParameters.ToString());
            else
            {
                if (path != null)
                    streamWriter.Write(path + (path.EndsWith("/") ? "" : "?"));
                else
                    streamWriter.Write("/");
                streamWriter.Write("trackID=0");
            }

            streamWriter.WriteLine(" RTSP/1.0");

            streamWriter.WriteLine("Cseq:" + cseq);

            streamWriter.Write("Transport:RTP/AVP;");

            if (!useMulticast)
                streamWriter.WriteLine("unicast;client_port=" + rtpPort + "-" + rtcpPort);
            else
            {
                streamWriter.Write("multicast;");
                
                if (multicastAddress != null)
                    streamWriter.Write("destination=" + multicastAddress.ToString() + ";");
                
                streamWriter.Write("port=" + rtpPort + "-" + rtcpPort);
                
                if (ttl != 0)
                    streamWriter.Write(";ttl=" + ttl);
                
                streamWriter.WriteLine(string.Empty);
            }

            streamWriter.WriteLine("Connection:close");

            streamWriter.WriteLine("");
            streamWriter.Close();

            SatIpLogger.Instance.Write("Sending SETUP message to " + hostAddress + " port " + hostPort);

            TcpClient tcpClient;

            try
            {
                tcpClient = new TcpClient(hostAddress, hostPort);
            }
            catch (SocketException e)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.NotFound, 0, "Socket exception", e.Message));
            }

            NetworkStream networkStream = tcpClient.GetStream();

            byte[] requestMessage = outputStream.ToArray();
            SatIpLogger.Instance.LogReply("RTSP SETUP request: ", requestMessage, requestMessage.Length);
            networkStream.Write(requestMessage, 0, requestMessage.Length);

            byte[] responseMessage = new byte[1024];
            DateTime startTime = DateTime.Now;            

            while ((DateTime.Now - startTime).TotalSeconds < 10 && !networkStream.DataAvailable)
                Thread.Sleep(500);

            if (!networkStream.DataAvailable)
            {
                networkStream.Close();
                tcpClient.Close();
                return (new ErrorSpec(RtspProtocolId, ErrorCode.TimedOut, 0, "No response to Setup request", null));
            }
            
            int byteCount = networkStream.Read(responseMessage, 0, 1024);

            networkStream.Close();
            tcpClient.Close();

            SatIpLogger.Instance.LogReply("RTSP SETUP response: ", responseMessage, byteCount); 

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
                        case "transport":
                            error = processTransport(parameters);
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

            if (Source == null)
                Source = hostAddress;

            return (null);
        }

        private ErrorSpec processTransport(string parameters)
        {
            string[] restParts = parameters.Split(new char[] { ';' });
            if (restParts.Length < 4)
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport format incorrect", parameters));

            if (restParts[0] != "RTP/AVP" && restParts[0] != "RTP/AVP/UDP")
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport format incorrect", parameters));

            if (restParts[1] != "unicast" && restParts[1] != "multicast")
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport format incorrect", parameters));

            ErrorSpec error = null;

            for (int index = 2; index < restParts.Length; index++)
            {
                string identity;
                string entryParameters;

                int identityIndex = restParts[index].IndexOf('=');
                if (identityIndex == -1)
                {
                    identity = restParts[index].Trim();
                    entryParameters = null;
                }
                else
                {
                    identity = restParts[index].Substring(0, identityIndex).Trim();
                    entryParameters = restParts[index].Substring(identityIndex + 1).Trim();
                }

                switch (identity.ToLowerInvariant())
                {
                    case "destination":
                        Destination = entryParameters;
                        break;
                    case "source":
                        Source = entryParameters;
                        break;
                    case "layers":
                        error = processLayers(entryParameters);                        
                        break;
                    case "mode":
                        Mode = entryParameters;
                        break;
                    case "append":
                        Append = true;
                        break;
                    case "interleaved":
                        error = processInterleaved(entryParameters);
                        break;
                    case "ttl":
                        error = processTimeToLive(entryParameters);
                        break;
                    case "port":
                        error = processPort(entryParameters);
                        break;
                    case "client_port":
                        error = processClientPort(entryParameters);
                        break;
                    case "server_port":
                        error = processServerPort(entryParameters);
                        break;
                    case "ssrc":
                        Ssrc = entryParameters;
                        break;
                    default:
                        break;
                }

                if (error != null)
                    return (error);
            }            
            
            return (null);
        }

        private ErrorSpec processLayers(string parameters)
        {
            try
            {
                Layers = Int32.Parse(parameters);
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport layers format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport layers out of range", parameters));
            }
        }

        private ErrorSpec processInterleaved(string parameters)
        {
            string[] channels = parameters.Split(new char[] { '-' });
            if (channels.Length > 2)
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport interleaved format incorrect", parameters));
            
            try
            {
                InterleavedChannel1 = Int32.Parse(channels[0].Trim());
                if (channels.Length == 2)
                    InterleavedChannel2 = Int32.Parse(channels[1].Trim());
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport interleaved format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport interleaved out of range", parameters));
            }
        }

        private ErrorSpec processTimeToLive(string parameters)
        {
            try
            {
                MulticastTimeToLive = Int32.Parse(parameters);
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport TTL format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport TTL out of range", parameters));
            }
        }

        private ErrorSpec processPort(string parameters)
        {
            string[] ports = parameters.Split(new char[] { '-' });
            if (ports.Length != 2)
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport port format incorrect", parameters));

            try
            {
                MulticastDataPort = Int32.Parse(ports[0].Trim());
                MulticastControlPort = Int32.Parse(ports[1].Trim());
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport port format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport port out of range", parameters));
            }
        }

        private ErrorSpec processClientPort(string parameters)
        {
            string[] ports = parameters.Split(new char[] { '-' });
            if (ports.Length != 2)
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport client port format incorrect", parameters));

            try
            {
                UnicastClientDataPort = Int32.Parse(ports[0].Trim());
                UnicastClientControlPort = Int32.Parse(ports[1].Trim());
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport client port format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport client port out of range", parameters));
            }
        }

        private ErrorSpec processServerPort(string parameters)
        {
            string[] ports = parameters.Split(new char[] { '-' });
            if (ports.Length != 2)
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport server port format incorrect", parameters));

            try
            {
                UnicastServerDataPort = Int32.Parse(ports[0].Trim());
                UnicastServerControlPort = Int32.Parse(ports[1].Trim());
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport server port format incorrect", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(RtspProtocolId, ErrorCode.FormatError, 0, "Transport server port out of range", parameters));
            }
        }
    }
}

