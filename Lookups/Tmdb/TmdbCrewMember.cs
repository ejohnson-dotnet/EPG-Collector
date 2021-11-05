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
    /// The class that describes a crew member.
    /// </summary>
    [DataContract]
    public class TmdbCrewMember
    {
        /// <summary>
        /// Get or set the department.
        /// </summary>
        [DataMember(Name = "department")]
        public string Department { get; set; }

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the job.
        /// </summary>
        [DataMember(Name = "job")]
        public string Job { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the profile path.
        /// </summary>
        [DataMember(Name = "profile_path")]
        public string ProfilePath { get; set; }

        /// <summary>
        /// Initialize a new instance of the TmdbCrewMember class.
        /// </summary>
        public TmdbCrewMember() { }

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
