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

namespace NetworkProtocols.Rtp
{
    /// <summary>
    /// The class that describes an RTP data packet.
    /// </summary>
    public class DataPacket
    {
        /// <summary>
        /// Get the version number.
        /// </summary>
        public int Version { get; private set; }
        /// <summary>
        /// Returns true if padding bytes are present; false otherwise.
        /// </summary>
        public bool Padding { get; private set; }
        /// <summary>
        /// Returns true if an extension header is present; false otherwise.
        /// </summary>
        public bool ExtensionHeaderPresent { get; private set; }
        /// <summary>
        /// Get the contributing source count.
        /// </summary>
        public int ContributingSourceCount { get; private set; }

        /// <summary>
        /// Returns true if the marker bit is set; false otherwise.
        /// </summary>
        public bool Marker { get; private set; }
        /// <summary>
        /// Get the payload type.
        /// </summary>
        public int PayloadType { get; private set; }

        /// <summary>
        /// Get the packet sequence number.
        /// </summary>
        public int SequenceNumber { get; private set; }

        /// <summary>
        /// Get the timestamp.
        /// </summary>
        public int TimeStamp { get; private set; }

        /// <summary>
        /// Get the synchronization source.
        /// </summary>
        public string SynchronizationSource { get; private set; }
        /// <summary>
        /// Get the list of contributing sources.
        /// </summary>
        public Collection<string> ContributingSources { get; private set; }

        /// <summary>
        /// Get the extension header identity.
        /// </summary>
        public int ExtensionHeaderId { get; private set; }
        /// <summary>
        /// Get the extension header length.
        /// </summary>
        public int ExtensionHeaderLength { get; private set; }

        /// <summary>
        /// Get the offset to the start of data portion of the packet.
        /// </summary>
        public int DataOffset { get; private set; }
        /// <summary>
        /// Get the length of the data portion of the packet.
        /// </summary>
        public int DataLength { get { return (dataLength); } }

        private int dataLength;

        /// <summary>
        /// Initialize a new instance of the DataPacket class.
        /// </summary>
        public DataPacket() { }

        /// <summary>
        /// Unpack the data in a packet.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet.</param>
        /// <param name="offset">The offset to the first byte of the packet within the buffer.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(byte[] buffer, int receiveCount)
        {
            Version = buffer[0] >> 6;
            Padding = (buffer[0] & 0x20) != 0;
            ExtensionHeaderPresent = (buffer[0] & 0x10) != 0;
            ContributingSourceCount = buffer[0] & 0x0f;

            Marker = (buffer[1] & 0x80) != 0;
            PayloadType = buffer[1] & 0x7f;

            SequenceNumber = Utils.Convert2BytesToInt(buffer, 2);
            TimeStamp = Utils.Convert4BytesToInt(buffer, 4);
            SynchronizationSource = Utils.ConvertBytesToString(buffer, 8, 4);

            int index = 12;

            if (ContributingSourceCount != 0)
            {
                ContributingSources = new Collection<string>();

                while (ContributingSources.Count < ContributingSourceCount)
                {
                    ContributingSources.Add(Utils.ConvertBytesToString(buffer, index, 4));
                    index += 4;
                }
            }

            if (!ExtensionHeaderPresent)
                DataOffset = index;
            else
            {
                ExtensionHeaderId = Utils.Convert2BytesToInt(buffer, index);
                ExtensionHeaderLength = Utils.Convert2BytesToInt(buffer, index + 2);
                DataOffset = index + ExtensionHeaderLength + 4;
            }

            dataLength = receiveCount - DataOffset;

            return (null);
        }        
    }
}

