using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlType("also_known_as")]
    public class TmdbAlsoKnownAs
    {
        [XmlElementAttribute("name")]
        public string[] Names { get; set; }
    }
}
