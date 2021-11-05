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

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DomainObjects
{
    /// <summary>
    /// The class that defines a translation language.
    /// </summary>
    [DataContract]
    public class TextTranslationLanguage
    {
        /// <summary>
        /// Get or set the list of language codes and decodes.
        /// </summary>
        [DataMember(Name = "langs")]
        public IDictionary<string, string> Languages { get; set; }

        /// <summary>
        /// Get the language code.
        /// </summary>
        public string LanguageCode { get; private set; }

        /// <summary>
        /// Get the name of the language.
        /// </summary>
        public string Name { get; private set; }

        private TextTranslationLanguage() { }

        /// <summary>
        /// Initialize a new instance of the TextTranslationLanguage class.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <param name="name">The name of the language.</param>
        public TextTranslationLanguage(string languageCode, string name)
        {
            LanguageCode = languageCode;
            Name = name;
        }

        /// <summary>
        /// Get a string representation of the instance.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}

