////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2020 nzsjb                                             //
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
using System.Runtime.Serialization;

namespace Lookups.Tmdb
{
    /// <summary>
    /// The class that describes the credits for a person.
    /// </summary>
    [DataContract]
    public class TmdbPersonCredit
    {
        /// <summary>
        /// Get or set the name of the character.
        /// </summary>
        [DataMember(Name = "character")]
        public string Character { get; set; }
        
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }
        
        /// <summary>
        /// Get or set the original title.
        /// </summary>
        [DataMember(Name = "original_title")]
        public string OriginalTitle { get; set; }
        
        /// <summary>
        /// Get or set the poster path.
        /// </summary>
        [DataMember(Name = "poster_path")]
        public string PosterPath { get; set; }

        /// <summary>
        /// Get or set the release date as a string.
        /// </summary>
        [DataMember(Name = "release_date")]
        public string ReleaseDateString
        {
            get { return releaseDateString; }
            set { releaseDateString = value; }
        }
        
        /// <summary>
        /// Get or set the title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Get the release date.
        /// </summary>
        public DateTime ReleaseDate
        {
            get
            {
                if (releaseDateString == null)
                    return new DateTime();
                else
                {
                    try
                    {
                        return DateTime.Parse(releaseDateString);
                    }
                    catch (FormatException)
                    {
                        return new DateTime();
                    }
                }
            }
        }

        private string releaseDateString;

        /// <summary>
        /// Initialize a new instance of the TmdbPersonCredit class.
        /// </summary>
        public TmdbPersonCredit() { }

        /// <summary>
        /// Get the poster image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetPosterImage(TmdbAPI instance, string fileName)
        {
            return instance.GetImage(ImageType.Poster, PosterPath, -1, fileName);
        }
    }
}
