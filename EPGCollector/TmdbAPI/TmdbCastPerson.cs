using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlType("person")]
    public class TmdbCastPerson
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("job")]
        public string Job { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("character")]
        public string Character { get; set; }

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("thumb")]
        public string Thumb { get; set; }

        [XmlAttribute("department")]
        public string Department { get; set; }

    }
}
