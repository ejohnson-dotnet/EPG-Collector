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
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text;

namespace DomainObjects
{
    /// <summary>
    /// The class that describes a translation response.
    /// </summary>
    [DataContract]
    public class TextTranslationReply
    {
        /// <summary>
        /// Get or set the response code.
        /// </summary>
        [DataMember(Name = "code")]
        public long ResponseCode { get; set; }

        /// <summary>
        /// Get or set the languages detected.
        /// </summary>
        [DataMember(Name = "detected")]
        public TextTranslationDetected Detected { get; set; }

        /// <summary>
        /// Get or set the conversion requested.
        /// </summary>
        [DataMember(Name = "lang")]
        public string LanguageSpec { get; set; }

        /// <summary>
        /// Get or set the translated text.
        /// </summary>
        [DataMember(Name = "text")]
        public Collection<string> Translations { get; set; }

        /// <summary>
        /// Initialize a new instance of the TextTranslationReply class.
        /// </summary>
        public TextTranslationReply() { }
    }
}

