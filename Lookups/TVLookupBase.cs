using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using DomainObjects;

namespace Lookups
{
    internal abstract class TVLookupBase
    {
        internal abstract int LookupTotals { get; }
        
        internal bool Initialized { get; set; }
        internal Collection<string> UnusedPosters { get; set; }
        
        private TVLookupBase() { }

        internal TVLookupBase(Logger streamLogger, string logHeader) { }

        internal abstract void CreateTVDatabase();
        internal abstract LookupController.LookupReply Process(EPGEntry epgEntry);
        internal abstract void LogStats();
    }
}
