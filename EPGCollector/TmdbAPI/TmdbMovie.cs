using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlType("movie")]
    public class TmdbMovie
    {
        /*[XmlElement("score")]*/
        public decimal Score{ get; set; }

        [XmlElement("popularity")]
        public decimal Popularity { get; set; }

        [XmlElement("translated")]
        public bool Translated { get; set; }

        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("alternative_name")]
        public string AlternativeName { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("imdb_id")]
        public string ImdbId { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("rating")]
        public decimal Rating { get; set; }

        [XmlElement("certification")]
        public string Certification { get; set; }

        [XmlElement("overview")]
        public string Overview { get; set; }        

        [XmlElement("released")]
        public string ReleasedString 
        {
            get
            {
                return Released.HasValue ? Released.Value.ToShortDateString() : "";
            }
            set
            {
                DateTime d;
                if (DateTime.TryParse(value, out d))
                    Released = d;
                else
                    Released = null;
            }
        }

        public DateTime? Released
        {
            set;
            get;
        }

        [XmlElement("runtime")]
        public string MovieRuntime
        {
            get
            {
                return Runtime.TotalMinutes.ToString();
            }
            set
            {
                int minutes;
                Runtime = TimeSpan.FromMinutes(int.TryParse(value,out minutes) ? minutes : 0);
            }
        }

        public TimeSpan Runtime { get; set; }

        [XmlElement("budget")]
        public string Budget { get; set; }

        [XmlElement("revenue")]
        public string Revenue { get; set; }

        [XmlElement("homepage")]
        public string Homepage { get; set; }

        [XmlElement("trailer")]
        public string Trailer { get; set; }

        [XmlArrayAttribute("categories")]
        public TmdbCategory[] Categories { get; set; }

        [XmlArrayAttribute("studios")]
        public TmdbStudio[] Studios { get; set; }

        [XmlArrayAttribute("countries")]
        public TmdbCountry[] Countries { get; set; }

        [XmlArrayAttribute("images")]
        public TmdbImage[] Images { get; set; }

        [XmlArrayAttribute("cast")]
        public TmdbCastPerson[] Cast { get; set; }

        [XmlElement("last_modified_at")]
        public string LastModifiedString
        {
            get
            {
                return LastModified.HasValue ? LastModified.Value.ToShortDateString() : "";
            }
            set
            {
                DateTime d;
                if (DateTime.TryParse(value, out d))
                    LastModified = d;
                else
                    LastModified = null;
            }
        }

        public DateTime? LastModified
        {
            set;
            get;
        }
    }
}
