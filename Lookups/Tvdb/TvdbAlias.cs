using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes an alias.
    /// </summary>
    [DataContract]
    public class TvdbAlias
    {
        /// <summary>
        /// Get or set the language code.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbAlias class.
        /// </summary>
        public TvdbAlias() { }
    }
}
