using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a login response.
    /// </summary>
    [DataContract]
    public class TvdbLoginResult
    {
        /// <summary>
        /// Get or set the status.
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Get or set the login response data.
        /// </summary>
        [DataMember(Name = "data")]
        public TvdbLogin Login { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbLoginResult class.
        /// </summary>
        public TvdbLoginResult() { }
    }
}
