using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlType("image")]
    public class TmdbImage
    {
        [XmlAttribute("type")]
        public TmdbImageType Type { get; set; }

        [XmlAttribute("size")]
        public TmdbImageSize Size { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("width")]
        public int Width { get; set; }

        [XmlAttribute("height")]
        public int Height { get; set; }
    }
}
