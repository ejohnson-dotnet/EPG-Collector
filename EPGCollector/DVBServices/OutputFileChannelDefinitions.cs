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
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Text;
using System.Reflection;
using System.Threading;

using DomainObjects;
using DirectShow;
using SatIp;
using VBox;

namespace DVBServices
{
    /// <summary>
    /// The class that creates the Channel Definitions file.
    /// </summary>
    public class OutputFileChannelDefinitions
    {
        private static Collection<SourceSpec> sources = new Collection<SourceSpec>();

        private OutputFileChannelDefinitions() { }

        /// <summary>
        /// Create a souce section in the file.
        /// </summary>
        /// <returns></returns>
        public static string Process()
        {
            if (!OptionEntry.IsDefined(OptionName.CreateChannelDefFile))
                return null;

            foreach (NetworkInformationSection networkInformationSection in NetworkInformationSection.NetworkInformationSections)
            {
                if (networkInformationSection.IsSatellite)
                    processNetworkInformation(sources, networkInformationSection);
            }

            return null;               
        }

        /// <summary>
        /// Complete the file.
        /// </summary>
        /// <returns></returns>
        public static string Finish()
        {
            if (!OptionEntry.IsDefined(OptionName.CreateChannelDefFile))
                return null;

            Logger.Instance.WriteSeparator("Generating Channel Definition File");
            processTerrestrialFrequencies(sources);

            return createFile(sources);
        }

        private static void processNetworkInformation(Collection<SourceSpec> sources, NetworkInformationSection networkInformationSection)
        {
            bool newSource = false;
            
            SatelliteSourceSpec source = (SatelliteSourceSpec)findSource(sources, networkInformationSection.NetworkID, networkInformationSection.NetworkName);
            if (source == null)
            {
                source = new SatelliteSourceSpec("DVB-S", networkInformationSection.NetworkID, networkInformationSection.NetworkName, networkInformationSection.TransportStreams[0].OrbitalPosition);
                newSource = true;
            }
            
            SatelliteFrequency satelliteFrequency = RunParameters.Instance.CurrentFrequency as SatelliteFrequency;
            if (satelliteFrequency != null)
            {
                source.LNBHighBand = satelliteFrequency.SatelliteDish.LNBHighBandFrequency;
                source.LNBLowBand = satelliteFrequency.SatelliteDish.LNBLowBandFrequency;
                source.LNBSwitch = satelliteFrequency.SatelliteDish.LNBSwitchFrequency;                

                if (satelliteFrequency.DiseqcRunParamters.DiseqcSwitch != null)
                    source.DiseqcSwitch = satelliteFrequency.DiseqcRunParamters.DiseqcSwitch;
            }

            foreach (TransportStream transportStream in networkInformationSection.TransportStreams)
                processTransportStream(source, transportStream);

            if (newSource && source.Frequencies.Count != 0)
                sources.Add(source);
        }

        private static SourceSpec findSource(Collection<SourceSpec> sources, int networkId, string networkName)
        {
            foreach (SourceSpec source in sources)
            {
                if (source.Identity == networkId && source.Name == networkName)
                    return source;
            }

            return null;
        }

        private static void processTransportStream(SourceSpec source, TransportStream transportStream)
        {
            FrequencySpec frequencySpec = null;

            foreach (DescriptorBase descriptor in transportStream.Descriptors)
            {
                DVBSatelliteDeliverySystemDescriptor satelliteDescriptor = descriptor as DVBSatelliteDeliverySystemDescriptor;
                if (satelliteDescriptor != null)
                    frequencySpec = processSatelliteDescriptor(source, transportStream, satelliteDescriptor);                
            }

            if (frequencySpec == null)
                return;

            if (transportStream.ServiceList != null && transportStream.ServiceList.Count != 0)
            {
                foreach (ServiceListEntry service in transportStream.ServiceList)
                {
                    TVStation station = TVStation.FindStation(RunParameters.Instance.StationCollection, transportStream.OriginalNetworkID, transportStream.TransportStreamID, service.ServiceID);
                    if (station != null)
                        frequencySpec.Channels.Add(station);
                }
            }
            else
            {
                foreach (TVStation station in RunParameters.Instance.StationCollection)
                {
                    if (station.OriginalNetworkID == transportStream.OriginalNetworkID && station.TransportStreamID == transportStream.TransportStreamID)
                        frequencySpec.Channels.Add(station);
                }
            }

            if (frequencySpec.Channels.Count != 0)
                source.Frequencies.Add(frequencySpec);
        }

        private static FrequencySpec processSatelliteDescriptor(SourceSpec source, TransportStream transportStream, DVBSatelliteDeliverySystemDescriptor satelliteDescriptor)
        {
            SatelliteFrequency frequency = new SatelliteFrequency();

            frequency.Frequency = satelliteDescriptor.Frequency * 10;
            frequency.DVBPolarization = satelliteDescriptor.Polarization;
            frequency.SymbolRate = satelliteDescriptor.SymbolRate / 10;
            frequency.FEC.DVBRate = satelliteDescriptor.InnerFEC;
            frequency.ModulationSystem = satelliteDescriptor.IsS2 ? 1 : 0;
            frequency.DVBModulation = satelliteDescriptor.ModulationType;

            return new FrequencySpec(frequency, 0);
        }

        private static void processTerrestrialFrequencies(Collection<SourceSpec> sources)
        {
            Collection<Provider> providers = new Collection<Provider>();

            foreach (TuningFrequency frequency in RunParameters.Instance.FrequencyCollection)
            {
                if (frequency.TunerType == TunerType.Terrestrial)
                {
                    if (!providers.Contains(frequency.Provider))
                        providers.Add(frequency.Provider);
                }
            }

            if (providers.Count == 0)
                return;

            TerrestrialProvider.Load();
            
            Logger.Instance.Write("There are " + providers.Count + " terrestrial providers to process");

            foreach (Provider provider in providers)
                processTerrestrialProvider(sources, provider);
        }

        private static void processTerrestrialProvider(Collection<SourceSpec> sources, Provider provider)
        {
            Logger.Instance.Write("Processing " + provider.Name);

            TerrestrialProvider loadedProvider = TerrestrialProvider.FindProvider(provider.Name);
            if (loadedProvider == null)
            {
                Logger.Instance.Write("Failed to find provider " + provider.Name + " - frequencies will not be included in the Channel Definitions file");
                return;
            }

            SourceSpec sourceSpec = new SourceSpec("DVB-T", 0, string.Empty, provider.Name);
            Collection<DVBTerrestrialDeliverySystemDescriptor> nitFrequencies = new Collection<DVBTerrestrialDeliverySystemDescriptor>();

            Logger.Instance.Write("There are " + loadedProvider.Frequencies.Count + " frequencies to process for provider " + provider.Name);

            foreach (TerrestrialFrequency frequency in loadedProvider.Frequencies)
                processTerrestrialFrequency(sources, sourceSpec, frequency, nitFrequencies);

            foreach (FrequencySpec frequencySpec in sourceSpec.Frequencies)
                removeFromNitFrequencies(nitFrequencies, frequencySpec.Frequency);

            Logger.Instance.Write("There are " + nitFrequencies.Count + " Network Information only frequencies to process");

            foreach (DVBTerrestrialDeliverySystemDescriptor frequency in nitFrequencies)
                processTerrestrialFrequency(sources, sourceSpec, createFrequency(frequency, loadedProvider), null);

            if (sourceSpec.Frequencies.Count != 0)
                sources.Add(sourceSpec);
        }

        private static void removeFromNitFrequencies(Collection<DVBTerrestrialDeliverySystemDescriptor> nitFrequencies, TuningFrequency frequency)
        {
            foreach (DVBTerrestrialDeliverySystemDescriptor terrestrialDescriptor in nitFrequencies)
            {
                if (normalizeDVBFrequency(terrestrialDescriptor.Frequency) == frequency.Frequency)
                {
                    nitFrequencies.Remove(terrestrialDescriptor);
                    return;
                }
            }
        }

        private static int normalizeDVBFrequency(int dvbFrequency)
        {
            return (dvbFrequency / 100000) * 1000;
        }

        private static int normalizeTuningFileFrequency(TuningFrequency frequency)
        {
            return (frequency.Frequency / 1000) * 1000;
        }

        private static TerrestrialFrequency createFrequency(DVBTerrestrialDeliverySystemDescriptor deliveryDescriptor, Provider provider)
        {
            TerrestrialFrequency frequency = new TerrestrialFrequency();
            
            frequency.Frequency = deliveryDescriptor.Frequency / 100;
            frequency.Provider = provider;

            switch (deliveryDescriptor.Bandwidth)
            {
                case 0:
                    frequency.Bandwidth = 8;
                    break;
                case 1:
                    frequency.Bandwidth = 7;
                    break;
                case 2:
                    frequency.Bandwidth = 6;
                    break;
                case 3:
                    frequency.Bandwidth = 5;
                    break;
                default:
                    break;
            }

            return frequency;
        }

        private static void processTerrestrialFrequency(Collection<SourceSpec> sources, SourceSpec sourceSpec, TerrestrialFrequency frequency, Collection<DVBTerrestrialDeliverySystemDescriptor> nitFrequencies)
        {
            Thread.Sleep(3000);

            Logger.Instance.Write("Tuning to " + frequency.ToString());
            ITunerDataProvider dataProvider = tuneFrequency(frequency);
            Logger.Instance.QuietMode = false;

            if (dataProvider == null)
            {
                Logger.Instance.Write("Failed to tune " + frequency.ToString() + " - channels will not be included");
                return;
            }
                
            Collection<NetworkInformationSection> networkInformationSections = getNetworkInfo((ISampleDataProvider)dataProvider);
            int normalizedFrequency = normalizeTuningFileFrequency(frequency);
            bool serviceListFound = false;

            foreach (NetworkInformationSection networkSection in networkInformationSections)
            {
                if (string.IsNullOrWhiteSpace(sourceSpec.Name))
                {
                    sourceSpec.Identity = networkSection.NetworkID;
                    sourceSpec.Name = networkSection.NetworkName;
                }

                if (networkSection.TransportStreams != null)
                {
                    foreach (TransportStream transportStream in networkSection.TransportStreams)
                    {
                        if (transportStream.Descriptors != null)
                        {
                            foreach (DescriptorBase descriptor in transportStream.Descriptors)
                            {
                                DVBServiceListDescriptor serviceListDescriptor = descriptor as DVBServiceListDescriptor;
                                if (serviceListDescriptor != null)
                                {
                                    if (normalizeDVBFrequency(transportStream.Frequency) == normalizedFrequency)
                                    {
                                        foreach (ServiceListEntry serviceListEntry in serviceListDescriptor.ServiceList)
                                            addServiceEntry(sources, sourceSpec, frequency, dataProvider.SignalStrength, transportStream.OriginalNetworkID, transportStream.TransportStreamID, serviceListEntry.ServiceID);
                                        serviceListFound = true;
                                    }
                                }
                                else
                                {
                                    DVBTerrestrialDeliverySystemDescriptor terrestrialDeliverySystem = descriptor as DVBTerrestrialDeliverySystemDescriptor;
                                    if (terrestrialDeliverySystem != null && nitFrequencies != null)
                                        addNitFrequency(nitFrequencies, terrestrialDeliverySystem, frequency.Provider.Frequencies);
                                }
                            }
                        }
                    }
                }                    
            }

            if (serviceListFound)
            {
                ((ITunerDataProvider)dataProvider).Dispose();
                return;
            }

            Logger.Instance.Write("Getting service list from PAT information");
            Collection<ProgramAssociationSection> patSections = getPatInfo((ISampleDataProvider)dataProvider);

            ((ITunerDataProvider)dataProvider).Dispose();

            if (patSections == null || patSections.Count == 0)
            {
                Logger.Instance.Write("No PAT data available - no channels added");
                return;
            }

            Collection<ProgramInfo> programInfos = new Collection<ProgramInfo>();

            foreach (ProgramAssociationSection programAssociationSection in patSections)
            {
                if (programAssociationSection.ProgramInfos != null)
                {
                    foreach (ProgramInfo programInfo in programAssociationSection.ProgramInfos)
                    {
                        if (programInfo.ProgramNumber != 0)
                            addProgramInfo(programInfo, programInfos);
                    }
                }
            }

            foreach (ProgramInfo programInfo in programInfos)
            {
                TVStation channel = TVStation.FindStation(RunParameters.Instance.StationCollection, 0, patSections[0].TransportStreamID, programInfo.ProgramNumber);
                if (channel != null)
                {
                    foreach (NetworkInformationSection section in networkInformationSections)
                    {
                        if (section.NetworkID == channel.OriginalNetworkID && section.TransportStreams != null)
                        {
                            foreach (TransportStream transportStream in section.TransportStreams)
                            {
                                if (transportStream.TransportStreamID == channel.TransportStreamID)
                                    frequency.PlpNumber = transportStream.IsS2 ? frequency.PlpNumber = 0 : frequency.PlpNumber = -1; 
                            }
                        }
                    }

                    addServiceEntry(sources, sourceSpec, frequency, dataProvider.SignalStrength, channel.OriginalNetworkID, channel.TransportStreamID, channel.ServiceID);
                }
                else
                    Logger.Instance.Write("Channel " + patSections[0].TransportStreamID + ":" + programInfo.ProgramNumber + " not in SDT - channel ignored");
            }
        }

        private static void addNitFrequency(Collection<DVBTerrestrialDeliverySystemDescriptor> nitFrequencies, DVBTerrestrialDeliverySystemDescriptor newFrequency, Collection<TuningFrequency> providerFrequencies)
        {
            if (newFrequency.Frequency == 0)
                return;

            int normalizedNewFrequency = normalizeDVBFrequency(newFrequency.Frequency);

            foreach (TuningFrequency providerFrequency in providerFrequencies)
            {
                if (normalizedNewFrequency == normalizeTuningFileFrequency(providerFrequency))
                    return;
            }

            foreach (DVBTerrestrialDeliverySystemDescriptor oldFrequency in nitFrequencies)
            {
                if (normalizeDVBFrequency(oldFrequency.Frequency) == normalizedNewFrequency)
                    return;
            }

            nitFrequencies.Add(newFrequency);
            Logger.Instance.Write("Added frequency " + normalizedNewFrequency + " to NIT only list");
        }

        private static Collection<ProgramAssociationSection> getPatInfo(ISampleDataProvider dataProvider)
        {
            dataProvider.ChangePidMapping(new int[] { BDAGraph.PatPid });

            TSReaderBase patReader = new TSStreamReader(BDAGraph.PatTable, 2000, dataProvider.BufferAddress);
            patReader.Run();

            Collection<ProgramAssociationSection> programAssociationSections = new Collection<ProgramAssociationSection>();
            Collection<int> sectionNumbers = new Collection<int>();

            Collection<Mpeg2Section> sections = new Collection<Mpeg2Section>();

            bool done = false;
            int lastCount = 0;
            int repeats = 0;
            int lastSectionNumber = Int16.MinValue;

            while (!done)
            {
                Thread.Sleep(2000);

                sections.Clear();

                patReader.Lock("ProcessPATSections");
                if (patReader.Sections.Count != 0)
                {
                    foreach (Mpeg2Section section in patReader.Sections)
                        sections.Add(section);
                    patReader.Sections.Clear();
                }
                patReader.Release("ProcessPATSections");

                foreach (Mpeg2Section section in sections)
                {
                    ProgramAssociationSection programAssociationSection = ProgramAssociationSection.ProcessProgramAssociationTable(section.Data);
                    if (programAssociationSection != null)
                    {
                        lastSectionNumber = programAssociationSection.LastSectionNumber;

                        if (!sectionNumbers.Contains(programAssociationSection.SectionNumber))
                        {
                            programAssociationSections.Add(programAssociationSection);
                            sectionNumbers.Add(programAssociationSection.SectionNumber);
                        }
                    }
                }

                done = sectionNumbers.Count == lastSectionNumber + 1;

                if (!done)
                {
                    if (sectionNumbers.Count == lastCount)
                    {
                        repeats++;
                        done = (repeats == RunParameters.Instance.Repeats);
                    }
                    else
                        repeats = 0;

                    lastCount = sectionNumbers.Count;
                }
            }

            patReader.Stop();

            return programAssociationSections;
        }

        private static void addProgramInfo(ProgramInfo newProgramInfo, Collection<ProgramInfo> programInfos)
        {
            foreach (ProgramInfo oldProgramInfo in programInfos)
            {
                if (oldProgramInfo.ProgramID == newProgramInfo.ProgramID)
                    return;
            }

            programInfos.Add(newProgramInfo);
        }

        private static ITunerDataProvider tuneFrequency(TerrestrialFrequency frequency)
        {
            Logger.Instance.Write("Tuning to frequency " + frequency.Frequency + " on " + frequency.TunerType);

            TuningSpec tuningSpec = new TuningSpec((TerrestrialFrequency)frequency);
            TunerNodeType tunerNodeType = TunerNodeType.Terrestrial;
            
            bool finished = false;
            int frequencyRetries = 0;

            Tuner currentTuner = null;
            ITunerDataProvider dataProvider = null;

            while (!finished)
            {
                if (!DebugEntry.IsDefined(DebugName.NotQuiet))
                    Logger.Instance.QuietMode = true;

                dataProvider = BDAGraph.FindTuner(frequency.SelectedTuners, tunerNodeType, tuningSpec, currentTuner);
                if (dataProvider == null)
                {
                    dataProvider = SatIpController.FindReceiver(frequency.SelectedTuners, tunerNodeType, tuningSpec, currentTuner, 0);
                    if (dataProvider == null)
                    {
                        dataProvider = VBoxController.FindReceiver(frequency.SelectedTuners, tunerNodeType, tuningSpec, currentTuner, 0, false);
                        if (dataProvider == null)
                        {
                            Logger.Instance.Write("<e> No tuner able to tune frequency " + frequency.ToString());
                            return null;
                        }
                    }
                }

                TimeSpan timeout = new TimeSpan();
                bool done = false;
                bool locked = false;

                while (!done)
                {
                    locked = dataProvider.SignalLocked;
                    if (!locked)
                    {
                        if (dataProvider.SignalQuality > 0)
                        {
                            locked = true;
                            done = true;
                        }
                        else
                        {
                            if (dataProvider.SignalPresent)
                            {
                                locked = true;
                                done = true;
                            }
                            else
                            {
                                Logger.Instance.Write("Signal not acquired: lock is " + dataProvider.SignalLocked + " quality is " + dataProvider.SignalQuality + " signal not present");
                                Thread.Sleep(1000);
                                timeout = timeout.Add(new TimeSpan(0, 0, 1));
                                done = (timeout.TotalSeconds == RunParameters.Instance.LockTimeout.TotalSeconds);
                            }
                        }
                    }
                    else
                        done = true;
                }

                if (!locked)
                {
                    Logger.Instance.Write("<e> Failed to acquire signal");
                    dataProvider.Dispose();

                    if (frequencyRetries == 2)
                    {
                        currentTuner = dataProvider.Tuner;
                        frequencyRetries = 0;
                    }
                    else
                    {
                        frequencyRetries++;
                        Logger.Instance.Write("Retrying frequency");
                    }
                }
                else
                {
                    finished = true;
                    Logger.Instance.QuietMode = false;
                    Logger.Instance.Write("Signal acquired: lock is " + dataProvider.SignalLocked + " quality is " + dataProvider.SignalQuality + " strength is " + dataProvider.SignalStrength);
                }
            }

            Logger.Instance.QuietMode = false;
            return (dataProvider);
        }

        private static Collection<NetworkInformationSection> getNetworkInfo(ISampleDataProvider dataProvider)
        {
            Logger.Instance.Write("Loading Network Information");

            dataProvider.ChangePidMapping(BDAGraph.NitPid);

            Collection<byte> tables = new Collection<byte>();
            tables.Add(0x40);
            tables.Add(0x41);
            TSStreamReader nitReader = new TSStreamReader(tables, 50000, dataProvider.BufferAddress);
            nitReader.Run();

            bool done = false;
            int lastCount = 0;
            int repeats = 0;

            Collection<NetworkInformationSection> networkInformationSections = new Collection<NetworkInformationSection>();

            while (!done)
            {
                Thread.Sleep(2000);

                Collection<Mpeg2Section> sections = new Collection<Mpeg2Section>();

                nitReader.Lock("ProcessNITSections");
                if (nitReader.Sections.Count != 0)
                {
                    foreach (Mpeg2Section section in nitReader.Sections)
                        sections.Add(section);
                    nitReader.Sections.Clear();
                }
                nitReader.Release("ProcessNITSections");

                foreach (Mpeg2Section section in sections)
                {
                    NetworkInformationSection networkInformationSection = NetworkInformationSection.ProcessNetworkInformationTable(section.Data);
                    if (networkInformationSection != null)
                        addNetworkInformationSection(networkInformationSections, networkInformationSection);
                }

                if (NetworkInformationSection.NetworkInformationSections.Count == lastCount)
                {
                    repeats++;
                    done = (repeats == 10);
                }
                else
                    repeats = 0;

                lastCount = NetworkInformationSection.NetworkInformationSections.Count;
            }

            nitReader.Stop();

            Logger.Instance.Write("Finished loading Network Information - " + networkInformationSections.Count + " sections loaded");

            return networkInformationSections;
        }

        private static void addNetworkInformationSection(Collection<NetworkInformationSection> networkInformationSections, NetworkInformationSection newSection)
        {
            foreach (NetworkInformationSection networkInformationSection in networkInformationSections)
            {
                if (networkInformationSection.NetworkID == newSection.NetworkID && networkInformationSection.SectionNumber == newSection.SectionNumber)
                    return;
            }

            networkInformationSections.Add(newSection);
        }

        private static void addServiceEntry(Collection<SourceSpec> sources, SourceSpec sourceSpec, TerrestrialFrequency frequency, long signalStrength, int onid, int tsid, int sid)
        {
            TVStation newChannel = TVStation.FindStation(RunParameters.Instance.StationCollection, onid, tsid, sid);
            if (newChannel == null)
            {
                Logger.Instance.Write("Channel " + onid + ":" + tsid + ":" + sid + " is not in the SDT and will be ignored");
                return;
            }

            int strongerFrequency = isOtherSignalStronger(sources, onid, tsid, sid, signalStrength);
            if (strongerFrequency != -1)
            {
                Logger.Instance.Write("Channel " + newChannel.Name + " is on frequency " + strongerFrequency + " with a stronger signal"); 
                return;
            }

            foreach (FrequencySpec frequencySpec in sourceSpec.Frequencies)
            {
                if (frequencySpec.Frequency.Frequency == frequency.Frequency)
                {
                    frequencySpec.Channels.Add(newChannel);
                    return;
                }
            }

            FrequencySpec newFrequency = new FrequencySpec(frequency, signalStrength);
            sourceSpec.Frequencies.Add(newFrequency);

            newFrequency.Channels.Add(newChannel);
        }

        private static int isOtherSignalStronger(Collection<SourceSpec> sources, int onid, int tsid, int sid, long signalStrength)
        {
            foreach (SourceSpec source in sources)
            {
                if (source.Type == "DVB-T")
                {
                    foreach (FrequencySpec frequencySpec in source.Frequencies)
                    {
                        foreach (TVStation channel in frequencySpec.Channels)
                        {
                            if (channel.OriginalNetworkID == onid && channel.TransportStreamID == tsid && channel.ServiceID == sid)
                            {
                                if (frequencySpec.SignalStrength >= signalStrength)
                                    return frequencySpec.Frequency.Frequency;

                                frequencySpec.Channels.Remove(channel);

                                if (frequencySpec.Channels.Count == 0)
                                {
                                    source.Frequencies.Remove(frequencySpec);
                                    if (source.Frequencies.Count == 0)
                                        sources.Remove(source);             
                                }

                                return -1;
                            }
                        }
                    }
                }
            }

            return -1;
        }

        private static string createFile(Collection<SourceSpec> sources)
        {
            Logger.Instance.Write("Creating file");

            string path;

            if (!string.IsNullOrWhiteSpace(RunParameters.Instance.ChannelDefinitionFileName))
                path = RunParameters.Instance.ChannelDefinitionFileName;
            else
                path = Path.Combine(RunParameters.DataDirectory, "Channel Definitions.xml");

            try
            {
                Logger.Instance.Write("Deleting any existing version of the file");
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
            }
            catch (IOException e)
            {
                Logger.Instance.Write("File delete exception: " + e.Message);
            }

            Logger.Instance.Write("Creating file: " + path);

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            settings.Encoding = new UTF8Encoding(false);
            settings.CloseOutput = true;

            XmlWriter xmlWriter = null;

            try
            {
                xmlWriter = XmlWriter.Create(path, settings);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Sources");

                xmlWriter.WriteAttributeString("generator-info-name", Assembly.GetCallingAssembly().GetName().Name
                        + "/" + Assembly.GetCallingAssembly().GetName().Version.ToString());

                foreach (SourceSpec source in sources)
                    outputSource(xmlWriter, source);

                xmlWriter.WriteEndElement(); // close Sources element
                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();
                xmlWriter.Close();

                Logger.Instance.Write("Finished - file created");

                return null;
            }
            catch (Exception e)
            {
                Logger.Instance.Write("Failed to create Channel Definitions file: " + e.Message);

                if (xmlWriter != null)
                    xmlWriter.Close();

                return e.Message;
            }
        }

        private static void outputSource(XmlWriter xmlWriter, SourceSpec source)
        {
            xmlWriter.WriteStartElement("Source");

            xmlWriter.WriteAttributeString("type", source.Type);
            
            if (source.Identity != 0)
                xmlWriter.WriteAttributeString("id", source.Identity.ToString());
            else
                xmlWriter.WriteAttributeString("id", "Not Available");
            
            if (!string.IsNullOrWhiteSpace(source.Name))
                xmlWriter.WriteAttributeString("name", source.Name);
            else
                xmlWriter.WriteAttributeString("name", "Not Available");

            if (!string.IsNullOrWhiteSpace(source.TuningId))
                xmlWriter.WriteAttributeString("tuningId", source.TuningId);
            else
                xmlWriter.WriteAttributeString("tuningId", "Not Available");

            if (source.Type == "DVB-S")
            {
                SatelliteSourceSpec satelliteSource = source as SatelliteSourceSpec;
                
                xmlWriter.WriteStartElement("LNB");
                xmlWriter.WriteElementString("HighBand", ((SatelliteSourceSpec)source).LNBHighBand.ToString());
                xmlWriter.WriteElementString("LowBand", ((SatelliteSourceSpec)source).LNBLowBand.ToString());
                xmlWriter.WriteElementString("Switch", ((SatelliteSourceSpec)source).LNBSwitch.ToString());
                xmlWriter.WriteEndElement();

                if (!string.IsNullOrWhiteSpace(satelliteSource.DiseqcSwitch))
                {
                    xmlWriter.WriteStartElement("Diseqc");
                    xmlWriter.WriteElementString("Value", satelliteSource.DiseqcSwitch);
                    xmlWriter.WriteEndElement();
                }
            }

            xmlWriter.WriteStartElement("Transponders");

            foreach (FrequencySpec frequency in sortFrequencies(source.Frequencies))
            {
                xmlWriter.WriteStartElement("Transponder");

                SatelliteFrequency satelliteFrequency = frequency.Frequency as SatelliteFrequency;
                if (satelliteFrequency != null)
                {
                    xmlWriter.WriteElementString("Frequency", satelliteFrequency.Frequency.ToString());
                    xmlWriter.WriteElementString("Polarity", decodePolarization(satelliteFrequency.DVBPolarization));
                    xmlWriter.WriteElementString("SymbolRate", satelliteFrequency.SymbolRate.ToString());
                    xmlWriter.WriteElementString("FEC", decodeInnerFec(satelliteFrequency.FEC.DVBRate));
                    xmlWriter.WriteElementString("Standard", satelliteFrequency.IsS2 ? "DVB-S2" : "DVB-S");
                    xmlWriter.WriteElementString("Modulation", decodeSatelliteModulationType(satelliteFrequency.DVBModulation));
                }
                else
                {
                    TerrestrialFrequency terrestrialFrequency = frequency.Frequency as TerrestrialFrequency;
                    if (terrestrialFrequency != null)
                    {
                        xmlWriter.WriteElementString("Frequency", terrestrialFrequency.Frequency.ToString());
                        xmlWriter.WriteElementString("Bandwidth", terrestrialFrequency.Bandwidth.ToString());
                        xmlWriter.WriteElementString("Standard", terrestrialFrequency.IsT2 ? "DVB-T2" : "DVB-T");
                    }
                }

                xmlWriter.WriteStartElement("Channels");

                foreach (TVStation station in sortChannels(frequency.Channels))
                {
                    station.InChannelDefinitions = true;

                    xmlWriter.WriteStartElement("Channel");

                    xmlWriter.WriteAttributeString("onid", station.OriginalNetworkID.ToString());
                    xmlWriter.WriteAttributeString("tsid", station.TransportStreamID.ToString());
                    xmlWriter.WriteAttributeString("sid", station.ServiceID.ToString());

                    if (station.LogicalChannelNumber != -1)
                        xmlWriter.WriteElementString("ChannelNumber", station.LogicalChannelNumber.ToString());
                    if (station.MinorChannelNumber != -1)
                        xmlWriter.WriteElementString("ChannelSubNumber", station.MinorChannelNumber.ToString());

                    xmlWriter.WriteElementString("ChannelName", station.Name);
                    xmlWriter.WriteElementString("Encrypted", station.Encrypted ? "Y" : "N");
                    xmlWriter.WriteElementString("ServiceType", station.ServiceType.ToString());
                    xmlWriter.WriteElementString("Provider", station.ProviderName);
                    xmlWriter.WriteElementString("EpgPresent", station.NextFollowingAvailable || station.ScheduleAvailable ? "Y" : "N");

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();        // Close Channels tag

                xmlWriter.WriteEndElement();        // close Transponder element
            }

            xmlWriter.WriteEndElement();            // close Transponders element  
            xmlWriter.WriteEndElement();            // close Source element    
        }

        private static Collection<FrequencySpec> sortFrequencies(Collection<FrequencySpec> unsortedFrequencies)
        {
            Collection<FrequencySpec> sortedFrequencies = new Collection<FrequencySpec>();

            foreach (FrequencySpec frequencySpec in unsortedFrequencies)
                addFrequencySorted(sortedFrequencies, frequencySpec);

            return sortedFrequencies;
        }

        private static void addFrequencySorted(Collection<FrequencySpec> sortedFrequencies, FrequencySpec newFrequency)
        {
            foreach (FrequencySpec oldFrequency in sortedFrequencies)
            {
                if (oldFrequency.Frequency.Frequency == newFrequency.Frequency.Frequency)
                {
                    SatelliteFrequency newSatelliteFrequency = newFrequency.Frequency as SatelliteFrequency;
                    if (newSatelliteFrequency != null)
                    {
                        SatelliteFrequency oldSatelliteFrequency = oldFrequency.Frequency as SatelliteFrequency;
                        if (oldSatelliteFrequency.Polarization.Polarization.CompareTo(newSatelliteFrequency.Polarization.Polarization) > 0)
                        {
                            sortedFrequencies.Insert(sortedFrequencies.IndexOf(oldFrequency), newFrequency);
                            return;
                        }
                    }
                }
                else
                {
                    if (oldFrequency.Frequency.Frequency > newFrequency.Frequency.Frequency)
                    {
                        sortedFrequencies.Insert(sortedFrequencies.IndexOf(oldFrequency), newFrequency);
                        return;
                    }
                }
            }

            sortedFrequencies.Add(newFrequency);
        }

        private static Collection<TVStation> sortChannels(Collection<TVStation> unsortedChannels)
        {
            Collection<TVStation> sortedChannels = new Collection<TVStation>();

            foreach (TVStation channel in unsortedChannels)
                addChannelSorted(sortedChannels, channel);

            return sortedChannels;
        }

        private static void addChannelSorted(Collection<TVStation> sortedChannels, TVStation newChannel)
        {
            string newChannelName = string.IsNullOrWhiteSpace(newChannel.NewName) ? newChannel.Name : newChannel.NewName;

            foreach (TVStation oldChannel in sortedChannels)
            {
                string oldChannelName = string.IsNullOrWhiteSpace(oldChannel.NewName) ? oldChannel.Name : oldChannel.NewName;

                if (oldChannelName.CompareTo(newChannelName) > 0)
                {
                    sortedChannels.Insert(sortedChannels.IndexOf(oldChannel), newChannel);
                    return;                        
                }
            }

            sortedChannels.Add(newChannel);
        }

        private static string decodePolarization(int polarization)
        {
            switch (polarization)
            {
                case 0:
                    return ("H");
                case 1:
                    return ("V");
                case 2:
                    return ("L");
                case 3:
                    return ("R");
                default:
                    return ("Undefined " + polarization);
            }
        }

        private static string decodeInnerFec(int innerFec)
        {
            switch (innerFec)
            {
                case 1:
                    return ("12");
                case 2:
                    return ("23");
                case 3:
                    return ("34");
                case 4:
                    return ("56");
                case 5:
                    return ("78");
                case 6:
                    return ("89");
                case 7:
                    return ("35");
                case 8:
                    return ("45");
                case 9:
                    return ("91");
                default:
                    return ("Undefined " + innerFec);
            }
        }

        private static string decodeSatelliteModulationType(int modulationType)
        {
            switch (modulationType)
            {
                case 0:
                    return ("AUTO");
                case 1:
                    return ("QPSK");
                case 2:
                    return ("8PSK");
                case 3:
                    return ("16QAM");
                default:
                    return ("Undefined " + modulationType);
            }
        }

        internal class SourceSpec
        {
            internal string Type { get; private set; }
            internal int Identity { get; set; }
            internal string Name { get; set; }
            internal string TuningId { get; private set; }

            internal Collection<FrequencySpec> Frequencies { get; private set; }

            internal SourceSpec() { }

            internal SourceSpec(string type, int identity, string name, string tuningId)
            {
                Type = type;
                Identity = identity;
                Name = name;
                TuningId = tuningId;

                Frequencies = new Collection<FrequencySpec>();
            }
        }

        internal class SatelliteSourceSpec : SourceSpec
        {
            internal int LNBHighBand { get; set; }
            internal int LNBLowBand { get; set; }
            internal int LNBSwitch { get; set; }

            internal string DiseqcSwitch { get; set; }

            internal SatelliteSourceSpec(string type, int identity, string name, string tuningId) : base(type, identity, name, tuningId) { }
        }

        internal class FrequencySpec
        {
            internal TuningFrequency Frequency { get; private set; }
            internal long SignalStrength { get; private set; }

            internal Collection<TVStation> Channels { get; private set; }
            
            private FrequencySpec() { }

            internal FrequencySpec(TuningFrequency frequency)
            {
                Frequency = frequency;

                Channels = new Collection<TVStation>();
            }

            internal FrequencySpec(TuningFrequency frequency, long signalStrength) : this(frequency)
            {
                SignalStrength = signalStrength;
            }
        }
    }
}
