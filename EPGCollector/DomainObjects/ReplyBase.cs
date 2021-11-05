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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainObjects
{
    /// <summary>
    /// The base class for reply data.
    /// </summary>
    public class ReplyBase
    {
        /// <summary>
        /// Get or set the message.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Get or set the response data.
        /// </summary>
        public object ResponseData { get; set; }
        
        private ReplyBase() { }

        /// <summary>
        /// Iniotialize a new instance of the ReplyBase class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="responseData">The response data.</param>
        public ReplyBase(string message, object responseData)
        {
            Message = message;
            ResponseData = responseData;
        }

        /// <summary>
        /// Create an instance of the ReplyBase class with no message or response data.
        /// </summary>
        /// <returns>An empty ReplyBase instance.</returns>
        public static ReplyBase NoReply()
        {
            return new ReplyBase(null, null);
        }

        /// <summary>
        /// Create an instance of the ReplyBase class with an error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <returns></returns>
        public static ReplyBase ErrorReply(string message)
        {
            return new ReplyBase(message, null);
        }

        /// <summary>
        /// Create an instance of the ReplyBase class with a data response.
        /// </summary>
        /// <param name="responseData">The data response.</param>
        /// <returns></returns>
        public static ReplyBase DataReply(object responseData)
        {
            return new ReplyBase(null, responseData);
        }
    }
}

