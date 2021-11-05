using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheMovieDB
{
    public class ImdbPersonInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {
        private TmdbPerson person;
        public TmdbPerson Person
        {
            get
            {
                RaiseExceptionIfNecessary();
                return person;
            }
        }

        public ImdbPersonInfoCompletedEventArgs(
        TmdbPerson person,
        Exception e,
        bool canceled,
        object state)
            : base(e, canceled, state)
        {
            this.person = person;
        }
    }
}
