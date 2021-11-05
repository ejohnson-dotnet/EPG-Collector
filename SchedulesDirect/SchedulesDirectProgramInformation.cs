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

using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SchedulesDirect
{
    /// <summary>
    /// The class the describes the Schedules Direct programme information.
    /// </summary>
    [DataContract]
    public class SchedulesDirectProgramInformation
    {
        /// <summary>
        /// Get or set the programme identifier.
        /// </summary>
        [DataMember(Name = "programID")]
        public string ProgramId { get; set; }

        /// <summary>
        /// Get or set the list of programme titles.
        /// </summary>
        [DataMember(Name = "titles")]
        public Collection<SchedulesDirectProgramTitle> Titles { get; set; }

        /// <summary>
        /// Get or set the episode title.
        /// </summary>
        [DataMember(Name = "episodeTitle")]
        public string EpisodeTitle { get; set; }

        /// <summary>
        /// Get or set the programme descriptions.
        /// </summary>
        [DataMember(Name = "descriptions")]
        public SchedulesDirectDescriptions Descriptions { get; set; }

        /// <summary>
        /// Get the event details information.
        /// </summary>
        [DataMember(Name = "eventDetails")]
        public SchedulesDirectEventDetails EventDetails { get; set; }

        /// <summary>
        /// Get or set the original air date.
        /// </summary>
        [DataMember(Name = "originalAirDate")]
        public DateTime? OriginalAirDate { get; set; }

        /// <summary>
        /// Get or set the list of genres.
        /// </summary>
        [DataMember(Name = "genres")]
        public Collection<string> Genres { get; set; }

        /// <summary>
        /// Get or set the programme metadata entries.
        /// </summary>
        [DataMember(Name = "metadata")]
        public Collection<SchedulesDirectProgramMetadata> MetadataEntries { get; set; }

        /// <summary>
        /// Get or set the list of cast members.
        /// </summary>
        [DataMember(Name = "cast")]
        public Collection<SchedulesDirectCastMember> Cast { get; set; }

        /// <summary>
        /// Get or set the list of crew members.
        /// </summary>
        [DataMember(Name = "crew")]
        public Collection<SchedulesDirectCastMember> Crew { get; set; }

        /// <summary>
        /// Get or set the entity type.
        /// </summary>
        [DataMember(Name = "entityType")]
        public string EntityType { get; set; }

        /// <summary>
        /// Get or set the show type.
        /// </summary>
        [DataMember(Name = "showType")]
        public string ShowType { get; set; }

        /// <summary>
        /// Returns true if the programme has artwork; false otherwise.
        /// </summary>
        [DataMember(Name = "hasImageArtwork")]
        public bool HasImageArtwork { get; set; }

        /// <summary>
        /// Returns true if there is series artwork available; false otherwise.
        /// </summary>
        [DataMember(Name = "hasSeriesArtwork")]
        public bool HasSeriesArtwork { get; set; }

        /// <summary>
        /// Returns true if there is episode artwork available; false otherwise.
        /// </summary>
        [DataMember(Name = "hasEpisodeArtwork")]
        public bool HasEpisodeArtwork { get; set; }

        /// <summary>
        /// Get or set movie information for the programme.
        /// </summary>
        [DataMember(Name = "movie")]
        public SchedulesDirectMovie Movie { get; set; }

        /// <summary>
        /// Get or set the episode image.
        /// </summary>
        [DataMember(Name = "episodeImage")]
        public SchedulesDirectEpisodeImage EpisodeImage { get; set; }

        /// <summary>
        /// Get or set the MD5 checksum for the programme.
        /// </summary>
        [DataMember(Name = "md5")]
        public string Md5 { get; set; }

        /// <summary>
        /// Initialize a new instance of the SchedulesDirecProgrammeInfoirmation class.
        /// </summary>
        public SchedulesDirectProgramInformation() { }
    }
}

