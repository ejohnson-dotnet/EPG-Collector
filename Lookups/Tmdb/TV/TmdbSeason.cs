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
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Lookups.Tmdb
{
    /// <summary>
    /// The class that describes a TV season.
    /// </summary>
    [DataContract]
    public class TmdbSeason
    {
        /// <summary>
        /// Get or set the release date as a string.
        /// </summary>
        [DataMember(Name = "air_date")]
        public string AirDateString { get; set; }

        /// <summary>
        /// Get or set the episode list.
        /// </summary>
        [DataMember(Name = "episodes")]
        public Collection<TmdbEpisode> Episodes { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the overview.
        /// </summary>
        [DataMember(Name = "overview")]
        public string Overview { get; set; }

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
        /// Get or set the season number.
        /// </summary>
        [DataMember(Name = "season_number")]
        public int SeasonNumber { get; set; }

        /// <summary>
        /// Initialize a new instance of the TmdbMovie class.
        /// </summary>
        public TmdbSeason() { }
    }
}
