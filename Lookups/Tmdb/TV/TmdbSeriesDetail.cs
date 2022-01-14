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
    /// The class that describes a TV series detail.
    /// </summary>
    [DataContract]
    public class TmdbSeriesDetail
    {  
        /// <summary>
        /// Get or set the genres.
        /// </summary>
        [DataMember(Name = "genres")]
        public Collection<TmdbGenre> Genres { get; set; }

        /// <summary>
        /// Get or set the number of seasons.
        /// </summary>
        [DataMember(Name = "number_of_seasons")]
        public int NumberOfSeasons { get; set; }

        /// <summary>
        /// Initialize a new instance of the TmdbSeriesDetail class.
        /// </summary>
        public TmdbSeriesDetail() { }

        /// <summary>
        /// Get series details.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="identity">The identity of the series.</param>
        /// <returns>The results object.</returns>
        public static TmdbSeriesDetail GetDetails(TmdbAPI instance, int identity)
        {
            return instance.GetSeriesDetail(identity);
        }
    }
}
