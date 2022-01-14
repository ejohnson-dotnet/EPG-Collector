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
using System.Xml;
using System.IO;
using System.Text;
using System.Reflection;
using System.Net;
using System.Web;

using DomainObjects;

namespace XmltvParser
{
    /// <summary>
    /// The class that describes the Xmltv parser controller.
    /// </summary>
    public sealed class XmltvController : ImportFileBase
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

        private string imageDirectory;
        private Collection<string> imagesStored = new Collection<string>();
        private int imagesAdded;
        private int imagesDeleted;
        private int imageDownloadErrors;
        private int imageDownloadErrorsLimit = 10;

        private int seriesCount;
        private int miniseriesCount;
        private int movieCount;
        private int genericCount;
        private int dstAdjustmentCount;

        private Logger streamLogger;

        private DateTime defaultPlayDate = new DateTime(1970, 1, 1);

        private bool hasSeriesCategory;

        private int invalidTimes;
        
        /// <summary>
        /// Process an XMLTV file.
        /// </summary>
        /// <param name="fileName">The actual file path.</param>
        /// <param name="fileSpec">The file definition.</param>
        /// <returns>An error message or null if the file is processed successfully.</returns>
        public override string Process(string fileName, ImportFileSpec fileSpec)
        {
            XmltvProgramCategory.Load();
            CustomProgramCategory.Load();

            XmltvChannel.Channels = new Collection<XmltvChannel>();
            XmltvProgramme.Programmes = new Collection<XmltvProgramme>();

            XmlReader xmlReader = null;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.DtdProcessing = DtdProcessing.Ignore;
            settings.CheckCharacters = false;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            
            try
            {
                xmlReader = XmlReader.Create(fileName, settings);
            }
            catch (IOException)
            {
                return("Failed to open " + fileName);
            }

            try
            {
                while (!xmlReader.EOF)
                {
                    xmlReader.Read();
                    if (xmlReader.IsStartElement())
                    {
                        switch (xmlReader.Name)
                        {
                            case "channel":
                                XmltvChannel channel = XmltvChannel.GetInstance(xmlReader.ReadSubtree());
                                channel.IdFormat = fileSpec.IdFormat;
                                XmltvChannel.Channels.Add(channel);                                
                                break;
                            case "programme":
                                XmltvProgramme programme = XmltvProgramme.GetInstance(xmlReader.ReadSubtree());
                                XmltvProgramme.Programmes.Add(programme);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (XmlException e)
            {
                return ("Failed to load xmltv file: " + e.Message);                
            }
            catch (IOException e)
            {
                return ("Failed to load xmltv file: " + e.Message);                
            }

            if (xmlReader != null)
                xmlReader.Close();

            hasSeriesCategory = checkForSeriesCategory(fileSpec);

            Collection<TVStation> xmltvChannels = createEPGData(fileSpec);
            if (xmltvChannels != null)
                MergeChannels(xmltvChannels, fileSpec.Precedence);

            if (imageDirectory != null)
                tidyImages(imageDirectory);

            if (OptionEntry.IsDefined(OptionName.NoInvalidEntries))
                Logger.Instance.Write("Number of EPG records with invalid start or end times = " + invalidTimes);

            return (null);
        }

        private bool checkForSeriesCategory(ImportFileSpec fileSpec)
        {
            if (fileSpec.IdFormat != XmltvIdFormat.Zap2ItAtsc)
                return false;

            if (XmltvProgramme.Programmes == null)
                return false;

            foreach (XmltvProgramme programme in XmltvProgramme.Programmes)
            {
                if (programme.Categories != null)
                {
                    foreach (XmltvText categoryText in programme.Categories)
                    {
                        string category = checkCategoryLanguage(categoryText, fileSpec.Language);
                        if (category != null && category == "Series")
                            return true;
                    }
                }
            }

            return false;
        }

        private Collection<TVStation> createEPGData(ImportFileSpec fileSpec)
        {
            Collection<TVStation> stations = ProcessChannels(fileSpec.Language, fileSpec.StoreImagesLocally);
            if (stations != null)
                processProgrammes(stations, fileSpec);

            return (stations);
        }

        /// <summary>
        /// Create stations from the XMLTV channel data.
        /// </summary>
        /// <param name="languageCode">The language code. May be null</param>
        /// <param name="storeImagesLocally">True to download images; false otherwise.</param>
        /// <returns>A list of stations or null if no channel data exists.</returns>
        public Collection<TVStation> ProcessChannels(LanguageCode languageCode, ImportImageMode storeImagesLocally)
        {
            if (XmltvChannel.Channels == null || XmltvChannel.Channels.Count == 0)
                return (null);

            Collection<TVStation> stations = new Collection<TVStation>();

            foreach (XmltvChannel channel in XmltvChannel.Channels)
            {
                TVStation station;

                if (channel.DisplayNames == null || channel.DisplayNames.Count == 0)
                    station = new TVStation("No Name");
                else
                    station = new TVStation(findLanguageString(channel.DisplayNames, languageCode).Trim());
                
                switch (channel.IdFormat)
                {
                    case XmltvIdFormat.FullChannelId:
                        setStationId(station, channel);
                        station.StationType = TVStationType.Dvb;
                        break;
                    case XmltvIdFormat.ServiceId:
                        setStationServiceId(station, channel);
                        station.StationType = TVStationType.Dvb;
                        break;
                    case XmltvIdFormat.Zap2ItAtsc:
                        setStationZap2ItAtsc(station, channel, stations.Count + 1);
                        station.IgnoreFrequencyForMerge = true;
                        station.StationType = TVStationType.Atsc;
                        break;
                    default:
                        station.ServiceID = stations.Count + 1;
                        station.UseNameForMerge = channel.IdFormat == XmltvIdFormat.Name;
                        station.StationType = TVStationType.Dvb;
                        break;
                }

                station.ProviderName = channel.Id;
                
                if (channel.IdFormat == XmltvIdFormat.UserChannelNumber)
                    setStationLogicalChannelNumber(station, channel);

                if (RunParameters.Instance.ImportChannelChanges != null)
                {
                    bool found = false;

                    foreach (ImportChannelChange channelChange in RunParameters.Instance.ImportChannelChanges)
                    {
                        if (channelChange.DisplayName == station.Name)
                        {
                            if (!string.IsNullOrWhiteSpace(channelChange.NewName)) 
                                station.NewName = channelChange.NewName;
                            if (!string.IsNullOrWhiteSpace(channelChange.CallSign))
                                station.ImportCallSign = channelChange.CallSign;
                            if (channelChange.ChannelNumber != -1)
                                station.LogicalChannelNumber = channelChange.ChannelNumber;
                            station.ExcludedByUser = channelChange.Excluded;

                            found = true;
                        }
                    }

                    if (!found && OptionEntry.IsDefined(OptionName.DontCreateImportChannels))
                        station.ExcludedByUser = true;
                }

                if (channel.Icon != null)
                {
                    if (storeImagesLocally == ImportImageMode.Channels || storeImagesLocally == ImportImageMode.Both)                                            
                        station.ImagePath = storeImageLocally(channel.Icon.Source);
                    else
                        station.ImagePath = channel.Icon.Source;
                }

                stations.Add(station);

                Logger.Instance.Write("Created channel '" + station.Name + "' identity '" + channel.Id + "'");
            }

            Logger.Instance.Write("Created " + stations.Count + " channels from the xmltv data");

            return (stations);
        }

        private static void setStationId(TVStation station, XmltvChannel channel)
        {
            string[] parts = channel.Id.Split(new char[] { ':' });
            if (parts.Length != 4)
                return;

            try
            {
                int originalNetworkId = Int32.Parse(parts[0].Trim());
                int transportStreamId = Int32.Parse(parts[1].Trim());
                int serviceId = Int32.Parse(parts[2].Trim());

                station.OriginalNetworkID = originalNetworkId;
                station.TransportStreamID = transportStreamId;
                station.ServiceID = serviceId;
            }
            catch (FormatException) 
            {
                Logger.Instance.Write("Full channel ID format wrong for '" + channel.Id + "'");
            }
            catch (OverflowException) 
            {
                Logger.Instance.Write("Full channel ID too large for '" + channel.Id + "'");
            }
        }

        private static void setStationServiceId(TVStation station, XmltvChannel channel)
        {
            try
            {
                station.ServiceID = Int32.Parse(channel.Id.Trim());
            }
            catch (FormatException)
            {
                Logger.Instance.Write("Channel ID format wrong for '" + channel.Id + "'");
            }
            catch (OverflowException)
            {
                Logger.Instance.Write("Channel ID too large for '" + channel.Id + "'");
            }
        }

        private static void setStationZap2ItAtsc(TVStation station, XmltvChannel channel, int errorServiceId)
        {
            try
            {
                string[] idParts = channel.Id.Split(new char[] { '.' });
                if (idParts.Length < 4 || idParts.Length > 5 || !idParts[0].StartsWith("I"))
                {
                    station.ServiceID = errorServiceId;
                    return;
                }
                else
                {
                    station.OriginalNetworkID = 0;

                    if (idParts.Length == 4)
                    {
                        int channelNumber = Int32.Parse(idParts[0].Substring(1));
                                                
                        station.TransportStreamID = 0;
                        station.ServiceID = channelNumber;                        

                        if (station.LogicalChannelNumber == -1)
                            station.LogicalChannelNumber = channelNumber;

                        station.MinorChannelNumber = -1;
                    }
                    else
                    {
                        int majorChannelNumber = Int32.Parse(idParts[0].Substring(1));
                        int minorChannelNumber = Int32.Parse(idParts[1]);

                        station.OriginalNetworkID = 0;
                        station.TransportStreamID = majorChannelNumber;
                        station.ServiceID = minorChannelNumber;
                        
                        if (station.LogicalChannelNumber == -1)
                            station.LogicalChannelNumber = (majorChannelNumber * 100) + minorChannelNumber;

                        station.MinorChannelNumber = minorChannelNumber;
                    }
                }
            }
            catch (FormatException)
            {
                Logger.Instance.Write("Channel ID format for Zap2it ATSC wrong for '" + channel.Id + "'");
                station.ServiceID = errorServiceId;
            }
            catch (OverflowException)
            {
                Logger.Instance.Write("Channel ID format for Zap2it ATSC wrong for '" + channel.Id + "'");
                station.ServiceID = errorServiceId;
            }
        }

        private static void setStationLogicalChannelNumber(TVStation station, XmltvChannel channel)
        {
            try
            {
                station.LogicalChannelNumber = Int32.Parse(channel.Id.Trim());
            }
            catch (FormatException)
            {
                Logger.Instance.Write("Channel ID format wrong for '" + channel.Id + "'");
            }
            catch (OverflowException)
            {
                Logger.Instance.Write("Channel ID too large for '" + channel.Id + "'");
            }
        }

        private void processProgrammes(Collection<TVStation> stations, ImportFileSpec fileSpec)
        {
            if (XmltvProgramme.Programmes == null || XmltvProgramme.Programmes.Count == 0)
                return;

            int created = 0;
            int noStation = 0;

            foreach (XmltvProgramme programme in XmltvProgramme.Programmes)
            {
                TVStation station = findStation(stations, programme.Channel);
                if (station != null)
                {
                    processProgramme(station, programme, fileSpec);
                    created++;
                }
                else
                    noStation++;
            }

            Logger.Instance.Write("EPG entries created = " + created + " Xmltv programmes ignored = " + noStation);
        }

        private TVStation findStation(Collection<TVStation> stations, string channelID)
        {
            foreach (TVStation station in stations)
            {
                if (station.ProviderName == channelID)
                    return (station);
            }

            return (null);
        }

        private void processProgramme(TVStation station, XmltvProgramme programme, ImportFileSpec fileSpec)
        {
            if (programme.StartTime == null || programme.StopTime == null)
                return;

            if (station.EPGCollection == null)
                station.EPGCollection = new Collection<EPGEntry>();

            EPGEntry epgEntry = new EPGEntry();

            epgEntry.OriginalNetworkID = station.OriginalNetworkID;
            epgEntry.TransportStreamID = station.TransportStreamID;
            epgEntry.ServiceID = station.ServiceID;

            epgEntry.EventName = EditSpec.ProcessTitle(findLanguageString(programme.Titles, fileSpec.Language));

            string newPrefix = "";
            if (fileSpec.ProcessNewTag)
                newPrefix = programme.New ? "New. " : "";

            string livePrefix = "";
            if (fileSpec.ProcessLiveTag)
                livePrefix = programme.Live ? "Live. " : "";

            if (programme.Descriptions != null && programme.Descriptions.Count != 0)
                epgEntry.ShortDescription = newPrefix + livePrefix + EditSpec.ProcessDescription(findLanguageString(programme.Descriptions, fileSpec.Language));
            else
                epgEntry.ShortDescription = newPrefix + livePrefix + RunParameters.NoProgrammeDescription;

            if (programme.SubTitles != null && programme.SubTitles.Count != 0)
                epgEntry.EventSubTitle = findLanguageString(programme.SubTitles, fileSpec.Language);

            if (string.IsNullOrWhiteSpace(fileSpec.TimeZone))
            {
                epgEntry.StartTime = (programme.StartTime.Time.Value - programme.StartTime.Offset.Value).ToLocalTime();
                epgEntry.Duration = (programme.StopTime.Time.Value - programme.StopTime.Offset.Value).ToLocalTime() - epgEntry.StartTime;
            }
            else
            {
                epgEntry.StartTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(programme.StartTime.Time.Value, fileSpec.TimeZone, TimeZoneInfo.Local.Id);
                DateTime stopTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(programme.StopTime.Time.Value, fileSpec.TimeZone, TimeZoneInfo.Local.Id);
                epgEntry.Duration = stopTime - epgEntry.StartTime;
            }

            if (programme.StartTime.Offset != null && programme.StopTime.Offset != null)
            {
                TimeSpan dstAdjustment = programme.StartTime.Offset.Value - programme.StopTime.Offset.Value;
                if (dstAdjustment.Ticks != 0)
                {
                    epgEntry.Duration += dstAdjustment;
                    dstAdjustmentCount++;
                }
            }

            epgEntry.Date = programme.Date;
            epgEntry.EventCategory = getEventCategory(programme.Categories, fileSpec, epgEntry.EventName, epgEntry.ShortDescription);            

            if (programme.Rating != null)
            {
                epgEntry.ParentalRating = programme.Rating.Value;
                epgEntry.ParentalRatingSystem = programme.Rating.System;
            }

            if (programme.Video != null)
            {
                epgEntry.VideoQuality = programme.Video.Quality;
                epgEntry.AspectRatio = programme.Video.Aspect;
            }

            if (programme.Audio != null)
                epgEntry.AudioQuality = programme.Audio.Stereo;

            if (programme.StarRating != null)
                epgEntry.StarRating = getStarRating(programme.StarRating.Value);

            if (programme.Subtitlings != null && programme.Subtitlings.Count != 0)
                epgEntry.SubTitles = findSubtitlingsLanguage(programme.Subtitlings, fileSpec.Language);

            if (programme.PreviouslyShown != null)
            {
                if (programme.PreviouslyShown.Start != null && programme.PreviouslyShown.Start.Time != null)
                    epgEntry.PreviousPlayDate = (programme.PreviouslyShown.Start.Time.Value - programme.PreviouslyShown.Start.Offset.Value);
                else
                {
                    if (fileSpec.SetPreviouslyShownDefault)
                        epgEntry.PreviousPlayDate = defaultPlayDate;
                }
            }

            processEpisodeTags(epgEntry, programme, fileSpec.IgnoreEpisodeTags);

            if (programme.Directors != null && programme.Directors.Count != 0)
            {
                epgEntry.Directors = new Collection<Person>();
                foreach (XmltvPerson director in programme.Directors)
                    epgEntry.Directors.Add(new Person(director.Name, null, null, epgEntry.Directors.Count + 1));
            }

            if (programme.Actors != null && programme.Actors.Count != 0)
            {
                epgEntry.Cast = new Collection<Person>();
                foreach (XmltvPerson actor in programme.Actors)
                    epgEntry.Cast.Add(new Person(actor.Name, null, actor.Role, epgEntry.Cast.Count + 1));
            }

            if (programme.Guests != null && programme.Guests.Count != 0)
            {
                epgEntry.GuestStars = new Collection<Person>();
                foreach (XmltvPerson guest in programme.Guests)
                    epgEntry.GuestStars.Add(new Person(guest.Name, null, null, epgEntry.GuestStars.Count + 1));
            }

            if (programme.Presenters != null && programme.Presenters.Count != 0)
            {
                epgEntry.Presenters = new Collection<Person>();
                foreach (XmltvPerson presenter in programme.Presenters)
                    epgEntry.Presenters.Add(new Person(presenter.Name, null, null, epgEntry.Presenters.Count + 1));
            }

            if (programme.Producers != null && programme.Producers.Count != 0)
            {
                epgEntry.Producers = new Collection<Person>();
                foreach (XmltvPerson producer in programme.Producers)
                    epgEntry.Producers.Add(new Person(producer.Name, null, null, epgEntry.Producers.Count + 1));
            }

            if (programme.Writers != null && programme.Writers.Count != 0)
            {
                epgEntry.Writers = new Collection<Person>();
                foreach (XmltvPerson writer in programme.Writers)
                    epgEntry.Writers.Add(new Person(writer.Name, null, null, epgEntry.Writers.Count + 1));
            }

            if (programme.Icon != null)
            {
                if (fileSpec.StoreImagesLocally == ImportImageMode.Programmes || fileSpec.StoreImagesLocally == ImportImageMode.Both)
                    epgEntry.PosterPath = storeImageLocally(programme.Icon.Source);
                else
                    epgEntry.PosterPath = programme.Icon.Source;

                epgEntry.PosterHeight = programme.Icon.Height;
                epgEntry.PosterWidth = programme.Icon.Width;
            }

            if (!epgEntry.IsNew)
                epgEntry.IsNew = programme.New;
            epgEntry.IsLive = programme.Live;

            checkIfSeries(epgEntry, programme.Categories, fileSpec);
            checkIfMiniseries(epgEntry, programme.Categories, fileSpec);
            checkIfMovie(epgEntry, programme.Categories, fileSpec);

            if (hasSeriesCategory)
                epgEntry.IsGeneric = (epgEntry.IdPrefix != null && epgEntry.IdPrefix == "SH") &&
                    epgEntry.IsSeries && !epgEntry.IsMiniseries &&
                    epgEntry.SeriesId != "00000001";
            else
                epgEntry.IsGeneric = (epgEntry.IdPrefix != null && epgEntry.IdPrefix == "SH") &&
                    epgEntry.SeriesId != "00000001";

            if (epgEntry.IsGeneric)
                genericCount++;

            if (fileSpec.Language != null)
                epgEntry.LanguageCode = fileSpec.Language.Code;

            epgEntry.NoLookup = fileSpec.NoLookup;

            addEPGEntry(station, epgEntry);            
        }

        private void checkIfSeries(EPGEntry epgEntry, Collection<XmltvText> categories, ImportFileSpec fileSpec)
        {
            if (epgEntry.IsSeries || categories == null)
                return;

            foreach (XmltvText category in categories)
            {
                string languageText = checkCategoryLanguage(category, fileSpec.Language);
                if (languageText != null)
                {
                    if (languageText.ToLowerInvariant() == "series")
                    {
                        epgEntry.IsSeries = true;
                        seriesCount++;
                        return;
                    }
                }
            }
        }

        private void checkIfMiniseries(EPGEntry epgEntry, Collection<XmltvText> categories, ImportFileSpec fileSpec)
        {
            if (epgEntry.IsMiniseries || categories == null)
                return;

            foreach (XmltvText category in categories)
            {
                string languageText = checkCategoryLanguage(category, fileSpec.Language);
                if (languageText != null)
                {
                    if (languageText.ToLowerInvariant() == "miniseries")
                    {
                        epgEntry.IsMiniseries = true;
                        miniseriesCount++;
                        return;
                    }
                }
            }
        }

        private void checkIfMovie(EPGEntry epgEntry, Collection<XmltvText> categories, ImportFileSpec fileSpec)
        {
            if (epgEntry.IsMovie || categories == null)
                return;

            foreach (XmltvText category in categories)
            {
                string languageText = checkCategoryLanguage(category, fileSpec.Language);
                if (languageText != null)
                {
                    if (languageText.ToLowerInvariant() == "movie")
                    {
                        epgEntry.IsMovie = true;
                        movieCount++;
                        return;
                    }
                }
            }
        }

        private string getStarRating(string rating)
        {
            string[] parts = rating.Trim().Split(new char[] { '/' });
            if (parts.Length != 2)
                return (null);

            int ratingValue = 0;
            int ratingMax = 0;

            try
            {
                ratingValue = Int32.Parse(parts[0].Trim());
                ratingMax = Int32.Parse(parts[1].Trim());
            }
            catch (FormatException)
            {
                return (null);
            }
            catch (OverflowException)
            {
                return (null);
            }

            int adjustedRating = ratingValue * (8 / ratingMax);

            switch (adjustedRating)
            {
                case 0:
                    return (null);
                case 1:
                    return ("+");
                case 2:
                    return ("*");
                case 3:
                    return ("*+");
                case 4:
                    return ("**");
                case 5:
                    return ("**+");
                case 6:
                    return ("***");
                case 7:
                    return ("***+");
                case 8:
                    return ("****");
                default:
                    return ("****");
            }
        }

        private EventCategorySpec getEventCategory(Collection<XmltvText> categories, ImportFileSpec fileSpec, string eventName, string description)
        {
            if (categories == null || categories.Count == 0)
                return null;

            Collection<string> genres = new Collection<string>();
            
            foreach (XmltvText programCategory in categories)
            {
                string languageText = checkCategoryLanguage(programCategory, fileSpec.Language);
                if (languageText != null)
                    genres.Add(languageText);                              
            }

            if (genres.Count == 0)
            {
                foreach (XmltvText programCategory in categories)
                {
                    string languageText = checkCategoryLanguage(programCategory, "en");
                    if (languageText != null)
                        genres.Add(languageText);
                }
            }

            if (genres.Count == 0)
                return null;

            ProgramCategory category = XmltvProgramCategory.FindCategory(genres[0]);     
            if (category != null)
            {                
                category.UsedCount++;
                category.SampleEvent = eventName;
                return new EventCategorySpec(category.AddToDescriptions(genres, 1));                
            }
                    
            XmltvProgramCategory.AddUndefinedCategory(genres[0], eventName);

            ProgramCategory customCategory = CustomProgramCategory.FindCategory(eventName);
            if (customCategory != null)
                return new EventCategorySpec(customCategory);
                        
            customCategory = CustomProgramCategory.FindCategory(description);
            if (customCategory != null)
                return new EventCategorySpec(customCategory);
                            
            string updatedCategory = genres[0];
            for (int index = 1; index < genres.Count; index++)
                updatedCategory += "," + genres[index];
            
            return new EventCategorySpec(updatedCategory);
        }

        private void processEpisodeTags(EPGEntry epgEntry, XmltvProgramme programme, bool ignoreTag)
        {
            if (programme.EpisodeNumbers == null)
                return;

            foreach (XmltvEpisodeNumber episodeNumber in programme.EpisodeNumbers)
            {
                if (episodeNumber.System == "xmltv_ns")
                {
                    epgEntry.EpisodeSystemType = episodeNumber.System;

                    string[] parts = episodeNumber.Episode.Split(new char[] { '.' });
                    epgEntry.SeasonNumber = getEpisodeData(parts[0]);
                    if (parts.Length > 1)
                    {
                        epgEntry.EpisodeNumber = getEpisodeData(parts[1]);
                        if (parts.Length > 2)
                            epgEntry.PartNumber = parts[2].Trim();
                    }

                    epgEntry.AddSeriesEpisodeToDescription();
                }
                else
                {
                    if (episodeNumber.System == "dd_progid")
                    {
                        if (epgEntry.EpisodeSystemType == null)
                            epgEntry.EpisodeSystemType = episodeNumber.System;
                        string[] parts = episodeNumber.Episode.Split(new char[] { '.' });

                        epgEntry.IdPrefix = parts[0].Substring(0, 2);                        
                        epgEntry.SeriesId = parts[0].Substring(2).Trim();
                        epgEntry.EpisodeId = parts[1].Trim();
                        epgEntry.IsNew = programme.New;
                        
                        if (epgEntry.IdPrefix == "EP")
                        {
                            if (parts[1] == "0000")
                                Logger.Instance.Write("<w> Unexpected episode-num format: " + episodeNumber.Episode);
                            epgEntry.IsSeries = true;
                        }
                        else
                        {
                            if (epgEntry.IdPrefix == "SH" || epgEntry.IdPrefix =="MV")
                            {
                                if (parts[1] != "0000")
                                    Logger.Instance.Write("<w> Unexpected episode-num format: " + episodeNumber.Episode);
                            }
                        }

                        epgEntry.UseBase64Crids = false;
                    }
                    else
                    {
                        if (episodeNumber.System == "bsepg-epid")
                        {
                            if (epgEntry.EpisodeSystemType == null)
                                epgEntry.EpisodeSystemType = episodeNumber.System;
                            string[] parts = episodeNumber.Episode.Split(new char[] { '.' });
                            epgEntry.SeriesId = parts[0].Trim();
                            epgEntry.EpisodeId = parts[1].Trim();
                        }
                        else
                        {
                            if (!ignoreTag)
                            {
                                epgEntry.EpisodeSystemType = programme.EpisodeNumbers[0].System;
                                epgEntry.EpisodeTag = programme.EpisodeNumbers[0].Episode;
                            }
                        }
                    }
                }
            }            
        }

        private int getEpisodeData(string part)
        {
            string[] parts = part.Split(new char[] { '/' });

            try
            {
                return (Int32.Parse(parts[0]) + 1);
            }
            catch (FormatException)
            {
                return (-1);
            }
            catch (OverflowException)
            {
                return (-1);
            }
        }

        private string storeImageLocally(string url)
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

                    WebRequestSpec webRequest = new WebRequestSpec(url, streamLogger, "XMLTV Import");
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

        private void addEPGEntry(TVStation station, EPGEntry newEntry)
        {
            if (OptionEntry.IsDefined(OptionName.NoInvalidEntries))
            {
                if (newEntry.Duration.TotalSeconds < 1)
                {
                    invalidTimes++;
                    return;
                }
            }

            foreach (EPGEntry oldEntry in station.EPGCollection)
            {
                if (oldEntry.StartTime == newEntry.StartTime)
                    return;

                if (oldEntry.StartTime.CompareTo(newEntry.StartTime) > 0)
                {
                    station.EPGCollection.Insert(station.EPGCollection.IndexOf(oldEntry), newEntry);
                    return;
                }
            }

            station.EPGCollection.Add(newEntry);
        }

        private static string findLanguageString(Collection<XmltvText> languageStrings, LanguageCode languageCode)
        {
            if (languageCode == null)
            {
                foreach (XmltvText textString in languageStrings)
                {
                    if (textString.Language == null)
                        return (textString.Text);
                }

                return (languageStrings[0].Text);
            }

            foreach (XmltvText textString in languageStrings)
            {
                if (textString.Language != null && textString.Language == languageCode.TranslationCode)
                    return (textString.Text);
            }

            if (languageCode.TranslationCode != "en")
            {
                foreach (XmltvText textString in languageStrings)
                {
                    if (textString.Language == "en")
                        return (textString.Text);
                }
            }

            foreach (XmltvText textString in languageStrings)
            {
                if (textString.Language == null)
                    return (textString.Text);
            }

            return (languageStrings[0].Text);
        }

        private string checkCategoryLanguage(XmltvText category, LanguageCode languageCode)
        {
            if (category.Language == null || languageCode == null)
                return (category.Text);

            if (category.Language == languageCode.TranslationCode)
                return (category.Text);

            return (null);
        }

        private string checkCategoryLanguage(XmltvText category, string languageCode)
        {
            if (category.Language == null || languageCode == null)
                return (category.Text);

            if (category.Language == languageCode)
                return (category.Text);

            return (null);
        }

        private string findSubtitlingsLanguage(Collection<XmltvSubtitling> subtitlings, LanguageCode languageCode)
        {
            if (languageCode == null)
            {
                foreach (XmltvSubtitling subtitling in subtitlings)
                {
                    if (subtitling.Language == null)
                        return (subtitling.Type);
                }

                return (subtitlings[0].Type);
            }

            foreach (XmltvSubtitling subtitling in subtitlings)
            {
                if (subtitling.Language != null && subtitling.Language == languageCode.TranslationCode)
                    return (subtitling.Type);
            }

            if (languageCode.TranslationCode != "en")
            {
                foreach (XmltvSubtitling subtitling in subtitlings)
                {
                    if (subtitling.Language == "en")
                        return (subtitling.Type);
                }
            }

            foreach (XmltvSubtitling subtitling in subtitlings)
            {
                if (subtitling.Language == null)
                    return (subtitling.Type);
            }

            if (subtitlings.Count > 0)
                return subtitlings[0].Type;

            return (null);
        }

        /// <summary>
        /// Process the channel information from an XMLTV file.
        /// </summary>
        /// <param name="fileName">The actual file path.</param>
        /// <param name="fileSpec">The file definition.</param>
        /// <returns>An error message or null if the file was processed successfully.</returns>
        public override string ProcessChannels(string fileName, ImportFileSpec fileSpec)
        {
            XmlReader xmlReader = null;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.DtdProcessing = DtdProcessing.Ignore;
            settings.CheckCharacters = false;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            
            try
            {
                xmlReader = XmlReader.Create(fileName, settings);
            }
            catch (IOException)
            {
                return("Failed to open " + fileName);
            }

            try
            {
                while (!xmlReader.EOF)
                {
                    xmlReader.Read();
                    if (xmlReader.IsStartElement())
                    {
                        switch (xmlReader.Name)
                        {
                            case "channel":
                                XmltvChannel channel = XmltvChannel.GetInstance(xmlReader.ReadSubtree());

                                if (XmltvChannel.Channels == null)
                                    XmltvChannel.Channels = new Collection<XmltvChannel>();

                                channel.IdFormat = fileSpec.IdFormat;
                                XmltvChannel.Channels.Add(channel);
                                
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (XmlException e)
            {
                return ("Failed to load xmltv file: " + e.Message);                
            }
            catch (IOException e)
            {
                return ("Failed to load xmltv file: " + e.Message);                
            }

            if (xmlReader != null)
                xmlReader.Close();

            return (null);
        }

        /// <summary>
        /// Clear the XMLTV data.
        /// </summary>
        public static void Clear()
        {
            if (XmltvChannel.Channels != null)
                XmltvChannel.Channels.Clear();

            if (XmltvProgramme.Programmes != null)
                XmltvProgramme.Programmes.Clear();
        }

        /// <summary>
        /// Log the import statistics.
        /// </summary>
        public override void LogStats()
        {
            Logger.Instance.Write("Images added = " + imagesAdded);
            Logger.Instance.Write("Images deleted= " + imagesDeleted);
            Logger.Instance.Write("Total images = " + imagesStored.Count);
            Logger.Instance.Write("Download errors = " + imageDownloadErrors);
            Logger.Instance.Write("");
            Logger.Instance.Write("Series category detected = " + (hasSeriesCategory ? "yes" : "no"));
            Logger.Instance.Write("");
            Logger.Instance.Write("Total series = " + seriesCount);
            Logger.Instance.Write("Total miniseries = " + miniseriesCount);
            Logger.Instance.Write("Total movies = " +movieCount);
            Logger.Instance.Write("");
            Logger.Instance.Write("Total generic programmes = " + genericCount);
            Logger.Instance.Write("");
            Logger.Instance.Write("Total DST adjustments = " + dstAdjustmentCount);

            XmltvProgramCategory.LogUsage(XmltvProgramCategory.Categories, XmltvProgramCategory.UndefinedCategories, "XMLTV");            
        }
    }
}

