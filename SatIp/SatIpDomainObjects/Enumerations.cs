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

namespace SatIpDomainObjects
{
    /// <summary>
    /// Error codes used in the ErrorSpec class.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// No error.
        /// </summary>
        OK,
        /// <summary>
        /// The server response is incorrect.
        /// </summary>
        ResponseError,
        /// <summary>
        /// An object could not be located.
        /// </summary>
        NotFound,
        /// <summary>
        /// Communications have timed out.
        /// </summary>
        TimedOut,
        /// <summary>
        /// The server reported insufficient bandwidth.
        /// </summary>
        InsufficientBandwidth,
        /// <summary>
        /// A session could not be located.
        /// </summary>
        SessionNotFound,
        /// <summary>
        /// The server has encountered an internal error.
        /// </summary>
        ServerError,
        /// <summary>
        /// The requested service is not available.
        /// </summary>
        ServiceUnavailable,
        /// <summary>
        /// A server response is incomplete.
        /// </summary>
        UnexpectedEndOfMessage,
        /// <summary>
        /// A server response is in the wrong format.
        /// </summary>
        FormatError,
        /// <summary>
        /// The server did not recognise a request.
        /// </summary>
        NotRecognized,
        /// <summary>
        /// A program exception has occurred.
        /// </summary>
        Exception
    }
}

