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

namespace NetworkProtocols.Rtp
{
    /// <summary>
    /// The class that describes an RTCP Application packet 
    /// </summary>
    public class ApplicationPacket : ControlPacket
    {
        /// <summary>
        /// Get the synchronization source.
        /// </summary>
        public int SynchronizationSource { get; private set; }
        /// <summary>
        /// Get the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Get the identity.
        /// </summary>
        public int Identity { get; private set; }
        /// <summary>
        /// Get the variable data portion.
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Initialize a new instance of the ApplicationPacket class.
        /// </summary>
        public ApplicationPacket() { }

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

            SynchronizationSource = Utils.Convert4BytesToInt(buffer, offset + 4);
            Name = Utils.ConvertBytesToString(buffer, offset + 8, 4);
            Identity = Utils.Convert2BytesToInt(buffer, offset + 12);

            int dataLength = Utils.Convert2BytesToInt(buffer, offset + 14);
            if (dataLength != 0)
                Data = Utils.ConvertBytesToString(buffer, offset + 16, dataLength);

            return (null);
        }
    }
}

