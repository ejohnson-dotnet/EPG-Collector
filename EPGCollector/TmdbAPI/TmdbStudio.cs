using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlType("studio")]
    public class TmdbStudio
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }
    }
}
