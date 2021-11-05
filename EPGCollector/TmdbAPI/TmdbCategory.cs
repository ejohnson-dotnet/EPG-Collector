using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlType("category")]
    public class TmdbCategory
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}
