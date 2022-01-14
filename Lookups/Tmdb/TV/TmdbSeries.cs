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
    /// The class that describes a TV series.
    /// </summary>
    [DataContract]
    public class TmdbSeries
    {
        /// <summary>
        /// Get or set the poster path.
        /// </summary>
        [DataMember(Name = "backdrop_path")]
        public string BackdropPath { get; set; }

        /// <summary>
        /// Get or set the first air date as a string.
        /// </summary>
        [DataMember(Name = "first_air_date")]
        public string FirstAirDateString { get; set; }

        /// <summary>
        /// Get or set the genres.
        /// </summary>
        [DataMember(Name = "genres")]
        public Collection<int> GenresIds { get; set; }

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }
        
        /// <summary>
        /// Get or set the series name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the original language.
        /// </summary>
        [DataMember(Name = "original_language")]
        public string OriginalLanguage { get; set; }

        /// <summary>
        /// Get or set the original name.
        /// </summary>
        [DataMember(Name = "original_name")]
        public string OriginalName { get; set; }

        /// <summary>
        /// Get or set the origin countries.
        /// </summary>
        [DataMember(Name = "origin_country")]
        public Collection<string> OriginCountries { get; set; }

        /// <summary>
        /// Get or set the series overview.
        /// </summary>
        [DataMember(Name = "overview")]
        public string Overview { get; set; }

        /// <summary>
        /// Get or set the poster path.
        /// </summary>
        [DataMember(Name = "poster_path")]
        public string PosterPath { get; set; }

        /// <summary>
        /// Get or set the popularity.
        /// </summary>
        [DataMember(Name = "popularity")]
        public decimal? Popularity { get; set; }

        /// <summary>
        /// Get or set the average vote.
        /// </summary>
        [DataMember(Name = "vote_average")]
        public decimal? VoteAverage { get; set; }

        /// <summary>
        /// Get or set the number of votes.
        /// </summary>
        [DataMember(Name = "vote_count")]
        public int? VoteCount { get; set; }

        /// <summary>
        /// Get or set the series genre.
        /// </summary>
        public Collection<TmdbGenre> Genres { get; set; }

        /// <summary>
        /// Get or set the list of cast members.
        /// </summary>
        public TmdbCast Cast { get; set; }
        
        /// <summary>
        /// Get the first aired date.
        /// </summary>
        public DateTime FirstAirDate
        {
            get
            {
                if (FirstAirDateString == null)
                    return new DateTime();
                else
                {
                    try
                    {
                        return DateTime.Parse(FirstAirDateString);
                    }
                    catch (FormatException)
                    {
                        return new DateTime();
                    }
                }
            }
        }

        /// <summary>
        /// Get the list of seasons.
        /// </summary>
        public Collection<TmdbSeason> Seasons { get; private set; }

        private bool detailsLoaded;

        /// <summary>
        /// Initialize a new instance of the TmdbMovie class.
        /// </summary>
        public TmdbSeries() { }

        internal void LoadDetails(TmdbAPI instance)
        {
            if (detailsLoaded)
                return;

            Cast = instance.GetSeriesCast(Identity);

            TmdbSeriesDetail details = TmdbSeriesDetail.GetDetails(instance, Identity);

            Genres = details.Genres;
            int seasonNumber = 1;

            while (seasonNumber <= details.NumberOfSeasons)
            {
                if (Seasons == null)
                    Seasons = new Collection<TmdbSeason>();

                try
                {
                    TmdbSeason season = instance.GetSeasonDetails(Identity, seasonNumber);
                    Seasons.Add(season);
                }
                catch (Exception) 
                {
                    TmdbSeason season = new TmdbSeason();
                    season.SeasonNumber = seasonNumber;
                    Seasons.Add(season);                    
                }

                seasonNumber++;
            }

            detailsLoaded = true;
        }

        /// <summary>
        /// Get the backdrop image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetBackdropImage(TmdbAPI instance, string fileName)
        {
            if (BackdropPath == null)
                return false;

            return instance.GetImage(ImageType.Backdrop, BackdropPath, -1, fileName);
        }

        /// <summary>
        /// Get the poster image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetPosterImage(TmdbAPI instance, string fileName)
        {
            if (PosterPath == null)
                return false;

            return instance.GetImage(ImageType.Poster, PosterPath, -1, fileName);
        }

        /// <summary>
        /// Search for a TV show.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="title">The title or part of it.</param>
        /// <returns>The results object.</returns>
        public static TmdbSeriesSearchResults Search(TmdbAPI instance, string title)
        {
            return Search(instance, title, null);
        }

        /// <summary>
        /// Search for a TV show with a language code.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="title">The title or part of it.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The results object.</returns>
        public static TmdbSeriesSearchResults Search(TmdbAPI instance, string title, string languageCode)
        {
            return instance.SearchForSeries(title, languageCode);
        }
    }
}
