using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a trailer.
    /// </summary>
    [DataContract]
    public class TvdbTrailer
    {
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the language.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Get or set the name of the trailer.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the url.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbTrailer class.
        /// </summary>
        public TvdbTrailer() { }
    }
}
