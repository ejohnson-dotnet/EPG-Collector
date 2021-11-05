using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that contains the Artwork Types query result.
    /// </summary>
    [DataContract]
    public class TvdbArtworkTypesResult
    {
        /// <summary>
        /// Get or set the artwork types.
        /// </summary>
        [DataMember(Name = "data")]
        public Collection<TvdbArtworkType> ArtworkTypes { get; set; }

        /// <summary>
        /// Get or set the query status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Intialize a new instance of the TvdbArtworkTypesResult class.
        /// </summary>
        public TvdbArtworkTypesResult() { }
    }
}
