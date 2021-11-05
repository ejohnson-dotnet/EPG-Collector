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

using System.Xml;
using System.IO;

using SatIpDomainObjects;

namespace UPnP
{
    /// <summary>
    /// The class that describes a service.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Get the service type.
        /// </summary>
        public string ServiceType { get; private set; }
        /// <summary>
        /// Get the service ID.
        /// </summary>
        public string ServiceId { get; private set; }
        /// <summary>
        /// Get the control Url.
        /// </summary>
        public string ControlUrl { get; private set; }
        /// <summary>
        /// Get the event sub Url.
        /// </summary>
        public string EventSubUrl { get; private set; }
        /// <summary>
        /// Get the SCPDU Url.
        /// </summary>
        public string SCPDUrl { get; private set; }
        
        /// <summary>
        /// Initialize a new instance of the Service class.
        /// </summary>
        public Service() { }

        /// <summary>
        /// Load data into the instance.
        /// </summary>
        /// <param name="xmlReader">An XmlReader containing the data.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Load(XmlReader xmlReader)
        {
            try
            {
                while (!xmlReader.EOF)
                {
                    if (xmlReader.IsStartElement())
                    {
                        string name = xmlReader.Name.ToLowerInvariant();

                        switch (name)
                        {
                            case "service":
                                break;
                            case "servicetype":
                                ServiceType = xmlReader.ReadString();
                                break;
                            case "serviceid":
                                ServiceId = xmlReader.ReadString();
                                break;
                            case "controlurl":
                                ControlUrl = xmlReader.ReadString();
                                break;
                            case "eventsuburl":
                                EventSubUrl = xmlReader.ReadString();
                                break;
                            case "scpdurl":
                                SCPDUrl = xmlReader.ReadString();
                                break;
                            default:
                                break;
                        }
                    }

                    xmlReader.Read();
                }
            }
            catch (XmlException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load service xml", e.Message));              
            }
            catch (IOException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load service xml", e.Message));                
            }

            return (null);
        }
    }
}

