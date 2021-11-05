using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that describes a content rating.
    /// </summary>
    [DataContract]
    public class TvdbContentRating
    {
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }

        /// <summary>
        /// Get or set the name of the rating.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Get or set the name of the country.
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Get or set the description.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Get or set the content type.
        /// </summary>
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// Get or set the order.
        /// </summary>
        [DataMember(Name = "order")]
        public int Order { get; set; }

        /// <summary>
        /// Get or set the full name of the rating.
        /// </summary>
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }

        /// <summary>
        /// Initialize a new instance of the TvdbContentRating class.
        /// </summary>
        public TvdbContentRating() { }

        /// <summary>
        /// Get a list of the content rating descriptions.
        /// </summary>
        /// <param name="ratings"></param>
        /// <returns></returns>
        public static Collection<string> GetContentRatingDescriptions(Collection<TvdbContentRating> ratings)
        {
            if (ratings == null || ratings.Count == 0)
                return null;

            Collection<string> descriptions = new Collection<string>();

            foreach (TvdbContentRating rating in ratings)
                descriptions.Add(rating.Name);

            return descriptions;
        }
    }
}
