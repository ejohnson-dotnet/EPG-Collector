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

using System.IO;
using System.Text;
using System.Xml;
using System.Collections.ObjectModel;

namespace DomainObjects
{
    internal sealed class OutputFileAreaRegionChannels
    {
        private static string actualFileName;

        private OutputFileAreaRegionChannels() { }

        internal static void Process(string fileName)
        {
            if (RunParameters.Instance.AreaRegionFileName != null)
                actualFileName = RunParameters.Instance.AreaRegionFileName;
            else
                actualFileName = Path.Combine(Path.GetDirectoryName(fileName), "AreaRegionChannelInfo.xml");

            try
            {
                Logger.Instance.Write("Deleting any existing version of Area/Region channel file");
                File.SetAttributes(actualFileName, FileAttributes.Normal);
                File.Delete(actualFileName);
            }
            catch (IOException e)
            {
                Logger.Instance.Write("File delete exception: " + e.Message);
            }

            Logger.Instance.Write("Creating Area/Region channel file: " + actualFileName);

            Collection<TVStation> processedStations = new Collection<TVStation>();

            XmlWriterSettings settings = new XmlWriterSettings();

            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            settings.Encoding = new UTF8Encoding(false);
            settings.CloseOutput = true;

            using (XmlWriter xmlWriter = XmlWriter.Create(actualFileName, settings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("areas");

                if (Bouquet.Bouquets != null && Bouquet.Bouquets.Count != 0)
                {
                    foreach (Bouquet bouquet in Bouquet.Bouquets)
                    {
                        bool writeStart = true;

                        foreach (Region region in bouquet.Regions)
                        {
                            bool include = checkArea(bouquet.BouquetID, region.Code);
                            if (include)
                            {
                                if (writeStart)
                                {
                                    xmlWriter.WriteStartElement("area");
                                    xmlWriter.WriteAttributeString("id", bouquet.BouquetID.ToString());
                                    xmlWriter.WriteAttributeString("name", string.IsNullOrWhiteSpace(bouquet.Name) ? "** Not Present **" : bouquet.Name);
                                    writeStart = false;
                                }

                                xmlWriter.WriteStartElement("region");
                                xmlWriter.WriteAttributeString("id", region.Code.ToString());
                                xmlWriter.WriteAttributeString("name", string.IsNullOrWhiteSpace(region.Name) ? "** Not Present **" : region.Name);

                                foreach (Channel channel in region.GetChannelsInChannelNumberOrder())
                                {
                                    {
                                        TVStation station = TVStation.FindStation(RunParameters.Instance.StationCollection,
                                            channel.OriginalNetworkID, channel.TransportStreamID, channel.ServiceID);
                                        if (station != null)
                                        {
                                            if (!OptionEntry.IsDefined(OptionName.CreateChannelDefFile) || (OptionEntry.IsDefined(OptionName.CreateChannelDefFile) && station.InChannelDefinitions))
                                            {
                                                xmlWriter.WriteStartElement("channel");

                                                xmlWriter.WriteAttributeString("id", channel.UserChannel.ToString());
                                                xmlWriter.WriteAttributeString("nid", channel.OriginalNetworkID.ToString());
                                                xmlWriter.WriteAttributeString("tid", channel.TransportStreamID.ToString());
                                                xmlWriter.WriteAttributeString("sid", channel.ServiceID.ToString());

                                                if (station.NewName == null)
                                                    xmlWriter.WriteAttributeString("name", station.Name);
                                                else
                                                    xmlWriter.WriteAttributeString("name", station.NewName);

                                                xmlWriter.WriteEndElement();

                                                processedStations.Add(station);
                                            }
                                        }
                                    }
                                }

                                xmlWriter.WriteEndElement();
                            }
                        }

                        if (!writeStart)
                            xmlWriter.WriteEndElement();
                    }
                } 

                bool firstRegion = true;

                processOtherChannels(xmlWriter, processedStations, new int[] { 1, 22, 23, 24 }, 65536, "TV SD", ref firstRegion);
                processOtherChannels(xmlWriter, processedStations, new int[] { 17, 25, 26, 27 }, 65537, "TV HD", ref firstRegion);
                processOtherChannels(xmlWriter, processedStations, new int[] { 2, 10 }, 65538, "Radio", ref firstRegion);

                if (!firstRegion)
                    xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();
                xmlWriter.Close();
            }
        }

        private static bool checkArea(int bouquet, int region)
        {
            foreach (TuningFrequency tuningFrequency in RunParameters.Instance.FrequencyCollection)
            {
                if (tuningFrequency.AdvancedRunParamters != null)
                {
                    if (tuningFrequency.AdvancedRunParamters.ChannelBouquet == -1)
                        return (true);
                    else
                    {
                        if (bouquet != tuningFrequency.AdvancedRunParamters.ChannelBouquet)
                            return (false);

                        if (tuningFrequency.AdvancedRunParamters.ChannelRegion == -1 || region == 65535)
                            return (true);
                            
                        return (region == tuningFrequency.AdvancedRunParamters.ChannelRegion);
                    }
                }
            }
            
            return (false);
        }

        private static void processOtherChannels(XmlWriter xmlWriter, Collection<TVStation> processedStations, int[] serviceTypes, int regionId, string typeDescription, ref bool firstRegion)
        {
            bool firstChannel = true;

            foreach (TVStation station in RunParameters.Instance.StationCollection)
            {
                if (isOtherChannelRelevant(station, processedStations, serviceTypes))
                {
                    if (firstRegion)
                    {
                        xmlWriter.WriteStartElement("area");
                        xmlWriter.WriteAttributeString("id", "65536");
                        xmlWriter.WriteAttributeString("name", "** Not Present **");

                        firstRegion = false;
                    }

                    if (firstChannel)
                    {
                        xmlWriter.WriteStartElement("region");
                        xmlWriter.WriteAttributeString("id", regionId.ToString());
                        xmlWriter.WriteAttributeString("name", typeDescription);

                        firstChannel = false;
                    }

                    xmlWriter.WriteStartElement("channel");

                    xmlWriter.WriteAttributeString("id", station.LogicalChannelNumber.ToString());
                    xmlWriter.WriteAttributeString("nid", station.OriginalNetworkID.ToString());
                    xmlWriter.WriteAttributeString("tid", station.TransportStreamID.ToString());
                    xmlWriter.WriteAttributeString("sid", station.ServiceID.ToString());

                    if (station.NewName == null)
                        xmlWriter.WriteAttributeString("name", station.Name);
                    else
                        xmlWriter.WriteAttributeString("name", station.NewName);

                    xmlWriter.WriteEndElement();
                }
            }

            if (!firstChannel)
                xmlWriter.WriteEndElement();
        }

        private static bool isServiceType(int[] serviceTypes, int serviceType)
        {
            foreach (int type in serviceTypes)
            {
                if (type == serviceType)
                    return true;
            }

            return false;
        }

        private static bool isOtherChannelRelevant(TVStation station, Collection<TVStation> processedStations, int[] serviceTypes)
        {
            if (processedStations.Contains(station))
                return false;
            
            if (station.TunerType == TunerType.Satellite)
                return false;
 
            if (station.LogicalChannelNumber == -1)
                return false;
            
            if (!isServiceType(serviceTypes, station.ServiceType))
                return false;

            if (OptionEntry.IsDefined(OptionName.CreateChannelDefFile) && !station.InChannelDefinitions)
                return false;

            return true;
        }
    }
}


