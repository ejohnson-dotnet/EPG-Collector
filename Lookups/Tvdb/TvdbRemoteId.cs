using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a remote ID.
    /// </summary>
    [DataContract]
    public class TvdbRemoteId
    {
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public string Identity { get; set; }

        /// <summary>
        /// Get or set the type.
        /// </summary>
        [DataMember(Name = "type")]
        public int Type { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbRemoteId class.
        /// </summary>
        public TvdbRemoteId() { }
    }
}
