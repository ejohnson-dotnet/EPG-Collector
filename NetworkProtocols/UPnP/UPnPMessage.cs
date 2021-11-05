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
using System.Net;

namespace NetworkProtocols.UPnP
{
    /// <summary>
    /// The base class for UPnP messages.
    /// </summary>
    public abstract class UPnPMessage
    {
        /// <summary>
        /// Get the protocol identification.
        /// </summary>
        public static string UPnPProtocolId { get { return ("UPnP"); } }

        /// <summary>
        /// Get or set the multicast address.
        /// </summary>
        public static IPAddress MulticastAddress { get; set; }
        /// <summary>
        /// Get or set the multicast port.
        /// </summary>
        public static int MulticastPort { get; set; }
        /// <summary>
        /// Get or set the local port.
        /// </summary>
        public static int UPnPClientPort { get; set; }
        
        /// <summary>
        /// Get or set the maximum age.
        /// </summary>
        public int MaxAge { get; protected set; }
        /// <summary>
        /// Get or set the location.
        /// </summary>
        public string Location { get; protected set; }
        /// <summary>
        /// Get or set the server description.
        /// </summary>
        public string Server { get; protected set; }        
        /// <summary>
        /// Get or set the service name.
        /// </summary>
        public string UniqueServiceName { get; protected set; }
        /// <summary>
        /// Get or set to boot identifier.
        /// </summary>
        public int BootID { get; protected set; }
        /// <summary>
        /// Get or set the config identifier.
        /// </summary>
        public int ConfigID { get; protected set; }
        /// <summary>
        /// Get or set the search port.
        /// </summary>
        public int SearchPort { get; protected set; }
        /// <summary>
        /// Get or set the device identifier.
        /// </summary>
        public int DeviceId { get; protected set; }

        /// <summary>
        /// Get or set the description.
        /// </summary>
        public Description Description { get; set; }

        /// <summary>
        /// Get or set the location address.
        /// </summary>
        public string LocationAddress { get { return (getLocationAddress(Location)); } }
        /// <summary>
        /// Get or set the location port.
        /// </summary>
        public int LocationPort { get { return (getLocationPort(Location)); } }

        protected IPEndPoint serverEndPoint;

        /// <summary>
        /// Add a UPnP response to the list.
        /// </summary>
        /// <param name="responses">The list of responses.</param>
        /// <param name="newResponse">The response to be added.</param>
        /// <returns>True if the response was added; false if it was a replacement.</returns>
        public static bool AddResponse(Collection<UPnPMessage> responses, UPnPMessage newResponse)
        {
            if (!newResponse.UniqueServiceName.Contains("::"))
                return (false);
            if (newResponse.UniqueServiceName.EndsWith("upnp:rootdevice"))
                return (false);

            Collection<UPnPMessage> replacedResponses = new Collection<UPnPMessage>();

            foreach (UPnPMessage oldResponse in responses)
            {
                if (oldResponse.serverEndPoint.Address.ToString() == newResponse.serverEndPoint.Address.ToString())
                    replacedResponses.Add(oldResponse);
            }

            foreach (UPnPMessage replacedResponse in replacedResponses)
                responses.Remove(replacedResponse);

            responses.Add(newResponse);

            return (replacedResponses.Count == 0);
        }

        private string getLocationAddress(string location)
        {
            if (location == null || !location.StartsWith("http://"))
                return (null);

            int index = location.IndexOf(":", 7);
            if (index == -1)
            {
                index = location.IndexOf("/", 7);
                if (index == -1)
                    return (location.Substring(7));
            }
            
            return (location.Substring(7, index - 7));
        }

        private int getLocationPort(string location)
        {
            if (location == null || !location.StartsWith("http://"))
                return (-1);

            int startIndex = location.IndexOf(":", 7);
            if (startIndex == -1)
                return (-1);

            int stopIndex = location.IndexOf("/", startIndex);
            if (stopIndex == -1)
                stopIndex = location.Length - 1;

            try
            {
                return(Int32.Parse(location.Trim().Substring(startIndex + 1, stopIndex - (startIndex + 1))));
            }
            catch (FormatException) { return(-1); }
            catch (OverflowException) { return(-1); }
        }

        /// <summary>
        /// Process the cache control parameter.
        /// </summary>
        /// <param name="parameters">The data to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        protected ErrorSpec ProcessCacheControl(string parameters)
        {
            string[] parts = parameters.Split(new char[] { '=' });
            if (parts.Length != 2)
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "CACHE-CONTROL has the wrong number of fields", parameters));

            if (parts[0].Trim().ToLowerInvariant() != "max-age")
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "CACHE-CONTROL max-age not recognized", parameters));

            try
            {
                MaxAge = Int32.Parse(parts[1].Trim());
                return (null);
            }
            catch (FormatException) 
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "CACHE-CONTROL max age field is in the wrong format", parameters)); 
            }
            catch (OverflowException) 
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "CACHE-CONTROL max age field is out of range", parameters));
            }
        }

        /// <summary>
        /// Process the location parameter.
        /// </summary>
        /// <param name="parameters">The data to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        protected ErrorSpec ProcessLocation(string parameters)
        {
            Location = parameters.Trim();
            return (null);
        }

        /// <summary>
        /// Process the server description.
        /// </summary>
        /// <param name="parameters">The data to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        protected ErrorSpec ProcessServer(string parameters)
        {
            Server = parameters.Trim();
            return (null);
        }

        /// <summary>
        /// Process the service name.
        /// </summary>
        /// <param name="parameters">The data to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        protected ErrorSpec ProcessUniqueServiceName(string parameters)
        {
            UniqueServiceName = parameters.Trim();
            return (null);
        }

        /// <summary>
        /// Process the boot identifier.
        /// </summary>
        /// <param name="parameters">The data to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        protected ErrorSpec ProcessBootID(string parameters)
        {
            try
            {
                BootID = Int32.Parse(parameters.Trim());
                return (null);
            }
            catch (FormatException) 
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "BOOTID is in the wrong format", parameters));
            }
            catch (OverflowException) 
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "BOOTID is out of range", parameters));
            }
        }

        /// <summary>
        /// Process the config identifier.
        /// </summary>
        /// <param name="parameters">The data to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        protected ErrorSpec ProcessConfigID(string parameters)
        {
            try
            {
                ConfigID = Int32.Parse(parameters.Trim());
                return (null);
            }
            catch (FormatException) 
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "CONFIGID is in the wrong format", parameters));
            }
            catch (OverflowException) 
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "CONFIGID is out of range", parameters));
            }
        }

        /// <summary>
        /// Process the device identifier.
        /// </summary>
        /// <param name="parameters">The data to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        protected ErrorSpec ProcessDeviceID(string parameters)
        {
            try
            {
                DeviceId = Int32.Parse(parameters.Trim());
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "DEVICEID field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "DEVICEID field is out of range", parameters));
            }
        }

        /// <summary>
        /// Process the search port.
        /// </summary>
        /// <param name="parameters">The data to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        protected ErrorSpec ProcessSearchPort(string parameters)
        {
            try
            {
                SearchPort = Int32.Parse(parameters.Trim());
                return (null);
            }
            catch (FormatException) 
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "SEARCHPORT is in the wrong format", parameters));
            }
            catch (OverflowException) 
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "SEARCHPORT is out of range", parameters));
            }
        }
    }
}

