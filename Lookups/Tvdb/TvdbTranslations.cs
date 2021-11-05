using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that prvoides a container for translations.
    /// </summary>
    [DataContract]
    public class TvdbTranslations
    {
        /// <summary>
        /// Get or set the list of name translations.
        /// </summary>
        [DataMember(Name = "nameTranslations")]
        public Collection<TvdbTranslation> NameTranslations { get; set; }

        /// <summary>
        /// Get or set the list of overview translations.
        /// </summary>
        [DataMember(Name = "overviewTranslations")]
        public Collection<TvdbTranslation> OverviewTranslations { get; set; }        

        /// <summary>
        /// Initialize a new instance of the TvdbTranslations class.
        /// </summary>
        public TvdbTranslations() { }
    }
}
