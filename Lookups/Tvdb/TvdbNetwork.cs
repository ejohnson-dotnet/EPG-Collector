using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a network.
    /// </summary>
    [DataContract]
    public class TvdbNetwork
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
        /// Get or set the name of the network.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the slug.
        /// </summary>
        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbNetwork class.
        /// </summary>
        public TvdbNetwork() { }
    }
}
