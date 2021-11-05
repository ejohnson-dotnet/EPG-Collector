using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a tag option.
    /// </summary>
    [DataContract]
    public class TvdbTagOption
    {
        /// <summary>
        /// Get or set the help text.
        /// </summary>
        [DataMember(Name = "helpText")]
        public string HelpText { get; set; }

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the name of the option.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the tag.
        /// </summary>
        [DataMember(Name = "tag")]
        public int Tag { get; set; }

        /// <summary>
        /// Get or set the name of the tag.
        /// </summary>
        [DataMember(Name = "tagName")]
        public string TagName { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbTagOption class.
        /// </summary>
        public TvdbTagOption() { }
    }
}
