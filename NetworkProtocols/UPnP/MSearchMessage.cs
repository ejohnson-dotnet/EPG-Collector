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
using System.Net;
using System.Net.Sockets;

using DomainObjects;

namespace NetworkProtocols.UPnP
{
    /// <summary>
    /// The class that processes MSEARCH messages.
    /// </summary>
    public class MSearchMessage : UPnPMessage
    {
        /// <summary>
        /// Get the date and time.
        /// </summary>
        public string Date { get; private set; }
        /// <summary>
        /// Get the search target.
        /// </summary>
        public string SearchTarget { get; protected set; }

        /// <summary>
        /// Initialize a new instance of the MSearchMessage class.
        /// </summary>
        public MSearchMessage() { }

        /// <summary>
        /// Send an MSEARCH request and process the response.
        /// </summary>
        /// <param name="address">The server address.</param>
        /// <param name="port">The server port.</param>
        /// <param name="sendSocket">The socket to use to send the request.</param>
        /// <param name="receiveSocket">The socket to use to receive the response.</param>
        /// <param name="searchTarget">The search target.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec SendReceive(IPAddress address, int port, Socket sendSocket, Socket receiveSocket, string searchTarget)
        {
            ErrorSpec reply = Send(address, port, sendSocket, searchTarget);
            if (reply == null)
                reply = Receive(receiveSocket, 2500);

            return (reply);
        }

        /// <summary>
        /// Send an MSEARCH request.
        /// </summary>
        /// <param name="address">The server address.</param>
        /// <param name="port">The server port.</param>
        /// <param name="socket">The socket to use to send the request.</param>
        /// <param name="searchTarget">The search target.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Send(IPAddress address, int port, Socket socket, string searchTarget)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            streamWriter.NewLine = "\r\n";

            streamWriter.WriteLine("M-SEARCH * HTTP/1.1");
            streamWriter.WriteLine("HOST: " + address + ":" + port);
            streamWriter.WriteLine(@"MAN: ""ssdp:discover""");
            streamWriter.WriteLine("MX: 2");
            if (searchTarget == null)
                streamWriter.WriteLine("ST: ssdp:all");
            else
                streamWriter.WriteLine("ST: " + searchTarget);
            streamWriter.WriteLine("");
            streamWriter.Close();

            byte[] requestMessage = memoryStream.ToArray();
            NetworkLogger.Instance.LogReply("M-SEARCH request: ", requestMessage, requestMessage.Length);

            try
            {
                EndPoint remoteEndPoint = (EndPoint)new IPEndPoint(address, port);
                socket.SendTo(requestMessage, remoteEndPoint);
            }
            catch (SocketException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, e.ErrorCode, "M-SEARCH send failed", e.Message));
            }

            return (null);
        }

        private ErrorSpec Receive(Socket socket, int timeout)
        {
            byte[] inputBuffer = new byte[1000];

            EndPoint remoteEndPoint = (EndPoint)new IPEndPoint(MulticastAddress, MulticastPort);
            socket.ReceiveTimeout = timeout;  // Relates to 2 secs set with MX: above

            int receiveCount = 0;

            try
            {
                receiveCount = socket.ReceiveFrom(inputBuffer, ref remoteEndPoint);
            }
            catch (SocketException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, e.ErrorCode, "M-SEARCH receive failed", e.Message));
            }

            serverEndPoint = remoteEndPoint as IPEndPoint;

            NetworkLogger.Instance.LogReply("M-SEARCH response: ", inputBuffer, receiveCount);
            return (Process(inputBuffer, receiveCount));
        }

        /// <summary>
        /// Process an MSEARCH response.
        /// </summary>
        /// <param name="buffer">The buffer containing the response.</param>
        /// <param name="count">The length of the response.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(byte[] buffer, int count)
        {
            MemoryStream memoryStream = new MemoryStream(buffer, 0, count);
            StreamReader streamReader = new StreamReader(memoryStream);

            if (streamReader.EndOfStream)
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.UnexpectedEndOfMessage, 0, "M-SEARCH unexpected end of response stream", null));

            string line = streamReader.ReadLine();
            if (line.Trim() != "HTTP/1.1 200 OK")
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "M-SEARCH response line 1 not valid", line));

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();

                if (!string.IsNullOrWhiteSpace(line))
                {
                    int index = line.IndexOf(":");
                    if (index == -1)
                        return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "M-SEARCH response line separator wrong", line));
                    else
                    {
                        string identifier = line.Substring(0, index).Trim();
                        string parameters = line.Substring(index + 1).Trim();

                        ErrorSpec reply = null;

                        switch (identifier.ToUpperInvariant())
                        {
                            case "CACHE-CONTROL":
                                reply = ProcessCacheControl(parameters);
                                break;
                            case "DATE":
                                reply = processDate(parameters);
                                break;
                            case "EXT":
                                break;
                            case "LOCATION":
                                reply = ProcessLocation(parameters);
                                break;
                            case "SERVER":
                                reply = ProcessServer(parameters);
                                break;
                            case "ST":
                                reply = processSearchTarget(parameters);
                                break;
                            case "USN":
                                reply = ProcessUniqueServiceName(parameters);
                                break;
                            case "BOOTID.UPNP.ORG":
                                reply = ProcessBootID(parameters);
                                break;
                            case "CONFIGID.UPNP.ORG":
                                reply = ProcessConfigID(parameters);
                                break;
                            case "DEVICEID.SES.COM":
                                reply = ProcessDeviceID(parameters);
                                break;
                            case "SEARCHPORT.UPNP.ORG":
                                reply = ProcessSearchPort(parameters);
                                break;
                            default:
                                break;                                
                        }

                        if (reply != null)
                            return (reply);
                    }
                }
            }

            streamReader.Close();

            if (Location == null)
                return (null);

            NetworkLogger.Instance.Write("M-SEARCH response: Loading description");

            WebClient webClient = new WebClient();
            byte[] response = null;

            try
            {
                response = webClient.DownloadData(Location);
            }
            catch (WebException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, -1, "M-SEARCH loading description failed", e.Message));
            }

            NetworkLogger.Instance.LogReply("M-SEARCH description: ", response, response.Length);
            
            Description = new Description();
            ErrorSpec descriptionReply = Description.Process(response, response.Length);
            if (descriptionReply != null)
            {
                descriptionReply.OriginalErrorCode = -1;
                return (descriptionReply);
            }

            return (null);
        }

        private ErrorSpec processDate(string parameters)
        {
            Date = parameters.Trim();
            return (null);
        }

        private ErrorSpec processSearchTarget(string parameters)
        {
            SearchTarget = parameters.Trim();
            return (null);
        }

        /// <summary>
        /// Search for UpnP servers.
        /// </summary>
        /// <param name="searchTarget">The search target.</param>
        /// <param name="responses">The list of responses.</param>
        /// <param name="uuid">Identifying part of the uuid or null.</param>
        /// <param name="type">The type of server.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public static ErrorSpec SearchForServers(string searchTarget, Collection<UPnPMessage> responses, string uuid, StreamServerType type)
        {
            NetworkLogger.Instance.Write("Searching for " + type + " servers");

            Socket socket = null;

            try
            {
                socket = createSocket(type);
            }
            catch (InvalidOperationException e)
            {
                NetworkLogger.Instance.Write("<e> Failed to create socket");
                return (new ErrorSpec(UPnPProtocolId, ErrorCode.Exception, 0, e.Message, null));
            }

            ErrorSpec reply = null;
            int count = 0;

            while (count < 3)
            {
                MSearchMessage searchMessage = new MSearchMessage();
                reply = searchMessage.Send(MulticastAddress, MulticastPort, socket, searchTarget);
                if (reply != null)
                    return (reply);

                count++;
            }

            while (reply == null)
            {
                MSearchMessage searchMessage = new MSearchMessage();
                reply = searchMessage.Receive(socket, 2500);
                if (reply == null)
                {
                    if (checkServer(searchMessage, searchTarget, uuid))
                        AddResponse(responses, searchMessage);
                }
                else
                {
                    NetworkLogger.Instance.Write("<e> Server search error: " + reply);
                    if (reply.OriginalErrorCode == -1)
                    {
                        NetworkLogger.Instance.Write("<e> Server response ignored");
                        reply = null;
                    }
                }

                count++;
            }

            NetworkLogger.Instance.Write("Searching for " + type + " servers returning " + responses.Count);

            if (responses.Count != 0)
                return (null);
            else
                return (reply);
        }

        private static bool checkServer(MSearchMessage searchMessage, string searchTarget, string uuid)
        {
            if (searchMessage.Server.ToUpperInvariant().Contains("WINDOWS") && !searchMessage.Server.ToUpperInvariant().Contains("DVBVIEWER"))
                return (false);

            if (searchTarget == null && uuid == null)
                return (true);

            if (searchTarget != null)
            {
                if (searchMessage.SearchTarget.StartsWith(searchTarget))
                {
                    if (uuid != null)
                        return (searchMessage.UniqueServiceName.Contains(uuid));
                    else
                        return (true);
                }
                else
                    return (false);
            }
            else
                return (searchMessage.UniqueServiceName.Contains(uuid));                           
        }

        private static Socket createSocket(StreamServerType serverType)
        {
            IPAddress localIPAddress = Utils.GetLocalIpAddress(NetworkLogger.Instance, serverType);
            if (localIPAddress == null)
                throw (new InvalidOperationException("No IPv4 address located to set the local IP address"));

            NetworkLogger.Instance.Write("Local IP address set to " + localIPAddress);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);            

            bool found = false;
            int tries = 0;

            while (!found)
            {
                try
                {
                    IPEndPoint localEndPoint = new IPEndPoint(localIPAddress, UPnPClientPort);
                    socket.Bind(localEndPoint);
                    found = true;
                }
                catch (SocketException e)
                {
                    if (tries < 1000)
                    {
                        UPnPClientPort++;
                        tries++;
                    }
                    else
                        throw (new InvalidOperationException(e.Message));

                }
            }

            NetworkLogger.Instance.Write("UPnP client port set to " + UPnPClientPort);

            return (socket);
        }
    }
}

