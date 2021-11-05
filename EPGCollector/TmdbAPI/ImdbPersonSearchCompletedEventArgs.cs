using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheMovieDB
{
    public class ImdbPersonSearchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {
        private TmdbPerson[] people;
        public TmdbPerson[] People
        {
            get
            {
                RaiseExceptionIfNecessary();
                return people;
            }
        }

        public ImdbPersonSearchCompletedEventArgs(
        TmdbPerson[] people,
        Exception e,
        bool canceled,
        object state)
            : base(e, canceled, state)
        {
            this.people = people;
        }
    }
}
