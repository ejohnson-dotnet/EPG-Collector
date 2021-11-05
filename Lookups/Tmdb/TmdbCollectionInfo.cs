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

using System.Runtime.Serialization;

namespace Lookups.Tmdb
{
    /// <summary>
    /// The class that describes a movie collection.
    /// </summary>
    [DataContract]
    public class TmdbCollectionInfo
    {
        /// <summary>
        /// Get or set the backdrop path.
        /// </summary>
        [DataMember(Name = "backdrop_path")]
        public string BackdropPath { get; set; }

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the list of collection entries.
        /// </summary>
        [DataMember(Name = "parts")]
        public TmdbCollectionEntry[] CollectionEntry { get; set; }

        /// <summary>
        /// Get or set the poster path.
        /// </summary>
        [DataMember(Name = "poster_path")]
        public string PosterPath { get; set; }

        /// <summary>
        /// Initialize a new instance of the TmdbCollectionInfo class.
        /// </summary>
        public TmdbCollectionInfo() { }

        /// <summary>
        /// Get the backdrop image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public void GetBackdropImage(TmdbAPI instance, string fileName)
        {
            instance.GetImage(ImageType.Backdrop, BackdropPath, -1, fileName);
        }

        /// <summary>
        /// Get the poster image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public void GetPosterImage(TmdbAPI instance, string fileName)
        {
            instance.GetImage(ImageType.Poster, PosterPath, -1, fileName);
        }
    }
}
