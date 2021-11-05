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
    /// The class that describes the image configuration.
    /// </summary>
    [DataContract]
    public class TmdbImageConfiguration
    {
        /// <summary>
        /// Get or set the backdrop sizes.
        /// </summary>
        [DataMember(Name = "backdrop_sizes")]
        public string[] BackdropSizes { get; set; }

        /// <summary>
        /// Get or set the base url.
        /// </summary>
        [DataMember(Name = "base_url")]
        public string BaseUrl { get; set; }
        
        /// <summary>
        /// Get or set the poster sizes.
        /// </summary>
        [DataMember(Name = "poster_sizes")]
        public string[] PosterSizes { get; set; }
        
        /// <summary>
        /// Get or set the profile sizes.
        /// </summary>
        [DataMember(Name = "profile_sizes")]
        public string[] ProfileSizes { get; set; }

        /// <summary>
        /// Get the original size.
        /// </summary>
        public string OriginalSize { get { return "original"; } }        

        /// <summary>
        /// Initialize a new instance of the TmdbImageConfiguration class.
        /// </summary>
        public TmdbImageConfiguration() { }
    }
}
