using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a login response.
    /// </summary>
    [DataContract]
    public class TvdbLogin
    {
        /// <summary>
        /// Get or set the session token.
        /// </summary>
        [DataMember(Name = "token")]
        public string Token { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbLogin class.
        /// </summary>
        public TvdbLogin() { }
    }
}
