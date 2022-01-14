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
    /// The class that describes a TV episode.
    /// </summary>
    [DataContract]
    public class TmdbEpisode
    {
        /// <summary>
        /// Get or set the air date as a string.
        /// </summary>
        [DataMember(Name = "air_date")]
        public string AirDateString { get; set; } 
        
        /// <summary>
        /// Get or set the episode number.
        /// </summary>
        [DataMember(Name = "episode_number")]
        public int EpisodeNumber { get; set; }

        /// <summary>
        /// Get or set the episode name.
        /// </summary>
        [DataMember(Name = "name")]
        public string EpisodeName { get; set; }

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
        /// Get or set the season number.
        /// </summary>
        [DataMember(Name = "season_number")]
        public int SeasonNumber { get; set; }

        /// <summary>
        /// Get or set the average vote.
        /// </summary>
        [DataMember(Name = "vote_average")]
        public decimal VoteAverage { get; set; }
        
        /// <summary>
        /// Get or set the number of votes.
        /// </summary>
        [DataMember(Name = "vote_count")]
        public int VoteCount { get; set; }        

        /// <summary>
        /// Get or set the list of cast members.
        /// </summary>
        public TmdbCast Cast { get; set; }

        /// <summary>
        /// Get or set the list of images.
        /// </summary>
        public TmdbImages Images { get; set; }

        /// <summary>
        /// Get the first aired date.
        /// </summary>
        public DateTime AirDate
        {
            get
            {
                if (AirDateString == null)
                    return new DateTime();
                else
                {
                    try
                    {
                        return DateTime.Parse(AirDateString);
                    }
                    catch (FormatException)
                    {
                        return new DateTime();
                    }
                }
            }
        }

        /// <summary>
        /// Initialize a new instance of the TmdbMovie class.
        /// </summary>
        public TmdbEpisode() { }

        /// <summary>
        /// Load this instance with the optional data for the movie.
        /// </summary>
        public void LoadLists(TmdbAPI instance, int seriesId, int seasonNumber)
        {
            try
            {
                Cast = instance.GetEpisodeCast(seriesId, seasonNumber, Identity);
                Images = instance.GetSeriesImages(Identity);
            }
            catch (Exception) { }        
        }

        /// <summary>
        /// Get the poster image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetPosterImage(TmdbAPI instance, string fileName)
        {
            if (Images == null)
                return false;

            return instance.GetImage(ImageType.Poster, Images.Posters[0].FilePath, -1, fileName);
        }
    }
}
