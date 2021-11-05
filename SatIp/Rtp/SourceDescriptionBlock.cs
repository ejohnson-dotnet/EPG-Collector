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
    /// The class that describes a source description block.
    /// </summary>
    public class SourceDescriptionBlock
    {
        /// <summary>
        /// Get the length of the block.
        /// </summary>
        public int BlockLength { get { return (blockLength + (blockLength % 4)); } }

        /// <summary>
        /// Get the synchronization source.
        /// </summary>
        public string SynchronizationSource { get; private set; }
        /// <summary>
        /// Get the list of source descriptioni items.
        /// </summary>
        public Collection<SourceDescriptionItem> Items;

        private int blockLength;

        /// <summary>
        /// Initialize a new instance of the SourceDescriptionBlock class.
        /// </summary>
        public SourceDescriptionBlock() { }

        /// <summary>
        /// Unpack the data in a packet.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet.</param>
        /// <param name="offset">The offset to the first byte of the packet within the buffer.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(byte[] buffer, int offset)
        {
            SynchronizationSource = Utils.ConvertBytesToString(buffer, offset, 4);

            Items = new Collection<SourceDescriptionItem>();
            int index = 4;

            bool done = false;

            do
            {
                SourceDescriptionItem item = new SourceDescriptionItem();
                ErrorSpec reply = item.Process(buffer, offset + index);
                if (reply != null)
                    return (reply);

                if (item.Type != 0)
                {
                    Items.Add(item);
                    index += item.ItemLength;

                    blockLength += item.ItemLength;
                }
                else
                {
                    blockLength++;
                    done = true;
                }
            }
            while (!done);

            return (null);
        }
    }
}

