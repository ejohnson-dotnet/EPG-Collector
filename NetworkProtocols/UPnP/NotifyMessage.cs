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

namespace NetworkProtocols.UPnP
{
    /// <summary>
    /// The class that processes NOTIFY messages.
    /// </summary>
    public class NotifyMessage : UPnPMessage
    {
        /// <summary>
        /// Get the server address.
        /// </summary>
        public string HostAddress { get; private set; }
        /// <summary>
        /// Get the server port.
        /// </summary>
        public int HostPort { get; private set; }
        /// <summary>
        /// Get the notification type.
        /// </summary>
        public string NotificationType { get; private set; }
        /// <summary>
        /// Get the NTS value.
        /// </summary>
        public string NTS { get; private set; }
        
        /// <summary>
        /// Get the list of notifications.
        /// </summary>
        public static Collection<UPnPMessage> Notifications;

        /// <summary>
        /// Initialize a new instance of the NotifyMessage class.
        /// </summary>
        public NotifyMessage() { }

        /// <summary>
        /// Receive and process a NOTIFY message.
        /// </summary>
        /// <param name="socket">The socket to use for receiving.</param>
        /// <param name="timeout">The timeout to use in milliseconds.</param>
        /// <param name="notificationType">The notification type expected.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Receive(Socket socket, int timeout, string notificationType)
        {
            byte[] inputBuffer = new byte[1000];

            EndPoint remoteEndPoint = (EndPoint)new IPEndPoint(MulticastAddress, MulticastPort);

            socket.ReceiveTimeout = timeout;
            int receiveCount = 0;

            try
            {
                receiveCount = socket.ReceiveFrom(inputBuffer, ref remoteEndPoint);
            }
            catch (SocketException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Notify receive failed", e.Message));
            }

            NetworkLogger.Instance.LogReply("Notify message: ", inputBuffer, receiveCount);            

            ErrorSpec reply = Process(inputBuffer, receiveCount);
            if (reply != null)
                return (reply);
            
            NetworkLogger.Instance.Write("Notify message: Processed successfully");

            if (notificationType != null && !NotificationType.StartsWith(notificationType))
                return (null);
            
            if (Location == null)
                return(null);
            
            NetworkLogger.Instance.Write("Notify message: Loading description");

            WebClient webClient = new WebClient();
            byte[] response = webClient.DownloadData(Location);

            NetworkLogger.Instance.LogReply("Notify description: ", response, response.Length); 
            
            Description = new Description();
            ErrorSpec descriptionReply = Description.Process(response, response.Length);
            if (descriptionReply != null)
                return (descriptionReply);

            if (Notifications == null)
                Notifications = new Collection<UPnPMessage>();
            bool newDevice = AddResponse(Notifications, this);

            if (newDevice && Description.Root.Devices != null)
            {
                foreach (Device device in Description.Root.Devices)
                    NetworkLogger.Instance.Write("Notify device: " + device.FriendlyName + " loaded");
            }

            return (null);
        }    

        /// <summary>
        /// Process a NOTIFY message.
        /// </summary>
        /// <param name="buffer">The buffer containing the message.</param>
        /// <param name="count">The length of the message.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(byte[] buffer, int count)
        {
            MemoryStream memoryStream = new MemoryStream(buffer, 0, count);
            StreamReader streamReader = new StreamReader(memoryStream);

            if (streamReader.EndOfStream)
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.UnexpectedEndOfMessage, 0, "NOTIFY unexpected end of response stream", null));

            string line = streamReader.ReadLine();
            if (line.Trim() != "NOTIFY * HTTP/1.1")
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "NOTIFY response line 1 not valid", line));              

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();

                if (!string.IsNullOrWhiteSpace(line))
                {
                    int index = line.IndexOf(":");
                    if (index == -1)
                        return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "NOTIFY response line separator wrong", line));
                    else
                    {
                        string identifier = line.Substring(0, index).Trim();
                        string parameters = line.Substring(index + 1).Trim();

                        ErrorSpec reply = null;

                        switch (identifier.ToUpperInvariant())
                        {
                            case "HOST":
                                reply = processHost(parameters);
                                break;
                            case "CACHE-CONTROL":
                                reply = ProcessCacheControl(parameters);
                                break;
                            case "LOCATION":
                                reply = ProcessLocation(parameters);
                                break;
                            case "NT":
                                reply = processNotificationType(parameters);
                                break;
                            case "NTS":
                                reply = processNTS(parameters);
                                break;
                            case "SERVER":
                                reply = ProcessServer(parameters);
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
                            case "SEARCHPORT.UPNP.ORG":
                                reply = ProcessSearchPort(parameters);
                                break;
                            case "DEVICEID.SES.COM":
                                reply = ProcessDeviceID(parameters);
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

            return (null);
        }

        private ErrorSpec processHost(string parameters)
        {
            string[] parts = parameters.Split(new char[] { ':' });
            if (parts.Length > 2)
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "NOTIFY host  has the wrong number of fields", parameters));

            if (parts.Length == 1)
            {
                HostPort = -1;
                HostAddress = parts[0].Trim();
            }
            else
            {
                try
                {
                    HostPort = Int32.Parse(parts[1].Trim());
                    HostAddress = parts[0].Trim();
                }
                catch (FormatException) 
                {
                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "NOTIFY host port number is in the wrong format", parameters));
                }
                catch (OverflowException) 
                {
                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "NOTIFY host port number is out of range", parameters));
                }
            }

            return (null);
        }

        private ErrorSpec processNotificationType(string parameters)
        {
            NotificationType = parameters.Trim();
            return (null);
        }

        private ErrorSpec processNTS(string parameters)
        {
            NTS = parameters.Trim();
            return (null);
        }
    }
}

