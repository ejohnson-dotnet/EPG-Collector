using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlType("country")]
    public class TmdbCountry
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("code")]
        public string Code { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}
