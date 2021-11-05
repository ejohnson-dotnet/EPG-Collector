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
    /// The class that describes a RTCP report block.
    /// </summary>
    public class ReportBlock
    {
        /// <summary>
        /// Get the length of the block.
        /// </summary>
        public int BlockLength { get { return(24); } }

        /// <summary>
        /// Get the synchronization source.
        /// </summary>
        public string SynchronizationSource { get; private set; }
        /// <summary>
        /// Get the fraction lost.
        /// </summary>
        public int FractionLost { get; private set; }
        /// <summary>
        /// Get the cumulative packets lost.
        /// </summary>
        public int CumulativePacketsLost { get; private set; }
        /// <summary>
        /// Get the highest number received.
        /// </summary>
        public int HighestNumberReceived { get; private set; }
        /// <summary>
        /// Get the inter arrival jitter.
        /// </summary>
        public int InterArrivalJitter { get; private set; }
        /// <summary>
        /// Get the timestamp of the last report.
        /// </summary>
        public int LastReportTimeStamp { get; private set; }
        /// <summary>
        /// Get the delay since the last report.
        /// </summary>
        public int DelaySinceLastReport { get; private set; }

        /// <summary>
        /// Initialize a new instance of the ReportBlock class.
        /// </summary>
        public ReportBlock() { }

        /// <summary>
        /// Unpack the data in a packet.
        /// </summary>
        /// <param name="buffer">The buffer containing the packet.</param>
        /// <param name="offset">The offset to the first byte of the packet within the buffer.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(byte[] buffer, int offset)
        {
            SynchronizationSource = Utils.ConvertBytesToString(buffer, offset, 4);
            FractionLost = buffer[offset + 4];
            CumulativePacketsLost = Utils.Convert3BytesToInt(buffer, offset + 5);
            HighestNumberReceived = Utils.Convert4BytesToInt(buffer, offset + 8);
            InterArrivalJitter = Utils.Convert4BytesToInt(buffer, offset + 12);
            LastReportTimeStamp = Utils.Convert4BytesToInt(buffer, offset + 16);
            DelaySinceLastReport = Utils.Convert4BytesToInt(buffer, offset + 20);

            return (null);
        }
    }
}

