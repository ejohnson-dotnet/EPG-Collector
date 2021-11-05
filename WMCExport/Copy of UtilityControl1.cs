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
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.Security.Principal;
using System.IO;
using System.Reflection;
using System.Globalization;

using Microsoft.Win32;

namespace WMCUtility
{
    internal sealed class OldUtilityControl1
    {
        private static string dataDirectory
        {
            get
            {
                if (applicationDirectory == null)
                {
                    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                        applicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Path.Combine("Geekzone", "EPG Collector"));
                    else
                        applicationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Path.Combine("Geekzone", "EPG Collector"));
                    if (!Directory.Exists(applicationDirectory))
                        Directory.CreateDirectory(applicationDirectory);
                }
                return (applicationDirectory);
            }
        }

        private static object objectStore;
        private static string applicationDirectory;

        private static string baseKeyName { get { return (@"Software\Microsoft\Windows\CurrentVersion\Media Center\Service\"); } }

        private static int totalChannelCount;
        private static int exportedChannelCount;

        private OldUtilityControl1() { }

        internal static void Export(string affiliateName)
        {
            Logger.Instance.Write("Windows Media Center Utility exporting data");
            ReflectionServices.LoadLibraries();

            bool reply;
            if (Environment.OSVersion.Version.Minor != 0)
                reply = getObjectStore();
            else

                reply = getObjectStoreVista();
            if (!reply)
                Environment.Exit(Program.ErrorExitCode);

            Logger.Instance.Write("Opened Windows Media Center database " + ReflectionServices.GetPropertyValue(objectStore.GetType(), objectStore, "StoreName"));

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
                ReflectionServices.InvokeMethod(objectStore.GetType(), objectStore, "Dispose", new object[] { });
                Logger.Instance.Write("<e> Windows Media Center export failed with an xml error");
                Logger.Instance.Write("<e> Exception: " + e.Message);
                Environment.Exit(Program.ErrorExitCode);
            }
            catch (IOException e)
            {
                ReflectionServices.InvokeMethod(objectStore.GetType(), objectStore, "Dispose", new object[] { });
                Logger.Instance.Write("<e> Windows Media Center export failed with an I/O error");
                Logger.Instance.Write("<e> Exception: " + e.Message);
                Environment.Exit(Program.ErrorExitCode);
            }

            Logger.Instance.Write(totalChannelCount + " merged channels processed from database");
            Logger.Instance.Write(exportedChannelCount + " merged channels exported from database");

            if (recordings != null)
                Logger.Instance.Write(recordings.Count + " recordings exported from database");
            else
                Logger.Instance.Write("0 recordings exported from database");

            if (affiliateName != null)
                removeAffiliates(affiliateName);

            ReflectionServices.InvokeMethod(objectStore.GetType(), objectStore, "Dispose", new object[] { });

            Logger.Instance.Write("Windows Media Center Utility finished - returning exit code 0");
            Environment.Exit(Program.OKExitCode);
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

        private static Collection<object> getMergedChannels()
        {
            object mergedChannelsObject = ReflectionServices.InvokeConstructor("mcepg", "MergedChannels", new Type[] { objectStore.GetType() }, new object[] { objectStore });
            object mergedChannels = ReflectionServices.InvokeMethod(mergedChannelsObject.GetType(), mergedChannelsObject, "ToList", new object[] { });

            Collection<object> selectedChannels = new Collection<object>();

            foreach (object channel in mergedChannels as System.Collections.IList)
            {
                totalChannelCount++;

                if (ReflectionServices.GetPropertyValue(channel.GetType(), channel, "Lineup") != null)
                {
                    object tuningInfosObject = ReflectionServices.GetPropertyValue(channel.GetType(), channel, "TuningInfos");
                    object tuningInfos = ReflectionServices.InvokeMethod(tuningInfosObject.GetType(), tuningInfosObject, "ToList", new object[] { });

                    foreach (object tuningInfo in tuningInfos as System.Collections.IList)
                    {
                        if (ReflectionServices.GetPropertyValue(tuningInfo.GetType(), tuningInfo, "Device") != null)
                        {
                            selectedChannels.Add(channel);
                            break;
                        }
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

            foreach (object recording in recordingsList as System.Collections.IList)
                recordings.Add(recording);

            return (recordings);
        }

        private static void createChannelsSection(XmlWriter xmlWriter, Collection<object> channels)
        {
            xmlWriter.WriteStartElement("channels");

            Collection<object> dvbProcessed = new Collection<object>();
            Collection<object> atscProcessed = new Collection<object>();
            Collection<object> digitalCableProcessed = new Collection<object>();
            Collection<object> typesNotProcessed = new Collection<object>();

            foreach (object channel in channels)
            {
                bool isBlocked = (bool)ReflectionServices.GetPropertyValue(channel.GetType(), channel, "IsBlocked");
                if (!isBlocked)
                {
                    xmlWriter.WriteStartElement("channel");

                    xmlWriter.WriteAttributeString("channelNumber", ReflectionServices.GetPropertyValue(channel.GetType(), channel, "ChannelNumber").ToString());
                    
                    string callSign = ReflectionServices.GetPropertyValue(channel.GetType(), channel, "CallSign") as string;
                    if (!string.IsNullOrWhiteSpace(callSign))
                        xmlWriter.WriteAttributeString("callSign", callSign);

                    object primaryChannel = ReflectionServices.GetPropertyValue(channel.GetType(), channel, "PrimaryChannel");
                    
                    string matchName = ReflectionServices.GetPropertyValue(primaryChannel.GetType(), primaryChannel, "MatchName") as string;
                    if (!string.IsNullOrWhiteSpace(matchName))
                        xmlWriter.WriteAttributeString("matchName", matchName);
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(callSign))
                            xmlWriter.WriteAttributeString("matchName", callSign);
                    }

                    string uid = getUniqueID(ReflectionServices.GetPropertyValue(primaryChannel.GetType(), primaryChannel, "UIds"));
                    if (!string.IsNullOrWhiteSpace(uid))
                        xmlWriter.WriteAttributeString("uid", uid);

                    object lineup = ReflectionServices.GetPropertyValue(channel.GetType(), channel, "Lineup");
                    xmlWriter.WriteAttributeString("lineup", (string)ReflectionServices.GetPropertyValue(lineup.GetType(), lineup, "Name"));

                    xmlWriter.WriteStartElement("tuningInfos");

                    object tuningInfosObject = ReflectionServices.GetPropertyValue(channel.GetType(), channel, "TuningInfos");
                    object tuningInfos = ReflectionServices.InvokeMethod(tuningInfosObject.GetType(), tuningInfosObject, "ToList", new object[] { });

                    foreach (object tuningInfo in tuningInfos as System.Collections.IList)
                    {
                        object tuneRequest = ReflectionServices.GetPropertyValue(tuningInfo.GetType(), tuningInfo, "TuneRequest");
                        if (tuneRequest != null)
                        {
                            if (tuneRequest.GetType().Name == "DVBTuneRequest")
                            {
                                bool alreadyDone = checkDVBProcessed(dvbProcessed, tuneRequest);
                                if (!alreadyDone)
                                {
                                    xmlWriter.WriteStartElement("dvbTuningInfo");

                                    object locator = ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "Locator");
                                    xmlWriter.WriteAttributeString("frequency", ((int)(ReflectionServices.GetPropertyValue(locator.GetType(), locator, "CarrierFrequency"))).ToString());
                                    xmlWriter.WriteAttributeString("onid", ((int)(ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "ONID"))).ToString());
                                    xmlWriter.WriteAttributeString("tsid", ((int)(ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "TSID"))).ToString());
                                    xmlWriter.WriteAttributeString("sid", ((int)(ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "SID"))).ToString());

                                    xmlWriter.WriteEndElement();

                                    dvbProcessed.Add(tuneRequest);
                                }
                                /*else
                                    Logger.Instance.Write("Tune request type '" + tuneRequest.GetType().Name + "' ignored - already processed");*/
                            }
                            else
                            {
                                if (tuneRequest.GetType().Name == "ATSCChannelTuneRequest")
                                {
                                    bool alreadyDone = checkATSCProcessed(atscProcessed, tuneRequest);
                                    if (!alreadyDone)
                                    {
                                        xmlWriter.WriteStartElement("atscTuningInfo");

                                        object locator = ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "Locator");
                                        xmlWriter.WriteAttributeString("frequency", ((int)(ReflectionServices.GetPropertyValue(locator.GetType(), locator, "CarrierFrequency"))).ToString());

                                        xmlWriter.WriteAttributeString("majorChannel", ((int)(ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "Channel"))).ToString());
                                        xmlWriter.WriteAttributeString("minorChannel", ((int)(ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "MinorChannel"))).ToString());

                                        xmlWriter.WriteEndElement();

                                        atscProcessed.Add(tuneRequest);
                                    }
                                    /*else
                                        Logger.Instance.Write("Tune request type '" + tuneRequest.GetType().Name + "' ignored - already processed");*/
                                }
                                else
                                {
                                    if (tuneRequest.GetType().Name == "DigitalCableTuneRequest")
                                    {
                                        bool alreadyDone = checkDigitalCableProcessed(digitalCableProcessed, tuneRequest);
                                        if (!alreadyDone)
                                        {
                                            xmlWriter.WriteStartElement("digitalCableTuningInfo");
                                            xmlWriter.WriteAttributeString("channelNumber", ((int)(ReflectionServices.GetPropertyValue(tuneRequest.GetType(), tuneRequest, "Channel"))).ToString());
                                            xmlWriter.WriteEndElement();

                                            atscProcessed.Add(tuneRequest);
                                        }
                                        /*else
                                            Logger.Instance.Write("Tune request type '" + tuneRequest.GetType().Name + "' ignored - already processed");*/
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
            }

            foreach (object notProcessedEntry in typesNotProcessed)
                Logger.Instance.Write("Tune request type '" + notProcessedEntry.GetType().Name + "' ignored - type not processed");
        }

        private static bool checkDVBProcessed(Collection<object> dvbProcessed, object newRequest)
        {
            if (newRequest == null)
                return true;

            object newLocator = ReflectionServices.GetPropertyValue(newRequest.GetType(), newRequest, "Locator");
            if (newLocator == null)
                return true;

            int newFrequency = (int)ReflectionServices.GetPropertyValue(newLocator.GetType(), newLocator, "CarrierFrequency");

            int newONID = (int)ReflectionServices.GetPropertyValue(newRequest.GetType(), newRequest, "ONID");
            int newTSID = (int)ReflectionServices.GetPropertyValue(newRequest.GetType(), newRequest, "TSID");
            int newSID = (int)ReflectionServices.GetPropertyValue(newRequest.GetType(), newRequest, "SID");

            foreach (object existingRequest in dvbProcessed)
            {
                object existingLocator = ReflectionServices.GetPropertyValue(existingRequest.GetType(), existingRequest, "Locator");
                int existingFrequency = (int)ReflectionServices.GetPropertyValue(existingLocator.GetType(), existingLocator, "CarrierFrequency");

                int existingONID = (int)ReflectionServices.GetPropertyValue(existingRequest.GetType(), existingRequest, "ONID");
                int existingTSID = (int)ReflectionServices.GetPropertyValue(existingRequest.GetType(), existingRequest, "TSID");
                int existingSID = (int)ReflectionServices.GetPropertyValue(existingRequest.GetType(), existingRequest, "SID");

                if (existingFrequency == newFrequency && existingONID == newONID &&
                    existingTSID == newTSID && existingSID == newSID)
                    return (true);
            }

            return (false);
        }

        private static bool checkATSCProcessed(Collection<object> atscProcessed, object newRequest)
        {
            if (newRequest == null)
                return true;

            object newLocator = ReflectionServices.GetPropertyValue(newRequest.GetType(), newRequest, "Locator");
            if (newLocator == null)
                return true;

            int newFrequency = (int)ReflectionServices.GetPropertyValue(newLocator.GetType(), newLocator, "CarrierFrequency");

            int newChannel = (int)ReflectionServices.GetPropertyValue(newRequest.GetType(), newRequest, "Channel");
            int newMinorChannel = (int)ReflectionServices.GetPropertyValue(newRequest.GetType(), newRequest, "MinorChannel");

            foreach (object existingRequest in atscProcessed)
            {
                object existingLocator = ReflectionServices.GetPropertyValue(existingRequest.GetType(), existingRequest, "Locator");
                int existingFrequency = (int)ReflectionServices.GetPropertyValue(existingLocator.GetType(), existingLocator, "CarrierFrequency");

                int existingChannel = (int)ReflectionServices.GetPropertyValue(existingRequest.GetType(), existingRequest, "Channel");
                int existingMinorChannel = (int)ReflectionServices.GetPropertyValue(existingRequest.GetType(), existingRequest, "MinorChannel");

                if (existingFrequency == newFrequency && existingChannel == newChannel && existingMinorChannel == newMinorChannel)
                    return (true);
            }

            return (false);
        }

        private static bool checkDigitalCableProcessed(Collection<object> digitalCableProcessed, object newRequest)
        {
            if (newRequest == null)
                return true;

            object newLocator = ReflectionServices.GetPropertyValue(newRequest.GetType(), newRequest, "Locator");
            if (newLocator == null)
                return true;

            int newChannel = (int)ReflectionServices.GetPropertyValue(newRequest.GetType(), newRequest, "Channel");

            foreach (object existingRequest in digitalCableProcessed)
            {
                int existingChannel = (int)ReflectionServices.GetPropertyValue(existingRequest.GetType(), existingRequest, "Channel");

                if (existingChannel == newChannel)
                    return (true);
            }

            return (false);
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

            System.Collections.IList uidList = ReflectionServices.InvokeMethod(uids.GetType(), uids, "ToList", new object[] { }) as System.Collections.IList;
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

        private static void removeAffiliates(string affiliateName)
        {
            Logger.Instance.Write("Removing dummy affiliates");

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

            Logger.Instance.Write("Services updated = " + updateCount);
        }

        internal static void DisableGuideLoader()
        {
            Logger.Instance.Write("Disabling in-band guide loader");

            bool reply = processRegistryKey("BackgroundScanner", "PeriodicScanEnabled", 0);
            if (reply)
                reply = processRegistryKey("GLID", "DisableInbandSchedule", 1);

            if (reply)
                Logger.Instance.Write("In-band guide loader disabled - the machine must be reloaded for any new settings to take effect");
            else
                Logger.Instance.Write("In-band guide loader may not have been disabled - check the registry");

            Environment.Exit(Program.OKExitCode);
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
    }
}

