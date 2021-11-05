using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlType("person")]
    public class TmdbPerson
    {
        [XmlElement("score")]
        public decimal Score { get; set; }

        [XmlElement("popularity")]
        public decimal Popularity { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElementAttribute("also_known_as")]
        public TmdbAlsoKnownAs AlsoKnownAs { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("biography")]
        public string Biography { get; set; }

        [XmlElement("known_movies")]
        public int KnownMovies { get; set; }

        [XmlElement("birthday")]
        public string BirthdayString { get; set; }

        public DateTime? Birthday
        {
            get
            {
                DateTime d;
                if (string.IsNullOrEmpty(BirthdayString) || !DateTime.TryParse(BirthdayString, out d))
                    return null;
                else
                    return d;
            }
        }

        [XmlElement("birthplace")]
        public string Birthplace { get; set; }

        [XmlArrayAttribute("images")]
        public TmdbImage[] Images { get; set; }

        [XmlArrayAttribute("filmography")]
        public TmdbPersonFilm[] Filmography { get; set; }
    }
}
