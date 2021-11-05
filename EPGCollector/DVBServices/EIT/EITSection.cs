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

using DomainObjects;

namespace DVBServices
{
    /// <summary>
    /// The class that describes an EIT section.
    /// </summary>
    public class EITSection
    {
        /// <summary>
        /// Get the collection of EIT entries in the section.
        /// </summary>
        public Collection<EITEntry> EITEntries { get { return (eitEntries); } }

        /// <summary>
        /// Get the collection of EIT category records.
        /// </summary>
        public static Collection<CategoryEntry> CategoryEntries { get { return (categoryEntries); } }

        /// <summary>
        /// Get the original network identification (ONID).
        /// </summary>
        public int OriginalNetworkID { get { return (originalNetworkID); } }
        /// <summary>
        /// Get the transport stream identification (TSID).
        /// </summary>
        public int TransportStreamID { get { return (transportStreamID); } }
        /// <summary>
        /// Get the service identification (SID).
        /// </summary>
        public int ServiceID { get { return (serviceID); } }

        /// <summary>
        /// Get the identification of the last table for the EIT section.
        /// </summary>
        public int LastTableID { get { return (lastTableID); } }
        /// <summary>
        /// Get the segment last section number for the EIT section.
        /// </summary>
        public int SegmentLastSectionNumber { get { return (segmentLastSectionNumber); } }        

        private Collection<EITEntry> eitEntries;

        private int transportStreamID;
        private int originalNetworkID;
        private int serviceID;

        private int segmentLastSectionNumber;
        private int lastTableID;

        private static Logger titleLogger;
        private static Logger descriptionLogger;

        private static Collection<CategoryEntry> categoryEntries;

        private int lastIndex;

        /// <summary>
        /// Initialize a new instance of the EITSection class.
        /// </summary>
        public EITSection()
        {
            eitEntries = new Collection<EITEntry>();
            Logger.ProtocolIndent = "";

            if (DebugEntry.IsDefined(DebugName.LogTitles) && titleLogger == null)
                titleLogger = new Logger("EPG Titles.log");
            if (DebugEntry.IsDefined(DebugName.LogDescriptions)&& descriptionLogger == null)
                descriptionLogger = new Logger("EPG Descriptions.log");
        }

        /// <summary>
        /// Parse the section.
        /// </summary>
        /// <param name="byteData">The MPEG2 section containing the section.</param>
        /// <param name="mpeg2Header">The MPEG2 header that preceedes the section.</param>
        public void Process(byte[] byteData, Mpeg2ExtendedHeader mpeg2Header)
        {
            lastIndex = mpeg2Header.Index;
            serviceID = mpeg2Header.TableIDExtension;

            try
            {
                transportStreamID = Utils.Convert2BytesToInt(byteData, lastIndex);
                lastIndex += 2;

                originalNetworkID = Utils.Convert2BytesToInt(byteData, lastIndex);
                lastIndex += 2;

                segmentLastSectionNumber = (int)byteData[lastIndex];
                lastIndex++;

                lastTableID = (int)byteData[lastIndex];
                lastIndex++;
            }
            catch (IndexOutOfRangeException)
            {
                throw (new ArgumentOutOfRangeException("EIT section is short"));
            }

            bool addStationNeeded = false;

            TVStation tvStation;
            if (!OptionEntry.IsDefined(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.Options, OptionName.SidMatchOnly))
                tvStation = TVStation.FindStation(RunParameters.Instance.StationCollection, originalNetworkID, transportStreamID, serviceID);
            else
                tvStation = TVStation.FindStation(RunParameters.Instance.StationCollection, serviceID);
            if (tvStation == null)
            {
                if (!OptionEntry.IsDefined(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.Options, OptionName.CreateMissingChannels))
                    return;
                else
                {
                    tvStation = new TVStation("Auto Generated: " + originalNetworkID + ":" + transportStreamID + ":" + serviceID);
                    tvStation.OriginalNetworkID = originalNetworkID;
                    tvStation.TransportStreamID = transportStreamID;
                    tvStation.ServiceID = serviceID;

                    addStationNeeded = true;
                }
            }

            bool newSection = tvStation.AddMapEntry(mpeg2Header.TableID, mpeg2Header.SectionNumber, lastTableID, mpeg2Header.LastSectionNumber, segmentLastSectionNumber);
            if (!newSection)
                return;

            while (lastIndex < byteData.Length - 4)
            {
                EITEntry eitEntry = new EITEntry();
                eitEntry.Process(byteData, lastIndex);

                if (eitEntry.StartTime != DateTime.MinValue)
                {
                    EPGEntry epgEntry = new EPGEntry();
                    epgEntry.OriginalNetworkID = tvStation.OriginalNetworkID;
                    epgEntry.TransportStreamID = tvStation.TransportStreamID;
                    epgEntry.ServiceID = tvStation.ServiceID;
                    epgEntry.EPGSource = EPGSource.EIT;

                    switch (eitEntry.ComponentTypeAudio)
                    {
                        case 3:
                            epgEntry.AudioQuality = "stereo";
                            break;
                        case 5:
                            epgEntry.AudioQuality = "dolby digital";
                            break;
                        default:
                            break;
                    }

                    if (eitEntry.ComponentTypeVideo > 9)
                        epgEntry.VideoQuality = "HDTV";

                    if (OptionEntry.IsDefined(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.Options, OptionName.UseDescAsCategory))
                        epgEntry.EventCategory = new EventCategorySpec(EditSpec.ProcessDescription(eitEntry.ShortDescription));
                    else
                        epgEntry.EventCategory = getEventCategory(eitEntry.EventName, EditSpec.ProcessDescription(eitEntry.Description), eitEntry.ContentType, eitEntry.ContentSubType);                        
                    
                    epgEntry.Duration = Utils.RoundTime(eitEntry.Duration);
                    epgEntry.EventID = eitEntry.EventID;
                    epgEntry.EventName = EditSpec.ProcessTitle(eitEntry.EventName);

                    if (RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.CountryCode != null)
                    {
                        epgEntry.ParentalRating = ParentalRating.FindRating(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.CountryCode, "EIT", (eitEntry.ParentalRating + 3).ToString());
                        epgEntry.MpaaParentalRating = ParentalRating.FindMpaaRating(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.CountryCode, "EIT", (eitEntry.ParentalRating + 3).ToString());
                    }
                    else
                    {
                        if (eitEntry.ParentalRating > 11)
                        {
                            epgEntry.ParentalRating = "AO";
                            epgEntry.MpaaParentalRating = "AO";
                        }
                        else
                        {
                            if (eitEntry.ParentalRating > 8)
                            {
                                epgEntry.ParentalRating = "PGR";
                                epgEntry.MpaaParentalRating = "PG";
                            }
                            else
                            {
                                epgEntry.ParentalRating = "G";
                                epgEntry.MpaaParentalRating = "G";
                            }
                        }
                    }

                    epgEntry.RunningStatus = eitEntry.RunningStatus;
                    epgEntry.Scrambled = eitEntry.Scrambled;

                    try
                    {
                        if (OptionEntry.IsDefined(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.Options, OptionName.UseDescAsCategory))
                            epgEntry.ShortDescription = EditSpec.ProcessDescription(eitEntry.ExtendedDescription);
                        else
                        {
                            if (OptionEntry.IsDefined(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.Options, OptionName.UseDescAsSubtitle))
                            {
                                epgEntry.ShortDescription = EditSpec.ProcessDescription(eitEntry.ExtendedDescription);
                                epgEntry.EventSubTitle = EditSpec.ProcessDescription(eitEntry.ShortDescription);
                            }
                            else
                            {
                                if (OptionEntry.IsDefined(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.Options, OptionName.UseNoDesc))
                                {
                                    if (string.IsNullOrWhiteSpace(eitEntry.ExtendedDescription))
                                        epgEntry.ShortDescription = EditSpec.ProcessDescription(eitEntry.ShortDescription);
                                    else
                                        epgEntry.ShortDescription = EditSpec.ProcessDescription(eitEntry.ExtendedDescription);
                                }
                                else
                                {
                                    string description = getCombinedTitleDescription(epgEntry, eitEntry.Description);
                                    epgEntry.ShortDescription = getShortDescription(EditSpec.ProcessDescription(description));
                                    epgEntry.EventSubTitle = getSubTitle(EditSpec.ProcessDescription(description));
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.Write("<E> An exception of type " + e.GetType().Name + " has occurred while processing a programme description");
                        Logger.Instance.Write("<E> " + e.Message);
                    }

                    if (!string.IsNullOrWhiteSpace(epgEntry.ShortDescription))
                    {
                        if (epgEntry.ShortDescription.StartsWith("'") && epgEntry.ShortDescription.EndsWith("'"))
                            epgEntry.ShortDescription = epgEntry.ShortDescription.Substring(1, epgEntry.ShortDescription.Length - 2);
                    }

                    epgEntry.StartTime = Utils.RoundTime(TimeOffsetEntry.GetOffsetTime(eitEntry.StartTime));
                    epgEntry.LanguageCode = eitEntry.LanguageCode;

                    if (eitEntry.Cast != null && eitEntry.Cast.Count != 0)
                    {
                        epgEntry.Cast = new Collection<Person>();
                        foreach (string castMember in eitEntry.Cast)
                            epgEntry.Cast.Add(new Person(castMember, "Actor"));
                    }

                    if (eitEntry.Producers != null && eitEntry.Producers.Count != 0)
                    {
                        epgEntry.Producers = new Collection<Person>();
                        foreach (string producer in eitEntry.Producers)
                            epgEntry.Producers.Add(new Person(producer, "Producer"));
                    }

                    if (eitEntry.Directors != null && eitEntry.Directors.Count != 0)
                    {
                        epgEntry.Directors = new Collection<Person>();
                        foreach (string director in eitEntry.Directors)
                            epgEntry.Directors.Add(new Person(director, "Director"));
                    }

                    if (eitEntry.Writers != null && eitEntry.Writers.Count != 0)
                    {
                        epgEntry.Writers = new Collection<Person>();
                        foreach (string writer in eitEntry.Writers)
                            epgEntry.Writers.Add(new Person(writer, "Writer"));
                    }                    
                    
                    epgEntry.Date = getDate(eitEntry, epgEntry);
                    if (eitEntry.TVRating != null)
                        epgEntry.ParentalRating = eitEntry.TVRating;
                    epgEntry.StarRating = eitEntry.StarRating;

                    if (eitEntry.TVRating != null)
                        epgEntry.ParentalRating = eitEntry.TVRating;

                    if (epgEntry.EventSubTitle == null)
                        epgEntry.EventSubTitle = eitEntry.Subtitle;

                    bool sePresent = false;

                    try
                    {
                        getSeriesEpisodeIds(epgEntry, eitEntry);
                        sePresent = getSeasonEpisodeNumbers(epgEntry, eitEntry);                            
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.Write("<E> An exception of type " + e.GetType().Name + " has occurred while extracting sesonepisode numbers");
                        Logger.Instance.Write("<E> " + e.Message);
                    }

                    if (!sePresent)
                        epgEntry.AddSeriesEpisodeToDescription();

                    epgEntry.Country = eitEntry.Country;

                    bool include = ControllerBase.CheckEPGDays(epgEntry.StartTime);
                    if (include)
                    {
                        tvStation.AddEPGEntry(epgEntry);

                        if (titleLogger != null)
                            logTitle(eitEntry.EventName, eitEntry, epgEntry, titleLogger);
                        if (descriptionLogger != null)
                            logDescription(eitEntry.Description, eitEntry, epgEntry, descriptionLogger);

                        if (DebugEntry.IsDefined(DebugName.CatXref))
                            updateCategoryEntries(tvStation, eitEntry);
                    }
                }

                lastIndex = eitEntry.Index;
            }

            if (addStationNeeded)
                RunParameters.Instance.StationCollection.Add(tvStation);
        }

        private string getCombinedTitleDescription(EPGEntry epgEntry, string description)
        {
            if (description == null || epgEntry.EventName == null)
                return (null);

            string editedDescription;

            if (RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.CountryCode == Country.Netherlands)
                return description;

            if (epgEntry.EventName.EndsWith("...") && description.StartsWith("...") &&
                    epgEntry.EventName.Length > 3 && description.Length > 3)
            {
                int fullStopIndex = description.IndexOf('.', 3);
                int colonIndex = description.IndexOf(':', 3);

                int index = fullStopIndex;
                if (index == -1 || (colonIndex != -1 && colonIndex < index))
                    index = colonIndex;

                if (index != -1)
                {
                    epgEntry.EventName = epgEntry.EventName.Substring(0, epgEntry.EventName.Length - 3) + " " +
                        description.Substring(3, index - 3);
                    editedDescription = description.Substring(index + 1).Trim();
                }
                else
                    editedDescription = description;
            }
            else
                editedDescription = description;

            return (editedDescription);
        }

        private string getShortDescription(string description)
        {
            if (description == null)
                return (null);

            if (RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.CountryCode == Country.Netherlands)
                return description;

            if (description.Length > 0 && description[0] == '(')
                return (description);

            int index = description.IndexOf(':');
            if (index < 5 || index > 60)
                index = -1;

            return (index == -1 ? description : description.Substring(index + 1).Trim());
        }

        private string getSubTitle(string description)
        {
            if (description == null || (description.Length > 0 && description[0] == '('))
                return (null);

            if (RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.CountryCode == Country.Netherlands)
                return null;

            int index = description.IndexOf(':');
            if (index < 5 ||index > 60)
                index = -1;

            return (index < 1 ? null : description.Substring(0, index).Trim());
        }

        private bool getSeasonEpisodeNumbers(EPGEntry epgEntry, EITEntry eitEntry)
        {
            bool sePresent = false;

            switch (RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.CountryCode)
            {
                case Country.NewZealand:
                    sePresent = Utils.GetNZLSeasonEpisodeNumbers(epgEntry);
                    break;
                case Country.Egypt:
                    sePresent = getEgyptSeasonEpisodeNumbers(epgEntry);
                    break;
                case Country.UnitedKingdom:
                    sePresent = getGBRSeasonEpisodeNumbers(epgEntry);
                    break;
                case Country.Netherlands:
                    break;
                default:
                    sePresent = getOtherSeasonEpisodeNumbers(epgEntry);
                    break;
            }

            if (epgEntry.SeasonNumber == -1)
                epgEntry.SeasonNumber = eitEntry.SeasonNumber; 
            if (epgEntry.EpisodeNumber == -1)
                epgEntry.EpisodeNumber = eitEntry.EpisodeNumber;
            if (epgEntry.EpisodeCount == -1)
                epgEntry.EpisodeCount = eitEntry.EpisodeCount;
            

            return sePresent;
        }

        private bool getOtherSeasonEpisodeNumbers(EPGEntry epgEntry)
        {
            if (epgEntry.ShortDescription == null || !epgEntry.ShortDescription.StartsWith("("))
                return false;

            int series = 0;
            int episode = 0;
            int index = (epgEntry.ShortDescription.StartsWith("(Ep. ") ? 5 : 1);
            
            while (index < epgEntry.ShortDescription.Length && epgEntry.ShortDescription[index] >= '0' && epgEntry.ShortDescription[index] <= '9')
            {
                episode = (episode * 10) + (epgEntry.ShortDescription[index] - '0');
                index++;
            }

            if (index == epgEntry.ShortDescription.Length)
                return false;

            if (epgEntry.ShortDescription[index] != ')')
            {
                if (index + 1 == epgEntry.ShortDescription.Length)
                    return false;

                if (epgEntry.ShortDescription[index] == ':')
                {
                    index++;

                    int parts = 0;

                    while (index < epgEntry.ShortDescription.Length && epgEntry.ShortDescription[index] >= '0' && epgEntry.ShortDescription[index] <= '9')
                    {
                        parts = (parts * 10) + (epgEntry.ShortDescription[index] - '0');
                        index++;
                    }

                    if (index == epgEntry.ShortDescription.Length)
                        return false;

                    if (epgEntry.ShortDescription[index] != '/' || index + 1 == epgEntry.ShortDescription.Length || epgEntry.ShortDescription[index + 1] != 's')
                        return false;
                }
                else
                {
                    if (epgEntry.ShortDescription[index] != '/' || index + 1 == epgEntry.ShortDescription.Length || epgEntry.ShortDescription[index + 1] != 's')
                        return false;
                }

                index += 2;

                if (index >= epgEntry.ShortDescription.Length)
                    return false;

                while (index < epgEntry.ShortDescription.Length && epgEntry.ShortDescription[index] >= '0' && epgEntry.ShortDescription[index] <= '9')
                {
                    series = (series * 10) + (epgEntry.ShortDescription[index] - '0');
                    index++;
                }

                if (index == epgEntry.ShortDescription.Length)
                    return false;

                if (epgEntry.ShortDescription[index] != ')')
                    return false;
            }

            epgEntry.SeasonNumber = series;
            epgEntry.EpisodeNumber = episode;

            if (!OptionEntry.IsDefined(OptionName.NoRemoveData))
            {
                if (index + 1 < epgEntry.ShortDescription.Length)
                    epgEntry.ShortDescription = epgEntry.ShortDescription.Substring(index + 1).TrimStart();
                else
                    epgEntry.ShortDescription = null;

                return false;
            }

            return true;
        }

        private bool getEgyptSeasonEpisodeNumbers(EPGEntry epgEntry)
        {
            string seasonId = "Season ";
            string episodeId = "Episode ";

            string description = null;
            bool inDescription = true;

            if (epgEntry.ShortDescription != null && (epgEntry.ShortDescription.StartsWith(seasonId) || epgEntry.ShortDescription.StartsWith(episodeId)))
                description = epgEntry.ShortDescription;
            else
            {
                if (epgEntry.EventSubTitle != null && (epgEntry.EventSubTitle.StartsWith(seasonId) || epgEntry.EventSubTitle.StartsWith(episodeId)))
                {
                    description = epgEntry.EventSubTitle;
                    inDescription = false;
                }
                else
                    return false;
            }

            int season = -1;
            int episode = -1;
            int index;
            bool episodePresent = true;

            if (description.StartsWith(seasonId))
            {
                index = seasonId.Length;
                season = 0;

                while (index < description.Length && char.IsDigit(description[index]))
                {
                    season = (season * 10) + (description[index] - '0');
                    index++;
                }

                if (index < description.Length && description[index] == ',')
                    index++;
                if (index < description.Length && description[index] == ' ')
                    index++;

                if (index + episodeId.Length < description.Length)
                {
                    if (description.Substring(index, episodeId.Length) == episodeId)
                        index += episodeId.Length;
                    else
                        episodePresent = false;
                }
            }
            else
            {
                index = episodeId.Length;
                if (description[index] == ' ')
                    index++;
            }

            if (episodePresent)
            {
                episode = 0;

                while (index < description.Length && char.IsDigit(description[index]))
                {
                    episode = (episode * 10) + (description[index] - '0');
                    index++;
                }

                if (index < description.Length && description[index] == '.')
                    index++;
            }

            epgEntry.SeasonNumber = season == 0 ? -1 : season;
            epgEntry.EpisodeNumber = episode == 0 ? -1 : episode;

            if (!OptionEntry.IsDefined(OptionName.NoRemoveData))
            {
                if (index + 2 < description.Length)
                {
                    if (inDescription)
                        epgEntry.ShortDescription = description.Substring(index).Trim();
                    else
                        epgEntry.EventSubTitle = description.Substring(index).Trim();
                }
                else
                {
                    if (inDescription)
                        epgEntry.ShortDescription = null;
                    else
                        epgEntry.EventSubTitle = null;

                    return false;
                }
            }

            return true;
        }

        private bool getGBRSeasonEpisodeNumbers(EPGEntry epgEntry)
        {
            if (string.IsNullOrWhiteSpace(epgEntry.ShortDescription))
                return false;

            int startIndex = epgEntry.ShortDescription.IndexOf("(S");
            if (startIndex != -1)
                return getGBRSeasonEpisodeNumbersFormat1(epgEntry, startIndex);

            startIndex = epgEntry.ShortDescription.IndexOf("(Ep");
            if (startIndex != -1)
                return getGBRSeasonEpisodeNumbersFormat2(epgEntry, startIndex);

            startIndex = epgEntry.ShortDescription.IndexOf("/Ep");
            if (startIndex != -1)
                return getGBRSeasonEpisodeNumbersFormat3(epgEntry, startIndex);

            startIndex = epgEntry.ShortDescription.IndexOf(" Ep");
            if (startIndex != -1)
                return getGBRSeasonEpisodeNumbersFormat4(epgEntry, startIndex);

            return false;
        }

        private bool getGBRSeasonEpisodeNumbersFormat1(EPGEntry epgEntry, int startIndex)
        {
            int season = -1;
            int episode = -1;
            
            int endIndex = epgEntry.ShortDescription.IndexOf(")", startIndex);
            if (endIndex == -1)
                return false;

            string seasonEpisodeString = epgEntry.ShortDescription.Substring(startIndex, endIndex - startIndex + 1);

            int scanIndex = 2;
            season = 0;

            while (Char.IsDigit(seasonEpisodeString[scanIndex]))
            {
                season = (season * 10) + (seasonEpisodeString[scanIndex] - '0');
                scanIndex++;
            }

            while (seasonEpisodeString[scanIndex] == ' ')
                scanIndex++;

            if (seasonEpisodeString[scanIndex] == '/')
                scanIndex++;

            while (seasonEpisodeString[scanIndex] == ' ')
                scanIndex++;

            if (seasonEpisodeString[scanIndex] == 'E' && seasonEpisodeString[scanIndex + 1] == 'p')
            {
                episode = 0;
                scanIndex += 2;

                if (seasonEpisodeString[scanIndex] == ' ')
                    scanIndex++;

                while (Char.IsDigit(seasonEpisodeString[scanIndex]))
                {
                    episode = (episode * 10) + (seasonEpisodeString[scanIndex] - '0');
                    scanIndex++;
                }

                if (seasonEpisodeString[scanIndex] != ')' && seasonEpisodeString[scanIndex] != '/')
                    return false;
            }
            else
            {
                if (seasonEpisodeString[scanIndex] != ')')
                    return false;
            }

            epgEntry.SeasonNumber = season;
            epgEntry.EpisodeNumber = episode;

            if (!OptionEntry.IsDefined(OptionName.NoRemoveData))
                epgEntry.ShortDescription = epgEntry.ShortDescription.Remove(startIndex, endIndex - startIndex + 1).Trim();

            return true;
        }

        private bool getGBRSeasonEpisodeNumbersFormat2(EPGEntry epgEntry, int startIndex)
        {
            int endIndex = epgEntry.ShortDescription.IndexOf(")", startIndex);
            if (endIndex == -1)
                return false;

            int episode = 0;
            string episodeString = epgEntry.ShortDescription.Substring(startIndex, endIndex - startIndex + 1);

            int scanIndex = 3;
            if (episodeString[scanIndex] == ' ')
                scanIndex++;

            while (Char.IsDigit(episodeString[scanIndex]))
            {
                episode = (episode * 10) + (episodeString[scanIndex] - '0');
                scanIndex++;
            }

            if (episodeString[scanIndex] != ')')
                return false;

            epgEntry.EpisodeNumber = episode;

            if (!OptionEntry.IsDefined(OptionName.NoRemoveData))
                epgEntry.ShortDescription = epgEntry.ShortDescription.Remove(startIndex, endIndex - startIndex + 1).Trim();

            return true;
        }

        private bool getGBRSeasonEpisodeNumbersFormat3(EPGEntry epgEntry, int startIndex)
        {
            int season = 0;
            int episode = 0;

            int episodeIndex = startIndex + 3;

            while (startIndex != -1 && epgEntry.ShortDescription[startIndex] != 'S')
                startIndex--;

            if (startIndex == -1)
                return false;

            int seasonIndex = startIndex + 1;

            while (Char.IsDigit(epgEntry.ShortDescription[seasonIndex]))
            {
                season = (season * 10) + (epgEntry.ShortDescription[seasonIndex] - '0');
                seasonIndex++;
            }

            while (episodeIndex < epgEntry.ShortDescription.Length && Char.IsDigit(epgEntry.ShortDescription[episodeIndex]))
            {
                episode = (episode * 10) + (epgEntry.ShortDescription[episodeIndex] - '0');
                episodeIndex++;
            }

            epgEntry.SeasonNumber = season;
            epgEntry.EpisodeNumber = episode;

            if (!OptionEntry.IsDefined(OptionName.NoRemoveData))
                epgEntry.ShortDescription = epgEntry.ShortDescription.Remove(startIndex, episodeIndex - startIndex).Trim();

            return true;
        }

        private bool getGBRSeasonEpisodeNumbersFormat4(EPGEntry epgEntry, int startIndex)
        {
            if (startIndex + 3 == epgEntry.ShortDescription.Length || !Char.IsDigit(epgEntry.ShortDescription[startIndex + 3]))
                return false;

            int season = 0;
            int episode = 0;

            int episodeIndex = startIndex + 3;

            while (startIndex != -1 && epgEntry.ShortDescription[startIndex] != 'S')
                startIndex--;

            if (startIndex == -1)
                return false;

            int seasonIndex = startIndex + 1;

            while (Char.IsDigit(epgEntry.ShortDescription[seasonIndex]))
            {
                season = (season * 10) + (epgEntry.ShortDescription[seasonIndex] - '0');
                seasonIndex++;
            }

            while (episodeIndex < epgEntry.ShortDescription.Length && Char.IsDigit(epgEntry.ShortDescription[episodeIndex]))
            {
                episode = (episode * 10) + (epgEntry.ShortDescription[episodeIndex] - '0');
                episodeIndex++;
            }

            epgEntry.SeasonNumber = season;
            epgEntry.EpisodeNumber = episode;

            if (!OptionEntry.IsDefined(OptionName.NoRemoveData))
                epgEntry.ShortDescription = epgEntry.ShortDescription.Remove(startIndex, episodeIndex - startIndex).Trim();

            return true;
        }

        private void getSeriesEpisodeIds(EPGEntry epgEntry, EITEntry eitEntry)
        {
            if (eitEntry.SeriesId == null && eitEntry.EpisodeId == null)
                return;

            epgEntry.SeasonCrid = eitEntry.SeriesId;
            epgEntry.EpisodeCrid = eitEntry.EpisodeId;

            if (eitEntry.SeriesId != null)
            {
                if (eitEntry.EpisodeId != null)
                {
                    epgEntry.SeriesId = eitEntry.SeriesId;
                    epgEntry.EpisodeId = eitEntry.EpisodeId;
                }
                else
                {
                    epgEntry.SeriesId = eitEntry.SeriesId;
                    epgEntry.EpisodeId = null;
                }
            }
            else
            {
                epgEntry.SeriesId = null;
                epgEntry.EpisodeId = eitEntry.EpisodeId;
            }
        }

        private EventCategorySpec getEventCategory(string title, string description, int contentType, int contentSubType)
        {
            if (contentType == -1 || contentSubType == -1)
            {
                ProgramCategory overrideCategory = getCustomCategory(title, description);
                if (overrideCategory != null)
                    return new EventCategorySpec(overrideCategory);
                else
                    return (null);
            }

            if (OptionEntry.IsDefined(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.Options, OptionName.CustomCategoryOverride))
            {
                ProgramCategory overrideCategory = getCustomCategory(title, description);
                if (overrideCategory != null)
                    return new EventCategorySpec(overrideCategory);
            }

            ProgramCategory category = null;

            if (OptionEntry.IsDefined(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.Options, OptionName.UseContentSubtype))
            {
                category = EITProgramCategory.FindCategory(contentType, contentSubType);
                if (category != null)
                {
                    if (category.SampleEvent == null)
                        category.SampleEvent = title;
                    category.UsedCount++;
                    return new EventCategorySpec(category);
                }
            }

            category = EITProgramCategory.FindCategory(contentType, 0);
            if (category != null)
            {
                if (category.SampleEvent == null)
                    category.SampleEvent = title;
                category.UsedCount++;
                return new EventCategorySpec(category);
            }

            EITProgramCategory.AddUndefinedCategory(contentType, contentSubType, title);

            if (OptionEntry.IsDefined(RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.Options,
                OptionName.CustomCategoryOverride))
                return (null);

            ProgramCategory customCategory = getCustomCategory(title, description);
            if (customCategory != null)
                return new EventCategorySpec(customCategory);
            else
                return null;
        }

        private ProgramCategory getCustomCategory(string title, string description)
        {
            ProgramCategory category = CustomProgramCategory.FindCategory(title);
            if (category != null)
                return (category);

            return (CustomProgramCategory.FindCategory(description));         
        }

        private string getDate(EITEntry eitEntry, EPGEntry epgEntry)
        {
            if (string.IsNullOrWhiteSpace(epgEntry.ShortDescription) || epgEntry.ShortDescription.Length < 4)
                return (null);

            switch (RunParameters.Instance.CurrentFrequency.AdvancedRunParamters.CountryCode)
            {
                case Country.Egypt:
                    return getEgyptDate(epgEntry);                    
                case Country.UnitedKingdom:
                    return getGBRDate(epgEntry);                    
                default:
                    return (eitEntry.Year);
            }
        }

        private string getEgyptDate(EPGEntry epgEntry)
        {
            if (!char.IsDigit(epgEntry.ShortDescription[0]) ||
                !char.IsDigit(epgEntry.ShortDescription[1]) ||
                !char.IsDigit(epgEntry.ShortDescription[2]) ||
                !char.IsDigit(epgEntry.ShortDescription[3]))
                return (null);

            if (epgEntry.ShortDescription.Length > 4 && epgEntry.ShortDescription[4] != ':')
                return (null);

            string date = epgEntry.ShortDescription.Substring(0, 4);

            if (!OptionEntry.IsDefined(OptionName.NoRemoveData))
            {
                if (epgEntry.ShortDescription.Length > 5)
                    epgEntry.ShortDescription = epgEntry.ShortDescription.Substring(5).Trim();
                else
                    epgEntry.ShortDescription = null;
            }

            return (date);
        }

        private string getGBRDate(EPGEntry epgEntry)
        {
            int startIndex = epgEntry.ShortDescription.IndexOf("(19");
            if (startIndex == -1)
                startIndex = epgEntry.ShortDescription.IndexOf("(20");

            if (startIndex == -1)
                return null;

            int endIndex = epgEntry.ShortDescription.IndexOf(")", startIndex);
            if (endIndex == -1)
                return null;

            string date = epgEntry.ShortDescription.Substring(startIndex + 1, endIndex - startIndex - 1);
            if (date.Length != 4)
                return null;

            if (!OptionEntry.IsDefined(OptionName.NoRemoveData))
                epgEntry.ShortDescription = epgEntry.ShortDescription.Remove(startIndex, endIndex - startIndex + 1).Trim();

            return (date);
        }

        private void logTitle(string title, EITEntry eitEntry, EPGEntry epgEntry, Logger logger)
        {
            logger.Write(epgEntry.OriginalNetworkID + ":" + epgEntry.TransportStreamID + ":" + epgEntry.ServiceID + " " +            
                epgEntry.StartTime.ToShortDateString() + " " +
                epgEntry.StartTime.ToString("HH:mm") + " - " +
                epgEntry.StartTime.Add(epgEntry.Duration).ToString("HH:mm") + " " +
                "Content: " + eitEntry.ContentType + "/" + eitEntry.ContentSubType + " " +
                title);
        }

        private void logDescription(string description, EITEntry eitEntry, EPGEntry epgEntry, Logger logger)
        {
            logger.Write(epgEntry.OriginalNetworkID + ":" + epgEntry.TransportStreamID + ":" + epgEntry.ServiceID + " " +
                epgEntry.StartTime.ToShortDateString() + " " +
                epgEntry.StartTime.ToString("HH:mm") + " - " +
                epgEntry.StartTime.Add(epgEntry.Duration).ToString("HH:mm") + " " +
                "Content: " + eitEntry.ContentType + "/" + eitEntry.ContentSubType + " " +
                description);
        }

        private void updateCategoryEntries(TVStation tvStation, EITEntry eitEntry)
        {
            if (categoryEntries == null)
                categoryEntries = new Collection<CategoryEntry>();

            CategoryEntry newEntry = new CategoryEntry(tvStation.OriginalNetworkID, tvStation.TransportStreamID, tvStation.ServiceID, eitEntry.StartTime, eitEntry.EventName, eitEntry.ContentType, eitEntry.ContentSubType);

            foreach (CategoryEntry oldEntry in categoryEntries)
            {
                if (oldEntry.NetworkID == newEntry.NetworkID &&
                    oldEntry.TransportStreamID == newEntry.TransportStreamID &&
                    oldEntry.ServiceID == newEntry.ServiceID &&
                    oldEntry.StartTime == newEntry.StartTime)
                    return;

                if (oldEntry.NetworkID > newEntry.NetworkID)
                {
                    categoryEntries.Insert(categoryEntries.IndexOf(oldEntry), newEntry);
                    return;
                }

                if (oldEntry.NetworkID == newEntry.NetworkID &&
                    oldEntry.TransportStreamID > newEntry.TransportStreamID)
                {
                    categoryEntries.Insert(categoryEntries.IndexOf(oldEntry), newEntry);
                    return;
                }

                if (oldEntry.NetworkID == newEntry.NetworkID &&
                    oldEntry.TransportStreamID == newEntry.TransportStreamID &&
                    oldEntry.ServiceID > newEntry.ServiceID)
                {
                    categoryEntries.Insert(categoryEntries.IndexOf(oldEntry), newEntry);
                    return;
                }

                if (oldEntry.NetworkID == newEntry.NetworkID &&
                    oldEntry.TransportStreamID == newEntry.TransportStreamID &&
                    oldEntry.ServiceID == newEntry.ServiceID &&
                    oldEntry.StartTime > newEntry.StartTime)
                {
                    categoryEntries.Insert(categoryEntries.IndexOf(oldEntry), newEntry);
                    return;
                }
            }

            categoryEntries.Add(newEntry);
        }

        /// <summary>
        /// Validate the entry fields.
        /// </summary>
        public void Validate() { }

        /// <summary>
        /// Log the entry fields.
        /// </summary>
        public void LogMessage()
        {
            if (Logger.ProtocolLogger == null)
                return;

            Logger.ProtocolLogger.Write(Logger.ProtocolIndent + "EIT Section: ONID: " + originalNetworkID +
                " TSID: " + transportStreamID +
                " SID: " + serviceID);
        }
    }
}

