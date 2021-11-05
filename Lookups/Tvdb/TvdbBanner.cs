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

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a series banner.
    /// </summary>
    [DataContract]
    public class TvdbBanner
    {
        /// <summary>
        /// Get or set the path.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the key type.
        /// </summary>
        [DataMember(Name = "keyType")]
        public string KeyType { get; set; }

        /// <summary>
        /// Get or set the sub key.
        /// </summary>
        [DataMember(Name = "subKey")]
        public string SubKey { get; set; }

        /// <summary>
        /// Get or set the filename.
        /// </summary>
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Get or set the resolution.
        /// </summary>
        [DataMember(Name = "resolution")]
        public string Resolution { get; set; }

        /// <summary>
        /// Get or set the ratings info.
        /// </summary>
        [DataMember(Name = "ratingsInfo")]
        public TvdbBannerRatingsInfo RatingsInfo { get; set; }

        /// <summary>
        /// Get or set the thumbnail filename.
        /// </summary>
        [DataMember(Name = "thumbnail")]
        public string ThumbNail { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbBanner class.
        /// </summary>
        public TvdbBanner() { }

        /// <summary>
        /// Check if the banner is a particular type.
        /// </summary>
        /// <param name="imageType">The banner type.</param>
        /// <returns>True if the type matches; false otherwise.</returns>
        public bool IsType(TvdbAPI.ImageType imageType)
        {
            switch (imageType)
            {
                case TvdbAPI.ImageType.Poster:
                    return KeyType == "poster";
                case TvdbAPI.ImageType.FanArt:
                    return KeyType == "fanart";
                case TvdbAPI.ImageType.Season:
                    return KeyType == "season";
                default:
                    return false;
            }
        }
    }
}
