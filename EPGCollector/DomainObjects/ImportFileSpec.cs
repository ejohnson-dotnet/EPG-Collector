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

namespace DomainObjects
{
    /// <summary>
    /// The class that defines an import file.
    /// </summary>
    public class ImportFileSpec
    {
        /// <summary>
        /// Get the name of the file.
        /// </summary>
        public string FileName { get; private set; }
        
        /// <summary>
        /// Get or set the language code of the file (XMLTV only).
        /// </summary>
        public LanguageCode Language { get; set; }
        
        /// <summary>
        /// Get or set the precedence the data takes.
        /// </summary>
        public DataPrecedence Precedence { get; set; }

        /// <summary>
        /// Get or set the flag which inhibits metadatalookup on import data.
        /// </summary>
        public bool NoLookup { get; set; }

        /// <summary>
        /// Get or set the flag which controls the appending of import data.
        /// </summary>
        public bool AppendOnly { get; set; }

        /// <summary>
        /// Get or set the format of the ID attribute.
        /// </summary>
        public XmltvIdFormat IdFormat { get; set; }

        /// <summary>
        /// Get the timezone of the data.
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// Get or set the flag which ignores non-standard episode tags.
        /// </summary>
        public bool IgnoreEpisodeTags { get; set; }

        /// <summary>
        /// Get or set the mode which controls the local caching of images.
        /// </summary>
        public ImportImageMode StoreImagesLocally { get; set; }

        /// <summary>
        /// Get or set the flag which controls the processing of the New tag
        /// </summary>
        public bool ProcessNewTag { get; set; }

        /// <summary>
        /// Get or set the flag which controls the processing of the Live tag
        /// </summary>
        public bool ProcessLiveTag { get; set; }

        /// <summary>
        /// Get or set the flag determines if empty previously showen tags get a default date.
        /// </summary>
        public bool SetPreviouslyShownDefault { get; set; }

        /// <summary>
        /// Get the decode for the ID format property.
        /// </summary>
        public string IdFormatDecode
        {
            get
            {
                switch (IdFormat)
                {
                    case XmltvIdFormat.FullChannelId:
                        return "Full channel ID";
                    case XmltvIdFormat.Name:
                        return "Channel name";
                    case XmltvIdFormat.ServiceId:
                        return "Service ID";
                    case XmltvIdFormat.UserChannelNumber:
                        return "User channel no.";
                    case XmltvIdFormat.Zap2ItAtsc:
                        return "Schedules Direct";
                    default:
                        return "Undefined";
                }
            }
        }

        /// <summary>
        /// Get the decode for the Precedence property.
        /// </summary>
        public string PrecedenceDecode
        {
            get
            {
                switch (Precedence)
                {
                    case DataPrecedence.Broadcast:
                        return "Merge, broadcast precedence";
                    case DataPrecedence.File:
                        return "Merge, import precedence";
                    case DataPrecedence.ImportAppend:
                        return "Append import";
                    case DataPrecedence.ImportReplace:
                        return "Replace with import";
                    default:
                        return "Undefined";
                }
            }            
        }

        private ImportFileSpec() { }

        /// <summary>
        /// Initialize a new instance of the ImportFileSpec class.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        public ImportFileSpec(string fileName)
        {
            FileName = fileName;
        }
    }
}

