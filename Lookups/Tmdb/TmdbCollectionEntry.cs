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
    /// The class that describes the collection data structure.
    /// </summary>
    public class TmdbCollectionEntry
    {
        /// <summary>
        /// Get or set the path to the backdrop.
        /// </summary>
        [DataMember(Name = "backdrop_path")]
        public string BackdropPath { get; set; }

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the poster path.
        /// </summary>
        [DataMember(Name = "poster_path")]
        public string PosterPath { get; set; }

        /// <summary>
        /// Get or set the release date.
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
                    return DateTime.Parse(releaseDateString);
            }
        }

        private string releaseDateString;

        /// <summary>
        /// Initialize a new instance of the TmdbCollectionEntry class.
        /// </summary>
        public TmdbCollectionEntry() { }

        /// <summary>
        /// Get the backdrop image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetBackdropImage(TmdbAPI instance, string fileName)
        {
            return instance.GetImage(ImageType.Backdrop, BackdropPath, -1, fileName);
        }

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
