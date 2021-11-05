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
using System.Runtime.Serialization;
using System.Net;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes an episode.
    /// </summary>
    [DataContract]
    public class TvdbEpisode
    {
        /// <summary>
        /// Get or set the aired date.
        /// </summary>
        [DataMember(Name = "aired")]
        public string Aired { get; set; }

        /// <summary>
        /// Get or set the airs after season.
        /// </summary>
        [DataMember(Name = "airsAfterSeason")]
        public int? AirsAfterSeason { get; set; }

        /// <summary>
        /// Get or set the airs before episode.
        /// </summary>
        [DataMember(Name = "airsBeforeEpisode")]
        public int? AirsBeforeEpisode { get; set; }

        /// <summary>
        /// Get or set the airs before season.
        /// </summary>
        [DataMember(Name = "airsBeforeSeason")]
        public int? AirsBeforeSeason { get; set; }

        /// <summary>
        /// Get or set the awards.
        /// </summary>
        [DataMember(Name = "awards")]
        public Collection<TvdbAward> Awards { get; set; }

        /// <summary>
        /// Get or set the characters.
        /// </summary>
        [DataMember(Name = "characters")]
        public Collection<TvdbCharacter> Characters { get; set; }

        /// <summary>
        /// Get or set the content ratings.
        /// </summary>
        [DataMember(Name = "contentRatings")]
        public Collection<TvdbContentRating> ContentRatings { get; set; }

        /// <summary>
        /// Get or set the episode ID.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the image.
        /// </summary>
        [DataMember(Name = "image")]
        public string Image { get; set; }

        /// <summary>
        /// Get or set the image type.
        /// </summary>
        [DataMember(Name = "imageType")]
        public int? ImageType { get; set; }

        /// <summary>
        /// Get or set the movie flag.
        /// </summary>
        [DataMember(Name = "isMovie")]
        public bool? IsMovie { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the name translations.
        /// </summary>
        [DataMember(Name = "nameTranslations")]
        public Collection<string> NameTranslations { get; set; }

        /// <summary>
        /// Get or set the network.
        /// </summary>
        [DataMember(Name = "networks")]
        public Collection<TvdbNetwork> Network { get; set; }

        /// <summary>
        /// Get or set the episode number.
        /// </summary>
        [DataMember(Name = "number")]
        public int? Number { get; set; }

        /// <summary>
        /// Get or set the overview translations.
        /// </summary>
        [DataMember(Name = "overviewTranslations")]
        public Collection<string> OverviewTranslations { get; set; }

        /// <summary>
        /// Get or set the production code.
        /// </summary>
        [DataMember(Name = "productionCode")]
        public string ProductionCode { get; set; }

        /// <summary>
        /// Get or set the remote ID's.
        /// </summary>
        [DataMember(Name = "remoteIds")]
        public Collection<TvdbRemoteId> RemoteIds { get; set; }

        /// <summary>
        /// Get or set the runtime.
        /// </summary>
        [DataMember(Name = "runtime")]
        public int? Runtime { get; set; }

        /// <summary>
        /// Get or set the season number.
        /// </summary>
        [DataMember(Name = "seasonNumber")]
        public int? SeasonNumber { get; set; }

        /// <summary>
        /// Get or set the list of seasons.
        /// </summary>
        [DataMember(Name = "seasons")]
        public Collection<TvdbSeason> Seasons { get; set; }

        /// <summary>
        /// Get or set the series ID.
        /// </summary>
        [DataMember(Name = "seriesId")]
        public int? SeriesId { get; set; }

        /// <summary>
        /// Get or set the tag options.
        /// </summary>
        [DataMember(Name = "tagOptions")]
        public Collection<TvdbTagOption> TagOptions { get; set; }

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
        /// Get the first air date. Returns null if not present or invalid.
        /// </summary>
        public DateTime? FirstAiredDate { get { return TvdbUtils.StringToDate(Aired); } }

        /// <summary>
        /// Get a list of actors.
        /// </summary>
        public Collection<string> Actors { get { return TvdbCharacter.GetCharacterNames(Characters, new string[] { "actor", "guest star" }); } }
        
        /// <summary>
        /// Get a comma separated string of actors.
        /// </summary>
        public string ActorsDisplayString { get { return TvdbUtils.CollectionToString(TvdbCharacter.GetCharacterNames(Characters, new string[] {"actor", "guest star"} )); } }

        /// <summary>
        /// Get a list of directors.
        /// </summary>
        public Collection<string> Directors { get { return TvdbCharacter.GetCharacterNames(Characters, new string[] { "director" }); } }
        
        /// <summary>
        /// Get a comma separated string of actors.
        /// </summary>
        public string DirectorsDisplayString { get { return TvdbUtils.CollectionToString(TvdbCharacter.GetCharacterNames(Characters, new string[] {"director"} )); } }

        /// <summary>
        /// Get a list of writers.
        /// </summary>
        public Collection<string> Writers { get { return TvdbCharacter.GetCharacterNames(Characters, new string[] { "creator", "writer" }); } }
        
        /// <summary>
        /// Get a comma separated string of actors.
        /// </summary>
        public string WritersDisplayString { get { return TvdbUtils.CollectionToString(TvdbCharacter.GetCharacterNames(Characters, new string[] {"creator", "writer" })); } }

        /// <summary>
        /// Get a list of guest stars.
        /// </summary>
        public Collection<string> GuestStars { get { return TvdbCharacter.GetCharacterNames(Characters, new string[] { "guest star" }); } }

        /// <summary>
        /// Get a comma separated string of guest stars.
        /// </summary>
        public string GuestStarsDisplayString { get { return TvdbUtils.CollectionToString(TvdbCharacter.GetCharacterNames(Characters, new string[] { "guest star" })); } }
        
        /// <summary>
        /// Get a comma separated string of content ratings.
        /// </summary>
        public string ContentRatingsDisplayString { get { return TvdbUtils.CollectionToString(TvdbContentRating.GetContentRatingDescriptions(ContentRatings)); } }

        /// <summary>
        /// Initialize a new instance of the TvdbEpisode class.
        /// </summary>
        public TvdbEpisode() { }

        /// <summary>
        /// Get the episode image.
        /// </summary>
        /// <param name="instance">An API instance.</param>
        /// <param name="fileName">The output filename.</param>
        /// <returns>True if the images is downloaded; false otherwise.</returns>
        public bool GetImage(TvdbAPI instance, string fileName)
        {
            if (string.IsNullOrWhiteSpace(Image))
                return false;

            return instance.GetImage(TvdbAPI.ImageType.Poster, Image, 0, fileName);
        }

        /// <summary>
        /// Get the small episode image.
        /// </summary>
        /// <param name="instance">An API instance.</param>
        /// <param name="fileName">The output filename.</param>
        /// <returns>True if the images is downloaded; false otherwise.</returns>
        public bool GetSmallImage(TvdbAPI instance, string fileName)
        {
            if (string.IsNullOrWhiteSpace(Image))
                return false;

            return instance.GetImage(TvdbAPI.ImageType.SmallPoster, Image, 0, fileName);
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
