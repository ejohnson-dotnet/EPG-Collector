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

namespace NetworkProtocols.UPnP
{
    /// <summary>
    /// The class that desribes a specification version.
    /// </summary>
    public class SpecVersion
    {
        /// <summary>
        /// Get the major version number.
        /// </summary>
        public int Major { get; private set; }
        /// <summary>
        /// Get the minor version number.
        /// </summary>
        public int Minor { get; private set; }

        /// <summary>
        /// Initialize a new instance of the SpecVersion class.
        /// </summary>
        public SpecVersion() { }

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
                            case "specversion":
                                break;
                            case "major":
                                string major = xmlReader.ReadString();
                                try 
                                { 
                                    Major = Int32.Parse(major.Trim()); 
                                }
                                catch (FormatException) 
                                {
                                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The spec version major field is in the wrong format", name));
                                }
                                catch (OverflowException)
                                {
                                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The spec version major field is out of range", name));
                                }
                                break;
                            case "minor":
                                string minor = xmlReader.ReadString();
                                try
                                {
                                    Minor = Int32.Parse(minor.Trim());
                                }
                                catch (FormatException)
                                {
                                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The spec version minor field is in the wrong format", name)); 
                                }
                                catch (OverflowException)
                                {
                                    return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.FormatError, 0, "The spec version minor field is out of range", minor)); 
                                }
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
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to spec version xml", e.Message));
            }
            catch (IOException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to spec version xml", e.Message));
            }

            return (null);
        }
    }
}

