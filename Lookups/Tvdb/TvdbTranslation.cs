using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a translation.
    /// </summary>
    [DataContract]
    public class TvdbTranslation
    {
        /// <summary>
        /// Get or set the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Returns true if the translation is an alias; false otherwise.
        /// </summary>
        [DataMember(Name = "isAlias")]
        public bool? IsAlias { get; set; }

        /// <summary>
        /// Get or set the overview.
        /// </summary>
        [DataMember(Name = "overview")]
        public string Overview { get; set; }

        /// <summary>
        /// Get or set the language.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Returns true if it is the primary translation; false otherwise.
        /// </summary>
        [DataMember(Name = "isPrimary")]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Get or set the list of aliases.
        /// </summary>
        [DataMember(Name = "aliases")]
        public Collection<string> Aliases { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbTranslation class.
        /// </summary>
        public TvdbTranslation() { }

        /// <summary>
        /// Find a translation.
        /// </summary>
        /// <param name="translations">The list of translation to search.</param>
        /// <param name="languageCode">The language code to search for.</param>
        /// <returns>The translation or null if it cannot be located.</returns>
        public static string FindTranslation(Collection<TvdbTranslation> translations, string languageCode)
        {
            if (translations == null || translations.Count == 0)
                return null;

            foreach (TvdbTranslation translation in translations)
            {
                if (translation.Language == languageCode)
                    return translation.Overview;
            }

            return null;
        }
    }
}
