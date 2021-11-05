using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheMovieDB
{
    public class ImdbMovieSearchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {
        private TmdbMovie[] movies;
        public TmdbMovie[] Movies
        {
            get
            {
                RaiseExceptionIfNecessary();
                return movies;
            }
        }

        public ImdbMovieSearchCompletedEventArgs(
        TmdbMovie[] movies,
        Exception e,
        bool canceled,
        object state)
            : base(e, canceled, state)
        {
            this.movies = movies;
        }
    }
}
