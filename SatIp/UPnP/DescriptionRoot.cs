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
using System.Xml;
using System.IO;

using SatIpDomainObjects;

namespace UPnP
{
    /// <summary>
    /// The class that describes the root node of a description.
    /// </summary>
    public class DescriptionRoot
    {
        /// <summary>
        /// Get the namespace.
        /// </summary>
        public string NameSpace { get; private set; }
        /// <summary>
        /// Get the configuration identity.
        /// </summary>
        public int ConfigId { get; private set; }
        /// <summary>
        /// Get the version number.
        /// </summary>
        public SpecVersion SpecVersion { get; private set; }
        /// <summary>
        /// Get the Url base.
        /// </summary>
        public string UrlBase { get; private set; }
        /// <summary>
        /// Get a list of the device descriptions.
        /// </summary>
        public Collection<Device> Devices { get; private set; }

        /// <summary>
        /// Initialize a new instance of the DescriptionRoot class.
        /// </summary>
        public DescriptionRoot() { }

        /// <summary>
        /// Load data into the instance.
        /// </summary>
        /// <param name="xmlReader">An XmlReader instance containing the data to process.</param>
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
                            case "root":
                                NameSpace = xmlReader.GetAttribute("xmlns");
                                string configId = xmlReader.GetAttribute("configId");
                                if (configId != null)
                                {
                                    try
                                    {
                                        ConfigId = Int32.Parse(configId.Trim());
                                    }
                                    catch (FormatException)
                                    {
                                        return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The root configId is in the wrong format", configId));                                                                         
                                    }
                                    catch (OverflowException)
                                    {
                                        return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The root configId is out of range", configId));
                                    }
                                }
                                break;
                            case "specversion":
                                SpecVersion = new SpecVersion();
                                ErrorSpec specReply = SpecVersion.Load(xmlReader.ReadSubtree());
                                if (specReply != null)
                                    return (specReply);
                                break;
                            case "urlbase":
                                UrlBase = xmlReader.ReadString();
                                break;
                            case "device":                                
                                Device device = new Device();
                                ErrorSpec deviceReply = device.Load(xmlReader.ReadSubtree());
                                if (deviceReply != null)
                                    return (deviceReply);
                                if (Devices == null)
                                    Devices = new Collection<Device>();
                                Devices.Add(device);
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
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load description root xml", e.Message));                              
            }
            catch (IOException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load description root xml", e.Message));                           
            }

            return (null);
        }
    }
}

