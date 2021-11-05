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
    /// The class that describes the image summary structure.
    /// </summary>
    [DataContract]
    public class TvdbImageSummary
    {
        /// <summary>
        /// Get or set the fan art count.
        /// </summary>
        [DataMember(Name = "fanart")]
        public int FanArtCount { get; set; }

        /// <summary>
        /// Get or set the poster count.
        /// </summary>
        [DataMember(Name = "poster")]
        public int PosterCount { get; set; }

        /// <summary>
        /// Get or set the season count.
        /// </summary>
        [DataMember(Name = "season")]
        public int SeasonCount { get; set; }

        /// <summary>
        /// Get or set the season wide count.
        /// </summary>
        [DataMember(Name = "seasonwide")]
        public int SeasonWide { get; set; }

        /// <summary>
        /// Get or set the series count.
        /// </summary>
        [DataMember(Name = "series")]
        public int SeriesCount { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbImageSummary class.
        /// </summary>
        public TvdbImageSummary() { }
    }
}
