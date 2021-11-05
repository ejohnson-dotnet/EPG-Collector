using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheMovieDB
{
    public class ImdbMovieInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {
        private TmdbMovie movie;
        public TmdbMovie Movie
        {
            get
            {
                RaiseExceptionIfNecessary();
                return movie;
            }
        }

        public ImdbMovieInfoCompletedEventArgs(
        TmdbMovie movie,
        Exception e,
        bool canceled,
        object state)
            : base(e, canceled, state)
        {
            this.movie = movie;
        }
    }
}
