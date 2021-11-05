using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes artwork.
    /// </summary>
    [DataContract]
    public class TvdbArtwork
    {
        /// <summary>
        /// Get or set the identity of the artwork.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the image address.
        /// </summary>
        [DataMember(Name = "image")]
        public string Image { get; set; }

        /// <summary>
        /// Get or set the language code.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Get or set the score.
        /// </summary>
        [DataMember(Name = "score")]
        public double? Score { get; set; }

        /// <summary>
        /// Get or set the thumbnail image address.
        /// </summary>
        [DataMember(Name = "thumbnail")]
        public string Thumbnail { get; set; }

        /// <summary>
        /// Get or set the type of artwork.
        /// </summary>
        [DataMember(Name = "type")]
        public int Type { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbArtwork class.
        /// </summary>
        public TvdbArtwork() { }

        /// <summary>
        /// Check if the artwork is a specified type.
        /// </summary>
        /// <param name="level">The level of the query.</param>
        /// <param name="imageType">The image type to check for.</param>
        /// <returns>Returns true if the artwork is of the specified type; false otherwise.</returns>
        public bool IsType(TvdbAPI.Level level, TvdbAPI.ImageType imageType)
        {
            if (Type == 0 || Type >= TvdbArtworkType.Types.Count)
                return false;

            TvdbArtworkType artworkType = TvdbArtworkType.Types[Type - 1];
            if (artworkType.RecordType.ToLowerInvariant() == "movie")
                return false;
            
            if (imageType == TvdbAPI.ImageType.Actor)
                return artworkType.RecordType.ToLowerInvariant() == "actor";

            switch (level)
            {
                case TvdbAPI.Level.Episode:
                    if (artworkType.RecordType.ToLowerInvariant() != "episode")
                        return false;
                    break;
                case TvdbAPI.Level.Season:
                    if (artworkType.RecordType.ToLowerInvariant() != "season")
                        return false;
                    break;
                case TvdbAPI.Level.Series:
                    if (artworkType.RecordType.ToLowerInvariant() != "series")
                        return false;
                    break;
                default:
                    return false;
            }

            switch (imageType)
            {
                case TvdbAPI.ImageType.Banner:
                    if (artworkType.Name.ToLowerInvariant() == "banner")
                        return true;
                    break;
                case TvdbAPI.ImageType.FanArt:
                case TvdbAPI.ImageType.Poster:
                case TvdbAPI.ImageType.SmallFanArt:
                case TvdbAPI.ImageType.SmallPoster:
                    if (artworkType.Name.ToLowerInvariant() == "poster")
                        return true;
                    break;
                case TvdbAPI.ImageType.Season:
                case TvdbAPI.ImageType.SmallSeason:
                    break;
                default:
                    break;
            }

            return false;
        }
    }
}
