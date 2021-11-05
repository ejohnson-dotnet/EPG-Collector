using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlRoot("OpenSearchDescription")]
    public class TmdbPersonSearchResults
    {
        [XmlArrayAttribute("people")]
        public TmdbPerson[] People { get; set; }
    }
}
