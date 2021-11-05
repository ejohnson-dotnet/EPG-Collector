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
using System.Xml;
using System.IO;

namespace NetworkProtocols.UPnP
{
    /// <summary>
    /// The class that describes a device description.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Get the device type.
        /// </summary>
        public string DeviceType { get; private set; }
        /// <summary>
        /// Get the friendly name.
        /// </summary>
        public string FriendlyName { get; private set; }
        /// <summary>
        /// Get the name of the manufacturer.
        /// </summary>
        public string Manufacturer { get; private set; }
        /// <summary>
        /// Get the Url of the manufacturer.
        /// </summary>
        public string ManufacturerUrl { get; private set; }
        /// <summary>
        /// Get the description of the model.
        /// </summary>
        public string ModelDescription { get; private set; }
        /// <summary>
        /// Get the name of the model.
        /// </summary>
        public string ModelName { get; private set; }
        /// <summary>
        /// Get the model number.
        /// </summary>
        public string ModelNumber { get; private set; }
        /// <summary>
        /// Get the Url of the model.
        /// </summary>
        public string ModelUrl { get; private set; }
        /// <summary>
        /// Get the serial number.
        /// </summary>
        public string SerialNumber { get; private set; }
        /// <summary>
        /// Get the Udn.
        /// </summary>
        public string Udn { get; private set; }
        /// <summary>
        /// Get the universal product code.
        /// </summary>
        public string Upc { get; private set; }
        /// <summary>
        /// Get the list of icons.
        /// </summary>
        public Collection<Icon> Icons { get; private set; }
        /// <summary>
        /// Get the list of services.
        /// </summary>
        public Collection<Service> Services { get; private set; }
        /// <summary>
        /// Get the list of devices.
        /// </summary>
        public Collection<Device> Devices { get; private set; }
        /// <summary>
        /// Get the presentation Url.
        /// </summary>
        public string PresentationUrl { get; private set; }
        /// <summary>
        /// Get the list of custom lines.
        /// </summary>
        public Collection<IDescriptionLine> CustomLines;

        /// <summary>
        /// Get the list of custom line loaders.
        /// </summary>
        public static Collection<ICreateLoader> CustomLoaders;

        /// <summary>
        /// Initialize a new instance of the Device class.
        /// </summary>
        public Device() { }

        /// <summary>
        /// Register a custom line loader.
        /// </summary>
        /// <param name="loader">The loader to register.</param>
        public static void RegisterLoader(ICreateLoader loader)
        {
            if (CustomLoaders == null)
                CustomLoaders = new Collection<ICreateLoader>();

            CustomLoaders.Add(loader);
        }

        /// <summary>
        /// Load data into the instance.
        /// </summary>
        /// <param name="xmlReader">An XmlReader instance containing the data to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Load(XmlReader xmlReader)
        {
            bool firstDevice = true;

            try
            {
                while (!xmlReader.EOF)
                {
                    if (xmlReader.IsStartElement())
                    {
                        string name = xmlReader.Name.ToLowerInvariant();

                        switch (name)
                        {
                            case "devicetype":
                                DeviceType = xmlReader.ReadString();
                                break;
                            case "friendlyname":
                                FriendlyName = xmlReader.ReadString();
                                break;
                            case "manufacturer":
                                Manufacturer = xmlReader.ReadString();
                                break;
                            case "manufacturerurl":
                                ManufacturerUrl = xmlReader.ReadString();
                                break;
                            case "modeldescription":
                                ModelDescription = xmlReader.ReadString();
                                break;
                            case "modelname":
                                ModelName = xmlReader.ReadString();
                                break;
                            case "modelnumber":
                                ModelNumber = xmlReader.ReadString();
                                break;
                            case "modelurl":
                                ModelUrl = xmlReader.ReadString();
                                break;
                            case "serialnumber":
                                SerialNumber = xmlReader.ReadString();
                                break;
                            case "udn":
                                Udn = xmlReader.ReadString();
                                break;
                            case "upc":
                                Upc = xmlReader.ReadString();
                                break;
                            case "iconlist":
                                break;
                            case "icon":
                                Icon icon = new Icon();
                                ErrorSpec iconReply = icon.Load(xmlReader.ReadSubtree());
                                if (iconReply != null)
                                    return(iconReply);
                                if (Icons == null)
                                    Icons = new Collection<Icon>();
                                Icons.Add(icon);
                                break;
                            case "servicelist":
                                break;
                            case "service":
                                Service service = new Service();
                                ErrorSpec serviceReply = service.Load(xmlReader.ReadSubtree());
                                if (serviceReply != null)
                                    return (serviceReply);
                                if (Services == null)
                                    Services = new Collection<Service>();
                                Services.Add(service);
                                break;
                            case "devicelist":
                                break;
                            case "device":
                                if (firstDevice)
                                    firstDevice = false;
                                else
                                {
                                    if (Devices == null)
                                        Devices = new Collection<Device>();
                                    Device device = new Device();
                                    ErrorSpec reply = device.Load(xmlReader);
                                    if (reply != null)
                                        return (reply);
                                    Devices.Add(device);
                                }
                                break;
                            case "presentationurl":
                                PresentationUrl = xmlReader.ReadString();
                                break;
                            default:
                                if (CustomLoaders == null)
                                    break;

                                foreach (ICreateLoader customLoader in CustomLoaders)
                                {
                                    if (customLoader.IsLoader(name))
                                    {
                                        IDescriptionLine lineLoader = customLoader.CreateLoader(name);
                                        ErrorSpec loaderReply = lineLoader.Load(xmlReader);
                                        if (loaderReply != null)
                                            return (loaderReply);
                                        else
                                        {
                                            if (CustomLines == null)
                                                CustomLines = new Collection<IDescriptionLine>();
                                            CustomLines.Add(lineLoader);
                                            break;
                                        }
                                    }
                                }

                                break;
                        }
                    }

                    xmlReader.Read();
                }
            }
            catch (XmlException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load device xml", e.Message));                              
            }
            catch (IOException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load device xml", e.Message));                                         
            }

            return (null);
        }
    }
}

