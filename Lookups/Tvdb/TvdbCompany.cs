using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a company.
    /// </summary>
    [DataContract]
    public class TvdbCompany
    {
        /// <summary>
        /// Get or set the active date.
        /// </summary>
        [DataMember(Name = "activeDate")]
        public string ActiveDate { get; set; }

        /// <summary>
        /// Get or set the list of aliases.
        /// </summary>
        [DataMember(Name = "aliases")]
        public Collection<TvdbAlias> Aliases { get; set; }

        /// <summary>
        /// Get or set the name of the country.
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the inactive date.
        /// </summary>
        [DataMember(Name = "inactiveDate")]
        public string InactiveDate { get; set; }

        /// <summary>
        /// Get or set the name of the company.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the list of name translations.
        /// </summary>
        [DataMember(Name = "nameTranslations")]
        public Collection<string> NameTranslations { get; set; }

        /// <summary>
        /// Get or set the list of overview translations.
        /// </summary>
        [DataMember(Name = "overviewTranslations")]
        public Collection<string> OverviewTranslations { get; set; }

        /// <summary>
        /// Get or set the primary company type.
        /// </summary>
        [DataMember(Name = "primaryCompanyType")]
        public int? PrimaryCompanyType { get; set; }

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
        /// Initialize a new instance of the TvdbCompany class.
        /// </summary>
        public TvdbCompany() { }
    }
}
