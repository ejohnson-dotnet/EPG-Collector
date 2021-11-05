////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2020 nzsjb                                           //
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

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes a Schedules Direct metadata entry.
    /// </summary>
    [DataContract]
    public class SchedulesDirectMetadataEntry
    {
        /// <summary>
        /// Get or set the season number.
        /// </summary>
        [DataMember(Name = "season")]
        public int Season { get; set; }

        /// <summary>
        /// Get or set the episode number.
        /// </summary>
        [DataMember(Name = "episode")]
        public int Episode { get; set; }

        /// <summary>
        /// Get or set the total number of seasons.
        /// </summary>
        [DataMember(Name = "totalSeasons")]
        public int TotalSeasons { get; set; }

        /// <summary>
        /// Get or set the total number of episodes.
        /// </summary>
        [DataMember(Name = "totalEpisodes")]
        public int TotalEpisodes { get; set; }

        /// <summary>
        /// Get or set the series ID.
        /// </summary>
        [DataMember(Name = "seriesID")]
        public int SeriesId { get; set; }

        /// <summary>
        /// Get or set the episode ID.
        /// </summary>
        [DataMember(Name = "episodeID")]
        public int EpisodeId { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirectMetadataEntry class.
        /// </summary>
        public SchedulesDirectMetadataEntry() { }
    }
}

