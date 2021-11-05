using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes air days.
    /// </summary>
    [DataContract]
    public class TvdbAirsDays
    {
        /// <summary>
        /// Get or set if Sunday is an air day.
        /// </summary>
        [DataMember(Name = "sunday")]
        public bool Sunday { get; set; }

        /// <summary>
        /// Get or set if Monday is an air day.
        /// </summary>
        [DataMember(Name = "monday")]
        public bool Monday { get; set; }

        /// <summary>
        /// Get or set if Tuesday is an air day.
        /// </summary>
        [DataMember(Name = "tuesday")]
        public bool Tuesday { get; set; }

        /// <summary>
        /// Get or set if Wednesday is an air day.
        /// </summary>
        [DataMember(Name = "wednesday")]
        public bool Wednesday { get; set; }

        /// <summary>
        /// Get or set if Thursday is an air day.
        /// </summary>
        [DataMember(Name = "thursday")]
        public bool Thursday { get; set; }

        /// <summary>
        /// Get or set if Friday is an air day.
        /// </summary>
        [DataMember(Name = "friday")]
        public bool Friday { get; set; }

        /// <summary>
        /// Get or set if Saturday is an air day.
        /// </summary>
        [DataMember(Name = "saturday")]
        public bool Saturday { get; set; }

        /// <summary>
        /// Intialize a new instance of the TvdbAirDays class.
        /// </summary>
        public TvdbAirsDays() { }
    }
}
