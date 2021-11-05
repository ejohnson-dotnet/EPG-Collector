using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TheMovieDB
{
    [XmlRoot("OpenSearchDescription")]
    public class TmdbMovieSearchResults
    {
        [XmlArrayAttribute("movies")]
        public TmdbMovie[] Movies { get; set; }        
    }
}
