using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a character.
    /// </summary>
    [DataContract]
    public class TvdbCharacter
    {
        /// <summary>
        /// Get or set the aliases.
        /// </summary>
        [DataMember(Name = "aliases")]
        public Collection<TvdbAlias> Aliases { get; set; }

        /// <summary>
        /// Get or set the episode identity.
        /// </summary>
        [DataMember(Name = "episodeId")]
        public int? EpisodeIdentity { get; set; }
        
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the image.
        /// </summary>
        [DataMember(Name = "image")]
        public string Image { get; set; }

        /// <summary>
        /// Returns true if the character is featured; false otherwise.
        /// </summary>
        [DataMember(Name = "isFeatured")]
        public bool IsFeatured { get; set; }

        /// <summary>
        /// Get or set the movie identity.
        /// </summary>
        [DataMember(Name = "movieId")]
        public int? MovieIdentity { get; set; }

        /// <summary>
        /// Get or setthe name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the name translations.
        /// </summary>
        [DataMember(Name = "nameTranslations")]
        public Collection<string> NameTranslations { get; set; }

        /// <summary>
        /// Get or set the overview translations.
        /// </summary>
        [DataMember(Name = "overviewTranslations")]
        public Collection<string> OverviewTranslations { get; set; }

        /// <summary>
        /// Get or set the person identity.
        /// </summary>
        [DataMember(Name = "peopleId")]
        public int? PeopleIdentity { get; set; }

        /// <summary>
        /// Get or set the person type.
        /// </summary>
        [DataMember(Name = "peopleType")]
        public string PeopleType { get; set; }

        /// <summary>
        /// Get or set the person name.
        /// </summary>
        [DataMember(Name = "personName")]
        public string PersonName { get; set; }

        /// <summary>
        /// Get or set the series identity.
        /// </summary>
        [DataMember(Name = "seriesId")]
        public int? SeriesIdentity { get; set; }

        /// <summary>
        /// Get or set the sort priority.
        /// </summary>
        [DataMember(Name = "sort")]
        public int? Sort { get; set; }

        /// <summary>
        /// Get or set the translations.
        /// </summary>
        [DataMember(Name = "translations")]
        public TvdbTranslations Translations { get; set; }

        /// <summary>
        /// Get or set the character type.
        /// </summary>
        [DataMember(Name = "type")]
        public int? Type { get; set; }

        /// <summary>
        /// Get or set the url.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbCharacter class.
        /// </summary>
        public TvdbCharacter() { }

        /// <summary>
        /// Get a list of character names.
        /// </summary>
        /// <param name="characters">The collection to be filled.</param>
        /// <param name="types">The types of characters required.</param>
        /// <returns>A collection of names or null if none can be located.</returns>
        public static Collection<string> GetCharacterNames(Collection<TvdbCharacter> characters, string[] types)
        {
            if (characters == null || characters.Count == 0)
                return (null);

            Collection<string> names = new Collection<string>();

            foreach (TvdbCharacter character in characters)
            {
                if (!string.IsNullOrWhiteSpace(character.PersonName) && !names.Contains(character.PersonName))
                {
                    foreach (string type in types)
                    {
                        if (type.ToLowerInvariant() == character.PeopleType.ToLowerInvariant())                   
                            names.Add(character.PersonName);
                    }
                }
            }

            return (names.Count != 0 ? names : null); 
        }
    }
}
