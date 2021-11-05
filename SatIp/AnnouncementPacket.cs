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

using NetworkProtocols;
using NetworkProtocols.Rtp;
using NetworkProtocols.Sdp;

namespace SatIp
{
    /// <summary>
    /// The class that describes a SAT>IP announcement packet.
    /// </summary>
    public class AnnouncementPacket : ApplicationPacket, ICreatePacket, IProcessPacket
    {
        /// <summary>
        /// Get the media format.
        /// </summary>
        public MediaFormat MediaFormat { get; private set; }

        /// <summary>
        /// Get an instance of the packet processor.
        /// </summary>
        /// <returns>An instance of the processor.</returns>
        public IProcessPacket GetInstance()
        {
            return (new AnnouncementPacket());
        }

        /// <summary>
        /// Parse the announcement packet.
        /// </summary>
        /// <param name="buffer">The buffer holding the data to parse.</param>
        /// <param name="offset">The offset to the first byte of the packet.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public override ErrorSpec Process(byte[] buffer, int offset)
        {
            base.Process(buffer, offset);

            if (PacketType != 204)
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "Announcement packet type not 204", PacketType.ToString()));                

            if (Name != "SES1")
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "Announcement packet name is not '0000'", Name));                               

            MediaFormat = new SatIpMediaFormat();
            ErrorSpec reply = MediaFormat.Process("33 " + Data);

            return (reply);
        }

        /// <summary>
        /// Get a text representation of this instance.
        /// </summary>
        /// <returns>The text representation.</returns>
        public override string ToString()
        {
            return ("Announcement Packet: name=" + Name + " " + MediaFormat.ToString()); 
        }
    }
}

