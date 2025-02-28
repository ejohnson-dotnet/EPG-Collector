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
using System.Net;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Reflection;

using Newtonsoft.Json;

using DomainObjects;

namespace Lookups.Tmdb
{
    /// <summary>
    /// The class that contains the Tmdb low level calls.
    /// </summary>
    public class TmdbAPI
    {
        /// <summary>
        /// Get the full assembly version number.
        /// </summary>
        public static string AssemblyVersion
        {
            get
            {
                System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
                return (version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision);
            }
        }

        /// <summary>
        /// Get the minimum access time.
        /// </summary>
        public int MinimumAccessTime { get; private set; }
        /// <summary>
        /// Get the total number of requests.
        /// </summary>
        public int TotalRequestCount { get; private set; }
        /// <summary>
        /// Get the total request time.
        /// </summary>
        public TimeSpan? TotalRequestTime { get; private set; }
        /// <summary>
        /// Get the total number of delays.
        /// </summary>
        public int TotalDelays { get; private set; }
        /// <summary>
        /// Get the total delay time.
        /// </summary>
        public int TotalDelayTime { get; private set; }
        /// <summary>
        /// Get the total time between requests.
        /// </summary>
        public TimeSpan? TotalTimeBetweenRequests { get; private set; }
        /// <summary>
        /// Get the minimum time between requests.
        /// </summary>
        public TimeSpan? MinimumTimeBetweenRequests { get; private set; }
        /// <summary>
        /// Get the maximum time between requests.
        /// </summary>
        public TimeSpan? MaximumTimeBetweenRequests { get; private set; }

        /// <summary>
        /// Get the configuration record.
        /// </summary>
        public TmdbConfiguration Configuration
        {
            get
            {
                if (configuration == null)
                    configuration = getConfiguration();

                return configuration;
            }
        }

        /// <summary>
        /// Get the current default language code.
        /// </summary>
        /// 
        public string DefaultLanguageCode { get { return defaultLanguageCode; } }

        /// <summary>
        /// Get or set the flag that determines if responses are logged or not.
        /// </summary>
        public bool LogResponse { get; set; }

        private const string configurationUrl = "http://api.themoviedb.org/3/configuration?api_key={0}";

        private const string movieSearchUrl = "https://api.themoviedb.org/3/search/movie?api_key={0}&query={1}&page={2}&include_adult=true";
        private const string movieGetInfoUrl = "https://api.themoviedb.org/3/movie/{1}?api_key={0}";
        private const string movieGetCastUrl = "https://api.themoviedb.org/3/movie/{1}/casts?api_key={0}";
        private const string movieGetAlternativeTitlesUrl = "https://api.themoviedb.org/3/movie/{1}/alternative_titles?api_key={0}";
        private const string movieGetImagesUrl = "https://api.themoviedb.org/3/movie/{1}/images?api_key={0}";
        private const string movieGetKeywordsUrl = "https://api.themoviedb.org/3/movie/{1}/keywords?api_key={0}";
        private const string movieGetReleaseInfoUrl = "https://api.themoviedb.org/3/movie/{1}/releases?api_key={0}";
        private const string movieGetTrailersUrl = "https://api.themoviedb.org/3/movie/{1}/trailers?api_key={0}";
        private const string movieGetTranslationsUrl = "https://api.themoviedb.org/3/movie/{1}/translations?api_key={0}";

        private const string personSearchUrl = "https://api.themoviedb.org/3/search/person?api_key={0}&query={1}&page={2}&include_adult=true";
        private const string personGetInfoUrl = "https://api.themoviedb.org/3/person/{1}?api_key={0}";
        private const string personGetCreditsUrl = "https://api.themoviedb.org/3/person/{1}/credits?api_key={0}";
        private const string personGetImagesUrl = "https://api.themoviedb.org/3/person/{1}/images?api_key={0}";

        private const string collectionGetInfoUrl = "https://api.themoviedb.org/3/collection/{1}?api_key={0}";

        private const string companyGetInfoUrl = "https://api.themoviedb.org/3/company/{1}?api_key={0}";
        private const string companyMoviesUrl = "https://api.themoviedb.org/3/company/{1}/movies?api_key={0}&page={2}";

        private const string tvSearchUrl = "https://api.themoviedb.org/3/search/tv?api_key={0}&query={1}&page={2}&include_adult=true";
        private const string tvSearchWithCodeUrl = "https://api.themoviedb.org/3/search/tv?api_key={0}&query={1}&language={2}&page={3}&include_adult=true";
        private const string tvGetSeriesDetailUrl = "https://api.themoviedb.org/3/tv/{1}?api_key={0}";
        private const string tvGetSeriesCreditsUrl = "https://api.themoviedb.org/3/tv/{1}/credits?api_key={0}";
        private const string tvGetImagesUrl = "https://api.themoviedb.org/3/tv/{1}/images?api_key={0}";
        private const string tvGetSeasonDetailsUrl = "https://api.themoviedb.org/3/tv/{1}/season/{2}?api_key={0}";
        private const string tvGetEpisodeDetailsUrl = "https://api.themoviedb.org/3/tv/{1}/season/{2}/episode/{3}?api_key={0}";
        private const string tvGetEpisodeCreditsUrl = "https://api.themoviedb.org/3/tv/{1}/season/{2}/episode/{3}/credits?api_key={0}";

        private string apiKey;
        private string defaultLanguageCode = "en";

        private TmdbConfiguration configuration;
        private DateTime? lastAccessTime;

        private Logger logger;
        private string logHeader;

        private TmdbAPI() { }

        /// <summary>
        /// Initialize a new instance of the TmdbAPI class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="logHeader">The prefix for log messages.</param>
        public TmdbAPI(string apiKey, Logger logger, string logHeader) 
        {
            this.apiKey = apiKey;
            this.logger = logger;
            this.logHeader = logHeader;
            
            MinimumAccessTime = 260;
            TotalRequestTime = new TimeSpan();
            TotalTimeBetweenRequests = new TimeSpan();
        }

        /// <summary>
        /// Get all movies matching a search string..
        /// </summary>
        /// <param name="title">The title or partial title of the movie to search for.</param>
        /// <returns>The results object.</returns>
        public TmdbMovieSearchResults GetMovies(string title)
        {
            initializeFunction();

            Collection<TmdbMovie> movies = new Collection<TmdbMovie>();            

            int pageNumber = 0;
            TmdbMovieSearchResults pageResults = null;
            int totalPages = -1;

            do
            {
                pageNumber++;
                pageResults = GetMovies(title, pageNumber);

                foreach (TmdbMovie movie in pageResults.Movies)
                    movies.Add(movie);

                if (totalPages == -1)
                    totalPages = pageResults.TotalPages;
            }
            while (pageNumber < totalPages);

            TmdbMovieSearchResults returnResults = new TmdbMovieSearchResults();
            returnResults.Movies = movies;
            returnResults.TotalResults = movies.Count;
            returnResults.TotalPages = pageNumber;

            return returnResults;            
        }

        /// <summary>
        /// Get a page of movie titles given a search string.
        /// </summary>
        /// <param name="title">The title of the movie to search for.</param>
        /// <param name="page">The page number.</param>
        /// <returns>The results object.</returns>
        public TmdbMovieSearchResults GetMovies(string title, int page)
        {
            initializeFunction();

            string url = string.Format(movieSearchUrl, apiKey, escapeQueryString(title), page);
            string responseString = getData(url);
            logResponse("Movie Search", responseString);

            return JsonConvert.DeserializeObject<TmdbMovieSearchResults>(responseString); 
        }

        /// <summary>
        /// Retrieve specific information about a movie. Things like overview, release date, cast data, genre's, YouTube trailer link, etc...
        /// </summary>
        /// <param name="id">The ID of the TMDb movie you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbMovie GetMovieInfo(int id)
        {
            initializeFunction();

            string url = string.Format(movieGetInfoUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Movie Info", responseString);

            return JsonConvert.DeserializeObject<TmdbMovie>(responseString);  
        }

        /// <summary>
        /// Retrieve alternative titles for a movie.
        /// </summary>
        /// <param name="id">The ID of the TMDb movie you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbAlternativeTitles GetMovieAlternativeTitles(int id)
        {
            initializeFunction();

            string url = string.Format(movieGetAlternativeTitlesUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Alternative Titles", responseString);

            return JsonConvert.DeserializeObject<TmdbAlternativeTitles>(responseString); 
        }

        /// <summary>
        /// Retrieve cast information about a movie.
        /// </summary>
        /// <param name="id">The ID of the TMDb movie you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbCast GetMovieCast(int id)
        {
            initializeFunction();

            string url = string.Format(movieGetCastUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Movie Cast", responseString);

            return JsonConvert.DeserializeObject<TmdbCast>(responseString); 
        }

        /// <summary>
        /// Retrieve images for a movie.
        /// </summary>
        /// <param name="id">The ID of the TMDb movie you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbImages GetMovieImages(int id)
        {
            initializeFunction();

            string url = string.Format(movieGetImagesUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Movie Images", responseString);

            return JsonConvert.DeserializeObject<TmdbImages>(responseString); 
        }

        /// <summary>
        /// Retrieve keywords for a movie.
        /// </summary>
        /// <param name="id">The ID of the TMDb movie you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbMovieKeywords GetMovieKeywords(int id)
        {
            initializeFunction();

            string url = string.Format(movieGetKeywordsUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Movie Keywords", responseString);

            return JsonConvert.DeserializeObject<TmdbMovieKeywords>(responseString); 
        }

        /// <summary>
        /// Retrieve release info for a movie.
        /// </summary>
        /// <param name="id">The ID of the TMDb movie you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbMovieReleaseInfo GetMovieReleaseInfo(int id)
        {
            initializeFunction();

            string url = string.Format(movieGetReleaseInfoUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Movie Release Info", responseString);

            return JsonConvert.DeserializeObject<TmdbMovieReleaseInfo>(responseString); 
        }

        /// <summary>
        /// Retrieve trailers for a movie.
        /// </summary>
        /// <param name="id">The ID of the TMDb movie you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbMovieTrailers GetMovieTrailers(int id)
        {
            initializeFunction();

            string url = string.Format(movieGetTrailersUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Movie Trailers", responseString);

            return JsonConvert.DeserializeObject<TmdbMovieTrailers>(responseString); 
        }

        /// <summary>
        /// Retrieve translations for a movie.
        /// </summary>
        /// <param name="id">The ID of the TMDb movie you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbMovieTranslations GetMovieTranslations(int id)
        {
            initializeFunction();

            string url = string.Format(movieGetTranslationsUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Movie Translations", responseString);

            return JsonConvert.DeserializeObject<TmdbMovieTranslations>(responseString); 
        }

        /// <summary>
        /// Get all people matching a search string.
        /// </summary>
        /// <param name="name">The name or partial name of the person to search for.</param>
        /// <returns>The results object.</returns>
        public TmdbPersonSearchResults GetPeople(string name)
        {
            initializeFunction();

            Collection<TmdbPerson> people = new Collection<TmdbPerson>();

            int pageNumber = 0;
            TmdbPersonSearchResults pageResults = null;

            do
            {
                pageNumber++;
                pageResults = GetPeople(name, pageNumber);

                foreach (TmdbPerson person in pageResults.People)
                    people.Add(person);
            }
            while (pageNumber < pageResults.TotalPages);

            TmdbPersonSearchResults returnResults = new TmdbPersonSearchResults();
            returnResults.People = people;
            returnResults.TotalPages = pageNumber;

            return returnResults;
        }            

        /// <summary>
        /// Search for a person.
        /// </summary>
        /// <param name="name">The name of the person you are searching for.</param>
        /// <param name="page">The page number.</param>
        /// <returns>The results object.</returns>
        public TmdbPersonSearchResults GetPeople(string name, int page)
        {
            initializeFunction();

            string url = string.Format(personSearchUrl, apiKey, escapeQueryString(name), page);
            string responseString = getData(url);
            logResponse("People Search", responseString);

            return JsonConvert.DeserializeObject<TmdbPersonSearchResults>(responseString); 
        }

        /// <summary>
        /// Retrieve the full filmography.
        /// </summary>
        /// <param name="id">The ID of the person you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbPerson GetPersonInfo(int id)
        {
            initializeFunction();

            string url = string.Format(personGetInfoUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Person Info", responseString);

            return JsonConvert.DeserializeObject<TmdbPerson>(responseString); 
        }

        /// <summary>
        /// Retrieve the credits for a person.
        /// </summary>
        /// <param name="id">The ID of the TMDb person you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbPersonCredits GetPersonCredits(int id)
        {
            initializeFunction();

            string url = string.Format(personGetCreditsUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Person Credits", responseString);

            return JsonConvert.DeserializeObject<TmdbPersonCredits>(responseString);
        }

        /// <summary>
        /// Retrieve the images for a person.
        /// </summary>
        /// <param name="id">The ID of the person you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbPersonImages GetPersonImages(int id)
        {
            initializeFunction();

            string url = string.Format(personGetImagesUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Person Images", responseString);

            return JsonConvert.DeserializeObject<TmdbPersonImages>(responseString);
        }

        /// <summary>
        /// Get the information for a film collection.
        /// </summary>
        /// <param name="id">The ID of the collection.</param>
        /// <returns>A TmdbCollectionInfo instance.</returns>
        public TmdbCollectionInfo GetCollectionInfo(int id)
        {
            initializeFunction();

            string url = string.Format(collectionGetInfoUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Collection Info", responseString);

            return JsonConvert.DeserializeObject<TmdbCollectionInfo>(responseString);
        }

        /// <summary>
        /// Get the information for a company.
        /// </summary>
        /// <param name="id">The ID of the company.</param>
        /// <returns>A TmdbCompanyInfo instance.</returns>
        public TmdbCompanyInfo GetCompanyInfo(int id)
        {
            initializeFunction();

            string url = string.Format(companyGetInfoUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("Company Info", responseString);

            return JsonConvert.DeserializeObject<TmdbCompanyInfo>(responseString);
        }

        /// <summary>
        /// Get all movies for a company.
        /// </summary>
        /// <param name="identity">The identity of the company.</param>
        /// <returns></returns>
        public TmdbCompanyMovies CompanyMovieSearch(int identity)
        {
            initializeFunction();

            Collection<TmdbCompanyMovie> companyMovies = new Collection<TmdbCompanyMovie>();

            int pageNumber = 0;
            TmdbCompanyMovies pageResults = null;

            do
            {
                pageNumber++;
                pageResults = CompanyMovieSearch(identity, pageNumber);

                foreach (TmdbCompanyMovie movie in pageResults.Movies)
                    companyMovies.Add(movie);
            }
            while (companyMovies.Count < pageResults.Total_Results);

            TmdbCompanyMovies returnResults = new TmdbCompanyMovies();
            returnResults.Movies = companyMovies;

            return returnResults;            
        }

        /// <summary>
        /// Get a page of movie titles for a company.
        /// </summary>
        /// <param name="identity">The identity of the company to search for.</param>
        /// <param name="page">The page number.</param>
        /// <returns>The results object.</returns>
        public TmdbCompanyMovies CompanyMovieSearch(int identity, int page)
        {
            initializeFunction();

            string url = string.Format(companyMoviesUrl, apiKey, identity, page);
            string responseString = getData(url);
            logResponse("Company Movies", responseString);

            return JsonConvert.DeserializeObject<TmdbCompanyMovies>(responseString);
        }

        /// <summary>
        /// Get an image.
        /// </summary>
        /// <param name="imageType">The type of image.</param>
        /// <param name="filePath">The source path.</param>
        /// <param name="size">The size of the image.</param>
        /// <param name="outputPath">The output path.</param>
        /// <returns>True if the image is downloaded; flase otherwise.</returns>
        public bool GetImage(ImageType imageType, string filePath, int size, string outputPath)
        {
            string url;

            switch (imageType)
            {
                case ImageType.Backdrop:
                    if (size != -1)
                        url = Configuration.Images.BaseUrl + Configuration.Images.BackdropSizes[size];
                    else
                        url = Configuration.Images.BaseUrl + Configuration.Images.OriginalSize;
                    break;
                case ImageType.Poster:
                    if (size != -1)
                        url = Configuration.Images.BaseUrl + Configuration.Images.PosterSizes[size];
                    else
                        url = Configuration.Images.BaseUrl + Configuration.Images.OriginalSize;
                    break;
                case ImageType.Profile:
                    if (size != -1)
                        url = Configuration.Images.BaseUrl + Configuration.Images.ProfileSizes[size];
                    else
                        url = Configuration.Images.BaseUrl + Configuration.Images.OriginalSize;
                    break;
                case ImageType.Logo:
                    url = Configuration.Images.BaseUrl;
                    break;
                default:
                    return false;
                    
            }

            string editedFilePath = (filePath.StartsWith("/") ? filePath.Substring(1) : filePath);
            string address;
            
            if (imageType != ImageType.Logo)
                address = url + @"/" + editedFilePath + "?api_key=" + apiKey;
            else
                address = editedFilePath + "?api_key=" + apiKey;

            try
            {
                checkRequestRate();

                if (getImage(address, outputPath))
                {
                    TotalRequestCount++;
                    TotalRequestTime += DateTime.Now - lastAccessTime.Value;
                }
            }
            catch (WebException)
            {
                throw;
            }

            return true;
        }

        /// <summary>
        /// Get all TV series matching a search string..
        /// </summary>
        /// <param name="title">The title or partial title of the show to search for.</param>
        /// <param name="languageCode">The language code or null if not specified.</param>
        /// <returns>The results object.</returns>
        public TmdbSeriesSearchResults SearchForSeries(string title, string languageCode)
        {
            initializeFunction();

            TmdbSeriesSearchResults returnResults = new TmdbSeriesSearchResults();
            returnResults.Series = new Collection<TmdbSeries>();

            int pageNumber = 0;
            TmdbSeriesSearchResults pageResults = null;
            int totalPages = -1;

            do
            {
                pageNumber++;                
                pageResults = SearchForSeries(title, languageCode, pageNumber);

                foreach (TmdbSeries tvShow in pageResults.Series)
                    returnResults.Series.Add(tvShow);

                if (totalPages == -1)
                    totalPages = pageResults.TotalPages;
            }
            while (pageNumber < totalPages);
            
            returnResults.TotalPages = pageNumber;

            return returnResults;
        }

        /// <summary>
        /// Get a page of tv titles given a search string.
        /// </summary>
        /// <param name="title">The title of the movie to search for.</param>
        /// <param name="languageCode">The language code or null if not specified.</param>
        /// <param name="page">The page number.</param>
        /// <returns>The results object.</returns>
        public TmdbSeriesSearchResults SearchForSeries(string title, string languageCode, int page)
        {
            initializeFunction();

            string url;
            if (!string.IsNullOrWhiteSpace(languageCode))
                url = string.Format(tvSearchWithCodeUrl, apiKey, escapeQueryString(title), languageCode, page);
            else
                url = string.Format(tvSearchUrl, apiKey, escapeQueryString(title), page);

            string responseString = getData(url);
            logResponse("TV Search", responseString);

            return JsonConvert.DeserializeObject<TmdbSeriesSearchResults>(responseString);
        }

        /// <summary>
        /// Retrieve details for a series.
        /// </summary>
        /// <param name="id">The ID of the TMDb series you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbSeriesDetail GetSeriesDetail(int id)
        {
            initializeFunction();

            string url = string.Format(tvGetSeriesDetailUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("TV Series Detail", responseString);

            return JsonConvert.DeserializeObject<TmdbSeriesDetail>(responseString);
        }

        /// <summary>
        /// Retrieve cast information about a series.
        /// </summary>
        /// <param name="id">The ID of the TMDb series you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbCast GetSeriesCast(int id)
        {
            initializeFunction();

            string url = string.Format(tvGetSeriesCreditsUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("TV Cast", responseString);

            return JsonConvert.DeserializeObject<TmdbCast>(responseString);
        }

        /// <summary>
        /// Retrieve images for a series.
        /// </summary>
        /// <param name="id">The ID of the TMDb series you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbImages GetSeriesImages(int id)
        {
            initializeFunction();

            string url = string.Format(tvGetImagesUrl, apiKey, id);
            string responseString = getData(url);
            logResponse("TV Images", responseString);

            return JsonConvert.DeserializeObject<TmdbImages>(responseString);
        }

        /// <summary>
        /// Retrieve details of a season.
        /// </summary>
        /// <param name="seriesId">The ID of the TMDb series you are searching for.</param>
        /// <param name="seasonId">The ID of the TMDb seeason you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbSeason GetSeasonDetails(int seriesId, int seasonId)
        {
            initializeFunction();

            string url = string.Format(tvGetSeasonDetailsUrl, apiKey, seriesId, seasonId);
            string responseString = getData(url);
            logResponse("TV Season Details", responseString);

            return JsonConvert.DeserializeObject<TmdbSeason>(responseString);
        }

        /// <summary>
        /// Retrieve details of an episode.
        /// </summary>
        /// <param name="seriesId">The ID of the TMDb series you are searching for.</param>
        /// <param name="seasonId">The ID of the TMDb seeason you are searching for.</param>
        /// <param name="episodeId">The ID of the TMDb episode you are searching for.</param>
        /// <returns>The results object.</returns>
        public TmdbEpisode GetEpisodeDetails(int seriesId, int seasonId, int episodeId)
        {
            initializeFunction();

            string url = string.Format(tvGetEpisodeDetailsUrl, apiKey, seriesId, seasonId, episodeId);
            string responseString = getData(url);
            logResponse("TV Episode Details", responseString);

            return JsonConvert.DeserializeObject<TmdbEpisode>(responseString);
        }

        /// <summary>
        /// Retrieve cast information about an episode.
        /// </summary>
        /// <param name="seriesId">The series ID.</param>
        /// <param name="seasonNumber">The season number.</param>
        /// <param name="episodeId">The episode ID.</param>
        /// <returns>The results object.</returns>
        public TmdbCast GetEpisodeCast(int seriesId, int seasonNumber, int episodeId)
        {
            initializeFunction();

            string url = string.Format(tvGetEpisodeCreditsUrl, apiKey, seriesId, seasonNumber, episodeId);
            string responseString = getData(url);
            logResponse("TV Cast", responseString);

            return JsonConvert.DeserializeObject<TmdbCast>(responseString);
        }

        private void initializeFunction()
        {
            if (apiKey == null)
                throw new InvalidOperationException("The API key has not been set");
        }

        private TmdbConfiguration getConfiguration()
        {
            if (apiKey == null)
                throw new InvalidOperationException("The API key has not been set");

            string url = string.Format(configurationUrl, apiKey);
            string responseString = getData(url);

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TmdbConfiguration));
            MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(responseString));

            return serializer.ReadObject(stream) as TmdbConfiguration;
        }

        private string escapeQueryString(string inputString)
        {
            return Uri.EscapeDataString(inputString);
        }

        private string getData(string url)
        {
            WebRequestSpec webRequestSpec = getWebRequest(url);

            try
            {
                checkRequestRate();

                ReplyBase reply = webRequestSpec.Process();
                if (reply.Message != null)
                    throw new Exception(reply.Message);

                TotalRequestCount++;
                TotalRequestTime += DateTime.Now - lastAccessTime.Value;

                return reply.ResponseData as string;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("404"))
                    return (null);

                throw;
            }
            
        }

        private bool getImage(string url, string outputPath)
        {
            WebRequestSpec webRequestSpec = new WebRequestSpec(url, logger, logHeader);

            checkRequestRate();

            ReplyBase reply = webRequestSpec.Process();
            if (reply.Message != null)
            {
                logMessage("Failed to download image - " + reply.Message);
                return false;
            }

            byte[] response = reply.ResponseData as byte[];
            FileStream streamWriter = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
            streamWriter.Write(response, 0, response.Length);
            streamWriter.Close();

            return true;
        }

        private WebRequestSpec getWebRequest(string url)
        {
            WebRequestSpec webRequestSpec = new WebRequestSpec(url, logger, logHeader);
            webRequestSpec.Accept = "application/json";

            webRequestSpec.Timeout = 30000;
            
            return webRequestSpec;
        }

        private void logResponse(string heading, string responseString)
        {
            if (!LogResponse)
                return;

            logMessage(heading + " " + responseString);
        }

        private void checkRequestRate()
        {
            if (lastAccessTime != null)
            {
                TimeSpan gap = DateTime.Now - lastAccessTime.Value;

                TotalTimeBetweenRequests += gap;
                if (MinimumTimeBetweenRequests == null || gap < MinimumTimeBetweenRequests.Value)
                    MinimumTimeBetweenRequests = gap;
                if (MaximumTimeBetweenRequests == null || gap > MaximumTimeBetweenRequests.Value)
                    MaximumTimeBetweenRequests = gap;

                if (gap.TotalMilliseconds < MinimumAccessTime)
                {
                    int waitTime = (int)(MinimumAccessTime - gap.TotalMilliseconds);

                    TotalDelays++;
                    TotalDelayTime+= waitTime;

                    Thread.Sleep(waitTime);
                }
            }

            lastAccessTime = DateTime.Now;
        }

        private void logMessage(string message)
        {
            if (logger != null)
                logger.Write(logHeader + " " + message);
        }
    }
}
