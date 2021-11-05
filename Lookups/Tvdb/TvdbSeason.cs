using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a season.
    /// </summary>
    [DataContract]
    public class TvdbSeason
    {
        /// <summary>
        /// Get or set the abbreviation.
        /// </summary>
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// Get or set the country.
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the image address.
        /// </summary>
        [DataMember(Name = "image")]
        public string Image { get; set; }

        /// <summary>
        /// Get or set the image type.
        /// </summary>
        [DataMember(Name = "imageType")]
        public int? ImageType { get; set; }

        /// <summary>
        /// Get or set the name of the season.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the list of name translations.
        /// </summary>
        [DataMember(Name = "nameTranslations")]
        public Collection<string> NameTranslations { get; set; }

        /// <summary>
        /// Get or set the season number.
        /// </summary>
        [DataMember(Name = "number")]
        public int? Number { get; set; }

        /// <summary>
        /// Get or set the list of overview translations.
        /// </summary>
        [DataMember(Name = "overviewTranslations")]
        public Collection<string> OverviewTranslations { get; set; }

        /// <summary>
        /// Get or set the series identity.
        /// </summary>
        [DataMember(Name = "seriesId")]
        public int? SeriesIdentity { get; set; }

        /// <summary>
        /// Get or set the slug.
        /// </summary>       
        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Get or set the list of translations.
        /// </summary>
        [DataMember(Name = "translations")]
        public TvdbTranslations Translations { get; set; }

        /// <summary>
        /// Get or set the season type.
        /// </summary>
        [DataMember(Name = "type")]
        public TvdbType Type { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbSeason class.
        /// </summary>
        public TvdbSeason() { }
    }
}
