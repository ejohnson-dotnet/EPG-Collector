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

using System.IO;
using System.Xml;

namespace NetworkProtocols.UPnP
{
    /// <summary>
    /// The class that describes a description.
    /// </summary>
    public class Description
    {
        /// <summary>
        /// Get the root node.
        /// </summary>
        public DescriptionRoot Root { get; private set; }

        /// <summary>
        /// Initialize a new instance of the Description class.
        /// </summary>
        public Description() { }

        /// <summary>
        /// Process a description.
        /// </summary>
        /// <param name="buffer">The buffer containing the description.</param>
        /// <param name="count">The number of bytes to process.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(byte[] buffer, int count)
        {
            MemoryStream memoryStream = new MemoryStream(buffer, 0, count);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.DtdProcessing = DtdProcessing.Ignore;
            settings.CheckCharacters = false;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;

            XmlReader xmlReader = XmlReader.Create(memoryStream, settings);
            
            try
            {
                while (!xmlReader.EOF)
                {
                    xmlReader.Read();

                    if (xmlReader.IsStartElement())
                    {
                        string name = xmlReader.Name.ToLowerInvariant();

                        switch (name)
                        {
                            case "root":
                                Root = new DescriptionRoot();
                                ErrorSpec reply = Root.Load(xmlReader.ReadSubtree());
                                if (reply != null)
                                    return (reply);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (XmlException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load xmltv file", e.Message));                
            }
            catch (IOException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load xmltv file", e.Message));                
            }

            if (xmlReader != null)
                xmlReader.Close();

            return (null);
        }
    }
}

