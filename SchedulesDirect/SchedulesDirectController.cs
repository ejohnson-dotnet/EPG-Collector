////////////////////////////////////////////////////////////////////////////////// 
//                                                                              //
//      Copyright © 2005-2020 nzsjb                                           //
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
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.
Security.Cryptography;
using System.Reflection;

using DomainObjects;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SchedulesDirect
{
    /// <summary>
    /// The class that controls access to the Schedules Direct API.
    /// </summary>
    public class SchedulesDirectController
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
        /// Get the single instance of the SchdulesDirectController class.
        /// </summary>
        public static SchedulesDirectController Instance
        {
            get
            {
                if (instance == null)
                    instance = new SchedulesDirectController();

                return instance;
            }
        }

        /// <summary>
        /// Return true if the instance has been initialized; false otherwise.
        /// </summary>
        public bool IsInitialized { get; private set; }

        private Logger logger;
        private string logIdentity = "Schedules Direct";
        
        private static string apiVersion2014 = "20141201";
        //private static string apiVersion2019 = "20191022";
        private static string apiVersion = apiVersion2014;
        
        private string baseAddress
        {
            get
            {
                if (apiVersion == apiVersion2014)
                    return "https://json.schedulesdirect.org";
                else
                    return "https://w8xmzqba6c.execute-api.us-east-1.amazonaws.com";
            }
        }
        
        private string tokenEndPoint = "/" + apiVersion + "/token";
        private string statusEndPoint = "/" + apiVersion + "/status";
        private string availableEndPoint = "/" + apiVersion + "/available";
        private string headEndsEndPoint = "/" + apiVersion + "/headends";
        private string lineupsEndPoint = "/" + apiVersion + "/lineups";
        private string previewEndPoint = "/" + apiVersion + "/lineups/preview";
        private string schedulesEndPoint = "/" + apiVersion + "/schedules";
        private string schedulesMd5EndPoint = "/" + apiVersion + "/schedules/md5";
        private string programsEndPoint = "/" + apiVersion + "/programs";
        private string metadataEndPoint = "/" + apiVersion + "/metadata/programs";
        private string imageEndPoint = "/" + apiVersion + "/image";

        private string token;
        private Collection<SchedulesDirectService> services;
        private Collection<SchedulesDirectCountry> countries;
        private Collection<SchedulesDirectSatellite> satellites;
        private Collection<SchedulesDirectTransmitter> transmitters;
        private Collection<SchedulesDirectProgramInformation> programInformations = new Collection<SchedulesDirectProgramInformation>();
        private Collection<SchedulesDirectProgramImageList> programImageLists = new Collection<SchedulesDirectProgramImageList>();
        private string transmittersCountryCode;

        private static SchedulesDirectController instance;
        private string userNameLoggedIn;
        private string passwordLoggedIn;

        private Collection<string> logGenres = new Collection<string>();
        private Collection<string> prefixes = new Collection<string>();
        private Collection<string> undefinedRoles = new Collection<string>();

        private int programInfoDownloadedCount;
        private int programInfoLocalCount;

        private int programImageCount;
        private int programNoImageCount;

        private string imageDirectory;
        private Collection<string> imagesStored = new Collection<string>();
        private int imagesAdded;
        private int imagesDeleted;
        private int imageDownloadErrors;
        private int imageDownloadErrorsLimit = 10;

        private Collection<string> jsonFilesUsed = new Collection<string>();
        private int jsonFilesAdded;
        private int jsonFilesDeleted;
        private int jsonFilesStored;

        private int totalContentRatings;
        private int usedContentRatings;
        private int totalQualityRatings;
        private int usedQualityRatings;

        private Logger streamLogger;

        private string baseDirectory = "Schedules Direct";
        private string programInformationDirectory = "Program Information";

        private string getMethod = "GET";
        private string postMethod = "POST";
        private string putMethod = "PUT";
        private string deleteMethod = "DELETE";
        private string userAgent = "EPG Collector";
        
        private SchedulesDirectController() { }

        /// <summary>
        /// Initialize the instance.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase Initialize(string userName, string password)
        {
            if (userName != userNameLoggedIn || password != passwordLoggedIn)
                IsInitialized = false;

            if (IsInitialized)
                return ReplyBase.NoReply();

            logger = new Logger(Logger.NetworkFilePath);
            ReplyBase webReply = setToken(userName, password);
            if (webReply.Message != null)
                return webReply;

            webReply = issueRequest(baseAddress + statusEndPoint, getMethod, null, null);
            if (webReply.Message != null)
                return webReply;

            string jsonString = webReply.ResponseData as string;
            logJson(jsonString, "Status Response");

            SchedulesDirectStatusResponse statusResponse = JsonConvert.DeserializeObject<SchedulesDirectStatusResponse>(jsonString);
            if (statusResponse.Code != 0)
                return ReplyBase.ErrorReply(statusResponse.Message);

            bool online = false;

            if (statusResponse.SystemStatuses != null)
            {
                foreach (SchedulesDirectSystemStatus status in statusResponse.SystemStatuses)
                {
                    if (status.Status.ToLowerInvariant() == "online")
                        online = true;
                }
            }

            if (!online)
                return ReplyBase.ErrorReply("The Schedules Direct service is not online");

            webReply = issueRequest(baseAddress + availableEndPoint, getMethod, null, null);
            if (webReply.Message != null)
                return webReply;

            jsonString = "{\"services\":" + webReply.ResponseData as string + "}";
            logJson(jsonString, "Available Response");

            SchedulesDirectAvailableResponse availableResponse = JsonConvert.DeserializeObject<SchedulesDirectAvailableResponse>(jsonString);
            if (availableResponse.Code != 0)
                return ReplyBase.ErrorReply(availableResponse.Message);

            services = availableResponse.Services;

            userNameLoggedIn = userName;
            passwordLoggedIn = password;
            IsInitialized = true;

            return ReplyBase.NoReply();
        }

        private ReplyBase setToken(string userName, string password)
        {
            WebRequestSpec tokenRequest = new WebRequestSpec(baseAddress + tokenEndPoint, logger, logIdentity);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/json";
            tokenRequest.UserAgent = userAgent;

            string loginData = "{" +
                "\"username\":\"" + userName + "\"," +
                "\"password\":\"" + BitConverter.ToString(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(password))).Replace("-", "").ToLowerInvariant() + "\"" +
                "}";

            byte[] loginBytes = Encoding.ASCII.GetBytes(loginData);

            tokenRequest.ContentLength = loginBytes.Length;

            WebReply webReply;
            
            using (Stream stream = tokenRequest.RequestStream)
            {
                stream.Write(loginBytes, 0, loginBytes.Length);

                webReply = tokenRequest.Process();
                if (webReply.Message != null)
                    return webReply;
            }

            string jsonString = webReply.ResponseData as string;
            logJson(jsonString, "Token Response");

            SchedulesDirectTokenResponse tokenResponse = JsonConvert.DeserializeObject<SchedulesDirectTokenResponse>(jsonString);
            if (tokenResponse.Code != 0)
                return ReplyBase.ErrorReply(tokenResponse.Message);

            token = tokenResponse.Token;
            logger.Write(logIdentity + " Token is now " + token);

            return ReplyBase.NoReply();
        }

        private ReplyBase issueRequest(string url, string method, string data, Collection<Tuple<string, string>> otherHeaders)
        {
            bool done = false;
            int attempts = 0;

            byte[] dataBytes = null;
            if (!string.IsNullOrWhiteSpace(data))
                dataBytes = Encoding.ASCII.GetBytes(data);

            ReplyBase webReply = null;

            while (!done)
            {
                WebRequestSpec request = new WebRequestSpec(url, logger, logIdentity);
                request.Method = method;
                request.ContentType = "application/json";
                request.UserAgent = userAgent;

                if (dataBytes != null)
                    request.ContentLength = dataBytes.Length;

                if (token != null)
                    request.SetHeader("token", token);

                if (otherHeaders != null)
                {
                    foreach (Tuple<string, string> otherHeader in otherHeaders)
                        request.SetHeader(otherHeader.Item1, otherHeader.Item2);
                }

                if (dataBytes != null)
                {
                    using (Stream stream = request.RequestStream)
                    {
                        stream.Write(dataBytes, 0, dataBytes.Length);
                        webReply = request.Process();                        
                    }
                }
                else
                    webReply = request.Process();

                if (webReply.Message == null || (!webReply.Message.ToLowerInvariant().Contains("forbidden") && !webReply.Message.ToLowerInvariant().Contains("the underlying connection was closed")))
                    done = true;
                else
                {
                    if (attempts > 2)
                        done = true;
                    else
                    {
                        webReply = setToken(userNameLoggedIn, passwordLoggedIn);
                        if (webReply.Message != null)
                            done = true;
                        else
                            attempts++;
                    }
                }
            }

            return webReply;
        }        

        /// <summary>
        /// Load the Schedules Direct country list.
        /// </summary>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase LoadCountries()
        {
            if (countries != null)
                return ReplyBase.DataReply(countries);

            ReplyBase webReply = issueRequest(baseAddress + findService("COUNTRIES").Url, getMethod, null, null);
            if (webReply.Message != null)
                return webReply;

            string jsonString = webReply.ResponseData as string;
            logJson(jsonString, "Countries Response");

            dynamic countriesResponse = JsonConvert.DeserializeObject<dynamic>(jsonString);

            foreach (JProperty entry in countriesResponse.Children())
            {
                Collection<SchedulesDirectCountry> countryEntries = ((JArray)entry.Value).ToObject<Collection<SchedulesDirectCountry>>();

                foreach (SchedulesDirectCountry countryEntry in countryEntries)
                {
                    if (countries == null)
                        countries = new Collection<SchedulesDirectCountry>();
                    countries.Add(countryEntry);
                }
            }

            return ReplyBase.DataReply(countries);
        }

        /// <summary>
        /// Load the Schedules Direct satellite list.
        /// </summary>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase LoadSatellites()
        {
            if (satellites != null)
                return ReplyBase.DataReply(satellites);

            ReplyBase webReply = issueRequest(baseAddress + findService("DVB-S").Url, getMethod, null, null);
            if (webReply.Message != null)
                return webReply;

            string jsonString = webReply.ResponseData as string;
            logJson(jsonString, "Satellites Response");

            dynamic satellitesResponse = JsonConvert.DeserializeObject<dynamic>(jsonString);
            satellites = new Collection<SchedulesDirectSatellite>();

            foreach (JObject entry in satellitesResponse as JArray)
                satellites.Add(new SchedulesDirectSatellite(entry["lineup"].ToString()));

            return ReplyBase.DataReply(satellites);    
        }

        /// <summary>
        /// Load the Schedules Direct terrestrial transmitter list.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase LoadTransmitters(string countryCode)
        {
            if (transmittersCountryCode != null && transmittersCountryCode == countryCode)
                return ReplyBase.DataReply(transmitters);

            ReplyBase webReply = issueRequest(baseAddress + findService("DVB-T").Url.Replace("{ISO 3166-1 alpha-3}", countryCode), getMethod, null, null);
            if (webReply.Message != null)
                return webReply;

            string jsonString = webReply.ResponseData as string;
            logJson(jsonString, "Transmitters Response");

            dynamic transmittersResponse = JsonConvert.DeserializeObject<dynamic>(jsonString);

            transmittersCountryCode = countryCode;
            transmitters = new Collection<SchedulesDirectTransmitter>();

            foreach (JProperty entry in transmittersResponse.Children())
                transmitters.Add(new SchedulesDirectTransmitter(entry.Name, entry.Value.ToString()));

            return ReplyBase.DataReply(transmitters);
        }

        /// <summary>
        /// Load the lineups for a postcode.
        /// </summary>
        /// <param name="countryCode">The country code.</param>
        /// <param name="otherCode">The postcode.></param>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase LoadLineups(string countryCode, string otherCode)
        {
            string requestUrl;
            
            if (apiVersion == apiVersion2014)
                requestUrl = baseAddress + headEndsEndPoint + "?country=" + countryCode + "&postalcode=" + otherCode;
            else
                requestUrl = baseAddress + lineupsEndPoint + "?country=" + countryCode + "&postalcode=" + otherCode;

            ReplyBase webReply = issueRequest(requestUrl, getMethod, null, null);
            if (webReply.Message != null)
                return webReply;

            Collection<SchedulesDirectLineup> lineups = new Collection<SchedulesDirectLineup>();

            string jsonString = "{\"headends\":" + webReply.ResponseData as string + "}";
            logJson(jsonString, "Headends/Lineups Response");

            SchedulesDirectHeadendsResponse headendsResponse = JsonConvert.DeserializeObject<SchedulesDirectHeadendsResponse>(jsonString);
            if (headendsResponse.Code != 0)
                return ReplyBase.ErrorReply(headendsResponse.Message);

            if (headendsResponse.Headends == null)
                return ReplyBase.DataReply(lineups);

            foreach (SchedulesDirectHeadend headend in headendsResponse.Headends)
            {
                if (apiVersion == apiVersion2014)
                {
                    if (headend.Lineups != null)
                    {
                        foreach (SchedulesDirectLineup lineup in headend.Lineups)
                        {
                            lineup.Location = headend.Location;
                            lineup.Transport = headend.Transport;

                            lineups.Add(lineup);
                        }
                    }
                }
                else
                {
                    SchedulesDirectLineup lineup = new SchedulesDirectLineup();
                    lineup.Identity = headend.LineupID;
                    lineup.Location = headend.Location;
                    lineup.Name = headend.Name;
                    lineup.Transport = headend.Transport;
                    lineup.Uri = headend.Uri;

                    lineups.Add(lineup);
                }
            }            

            return ReplyBase.DataReply(lineups);
        }

        /// <summary>
        /// Get the users lineup list.
        /// </summary>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase GetLineups()
        {
            string getLineupUrl;

            if (apiVersion == apiVersion2014)
                getLineupUrl = baseAddress + lineupsEndPoint;
            else
                getLineupUrl = baseAddress + statusEndPoint;

            ReplyBase webReply = issueRequest(getLineupUrl, getMethod, null, null);
            if (webReply.Message != null)
                return webReply;

            string jsonString = webReply.ResponseData as string;
            logJson(jsonString, "Users Lineups Response");

            SchedulesDirectLineupsResponse lineupsResponse = JsonConvert.DeserializeObject<SchedulesDirectLineupsResponse>(jsonString);
            if (lineupsResponse.Code != 0)
                return ReplyBase.ErrorReply(lineupsResponse.Message);

            return ReplyBase.DataReply(lineupsResponse.Lineups);
        }

        /// <summary>
        /// Add a lineup to the users account.
        /// </summary>
        /// <param name="lineup">The identity of the lineup.</param>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase AddLineup(string lineup)
        {
            ReplyBase webReply = issueRequest(baseAddress + lineupsEndPoint + "/" + lineup, putMethod, null, null);
            if (webReply.Message != null)
                return webReply;
            else
                return ReplyBase.NoReply();
        }

        /// <summary>
        /// Delete a lineup from a users account.
        /// </summary>
        /// <param name="lineup">The identification of the lineup to delete.</param>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase DeleteLineup(string lineup)
        {
            ReplyBase webReply = issueRequest(baseAddress + lineupsEndPoint + "/" + lineup, deleteMethod, null, null);
            if (webReply.Message != null)
                return webReply;

            string jsonString = webReply.ResponseData as string;
            logJson(jsonString, "Delete Lineup Response");

            SchedulesDirectResponse response = JsonConvert.DeserializeObject<SchedulesDirectResponse>(jsonString);
            if (response.Code != 0)
                return ReplyBase.ErrorReply(response.Message);

            return ReplyBase.NoReply();
        }

        /// <summary>
        /// Load the channels in a lineup.
        /// </summary>
        /// <param name="lineupIdentity">The identity of the lineup.</param>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase LoadPreview(string lineupIdentity)
        {
            ReplyBase webReply = issueRequest(baseAddress + previewEndPoint + "/" + lineupIdentity, getMethod, null, null);
            if (webReply.Message != null)
                return webReply;

            string jsonString = "{\"channels\":" + webReply.ResponseData as string + "}";
            logJson(jsonString, "Preview Response");

            Collection<SchedulesDirectChannel> channels = new Collection<SchedulesDirectChannel>();

            SchedulesDirectPreviewResponse previewResponse = JsonConvert.DeserializeObject<SchedulesDirectPreviewResponse>(jsonString);
            if (previewResponse.Code != 0)
                return ReplyBase.ErrorReply(previewResponse.Message);

            if (previewResponse.Channels == null)
                return ReplyBase.DataReply(channels);

            foreach (SchedulesDirectChannel channel in previewResponse.Channels)
                channels.Add(channel);            

            return ReplyBase.DataReply(channels);
        }

        /// <summary>
        /// Load the Schedules Direct station map and details.
        /// </summary>
        /// <param name="uri">The URL of the map.</param>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase LoadStations(string uri)
        {
            Collection<Tuple<string, string>> otherHeaders = new Collection<Tuple<string,string>>();
            otherHeaders.Add(Tuple.Create<string, string>("verboseMap", "true"));

            ReplyBase webReply = issueRequest(baseAddress + uri, getMethod, null, otherHeaders );
            if (webReply.Message != null)
                return webReply;

            string jsonString = (webReply.ResponseData as string).Replace("\"}},[],{\"", "\"}},{\""); ;
            logJson(jsonString, "Map Response");

            Collection<SchedulesDirectChannel> stations = new Collection<SchedulesDirectChannel>();

            SchedulesDirectMapResponse mapResponse = JsonConvert.DeserializeObject<SchedulesDirectMapResponse>(jsonString);
            if (mapResponse.Code != 0)
                return ReplyBase.ErrorReply(mapResponse.Message);

            if (mapResponse.Stations != null)
            {
                foreach (SchedulesDirectStation station in mapResponse.Stations)
                {
                    SchedulesDirectChannel channel = new SchedulesDirectChannel();
                    channel.Identity = station.StationId;
                    channel.Name = station.Name;
                    channel.CallSign = station.CallSign;
                    channel.Affiliate = station.Affiliate;

                    setChannelNumbers(mapResponse.MapEntries, station.StationId, channel);

                    if (station.StationLogos != null && station.StationLogos.Count != 0)
                    {                        
                        foreach (SchedulesDirectLogo logo in station.StationLogos)
                        {
                            if (!string.IsNullOrWhiteSpace(logo.Url))
                            {
                                if (channel.Logos == null)
                                    channel.Logos = new Collection<string>();

                                channel.Logos.Add(logo.Url);
                            }
                        }
                    }
                    
                    stations.Add(channel);                                
                }
            }

            return ReplyBase.DataReply(stations);
        }

        private void setChannelNumbers(Collection<SchedulesDirectMapEntry> mapEntries, string stationId, SchedulesDirectChannel channel)
        {
            if (mapEntries == null)
                return;

            foreach (SchedulesDirectMapEntry mapEntry in mapEntries)
            {
                if (mapEntry.StationId == stationId)
                    processStationMapEntry(channel, mapEntry);
            }
        }

        private void processStationMapEntry(SchedulesDirectChannel channel, SchedulesDirectMapEntry mapEntry)
        {
            if (mapEntry.AtscMajor != 0)
            {
                channel.MajorChannelNumber = mapEntry.AtscMajor;
                channel.MinorChannelNumber = mapEntry.AtscMinor;
                return;
            }

            if (mapEntry.ChannelMajor != 0)
            {
                channel.MajorChannelNumber = mapEntry.ChannelMajor;
                channel.MinorChannelNumber = mapEntry.ChannelMinor;
                return;
            }

            if (mapEntry.NetworkId != 0)
            {
                channel.OriginalNetworkId = mapEntry.NetworkId;
                channel.TransportStreamId = mapEntry.TransportId;
                channel.ServiceId = mapEntry.ServiceId;                
            }

            if (!string.IsNullOrWhiteSpace(mapEntry.Channel))
            {
                string[] parts = mapEntry.Channel.Split(new char[] { '.' });
                if (parts.Length > 0)
                    channel.MajorChannelNumber = Int32.Parse(parts[0]);
                if (parts.Length > 1)
                    channel.MinorChannelNumber = Int32.Parse(parts[1]);
            }
        }

        /// <summary>
        /// Load the EPG data for a list of channels.
        /// </summary>
        /// <param name="channels">The list of channels.</param>
        /// <returns>A ReplyBase instance.</returns>
        public ReplyBase LoadEpg(Collection<SchedulesDirectChannel> channels)
        {
            ParentalRating.Load();

            ReplyBase reply = loadSchedules(channels, null);
            if (reply.Message != null)
                return reply;

            SchedulesDirectProgramCategory.Load();
            CustomProgramCategory.Load();

            int epgCount = 0;

            foreach (SchedulesDirectSchedule schedule in reply.ResponseData as Collection<SchedulesDirectSchedule>)
            {
                if (RunParameters.Instance.AbandonRequested)
                    break;

                SchedulesDirectChannel channel = findChannel(RunParameters.Instance.SdChannels, schedule.StationId);
                if (channel != null)
                {
                    TVStation station = findStation(channel);

                    if (schedule.Programmes != null)
                    {
                        Collection<SchedulesDirectProgramInformation> programInfos = null;
                        Collection<SchedulesDirectProgramImageList> imageLists = null;

                        reply = loadProgramInformation(schedule.Programmes);
                        if (reply.Message != null)
                            Logger.Instance.Write("Failed to load program information: " + reply.Message);
                        else
                            programInfos = reply.ResponseData as Collection<SchedulesDirectProgramInformation>;

                        reply = loadMetadataInformation(schedule.Programmes);
                        if (reply.Message != null)
                            Logger.Instance.Write("Failed to load metadata information: " + reply.Message);
                        else
                            imageLists = reply.ResponseData as Collection<SchedulesDirectProgramImageList>;

                        foreach (SchedulesDirectProgram program in schedule.Programmes)
                        {
                            SchedulesDirectProgramInformation currentProgramInformation = null;
                            if (programInfos != null)
                                currentProgramInformation = findProgramInformation(programInfos, program.ProgramId, program.Md5);

                            EPGEntry epgEntry = new EPGEntry();
                            epgEntry.StartTime = program.AirDateTime.ToLocalTime();
                            epgEntry.Duration = new TimeSpan(program.Duration * TimeSpan.TicksPerSecond);

                            epgEntry.EpisodeSystemType = "dd_progid";
                            epgEntry.IdPrefix = program.ProgramId.Substring(0, 2);

                            if (!prefixes.Contains(epgEntry.IdPrefix))
                                prefixes.Add(epgEntry.IdPrefix);

                            if (epgEntry.IdPrefix == "SH" || epgEntry.IdPrefix == "EP" || epgEntry.IdPrefix == "MV" || epgEntry.IdPrefix == "SP")
                            {
                                epgEntry.SeriesId = program.ProgramId.Substring(2, program.ProgramId.Length - 6);
                                epgEntry.EpisodeId = program.ProgramId.Substring(program.ProgramId.Length - 4);
                            }
                            epgEntry.UseBase64Crids = false;

                            switch (epgEntry.IdPrefix)
                            {
                                case "EP":                                    
                                    break;
                                case "SH":
                                    /*if (epgEntry.EpisodeId == "0000")
                                        epgEntry.SetIdentitySuffix();*/
                                    break;
                                case "MV":
                                    epgEntry.IsMovie = true;
                                    break;
                                case "SP":
                                    epgEntry.IsSports = true;
                                    break;
                                default:
                                    break;
                            }
                            
                            epgEntry.IsNew = program.New;
                            epgEntry.IsPremiere = program.Premiere;
                            epgEntry.IsRepeat = program.Repeat;                            
                            epgEntry.LiveTapeDelay = program.LiveTapeDelay;
                            epgEntry.PremiereOrFinale = program.IsPremiereOrFinale;

                            if (program.ContentRatings != null && program.ContentRatings.Count != 0)
                            {
                                totalContentRatings++;
 
                                foreach (SchedulesDirectContentRating rating in program.ContentRatings)
                                {
                                    epgEntry.ParentalRating = ParentalRating.FindRating(rating.Body, "Schedules Direct", rating.Code);
                                    epgEntry.MpaaParentalRating = ParentalRating.FindMpaaRating(rating.Body, "Schedules Direct", rating.Code);
                                    epgEntry.ParentalRatingSystem = ParentalRating.FindSystem(rating.Body, "Schedules Direct", rating.Code);

                                    if (!string.IsNullOrWhiteSpace(epgEntry.MpaaParentalRating))
                                    {
                                        usedContentRatings++;
                                        break;
                                    }
                                }                                
                            }

                            if (program.VideoProperties != null)
                            {
                                foreach (string videoProperty in program.VideoProperties)
                                {
                                    switch (videoProperty.ToLowerInvariant())
                                    {
                                        case "hdtv":
                                            epgEntry.VideoQuality = "HDTV";
                                            break;
                                        default:
                                            epgEntry.VideoQuality = videoProperty;
                                            break;
                                    }
                                }
                            }

                            if (program.AudioProperties != null)
                            {
                                foreach (string audioProperty in program.AudioProperties)
                                {
                                    switch (audioProperty.ToLowerInvariant())
                                    {
                                        case "stereo":
                                            epgEntry.AudioQuality = "stereo";
                                            break;
                                        case "dd":
                                        case "dolby":
                                        case "surround":
                                            epgEntry.AudioQuality = "dolby";
                                            break;
                                        case "dd 5.1":
                                            epgEntry.AudioQuality = "dolby digital";
                                            break;
                                        default:
                                            epgEntry.AudioQuality = audioProperty;
                                            break;
                                    }
                                }
                            }

                            if (currentProgramInformation != null)
                                updateEpgEntry(epgEntry, currentProgramInformation);

                            epgEntry.IsGeneric = (epgEntry.IdPrefix != null && epgEntry.IdPrefix == "SH") &&
                                epgEntry.IsSeries &&
                                !epgEntry.IsMiniseries &&
                                (epgEntry.EventName != null && epgEntry.EventName != "Paid Programming");

                            station.EPGCollection.Add(epgEntry);
                            epgCount++;
                        }
                    }
                }
            }

            if (RunParameters.Instance.AbandonRequested)
                return ReplyBase.DataReply(0);

            if (imageDirectory != null)
                tidyImages(imageDirectory);

            string jsonPath = Path.Combine(RunParameters.DataDirectory, baseDirectory, programInformationDirectory);
            if (Directory.Exists(jsonPath))
                tidyJsonFiles(jsonPath);

            return ReplyBase.DataReply(epgCount);
        }
 
        private SchedulesDirectChannel findChannel(Collection<SchedulesDirectChannel> channels, string identity)
        {
            foreach (SchedulesDirectChannel existingChannel in channels)
            {
                if (existingChannel.Identity == identity)
                    return existingChannel;
            }

            return null;
        }

        private TVStation findStation(SchedulesDirectChannel channel)
        {
            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (station.Name == channel.Name)
                    return station;
            }

            TVStation newStation = new TVStation(channel.Name);
            newStation.ImportCallSign = channel.CallSign;

            if (channel.OriginalNetworkId != 0)
            {
                newStation.OriginalNetworkID = channel.OriginalNetworkId;
                newStation.TransportStreamID = channel.TransportStreamId;
                newStation.ServiceID = channel.ServiceId;
                newStation.StationType = TVStationType.Dvb;
            }
            else
            {
                newStation.OriginalNetworkID = 0;

                if (channel.MinorChannelNumber != 0)
                {
                    newStation.TransportStreamID = channel.MajorChannelNumber;
                    newStation.ServiceID = channel.MinorChannelNumber;

                    newStation.MinorChannelNumber = channel.MinorChannelNumber;
                }
                else
                {
                    newStation.TransportStreamID = 0;
                    newStation.ServiceID = channel.MajorChannelNumber;
                    
                    newStation.LogicalChannelNumber = channel.MajorChannelNumber;
                }

                newStation.StationType = TVStationType.Atsc;
            }

            newStation.Affiliate = channel.Affiliate;
            
            if (!string.IsNullOrWhiteSpace(channel.UserName))
                newStation.NewName = channel.UserName;
            if (channel.UserChannelNumber != 0)
                newStation.LogicalChannelNumber = channel.UserChannelNumber;

            if (channel.Logos != null && channel.Logos.Count != 0)
            {
                if (RunParameters.Instance.SdStoreImagesLocally == ImportImageMode.Channels || RunParameters.Instance.SdStoreImagesLocally == ImportImageMode.Both)
                {
                    newStation.ImagePath = StoreImageLocally(channel.Logos[0].StartsWith("http") ? channel.Logos[0] : baseAddress + imageEndPoint + "/" + channel.Logos[0]);

                    for (int index = 1; index < channel.Logos.Count; index++)
                        StoreImageLocally(channel.Logos[index].StartsWith("http") ? channel.Logos[index] : baseAddress + imageEndPoint + "/" + channel.Logos[index]);
                }
                else
                    newStation.ImagePath = channel.Logos[0].StartsWith("http") ? channel.Logos[0] : baseAddress + imageEndPoint + "/" + channel.Logos[0];
            }

            newStation.EPGCollection = new Collection<EPGEntry>();
            RunParameters.Instance.StationCollection.Add(newStation);

            return newStation;
        }

        private void updateEpgEntry(EPGEntry epgEntry, SchedulesDirectProgramInformation program)
        {
            if (program.Titles != null && program.Titles.Count != 0)
                epgEntry.EventName = program.Titles[0].Title;

            if (program.Descriptions != null)
            {
                if (RunParameters.Instance.SdUseLongDescriptions)
                {
                    if (program.Descriptions.LongDescriptions != null && program.Descriptions.LongDescriptions.Count != 0)
                        epgEntry.ShortDescription = program.Descriptions.LongDescriptions[0].Description;
                    else
                    {
                        if (program.Descriptions.ShortDescriptions != null && program.Descriptions.ShortDescriptions.Count != 0)
                            epgEntry.ShortDescription = program.Descriptions.ShortDescriptions[0].Description;
                    }
                }
                else
                {
                    if (program.Descriptions.ShortDescriptions != null && program.Descriptions.ShortDescriptions.Count != 0)
                        epgEntry.ShortDescription = program.Descriptions.ShortDescriptions[0].Description;
                    else
                    {
                        if (program.Descriptions.LongDescriptions != null && program.Descriptions.LongDescriptions.Count != 0)
                            epgEntry.ShortDescription = program.Descriptions.LongDescriptions[0].Description;
                    }
                }
            }

            if (program.ShowType.ToLowerInvariant().Contains("series") && program.OriginalAirDate != null)
                epgEntry.PreviousPlayDate = program.OriginalAirDate.Value;

            epgEntry.EventCategory = getEventCategory(program.Genres, epgEntry.EventName, epgEntry.ShortDescription);
            if (program.Genres != null && program.Genres.Count != 0)
            {
                foreach (string logGenre in program.Genres)
                {
                    int index = program.Genres.IndexOf(logGenre);

                    if (index == 0)
                    {
                        string storedGenre = logGenre + ":" + index;
                        if (!logGenres.Contains(storedGenre))
                            logGenres.Add(storedGenre);
                    }
                    else
                    {
                        if (!logGenres.Contains(logGenre))
                            logGenres.Add(logGenre);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(program.EpisodeTitle))
                epgEntry.EventSubTitle = program.EpisodeTitle;

            if (program.MetadataEntries != null)
            {
                foreach (SchedulesDirectProgramMetadata metadata in program.MetadataEntries)
                {
                    if (metadata.GracenoteMetadata != null)
                    {
                        if (metadata.GracenoteMetadata.Season != 0)
                            epgEntry.SeasonNumber = metadata.GracenoteMetadata.Season;
                        if (metadata.GracenoteMetadata.Episode != 0)
                            epgEntry.EpisodeNumber = metadata.GracenoteMetadata.Episode;
                    }
                    else
                    {
                        if (metadata.TvdbMetadata != null)
                        {
                            if (metadata.TvdbMetadata.Season != 0)
                                epgEntry.SeasonNumber = metadata.TvdbMetadata.Season;
                            if (metadata.TvdbMetadata.Episode != 0)
                                epgEntry.EpisodeNumber = metadata.TvdbMetadata.Episode;
                        }
                    }   
                }
            }

            if (program.Cast != null && program.Cast.Count != 0)
            {
                foreach (SchedulesDirectCastMember castMember in program.Cast)
                    processRole(epgEntry, castMember);
            }

            if (program.Crew != null && program.Crew.Count != 0)
            {
                foreach (SchedulesDirectCastMember castMember in program.Crew)
                    processRole(epgEntry, castMember);
            }

            if (program.HasImageArtwork)
            {
                if (program.EpisodeImage == null)
                {
                    SchedulesDirectProgramImageList imageList = findProgramImageList(programImageLists, program.ProgramId);
                    if (imageList != null && imageList.ImageEntries != null && imageList.ImageEntries.Count != 0)
                    {
                        SchedulesDirectImageEntry bestImage = findBestImage(imageList.ImageEntries);
                        if (bestImage != null)
                        {
                            if (RunParameters.Instance.SdStoreImagesLocally == ImportImageMode.Programmes || RunParameters.Instance.SdStoreImagesLocally == ImportImageMode.Both)
                                epgEntry.PosterPath = StoreImageLocally(bestImage.Uri.StartsWith("http") ? bestImage.Uri : baseAddress + imageEndPoint + "/" + bestImage.Uri);
                            else
                                epgEntry.PosterPath = bestImage.Uri.StartsWith("http") ? bestImage.Uri : baseAddress + imageEndPoint + "/" + bestImage.Uri;

                            epgEntry.PosterHeight = bestImage.Height;
                            epgEntry.PosterWidth = bestImage.Width;

                            programImageCount++;
                        }
                    }
                    else
                        programNoImageCount++;
                }
                else
                {
                    if (RunParameters.Instance.SdStoreImagesLocally == ImportImageMode.Programmes || RunParameters.Instance.SdStoreImagesLocally == ImportImageMode.Both)
                        epgEntry.PosterPath = StoreImageLocally(baseAddress + program.EpisodeImage.Uri);
                    else
                        epgEntry.PosterPath = baseAddress + program.EpisodeImage;

                    epgEntry.PosterHeight = program.EpisodeImage.Height;
                    epgEntry.PosterWidth = program.EpisodeImage.Width;

                    programImageCount++;
                }
            }

            if (program.Movie != null && program.Movie.Ratings != null)
            {
                foreach (SchedulesDirectQualityRating rating in program.Movie.Ratings)
                {
                    totalQualityRatings++;

                    epgEntry.StarRating = rating.StarRating;
                    if (epgEntry.StarRating != null)
                    {
                        usedQualityRatings++;
                        break;
                    }
                }
            }

            epgEntry.IsMovie = program.EntityType.ToLowerInvariant() == "movie";
            epgEntry.IsSports = program.EntityType.ToLowerInvariant() == "sports";
            epgEntry.IsSeries = program.ShowType.ToLowerInvariant() == "series";

            if (program.Genres != null)
                epgEntry.IsMiniseries = program.Genres.Contains("Miniseries");
        }

        private EventCategorySpec getEventCategory(Collection<string> categories, string eventName, string description)
        {
            if (categories == null || categories.Count == 0)
                return null;

            ProgramCategory category = SchedulesDirectProgramCategory.FindCategory(categories[0]);
            if (category != null)
            {
                category.UsedCount++;
                category.SampleEvent = eventName;
                return new EventCategorySpec(category.AddToDescriptions(categories, 1));
            }

            SchedulesDirectProgramCategory.AddUndefinedCategory(categories[0], eventName);

            ProgramCategory customCategory = CustomProgramCategory.FindCategory(eventName);
            if (customCategory != null)
                return new EventCategorySpec(customCategory);

            customCategory = CustomProgramCategory.FindCategory(description);
            if (customCategory != null)
                return new EventCategorySpec(customCategory);

            string updatedCategory = categories[0];
            for (int index = 1; index < categories.Count; index++)
                updatedCategory += "," + categories[index];
            
            return new EventCategorySpec(updatedCategory);
        }

        private void processRole(EPGEntry epgEntry, SchedulesDirectCastMember castMember)
        {
            if (string.IsNullOrWhiteSpace(castMember.Name))
                return;

            switch (castMember.Role.ToLowerInvariant())
            {
                case "actor":
                case "music performer":
                case "film editor":
                case "cinematographer":
                case "hair stylist":
                case "makeup artist":
                case "costume designer":                
                case "casting":
                case "set decoration":                
                case "visual effects":
                case "special effects":
                case "makeup supervisor":
                case "key makeup artist":
                case "hair stylist supervisor":
                case "set dresser":
                case "sound mixer":
                case "sound designer":
                case "supervising sound editor":
                case "sound editor":
                case "boom operator":
                case "production sound mixer":
                case "sound re-recording mixer":
                    if (epgEntry.Cast == null)
                        epgEntry.Cast = new Collection<Person>();
                    epgEntry.Cast.Add(new Person(castMember.Name, castMember.Role, castMember.CharacterName, castMember.BillingOrderNumber));
                    break;
                case "director":
                case "art direction":
                case "art director":
                case "artistic director":
                case "assistant director":
                case "director of photography":
                case "first assistant director":
                case "second assistant director":
                case "second unit director":
                case "supervising art direction":
                case "casting director":
                case "music director":
                case "associate set director":
                case "sound":
                    if (epgEntry.Directors == null)
                        epgEntry.Directors = new Collection<Person>();
                    epgEntry.Directors.Add(new Person(castMember.Name, castMember.Role, castMember.CharacterName, epgEntry.Directors.Count + 1));
                    break;
                case "writer":
                case "writer (story)":
                case "writer (screenplay)":
                case "writer (play)":
                case "writer (novel)":
                case "writer (characters)":
                case "writer (book)":
                case "writer (screen story)":
                case "writer (television series)":
                case "creator":
                case "screenwriter":
                case "original songs":
                case "original music":
                case "composer":
                case "music":
                case "adaptation":
                case "story":
                case "editor":
                case "non-original music":
                case "music editor":
                case "original concept":
                case "editing":
                case "lyrics":
                    if (epgEntry.Writers == null)
                        epgEntry.Writers = new Collection<Person>();
                    epgEntry.Writers.Add(new Person(castMember.Name, castMember.Role, castMember.CharacterName, epgEntry.Writers.Count + 1));
                    break;
                case "producer":
                case "assistant producer":
                case "co-producer":
                case "executive producer":
                case "co-executive producer":
                case "supervising producer":
                case "senior producer":
                case "coordinating producer":
                case "production manager":
                case "production design":
                case "production designer":
                case "associate producer":
                case "executive in charge of production":
                    if (epgEntry.Producers == null)
                        epgEntry.Producers = new Collection<Person>();
                    epgEntry.Producers.Add(new Person(castMember.Name, castMember.Role, castMember.CharacterName, epgEntry.Producers.Count + 1));
                    break;
                case "host":
                case "anchor":
                case "voice":
                case "narrator":
                case "correspondent":
                    if (epgEntry.Presenters == null)
                        epgEntry.Presenters = new Collection<Person>();
                    epgEntry.Presenters.Add(new Person(castMember.Name, castMember.Role, castMember.CharacterName, epgEntry.Presenters.Count + 1));
                    break;
                case "guest":
                case "guest star":
                case "guest voice":
                case "judge":
                case "contestant":
                    if (epgEntry.GuestStars == null)
                        epgEntry.GuestStars = new Collection<Person>();
                    epgEntry.GuestStars.Add(new Person(castMember.Name, castMember.Role, castMember.CharacterName, epgEntry.GuestStars.Count + 1));
                    break;
                default:                    
                    if (epgEntry.Cast == null)
                        epgEntry.Cast = new Collection<Person>();
                    epgEntry.Cast.Add(new Person(castMember.Name, castMember.Role, castMember.CharacterName, castMember.BillingOrderNumber));

                    if (undefinedRoles == null)
                        undefinedRoles = new Collection<string>();
                    if (!undefinedRoles.Contains(castMember.Role))
                        undefinedRoles.Add(castMember.Role);

                    break;
            }
        }

        private SchedulesDirectImageEntry findBestImage(Collection<SchedulesDirectImageEntry>  imageEntries)
        {
            if (imageEntries == null)
                return null;

            SchedulesDirectImageEntry selectedEntry = findImage(imageEntries, "banner-lot");
            if (selectedEntry != null)
                return selectedEntry;

            selectedEntry = findImage(imageEntries, "banner-lo");
            if (selectedEntry != null)
                return selectedEntry;

            selectedEntry = findImage(imageEntries, "banner-l3");
            if (selectedEntry != null)
                return selectedEntry;

            selectedEntry = findImage(imageEntries, "banner");
            if (selectedEntry != null)
                return selectedEntry;

            selectedEntry = findImage(imageEntries, "poster art");
            if (selectedEntry != null)
                return selectedEntry;

            selectedEntry = findImage(imageEntries, "logo");
            if (selectedEntry != null)
                return selectedEntry;

            selectedEntry = findImage(imageEntries, "iconic");
            if (selectedEntry != null)
                return selectedEntry;

            selectedEntry = findImage(imageEntries, "staple");
            if (selectedEntry != null)
                return selectedEntry;

            if (imageEntries[0].Uri != null)
                return imageEntries[0];
            else
                return null;
        }

        private SchedulesDirectImageEntry findImage(Collection<SchedulesDirectImageEntry> imageEntries, string category)
        {
            SchedulesDirectImageEntry selectedEntry = null;

            foreach (SchedulesDirectImageEntry imageEntry in imageEntries)
            {
                if (imageEntry.Category != null)
                {
                    if (imageEntry.Category.ToLowerInvariant() == category)
                    {
                        if (selectedEntry == null || Int32.Parse(imageEntry.Height) < Int32.Parse(selectedEntry.Height))
                            selectedEntry = imageEntry;
                    }
                }
            }

            return selectedEntry;
        }

        private ReplyBase loadSchedulesMd5(Collection<SchedulesDirectChannel> channels, Collection<DateTime> dates)
        {
            StringBuilder schedulesData = new StringBuilder("[");

            foreach (SchedulesDirectChannel channel in channels)
            {
                if (channels.IndexOf(channel) != 0)
                    schedulesData.Append(",");

                if (dates == null)
                    schedulesData.Append("{" + "\"stationID\":\"" + channel.Identity + "\"}");
                else
                {
                    foreach (DateTime date in dates)
                    {
                        if (dates.IndexOf(date) != 0)
                            schedulesData.Append(",");
                        schedulesData.Append("\"" + date.ToString("yyyy-MM-dd") + "\"");
                    }

                    schedulesData.Append("]}");
                }
            }

            schedulesData.Append("]");

            ReplyBase webReply = issueRequest(baseAddress + schedulesMd5EndPoint, postMethod, schedulesData.ToString(), null);
            if (webReply.Message != null)
                return webReply;

            string jsonString = "{\"schedules\":" + webReply.ResponseData as string + "}";
            logJson(jsonString, "Schedules MD5 Response");

            Collection<SchedulesDirectMd5Entry> md5Entries = new Collection<SchedulesDirectMd5Entry>();

            dynamic jsonData = JsonConvert.DeserializeObject<dynamic>(jsonString);

            foreach (JProperty stationEntry in ((JContainer)jsonData).Children())
            {
                string stationId = stationEntry.Name;

                foreach (JToken dateEntry in stationEntry.Children())                {
                    DateTime date = DateTime.ParseExact(dateEntry.Value<string>(), "yyyy-MM-dd", null);

                    SchedulesDirectMd5Response md5Response = ((JObject)dateEntry.Value<string>() as dynamic).ToObject<SchedulesDirectMd5Response>();                    
                    md5Entries.Add(new SchedulesDirectMd5Entry(stationId, date, md5Response.Md5, md5Response.LastModified));
                }
            }

            return ReplyBase.DataReply(md5Entries);
        }

        private ReplyBase loadSchedules(Collection<SchedulesDirectChannel> channels, Collection<DateTime> dates)
        {
            StringBuilder schedulesData = new StringBuilder("[");

            foreach (SchedulesDirectChannel channel in channels)
            {
                if (channels.IndexOf(channel) != 0)
                    schedulesData.Append(",");

                if (dates == null)
                    schedulesData.Append("{" + "\"stationID\":\"" + channel.Identity + "\"}");
                else
                {
                    foreach (DateTime date in dates)
                    {
                        if (dates.IndexOf(date) != 0)
                            schedulesData.Append(",");
                        schedulesData.Append("\"" + date.ToString("yyyy-MM-dd") + "\"");
                    }

                    schedulesData.Append("]}");
                }
            }

            schedulesData.Append("]");

            ReplyBase webReply = issueRequest(baseAddress + schedulesEndPoint, postMethod, schedulesData.ToString(), null);
            if (webReply.Message != null)
                    return webReply;

            string jsonString = "{\"schedules\":" + webReply.ResponseData as string + "}";
            logJson(jsonString, "Schedules Response");

            SchedulesDirectSchedulesResponse schedulesResponse = JsonConvert.DeserializeObject<SchedulesDirectSchedulesResponse>(jsonString);
            if (schedulesResponse.Code != 0)
                return ReplyBase.ErrorReply(schedulesResponse.Message);

            return ReplyBase.DataReply(schedulesResponse.Schedules);
        }

        private ReplyBase loadProgramInformation(Collection<SchedulesDirectProgram> programs)
        {
            WebRequestSpec programsRequest = new WebRequestSpec(baseAddress + programsEndPoint, logger, logIdentity);
            programsRequest.Method = "POST";

            Collection<Tuple<string, string>> uniqueProgramIds = new Collection<Tuple<string, string>>();
            foreach (SchedulesDirectProgram program in programs)
            {                
                bool found = false;

                foreach (Tuple<string, string> programTuple in uniqueProgramIds)
                {
                    if (programTuple.Item1 == program.ProgramId)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    uniqueProgramIds.Add(Tuple.Create<string, string>(program.ProgramId, program.Md5));
            }

            Collection<SchedulesDirectProgramInformation> programsList = new Collection<SchedulesDirectProgramInformation>();

            bool first = true;
            StringBuilder programData = new StringBuilder("[");

            foreach (Tuple<string, string> programTuple in uniqueProgramIds)
            {
                SchedulesDirectProgramInformation storedProgramInformation = findProgramInformation(programInformations, programTuple.Item1, programTuple.Item2);
                if (storedProgramInformation != null)
                    programsList.Add(storedProgramInformation);
                else
                {
                    if (!first)
                        programData.Append(",");
                    else
                        first = false;

                    programData.Append("\"" + programTuple.Item1 + "\"");
                    programInfoDownloadedCount++;
                }
            }

            if (!first)
            {
                programData.Append("]");

                ReplyBase webReply = issueRequest(baseAddress + programsEndPoint, postMethod, programData.ToString(), null);
                if (webReply.Message != null)
                        return webReply;

                string jsonString = "{\"programs\":" + webReply.ResponseData as string + "}";
                logJson(jsonString, "Programs Response");                

                SchedulesDirectProgramInformationResponse programInformationResponse = JsonConvert.DeserializeObject<SchedulesDirectProgramInformationResponse>(jsonString);
                if (programInformationResponse.Code != 0)
                    return ReplyBase.ErrorReply(programInformationResponse.Message);

                foreach (SchedulesDirectProgramInformation program in programInformationResponse.Programmes)
                {
                    programsList.Add(program);
                    programInformations.Add(program);
                    saveProgramInformation(program);
                }
            }
            
            return ReplyBase.DataReply(programsList);
        }

        private void saveProgramInformation(SchedulesDirectProgramInformation program)
        {
            string programInfoPath = Path.Combine(RunParameters.DataDirectory, baseDirectory, programInformationDirectory);
            if (!Directory.Exists(programInfoPath))
                Directory.CreateDirectory(programInfoPath);

            string fileName = program.ProgramId.ToString() + "_" + getValidName(program.Md5) + ".json";

            string programInfoFileName = Path.Combine(programInfoPath, fileName);
            if (File.Exists(programInfoFileName))
                return;

            string jsonString = JsonConvert.SerializeObject(program);

            StreamWriter writer = new StreamWriter(new FileStream(programInfoFileName, FileMode.Create, FileAccess.Write));
            writer.Write(jsonString);
            writer.Close();

            jsonFilesUsed.Add(fileName);
            jsonFilesAdded++;
        }

        private ReplyBase loadMetadataInformation(Collection<SchedulesDirectProgram> programs)
        {
            WebRequestSpec metadataRequest = new WebRequestSpec(baseAddress + metadataEndPoint, logger, logIdentity);
            metadataRequest.Method = "POST";

            Collection<string> uniqueProgramIds = new Collection<string>();
            foreach (SchedulesDirectProgram program in programs)
            {
                if (!uniqueProgramIds.Contains(program.ProgramId))
                    uniqueProgramIds.Add(program.ProgramId);
            }

            Collection<SchedulesDirectProgramImageList> replyImageList = new Collection<SchedulesDirectProgramImageList>();

            bool first = true;
            StringBuilder programData = new StringBuilder("[");

            foreach (string programId in uniqueProgramIds)
            {
                SchedulesDirectProgramImageList storedProgramImageList = findProgramImageList(programImageLists, programId);
                if (storedProgramImageList != null)
                    replyImageList.Add(storedProgramImageList);
                else
                {
                    if (!first)
                        programData.Append(",");
                    else
                        first = false;

                    programData.Append("\"" + programId + "\"");
                }
            }

            if (!first)
            {
                programData.Append("]");

                ReplyBase webReply = issueRequest(baseAddress + metadataEndPoint, postMethod, programData.ToString(), null); 
                if (webReply.Message != null)
                        return webReply;

                string jsonString = "{\"programs\":" + webReply.ResponseData as string + "}";
                logJson(jsonString, "Metadata Response 1");
                jsonString = jsonString.Replace("\"data\":{", "\"data\":[],\"error\":{");
                logJson(jsonString, "Metadata Response 2");

                SchedulesDirectImageResponse imageResponse = JsonConvert.DeserializeObject<SchedulesDirectImageResponse>(jsonString);
                if (imageResponse.Code != 0)
                    return ReplyBase.ErrorReply(imageResponse.Message);

                foreach (SchedulesDirectProgramImageList imageList in imageResponse.ImageList)
                {
                    replyImageList.Add(imageList);
                    programImageLists.Add(imageList);
                }
            }

            return ReplyBase.DataReply(replyImageList);
        }

        /// <summary>
        /// Log collection statistics.
        /// </summary>
        public void LogStats()
        {
            Logger.Instance.Write("Program Information records locally accessed = " + programInfoLocalCount);
            Logger.Instance.Write("Program Information records downloaded = " + programInfoDownloadedCount);
            Logger.Instance.Write("");
            Logger.Instance.Write("Program Information records added = " + jsonFilesAdded);
            Logger.Instance.Write("Program Information records deleted = " + jsonFilesDeleted);
            Logger.Instance.Write("Program Information records stored locally = " + jsonFilesStored);
            Logger.Instance.Write("");
            Logger.Instance.Write("Total content ratings = " + totalContentRatings);
            Logger.Instance.Write("Used content ratings = " + usedContentRatings);
            Logger.Instance.Write("Total quality ratings = " + totalQualityRatings);
            Logger.Instance.Write("Used quality ratings = " + usedQualityRatings);
            Logger.Instance.Write("");
            Logger.Instance.Write("Programs with images = " + programImageCount);
            Logger.Instance.Write("Programs without images = " + programNoImageCount);
            Logger.Instance.Write("Images added = " + imagesAdded);
            Logger.Instance.Write("Images deleted= " + imagesDeleted);
            Logger.Instance.Write("Total images = " + imagesStored.Count);
            Logger.Instance.Write("Download errors = " + imageDownloadErrors);
            Logger.Instance.Write("");
            Logger.Instance.Write("Total JSON files deleted = " + jsonFilesDeleted);
            Logger.Instance.Write("Total images = " + imagesStored.Count);

            ProgramCategory.LogUsage(SchedulesDirectProgramCategory.Categories, SchedulesDirectProgramCategory.UndefinedCategories, "Schedules Direct");
            
            if (undefinedRoles != null)
            {
                StringBuilder undefinedRolesString = new StringBuilder();

                foreach (string undefinedRole in undefinedRoles)
                {
                    if (undefinedRolesString.Length == 0)
                        undefinedRolesString.Append("Undefined roles: " + undefinedRole);
                    else
                        undefinedRolesString.Append(", " + undefinedRole);
                }

                Logger.Instance.Write(undefinedRolesString.ToString());
            }

            if (DebugEntry.IsDefined(DebugName.LogSdPrefixes))
            {
                string prefixString = "Prefixes used: ";
                foreach (string prefix in prefixes)
                    prefixString += prefix + ", ";
                Logger.Instance.Write(prefixString.Substring(0, prefixString.Length - 2));
            }

            if (DebugEntry.IsDefined(DebugName.LogSdGenres))
            {
                foreach (string genre in logGenres)
                    Logger.Instance.Write("Genre used: " + genre);
            }
        }

        private SchedulesDirectProgramInformation findProgramInformation(Collection<SchedulesDirectProgramInformation> programs, string programId, string md5)
        {
            foreach (SchedulesDirectProgramInformation programInformation in programs)
            {
                if (programInformation.ProgramId == programId)
                    return programInformation;
            }

            string fileName = programId.ToString() + "_" + getValidName(md5) + ".json";
            string programInfoFileName = Path.Combine(RunParameters.DataDirectory, baseDirectory, programInformationDirectory, fileName);
            if (File.Exists(programInfoFileName))
            {
                StreamReader reader = new StreamReader(new FileStream(programInfoFileName, FileMode.Open, FileAccess.Read));
                SchedulesDirectProgramInformation programInfo = JsonConvert.DeserializeObject<SchedulesDirectProgramInformation>(reader.ReadToEnd());
                reader.Close();

                programInfoLocalCount++;

                if (!jsonFilesUsed.Contains(fileName))
                    jsonFilesUsed.Add(fileName);

                return programInfo;
            }

            return null;
        }

        private string getValidName(string name)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(name));
        }

        private SchedulesDirectProgramImageList findProgramImageList(Collection<SchedulesDirectProgramImageList> imageLists, string programId)
        {
            foreach (SchedulesDirectProgramImageList programImageList in imageLists)
            {
                if (programImageList.ProgramId == programId)
                    return programImageList;
            }

            return null;
        }

        private SchedulesDirectService findService(string type)
        {
            if (services == null)
                return null;

            foreach (SchedulesDirectService service in services)
            {
                if (service.Type == type)
                    return service;
            }

            return null;
        }

        /// <summary>
        /// Get an icon and optionally output to a file.
        /// </summary>
        /// <param name="url">The URL of the icon.</param>
        /// <returns>The original URL if the icon cannot be located or the path to the stored icon.</returns>
        public string StoreImageLocally(string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                return url;

            if (imageDownloadErrors >= imageDownloadErrorsLimit)
                return url;

            if (RunParameters.Instance.HttpProxy == null)
            {
                Logger.Instance.Write("HTTP proxy not available - images cannot be stored locally");
                imageDownloadErrors = imageDownloadErrorsLimit;
                return url;
            }

            if (streamLogger == null)
                streamLogger = new Logger("EPG Collector Stream.log");

            imageDirectory = Path.Combine(RunParameters.DataDirectory, "Images", "Imports");
            if (!Directory.Exists(imageDirectory))
                Directory.CreateDirectory(imageDirectory);

            Collection<string> files = new Collection<string>();
            foreach (string file in Directory.GetFiles(imageDirectory))
            {
                FileInfo fileInfo = new FileInfo(file);
                files.Add(fileInfo.FullName);
            }

            try
            {
                Uri imageUri = new Uri(url);
                string fullImagePath = Path.Combine(imageDirectory, imageUri.Segments[imageUri.Segments.Length - 1]);

                if (!files.Contains(fullImagePath))
                {
                    Logger.Instance.Write("Downloading image '" + url + "'");

                    WebRequestSpec webRequest = new WebRequestSpec(url, streamLogger, "Schedules Direct");
                    ReplyBase reply = webRequest.Process();
                    if (reply.Message != null)
                    {
                        Logger.Instance.Write("Failed to download image '" + url + "' - " + reply.Message);

                        imageDownloadErrors++;
                        if (imageDownloadErrors >= imageDownloadErrorsLimit)
                            Logger.Instance.Write("Download failures have reached the maximum of " + imageDownloadErrorsLimit + " - no more images downloaded");

                        return url;
                    }
                    else
                    {
                        FileStream fileStream = new FileStream(fullImagePath, FileMode.Create, FileAccess.Write);
                        BinaryWriter writer = new BinaryWriter(fileStream);
                        writer.Write(reply.ResponseData as byte[]);
                        writer.Close();

                        imagesAdded++;
                    }
                }

                imagesStored.Add(fullImagePath);


                return fullImagePath;
            }
            catch (Exception e)
            {
                Logger.Instance.Write("Failed to download image '" + url + "' exception - " + e.Message);
                if (e.InnerException != null)
                    Logger.Instance.Write(e.InnerException.Message);

                imageDownloadErrors++;

                if (imageDownloadErrors >= imageDownloadErrorsLimit)
                    Logger.Instance.Write("Download failures have reached the maximum of " + imageDownloadErrorsLimit + " - no more images downloaded");

                return url;
            }
        }

        private void tidyImages(string path)
        {
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                if (file.EndsWith(".jpg") || file.EndsWith(".png"))
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);

                        if (!imagesStored.Contains(fileInfo.FullName) && fileInfo.LastWriteTime < DateTime.Now.AddDays(-14))
                        {
                            File.Delete(file);
                            imagesDeleted++;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.Write("Failed to delete stored image '" + file + "' exception - " + e.Message);
                    }
                }
            }
        }

        private void tidyJsonFiles(string path)
        {
            string[] files = Directory.GetFiles(path);            

            foreach (string file in files)
            {
                if (file.EndsWith(".json"))
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);

                        if (!jsonFilesUsed.Contains(fileInfo.FullName) && fileInfo.LastWriteTime < DateTime.Now.AddDays(-14))
                        {
                            File.Delete(file);
                            jsonFilesDeleted++;
                        }
                        else
                            jsonFilesStored++;

                    }
                    catch (Exception e)
                    {
                        Logger.Instance.Write("Failed to delete stored JSON file '" + file + "' exception - " + e.Message);
                    }
                }
            }
        }

        private void logJson(string data, string name)
        {
            if (!DebugEntry.IsDefined(DebugName.LogJsonStructure) && !DebugEntry.IsDefined(DebugName.LogJsonText))
                return;

            if (DebugEntry.IsDefined(DebugName.LogJsonText))
                logger.Write(logIdentity + " JSON text for " + name + ": " + data);

            if (DebugEntry.IsDefined(DebugName.LogJsonStructure))
            {
                try
                {
                    dynamic jsonData = JsonConvert.DeserializeObject<object>(data);
                    logger.Write(logIdentity + " JSON structure for " + name + ": " + jsonData.ToString());
                }
                catch (Exception e)
                {
                    logger.Write(logIdentity + " Failed to deserialize JSON text for " + name);
                    logger.Write(logIdentity + e.Message);
                    logger.Write(logIdentity + " " + data);
                }
            }
        }
    }
}

