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
    /// The class that describes a series search.
    /// </summary>
    [DataContract]
    public class TvdbSeriesSearch
    {
        /// <summary>
        /// Get or set the aliases.
        /// </summary>
        [DataMember(Name = "aliases")]
        public Collection<string> Aliases { get; set; }

        /// <summary>
        /// Get or set the companies.
        /// </summary>
        [DataMember(Name = "companies")]
        public Collection<string> Companies { get; set; }

        /// <summary>
        /// Get or set the company type.
        /// </summary>
        [DataMember(Name = "companyType")]
        public string CompanyType { get; set; }

        /// <summary>
        /// Get or set the country.
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Get or set the director.
        /// </summary>
        [DataMember(Name = "director")]
        public string Director { get; set; }

        /// <summary>
        /// Get or set the extended title.
        /// </summary>
        [DataMember(Name = "extendedTitle")]
        public string ExtendedTitle { get; set; }

        /// <summary>
        /// Get or set the string of genres.
        /// </summary>
        [DataMember(Name = "genre")]
        public Collection<string> Genres { get; set; }

        /// <summary>
        /// Get or set the internal identity.
        /// </summary>
        [DataMember(Name = "id")]
        public string Identity { get; set; }

        /// <summary>
        /// Get or set the image URL.
        /// </summary>
        [DataMember(Name = "imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Get or set the series name.
        /// </summary>
        [DataMember(Name = "name")]
        public string SeriesName { get; set; }

        /// <summary>
        /// Get or set the series name translated.
        /// </summary>
        [DataMember(Name = "nameTranslated")]
        public string NameTranslated { get; set; }

        /// <summary>
        /// Get or set the official list.
        /// </summary>
        [DataMember(Name = "officialList")]
        public string OfficialList { get; set; }

        /// <summary>
        /// Get or set the overview.
        /// </summary>
        [DataMember(Name = "overview")]
        public string Overview { get; set; }

        /// <summary>
        /// Get or set the overview translated.
        /// </summary>
        [DataMember(Name = "overviewTranslated")]
        public Collection<string> OverviewTranslated { get; set; }

        /// <summary>
        /// Get or set the posters.
        /// </summary>
        [DataMember(Name = "posters")]
        public Collection<string> Posters { get; set; }

        /// <summary>
        /// Get or set the primary language.
        /// </summary>
        [DataMember(Name = "primaryLanguage")]
        public string PrimaryLanguage { get; set; }

        /// <summary>
        /// Get or set the primary type.
        /// </summary>
        [DataMember(Name = "primaryType")]
        public string PrimaryType { get; set; }

        /// <summary>
        /// Get or set the status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Get or set the translations with language.
        /// </summary>
        [DataMember(Name = "translationsWithLang")]
        public Collection<string> TranslationsWithLanguage { get; set; }

        /// <summary>
        /// Get or set the TVDB identity.
        /// </summary>
        [DataMember(Name = "tvdb_id")]
        public string TvdbIdentity { get; set; }

        /// <summary>
        /// Get or set the type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Get or set the year.
        /// </summary>
        [DataMember(Name = "year")]
        public string Year { get; set; }

        /// <summary>
        /// Get a comma separated string of genres.
        /// </summary>
        public string GenresDisplayString { get { return TvdbUtils.CollectionToString(Genres); } }

        /// <summary>
        /// Get the last error message.
        /// </summary>
        public string LastError { get; private set; }

        /// <summary>
        /// Get or set the series details.
        /// </summary>
        public TvdbSeries Series { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbSeries class.
        /// </summary>
        public TvdbSeriesSearch() { }

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
    }
}
