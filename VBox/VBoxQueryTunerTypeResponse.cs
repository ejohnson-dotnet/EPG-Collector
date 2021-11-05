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

namespace VBox
{
    /// <summary>
    /// The class that describes the response to the QueryTunerType request.
    /// </summary>
    public class VBoxQueryTunerTypeResponse : VBoxResponse
    {
        /// <summary>
        /// Get the type of tuner.
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Return true if the tuner supports DVB-S or DVB-S2; false otherwise.
        /// </summary>
        public bool IsDvbs { get { return (Type == "DVBS" || Type == "DVBS2"); } }
        /// <summary>
        /// Return true if the tuner supports DVB-T; false otherwise.
        /// </summary>
        public bool IsDvbt { get { return (Type == "DVBT" || Type == "DVBT2"); } }
        /// <summary>
        /// Return true if the tuner supports DVB-C;false otherwise.
        /// </summary>
        public bool IsDvbc { get { return (Type == "DVBC"); } }

        internal VBoxQueryTunerTypeResponse() { }

        internal override void Process(XmlReader reader)
        {
            while (!reader.EOF)
            {
                reader.Read();
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Status":
                            base.Process(reader.ReadSubtree());
                            break;
                        case "DvbMode":
                            Type = reader.ReadString();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Get a text representation of the response.
        /// </summary>
        /// <returns>The text description.</returns>
        public override string ToString()
        {
            return (base.ToString() + " DvbMode=" + Type);
        }
    }
}

