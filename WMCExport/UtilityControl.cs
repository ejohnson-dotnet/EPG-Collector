﻿////////////////////////////////////////////////////////////////////////////////// 
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
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.Security.Principal;
using System.IO;
using System.Reflection;
using System.Globalization;

using Microsoft.Win32;

using Microsoft.MediaCenter.Guide;
using Microsoft.MediaCenter.Store;
using Microsoft.MediaCenter.Pvr;
using Microsoft.MediaCenter.TV.Tuning;

namespace WMCUtility
{
    internal sealed class UtilityControl
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

        private static ObjectStore objectStore;
        private static string applicationDirectory;

        private static string baseKeyName { get { return (@"Software\Microsoft\Windows\CurrentVersion\Media Center\Service\"); } }

        private UtilityControl() { }

        internal static void Export()
        {
            Logger.Instance.Write("Windows Media Center Utility exporting data");

            bool reply = getObjectStore();
            if (!reply)
                Environment.Exit(1);

            Logger.Instance.Write("Opened Windows Media Center database " + objectStore.StoreName);

            Collection<MergedChannel> channels = getMergedChannels();
            Collection<Recording> recordings = getRecordings();

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
                objectStore.Dispose();
                Logger.Instance.Write("<e> Windows Media Center export failed with an xml error");
                Logger.Instance.Write("<e> Exception: " + e.Message);
                Environment.Exit(1);
            }
            catch (IOException e)
            {
                objectStore.Dispose();
                Logger.Instance.Write("<e> Windows Media Center export failed with an I/O error");
                Logger.Instance.Write("<e> Exception: " + e.Message);
                Environment.Exit(1);
            }

            objectStore.Dispose();

            Logger.Instance.Write(channels.Count + " channels exported from database");
            if (recordings != null)
                Logger.Instance.Write(recordings.Count + " recordings exported from database");
            else
                Logger.Instance.Write("0 recordings exported from database");

            Logger.Instance.Write("Windows Media Center Utility finished - returning exit code 0");
            Environment.Exit(0);
        }

        private static bool getObjectStore()
        {
            string s = "Unable upgrade recording state.";
            byte[] bytes = Convert.FromBase64String("FAAODBUITwADRicSARc=");
            byte[] buffer2 = Encoding.ASCII.GetBytes(s);

            for (int i = 0; i != bytes.Length; i++)
                bytes[i] = (byte)(bytes[i] ^ buffer2[i]);

            string clientId = ObjectStore.GetClientId(true);
            
            SHA256Managed managed = new SHA256Managed();
            byte[] buffer = Encoding.Unicode.GetBytes(clientId);
            clientId = Convert.ToBase64String(managed.ComputeHash(buffer));

            string friendlyName = Encoding.ASCII.GetString(bytes);
            string displayName = clientId;

            try
            {
                objectStore = Microsoft.MediaCenter.Store.ObjectStore.Open("", friendlyName, displayName, true);
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

        private static Collection<MergedChannel> getMergedChannels()
        {
            MergedChannels channels = new MergedChannels(objectStore);            
            
            Collection<MergedChannel> selectedChannels = new Collection<MergedChannel>();

            foreach (MergedChannel channel in channels)
            {
                if (channel.Lineup != null)
                {
                    foreach (TuningInfo tuningInfo in channel.TuningInfos)
                    {
                        if (tuningInfo.Device != null)
                        {
                            selectedChannels.Add(channel);

                            /*if (!channel.HasUserMappedListings && channel.PrimaryChannel != null && channel.ChannelType != ChannelType.UserHidden)
                                channel.AddChannelListings(channel.PrimaryChannel);*/

                            break;
                        }
                    }
                }
            }

            return (selectedChannels);
        }

        private static Collection<Recording> getRecordings()
        {
            Library library = new Library(objectStore, false, false);
            if (library.Recordings == null)
            {
                Logger.Instance.Write("No library found");
                return (null);
            }

            Collection<Recording> recordings = new Collection<Recording>();

            foreach (Recording recording in library.Recordings)
                recordings.Add(recording);

            return (recordings);
        }

        private static void createChannelsSection(XmlWriter xmlWriter, Collection<MergedChannel> channels)
        {
            xmlWriter.WriteStartElement("channels");

            Collection<DVBTuneRequest> dvbProcessed = new Collection<DVBTuneRequest>();
            Collection<ATSCChannelTuneRequest> atscProcessed = new Collection<ATSCChannelTuneRequest>();

            foreach (MergedChannel channel in channels)
            {
                xmlWriter.WriteStartElement("channel");

                xmlWriter.WriteAttributeString("channelNumber", channel.ChannelNumber.ToString());
                xmlWriter.WriteAttributeString("callSign", channel.CallSign);
                xmlWriter.WriteAttributeString("matchName", channel.PrimaryChannel.MatchName);
                xmlWriter.WriteAttributeString("uid", getUniqueID(channel.PrimaryChannel.UIds));
                xmlWriter.WriteAttributeString("lineup", channel.Lineup.Name);

                xmlWriter.WriteStartElement("tuningInfos");

                foreach (TuningInfo tuningInfo in channel.TuningInfos)
                {
                    DVBTuneRequest dvbTuneRequest = tuningInfo.TuneRequest as DVBTuneRequest;
                    if (dvbTuneRequest != null)
                    {
                        bool alreadyDone = checkDVBProcessed(dvbProcessed, dvbTuneRequest);
                        if (!alreadyDone)
                        {
                            xmlWriter.WriteStartElement("dvbTuningInfo");

                            xmlWriter.WriteAttributeString("frequency", dvbTuneRequest.Locator.CarrierFrequency.ToString());
                            xmlWriter.WriteAttributeString("onid", dvbTuneRequest.ONID.ToString());
                            xmlWriter.WriteAttributeString("tsid", dvbTuneRequest.TSID.ToString());
                            xmlWriter.WriteAttributeString("sid", dvbTuneRequest.SID.ToString());

                            xmlWriter.WriteEndElement();

                            dvbProcessed.Add(dvbTuneRequest);
                        }
                    }
                    else
                    {
                        ATSCChannelTuneRequest atscTuneRequest = tuningInfo.TuneRequest as ATSCChannelTuneRequest;
                        if (atscTuneRequest != null)
                        {
                            bool alreadyDone = checkATSCProcessed(atscProcessed, atscTuneRequest);
                            if (!alreadyDone)
                            {
                                xmlWriter.WriteStartElement("atscTuningInfo");

                                xmlWriter.WriteAttributeString("frequency", atscTuneRequest.Locator.CarrierFrequency.ToString());
                                xmlWriter.WriteAttributeString("majorChannel", atscTuneRequest.Channel.ToString());
                                xmlWriter.WriteAttributeString("minorChannel", atscTuneRequest.MinorChannel.ToString());

                                xmlWriter.WriteEndElement();

                                atscProcessed.Add(atscTuneRequest);
                            }
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
        }

        private static bool checkDVBProcessed(Collection<DVBTuneRequest> dvbProcessed, DVBTuneRequest newRequest)
        {
            foreach (DVBTuneRequest existingRequest in dvbProcessed)
            {
                if (existingRequest.Locator.CarrierFrequency == newRequest.Locator.CarrierFrequency &&
                    existingRequest.ONID == newRequest.ONID &&
                    existingRequest.TSID == newRequest.TSID &&
                    existingRequest.SID == newRequest.SID)
                    return (true);
            }

            return (false);
        }

        private static bool checkATSCProcessed(Collection<ATSCChannelTuneRequest> atscProcessed, ATSCChannelTuneRequest newRequest)
        {
            foreach (ATSCChannelTuneRequest existingRequest in atscProcessed)
            {
                if (existingRequest.Locator.CarrierFrequency == newRequest.Locator.CarrierFrequency &&
                    existingRequest.Channel == newRequest.Channel &&
                    existingRequest.MinorChannel == newRequest.MinorChannel)
                    return (true);
            }

            return (false);
        }

        private static string getUniqueID(UIds uids)
        {
            if (uids == null)
                return (null);

            IList<UId> uidList = uids.ToList();
            if (uidList.Count == 0)
                return (null);

            foreach (UId uid in uidList)
            {
                if (uid.GetFullName().Contains("EPGCollector"))
                    return (uid.GetFullName());
            }

            return (uidList[0].GetFullName());
        }

        private static void createRecordingsSection(XmlWriter xmlWriter, Collection<Recording> recordings)
        {
            xmlWriter.WriteStartElement("recordings");

            foreach (Recording recording in recordings)
            {
                xmlWriter.WriteStartElement("recording");

                xmlWriter.WriteAttributeString("title", recording.Program.Title);
                xmlWriter.WriteAttributeString("description", recording.Program.ShortDescription);
                xmlWriter.WriteAttributeString("startTime", recording.ContentStartTime.ToString(CultureInfo.InvariantCulture));
                xmlWriter.WriteAttributeString("seasonNumber", recording.Program.SeasonNumber.ToString());
                xmlWriter.WriteAttributeString("episodeNumber", recording.Program.EpisodeNumber.ToString());

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
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

            Environment.Exit(0);
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

