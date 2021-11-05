using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlType("movie")]
    public class TmdbPersonFilm
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("job")]
        public string Job { get; set; }

        [XmlAttribute("department")]
        public string Department { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("character")]
        public string Character { get; set; }

        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
