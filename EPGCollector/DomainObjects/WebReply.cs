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
    /// The reply class for web replies.
    /// </summary>
    public class WebReply : ReplyBase
    {
        /// <summary>
        /// Get or set the HTTP location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Initialize a new instance of the WebReply class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="responseData">The response data.</param>
        public WebReply(string message, object responseData) : base(message, responseData) {}

        /// <summary>
        /// Initialize a new instance of the WebReply class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="responseData">The response data.</param>
        /// <param name="location">The HTTP location.</param>
        public WebReply(string message, object responseData, string location) : base(message, responseData)
        {
            Location = location;
        }

        /// <summary>
        /// Initialize a new instance of the WebReply class.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns></returns>
        public static WebReply LocationReply(string location)
        {
            return new WebReply(null, null, location);
        }
    }
}

