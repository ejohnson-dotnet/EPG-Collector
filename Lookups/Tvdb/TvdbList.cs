using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a list.
    /// </summary>
    [DataContract]
    public class TvdbList
    {
        /// <summary>
        /// Get or set the list of aliases.
        /// </summary>
        [DataMember(Name = "aliases")]
        public Collection<TvdbAlias> Aliases { get; set; }

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Returns true if the list is official; false otherwise.
        /// </summary>
        [DataMember(Name = "isOfficial")]
        public bool IsOfficial { get; set; }

        /// <summary>
        /// Get or set the name of the list.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the list of name translations.
        /// </summary>
        [DataMember(Name = "nameTranslations")]
        public Collection<string> NameTranslations { get; set; }

        /// <summary>
        /// Get or set the overview.
        /// </summary>
        [DataMember(Name = "overview")]
        public string Overview { get; set; }

        /// <summary>
        /// Get or set the list of overview translations.
        /// </summary>
        [DataMember(Name = "overviewTranslations")]
        public Collection<string> OverviewTranslations { get; set; }

        /// <summary>
        /// Get or set the list of translations.
        /// </summary>
        [DataMember(Name = "translations")]
        public TvdbTranslations Translations { get; set; }

        /// <summary>
        /// Get or set the url.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbList class.
        /// </summary>
        public TvdbList() { }
    }
}
