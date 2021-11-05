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

using System.Net;

namespace NetReceiver
{
    internal class MessageSpec
    {
        internal byte[] Buffer { get; private set; }
        internal int Offset { get; private set; }
        internal int Length { get; private set; }
        internal EndPoint RemoteEndPoint { get; private set; }
        internal int ReceiverNumber { get; private set; }

        internal bool EndMessage { get; set; }

        private MessageSpec() { }

        internal MessageSpec(byte[] buffer, int offset, int length, EndPoint remoteEndPoint, int receiverNumber)
        {
            Buffer = buffer;
            Offset = offset;
            Length = length;
            RemoteEndPoint = remoteEndPoint;
            ReceiverNumber = receiverNumber;
        }

        internal MessageSpec(bool endMessage)
        {
            EndMessage = endMessage;
        }
    }
}

