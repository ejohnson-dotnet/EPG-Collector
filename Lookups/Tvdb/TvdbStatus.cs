using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes the status.
    /// </summary>
    [DataContract]
    public class TvdbStatus
    {
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Returns true if the status is to be kept updated; false otherwise.
        /// </summary>
        [DataMember(Name = "keepUpdated")]
        public bool KeepUpdated { get; set; }

        /// <summary>
        /// Get or set the name of the status.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the record type.
        /// </summary>
        [DataMember(Name = "recordType")]
        public string RecordType { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbStatus class.
        /// </summary>
        public TvdbStatus() { }
    }
}
