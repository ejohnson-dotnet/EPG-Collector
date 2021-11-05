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

using SatIpDomainObjects;

namespace Rtp
{
    /// <summary>
    /// The class that describes a source description item.
    /// </summary>
    public class SourceDescriptionItem
    {
        /// <summary>
        /// Get the type.
        /// </summary>
        public int Type { get; private set; }
        /// <summary>
        /// Get the text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Get the length of the item.
        /// </summary>
        public int ItemLength { get { return (Text.Length + 2); } }
        
        /// <summary>
        /// Initialize a new instance of the SourceDescriptionItem class.
        /// </summary>
        public SourceDescriptionItem() { }

        /// <summary>
        /// Unpack the data in a packet.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet.</param>
        /// <param name="offset">The offset to the first byte of the packet within the buffer.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(byte[] buffer, int offset)
        {
            Type = buffer[offset];

            if (Type != 0)
            {
                int length = buffer[offset + 1];
                Text = Utils.ConvertBytesToString(buffer, offset + 2, length);
            }

            return (null);
        }
    }
}

