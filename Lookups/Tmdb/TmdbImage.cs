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
    /// The class that describes an image.
    /// </summary>
    [DataContract]
    public class TmdbImage
    {
        /// <summary>
        /// Get or set the aspect ratio.
        /// </summary>
        [DataMember(Name = "aspect_ratio")]
        public decimal AspectRatio { get; set; }

        /// <summary>
        /// Get or set the file path.
        /// </summary>
        [DataMember(Name = "file_path")]
        public string FilePath { get; set; }
        
        /// <summary>
        /// Get or set the height.
        /// </summary>
        [DataMember(Name = "height1")]
        public int Height { get; set; }
        
        /// <summary>
        /// Get or set the country code.
        /// </summary>
        [DataMember(Name = "iso_639_1")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Get or set the width.
        /// </summary>
        [DataMember(Name = "width")]
        public int Width { get; set; }

        /// <summary>
        /// Initialize a new instance of the TmdbImage class.
        /// </summary>
        public TmdbImage() { }

        /// <summary>
        /// Get the image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="imageType">The type of image.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public void GetImage(TmdbAPI instance, ImageType imageType, string fileName)
        {
            instance.GetImage(imageType, FilePath, -1, fileName);
        }
    }
}
