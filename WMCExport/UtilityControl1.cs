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
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.Security.Principal;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Diagnostics;
using System.Threading;
using System.Xml.Serialization;

using Microsoft.Win32;

namespace WMCUtility
{
    internal sealed class UtilityControl1
    {
        private static string dataDirectory
        {
            get
            {
                if (applicationDirectory == null)
                {
                    applicationDirectory = Environment.GetEnvironmentVariable("EPGC_DATA_DIR", EnvironmentVariableTarget.Machine);
                    if (string.IsNullOrWhiteSpace(applicationDirectory))
                    {
                        applicationDirectory = Environment.GetEnvironmentVariable("EPGC_DATA_DIR", EnvironmentVariableTarget.Process);
                        if (string.IsNullOrWhiteSpace(applicationDirectory))
                            applicationDirectory = Environment.GetEnvironmentVariable("EPGC_DATA_DIR", EnvironmentVariableTarget.User);
                    }

                    if (string.IsNullOrWhiteSpace(applicationDirectory))
                    {
                        WindowsIdentity identity = WindowsIdentity.GetCurrent();
                        WindowsPrincipal principal = new WindowsPrincipal(identity);
                        if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                            applicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Path.Combine("Geekzone", "EPG Collector"));
                        else
                            applicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Path.Combine("Geekzone", "EPG Collector"));
                    }

                    if (!Directory.Exists(applicationDirectory))
                        Directory.CreateDirectory(applicationDirectory);                    
                }
                return (applicationDirectory);
            }
        }

        private static object objectStore;
        private static string storeName;
        private static string applicationDirectory;

        private static string baseKeyName { get { return (@"Software\Microsoft\Windows\CurrentVersion\Media Center\Service\"); } }

        private static int totalChannelCount;
        private static int exportedChannelCount;

        private static bool importExited;

        private UtilityControl1() { }

        internal static int Export()
        {
            Logger.Instance.Write("Windows Media Center Utility exporting data");
            ReflectionServices.LoadLibraries();

            if (!openStore())
                return Program.ErrorExitCode;

            Collection<object> channels = getMergedChannels();
            Collection<object> recordings = getRecordings();

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.NewLineOnAttributes = false;
            settings.Encoding = new UnicodeEncoding();
            settings.CloseOutput = true;

            string exportName = Path.Combine(dataDirectory, "WMC Export.xml");

            Logger.Instance.Write("Exporting Windows Media Center data to " + exportName);

            try
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(exportName, settings))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("wmcdata");

                    xmlWriter.WriteAttributeString("generator-info-name", Assembly.GetExecutingAssembly().GetName().Name
                        + "/" + Assembly.GetExecutingAssembly().GetName().Version.ToString());

                    createChannelsSection(xmlWriter, channels);
                    if (recordings != null)
                        createRecordingsSection(xmlWriter, recordings);

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();

                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
            catch (XmlException e)
            {
                closeStore();
                Logger.Instance.Write("<e> Windows Media Center export failed with an xml error");
                Logger.Instance.Write("<e> Exception: " + e.Message);
                return Program.ErrorExitCode;
            }
            catch (IOException e)
            {
                closeStore();
                Logger.Instance.Write("<e> Windows Media Center export failed with an I/O error");
                Logger.Instance.Write("<e> Exception: " + e.Message);
                return Program.ErrorExitCode;
            }

            Logger.Instance.Write(totalChannelCount + " merged channels processed from database");
            Logger.Instance.Write(exportedChannelCount + " merged channels exported from database");

            if (recordings != null)
                Logger.Instance.Write(recordings.Count + " recordings exported from database");
            else
                Logger.Instance.Write("0 recordings exported from database");

            closeStore();
            
            return Program.OKExitCode;
        }

        private static Collection<object> getMergedChannels()
        {
            object deviceGroups = ReflectionServices.InvokeConstructor("mcepg", "DeviceGroups", new Type[] { objectStore.GetType() }, new object[] { objectStore });
            object deviceGroup = ReflectionServices.InvokeMethod(deviceGroups.GetType(), deviceGroups, "FindFirstEnabledDeviceGroup", new object[] { });
            if (deviceGroup == null)
                return null;

            object mergedLineup = ReflectionServices.GetPropertyValue(deviceGroup.GetType(), deviceGroup, "Lineup");
            if (mergedLineup == null)
                return null;

            object channelList = ReflectionServices.InvokeMethod(mergedLineup.GetType(), mergedLineup, "GetGridChannels", new object[] { });
            if (channelList == null)
                return null;
            
            Collection<object> selectedChannels = new Collection<object>();

            foreach (object channel in channelList as IList)
            {
                totalChannelCount++;

                if (!(bool)ReflectionServices.GetPropertyValue(channel.GetType(), channel, "IsBlocked"))
                {
                    object tuningInfosObject = ReflectionServices.GetPropertyValue(channel.GetType(), channel, "TuningInfos");
                    if (tuningInfosObject != null)
                    {
                        IList tuningInfos = ReflectionServices.InvokeMethod(tuningInfosObject.GetType(), tuningInfosObject, "ToList", new object[] { }) as IList;
                        if (tuningInfos.Count != 0)
                            selectedChannels.Add(channel);
                    }
                }
            }

            return (selectedChannels);
        }

        private static Collection<object> getRecordings()
        {
            object library = ReflectionServices.InvokeConstructor("mcepg", "Library", new Type[] { objectStore.GetType(), typeof(bool), typeof(bool) }, new object[] { objectStore, false, false });
            object recordingsObject = ReflectionServices.GetPropertyValue(library.GetType(), library, "Recordings");
            if (recordingsObject == null)
            {
                Logger.Instance.Write("No library found");
                return (null);
            }

            object recordingsList = ReflectionServices.InvokeMethod(recordingsObject.GetType(), recordingsObject, "ToList", new object[] { });
            Collection<object> recordings = new Collection<object>();

            foreach (object recording in recordingsList as IList)
                recordings.Add(recording);

            return (recordings);
        }

        private static void createChannelsSection(XmlWriter xmlWriter, Collection<object> channels)
        {
            Collection<object> typesNotProcessed = new Collection<object>();

            xmlWriter.WriteStartElement("channels");

            foreach (object channel in channels)
            {
                xmlWriter.WriteStartElement("channel");

                xmlWriter.WriteAttributeString("channelNumber", ReflectionServices.GetPropertyValue(channel.GetType(), channel, "ChannelNumber").ToString());
                    
                string callSign = ReflectionServices.GetPropertyValue(channel.GetType(), channel, "CallSign") as string;
                if (!string.IsNullOrWhiteSpace(callSign))
                    xmlWriter.WriteAttributeString("callSign", callSign);

                string matchName = ReflectionServices.GetPropertyValue(channel.GetType(), channel, "MatchName") as string;
                if (!string.IsNullOrWhiteSpace(matchName))
                    xmlWriter.WriteAttributeString("matchName", matchName);                    

                string uid = getUniqueID(ReflectionServices.GetPropertyValue(channel.GetType(), channel, "UIds"));
                if (!string.IsNullOrWhiteSpace(uid))
                    xmlWriter.WriteAttributeString("uid", uid);
                else
                {
                    object primaryChannel = getPrimaryChannel(channel);
                    if (primaryChannel != null)
                    {
                        uid = getUniqueID(ReflectionServices.GetPropertyValue(primaryChannel.GetType(), primaryChannel, "UIds"));
                        if (!string.IsNullOrWhiteSpace(uid))
                            xmlWriter.WriteAttributeString("uid", uid);
                    }
                }

                xmlWriter.WriteStartElement("tuningInfos");

                object tuningInfosObject = ReflectionServices.GetPropertyValue(channel.GetType(), channel, "TuningInfos");
                object tuningInfos = ReflectionServices.InvokeMethod(tuningInfosObject.GetType(), tuningInfosObject, "ToList", new object[] { });

                Collection<TuningInfo> processed = new Collection<TuningInfo>();

                foreach (object tuningInfo in tuningInfos as IList)
                {
                    object tuneRequest = ReflectionServices.GetPropertyValue(tuningInfo.GetType(), tuningInfo, "TuneRequest");
                    if (tuneRequest != null)
                    {
                        if (tuneRequest.GetType().Name == "DVBTuneRequest")
                        {
                            object locator = ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "Locator");
                            if (locator != null)
                            {
                                int frequency = (int)(ReflectionServices.GetPropertyValue(locator.GetType(), locator, "CarrierFrequency"));
                                int originalNetworkId = (int)(ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "ONID"));
                                int transportStreamId = (int)(ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "TSID"));
                                int serviceId = (int)(ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "SID"));

                                if (!checkAlreadyProcessed(processed, frequency, originalNetworkId, transportStreamId, serviceId))
                                {
                                    xmlWriter.WriteStartElement("dvbTuningInfo");
                                    xmlWriter.WriteAttributeString("frequency", frequency.ToString());
                                    xmlWriter.WriteAttributeString("onid", originalNetworkId.ToString());
                                    xmlWriter.WriteAttributeString("tsid", transportStreamId.ToString());
                                    xmlWriter.WriteAttributeString("sid", serviceId.ToString());
                                    xmlWriter.WriteEndElement();
                                }
                            }                                
                        }
                        else
                        {
                            if (tuneRequest.GetType().Name == "ATSCChannelTuneRequest")
                            {  
                                int majorChannelNumber = (int)(ReflectionServices.GetPropertyValue(channel.GetType(), channel, "Number"));
                                int minorChannelNumber = (int)(ReflectionServices.GetPropertyValue(channel.GetType(), channel, "SubNumber"));

                                if (!checkAlreadyProcessed(processed, majorChannelNumber, minorChannelNumber))
                                {
                                    xmlWriter.WriteStartElement("atscTuningInfo");
                                    xmlWriter.WriteAttributeString("majorChannel", majorChannelNumber.ToString());
                                    xmlWriter.WriteAttributeString("minorChannel", minorChannelNumber.ToString());
                                    xmlWriter.WriteEndElement();
                                }
                            }
                            else
                            {
                                if (tuneRequest.GetType().Name == "DigitalCableTuneRequest")
                                {
                                    int channelNumber = (int)(ReflectionServices.GetPropertyValue(channel.GetType(), channel, "Number"));

                                    if (!checkAlreadyProcessed(processed, channelNumber))
                                    {
                                        xmlWriter.WriteStartElement("digitalCableTuningInfo");
                                        xmlWriter.WriteAttributeString("channelNumber", channelNumber.ToString());
                                        xmlWriter.WriteEndElement();
                                    }                                    
                                }
                                else
                                    addToNotProcessed(typesNotProcessed, tuneRequest);
                            }

                        }
                    }
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();

                exportedChannelCount++;                
            }

            xmlWriter.WriteEndElement();

            foreach (object notProcessedEntry in typesNotProcessed)
                Logger.Instance.Write("Tune request type '" + notProcessedEntry.GetType().Name + "' ignored - type not processed");
        }

        private static object getPrimaryChannel(object channel)
        {
            return ReflectionServices.GetPropertyValue(channel.GetType(), channel, "PrimaryChannel");
        }

        private static bool checkAlreadyProcessed(Collection<TuningInfo> processed, int frequency, int originalNetworkId, int transportStreamId, int serviceId)
        {
            foreach (TuningInfo tuningInfo in processed)
            {
                if (tuningInfo.Equal(frequency, originalNetworkId, transportStreamId, serviceId))
                    return true;
            }

            processed.Add(new TuningInfo(frequency, originalNetworkId, transportStreamId, serviceId));
            return false;
        }

        private static bool checkAlreadyProcessed(Collection<TuningInfo> processed, int majorChannelNumber, int minorChannelNumber)
        {
            foreach (TuningInfo tuningInfo in processed)
            {
                if (tuningInfo.Equal(majorChannelNumber, minorChannelNumber))
                    return true;
            }

            processed.Add(new TuningInfo(majorChannelNumber, minorChannelNumber));
            return false;
        }

        private static bool checkAlreadyProcessed(Collection<TuningInfo> processed, int channelNumber)
        {
            foreach (TuningInfo tuningInfo in processed)
            {
                if (tuningInfo.Equal(channelNumber))
                    return true;
            }

            processed.Add(new TuningInfo(channelNumber));
            return false;
        }

        private static void addToNotProcessed(Collection<object> typesNotProcessed, object tuneRequest)
        {
            foreach (object existingTuneRequest in typesNotProcessed)
            {
                if (existingTuneRequest.GetType().Name == tuneRequest.GetType().Name)
                    return;
            }

            typesNotProcessed.Add(tuneRequest);
        }

        private static string getUniqueID(object uids)
        {
            if (uids == null)
                return (null);

            IList uidList = ReflectionServices.InvokeMethod(uids.GetType(), uids, "ToList", new object[] { }) as IList;
            if (uidList.Count == 0)
                return (null);

            string firstName = null;

            foreach (object uid in uidList)
            {
                string fullName = ReflectionServices.InvokeMethod(uid.GetType(), uid, "GetFullName", new object[] { }) as string;

                if (firstName == null)
                    firstName = fullName;

                if (fullName.Contains("EPGCollector"))
                    return (fullName);
            }

            return (firstName);
        }

        private static void createRecordingsSection(XmlWriter xmlWriter, Collection<object> recordings)
        {
            xmlWriter.WriteStartElement("recordings");

            foreach (object recording in recordings)
            {
                xmlWriter.WriteStartElement("recording");

                object program = ReflectionServices.GetPropertyValue(recording.GetType(), recording, "Program");

                xmlWriter.WriteAttributeString("title", (string)ReflectionServices.GetPropertyValue(program.GetType(), program, "Title"));
                xmlWriter.WriteAttributeString("description", (string)ReflectionServices.GetPropertyValue(program.GetType(), program, "ShortDescription"));
                xmlWriter.WriteAttributeString("startTime", ((DateTime)(ReflectionServices.GetPropertyValue(recording.GetType(), recording, "ContentStartTime"))).ToString(CultureInfo.InvariantCulture));
                xmlWriter.WriteAttributeString("seasonNumber", ((int)(ReflectionServices.GetPropertyValue(program.GetType(), program, "SeasonNumber"))).ToString());
                xmlWriter.WriteAttributeString("episodeNumber", ((int)(ReflectionServices.GetPropertyValue(program.GetType(), program, "EpisodeNumber"))).ToString());

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
        }

        public static int RemoveAffiliates(string affiliateName)
        {
            Logger.Instance.Write("Windows Media Center Utility removing dummy affiliates - " + affiliateName);
            ReflectionServices.LoadLibraries();

            if (!openStore())
                return Program.ErrorExitCode;

            string removeName = affiliateName == string.Empty ? "EPG Collector" : affiliateName;
            int updateCount = 0;

            object servicesObject = ReflectionServices.InvokeConstructor("mcepg", "Services", new Type[] { objectStore.GetType() }, new object[] { objectStore });
            object services = ReflectionServices.InvokeMethod(servicesObject.GetType(), servicesObject, "ToList", new object[] { });

            foreach (object service in services as System.Collections.IList)
            {
                object serviceName = ReflectionServices.GetPropertyValue(service.GetType(), service, "Name");

                object affiliate = ReflectionServices.GetPropertyValue(service.GetType(), service, "Affiliate");
                if (affiliate != null)
                {
                    string serviceAffiliateName = ReflectionServices.GetPropertyValue(affiliate.GetType(), affiliate, "Name") as string;
                    if (!string.IsNullOrWhiteSpace(serviceAffiliateName) && serviceAffiliateName.ToLowerInvariant().StartsWith(removeName.ToLowerInvariant()))
                    {
                        ReflectionServices.SetPropertyValue(service.GetType(), service, "Affiliate", null);
                        ReflectionServices.InvokeMethod(service.GetType(), service, "Update", new object[] { });
                        ReflectionServices.InvokeMethod(service.GetType(), service, "Unlock", new object[] { });

                        Logger.Instance.Write("Service " + serviceName + " - affiliate '" + removeName + "' removed");
                        updateCount++;
                    }
                }
            }

            closeStore();
            Logger.Instance.Write("Windows Media Center Utility removed affiliates - services updated = " + updateCount);

            return Program.OKExitCode;
        }

        public static int RemoveChannelLogos()
        {
            Logger.Instance.Write("Windows Media Center Utility removing channel logos");
            ReflectionServices.LoadLibraries();

            if (!openStore())
                return Program.ErrorExitCode;

            int updateCount = 0;

            object servicesObject = ReflectionServices.InvokeConstructor("mcepg", "Services", new Type[] { objectStore.GetType() }, new object[] { objectStore });
            object services = ReflectionServices.InvokeMethod(servicesObject.GetType(), servicesObject, "ToList", new object[] { });

            foreach (object service in services as System.Collections.IList)
            {
                object serviceName = ReflectionServices.GetPropertyValue(service.GetType(), service, "Name");

                object logo = ReflectionServices.GetPropertyValue(service.GetType(), service, "LogoImage");
                if (logo != null)
                {
                    ReflectionServices.SetPropertyValue(service.GetType(), service, "LogoImage", null);
                    ReflectionServices.InvokeMethod(service.GetType(), service, "Update", new object[] { });
                    ReflectionServices.InvokeMethod(service.GetType(), service, "Unlock", new object[] { });

                    Logger.Instance.Write("Service " + serviceName + " - logo removed");
                    updateCount++;
                }
            }

            closeStore();
            Logger.Instance.Write("Windows Media Center Utility removed channel logos - services updated = " + updateCount);

            return Program.OKExitCode;
        }

        internal static int DisableGuideLoader()
        {
            Logger.Instance.Write("Windows Media Center Utility disabling in-band guide loader");

            bool reply = processRegistryKey("BackgroundScanner", "PeriodicScanEnabled", 0);
            if (reply)
                reply = processRegistryKey("GLID", "DisableInbandSchedule", 1);

            if (reply)
                Logger.Instance.Write("In-band guide loader disabled - the machine must be reloaded for any new settings to take effect");
            else
                Logger.Instance.Write("In-band guide loader may not have been disabled - check the registry");

            return Program.OKExitCode;
        }

        private static bool processRegistryKey(string subKey, string name, int value)
        {
            try
            {
                Logger.Instance.Write("Opening registry key " + baseKeyName + subKey);

                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(baseKeyName + subKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                if (registryKey == null)
                {
                    Logger.Instance.Write("Creating registry key " + baseKeyName + subKey);
                    registryKey = Registry.LocalMachine.CreateSubKey(baseKeyName + subKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
                    if (registryKey == null)
                        return (false);

                    Logger.Instance.Write("Setting " + name + " to value " + value);
                    registryKey.SetValue(name, value);
                }
                else
                {
                    Logger.Instance.Write("Registry key " + baseKeyName + subKey + " already exists");

                    object namedData = registryKey.GetValue(name);

                    if (namedData == null)
                    {
                        Logger.Instance.Write("Creating " + name + " with value " + value);
                        registryKey.SetValue(name, value);
                    }
                    else
                    {
                        if ((int)namedData != value)
                        {
                            Logger.Instance.Write("Setting " + name + " to value " + value);
                            registryKey.SetValue(name, value);

                        }
                        else
                            Logger.Instance.Write(name + " not changed - value already " + value);
                    }
                }

                registryKey.Close();

                return (true);
            }
            catch (Exception e)
            {
                Logger.Instance.Write("<E> An exception of type " + e.GetType().Name + " has occurred");
                Logger.Instance.Write("<E> " + e.Message);
                return (false);
            }
        }

        internal static int ClearGuide()
        {
            Logger.Instance.Write("Windows Media Center Utility clearing guide");

            ReflectionServices.LoadLibraries();
            openStore();
            closeStore();

            string[] storeNameParts = storeName.Split(new char[] { '.' });

            if (!backupDatabase())
                return Program.ErrorExitCode;

            Collection<string> backupFileNames = getBackupFileNames(storeNameParts[0]);
            if (backupFileNames.Count == 0)
            {
                Logger.Instance.Write("<e> There are no Windows Media Centre database files available");
                return Program.ErrorExitCode;
            }
            
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "ehome", storeName);            
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                Logger.Instance.Write("<e> Failed to delete Windows Media Centre database");
                Logger.Instance.Write("<e> " + e.Message);
                return Program.ErrorExitCode;
            }

            if (!restoreDatabase(backupFileNames))
                return Program.ErrorExitCode;

            Logger.Instance.Write("Windows Media Center Utility has cleared the guide");

            return Program.OKExitCode;
        }

        public static int TidyChannels()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BackupLineupRoot));
            FileStream fileStream = new FileStream(@"G:\Lineup Backup", FileMode.Open);
            BackupLineupRoot lineupRoot = (BackupLineupRoot)serializer.Deserialize(fileStream);
            
            return Program.OKExitCode;
        }

        private static bool backupDatabase()
        {
            string runDirectory = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "ehome");
            Logger.Instance.Write("Running Windows Media Centre backup task 'mcupdate'");

            Process backupProcess = new Process();
            backupProcess.StartInfo.FileName = Path.Combine(runDirectory, "mcUpdate.exe");
            backupProcess.StartInfo.WorkingDirectory = runDirectory + Path.DirectorySeparatorChar;
            backupProcess.StartInfo.Arguments = "-b";
            backupProcess.StartInfo.UseShellExecute = false;
            backupProcess.StartInfo.CreateNoWindow = true;

            try
            {
                bool reply = backupProcess.Start();
                if (!reply)
                {
                    Logger.Instance.Write("Windows Media Centre backup task has started");
                    return false;
                }
                
                backupProcess.WaitForExit();

                if (backupProcess.ExitCode != 0)
                    Logger.Instance.Write("Windows Media Centre backup task failed - exit code " + backupProcess.ExitCode);
                else
                    Logger.Instance.Write("Windows Media Centre backup task completed successfully");

                return backupProcess.ExitCode == 0;
            }
            catch (Exception e)
            {
                Logger.Instance.Write("<e> Failed to run Windows Media Centre backup task");
                Logger.Instance.Write("<e> " + e.Message);
                return false;
            }
        }

        private static Collection<string> getBackupFileNames(string storeName)
        {
            Collection<string> backupFileNames = new Collection<string>();
            
            string lineupFileName = processBackupDirectory(storeName, "lineup");
            if (lineupFileName != null)
                backupFileNames.Add(lineupFileName);

            string recordingsFileName = processBackupDirectory(storeName, "recordings");
            if (recordingsFileName != null)
                backupFileNames.Add(recordingsFileName);

            string subscriptionsFileName = processBackupDirectory(storeName, "subscriptions");
            if (subscriptionsFileName != null)
                backupFileNames.Add(subscriptionsFileName);

            return backupFileNames;
        }

        private static string processBackupDirectory(string storeName, string backupName)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft", "ehome", storeName, "backup", backupName); 
            if (!Directory.Exists(path))
            {
                Logger.Instance.Write("<e> Backup directory does not exist - " + path);
                return null;
            }

            Logger.Instance.Write("Getting latest backup file from " + path);

            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            FileInfo selectedFile = null;

            foreach (FileInfo file in files)
            {
                if (selectedFile == null)
                    selectedFile = file;
                else
                {
                    if (file.Name.CompareTo(selectedFile.Name) > 0)
                        selectedFile = file;
                }
            }

            if (selectedFile == null)
                Logger.Instance.Write("<e> Backup directory is empty");
            else
                Logger.Instance.Write("Latest backup file is " + selectedFile.Name);

            return selectedFile.FullName;
        }

        private static bool restoreDatabase(Collection<string> backupFileNames)
        {
            foreach (string backupFileName in backupFileNames)
            {
                if (!restoreFile(backupFileName))
                    return false;
            }

            return true;
        }

        private static bool restoreFile(string fileName)
        {
            Logger.Instance.Write("Processing backup file " + fileName);

            string runDirectory = Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "ehome");
            Logger.Instance.Write("Running Windows Media Centre import utility LoadMXF from " + runDirectory);

            importExited = false;

            Process importProcess = new Process();

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
                    return (true);
                else
                {
                    Logger.Instance.Write("Failed to load Windows Media Centre data: reply code " + importProcess.ExitCode);
                    return false;
                }
            }
            catch (Exception e)
            {
                Logger.Instance.Write("<e> Failed to run the Windows Media Centre import utility LoadMXF");
                Logger.Instance.Write("<e> " + e.Message);
                return false;
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

        private static bool openStore()
        {
            try
            {
                bool reply;

                if (Environment.OSVersion.Version.Minor != 0)
                    reply = getObjectStore();
                else
                    reply = getObjectStoreVista();

                if (!reply)
                    return false;

                storeName = (string)ReflectionServices.GetPropertyValue(objectStore.GetType(), objectStore, "StoreName");
                Logger.Instance.Write("Opened Windows Media Center database " + storeName);

                return true;
            }
            catch (Exception e)
            {
                Logger.Instance.Write("Failed to open Windows Media Center database: " + e.Message);
                return false;
            }
        }

        private static bool getObjectStore()
        {
            Logger.Instance.Write("Opening Windows Media Center database - not Vista OS");

            string s = "Unable upgrade recording state.";
            byte[] bytes = Convert.FromBase64String("FAAODBUITwADRicSARc=");
            byte[] buffer2 = Encoding.ASCII.GetBytes(s);

            for (int i = 0; i != bytes.Length; i++)
                bytes[i] = (byte)(bytes[i] ^ buffer2[i]);

            string clientId = ReflectionServices.GetStaticValue(ReflectionServices.GetType("mcstore", "ObjectStore"), "GetClientId", new object[] { true }) as string;

            SHA256Managed managed = new SHA256Managed();
            byte[] buffer = Encoding.Unicode.GetBytes(clientId);
            clientId = Convert.ToBase64String(managed.ComputeHash(buffer));

            string friendlyName = Encoding.ASCII.GetString(bytes);
            string displayName = clientId;

            try
            {
                objectStore = ReflectionServices.InvokeMethod(ReflectionServices.GetType("mcstore", "ObjectStore"),
                    null,
                    "Open",
                    new object[] { "", friendlyName, displayName, true });

                if (objectStore == null)
                {
                    Logger.Instance.Write("<e> Cannot get Windows Media Center information: database cannot be opened");
                    return (false);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.Write("<e> Cannot get Windows Media Center information: database cannot be opened");
                Logger.Instance.Write("<e> Exception: " + e.Message);
                return (false);
            }

            return (true);
        }

        private static bool getObjectStoreVista()
        {
            Logger.Instance.Write("Opening Windows Media Center database - Vista OS");

            string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"Microsoft/ehome");
            if (!Directory.Exists(basePath))
                return (false);

            DirectoryInfo directoryInfo = new DirectoryInfo(basePath);
            FileInfo[] fileInfo = directoryInfo.GetFiles("*.db");
            if (fileInfo == null || fileInfo.Length == 0)
                return (false);

            FileInfo latestFileInfo = fileInfo[0];

            foreach (FileInfo currentFileInfo in fileInfo)
            {
                if (currentFileInfo.LastWriteTime > latestFileInfo.LastWriteTime)
                    latestFileInfo = currentFileInfo;
            }

            try
            {
                objectStore = ReflectionServices.InvokeMethod(ReflectionServices.GetType("mcstore", "ObjectStore"),
                    null,
                    "Open",
                    new object[] { latestFileInfo.FullName });
                if (objectStore == null)
                {
                    Logger.Instance.Write("<e> Cannot get Windows Media Center information: database " + latestFileInfo.FullName + " cannot be opened");
                    return (false);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.Write("<e> Cannot get Windows Media Center information: database cannot be opened");
                Logger.Instance.Write("<e> Exception: " + e.Message);
                return (false);
            }

            return (true);
        }

        private static void closeStore()
        {
            ReflectionServices.InvokeMethod(objectStore.GetType(), objectStore, "Dispose", new object[] { });
            Logger.Instance.Write("Closed Windows Media Center database " + storeName);
        }

        internal class TuningInfo
        {
            internal int Frequency { get; private set; }
            internal int OriginalNetworkId { get; private set; }
            internal int TransportStreamId { get; private set; }
            internal int ServiceId { get; private set; }

            internal int MajorChannelNumber { get; private set; }
            internal int MinorChannelNumber { get; private set; }

            internal int ChannelNumber { get; private set; }

            private TuningInfo() { }

            internal TuningInfo(int frequency, int originalNetworkId, int transportStreamId, int serviceId)
            {
                Frequency = frequency;
                OriginalNetworkId = originalNetworkId;
                TransportStreamId = transportStreamId;
                ServiceId = serviceId;
            }

            internal TuningInfo(int majorChannelNumber, int minorChannelNumber)
            {
                MajorChannelNumber = majorChannelNumber;
                MinorChannelNumber = minorChannelNumber;
            }

            internal TuningInfo(int channelNumber)
            {
                ChannelNumber = channelNumber;
            }

            internal bool Equal(int frequency, int originalNetworkId, int transportStreamId, int serviceId)
            {
                return Frequency == frequency && OriginalNetworkId == originalNetworkId && TransportStreamId == transportStreamId && ServiceId == serviceId;
            }

            internal bool Equal(int majorChannelNumber, int minorChannelNumber)
            {
                return MajorChannelNumber == majorChannelNumber && MinorChannelNumber == minorChannelNumber;
            }

            internal bool Equal(int channelNumber)
            {
                return ChannelNumber == channelNumber;
            }
        }
    }
}

