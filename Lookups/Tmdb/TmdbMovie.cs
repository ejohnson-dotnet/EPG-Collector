////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2020 nzsjb                                             //
//                                                                              //
//  This Program is free software; you can redistribute it and/or modify        //
//  it under the terms of the GNU General Public License as published by        //
//  the Free Software Foundation; either version 2, or (at your option)         //
//  any later version.                                                          //
//                                                                              //
//  This Program is distributed in the hope that it will be useful,             //
//  but WITHOUT ANY WARRANTY; without even the implied warranty of              //
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                //
//  GNU General Public License for more details.                                //
//                                                                              //
//  You should have received a copy of the GNU General Public License           //
//  along with GNU Make; see the file COPYING.  If not, write to                //
//  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA.       //
//  http://www.gnu.org/copyleft/gpl.html                                        //
//                                                                              //  
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Lookups.Tmdb
{
    /// <summary>
    /// The class that describes a movie.
    /// </summary>
    [DataContract]
    public class TmdbMovie
    {  
        /// <summary>
        /// Get or set the backdrop path.
        /// </summary>
        [DataMember(Name = "backdrop_path")]
        public string BackdropPath { get; set; }        
        
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        [DataMember(Name = "id")]
        public int Identity { get; set; }
        
        /// <summary>
        /// Get or set the original title.
        /// </summary>
        [DataMember(Name = "original_title")]
        public string OriginalTitle { get; set; }
        
        /// <summary>
        /// Get or set the popularity rating.
        /// </summary>
        [DataMember(Name = "popularity")]
        public decimal Popularity { get; set; }
        
        /// <summary>
        /// Get or set the poster path.
        /// </summary>
        [DataMember(Name = "poster_path")]
        public string PosterPath { get; set; }        
        
        /// <summary>
        /// Get or set the release date as a string.
        /// </summary>
        [DataMember(Name = "release_date")]
        public string ReleaseDateString 
        {
            get { return releaseDateString; }
            set { releaseDateString = value; }
        }
        
        /// <summary>
        /// Get or set the title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Get or set the adult flag.
        /// </summary>
        [DataMember(Name = "adult")]
        public bool Adult { get; set; }
        
        /// <summary>
        /// Get or set the collection the movie belongs to.
        /// </summary>
        [DataMember(Name = "belongs_to_collection")]
        public TmdbCollectionInfo BelongsToCollection { get; set; }
        
        /// <summary>
        /// Get or set the budget.
        /// </summary>
        [DataMember(Name = "budget")]
        public int Budget { get; set; }
        
        /// <summary>
        /// Get or set the gnere list.
        /// </summary>
        [DataMember(Name = "genres")]
        public TmdbGenre[] Genres { get; set; }
        
        /// <summary>
        /// Get or set the home page address.
        /// </summary>
        [DataMember(Name = "homepage")]
        public string HomePage { get; set; }
        
        /// <summary>
        /// Get or set the IMDB identity.
        /// </summary>
        [DataMember(Name = "imdb_id")]
        public string ImdbIdentity { get; set; }
        
        /// <summary>
        /// Get or set the overview.
        /// </summary>
        [DataMember(Name = "overview")]
        public string Overview { get; set; }
        
        /// <summary>
        /// Get or set the list of production companies.
        /// </summary>
        [DataMember(Name = "production_companies")]
        public TmdbProductionCompany[] ProductionCompanies { get; set; }
        
        /// <summary>
        /// Get or set the list of production countries.
        /// </summary>
        [DataMember(Name = "production_countries")]
        public TmdbProductionCountry[] ProductionCountries { get; set; }
        
        /// <summary>
        /// Get or set the revenue.
        /// </summary>
        [DataMember(Name = "revenue")]
        public long Revenue { get; set; }
        
        /// <summary>
        /// Get or set the running time.
        /// </summary>
        [DataMember(Name = "runtime")]
        public string RuntimeString 
        {
            get { return runtimeString; }
            set { runtimeString = value; } 
        }
        
        /// <summary>
        /// Get or set the languages.
        /// </summary>
        [DataMember(Name = "spoken_languages")]
        public TmdbSpokenLanguage[] SpokenLanguages { get; set; }
        
        /// <summary>
        /// Get or set the tag line.
        /// </summary>
        [DataMember(Name = "tagline")]
        public string TagLine { get; set; }
        
        /// <summary>
        /// Get or set the average vote.
        /// </summary>
        [DataMember(Name = "vote_average")]
        public decimal VoteAverage { get; set; }
        
        /// <summary>
        /// Get or set the number of votes.
        /// </summary>
        [DataMember(Name = "vote_count")]
        public int VoteCount { get; set; }        

        /// <summary>
        /// Get or set the list of cast members.
        /// </summary>
        public TmdbCast Cast { get; set; }

        /// <summary>
        /// Get or set the list of alternative titles.
        /// </summary>
        public TmdbAlternativeTitles AlternativeTitles { get; set; }
        
        /// <summary>
        /// Get or set the list of images.
        /// </summary>
        public TmdbMovieImages Images { get; set; }
        
        /// <summary>
        /// Get or set the list of keywords.
        /// </summary>
        public TmdbMovieKeywords Keywords { get; set; }
        
        /// <summary>
        /// Get or set the release information.
        /// </summary>
        public TmdbMovieReleaseInfo ReleaseInfo { get; set; }
       
        /// <summary>
        /// Get or set the list of trailers.
        /// </summary>
        public TmdbMovieTrailers Trailers { get; set; }
        
        /// <summary>
        /// Get or set the list of translations.
        /// </summary>
        public TmdbMovieTranslations Translations { get; set; }

        /// <summary>
        /// Get the string representation of the running time.
        /// </summary>
        public TimeSpan Runtime
        {
            get
            {
                if (runtimeString == null)
                    return new TimeSpan();
                else
                {
                    try
                    {
                        int totalMinutes = Int32.Parse(runtimeString);
                        int hours = totalMinutes / 60;
                        int minutes = totalMinutes % 60;
                        return new TimeSpan(hours, minutes, 0);
                    }
                    catch (FormatException)
                    {
                        return new TimeSpan();
                    }
                    catch (OverflowException)
                    {
                        return new TimeSpan();
                    }
                }
            }
        }

        /// <summary>
        /// Get the release date.
        /// </summary>
        public DateTime ReleaseDate
        {
            get
            {
                if (releaseDateString == null)
                    return new DateTime();
                else
                {
                    try
                    {
                        return DateTime.Parse(releaseDateString);
                    }
                    catch (FormatException)
                    {
                        return new DateTime();
                    }
                }
            }
        }

        /// <summary>
        /// Get the genres as a comma separated list.
        /// </summary>
        public string GenreString
        {
            get
            {
                if (Genres == null)
                    return string.Empty;

                StringBuilder reply = new StringBuilder();

                foreach (TmdbGenre genre in Genres)
                {
                    if (reply.Length != 0)
                        reply.Append(", ");

                    reply.Append(genre.Name);
                }

                return reply.ToString();
            }
        }

        /// <summary>
        /// Get the keywords as a comma separated list.
        /// </summary>
        public string KeywordString
        {
            get
            {
                if (Keywords == null)
                    return string.Empty;

                StringBuilder reply = new StringBuilder();

                foreach (TmdbMovieKeyword keyword in Keywords.Keywords)
                {
                    if (reply.Length != 0)
                        reply.Append(", ");

                    reply.Append(keyword.Keyword);
                }

                return reply.ToString();
            }
        }

        /// <summary>
        /// Get the list of production company names.
        /// </summary>
        public Collection<string> ProductionCompanyNames
        {
            get
            {
                if (ProductionCompanies == null)
                    return null;

                Collection<string> companyNames = new Collection<string>();

                foreach (TmdbProductionCompany productionCompany in ProductionCompanies)
                    companyNames.Add(productionCompany.Name);

                return companyNames;
            }
        }

        private string releaseDateString;
        private string runtimeString;

        /// <summary>
        /// Initialize a new instance of the TmdbMovie class.
        /// </summary>
        public TmdbMovie() { }

        /// <summary>
        /// Load this instance with all the data for a movie.
        /// </summary>
        public void LoadAllData(TmdbAPI instance)
        {
            LoadAdditionalData(instance);
            LoadLists(instance);
        }

        /// <summary>
        /// Load this instance with the extended information for the movie.
        /// </summary>       
        public void LoadAdditionalData(TmdbAPI instance)
        {
            TmdbMovie movie = instance.GetMovieInfo(Identity);

            Adult = movie.Adult;
            BelongsToCollection = movie.BelongsToCollection;
            Budget = movie.Budget;
            Genres = movie.Genres;
            HomePage = movie.HomePage;
            ImdbIdentity = movie.ImdbIdentity;
            Overview = movie.Overview;
            ProductionCompanies = movie.ProductionCompanies;
            ProductionCountries = movie.ProductionCountries;
            Revenue = movie.Revenue;
            RuntimeString = movie.RuntimeString;
            SpokenLanguages = movie.SpokenLanguages;
            TagLine = movie.TagLine;
            VoteAverage = movie.VoteAverage;
            VoteCount = movie.VoteCount;
        }

        /// <summary>
        /// Load this instance with the optional data for the movie.
        /// </summary>
        public void LoadLists(TmdbAPI instance)
        {
            Cast = instance.GetMovieCast(Identity);
            AlternativeTitles = instance.GetMovieAlternativeTitles(Identity);
            Images = instance.GetMovieImages(Identity);
            Keywords = instance.GetMovieKeywords(Identity);
            ReleaseInfo = instance.GetMovieReleaseInfo(Identity);
            Trailers = instance.GetMovieTrailers(Identity);

            try
            {
                Translations = instance.GetMovieTranslations(Identity);
            }
            catch (SerializationException) { }
        }

        /// <summary>
        /// Get the backdrop image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetBackdropImage(TmdbAPI instance, string fileName)
        {
            if (BackdropPath == null)
                return false;

            return instance.GetImage(ImageType.Backdrop, BackdropPath, -1, fileName);
        }

        /// <summary>
        /// Get the backdrop image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="index">The index of the image.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetBackdropImage(TmdbAPI instance, int index, string fileName)
        {
            if (Images == null || index < 0 || index >= Images.Backdrops.Length)
                throw new IndexOutOfRangeException("The image index is incorrect");

            return instance.GetImage(ImageType.Backdrop, Images.Backdrops[index].FilePath, -1, fileName);
        }

        /// <summary>
        /// Get the poster image.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="fileName">The output path.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetPosterImage(TmdbAPI instance, string fileName)
        {
            if (PosterPath == null)
                return false;

            return instance.GetImage(ImageType.Poster, PosterPath, -1, fileName);
        }

        /// <summary>
        /// Search for a movie title.
        /// </summary>
        /// <param name="instance">The API instance.</param>
        /// <param name="title">The movie title or part of it.</param>
        /// <returns>The results object.</returns>
        public static TmdbMovieSearchResults Search(TmdbAPI instance, string title)
        {
            return instance.GetMovies(title);
        }
    }
}
