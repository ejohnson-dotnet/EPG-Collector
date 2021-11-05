////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2016 nzsjb                                             //
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
using System.Net;
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a series.
    /// </summary>
    [DataContract]
    public class TvdbSeries
    {
        /// <summary>
        /// Get or set the abbreviation.
        /// </summary>
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// Get or set the air days.
        /// </summary>
        [DataMember(Name = "airsDays")]
        public TvdbAirsDays AirsDays { get; set; }

        /// <summary>
        /// Get or set the airs time.
        /// </summary>
        [DataMember(Name = "airsTime")]
        public string AirsTime { get; set; }

        /// <summary>
        /// Get or set the aliases.
        /// </summary>
        [DataMember(Name = "aliases")]
        public Collection<TvdbAlias> Aliases { get; set; }

        /// <summary>
        /// Get or set the artworks.
        /// </summary>
        [DataMember(Name = "artWorks")]
        public Collection<TvdbArtwork> Artworks { get; set; }

        /// <summary>
        /// Get or set the average runtime.
        /// </summary>
        [DataMember(Name = "averageRuntime")]
        public int? AverageRuntime { get; set; }

        /// <summary>
        /// Get or set the characters.
        /// </summary>
        [DataMember(Name = "characters")]
        public Collection<TvdbCharacter> Characters { get; set; }

        /// <summary>
        /// Get or set the companies.
        /// </summary>
        [DataMember(Name = "companies")]
        public Collection<TvdbCompany> Companies { get; set; }

        /// <summary>
        /// Get or set the country.
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Get or set the default season type.
        /// </summary>
        [DataMember(Name = "defaultSeasonType")]
        public string DefaultSeasonType { get; set; }

        /// <summary>
        /// Get or set the episodes.
        /// </summary>
        [DataMember(Name = "episodes")]
        public Collection<TvdbEpisode> Episodes { get; set; }

        /// <summary>
        /// Get or set the first air date string.
        /// </summary>
        [DataMember(Name = "firstAired")]
        public string FirstAired { get; set; }

        /// <summary>
        /// Get or set the genres.
        /// </summary>
        [DataMember(Name = "genres")]
        public Collection<TvdbGenre> Genres { get; set; }

        /// <summary>
        /// Get or set the internal identity.
        /// </summary>
        [DataMember(Name = "id")]
        public string Identity { get; set; }

        /// <summary>
        /// Get or set the image.
        /// </summary>
        [DataMember(Name = "image")]
        public string Image { get; set; }

        /// <summary>
        /// Get or set if the order randomized.
        /// </summary>
        [DataMember(Name = "isOrderRandomized")]
        public bool IsOrderRandomized { get; set; }

        /// <summary>
        /// Get or set the last air date string.
        /// </summary>
        [DataMember(Name = "lastAired")]
        public string LastAired { get; set; }

        /// <summary>
        /// Get or set the lists.
        /// </summary>
        [DataMember(Name = "lists")]
        public Collection<TvdbList> Lists { get; set; }

        /// <summary>
        /// Get or set the series name.
        /// </summary>
        [DataMember(Name = "name")]
        public string SeriesName { get; set; }

        /// <summary>
        /// Get or set the series name translations.
        /// </summary>
        [DataMember(Name = "nameTranslations")]
        public Collection<string> NameTranslations { get; set; }

        /// <summary>
        /// Get or set the next air date string.
        /// </summary>
        [DataMember(Name = "nextAired")]
        public string NextAired { get; set; }

        /// <summary>
        /// Get or set the original country.
        /// </summary>
        [DataMember(Name = "originalCountry")]
        public string OriginalCountry { get; set; }

        /// <summary>
        /// Get or set the original language.
        /// </summary>
        [DataMember(Name = "originalLanguage")]
        public string OriginalLanguage { get; set; }

        /// <summary>
        /// Get or set the overview translations.
        /// </summary>
        [DataMember(Name = "overviewTranslations")]
        public Collection<string> OverviewTranslations { get; set; }

        /// <summary>
        /// Get or set the remote ID's.
        /// </summary>
        [DataMember(Name = "remoteIds")]
        public Collection<TvdbRemoteId> RemoteIds { get; set; }

        /// <summary>
        /// Get or set the score.
        /// </summary>
        [DataMember(Name = "score")]
        public double? Score { get; set; }

        /// <summary>
        /// Get or set the seasons.
        /// </summary>
        [DataMember(Name = "seasons")]
        public Collection<TvdbSeason> Seasons { get; set; }

        /// <summary>
        /// Get or set the slug.
        /// </summary>
        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Get or set the status.
        /// </summary>
        [DataMember(Name = "status")]
        public TvdbStatus Status { get; set; }

        /// <summary>
        /// Get or set the trailers.
        /// </summary>
        [DataMember(Name = "trailers")]
        public Collection<TvdbTrailer> Trailers { get; set; }

        /// <summary>
        /// Get or set the translations.
        /// </summary>
        [DataMember(Name = "translations")]
        public TvdbTranslations Translations { get; set; }

        /// <summary>
        /// Get or set the first air date.
        /// </summary>
        public DateTime? FirstAiredDate { get { return TvdbUtils.StringToDate(FirstAired); } }

        /// <summary>
        /// Get the string representation of the first aired date.
        /// </summary>
        public string FirstAiredString
        {
            get
            {
                if (FirstAiredDate == null || !FirstAiredDate.HasValue)
                    return string.Empty;
                else
                    return FirstAiredDate.Value.ToShortDateString();
            }
        }

        /// <summary>
        /// Get the running time. Returns null if not available or invalid.
        /// </summary>
        public TimeSpan? RunningTime
        {
            get
            {
                if (AverageRuntime == null)
                    return null;
                else
                    return new TimeSpan((long)(AverageRuntime * TimeSpan.TicksPerMinute));                
            }
        }

        /// <summary>
        /// Get the string representation of the running time.
        /// </summary>
        public string RunningTimeString
        {
            get
            {
                if (RunningTime == null || !RunningTime.HasValue)
                    return string.Empty;
                else
                    return RunningTime.Value.ToString();
            }
        }
            
        /// <summary>
        /// Get a comma separated string of genres.
        /// </summary>
        public string GenresDisplayString 
        { 
            get 
            {
                if (Genres == null)
                    return string.Empty;

                string genres = string.Empty;

                foreach (TvdbGenre genre in Genres)
                {
                    if (genres.Length != 0)
                        genres += ", ";
                    genres += genre.Name;

                }
                
                return genres; 
            } 
        }

        /// <summary>
        /// Get a list of actors.
        /// </summary>
        public Collection<string> Actors { get { return TvdbCharacter.GetCharacterNames(Characters, new string[] { "actor", "guest star" }); } }

        /// <summary>
        /// Get a comma separated string of actors.
        /// </summary>
        public string ActorsDisplayString { get { return TvdbUtils.CollectionToString(TvdbCharacter.GetCharacterNames(Characters, new string[] {"actor" })); } }

        /// <summary>
        /// Get a list of directors.
        /// </summary>
        public Collection<string> Directors { get { return TvdbCharacter.GetCharacterNames(Characters, new string[] { "director" }); } }

        /// <summary>
        /// Get a comma separated string of directors.
        /// </summary>
        public string DirectorsDisplayString { get { return TvdbUtils.CollectionToString(TvdbCharacter.GetCharacterNames(Characters, new string[] {"director"} )); } }

        /// <summary>
        /// Get a list of writers.
        /// </summary>
        public Collection<string> Writers { get { return TvdbCharacter.GetCharacterNames(Characters, new string[] { "creator", "writer" }); } }

        /// <summary>
        /// Get a list of guest stars.
        /// </summary>
        public Collection<string> GuestStars { get { return TvdbCharacter.GetCharacterNames(Characters, new string[] { "guest star" }); } }

        /// <summary>
        /// Get a comma separated string of guest stars.
        /// </summary>
        public string GuestStarsDisplayString { get { return TvdbUtils.CollectionToString(TvdbCharacter.GetCharacterNames(Characters, new string[] { "guest star" })); } }
        
        /// <summary>
        /// Get a comma separated string of writers.
        /// </summary>
        public string WritersDisplayString { get { return TvdbUtils.CollectionToString(TvdbCharacter.GetCharacterNames(Characters, new string[] {"creator", "writer"} )); } }

        /// <summary>
        /// Get the last error message.
        /// </summary>
        public string LastError { get; private set; }

        /// <summary>
        /// Initialize a new instance of the TvdbSeries class.
        /// </summary>
        public TvdbSeries() { }

        /// <summary>
        /// Search for series given a title.
        /// </summary>
        /// <param name="instance">An API instance..</param>
        /// <param name="title">The title to search for.</param>
        /// <returns>The results object.</returns>
        public static TvdbSeriesSearchResult Search(TvdbAPI instance, string title)
        {
            return instance.GetSeries(title, null);
        }

        /// <summary>
        /// Search for a series in an specific language.
        /// </summary>
        /// <param name="instance">An API instance.</param>
        /// <param name="title">Part or all of the title.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The results object.</returns>
        public static TvdbSeriesSearchResult Search(TvdbAPI instance, string title, string languageCode)
        {
            return instance.GetSeries(title, languageCode);
        }

        /// <summary>
        /// Get a banner.
        /// </summary>
        /// <param name="instance">An API instance.</param>
        /// <param name="fileName">The full name of the file to create.</param>
        /// <returns>Returns true if the banner was downloaded to the file; false otherwise.</returns>
        public bool GetBanner(TvdbAPI instance, string fileName)
        {
            TvdbArtwork selectedBanner = findBanner(instance, TvdbAPI.ImageType.Banner, false);
            if (selectedBanner == null)
                return false;

            try
            {
                return instance.GetImage(TvdbAPI.ImageType.Banner, selectedBanner.Image, 0, fileName);
            }
            catch (WebException e)
            {
                HttpWebResponse response = e.Response as HttpWebResponse;
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                    return false;
                throw e;
            }
        }

        /// <summary>
        /// Get the poster image.
        /// </summary>
        /// <param name="instance">An API instance.</param>
        /// <param name="fileName">The output filename.</param>
        /// <returns>True if the poster was downloaded; false otherwise.</returns>
        public bool GetPoster(TvdbAPI instance, string fileName)
        {
            TvdbArtwork selectedBanner = findBanner(instance, TvdbAPI.ImageType.Poster, false);
            if (selectedBanner == null)
                return false;

            try
            {
                return instance.GetImage(TvdbAPI.ImageType.Poster, selectedBanner.Image, 0, fileName);
            }
            catch (WebException e)
            {
                HttpWebResponse response = e.Response as HttpWebResponse;
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                    return false;
                throw e;
            }      
        }

        /// <summary>
        /// Get the small poster image.
        /// </summary>
        /// <param name="instance">An API instance.</param>
        /// <param name="fileName">The name of the output file.</param>
        /// <returns>True if the image was downloaded; false otherwise.</returns>
        public bool GetSmallPoster(TvdbAPI instance, string fileName)
        {
            TvdbArtwork selectedBanner = findBanner(instance, TvdbAPI.ImageType.Poster, true);
            if (selectedBanner == null)
                return false;

            try
            {
                return instance.GetImage(TvdbAPI.ImageType.Poster, selectedBanner.Thumbnail, 0, fileName);
            }
            catch (WebException e)
            {
                HttpWebResponse response = e.Response as HttpWebResponse;
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                    return false;
                throw e;
            }   
        }

        /// <summary>
        /// Get the fan art image.
        /// </summary>
        /// <param name="instance">An API instance.</param>
        /// <param name="fileName">The output filename.</param>
        /// <returns>True if the image was downloaded; false otherwise.</returns>
        public bool GetFanArt(TvdbAPI instance, string fileName)
        {
            TvdbArtwork selectedBanner = findBanner(instance, TvdbAPI.ImageType.FanArt, false);
            if (selectedBanner == null)
                return false;

            try
            {
                return instance.GetImage(TvdbAPI.ImageType.FanArt, selectedBanner.Image, 0, fileName);
            }
            catch (WebException e)
            {
                HttpWebResponse response = e.Response as HttpWebResponse;
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                    return false;
                throw e;
            }   
        }

        /// <summary>
        /// Get the small fan art image.
        /// </summary>
        /// <param name="instance">An API instance.</param>
        /// <param name="fileName">The output filename.</param>
        /// <returns>True if the image was downloaded; false otherwise.</returns>
        public bool GetSmallFanArt(TvdbAPI instance, string fileName)
        {
            TvdbArtwork selectedBanner = findBanner(instance, TvdbAPI.ImageType.FanArt, true);
            if (selectedBanner == null)
                return false;

            try
            {
                return instance.GetImage(TvdbAPI.ImageType.FanArt, selectedBanner.Thumbnail, 0, fileName);
            }
            catch (WebException e)
            {
                HttpWebResponse response = e.Response as HttpWebResponse;
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                    return false;
                throw e;
            }   
        }

        private TvdbArtwork findBanner(TvdbAPI instance, TvdbAPI.ImageType imageType, bool thumbnail)
        {
            if (Artworks == null || Artworks.Count == 0)
                return null;

            TvdbArtwork selectedBanner = null;

            foreach (TvdbArtwork banner in Artworks)
            {
                if (banner.IsType(TvdbAPI.Level.Series, imageType))
                {
                    if ((!thumbnail && banner.Image != null) || (thumbnail && banner.Thumbnail != null))
                    {
                        if (selectedBanner == null)
                            selectedBanner = banner;
                        else
                        {
                            if (selectedBanner.Score == null)
                                selectedBanner = banner;
                            else
                            {
                                if (banner.Score != null)                                    
                                {
                                    if (banner.Score > selectedBanner.Score)
                                        selectedBanner = banner;
                                }
                            }
                        }
                    }
                }
            }

            return selectedBanner;
        }

        /// <summary>
        /// Get the translation overview.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The overview or null if none exists.</returns>
        public string GetOverview(string languageCode)
        {
            if (Translations != null)
                return TvdbTranslation.FindTranslation(Translations.OverviewTranslations, languageCode);
            else
                return null;
        }
    }
}
