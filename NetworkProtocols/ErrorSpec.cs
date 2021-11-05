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

using System;
using System.Text;

namespace NetworkProtocols
{
    /// <summary>
    /// The class that describes an error condition.
    /// </summary>
    public class ErrorSpec
    {
        /// <summary>
        /// Get or set the protocol.
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// Get or set the error code.
        /// </summary>
        public ErrorCode ErrorCode { get; set; }
        /// <summary>
        /// Get or set the original error code.
        /// </summary>
        public int OriginalErrorCode { get; set; }
        /// <summary>
        /// Get or set the error message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Get or set any additional data about the error.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Initialize a new instance of the ErrorSpec class.
        /// </summary>
        public ErrorSpec() { }

        /// <summary>
        /// Initialize a new instance of the ErrorSpec class.
        /// </summary>
        /// <param name="protocol">The protocol which generated the error.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="originalErrorCode">The original error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="data">Any additional data about the error.</param>
        public ErrorSpec(string protocol, ErrorCode errorCode, int originalErrorCode, string message, string data)
        {
            storeParameters(protocol, errorCode, originalErrorCode, message, data);
        }

        /// <summary>
        /// Initialize a new instance of the ErrorSpec class.
        /// </summary>
        /// <param name="protocol">The protocol which generated the error.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="originalErrorCode">The original error code.</param>
        /// <param name="message">The error message.</param>
        /// <param name="data">Any additional data about the error.</param>
        public ErrorSpec(string protocol, ErrorCode errorCode, string originalErrorCode, string message, string data)
        {
            try
            {
                int responseCode = Int32.Parse(originalErrorCode);
                storeParameters(protocol, errorCode, responseCode, message, data);
            }
            catch (FormatException)
            {
                storeParameters(protocol, errorCode, 0, message, data);
            }
            catch (OverflowException)
            {
                storeParameters(protocol, errorCode, 0, message, data);
            }
        }

        private void storeParameters(string protocol, ErrorCode errorCode, int originalErrorCode, string message, string data)
        {
            Protocol = protocol;
            ErrorCode = errorCode;
            OriginalErrorCode = originalErrorCode;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Convert the values of this instance to a readable string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder reply = new StringBuilder();

            reply.Append(Protocol + ": Error " + ErrorCode + "/" + OriginalErrorCode + " " + Message);

            if (Data != null)
                reply.Append(" Data=" + Data);

            return (reply.ToString());
        }
    }
}

