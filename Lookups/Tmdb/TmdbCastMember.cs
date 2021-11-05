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
    /// The class that describes a cast member.
    /// </summary>
    [DataContract]
    public class TmdbCastMember
    {        
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
        /// Get or set the character.
        /// </summary>
        [DataMember(Name = "character")]
        public string Character { get; set; }

        /// <summary>
        /// Get or set the order.
        /// </summary>
        [DataMember(Name = "order")]
        public int Order { get; set; }

        /// <summary>
        /// Get or set the profile path.
        /// </summary>
        [DataMember(Name = "profile_path")]
        public string ProfilePath { get; set; }

        /// <summary>
        /// Initialize a new instance of the TmdbCastMember class.
        /// </summary>
        public TmdbCastMember() { }

        /// <summary>
        /// Get the profile image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetProfileImage(TmdbAPI instance, string fileName)
        {
            return instance.GetImage(ImageType.Profile, ProfilePath, -1, fileName);
        }
    }
}
