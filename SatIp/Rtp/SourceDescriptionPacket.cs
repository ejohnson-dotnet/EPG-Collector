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

using SatIpDomainObjects;

namespace Rtp
{
    /// <summary>
    /// The class that describes an RTCP source description packet.
    /// </summary>
    public class SourceDescriptionPacket : ControlPacket
    {
        /// <summary>
        /// Get the list of source descriptions.
        /// </summary>
        public Collection<SourceDescriptionBlock> Descriptions;

        /// <summary>
        /// Initialize a new instance of the SourceDescriptionPacket class.
        /// </summary>
        public SourceDescriptionPacket() { }

        /// <summary>
        /// Unpack the data in a packet.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet.</param>
        /// <param name="offset">The offset to the first byte of the packet within the buffer.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public override ErrorSpec Process(byte[] buffer, int offset)
        {
            ErrorSpec reply = base.Process(buffer, offset);
            if (reply != null)
                return (reply);

            if (Length == 0)
                return (null);

            Descriptions = new Collection<SourceDescriptionBlock>();

            int index = 4;

            while (Descriptions.Count < ReportCount)
            {
                SourceDescriptionBlock descriptionBlock = new SourceDescriptionBlock();
                reply = descriptionBlock.Process(buffer, offset + index);
                if (reply != null)
                    return (reply);

                Descriptions.Add(descriptionBlock);
                index += descriptionBlock.BlockLength;
            }

            return (null);
        }
    }
}

