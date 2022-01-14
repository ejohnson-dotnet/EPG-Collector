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
using System.Xml;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Security.Cryptography;

namespace DomainObjects
{
    /// <summary>
    /// The class that creates an MXF file for import to 7MC.
    /// </summary>
    public sealed class OutputFileMXF
    {
        private static string actualFileName;

        private static Collection<KeywordGroup> groups;
        private static Collection<Person> people;
        private static Collection<string> series;
        private static Collection<int> stationImages;
        private static Collection<string> programImages;
        private static Collection<string> duplicateStationNames;
        private static Collection<ImportedImage> importedImages;
        
        private static bool isSpecial;
        private static bool isMovie;
        private static bool isSports;
        private static bool isKids;
        private static bool isNews;
        
        private static string importName;
        private static string importReference;

        private static Process importProcess;
        private static bool importExited;

        private static string mcstoreVersion;
        private static string mcstorePublicKey;

        private static string mcepgVersion;
        private static string mcepgPublicKey;

        private static Collection<string> progIds = new Collection<string>();
        private static Collection<string> uniqueIdentifiers = new Collection<string>();

        private static decimal totalEntriesProcessed;
        private static decimal totalSeriesIds;
        private static decimal totalEpisodeIds;
        private static int totalGenericProgrammes;

        private static SHA256 sha256;

        private static int invalidTimes;

        private OutputFileMXF() { }

        /// <summary>
        /// Create the MXF file.
        /// </summary>
        /// <returns>An error message if the process fails; null otherwise.</returns>
        public static string Process()
        {
            if (OptionEntry.IsDefined(RunParameters.Instance.Options, OptionName.NoDataNoFile))
            {
                if (TVStation.EPGCount(RunParameters.Instance.StationCollection) == 0)
                {
                    Logger.Instance.Write("No data collected - import process abandoned");
                    return null;
                }
            }

            mcepgVersion = getAssemblyVersion("mcepg.dll");
            if (string.IsNullOrWhiteSpace(mcepgVersion))
                return ("Failed to get the assembly version for mcpeg.dll");
 
            mcepgPublicKey = getAssemblyPublicKey("mcepg.dll");
            if (string.IsNullOrWhiteSpace(mcepgPublicKey))
                return ("Failed to get the public key for mcpeg.dll");
            
            mcstoreVersion = getAssemblyVersion("mcstore.dll");
            if (string.IsNullOrWhiteSpace(mcstoreVersion))
                return ("Failed to get the assembly version for mcstore.dll");

            mcstorePublicKey = getAssemblyPublicKey("mcstore.dll");
            if (string.IsNullOrWhiteSpace(mcstorePublicKey))
                return ("Failed to get the public key for mcstore.dll");

            actualFileName = Path.Combine(RunParameters.DataDirectory, "TVGuide.mxf");

            try
            {
                Logger.Instance.Write("Deleting any existing version of output file");
                File.SetAttributes(actualFileName, FileAttributes.Normal);
                File.Delete(actualFileName);
            }
            catch (IOException e)
            {
                Logger.Instance.Write("File delete exception: " + e.Message);
            }

            if (RunParameters.Instance.WMCImportName != null)
                importName = RunParameters.Instance.WMCImportName;
            if (importName == null)
                importName = "EPG Collector";
            importReference = importName.Replace(" ", string.Empty);
            Logger.Instance.Write("Import name set to '" + importName + "'");

            duplicateStationNames = new Collection<string>();
            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (station.Included)
                {
                    int occurrences = countStationName(station);
                    if (occurrences > 1)
                    {
                        if (!duplicateStationNames.Contains(station.Name))
                            duplicateStationNames.Add(station.Name);
                    }
                }
            }

            Logger.Instance.Write("Creating output file: " + actualFileName);

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.NewLineOnAttributes = false;
            if (!OutputFile.UseUnicodeEncoding)
                settings.Encoding = new UTF8Encoding();
            else
                settings.Encoding = new UnicodeEncoding();
            settings.CloseOutput = true;
            
            try
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(actualFileName, settings))
                {
                    xmlWriter.WriteStartDocument();

                    xmlWriter.WriteStartElement("MXF");
                    xmlWriter.WriteAttributeString("generator-info-name", Assembly.GetCallingAssembly().GetName().Name
                        + "/" + Assembly.GetCallingAssembly().GetName().Version.ToString());
                    
                    xmlWriter.WriteStartElement("Assembly");
                    xmlWriter.WriteAttributeString("name", "mcepg");
                    xmlWriter.WriteAttributeString("version", mcepgVersion);

                    xmlWriter.WriteAttributeString("cultureInfo", "");
                    xmlWriter.WriteAttributeString("publicKey", mcepgPublicKey);
                    xmlWriter.WriteStartElement("NameSpace");
                    xmlWriter.WriteAttributeString("name", "Microsoft.MediaCenter.Guide");

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "Lineup");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "Channel");
                    xmlWriter.WriteAttributeString("parentFieldName", "lineup");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "Service");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "ScheduleEntry");
                    xmlWriter.WriteAttributeString("groupName", "ScheduleEntries");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "Program");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "Keyword");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "KeywordGroup");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "Person");
                    xmlWriter.WriteAttributeString("groupName", "People");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "ActorRole");
                    xmlWriter.WriteAttributeString("parentFieldName", "program");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "DirectorRole");
                    xmlWriter.WriteAttributeString("parentFieldName", "program");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "WriterRole");
                    xmlWriter.WriteAttributeString("parentFieldName", "program");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "HostRole");
                    xmlWriter.WriteAttributeString("parentFieldName", "program");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "GuestActorRole");
                    xmlWriter.WriteAttributeString("parentFieldName", "program");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "ProducerRole");
                    xmlWriter.WriteAttributeString("parentFieldName", "program");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "GuideImage");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "Affiliate");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "SeriesInfo");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "Season");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Assembly");
                    xmlWriter.WriteAttributeString("name", "mcstore");
                    xmlWriter.WriteAttributeString("version", mcstoreVersion);

                    xmlWriter.WriteAttributeString("cultureInfo", "");
                    xmlWriter.WriteAttributeString("publicKey", mcstorePublicKey);
                    xmlWriter.WriteStartElement("NameSpace");
                    xmlWriter.WriteAttributeString("name", "Microsoft.MediaCenter.Store");

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "Provider");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Type");
                    xmlWriter.WriteAttributeString("name", "UId");
                    xmlWriter.WriteAttributeString("parentFieldName", "target");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Providers");
                    xmlWriter.WriteStartElement("Provider");
                    xmlWriter.WriteAttributeString("id", "provider1");
                    xmlWriter.WriteAttributeString("name", importReference);
                    xmlWriter.WriteAttributeString("displayName", "EPG Collector V" + RunParameters.SystemVersion);
                    xmlWriter.WriteAttributeString("copyright", "");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("With");
                    xmlWriter.WriteAttributeString("provider", "provider1");

                    xmlWriter.WriteStartElement("Keywords");
                    processKeywords(xmlWriter);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("KeywordGroups");
                    processKeywordGroups(xmlWriter);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("GuideImages");
                    processGuideImages(xmlWriter);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("People");
                    processPeople(xmlWriter);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("SeriesInfos");
                    processSeries(xmlWriter);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Seasons");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Programs");
                    processPrograms(xmlWriter);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Affiliates");
                    processAffiliates(xmlWriter);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Services");
                    processServices(xmlWriter);
                    xmlWriter.WriteEndElement();

                    processSchedules(xmlWriter);

                    xmlWriter.WriteStartElement("Lineups");
                    processLineUps(xmlWriter);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();

                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
            catch (XmlException ex1)
            {
                return (ex1.Message);
            }
            catch (IOException ex2)
            {
                return (ex2.Message);
            }

            if (OptionEntry.IsDefined(OptionName.UseWmcRepeatCheck) || OptionEntry.IsDefined(OptionName.UseWmcRepeatCheckBroadcast))
                produceReferenceIdAnalysis();

            Logger.Instance.Write("");
            Logger.Instance.Write("Total generic programmes created = " + totalGenericProgrammes);

            if (OptionEntry.IsDefined(OptionName.NoInvalidEntries))
                Logger.Instance.Write("Number of EPG records with invalid start or end times = " + invalidTimes);

            string reply = runImportUtility(actualFileName);
            if (reply != null)
                return (reply);

            runStandardTasks();

            if (OptionEntry.IsDefined(OptionName.CreateBrChannels))
                OutputFileBladeRunner.Process(actualFileName);
            if (OptionEntry.IsDefined(OptionName.CreateArChannels))
                OutputFileAreaRegionChannels.Process(actualFileName);
            if (OptionEntry.IsDefined(OptionName.CreateSageTvFrq))
                OutputFileSageTVFrq.Process(actualFileName);

            return (null);
        }

        private static int countStationName(TVStation station)
        {
            int count = 0;

            foreach (TVStation existingStation in RunParameters.Instance.StationCollection)
            {
                if (existingStation.Included && station.Name == existingStation.Name)
                    count++;
            }

            return (count);
        }

        private static void produceReferenceIdAnalysis()
        {
            if (ReferenceIdEntry.ReferenceIdEntries != null)
            {
                foreach (ReferenceIdEntry referenceIdEntry in ReferenceIdEntry.ReferenceIdEntries)
                {
                    if (referenceIdEntry.OtherTitles != null)
                    {
                        Logger.Instance.Write("<w> The following programmes are linked to the same hash value: ");

                        foreach (string title in referenceIdEntry.OtherTitles)
                            Logger.Instance.Write("<w>     " + title);
                    }
                }

                foreach (ReferenceIdEntry referenceIdEntry in ReferenceIdEntry.ReferenceIdEntries)
                {
                    if (referenceIdEntry.OtherReferenceIds != null)
                        Logger.Instance.Write("<w> Programme '" + referenceIdEntry.Title + "' is broadcast as " + (referenceIdEntry.OtherReferenceIds.Count + 1) + " different series");
                }
            }

            if (OptionEntry.IsDefined(OptionName.UseWmcRepeatCheckBroadcast))
            {
                if (totalEntriesProcessed != 0)
                {
                    Logger.Instance.Write("Entries with series reference ID's = " + totalSeriesIds + " (" + ((totalSeriesIds * 100) / totalEntriesProcessed).ToString("N2") + "%)");
                    Logger.Instance.Write("Entries with episode reference ID's = " + totalEpisodeIds + " (" + ((totalEpisodeIds * 100) / totalEntriesProcessed).ToString("N2") + "%)");
                }
                else
                {
                    Logger.Instance.Write("Entries with series reference ID's = 0 (0.00%)");
                    Logger.Instance.Write("Entries with episode reference ID's = 0 (0.00%)");
                }
            }
        }

        private static void processKeywords(XmlWriter xmlWriter)
        {
            groups = new Collection<KeywordGroup>();
            groups.Add(new KeywordGroup("General"));

            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (station.Included)
                {
                    foreach (EPGEntry epgEntry in station.EPGCollection)
                    {
                        if (epgEntry.EventCategory != null)
                            processCategory(xmlWriter, groups, epgEntry.EventCategory.GetDescription(EventCategoryMode.Wmc));                        
                    }
                }
            }

            foreach (KeywordGroup group in groups)
            {
                xmlWriter.WriteStartElement("Keyword");
                xmlWriter.WriteAttributeString("id", "k" + ((groups.IndexOf(group) + 1)));
                xmlWriter.WriteAttributeString("word", group.Name.Trim()); 
                xmlWriter.WriteEndElement();

                foreach (string keyword in group.Keywords)
                {
                    xmlWriter.WriteStartElement("Keyword");
                    xmlWriter.WriteAttributeString("id", "k" + (((groups.IndexOf(group) + 1) * 100) + group.Keywords.IndexOf(keyword)));
                    xmlWriter.WriteAttributeString("word", keyword.Trim());
                    xmlWriter.WriteEndElement();
                }
            }
        }

        private static void processCategory(XmlWriter xmlWriter, Collection<KeywordGroup> groups, string category)
        {
            if (category == null)
                return;

            string[] parts = removeSpecialCategories(category);
            if (parts == null)
                return;

            if (parts.Length == 1)
            {                
                foreach (KeywordGroup keywordGroup in groups)
                {
                    if (keywordGroup.Name == parts[0])
                        return;
                }

                KeywordGroup singleGroup = new KeywordGroup(parts[0]);
                singleGroup.Keywords.Add("All");
                singleGroup.Keywords.Add("General");
                groups.Add(singleGroup);
                return;
            }

            foreach (KeywordGroup group in groups)
            {
                if (group.Name == parts[0])
                {
                    for (int index = 1; index < parts.Length; index++)
                    {
                        bool keywordFound = false;

                        foreach (string keyword in group.Keywords)
                        {
                            if (keyword == parts[index])
                                keywordFound = true;
                        }

                        if (!keywordFound)
                        {
                            if (group.Keywords.Count == 0)
                                group.Keywords.Add("All");
                            group.Keywords.Add(parts[index]);
                        }
                    }

                    return;
                }
            }

            KeywordGroup newGroup = new KeywordGroup(parts[0]);
            newGroup.Keywords.Add("All");

            for (int partAddIndex = 1; partAddIndex < parts.Length; partAddIndex++)
                newGroup.Keywords.Add(parts[partAddIndex]);

            groups.Add(newGroup);            
        }

        private static string[] removeSpecialCategories(string category)
        {
            if (category == null)
                return null;

            string[] parts = category.Split(new string[] { "," }, StringSplitOptions.None);

            int specialCategoryCount = 0;

            foreach (string part in parts)
            {
                string specialCategory = getSpecialCategory(part);
                if (specialCategory != null)
                    specialCategoryCount++;
            }

            if (specialCategoryCount == parts.Length)
                return (null);

            string[] editedParts = new string[parts.Length - specialCategoryCount];
            int index = 0;

            foreach (string part in parts)
            {
                string specialCategory = getSpecialCategory(part);
                if (specialCategory == null)
                {
                    editedParts[index] = part;
                    index++;
                }

            }

            return (editedParts);
        }

        private static void processKeywordGroups(XmlWriter xmlWriter)
        {
            int groupNumber = 1;

            foreach (KeywordGroup group in groups)
            {
                xmlWriter.WriteStartElement("KeywordGroup");
                xmlWriter.WriteAttributeString("uid", "!KeywordGroup!k-" + group.Name.ToLowerInvariant().Replace(' ', '-'));
                xmlWriter.WriteAttributeString("groupName", "k" + groupNumber);

                StringBuilder keywordString = new StringBuilder();
                int keywordNumber = 0;

                foreach (string keyword in group.Keywords)
                {
                    if (keywordString.Length != 0)
                        keywordString.Append(",");
                    keywordString.Append("k" + ((groupNumber * 100) + keywordNumber));

                    keywordNumber++;
                }

                xmlWriter.WriteAttributeString("keywords", keywordString.ToString());
                xmlWriter.WriteEndElement();

                groupNumber++;
            }
        }

        private static void processGuideImages(XmlWriter xmlWriter)
        {
            string stationDirectory = Path.Combine(RunParameters.DataDirectory, "Images") + Path.DirectorySeparatorChar;
            
            if (Directory.Exists(stationDirectory))
            {
                stationImages = new Collection<int>();

                DirectoryInfo directoryInfo = new DirectoryInfo(stationDirectory);

                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    if (fileInfo.Extension.ToLowerInvariant() == ".png")
                    {
                        string serviceID = fileInfo.Name.Remove(fileInfo.Name.Length - 4);

                        try
                        {
                            stationImages.Add(Int32.Parse(serviceID));

                            xmlWriter.WriteStartElement("GuideImage");
                            xmlWriter.WriteAttributeString("id", "i" + stationImages.Count);
                            xmlWriter.WriteAttributeString("imageUrl", "file://" + fileInfo.FullName);
                            xmlWriter.WriteEndElement();
                        }
                        catch (FormatException) { }
                        catch (OverflowException) { }                        
                    }
                }
            }

            if (!RunParameters.Instance.LookupImagesInBase)
            {
                addLookupImages(xmlWriter, Path.Combine(RunParameters.ImagePath, "Movies"));
                addLookupImages(xmlWriter, Path.Combine(RunParameters.ImagePath, "TV Series"));
            }
            else
                addLookupImages(xmlWriter, Path.Combine(RunParameters.ImagePath));

            addImportedImages(xmlWriter, Path.Combine(RunParameters.ImagePath, "Imports"));
        }

        private static void addLookupImages(XmlWriter xmlWriter, string directory)
        {
            if (Directory.Exists(directory))
            {
                if (programImages == null)
                    programImages = new Collection<string>();

                DirectoryInfo directoryInfo = new DirectoryInfo(directory);

                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    if (fileInfo.Extension.ToLowerInvariant() == ".jpg")
                    {
                        try
                        {
                            string imageId = fileInfo.Name.Substring(0, fileInfo.Name.Length - 4);
                            programImages.Add(imageId);

                            xmlWriter.WriteStartElement("GuideImage");
                            xmlWriter.WriteAttributeString("id", "i-" + imageId);
                            xmlWriter.WriteAttributeString("imageUrl", "file://" + fileInfo.FullName);
                            xmlWriter.WriteEndElement();
                        }
                        catch (FormatException) { }
                        catch (OverflowException) { }
                    }
                }
            }
        }

        private static void addImportedImages(XmlWriter xmlWriter, string directory)
        {
            if (Directory.Exists(directory))
            {
                if (importedImages == null)
                    importedImages = new Collection<ImportedImage>();

                DirectoryInfo directoryInfo = new DirectoryInfo(directory);

                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    if (fileInfo.Extension.ToLowerInvariant() == ".jpg" || fileInfo.Extension.ToLowerInvariant() == ".png")
                    {
                        try
                        {
                            ImportedImage importedImage = new ImportedImage(fileInfo.FullName);
                            importedImages.Add(importedImage);

                            xmlWriter.WriteStartElement("GuideImage");
                            xmlWriter.WriteAttributeString("id", "i-" + importedImage.Guid);
                            xmlWriter.WriteAttributeString("imageUrl", "file://" + fileInfo.FullName);
                            xmlWriter.WriteEndElement();
                        }
                        catch (FormatException) { }
                        catch (OverflowException) { }
                    }
                }
            }
        }

        private static void processPeople(XmlWriter xmlWriter)
        {
            people = new Collection<Person>();

            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (station.Included)
                {
                    foreach (EPGEntry epgEntry in station.EPGCollection)
                    {
                        if (epgEntry.Cast != null)
                        {
                            foreach (Person person in epgEntry.Cast)
                                processPerson(xmlWriter, people, person);
                        }

                        if (epgEntry.Directors != null)
                        {
                            foreach (Person person in epgEntry.Directors)
                                processPerson(xmlWriter, people, person);
                        }

                        if (epgEntry.Producers != null)
                        {
                            foreach (Person person in epgEntry.Producers)
                                processPerson(xmlWriter, people, person);
                        }

                        if (epgEntry.Writers != null)
                        {
                            foreach (Person person in epgEntry.Writers)
                                processPerson(xmlWriter, people, person);
                        }

                        if (epgEntry.Presenters != null)
                        {
                            foreach (Person person in epgEntry.Presenters)
                                processPerson(xmlWriter, people, person);
                        }

                        if (epgEntry.GuestStars != null)
                        {
                            foreach (Person person in epgEntry.GuestStars)
                                processPerson(xmlWriter, people, person);
                        }
                    }
                }
            }
        }

        private static void processPerson(XmlWriter xmlWriter, Collection<Person> people, Person newPerson)
        {
            foreach (Person existingPerson in people)
            {
                if (existingPerson.Name == newPerson.Name)
                    return;
            }

            people.Add(newPerson);

            xmlWriter.WriteStartElement("Person");
            xmlWriter.WriteAttributeString("id", "prs" + people.Count);
            xmlWriter.WriteAttributeString("name", newPerson.Name);
            xmlWriter.WriteAttributeString("uid", "!Person!" + newPerson.Name);
            xmlWriter.WriteEndElement();
        }

        private static void processSeries(XmlWriter xmlWriter)
        {
            series = new Collection<string>();

            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (station.Included)
                {
                    foreach (EPGEntry epgEntry in station.EPGCollection)
                    {
                        string seriesLink = processEpisode(series, epgEntry);
                        if (seriesLink != null)
                        {
                            xmlWriter.WriteStartElement("SeriesInfo");
                            xmlWriter.WriteAttributeString("id", "si" + series.Count);
                            xmlWriter.WriteAttributeString("uid", "!Series!" + seriesLink);
                            xmlWriter.WriteAttributeString("title", epgEntry.EventName);
                            xmlWriter.WriteAttributeString("shortTitle", epgEntry.EventName);

                            if (epgEntry.SeriesDescription != null)
                            {
                                xmlWriter.WriteAttributeString("description", epgEntry.SeriesDescription);
                                xmlWriter.WriteAttributeString("shortDescription", epgEntry.SeriesDescription);
                            }
                            else
                            {
                                xmlWriter.WriteAttributeString("description", epgEntry.EventName);
                                xmlWriter.WriteAttributeString("shortDescription", epgEntry.EventName);
                            }

                            if (epgEntry.SeriesStartDate != null)
                                xmlWriter.WriteAttributeString("startAirdate", convertDateTimeToString(epgEntry.SeriesStartDate.Value, false));
                            else
                                xmlWriter.WriteAttributeString("startAirdate", convertDateTimeToString(DateTime.MinValue, false));

                            if (epgEntry.SeriesEndDate != null)
                                xmlWriter.WriteAttributeString("endAirdate", convertDateTimeToString(epgEntry.SeriesEndDate.Value, false));
                            else
                                xmlWriter.WriteAttributeString("endAirdate", convertDateTimeToString(DateTime.MinValue, false));
                            
                            setGuideImage(xmlWriter, epgEntry);
       
                            xmlWriter.WriteEndElement();
                        }
                    }
                }
            }
        }

        private static string processEpisode(Collection<string> series, EPGEntry epgEntry)
        {
            string newSeriesLink = getSeriesLink(epgEntry);
            if (newSeriesLink == null)
                return (null);

            if (series.Contains(newSeriesLink))
                return (null);

            series.Add(newSeriesLink);

            return (newSeriesLink);
        }

        private static void processPrograms(XmlWriter xmlWriter)
        {
            int programNumber = 1;

            if (OptionEntry.IsDefined(OptionName.UseWmcRepeatCheck) || OptionEntry.IsDefined(OptionName.UseWmcRepeatCheckBroadcast))
            {
                foreach (TVStation station in RunParameters.Instance.StationCollection)
                {
                    if (station.Included)
                    {
                        foreach (EPGEntry epgEntry in station.EPGCollection)
                        {
                            epgEntry.UniqueIdentifier = getProgramIdentifier(epgEntry);

                            int index = uniqueIdentifiers.IndexOf(epgEntry.UniqueIdentifier);
                            if (index != -1)
                            {
                                uniqueIdentifiers.RemoveAt(index);
                                uniqueIdentifiers.Insert(index, string.Empty);
                            }

                            uniqueIdentifiers.Add(epgEntry.UniqueIdentifier);
                        }
                    }
                }

                foreach (TVStation station in RunParameters.Instance.StationCollection)
                {
                    if (station.Included)
                    {
                        totalEntriesProcessed += station.EPGCollection.Count;

                        foreach (EPGEntry epgEntry in station.EPGCollection)
                        {
                            if (uniqueIdentifiers[programNumber - 1] != string.Empty)
                                processProgram(xmlWriter, programNumber, epgEntry, uniqueIdentifiers[programNumber - 1]);
                            programNumber++;
                        }
                    }
                }
            }
            else
            {
                foreach (TVStation station in RunParameters.Instance.StationCollection)
                {
                    if (station.Included)
                    {
                        totalEntriesProcessed += station.EPGCollection.Count;

                        foreach (EPGEntry epgEntry in station.EPGCollection)
                        {
                            if (processProgram(xmlWriter, programNumber, epgEntry, getProgramIdentifier(epgEntry)))
                                programNumber++;
                        }
                    }
                }
            }
        }

        private static bool processProgram(XmlWriter xmlWriter, int programNumber, EPGEntry epgEntry, string uniqueIdentifier)
        {
            if (OptionEntry.IsDefined(OptionName.NoInvalidEntries))
            {
                if (epgEntry.Duration.TotalSeconds < 1)
                {
                    invalidTimes++;
                    return false;
                }
            }

            isSpecial = false;
            isMovie = false;
            isSports = false;
            isKids = false;
            isNews = false; 

            xmlWriter.WriteStartElement("Program");
            xmlWriter.WriteAttributeString("id", "prg" + programNumber);
            xmlWriter.WriteAttributeString("uid", "!Program!" + uniqueIdentifier);

            if (epgEntry.EventName != null)
                xmlWriter.WriteAttributeString("title", epgEntry.EventName);
            else
                xmlWriter.WriteAttributeString("title", "No Title");

            if (epgEntry.ShortDescription != null)
                xmlWriter.WriteAttributeString("description", epgEntry.ShortDescription);
            else
            {
                if (epgEntry.EventName != null)
                    xmlWriter.WriteAttributeString("description", epgEntry.EventName);
                else
                    xmlWriter.WriteAttributeString("description", "No Description");
            }
            
            if (epgEntry.EventSubTitle != null)
                xmlWriter.WriteAttributeString("episodeTitle", epgEntry.EventSubTitle);

            if (epgEntry.HasAdult)
                xmlWriter.WriteAttributeString("hasAdult", "1");
            if (epgEntry.HasGraphicLanguage)
                xmlWriter.WriteAttributeString("hasGraphicLanguage", "1");
            if (epgEntry.HasGraphicViolence)
                xmlWriter.WriteAttributeString("hasGraphicViolence", "1");
            if (epgEntry.HasNudity)
                xmlWriter.WriteAttributeString("hasNudity", "1");
            if (epgEntry.HasStrongSexualContent)
                xmlWriter.WriteAttributeString("hasStrongSexualContent", "1");
            
            if (epgEntry.MpaaParentalRating != null)
            {
                switch (epgEntry.MpaaParentalRating)
                {
                    case "G":
                        xmlWriter.WriteAttributeString("mpaaRating", "1");
                        break;
                    case "PG":
                        xmlWriter.WriteAttributeString("mpaaRating", "2");
                        break;
                    case "PG13":
                        xmlWriter.WriteAttributeString("mpaaRating", "3");
                        break;
                    case "R":
                        xmlWriter.WriteAttributeString("mpaaRating", "4");
                        break;
                    case "NC17":
                        xmlWriter.WriteAttributeString("mpaaRating", "5");
                        break;
                    case "X":
                        xmlWriter.WriteAttributeString("mpaaRating", "6");
                        break;
                    case "NR":
                        xmlWriter.WriteAttributeString("mpaaRating", "7");
                        break;
                    case "AO":
                        xmlWriter.WriteAttributeString("mpaaRating", "8");
                        break;
                    default:
                        break;
                }
            }

            if (epgEntry.EventCategory != null)
                processCategoryKeywords(xmlWriter, epgEntry.EventCategory.GetDescription(EventCategoryMode.Wmc));
            else
                xmlWriter.WriteAttributeString("keywords", "");

            if (epgEntry.Date != null && epgEntry.Date.Length > 3)
                xmlWriter.WriteAttributeString("year", epgEntry.Date.Substring(0, 4));

            if (epgEntry.SeasonNumber != -1)
                xmlWriter.WriteAttributeString("seasonNumber", epgEntry.SeasonNumber.ToString());
            if (epgEntry.EpisodeNumber != -1)
                xmlWriter.WriteAttributeString("episodeNumber", epgEntry.EpisodeNumber.ToString());

            if (!epgEntry.IsNew)
                xmlWriter.WriteAttributeString("originalAirdate", convertDateTimeToString(epgEntry.PreviousPlayDate, false));
            else
            {
                if (epgEntry.IdPrefix != "SH")
                    xmlWriter.WriteAttributeString("originalAirdate", convertDateTimeToString(epgEntry.StartTime, false));
                else
                    xmlWriter.WriteAttributeString("originalAirdate", convertDateTimeToString(epgEntry.PreviousPlayDate, false));
            }

            processSeries(xmlWriter, epgEntry);

            if (epgEntry.StarRating != null)
            {
                switch (epgEntry.StarRating)
                {
                    case "+":
                        xmlWriter.WriteAttributeString("halfStars", "1");
                        break;
                    case "*":
                        xmlWriter.WriteAttributeString("halfStars", "2");
                        break;
                    case "*+":
                        xmlWriter.WriteAttributeString("halfStars", "3");
                        break;
                    case "**":
                        xmlWriter.WriteAttributeString("halfStars", "4");
                        break;
                    case "**+":
                        xmlWriter.WriteAttributeString("halfStars", "5");
                        break;
                    case "***":
                        xmlWriter.WriteAttributeString("halfStars", "6");
                        break;
                    case "***+":
                        xmlWriter.WriteAttributeString("halfStars", "7");
                        break;
                    case "****":
                        xmlWriter.WriteAttributeString("halfStars", "8");
                        if (OptionEntry.IsDefined(OptionName.WmcStarSpecial))
                            isSpecial = true;
                        break;
                    default:
                        break;
                }
            }

            xmlWriter.WriteAttributeString("isGeneric", (epgEntry.IsGeneric ? "1" : "0"));
            if (epgEntry.IsGeneric)
                totalGenericProgrammes++;

            if (epgEntry.IsMovie)
                isMovie = true;
            if (epgEntry.IsSports)
                isSports = true;
            processCategoryAttributes(xmlWriter);
            
            setGuideImage(xmlWriter, epgEntry);

            if (epgEntry.Cast != null && epgEntry.Cast.Count != 0)
                processCast(xmlWriter, epgEntry.Cast);

            if (epgEntry.Directors != null && epgEntry.Directors.Count != 0)
                processDirectors(xmlWriter, epgEntry.Directors);

            if (epgEntry.Producers != null && epgEntry.Producers.Count != 0)
                processProducers(xmlWriter, epgEntry.Producers);

            if (epgEntry.Writers != null && epgEntry.Writers.Count != 0)
                processWriters(xmlWriter, epgEntry.Writers);

            if (epgEntry.Presenters != null && epgEntry.Presenters.Count != 0)
                processPresenters(xmlWriter, epgEntry.Presenters);

            if (epgEntry.GuestStars != null && epgEntry.GuestStars.Count != 0)
                processGuestStars(xmlWriter, epgEntry.GuestStars);

            xmlWriter.WriteEndElement();

            return (true);
        }

        private static string getProgramIdentifier(EPGEntry epgEntry)
        {
            if (!OptionEntry.IsDefined(OptionName.UseWmcRepeatCheck) && !OptionEntry.IsDefined(OptionName.UseWmcRepeatCheckBroadcast))
            {
                return (epgEntry.OriginalNetworkID + ":" + epgEntry.TransportStreamID + ":" + epgEntry.ServiceID + ":" +
                    getUtcTime(epgEntry.StartTime).ToString("dd-MM-yyyy+HH-mm-ss"));
            }

            if (OptionEntry.IsDefined(OptionName.UseWmcRepeatCheck))
                epgEntry.UniqueIdentifier = getIdentifierFromTitle(epgEntry);
            else
                epgEntry.UniqueIdentifier = getIdentifierFromCrids(epgEntry);

            string uidTitle = UidEntry.FindEntry(epgEntry.UniqueIdentifier);
            if (uidTitle != null)
            {
                if (uidTitle != epgEntry.EventName)
                {
                    if (DebugEntry.IsDefined(DebugName.LogMxfWarnings))
                        Logger.Instance.Write("<w> Duplicate UID generated for '" + uidTitle + "' and '" + epgEntry.EventName + "'");
                }
                
                return epgEntry.UniqueIdentifier;
            }

            UidEntry.AddEntry(epgEntry.UniqueIdentifier, epgEntry.EventName);
                                    
            return (epgEntry.UniqueIdentifier);
        }

        private static string getIdentifierFromTitle(EPGEntry epgEntry)
        {
            string matchString;

            if (epgEntry.SeasonNumber != -1 && epgEntry.EpisodeNumber != -1)
                matchString = epgEntry.EventName + ":" + epgEntry.SeasonNumber + ":" + epgEntry.EpisodeNumber;
            else
                matchString = epgEntry.EventName + ":" + epgEntry.ShortDescription;

            if (sha256 == null)
                sha256 = SHA256.Create();

            matchString = matchString.ToLowerInvariant();
            
            string uniqueId = Convert.ToBase64String(sha256.ComputeHash(Encoding.ASCII.GetBytes(matchString)));

            ReferenceIdEntry referenceIdEntry = ReferenceIdEntry.FindTitleEntry(uniqueId);
            if (referenceIdEntry != null)
            {
                if (referenceIdEntry.Title != matchString)
                    referenceIdEntry.AddOtherTitle(matchString);
            }
            else
                ReferenceIdEntry.AddEntry(uniqueId, matchString);

            return uniqueId;
        }

        private static string getIdentifierFromCrids(EPGEntry epgEntry)
        {
            string eventName = string.IsNullOrWhiteSpace(epgEntry.EventName) ? "No Title" : epgEntry.EventName;
            string tickString = string.IsNullOrWhiteSpace(epgEntry.IdentitySuffix) ? string.Empty : ":" + epgEntry.IdentitySuffix;                     

            if (!string.IsNullOrEmpty(epgEntry.SeasonCrid))
            {
                totalSeriesIds++;

                ReferenceIdEntry referenceIdEntry = ReferenceIdEntry.FindReferenceIdEntry(epgEntry.EventName);
                if (referenceIdEntry != null)
                {
                    if (referenceIdEntry.ReferenceId != epgEntry.SeasonCrid)
                        referenceIdEntry.AddOtherReferenceId(epgEntry.SeasonCrid);
                }
                else
                    ReferenceIdEntry.AddEntry(epgEntry.SeasonCrid, epgEntry.EventName);

                if (!string.IsNullOrEmpty(epgEntry.EpisodeCrid))
                {
                    totalEpisodeIds++;
                    return getBase64String(epgEntry.SeasonCrid + ":" + epgEntry.EpisodeCrid + tickString, epgEntry.UseBase64Crids);                    
                }
                else
                    return getBase64String(epgEntry.SeasonCrid + ":" + tickString, epgEntry.UseBase64Crids);
            }
            else
            {
                if (!string.IsNullOrEmpty(epgEntry.EpisodeCrid))
                {
                    totalEpisodeIds++;
                    return getBase64String(eventName + "::" + epgEntry.EpisodeCrid + tickString, epgEntry.UseBase64Crids);
                }
            }                
                
            if (!string.IsNullOrEmpty(epgEntry.SeriesId))
            {
                totalSeriesIds++;

                ReferenceIdEntry referenceIdEntry = ReferenceIdEntry.FindReferenceIdEntry(epgEntry.EventName);
                if (referenceIdEntry != null)
                {
                    if (referenceIdEntry.ReferenceId != epgEntry.SeriesId)
                        referenceIdEntry.AddOtherReferenceId(epgEntry.SeasonCrid);
                }
                else
                    ReferenceIdEntry.AddEntry(epgEntry.SeriesId, epgEntry.EventName);

                if (!string.IsNullOrEmpty(epgEntry.EpisodeId))
                {
                    totalEpisodeIds++;
                    return getBase64String((epgEntry.IdPrefix == null ? "" : epgEntry.IdPrefix) + epgEntry.SeriesId + ":" + epgEntry.EpisodeId + tickString, epgEntry.UseBase64Crids);
                }
                else
                    return getBase64String((epgEntry.IdPrefix == null ? "" : epgEntry.IdPrefix) + epgEntry.SeriesId + ":" + tickString, epgEntry.UseBase64Crids);
            }
            else
            {
                if (!string.IsNullOrEmpty(epgEntry.EpisodeId))
                {
                    totalEpisodeIds++;
                    return getBase64String(eventName + "::" + epgEntry.EpisodeId + tickString, epgEntry.UseBase64Crids);
                }
            }        

            if (epgEntry.SeasonNumber != -1)
            {
                if (epgEntry.EpisodeNumber != -1)
                    return getBase64String(eventName + ":" + epgEntry.SeasonNumber + ":" + epgEntry.EpisodeNumber + tickString, epgEntry.UseBase64Crids);                
                else
                    return getBase64String(eventName + ":" + epgEntry.SeasonNumber + ":" + tickString, epgEntry.UseBase64Crids);
            }
            else
            {
                if (epgEntry.EpisodeNumber != -1)
                    return getBase64String(eventName + "::" + epgEntry.EpisodeNumber + tickString, epgEntry.UseBase64Crids);
            }

            return getBase64String(eventName, epgEntry.UseBase64Crids);            
        }

        private static void processCategoryKeywords(XmlWriter xmlWriter, string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                xmlWriter.WriteAttributeString("keywords", "");
                return;
            }

            string[] parts = processSpecialCategories(xmlWriter, category);
            if (parts == null)
            {
                xmlWriter.WriteAttributeString("keywords", "");
                return;
            }

            /*if (parts.Length < 2)
                return;*/

            StringBuilder keywordString = new StringBuilder();    

            int groupNumber = 1;            

            foreach (KeywordGroup group in groups)
            {
                if (group.Name == parts[0])
                {                    
                    keywordString.Append("k" + groupNumber);                    

                    int keywordNumber = groupNumber * 100;

                    if (parts.Length < 2)
                        keywordString.Append(",k" + (keywordNumber + 1));
                    else
                    {
                        for (int keywordIndex = 1; keywordIndex < group.Keywords.Count; keywordIndex++)
                        {
                            keywordNumber++;

                            for (int partsIndex = 1; partsIndex < parts.Length; partsIndex++)
                            {
                                if (group.Keywords[keywordIndex] == parts[partsIndex])
                                    keywordString.Append(",k" + keywordNumber);
                            }
                        }
                    }

                    xmlWriter.WriteAttributeString("keywords", keywordString.ToString());
                    return;
                }
                groupNumber++;
            }

            xmlWriter.WriteAttributeString("keywords", "");
        }

        private static string[] processSpecialCategories(XmlWriter xmlWriter, string category)
        {
            Collection<string> specialCategories = new Collection<string>();

            string[] parts = category.Split(new string[] { "," }, StringSplitOptions.None);

            foreach (string part in parts)
            {
                string specialCategory = getSpecialCategory(part);
                if (specialCategory != null)
                    specialCategories.Add(specialCategory);
            }

            if (specialCategories.Count == parts.Length)
                return (null);

            string[] editedParts = new string[parts.Length - specialCategories.Count];
            int index = 0;

            foreach (string part in parts)
            {
                string specialCategory = getSpecialCategory(part);
                if (specialCategory == null)
                {
                    editedParts[index] = part;
                    index++;
                }

            }

            return (editedParts);
        }

        private static string getSpecialCategory(string category)
        {
            switch (category.ToUpperInvariant())
            {
                case "ISMOVIE":
                    isMovie = true;
                    return ("isMovie");
                case "ISSPECIAL":
                    isSpecial = true;
                    return ("isSpecial");
                case "ISSPORTS":
                    isSports = true;
                    return ("isSports");
                case "ISNEWS":
                    isNews = true;
                    return ("isNews");
                case "ISKIDS":
                    isKids = true;
                    return ("isKids");
                default:
                    return (null);
            }
        }

        private static void addSpecialCategory(Collection<string> specialCategories, string newCategory)
        {
            foreach (string oldCategory in specialCategories)
            {
                if (oldCategory == newCategory)
                    return;
            }

            specialCategories.Add(newCategory);
        }

        private static void processCast(XmlWriter xmlWriter, Collection<Person> cast)
        {
            if (people == null)
                return;

            foreach (Person actor in cast)
            {
                xmlWriter.WriteStartElement("ActorRole");
                xmlWriter.WriteAttributeString("person", "prs" + findPerson(people, actor.Name));

                if (actor.Rank != 0)
                    xmlWriter.WriteAttributeString("rank", actor.Rank.ToString());

                if (!string.IsNullOrWhiteSpace(actor.Character))
                    xmlWriter.WriteAttributeString("character", actor.Character);

                xmlWriter.WriteEndElement();
            }
        }

        private static void processDirectors(XmlWriter xmlWriter, Collection<Person> directors)
        {
            if (people == null)
                return;

            foreach (Person director in directors)
            {
                xmlWriter.WriteStartElement("DirectorRole");
                xmlWriter.WriteAttributeString("person", "prs" + findPerson(people, director.Name));
                if (director.Rank != 0)
                    xmlWriter.WriteAttributeString("rank", director.Rank.ToString());
                xmlWriter.WriteEndElement();
            }
        }

        private static void processProducers(XmlWriter xmlWriter, Collection<Person> producers)
        {
            if (people == null)
                return;

            foreach (Person producer in producers)
            {
                xmlWriter.WriteStartElement("ProducerRole");
                xmlWriter.WriteAttributeString("person", "prs" + findPerson(people, producer.Name));
                if (producer.Rank != 0)
                    xmlWriter.WriteAttributeString("rank", producer.Rank.ToString());
                xmlWriter.WriteEndElement();
            }
        }

        private static void processWriters(XmlWriter xmlWriter, Collection<Person> writers)
        {
            if (people == null)
                return;

            foreach (Person writer in writers)
            {
                xmlWriter.WriteStartElement("WriterRole");
                xmlWriter.WriteAttributeString("person", "prs" + findPerson(people, writer.Name));
                if (writer.Rank != 0)
                    xmlWriter.WriteAttributeString("rank", writer.Rank.ToString());
                xmlWriter.WriteEndElement();
            }
        }

        private static void processPresenters(XmlWriter xmlWriter, Collection<Person> presenters)
        {
            if (people == null)
                return;

            foreach (Person presenter in presenters)
            {
                xmlWriter.WriteStartElement("HostRole");
                xmlWriter.WriteAttributeString("person", "prs" + findPerson(people, presenter.Name));
                if (presenter.Rank != 0)
                    xmlWriter.WriteAttributeString("rank", presenter.Rank.ToString());
                xmlWriter.WriteEndElement();
            }
        }

        private static void processGuestStars(XmlWriter xmlWriter, Collection<Person> guestStars)
        {
            if (people == null)
                return;

            foreach (Person guestStar in guestStars)
            {
                xmlWriter.WriteStartElement("GuestActorRole");
                xmlWriter.WriteAttributeString("person", "prs" + findPerson(people, guestStar.Name));                
                if (guestStar.Rank != 0)
                    xmlWriter.WriteAttributeString("rank", guestStar.Rank.ToString());
                xmlWriter.WriteEndElement();
            }
        }

        private static int findPerson(Collection<Person> people, string name)
        {
            foreach (Person person in people)
            {
                if (person.Name == name)
                    return people.IndexOf(person) + 1;
            }

            throw new InvalidOperationException("Person lost");
        }

        private static void processSeries(XmlWriter xmlWriter, EPGEntry epgEntry)
        {
            string seriesLink = getSeriesLink(epgEntry);
            if (seriesLink == null)
            {
                xmlWriter.WriteAttributeString("isSeries", "0");
                return;
            }

            int index = series.IndexOf(seriesLink);
            if (index != -1)
            {
                xmlWriter.WriteAttributeString("isSeries", "1");
                xmlWriter.WriteAttributeString("series", "si" + (index + 1).ToString());
            }
            else
                xmlWriter.WriteAttributeString("isSeries", "0");

            /*foreach (string oldSeriesLink in series)
            {
                if (oldSeriesLink == seriesLink)
                {
                    xmlWriter.WriteAttributeString("isSeries", "1");
                    xmlWriter.WriteAttributeString("series", "si" + (series.IndexOf(oldSeriesLink) + 1).ToString());

                    return;
                }
            }

            xmlWriter.WriteAttributeString("isSeries", "0");*/
        }

        private static void processCategoryAttributes(XmlWriter xmlWriter)
        {
            if (isSpecial)
                xmlWriter.WriteAttributeString("isSpecial", "1");
            else
                xmlWriter.WriteAttributeString("isSpecial", "0");

            if (isMovie)
                xmlWriter.WriteAttributeString("isMovie", "1");
            else
                xmlWriter.WriteAttributeString("isMovie", "0");

            if (isSports)
                xmlWriter.WriteAttributeString("isSports", "1");
            else
                xmlWriter.WriteAttributeString("isSports", "0");

            if (isNews)
                xmlWriter.WriteAttributeString("isNews", "1");
            else
                xmlWriter.WriteAttributeString("isNews", "0");

            if (isKids)
                xmlWriter.WriteAttributeString("isKids", "1");
            else
                xmlWriter.WriteAttributeString("isKids", "0");
        }

        private static void setGuideImage(XmlWriter xmlWriter, EPGEntry epgEntry)
        {
            if (epgEntry.Poster != null)
            {
                if (programImages != null)
                {
                    foreach (string imageId in programImages)
                    {
                        if (imageId == epgEntry.Poster)
                        {
                            xmlWriter.WriteAttributeString("guideImage", "i-" + imageId);
                            return;
                        }
                    }
                }

                return;
            }

            if (epgEntry.PosterPath != null)
            {
                if (importedImages != null)
                {
                    foreach (ImportedImage importedImage in importedImages)
                    {
                        if (importedImage.Path == epgEntry.PosterPath)
                        {
                            xmlWriter.WriteAttributeString("guideImage", "i-" + importedImage.Guid);
                            return;
                        }
                    }
                }

                return;
            }
        }

        private static void processAffiliates(XmlWriter xmlWriter)
        {
            Collection<string> affiliatesProcessed = new Collection<string>();
            bool firstDummy = true;

            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (station.Included)
                {
                    if (!string.IsNullOrWhiteSpace(station.Affiliate))
                    {
                        if (!affiliatesProcessed.Contains(station.Affiliate))
                        {
                            xmlWriter.WriteStartElement("Affiliate");
                            xmlWriter.WriteAttributeString("name", station.Affiliate);
                            xmlWriter.WriteAttributeString("uid", "!Affiliate!" + station.Affiliate);
                            xmlWriter.WriteEndElement();

                            affiliatesProcessed.Add(station.Affiliate);
                        }
                    }
                    else
                    {
                        if (!OptionEntry.IsDefined(RunParameters.Instance.Options, OptionName.NoWmcDummyAffiliates))
                        {
                            if (firstDummy)
                            {
                                xmlWriter.WriteStartElement("Affiliate");
                                xmlWriter.WriteAttributeString("name", importName);
                                xmlWriter.WriteAttributeString("uid", "!Affiliate!" + importReference);
                                xmlWriter.WriteEndElement();

                                firstDummy = false;
                            }

                            if (duplicateStationNames.Contains(station.Name))
                            {
                                xmlWriter.WriteStartElement("Affiliate");
                                xmlWriter.WriteAttributeString("name", importName + "-" + station.ServiceID);
                                xmlWriter.WriteAttributeString("uid", "!Affiliate!" + importReference + "-" + station.ServiceID);
                                xmlWriter.WriteEndElement();
                            }
                        }
                    }
                }
            }
        }

        private static void processServices(XmlWriter xmlWriter)
        {
            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (station.Included)
                {
                    xmlWriter.WriteStartElement("Service");
                    xmlWriter.WriteAttributeString("id", "s" + (RunParameters.Instance.StationCollection.IndexOf(station) + 1));
                    xmlWriter.WriteAttributeString("uid", "!Service!" +
                        station.OriginalNetworkID + ":" + 
                        station.TransportStreamID + ":" + 
                        station.ServiceID);

                    string serviceName = string.IsNullOrWhiteSpace(station.NewName) ? station.Name : station.NewName;
                    xmlWriter.WriteAttributeString("name", serviceName);

                    if (!string.IsNullOrWhiteSpace(station.ImportCallSign))
                        xmlWriter.WriteAttributeString("callSign", station.ImportCallSign);
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(station.WMCCallSign))
                            xmlWriter.WriteAttributeString("callSign", station.WMCCallSign);
                        else
                            xmlWriter.WriteAttributeString("callSign", serviceName);
                    }

                    if (!string.IsNullOrWhiteSpace(station.Affiliate))
                        xmlWriter.WriteAttributeString("affiliate", "!Affiliate!" + station.Affiliate);
                    else
                    {
                        if (!OptionEntry.IsDefined(RunParameters.Instance.Options, OptionName.NoWmcDummyAffiliates))
                        {
                            if (!duplicateStationNames.Contains(station.Name))
                                xmlWriter.WriteAttributeString("affiliate", "!Affiliate!" + importReference);
                            else
                                xmlWriter.WriteAttributeString("affiliate", "!Affiliate!" + importReference + "-" + station.ServiceID);
                        }
                    }

                    if (stationImages != null)
                    {
                        int imageIndex = 1;

                        foreach (int imageServiceID in stationImages)
                        {
                            if (imageServiceID == station.ServiceID)
                            {
                                xmlWriter.WriteAttributeString("logoImage", "i" + imageIndex.ToString());
                                break;
                            }

                            imageIndex++;
                        }
                    }

                    if (station.ImagePath != null)
                    {
                        if (importedImages != null)
                        {
                            foreach (ImportedImage importedImage in importedImages)
                            {
                                if (importedImage.Path == station.ImagePath)
                                {
                                    xmlWriter.WriteAttributeString("logoImage", "i-" + importedImage.Guid);
                                    break;
                                }
                            }
                        }
                    }

                    xmlWriter.WriteEndElement();
                }
            }
        }

        private static void processSchedules(XmlWriter xmlWriter)
        {
            int programNumber = 1;

            adjustOldStartTimes();

            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (station.Included)
                {
                    xmlWriter.WriteStartElement("ScheduleEntries");
                    xmlWriter.WriteAttributeString("service", "s" + (RunParameters.Instance.StationCollection.IndexOf(station) + 1));
                    
                    foreach (EPGEntry epgEntry in station.EPGCollection)
                    {
                        if (!OptionEntry.IsDefined(OptionName.NoInvalidEntries) || (OptionEntry.IsDefined(OptionName.NoInvalidEntries) && epgEntry.Duration.TotalSeconds > 0))
                        {
                            xmlWriter.WriteStartElement("ScheduleEntry");

                            if (OptionEntry.IsDefined(OptionName.UseWmcRepeatCheck) || OptionEntry.IsDefined(OptionName.UseWmcRepeatCheckBroadcast))
                                xmlWriter.WriteAttributeString("program", "prg" + (uniqueIdentifiers.IndexOf(epgEntry.UniqueIdentifier) + 1));
                            else
                                xmlWriter.WriteAttributeString("program", "prg" + programNumber);

                            xmlWriter.WriteAttributeString("startTime", convertDateTimeToString(epgEntry.StartTime, true));
                            xmlWriter.WriteAttributeString("duration", epgEntry.Duration.TotalSeconds.ToString());

                            if (epgEntry.VideoQuality != null && epgEntry.VideoQuality.ToLowerInvariant() == "hdtv")
                                xmlWriter.WriteAttributeString("isHdtv", "1");

                            if (epgEntry.IsLive)
                                xmlWriter.WriteAttributeString("isLive", "1");

                            if (epgEntry.AudioQuality != null)
                            {
                                switch (epgEntry.AudioQuality.ToLowerInvariant())
                                {
                                    case "mono":
                                        xmlWriter.WriteAttributeString("audioFormat", "1");
                                        break;
                                    case "stereo":
                                        xmlWriter.WriteAttributeString("audioFormat", "2");
                                        break;
                                    case "dolby":
                                    case "surround":
                                        xmlWriter.WriteAttributeString("audioFormat", "3");
                                        break;
                                    case "dolby digital":
                                        xmlWriter.WriteAttributeString("audioFormat", "4");
                                        break;
                                    default:
                                        break;
                                }
                            }

                            xmlWriter.WriteEndElement();

                            programNumber++;
                        }                        
                    }

                    xmlWriter.WriteEndElement();
                }
            }
        }

        private static void processLineUps(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("Lineup");
            xmlWriter.WriteAttributeString("id", "l1");
            xmlWriter.WriteAttributeString("uid", "!Lineup!" + importName);
            xmlWriter.WriteAttributeString("name", importName);
            xmlWriter.WriteAttributeString("primaryProvider", "!MCLineup!MainLineup");

            xmlWriter.WriteStartElement("channels");

            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (station.Included)
                {
                    xmlWriter.WriteStartElement("Channel");

                    if (station.WMCUniqueID != null)
                        xmlWriter.WriteAttributeString("uid", station.WMCUniqueID);
                    else
                    {
                        string uidName = "MainLineup";

                        switch (station.StationType)
                        {
                            case TVStationType.Atsc:
                                if (station.TransportStreamID != 0)
                                    xmlWriter.WriteAttributeString("uid", "!Channel!" + uidName + "!" + station.TransportStreamID + "_" + station.ServiceID);
                                else
                                    xmlWriter.WriteAttributeString("uid", "!Channel!" + uidName + "!" + station.ServiceID);
                                break;
                            default:
                                xmlWriter.WriteAttributeString("uid", "!Channel!" + uidName + "!" + station.OriginalNetworkID + "_" + station.TransportStreamID + "_" + station.ServiceID);
                                break;
                        }
                    }
                    
                    xmlWriter.WriteAttributeString("lineup", "l1");
                    xmlWriter.WriteAttributeString("service", "s" + (RunParameters.Instance.StationCollection.IndexOf(station) + 1));
                    
                    if (OptionEntry.IsDefined(OptionName.AutoMapEpg))
                    {
                        if (!string.IsNullOrWhiteSpace(station.WMCMatchName))
                            xmlWriter.WriteAttributeString("matchName", station.WMCMatchName);
                        else
                        {                            
                            if (station.StationType == TVStationType.Dvb)
                            {
                                switch (station.TunerType)
                                {
                                    case TunerType.Satellite:
                                        if (station.Satellite != null)
                                            xmlWriter.WriteAttributeString("matchName", "DVBS:" + station.Satellite.Longitude + ":" + station.Frequency + ":" + station.OriginalNetworkID + ":" + station.TransportStreamID + ":" + station.ServiceID);
                                        else
                                            Logger.Instance.Write("Station " + station.FullDescription + " satellite not defined - no match name output to MXF file");
                                        break;
                                    case TunerType.Terrestrial:
                                        xmlWriter.WriteAttributeString("matchName", "DVBT:" + station.Frequency + ":" + station.OriginalNetworkID + ":" + station.TransportStreamID + ":" + station.ServiceID);
                                        break;
                                    case TunerType.Cable:
                                        xmlWriter.WriteAttributeString("matchName", "DVBC:" + station.Frequency + ":" + station.OriginalNetworkID + ":" + station.TransportStreamID + ":" + station.ServiceID);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {                                                              
                                if (station.TransportStreamID != 0)
                                    xmlWriter.WriteAttributeString("matchName", "OC:" + station.TransportStreamID + ":" + station.ServiceID);
                                else
                                    xmlWriter.WriteAttributeString("matchName", "OC:" + station.ServiceID);                                  
                            }
                        }
                    }

                    if (station.MinorChannelNumber != -1)
                    {
                        xmlWriter.WriteAttributeString("number", station.TransportStreamID.ToString());
                        xmlWriter.WriteAttributeString("subNumber", station.MinorChannelNumber.ToString());
                    }
                    else
                    {
                        if (station.LogicalChannelNumber != -1)
                            xmlWriter.WriteAttributeString("number", station.LogicalChannelNumber.ToString());
                    }

                    xmlWriter.WriteEndElement();
                }
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
        }

        private static string runImportUtility(string fileName)
        {
            string runDirectory = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "ehome");
            Logger.Instance.Write("Running Windows Media Centre import utility LoadMXF from " + runDirectory);
 
            importProcess = new Process();

            importProcess.StartInfo.FileName = Path.Combine(runDirectory, "LoadMXF.exe");
            importProcess.StartInfo.WorkingDirectory = runDirectory + Path.DirectorySeparatorChar;
            importProcess.StartInfo.Arguments = @"-v -i " + '"' + fileName + '"';
            importProcess.StartInfo.UseShellExecute = false;
            importProcess.StartInfo.CreateNoWindow = true;
            importProcess.StartInfo.RedirectStandardOutput = true;
            importProcess.StartInfo.RedirectStandardError = true;
            importProcess.EnableRaisingEvents = true;
            importProcess.OutputDataReceived += new DataReceivedEventHandler(importProcessOutputDataReceived);
            importProcess.ErrorDataReceived += new DataReceivedEventHandler(importProcessErrorDataReceived);
            importProcess.Exited += new EventHandler(importProcessExited);

            try
            {
                importProcess.Start();

                importProcess.BeginOutputReadLine();
                importProcess.BeginErrorReadLine();

                while (!importExited)
                    Thread.Sleep(500);

                Logger.Instance.Write("Windows Media Centre import utility LoadMXF has completed: exit code " + importProcess.ExitCode);
                if (importProcess.ExitCode == 0)
                    return (null);
                else
                    return ("Failed to load Windows Media Centre data: reply code " + importProcess.ExitCode);
            }
            catch (Exception e)
            {
                Logger.Instance.Write("<e> Failed to run the Windows Media Centre import utility LoadMXF");
                Logger.Instance.Write("<e> " + e.Message);
                return ("Failed to load Windows Media Centre data due to an exception");
            }
        }

        private static void importProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;

            Logger.Instance.Write("LoadMXF message: " + e.Data);
        }

        private static void importProcessErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;

            Logger.Instance.Write("<e> LoadMXF error: " + e.Data);
        }

        private static void importProcessExited(object sender, EventArgs e)
        {
            importExited = true;
        }

        private static void runStandardTasks()
        {
            if (!OptionEntry.IsDefined(RunParameters.Instance.Options, OptionName.RunWmcTasks))
                return;
            
            string scheduler = "schtasks.exe";
            string schedulerReindexTaskName = "Task scheduler reindex task";
            string schedulerReindexArgument = "/run /tn \"Microsoft\\Windows\\Media Center\\ReindexSearchRoot\"";
            string schedulerPvrTaskName = "Task scheduler update PVR task";
            string schedulerPvrArgument = "/run /tn \"Microsoft\\Windows\\Media Center\\PvrScheduleTask\"";

            string privateJobName = "Private job reindex";
            string privateJob = "ehPrivJob.exe";
            string privateJobArgument = "/DoReindexSearchRoot";

            string mcUpdateName = "WMC update PVR schedule";
            string mcUpdate = "mcUpdate.exe";
            string mcUpdateArgument = "-PvrSchedule";

            bool reply = runTask(privateJobName, privateJob, privateJobArgument, false);
            if (!reply)
                runTask(schedulerReindexTaskName, scheduler, schedulerReindexArgument, true);

            reply = runTask(mcUpdateName, mcUpdate, mcUpdateArgument, false);
            if (!reply)
                runTask(schedulerPvrTaskName, scheduler, schedulerPvrArgument, true);
        }

        private static bool runTask(string taskName, string exeName, string arguments, bool global)
        {
            string runDirectory = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "ehome");
            Logger.Instance.Write("Running Windows Media Centre standard task '" + taskName + "'");

            importProcess = new Process();

            if (!global)
                importProcess.StartInfo.FileName = Path.Combine(runDirectory, exeName);
            else
                importProcess.StartInfo.FileName = exeName;
            importProcess.StartInfo.WorkingDirectory = runDirectory + Path.DirectorySeparatorChar;
            importProcess.StartInfo.Arguments = arguments;
            importProcess.StartInfo.UseShellExecute = false;
            importProcess.StartInfo.CreateNoWindow = true;

            try
            {
                bool reply = importProcess.Start();
                if (reply)
                    Logger.Instance.Write("Windows Media Centre standard task has started");
                else
                    Logger.Instance.Write("Windows Media Center standard task has failed to start");
                return reply;
            }
            catch (Exception e)
            {
                Logger.Instance.Write("<e> Failed to run Windows Media Centre standard task");
                Logger.Instance.Write("<e> " + e.Message);
                return false;
            }
        }

        private static string convertDateTimeToString(DateTime dateTime, bool convertToUtc)
        {
            DateTime utcTime;
            
            if (convertToUtc)
                utcTime = getUtcTime(dateTime);
            else
                utcTime = dateTime;

            return (utcTime.Date.ToString("yyyy-MM-dd") + "T" +
                utcTime.Hour.ToString("00") + ":" +
                utcTime.Minute.ToString("00") + ":" +
                utcTime.Second.ToString("00"));            
        }

        private static DateTime getUtcTime(DateTime dateTime)
        {
            try
            {
                return(TimeZoneInfo.ConvertTimeToUtc(dateTime));
            }
            catch (ArgumentException e)
            {
                Logger.Instance.Write("<e> Local start date/time is invalid: " + dateTime);
                Logger.Instance.Write("<e> " + e.Message);
                Logger.Instance.Write("<e> Start time will be advanced by 1 hour");

                return(TimeZoneInfo.ConvertTimeToUtc(dateTime.AddHours(1)));
            }
        }

        private static void adjustOldStartTimes()
        {
            if (!DebugEntry.IsDefined(DebugName.AdjustStartTimes))
                return;

            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (station.Included && station.EPGCollection.Count > 0)
                {
                    TimeSpan offset = DateTime.Now - station.EPGCollection[0].StartTime;

                    foreach (EPGEntry epgEntry in station.EPGCollection)
                        epgEntry.StartTime = epgEntry.StartTime + offset;
                }
            }
        }

        private static string getSeriesLink(EPGEntry epgEntry)
        {
            if (!OptionEntry.IsDefined(OptionName.UseWmcRepeatCheckBroadcast))
                return (epgEntry.EventName.Replace("!", string.Empty).ToLowerInvariant());

            if (!string.IsNullOrEmpty(epgEntry.SeasonCrid) && !string.IsNullOrEmpty(epgEntry.EpisodeCrid))
                return getBase64String(epgEntry.SeasonCrid, epgEntry.UseBase64Crids);

            if (!string.IsNullOrEmpty(epgEntry.SeriesId) && !string.IsNullOrEmpty(epgEntry.EpisodeId))
            {
                if (epgEntry.IsSeries)
                    return getBase64String(epgEntry.SeriesId, epgEntry.UseBase64Crids);
                else
                    return null;
            }

            if (!string.IsNullOrEmpty(epgEntry.EpisodeCrid))
                return getBase64String(epgEntry.EventName.ToLowerInvariant() + ":" + epgEntry.EpisodeCrid, epgEntry.UseBase64Crids);

            if (!string.IsNullOrEmpty(epgEntry.EpisodeId))
                return getBase64String(epgEntry.EventName.ToLowerInvariant() + ":" + epgEntry.EpisodeId, epgEntry.UseBase64Crids);

            if (DebugEntry.IsDefined(DebugName.LogMxfWarnings))
                Logger.Instance.Write("<w> No series reference ID for " + epgEntry.EventName);
            
            return null;
        }

        private static string getNumber(string text)
        {
            if (text.Trim().Length == 0)
                return (string.Empty);

            StringBuilder numericString = new StringBuilder();

            foreach (char cridChar in text)
            {
                if (char.IsNumber(cridChar))
                    numericString.Append(cridChar);
            }

            if (numericString.Length != 0)
                return (numericString.ToString());
            else
                return (string.Empty);
        }

        private static string getBase64String(string inputString, bool useBase64)
        {
            if (useBase64)
                return Convert.ToBase64String(Encoding.ASCII.GetBytes(inputString));
            else
                return inputString;
        }

        private static string getAssemblyVersion(string fileName)
        {
            string path = Path.Combine(Environment.GetEnvironmentVariable("windir"), Path.Combine("ehome", fileName));

            try
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(path);

                return (assemblyName.Version.Major + "." +
                    assemblyName.Version.Minor + "." +
                    assemblyName.Version.MajorRevision + "." +
                    assemblyName.Version.MinorRevision);
            }
            catch (IOException e)
            {
                Logger.Instance.Write("Failed to get assembly version for " + fileName);
                Logger.Instance.Write(e.Message);
                return (string.Empty);
            }
        }

        private static string getAssemblyPublicKey(string fileName)
        {
            string path = Path.Combine(Environment.GetEnvironmentVariable("windir"), Path.Combine("ehome", fileName));

            try
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(path);

                byte[] publicKey = assemblyName.GetPublicKey();

                StringBuilder builder = new StringBuilder();
                foreach (byte keyByte in publicKey)
                    builder.Append(keyByte.ToString("x2"));

                return (builder.ToString());
            }
            catch (IOException e)
            {
                Logger.Instance.Write("Failed to get assembly public key for " + fileName);
                Logger.Instance.Write(e.Message);
                return (string.Empty);
            }
        }

        internal class KeywordGroup
        {
            internal string Name { get { return(name); } }
            internal Collection<string> Keywords { get { return (keywords); } }

            private string name;
            private Collection<string> keywords;

            internal KeywordGroup(string name)
            {
                this.name = name;
                keywords = new Collection<string>();
            }
        }

        internal class ImportedImage
        {
            internal Guid Guid { get; private set; } 
            internal string Path { get; private set; } 

            private ImportedImage() { }

            internal ImportedImage(string path)
            {
                Guid = Guid.NewGuid();
                Path = path;                
            }
        }

        internal class UidEntry
        {
            private static Collection<UidEntry> uidEntries;

            private string uniqueId;
            private string title;

            private UidEntry() { }

            internal UidEntry(string uniqueId, string title)
            {
                this.uniqueId = uniqueId;
                this.title = title;
            }

            internal static void AddEntry(string uniqueId, string title)
            {
                if (uidEntries == null)
                    uidEntries = new Collection<UidEntry>();

                uidEntries.Add(new UidEntry(uniqueId, title));
            }

            internal static string FindEntry(string uniqueId)
            {
                if (uidEntries == null)
                    return null;

                foreach (UidEntry uidEntry in uidEntries)
                {
                    if (uidEntry.uniqueId == uniqueId)
                        return uidEntry.title;
                }

                return null;
            }

            internal static int FindIndex(string uniqueId)
            {
                if (uidEntries == null)
                    return -1;

                for (int index = 0; index < uidEntries.Count; index++)
                {
                    if (uidEntries[index].uniqueId == uniqueId)
                        return index;
                }

                return -1;
            }
        }

        internal class ReferenceIdEntry
        {
            internal string ReferenceId { get; private set; }
            internal string Title { get; private set; }

            internal Collection<string> OtherReferenceIds { get; private set; }
            internal Collection<string> OtherTitles { get; private set; }

            internal static Collection<ReferenceIdEntry> ReferenceIdEntries;
            
            private ReferenceIdEntry() { }

            internal ReferenceIdEntry(string referenceId, string title)
            {                
                ReferenceId = referenceId;
                Title = title;
            }

            internal static void AddEntry(string referenceId, string title)
            {
                if (ReferenceIdEntries == null)
                    ReferenceIdEntries = new Collection<ReferenceIdEntry>();

                ReferenceIdEntries.Add(new ReferenceIdEntry(referenceId, title));
            }

            internal void AddOtherReferenceId(string referenceId)
            {
                if (OtherReferenceIds == null)
                    OtherReferenceIds = new Collection<string>();

                if (!OtherReferenceIds.Contains(referenceId))
                    OtherReferenceIds.Add(referenceId);
            }

            internal void AddOtherTitle(string title)
            {
                if (OtherTitles == null)
                    OtherTitles = new Collection<string>();

                if (!OtherTitles.Contains(title))
                    OtherTitles.Add(title);
            }

            internal static ReferenceIdEntry FindReferenceIdEntry(string title)
            {
                if (ReferenceIdEntries == null)
                    return null;

                foreach (ReferenceIdEntry referenceIdEntry in ReferenceIdEntries)
                {
                    if (referenceIdEntry.Title == title)
                        return referenceIdEntry;
                }

                return null;
            }

            internal static ReferenceIdEntry FindTitleEntry(string referenceId)
            {
                if (ReferenceIdEntries == null)
                    return null;

                foreach (ReferenceIdEntry referenceIdEntry in ReferenceIdEntries)
                {
                    if (referenceIdEntry.ReferenceId == referenceId)
                        return referenceIdEntry;
                }

                return null;
            }
        }
    }
}

