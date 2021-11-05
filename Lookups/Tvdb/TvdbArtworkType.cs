using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes the artwork type.
    /// </summary>
    [DataContract]
    public class TvdbArtworkType
    {
        /// <summary>
        /// Get or set the height.
        /// </summary>
        [DataMember(Name = "height")]
        public int Height { get; set; }

        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the image format.
        /// </summary>
        [DataMember(Name = "imageFormat")]
        public string ImageFormat { get; set; }

        /// <summary>
        /// Get or set the name of the artwork.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the record type.
        /// </summary>
        [DataMember(Name = "recordType")]
        public string RecordType { get; set; }

        /// <summary>
        /// Get or set the slug.
        /// </summary>
        [DataMember(Name = "slug")]
        public string Slug { get; set; }

        /// <summary>
        /// Get or set the thumbnail height.
        /// </summary>
        [DataMember(Name = "thumbnailHeight")]
        public int? ThumbnailHeight { get; set; }

        /// <summary>
        /// Get or set the thumbnail width.
        /// </summary>
        [DataMember(Name = "thumbnailWidth")]
        public int? ThumbnailWidth { get; set; }

        /// <summary>
        /// Ge or set the list of artwork types.
        /// </summary>
        public static Collection<TvdbArtworkType> Types { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbArtworkType class.
        /// </summary>
        public TvdbArtworkType() { }
    }
}
