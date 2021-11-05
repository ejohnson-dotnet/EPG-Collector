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
using System.Xml;

namespace VBox
{
    /// <summary>
    /// The class that describes the response to an OpenMuxStream request.
    /// </summary>
    public class VBoxOpenMuxStreamResponse : VBoxResponse
    {
        /// <summary>
        /// Get the response Url.
        /// </summary>
        public string MuxUrl { get; private set; }

        /// <summary>
        /// Get the mux address element of the response Url.
        /// </summary>
        public string MuxAddress
        {
            get
            {
                if (MuxUrl == null)
                    return (null);

                string[] parts = MuxUrl.Substring(7).Split(new char[] { ':' });
                if (parts.Length != 2)
                    return (null);
                else
                    return (parts[0]);
            }
        }

        /// <summary>
        /// Get the mux port element of the response Url.
        /// </summary>
        public int MuxPort
        {
            get
            {
                if (MuxUrl == null)
                    return (0);

                string[] parts = MuxUrl.Substring(7).Split(new char[] { ':' });
                if (parts.Length != 2)
                    return (0);
                else
                {
                    int index = parts[1].IndexOf('/');
                    if (index == -1)
                        return (0);
                    else
                        return (Int32.Parse(parts[1].Substring(0, index)));
                }
            }
        }

        /// <summary>
        /// Get the mux path element of the response Url.
        /// </summary>
        public string MuxPath
        {
            get
            {
                if (MuxUrl == null)
                    return (null);

                string[] parts = MuxUrl.Substring(7).Split(new char[] { ':' });
                if (parts.Length != 2)
                    return (null);
                else
                {
                    int index = parts[1].IndexOf('/');
                    if (index == -1)
                        return (null);
                    else
                        return (parts[1].Substring(index + 1));
                }
            }
        }

        internal VBoxOpenMuxStreamResponse() { }

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
                        case "MuxUrl":
                             MuxUrl = reader.ReadString();
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
            return (base.ToString() + " MuxUrl=" + MuxUrl);
        }
    }
}

