using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a genre.
    /// </summary>
    [DataContract]
    public class TvdbGenre
    {
        /// <summary>
        /// Get or set the genre.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the name of the genre.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the slug.
        /// </summary>
        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbOverview class.
        /// </summary>
        public TvdbGenre() { }
    }
}
