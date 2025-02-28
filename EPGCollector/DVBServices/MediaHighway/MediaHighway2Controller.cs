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

using System.Collections.ObjectModel;
using System.Threading;
using System.ComponentModel;

using DirectShow;
using DomainObjects;

namespace DVBServices
{
    /// <summary>
    /// The class that controls the acquisition and processing of MediaHighway2 data.
    /// </summary>
    public class MediaHighway2Controller : ControllerBase
    {
        /// <summary>
        /// Get the collection type supported by this collector.
        /// </summary>
        public override CollectionType CollectionType { get { return (CollectionType.MediaHighway2); } }
        /// <summary>
        /// Return true if all data has been processed; false otherwise.
        /// </summary>
        public override bool AllDataProcessed { get { return (true); } }

        private TSStreamReader channelReader;
        private TSStreamReader categoryReader;
        private TSStreamReader titleReader;
        private TSStreamReader summaryReader;

        /// <summary>
        /// Initialize a new instance of the MediaHighway2Controller class.
        /// </summary>
        public MediaHighway2Controller() { }

        /// <summary>
        /// Stop acquiring and processing data.
        /// </summary>
        public override void Stop()
        {
            Logger.Instance.Write("Stopping section readers");

            if (channelReader != null)
                channelReader.Stop();

            if (categoryReader != null)
                categoryReader.Stop();

            if (titleReader != null)
                titleReader.Stop();

            if (summaryReader != null)
                summaryReader.Stop();

            Logger.Instance.Write("Stopped section readers");
        }

        /// <summary>
        /// Acquire and process MediaHighway2 data.
        /// </summary>
        /// <param name="dataProvider">A sample data provider.</param>
        /// <param name="worker">The background worker that is running this collection.</param>
        /// <returns>A CollectorReply code.</returns>
        public override CollectorReply Process(ISampleDataProvider dataProvider, BackgroundWorker worker)
        {
            return (Process(dataProvider, worker, CollectionSpan.AllData));
        }

        /// <summary>
        /// Acquire and process MediaHighway2 data.
        /// </summary>
        /// <param name="dataProvider">A sample data provider.</param>
        /// <param name="worker">The background worker that is running this collection.</param>
        /// <param name="collectionSpan">The amount of data to collect.</param>
        /// <returns>A CollectorReply code.</returns>
        public override CollectorReply Process(ISampleDataProvider dataProvider, BackgroundWorker worker, CollectionSpan collectionSpan)
        {
            if (collectionSpan == CollectionSpan.AllData)
            {
                MediaHighwayProgramCategory.LoadFromFrequency("2", dataProvider.Frequency.ToString());
                CustomProgramCategory.Load();
                ParentalRating.Load();
            }

            if (RunParameters.Instance.NetworkDataNeeded)
            {
                GetNetworkInformation(dataProvider, worker);
                if (worker.CancellationPending)
                    return (CollectorReply.Cancelled);
            }

            getChannelSections(dataProvider, worker);
            if (worker.CancellationPending)
                return (CollectorReply.Cancelled);

            if (Channel.Channels.Count == 0)
            {
                Logger.Instance.Write("<e> No channels located - data collection abandoned");
                return (CollectorReply.OK);
            }

            creatStationsFromChannels();

            if (collectionSpan == CollectionSpan.ChannelsOnly)
                return (CollectorReply.OK);

            getCategorySections(dataProvider, worker);
            if (worker.CancellationPending)
                return (CollectorReply.Cancelled);

            getTitleSections(dataProvider, worker);
            if (worker.CancellationPending)
                return (CollectorReply.Cancelled);

            getSummarySections(dataProvider, worker);

            return (CollectorReply.OK);
        }

        private void getChannelSections(ISampleDataProvider dataProvider, BackgroundWorker worker)
        {
            Logger.Instance.Write("Collecting channel data");
            Channel.Channels.Clear();

            dataProvider.ChangePidMapping(new int[] { 0x231 });            

            channelReader = new TSStreamReader(0xc8, 2000, dataProvider.BufferAddress);
            channelReader.Run();

            int lastCount = 0;
            int repeats = 0;

            bool channelSectionsDone = false;

            while (!channelSectionsDone)
            {
                if (worker.CancellationPending)
                    return;

                Thread.Sleep(2000);
                LogBufferSpaceUsed("channel data", dataProvider);

                Collection<Mpeg2Section> sections = new Collection<Mpeg2Section>();

                channelReader.Lock("ProcessMHW2Sections");
                if (channelReader.Sections.Count != 0)
                {
                    foreach (Mpeg2Section section in channelReader.Sections)
                        sections.Add(section);
                    channelReader.Sections.Clear();
                }
                channelReader.Release("ProcessMHW2Sections");

                if (sections.Count != 0)
                    processChannelSections(sections);

                if (Channel.Channels.Count == lastCount)
                {
                    repeats++;
                    channelSectionsDone = (repeats == RunParameters.Instance.Repeats);
                }
                else
                    repeats = 0;

                lastCount = Channel.Channels.Count;
            }

            Logger.Instance.Write("Stopping channel reader for PID 0x231");
            channelReader.Stop();

            Logger.Instance.Write("Channel count: " + Channel.Channels.Count +
                " buffer space used: " + dataProvider.BufferSpaceUsed +
                " discontinuities: " + channelReader.Discontinuities);            
        }

        private void getCategorySections(ISampleDataProvider dataProvider, BackgroundWorker worker)
        {
            Logger.Instance.Write("Collecting category data");
            
            categoryReader = new TSStreamReader(0xc8, 2000, dataProvider.BufferAddress);
            categoryReader.Run();

            int lastCount = 0;
            int repeats = 0;

            bool categorySectionsDone = false;

            while (!categorySectionsDone)
            {
                if (worker.CancellationPending)
                    return;

                Thread.Sleep(2000);
                LogBufferSpaceUsed("category data", dataProvider);

                Collection<Mpeg2Section> sections = new Collection<Mpeg2Section>();

                categoryReader.Lock("ProcessMHW2Sections");
                if (categoryReader.Sections.Count != 0)
                {
                    foreach (Mpeg2Section section in categoryReader.Sections)
                        sections.Add(section);
                    categoryReader.Sections.Clear();
                }
                categoryReader.Release("ProcessMHW2Sections");

                if (sections.Count != 0)
                    processCategorySections(sections);

                if (MediaHighwayProgramCategory.Categories.Count == lastCount)
                {
                    repeats++;
                    categorySectionsDone = (repeats == RunParameters.Instance.Repeats);
                }
                else
                    repeats = 0;

                lastCount = MediaHighwayProgramCategory.Categories.Count;
            }

            Logger.Instance.Write("Stopping category reader for PID 0x231");
            categoryReader.Stop();

            Logger.Instance.Write("Category count: " + MediaHighwayProgramCategory.Categories.Count +
                " buffer space used: " + dataProvider.BufferSpaceUsed +
                " discontinuities: " + categoryReader.Discontinuities);            
        }

        private void getTitleSections(ISampleDataProvider dataProvider, BackgroundWorker worker)
        {
            Logger.Instance.Write("Collecting title data");

            dataProvider.ChangePidMapping(new int[] { 0x234 });            

            titleReader = new TSStreamReader(0xe6, 2000, dataProvider.BufferAddress);
            titleReader.Run();

            int lastCount = 0;
            int repeats = 0;
            int titleDataCount = 0;

            bool titleSectionsDone = false;

            while (!titleSectionsDone)
            {
                if (worker.CancellationPending)
                    return;

                Thread.Sleep(1000);
                LogBufferSpaceUsed("title data", dataProvider);

                Collection<Mpeg2Section> sections = new Collection<Mpeg2Section>();

                titleReader.Lock("ProcessMHW2Sections");

                if (titleReader.Sections.Count != 0)
                {
                    foreach (Mpeg2Section section in titleReader.Sections)
                        sections.Add(section);                    

                    titleReader.Sections.Clear();
                }

                titleReader.Release("ProcessMHW2Sections");

                if (sections.Count != 0)
                    processTitleSections(sections);

                titleDataCount = 0;
                foreach (MediaHighwayChannel channel in MediaHighwayChannel.Channels)
                    titleDataCount += channel.Titles.Count;

                if (titleDataCount == lastCount)
                {
                    repeats++;
                    titleSectionsDone = (repeats == RunParameters.Instance.Repeats);
                }
                else
                    repeats = 0;

                lastCount = titleDataCount;
            }

            Logger.Instance.Write("Stopping title reader for PID 0x234");
            titleReader.Stop();

            Logger.Instance.Write("Title count: " + titleDataCount +
                " buffer space used: " + dataProvider.BufferSpaceUsed +
                " discontinuities: " + titleReader.Discontinuities);
        }

        private void getSummarySections(ISampleDataProvider dataProvider, BackgroundWorker worker)
        {
            Logger.Instance.Write("Collecting summary data");

            dataProvider.ChangePidMapping(new int[] { 0x236 });

            summaryReader = new TSStreamReader(0x96, 2000, dataProvider.BufferAddress);
            summaryReader.Run();

            int lastCount = 0;
            int repeats = 0;

            bool summarySectionsDone = false;

            while (!summarySectionsDone)
            {
                if (worker.CancellationPending)
                    return;

                Thread.Sleep(1000);
                LogBufferSpaceUsed("summary data", dataProvider);

                Collection<Mpeg2Section> sections = new Collection<Mpeg2Section>();

                summaryReader.Lock("ProcessMHW2Sections");

                if (summaryReader.Sections.Count != 0)
                {
                    foreach (Mpeg2Section section in summaryReader.Sections)
                        sections.Add(section);
                    summaryReader.Sections.Clear();
                }

                summaryReader.Release("ProcessMHW2Sections");

                if (sections.Count != 0)
                    processSummarySections(sections);

                if (MediaHighwaySummary.Summaries.Count == lastCount)
                {
                    repeats++;
                    summarySectionsDone = (repeats == RunParameters.Instance.Repeats);
                }
                else
                    repeats = 0;

                lastCount = MediaHighwaySummary.Summaries.Count;
            }

            Logger.Instance.Write("Stopping summary reader for PID 0x236");
            summaryReader.Stop();

            Logger.Instance.Write("Summary count: " + MediaHighwaySummary.Summaries.Count +
                " buffer space used: " + dataProvider.BufferSpaceUsed +
                " discontinuities: " + summaryReader.Discontinuities);            
        }

        private void processChannelSections(Collection<Mpeg2Section> sections)
        {
            foreach (Mpeg2Section section in sections)
            {
                if (TraceEntry.IsDefined(TraceName.DumpChannelSections))
                    Logger.Instance.Dump("Channel Section", section.Data, section.Length);

                MediaHighway2ChannelSection channelSection = MediaHighway2ChannelSection.ProcessMediaHighwayChannelTable(section.Data);
                if (channelSection != null)
                {
                    if (channelSection.Channels != null)
                    {
                        foreach (MediaHighwayChannelInfoEntry channelInfoEntry in channelSection.Channels)
                        {
                            MediaHighwayChannel channel = new MediaHighwayChannel();
                            channel.ChannelID = channelSection.Channels.IndexOf(channelInfoEntry) + 1;
                            
                            channel.OriginalNetworkID = channelInfoEntry.OriginalNetworkID;
                            channel.TransportStreamID = channelInfoEntry.TransportStreamID;
                            channel.ServiceID = channelInfoEntry.ServiceID;
                            channel.ChannelName = channelInfoEntry.Name;
                            channel.UserChannel = Channel.Channels.Count + 1;
                            channel.ChannelType = channelSection.SectionNumber;
                            Channel.AddChannel(channel);
                        }
                    }
                }
            }
        }

        private void processCategorySections(Collection<Mpeg2Section> sections)
        {
            foreach (Mpeg2Section section in sections)
            {
                if (TraceEntry.IsDefined(TraceName.DumpCategorySections))
                    Logger.Instance.Dump("Category Section", section.Data, section.Length);

                MediaHighway2CategorySection categorySection = MediaHighway2CategorySection.ProcessMediaHighwayCategoryTable(section.Data);
                if (categorySection != null)
                {
                    if (categorySection.Categories != null)
                    {
                        foreach (MediaHighwayCategoryEntry categoryEntry in categorySection.Categories)
                        {
                            if (MediaHighwayProgramCategory.AddCategory(categoryEntry.Number, categoryEntry.Description + "=" + categoryEntry.Description))
                                Logger.Instance.Write("Added category from broadcast data: " + categoryEntry.Number + " - " + categoryEntry.Description);
                        }
                    }
                }
            }
        }

        private void processTitleSections(Collection<Mpeg2Section> sections)
        {
            foreach (Mpeg2Section section in sections)
            {
                if (TraceEntry.IsDefined(TraceName.DumpTitleSections))
                    Logger.Instance.Dump("Title Section", section.Data, section.Length);

                MediaHighway2TitleSection titleSection = MediaHighway2TitleSection.ProcessMediaHighwayTitleTable(section.Data);
                if (titleSection != null && titleSection.Titles != null)
                {
                    foreach (MediaHighway2TitleData titleData in titleSection.Titles)
                    {
                        Collection<Channel> channels = MediaHighwayChannel.FindChannels(titleData.ChannelID + 1);
                        if (channels.Count != 0)
                        {
                            MediaHighwayTitle title = new MediaHighwayTitle();
                            title.CategoryID = titleData.CategoryID;
                            title.Duration = titleData.Duration;
                            title.EventID = titleData.EventID;
                            title.EventName = titleData.EventName;
                            title.StartTime = titleData.StartTime;
                            title.SummaryAvailable = (title.EventID != 0xffff);
                            title.Unknown = titleData.Unknown;
                            title.MainCategory = titleData.MainCategory;
                            title.SubCategory = titleData.SubCategory;

                            foreach (MediaHighwayChannel channel in channels)
                            {
                                MediaHighwayTitle existingTitle = channel.AddTitleData(title);
                                if (existingTitle != null && existingTitle.EventID != title.EventID)
                                    Logger.Instance.Write("Title already exists with different event ID");
                            }
                        }
                        else
                            Logger.Instance.Write("Title data has no matching channel - channel ID is " + (titleData.ChannelID + 1));

                    }
                }
            }
        }

        private void processSummarySections(Collection<Mpeg2Section> sections)
        {
            foreach (Mpeg2Section section in sections)
            {
                if (TraceEntry.IsDefined(TraceName.DumpSummarySections))
                    Logger.Instance.Dump("Summary Section", section.Data, section.Length);

                MediaHighway2SummarySection summarySection = MediaHighway2SummarySection.ProcessMediaHighwaySummaryTable(section.Data);
                if (summarySection != null && summarySection.SummaryData != null)
                {
                    MediaHighwaySummary summary = new MediaHighwaySummary();
                    summary.EventID = summarySection.SummaryData.EventID;
                    summary.ShortDescription = summarySection.SummaryData.ShortDescription;
                    summary.Unknown = summarySection.SummaryData.Unknown;
                    MediaHighwaySummary.AddSummary(summary);
                }
            }
        }

        /// <summary>
        /// Create the EPG entries.
        /// </summary>
        public override void FinishFrequency()
        {
            if (MediaHighwayChannel.Channels.Count == 0)
                return;

            Logger titleLogger = null;
            Logger descriptionLogger = null;

            if (DebugEntry.IsDefined(DebugName.LogTitles))
                titleLogger = new Logger("EPG Titles.log");
            if (DebugEntry.IsDefined(DebugName.LogDescriptions))
                descriptionLogger = new Logger("EPG Descriptions.log");

            foreach (MediaHighwayChannel channel in MediaHighwayChannel.Channels)
            {
                TVStation station = TVStation.FindStation(RunParameters.Instance.StationCollection,
                    channel.OriginalNetworkID, channel.TransportStreamID, channel.ServiceID);
                if (station != null && station.EPGCollection.Count == 0)
                    channel.ProcessChannelForEPG(station, titleLogger, descriptionLogger, CollectionType.MediaHighway2);
            }

            MediaHighwayProgramCategory.LogUsage(MediaHighwayProgramCategory.Categories, MediaHighwayProgramCategory.UndefinedCategories, "MHW2");            
            //Channel.LogChannelsInChannelIDOrder();            
        }

        private void creatStationsFromChannels()
        {
            Logger.Instance.Write("Creating stations from MHW2 channels");

            Collection<MediaHighwayChannel> sortedChannels = new Collection<MediaHighwayChannel>();

            foreach (MediaHighwayChannel channel in MediaHighwayChannel.Channels)
            {
                if (channel.ChannelType != 0x03)
                {
                    TVStation station = TVStation.FindStation(RunParameters.Instance.StationCollection,
                        channel.OriginalNetworkID, channel.TransportStreamID, channel.ServiceID);
                    if (station == null)
                    {
                        station = new TVStation(channel.ChannelName);
                        station.OriginalNetworkID = channel.OriginalNetworkID;
                        station.TransportStreamID = channel.TransportStreamID;
                        station.ServiceID = channel.ServiceID;

                        TVStation.AddStation(RunParameters.Instance.StationCollection, station);
                        addNewChannel(sortedChannels, channel);
                    }
                    else
                        Logger.Instance.Write("Channel " + channel.ChannelName + "(" + channel.OriginalNetworkID + "," + channel.TransportStreamID + "," + channel.ServiceID + ") already exists");

                    station.Name = channel.ChannelName;

                    if (station.LogicalChannelNumber == -1)
                        station.LogicalChannelNumber = channel.UserChannel;
                }
                else
                    Logger.Instance.Write("Channel " + channel.ChannelName + "(" + channel.OriginalNetworkID + "," + channel.TransportStreamID + "," + channel.ServiceID + ") type 0x03 ignored");
            }

            Logger.Instance.Write("Duplicate channels: " + (MediaHighwayChannel.Channels.Count - sortedChannels.Count));

            foreach (MediaHighwayChannel channel in sortedChannels)
                Logger.Instance.Write("Station: " + channel.ChannelName + " (" +
                    channel.OriginalNetworkID + "," + channel.TransportStreamID + "," + channel.ServiceID + ") Channel ID " + channel.ChannelID + " Type 0x" + channel.ChannelType.ToString("x2"));

            Logger.Instance.Write("Station count now: " + RunParameters.Instance.StationCollection.Count);
        }

        private void addNewChannel(Collection<MediaHighwayChannel> sortedChannels, MediaHighwayChannel newChannel)
        {
            foreach (MediaHighwayChannel channel in sortedChannels)
            {
                int result = channel.ChannelName.CompareTo(newChannel.ChannelName);
                if (result > 0)
                {
                    sortedChannels.Insert(sortedChannels.IndexOf(channel), newChannel);
                    return;
                }
            }

            sortedChannels.Add(newChannel);
        }

        internal static string FixString(string description, int offset)
        {
            string editedDescription = description;
            string correctChar = ((char)(0xa1)).ToString();

            if (editedDescription.Contains("jjj"))
            {
                editedDescription = editedDescription.Replace("jjj", correctChar + correctChar + correctChar);
                return editedDescription;
            }

            int editIndex = editedDescription.IndexOf("!");
            if (editIndex != -1)
            {
                if (editedDescription[offset] == 'j')
                {
                    if (offset == 0)
                        editedDescription = correctChar + editedDescription.Substring(1);
                    else
                        editedDescription = editedDescription.Substring(0, offset) + correctChar + editedDescription.Substring(offset + 1);
                }
                else
                {
                    editedDescription = editedDescription.Replace(". j", ". " + correctChar);
                    editedDescription = editedDescription.Replace(": j", ". " + correctChar);
                    editedDescription = editedDescription.Replace(", j", ". " + correctChar);
                }
            }

            return editedDescription;
        }
    }
}

