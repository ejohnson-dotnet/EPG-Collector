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
    /// The class that describes a receiver report packet.
    /// </summary>
    public class ReceiverReportPacket : ControlPacket
    {
        /// <summary>
        /// Get the synchronization source.
        /// </summary>
        public string SynchronizationSource { get; private set; }
        /// <summary>
        /// Get the list of report blocks.
        /// </summary>
        public Collection<ReportBlock> ReportBlocks { get; private set; }
        /// <summary>
        /// Get the profile extension data.
        /// </summary>
        public byte[] ProfileExtension { get; private set; }

        /// <summary>
        /// Initialize a new instance of the ReceiverReportPacket.
        /// </summary>
        public ReceiverReportPacket() { }

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
                return(null);

            SynchronizationSource = Utils.ConvertBytesToString(buffer, offset + 4, 4);

            ReportBlocks = new Collection<ReportBlock>(); 
            int index = 8;

            while (ReportBlocks.Count < ReportCount)
            {
                ReportBlock reportBlock = new ReportBlock();
                reply = reportBlock.Process(buffer, offset + index);
                if (reply != null)
                    return (reply);
                
                ReportBlocks.Add(reportBlock);
                index += reportBlock.BlockLength;                
            }

            if (index < Length)
            {
                ProfileExtension = new byte[Length - index];

                for (int extensionIndex = 0; index < Length; index++)
                {
                    ProfileExtension[extensionIndex] = buffer[offset + index];
                    extensionIndex++;
                }
            }

            return (null);
        }
    }
}

