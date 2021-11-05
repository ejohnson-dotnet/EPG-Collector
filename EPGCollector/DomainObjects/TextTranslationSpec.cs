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
    /// The class that defines the requirements for translating text for a channel.
    /// </summary>
    public class TextTranslationSpec
    {
        /// <summary>
        /// The original network ID.
        /// </summary>
        public int OriginalNetworkID { get; set; }
        /// <summary>
        /// The transport stream ID.
        /// </summary>
        public int TransportStreamID { get; set; }
        /// <summary>
        /// The service ID.
        /// </summary>
        public int ServiceID { get; set; }        
        
        /// <summary>
        /// Initialize a new instance of the TextTranslationSpec class.
        /// </summary>
        public TextTranslationSpec() { }

        /// <summary>
        /// Initialize a new instance of the TextTranslationSpec class.
        /// </summary>
        /// <param name="originalNetworkID">The original network ID.</param>
        /// <param name="transportStreamID">The transport stream ID.</param>
        /// <param name="serviceID">The service ID.</param>
        public TextTranslationSpec(int originalNetworkID, int transportStreamID, int serviceID)
        {
            OriginalNetworkID = originalNetworkID;
            TransportStreamID = transportStreamID;
            ServiceID = serviceID;
        }

        /// <summary>
        /// Get a clone of the current instance.
        /// </summary>
        /// <returns>A copy of the current instance</returns>
        public TextTranslationSpec Clone()
        {
            return new TextTranslationSpec(OriginalNetworkID, TransportStreamID, ServiceID);
        }
    }
}

