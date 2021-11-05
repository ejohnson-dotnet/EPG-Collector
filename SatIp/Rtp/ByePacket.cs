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
    /// The class that describes an RTCP bye bye packet.
    /// </summary>
    public class ByePacket : ControlPacket
    {
        /// <summary>
        /// Get the list of synchronization sources.
        /// </summary>
        public Collection<string> SynchronizationSources { get; private set; }
        /// <summary>
        /// Get the reason for leaving.
        /// </summary>
        public string ReasonForLeaving { get; private set; }
 
        /// <summary>
        /// Initialize a new instance of the ByePacket class.
        /// </summary>
        public ByePacket() { }

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

            SynchronizationSources = new Collection<string>();
            int index = 4;

            while (SynchronizationSources.Count < ReportCount)
            {
                SynchronizationSources.Add(Utils.ConvertBytesToString(buffer, offset + index, 4));
                index+= 4;
            }

            if (index < Length)
            {
                int reasonLength = buffer[offset + index];
                ReasonForLeaving = Utils.ConvertBytesToString(buffer, offset + index + 1, reasonLength);                 
            }

            return (null);
        }
    }
}

