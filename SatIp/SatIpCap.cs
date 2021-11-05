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
using System.IO;
using System.Xml;

using NetworkProtocols;
using NetworkProtocols.UPnP;

namespace SatIp
{
    /// <summary>
    /// The class that describes the SAT>IP capabilities line.
    /// </summary>
    public class SatIpCap : ICreateLoader, IDescriptionLine
    {
        /// <summary>
        /// Get the name space.
        /// </summary>
        public string NameSpace { get; private set; }
        /// <summary>
        /// Get the list of modulation systems.
        /// </summary>
        public Collection<ModulationSystem> ModulationSystems;

        ///Initialize a new instance of the SatIpCap class. 
        public SatIpCap() { }

        /// <summary>
        /// Determine if the instance processes a specified line.
        /// </summary>
        /// <param name="identity">The identity of the line.</param>
        /// <returns>True if the identity is processed; false otherwise.</returns>
        public bool IsLoader(string identity)
        {
            return (identity == "satip:x_satipcap");
        }

        /// <summary>
        /// Create an instance of the processor.
        /// </summary>
        /// <param name="identity">The name of the instance.</param>
        /// <returns>An instance of the processor.</returns>
        public IDescriptionLine CreateLoader(string identity)
        {
            return (new SatIpCap());
        }

        /// <summary>
        /// Load the instance with data.
        /// </summary>
        /// <param name="xmlReader">An XmlReader instance containing the data to load.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Load(XmlReader xmlReader)
        {
            try
            {
                NameSpace = xmlReader.GetAttribute("xmlns:satip");

                string[] entries = xmlReader.ReadString().Trim().Split(new char[] { ',' });

                foreach (string entry in entries)
                {
                    ModulationSystem modulationSystem = new ModulationSystem();
                    ErrorSpec reply = modulationSystem.Load(entry);
                    if (reply != null)
                        return (reply);
                    if (ModulationSystems == null)
                        ModulationSystems = new Collection<ModulationSystem>();
                    ModulationSystems.Add(modulationSystem);
                }

            }
            catch (XmlException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load satip cap xml", e.Message));
            }
            catch (IOException e)
            {
                return (new ErrorSpec(UPnPMessage.UPnPProtocolId, ErrorCode.Exception, 0, "Failed to load satip cap xml", e.Message));
            }

            return (null);
        }
    }
}

