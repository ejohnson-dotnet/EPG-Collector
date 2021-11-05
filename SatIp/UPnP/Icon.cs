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
using System.Xml;

using SatIpDomainObjects;

namespace UPnP
{
    /// <summary>
    /// The class that describes an icon.
    /// </summary>
    public class Icon
    {
        /// <summary>
        /// Get the mime type.
        /// </summary>
        public string MimeType { get; private set; }
        /// <summary>
        /// Get the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Get the height.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Get the depth.
        /// </summary>
        public int Depth { get; private set; }
        /// <summary>
        /// Get the Url.
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// Initialize a new instance of the Icon class.
        /// </summary>
        public Icon() { }

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
                            case "icon":
                                break;
                            case "mimetype":
                                MimeType = xmlReader.ReadString();
                                break;
                            case "width":
                                string width = xmlReader.ReadString();
                                try 
                                { 
                                    Width = Int32.Parse(width.Trim()); 
                                }
                                catch (FormatException) 
                                {
                                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The icon width field is in the wrong format", width));
                                }
                                catch (OverflowException)
                                {
                                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The icon width field is out of range", width));
                                }
                                break;
                            case "height":
                                string height = xmlReader.ReadString();
                                try
                                {
                                    Height = Int32.Parse(height.Trim());
                                }
                                catch (FormatException)
                                {
                                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The icon height field is in the wrong format", height));
                                }
                                catch (OverflowException)
                                {
                                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The icon height field is out of range", height));
                                }
                                break;
                            case "depth":
                                string depth = xmlReader.ReadString();
                                try
                                {
                                    Depth = Int32.Parse(depth.Trim());
                                }
                                catch (FormatException)
                                {
                                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The icon depth field is in the wrong format", depth));
                                }
                                catch (OverflowException)
                                {
                                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The icon depth field is out of range", depth));
                                }
                                break;
                            case "url":
                                Url = xmlReader.ReadString();
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
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load icon xml", e.Message));
            }
            catch (IOException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load icon xml", e.Message));
            }

            return (null);
        }
    }
}

