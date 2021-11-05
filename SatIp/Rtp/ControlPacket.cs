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
    /// The base class for RTCP packets.
    /// </summary>
    public abstract class ControlPacket
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
        /// Get the report count.
        /// </summary>
        public int ReportCount { get; private set; }

        /// <summary>
        /// Get the packet type.
        /// </summary>
        public int PacketType { get; private set; }

        /// <summary>
        /// Get the length of the packet.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Get the timestamp of the packet.
        /// </summary>
        public int TimeStamp { get; private set; }

        /// <summary>
        /// Get or set the instance creator for custom packet types.
        /// </summary>
        public static ICreatePacket CustomPacket { get; set; }
        
        /// <summary>
        /// Initialize a new instance of the ControlPacket class.
        /// </summary>
        protected ControlPacket() { }

        /// <summary>
        /// Unpack the data in a packet.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet.</param>
        /// <param name="offset">The offset to the first byte of the packet within the buffer.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public virtual ErrorSpec Process(byte[] buffer, int offset)
        {
            Version = buffer[offset] >> 6;
            Padding = (buffer[offset] & 0x20) != 0;
            ReportCount = buffer[offset] & 0x1f;
            PacketType = buffer[offset + 1];
            Length = (Utils.Convert2BytesToInt(buffer, offset + 2) * 4) + 4;

            return (null);
        }

        /// <summary>
        /// Get an instance of a packet class.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet.</param>
        /// <param name="offset">The offset to the first byte of the packet within the buffer.</param>
        /// <returns>An ErrorSpec instance if an error occurs; otherwise an instance of the class that processes the packet.</returns>
        public static object GetInstance(byte[] buffer, int offset)
        {
            ErrorSpec errorSpec;

            switch (buffer[offset + 1])
            {
                case 200:
                    SenderReportPacket senderReport = new SenderReportPacket();
                    errorSpec = senderReport.Process(buffer, offset);
                    if (errorSpec != null)
                        return (errorSpec);
                    else
                        return (senderReport);
                case 201:
                    ReceiverReportPacket receiverReport = new ReceiverReportPacket();
                    errorSpec = receiverReport.Process(buffer, offset);
                    if (errorSpec != null)
                        return (errorSpec);
                    else
                        return (receiverReport);
                case 202:
                    SourceDescriptionPacket sourceDescription = new SourceDescriptionPacket();
                    errorSpec = sourceDescription.Process(buffer, offset);
                    if (errorSpec != null)
                        return (errorSpec);
                    else
                        return (sourceDescription);
                case 203:
                    ByePacket byePacket = new ByePacket();
                    errorSpec = byePacket.Process(buffer, offset);
                    if (errorSpec != null)
                        return (errorSpec);
                    else
                        return (byePacket);
                case 204:
                    ApplicationPacket applicationPacket = null;

                    if (CustomPacket != null)
                        applicationPacket = (ApplicationPacket)CustomPacket.GetInstance();
                    else
                        applicationPacket = new ApplicationPacket();
                    
                        errorSpec = applicationPacket.Process(buffer, offset);
                    if (errorSpec != null)
                        return (errorSpec);
                    else
                        return (applicationPacket);
                default:
                    return (new ErrorSpec("RTCP", ErrorCode.NotRecognized, 0, "Control packet type not recognized", buffer[offset + 1].ToString("x2")));                    
            }            
        }
    }
}

