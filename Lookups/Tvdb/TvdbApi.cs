////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2016 nzsjb                                             //
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
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Threading;
using System.Reflection;

using Newtonsoft.Json;

using DomainObjects;

namespace Lookups.Tvdb
{
    /// <summary>
    /// The class that contains the TVDB low level calls.
    /// </summary>
    public class TvdbAPI
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
        /// Get the current default language code.
        /// </summary>
        /// 
        public string DefaultLanguageCode { get { return defaultLanguageCode; } }

        /// <summary>
        /// The web response keys.
        /// </summary>
        public StringDictionary ResponseKeys { get; private set; }
        
        /// <summary>
        /// Get the minimum web access time.
        /// </summary>
        public int MinimumAccessTime { get; private set; }
        /// <summary>
        /// Get the total number of requests.
        /// </summary>
        public int TotalRequestCount { get; private set; }
        /// <summary>
        /// Get the total web request time.
        /// </summary>
        public TimeSpan? TotalRequestTime { get; private set; }
        /// <summary>
        /// Get the total number of delayed web requests.
        /// </summary>
        public int TotalDelays { get; private set; }
        /// <summary>
        /// Get the total web request delay time.
        /// </summary>
        public int TotalDelayTime { get; private set; }
        /// <summary>
        /// Get the total time between requests.
        /// </summary>
        public TimeSpan? TotalTimeBetweenRequests { get; private set; }
        /// <summary>
        /// Get the minimum time between web requests.
        /// </summary>
        public TimeSpan? MinimumTimeBetweenRequests { get; private set; }
        /// <summary>
        /// Get the maximum time between requests.
        /// </summary>
        public TimeSpan? MaximumTimeBetweenRequests { get; private set; }

        /// <summary>
        /// The level of the query.
        /// </summary>
        public enum Level
        {
            /// <summary>
            /// Query is for series.
            /// </summary>
            Series,
            /// <summary>
            /// Query is for season.
            /// </summary>
            Season,
            /// <summary>
            /// Query is for episode.
            /// </summary>
            Episode
        }

        /// <summary>
        /// The image type.
        /// </summary>
        public enum ImageType
        {
            /// <summary>
            /// Image type is banner.
            /// </summary>
            Banner,
            /// <summary>
            /// Image type is poster.
            /// </summary>
            Poster,
            /// <summary>
            /// Image type is fan art.
            /// </summary>
            FanArt,
            /// <summary>
            /// Image type is actor.
            /// </summary>
            Actor,
            /// <summary>
            /// Image type is small poster.
            /// </summary>
            SmallPoster,
            /// <summary>
            /// Image type is small fan art.
            /// </summary>
            SmallFanArt,
            /// <summary>
            /// Image type is small actor.
            /// </summary>
            SmallActor,
            /// <summary>
            /// Image type is season.
            /// </summary>
            Season,
            /// <summary>
            /// Image type is small season.
            /// </summary>
            SmallSeason
        }

        /// <summary>
        /// Get or set the result of initializing.
        /// </summary>
        public string InitializeResult { get; set; }

        private const string baseEndPoint = "https://api4.thetvdb.com/v4";
        private const string imageEndPoint = "https://artworks.thetvdb.com";

        private const string loginEndPoint = baseEndPoint + "/login";
        private const string getArtworkTypesUrl = baseEndPoint + "/artwork/types";

        private const string getSeriesUrl = baseEndPoint + "/search?q={0}&type=series";
        private const string getSeriesDetailsUrl = baseEndPoint + "/series/{0}/extended?meta=episodes";
        private const string getEpisodeDetailsUrl = baseEndPoint + "/episodes/{0}/extended?meta=translations";

        /// <summary>
        /// Get or set the flag that determines if responses are logged or not.
        /// </summary>
        public bool LogResponse { get; set; }

        private string apiKey;
        private string pin;
        private string token;
        private string defaultLanguageCode = "en";
                
        private Logger logger;
        private string logHeader;

        private DateTime? lastAccessTime;

        private TvdbAPI() { }

        /// <summary>
        /// Initialize a new instance of the TvdbAPI class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="pin">The users PIN.</param>
        /// <param name="logger">The logger instance to use.</param>
        /// <param name="logHeader">The prefix for log messages.</param>
        public TvdbAPI(string apiKey, string pin, Logger logger, string logHeader)
        {
            LogResponse = true;

            this.apiKey = apiKey;
            this.pin = pin;
            this.logger = logger;
            this.logHeader = logHeader;
            
            ReplyBase reply = login(apiKey, pin);
            if (reply.Message != null)
                logMessage("Failed to get token - " + reply.Message);
            else
            {
                token = reply.ResponseData as string;
                logger.Write("Token is " + token);
            }

            TvdbArtworkType.Types = getArtworkTypes().ArtworkTypes;            

            MinimumAccessTime = 260;
            TotalRequestTime = new TimeSpan();
            TotalTimeBetweenRequests = new TimeSpan();
        }

        /// <summary>
        /// Initialize a new instance of the TvdbAPI class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="pin">The users PIN.</param>
        /// <param name="defaultLanguageCode">The default language code.</param>
        /// <param name="logger">The logger instance to use.</param>
        /// <param name="logHeader">The prefix for log messages.</param>
        public TvdbAPI(string apiKey, string pin, string defaultLanguageCode, Logger logger, string logHeader) : this(apiKey, pin, logger, logHeader)
        {
            this.defaultLanguageCode = defaultLanguageCode;
        }

        private ReplyBase login(string apiKey, string pin)
        {
            WebRequestSpec webRequestSpec = new WebRequestSpec(loginEndPoint, logger, logHeader);
            webRequestSpec.ContentType = "application/json";
            webRequestSpec.Method = "POST";

            using (StreamWriter streamWriter = new StreamWriter(webRequestSpec.RequestStream))
            {
                string json = 
                    "{" +
                        "\"apikey\":\"" + apiKey + "\"," +
                        "\"pin\":\"" + pin + "\"" +
                    "}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebReply reply = webRequestSpec.Process();
            if (reply.Message != null)
                return reply;

            string responseString = reply.ResponseData as string;
            logResponse("Login", responseString);

            TvdbLoginResult result = JsonConvert.DeserializeObject<TvdbLoginResult>(responseString);
            if (result.Status.ToLowerInvariant() != "success")
                return null;
            else
                return ReplyBase.DataReply(result.Login.Token);            
        }

        private TvdbArtworkTypesResult getArtworkTypes()
        {
            initializeFunction();

            string responseString = getData(getArtworkTypesUrl, null);
            logResponse("Artwork Types", responseString);

            return JsonConvert.DeserializeObject<TvdbArtworkTypesResult>(responseString);
        }

        /// <summary>
        /// Search for the basic series data. 
        /// </summary>
        /// <param name="title">Part or all of the title.</param>
        /// <returns>The results object.</returns>
        public TvdbSeriesSearchResult GetSeries(string title)
        {
            return GetSeries(title, defaultLanguageCode);
        }

        /// <summary>
        /// Search for the basic series data for a language code. 
        /// </summary>
        /// <param name="title">Part or all of the title.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The results object.</returns>
        public TvdbSeriesSearchResult GetSeries(string title, string languageCode)
        {
            initializeFunction();

            string url = string.Format(getSeriesUrl, escapeQueryString(title));
            string responseString = getData(url, languageCode);
            logResponse("Series Search", responseString);

            return JsonConvert.DeserializeObject<TvdbSeriesSearchResult>(responseString);
        }

        /// <summary>
        /// Get the details of a series.
        /// </summary>
        /// <param name="identity">The series identity.</param>
        /// <returns>The results object.</returns>
        public TvdbSeriesInfoResult GetSeriesDetails(string identity)
        {
            return GetSeriesDetails(identity, defaultLanguageCode);
        }

        /// <summary>
        /// Get the details of a series for a language code.
        /// </summary>
        /// <param name="identity">The series identity.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The results object.</returns>
        public TvdbSeriesInfoResult GetSeriesDetails(string identity, string languageCode)
        {
            initializeFunction();

            string url = string.Format(getSeriesDetailsUrl, identity);
            string responseString = getData(url, languageCode);
            logResponse("Series Detail", responseString);

            return JsonConvert.DeserializeObject<TvdbSeriesInfoResult>(responseString);
        }

        /// <summary>
        /// Get the details of an episode.
        /// </summary>
        /// <param name="episodeIdentity">The episode identity.</param>
        /// <returns>The results object.</returns>
        public TvdbEpisodeInfoResult GetEpisodeDetails(int episodeIdentity)
        {
            return GetEpisodeDetails(episodeIdentity, defaultLanguageCode);
        }

        /// <summary>
        /// Get the details of an episode for a language code.
        /// </summary>
        /// <param name="episodeIdentity">The episode identity.</param>
        /// <param name="languageCode">The language code.</param>
        /// <returns>The results object.</returns>
        public TvdbEpisodeInfoResult GetEpisodeDetails(int episodeIdentity, string languageCode)
        {
            initializeFunction();

            string url = string.Format(getEpisodeDetailsUrl, episodeIdentity);
            string responseString = getData(url, languageCode);
            logResponse("Episode Detail", responseString);

            return JsonConvert.DeserializeObject<TvdbEpisodeInfoResult>(responseString);
        }

        /// <summary>
        /// Download an image.
        /// </summary>
        /// <param name="imageType">The type of image.</param>
        /// <param name="filePath">The filename of the image.</param>
        /// <param name="size">The size of the image. Currently not used.</param>
        /// <param name="outputPath">The output path for the downloaded image.</param>
        /// <returns>True if the image is downloaded; false otherwise.</returns>
        public bool GetImage(ImageType imageType, string filePath, int size, string outputPath)
        {
            try
            {
                checkRequestRate();

                getImage(filePath, outputPath);

                TotalRequestCount++;
                TotalRequestTime += DateTime.Now - lastAccessTime.Value;
            }
            catch (WebException)
            {
                throw;
            }

            return true;
        }

        private void initializeFunction()
        {
            if (apiKey == null)
                throw new InvalidOperationException("The API key has not been set");
            if (token == null)
                throw new InvalidOperationException("The token has not been set");
        }

        private string escapeQueryString(string inputString)
        {
            return Uri.EscapeDataString(inputString);
        }

        private string getData(string url, string languageCode)
        {
            WebRequestSpec webRequestSpec = getWebRequest(url, languageCode);
            
            try
            {
                checkRequestRate();

                ReplyBase reply = webRequestSpec.Process();
                if (reply.Message != null)
                    throw new Exception(reply.Message);

                string response = reply.ResponseData as string;

                TotalRequestCount++;
                TotalRequestTime += DateTime.Now - lastAccessTime.Value;

                return response;
            }
            catch (WebException e)
            {
                if (e.Message.Contains("404"))
                    return (null);

                throw;
            }
        }

        private bool getImage(string url, string outputPath)
        {
            WebRequestSpec webRequestSpec = getWebRequest(url.StartsWith("http") ? url : imageEndPoint + url, null);

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

            TotalRequestCount++;
            TotalRequestTime += DateTime.Now - lastAccessTime.Value;

            return true;
        }

        private WebRequestSpec getWebRequest(string url, string languageCode)
        {
            WebRequestSpec webRequestSpec = new WebRequestSpec(url, logger, logHeader);
            webRequestSpec.SetHeader("Authorization", "Bearer " + token);
            if (languageCode != null)
                webRequestSpec.SetHeader("Accept-Language", languageCode);
            
            return webRequestSpec;
        }

        private void logResponse(string heading, string jsonString)
        {
            if (!LogResponse)
                return;

            logMessage(heading + " " + jsonString);            
        
            try
            {
                dynamic jsonData = JsonConvert.DeserializeObject<object>(jsonString);
                logMessage("JSON structure for " + heading + ": " + jsonData.ToString());
            }
            catch (Exception e)
            {
                logMessage("Failed to deserialize JSON text for " + heading);
                logMessage(e.Message);
            }
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
                    TotalDelayTime += waitTime;

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
