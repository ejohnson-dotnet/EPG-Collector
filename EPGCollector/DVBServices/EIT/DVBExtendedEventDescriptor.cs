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
    /// DVB Extended Event descriptor class.
    /// </summary>
    internal class DVBExtendedEventDescriptor : DescriptorBase
    {
        /// <summary>
        /// Get the item descriptions.
        /// </summary>
        public Collection<string> ItemDescriptions { get { return (itemDescriptions); } }
        
        /// <summary>
        /// Get the items.
        /// </summary>
        public Collection<string> Items { get { return (items); } }

        /// <summary>
        /// Get the descriptor number.
        /// </summary>
        public int DescriptorNumber { get { return (descriptorNumber); } }

        /// <summary>
        /// Get the last descriptor number.
        /// </summary>
        public int LastDescriptorNumber { get { return (lastDescriptorNumber); } }
        
        /// <summary>
        /// Get the non itemised text.
        /// </summary>
        public byte[] Text { get { return (text); } }

        /// <summary>
        /// Get the language code.
        /// </summary>
        public string LanguageCode { get { return (languageCode); } }

        /// <summary>
        /// Get the list of cast members.
        /// </summary>
        public Collection<string> Cast { get { return getDescriptions(new string[] { "ACTORS", "ACTOR", "INT", "AKTOR" }); } }

        /// <summary>
        /// Get the list of producers.
        /// </summary>
        public Collection<string> Producers { get { return getDescriptions(new string[] { "PRODUCERS", "PRODUCER" }); } }

        /// <summary>
        /// Get the list of directorrs.
        /// </summary>
        public Collection<string> Directors { get { return getDescriptions(new string[] { "DIRECTORS", "DIRECTOR", "DIR", "REŻYSER", "REZYSER" }); } }

        /// <summary>
        /// Get the list of writers.
        /// </summary>
        public Collection<string> Writers { get { return getDescriptions(new string[] { "WRITERS", "WRITER", "GUI" }); } }

        /// <summary>
        /// Get the year of production.
        /// </summary>
        public string Year
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (null);

                string yearString;

                if (itemDescriptions.Contains("YEAR"))
                    yearString = "YEAR";
                else
                {
                    if (itemDescriptions.Contains("PRODUCTION YEAR"))
                        yearString = "PRODUCTION YEAR";
                    else
                    {
                        if (itemDescriptions.Contains("AÑO"))
                            yearString = "AÑO";
                        else
                        {
                            if (itemDescriptions.Contains("ROK PRODUKCJI"))
                                yearString = "ROK PRODUKCJI";
                            else
                            {
                                if (itemDescriptions.Contains("DATE"))
                                    yearString = "DATE";
                                else
                                    return (null);
                            }
                        }
                    }
                }

                int itemIndex = itemDescriptions.IndexOf(yearString);
                if (itemIndex >= items.Count)
                    return(null);

                string item = items[itemIndex].Trim();
                if (item == string.Empty)
                    return (null);

                return (item);
            }
        }

        /// <summary>
        /// Get the star rating.
        /// </summary>
        public string StarRating
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (null);

                int itemIndex = itemDescriptions.IndexOf("STAR");
                if (itemIndex == -1 || itemIndex >= items.Count)
                    return(null);

                string item = items[itemIndex].Trim();
                if (item == string.Empty)
                    return (null);

                return (item);
            }
        }

        /// <summary>
        /// Get the series ID.
        /// </summary>
        public string SeriesID
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (null);

                int itemIndex = itemDescriptions.IndexOf("SERIESID");
                if (itemIndex == -1 || itemIndex >= items.Count)
                    return(null);

                string item = items[itemIndex].Trim();
                if (item == string.Empty)
                    return (null);

                return (item);
            }
        }

        /// <summary>
        /// Get the season ID.
        /// </summary>
        public string SeasonID
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (null);

                int itemIndex = itemDescriptions.IndexOf("SEASONID");
                if (itemIndex == -1 || itemIndex >= items.Count)
                    return (null);

                string item = items[itemIndex].Trim();
                if (item == string.Empty)
                    return (null);

                return (item);
            }
        }

        /// <summary>
        /// Get the episode ID.
        /// </summary>
        public string EpisodeID
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (null);

                int itemIndex = itemDescriptions.IndexOf("EPISODEID");
                if (itemIndex == -1 || itemIndex >= items.Count)
                    return (null);

                string item = items[itemIndex].Trim();
                if (item == string.Empty)
                    return (null);

                return (item);
            }
        }

        /// <summary>
        /// Get the season number.
        /// </summary>
        public int SeasonNumber
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (-1);

                int itemIndex = itemDescriptions.IndexOf("TEP");
                if (itemIndex == -1 || itemIndex >= items.Count)
                    return  getSeasonNumber("EPISODE-NUM");

                string item = items[itemIndex].Trim();
                if (item == string.Empty)
                    return (-1);

                string[] itemParts = item.Split(new char[] { ':' });
                if (itemParts.Length != 2)
                    return (-1);

                try
                {
                    return (Int32.Parse(itemParts[0]));
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
        }

        /// <summary>
        /// Get the episode number.
        /// </summary>
        public int EpisodeNumber
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (-1);

                int reply = getEpisodeNumber("TEP", ':', null);
                if (reply != -1)
                    return reply;

                reply = getEpisodeNumber("EPISODENO", ' ', "Ep");
                if (reply != -1)
                    return reply;

                reply = getEpisodeNumber("EPISODENO", '|', null);
                if (reply != -1)
                    return reply;

                return getEpisodeNumber1("EPISODE-NUM");
            }
        }

        /// <summary>
        /// Get the episode count.
        /// </summary>
        public int EpisodeCount
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (-1);

                return getEpisodeCount("EPISODE-NUM");
            }
        }

        /// <summary>
        /// Get the TV rating.
        /// </summary>
        public string TVRating
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (null);

                int itemIndex = itemDescriptions.IndexOf("TV RATINGS");
                if (itemIndex == -1 || itemIndex >= items.Count)
                    return (null);

                string item = items[itemIndex].Trim();
                if (item == string.Empty)
                    return (null);

                return (item);
            }
        }

        /// <summary>
        /// Get the offset back to the previous play date.
        /// </summary>
        public string PreviousPlayDate
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (null);

                int itemIndex = itemDescriptions.IndexOf("PPD");
                if (itemIndex == -1 || itemIndex >= items.Count)
                    return (null);

                string item = items[itemIndex].Trim();
                if (item == string.Empty)
                    return (null);

                return (item);
            }
        }

        /// <summary>
        /// Get the country of origin.
        /// </summary>
        public string Country
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (null);

                string countryString;

                if (itemDescriptions.Contains("COUNTRY"))
                    countryString = "COUNTRY";
                else
                {
                    if (itemDescriptions.Contains("NAC"))
                        countryString = "NAC";
                    else
                    {
                        if (itemDescriptions.Contains("KRAJ PRODUKCJI"))
                            countryString = "KRAJ PRODUKCJI";
                        else
                            return (null);
                    }
                }

                int itemIndex = itemDescriptions.IndexOf(countryString);
                if (itemIndex == -1 || itemIndex >= items.Count)
                    return (null);

                string item = items[itemIndex].Trim();
                if (item == string.Empty)
                    return (null);

                return (item);
            }
        }

        /// <summary>
        /// Get the episode.
        /// </summary>
        public string Episode
        {
            get
            {
                if (itemDescriptions == null || items == null)
                    return (null);

                string episodeString;

                if (itemDescriptions.Contains("EPISODE"))
                    episodeString = "EPISODE";
                else
                {
                    if (itemDescriptions.Contains("EPISODETITLE"))
                        episodeString = "EPISODETITLE";
                    else
                        return (null);
                }

                int itemIndex = itemDescriptions.IndexOf(episodeString);
                if (itemIndex == -1 || itemIndex >= items.Count)
                    return (null);

                string item = items[itemIndex].Trim();
                if (item == string.Empty)
                    return (null);

                return (item);
            }
        }

        /// <summary>
        /// Get the index of the next byte in the EIT section following this descriptor.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The descriptor has not been processed.
        /// </exception>
        public override int Index
        {
            get
            {
                if (lastIndex == -1)
                    throw (new InvalidOperationException("ExtendedEventDescriptor: Index requested before block processed"));
                return (lastIndex);
            }
        }

        private int descriptorNumber;
        private int lastDescriptorNumber;
        private string languageCode;
        private Collection<string> itemDescriptions;
        private Collection<string> items;
        private byte[] text;

        private byte[] textCodePage;

        private int lastIndex = -1;

        /// <summary>
        /// Initialize a new instance of the DVBExtendedEventDescriptor class.
        /// </summary>
        internal DVBExtendedEventDescriptor() { }

        /// <summary>
        /// Parse the descriptor.
        /// </summary>
        /// <param name="byteData">The MPEG2 section containing the descriptor.</param>
        /// <param name="index">Index of the byte in the MPEG2 section following the descriptor length.</param>
        internal override void Process(byte[] byteData, int index)
        {
            lastIndex = index;

            try
            {
                descriptorNumber = (int)byteData[lastIndex] >> 4;
                lastDescriptorNumber = (int)byteData[lastIndex] & 0x0f;
                lastIndex++;

                languageCode = Utils.GetAsciiString(byteData, lastIndex, 3);
                lastIndex += 3;

                int totalItemLength = (int)byteData[lastIndex];
                lastIndex++;

                if (totalItemLength != 0)
                {
                    itemDescriptions = new Collection<string>();
                    items = new Collection<string>();

                    while (totalItemLength != 0)
                    {
                        int itemDescriptionLength = (int)byteData[lastIndex];
                        lastIndex++;

                        if (itemDescriptionLength != 0)
                        {
                            string itemDescription = Utils.GetString(byteData, lastIndex, itemDescriptionLength, true);
                            itemDescriptions.Add(itemDescription.ToUpper());
                            lastIndex += itemDescriptionLength;
                        }

                        int itemLength = (int)byteData[lastIndex];
                        lastIndex++;

                        if (itemLength != 0)
                        {
                            string item = Utils.GetString(byteData, lastIndex, itemLength, true);
                            items.Add(item);
                            lastIndex += itemLength;
                        }
                        else
                            items.Add("");

                        totalItemLength -= (itemDescriptionLength + itemLength + 2);
                    }
                }

                int textLength = (int)byteData[lastIndex];
                lastIndex++;

                if (textLength != 0)
                {
                    text = Utils.GetBytes(byteData, lastIndex, textLength);

                    int byteLength = textLength > 2 ? 3 : 1;
                    textCodePage = Utils.GetBytes(byteData, lastIndex, byteLength);

                    lastIndex += textLength;
                }

                Validate();
            }
            catch (IndexOutOfRangeException)
            {
                throw (new ArgumentOutOfRangeException("The DVB Extended Event Descriptor message is short"));
            }
        }

        private int getEpisodeNumber(string identity, char separator, string firstPart)
        {
            int itemIndex = itemDescriptions.IndexOf(identity);
            if (itemIndex == -1 || itemIndex >= items.Count)
                return (-1);

            string item = items[itemIndex].Trim();
            if (item == string.Empty)
                return (-1);

            if (separator != '|')
            {
                string[] itemParts = item.Split(new char[] { separator });
                if (itemParts.Length != 2)
                    return (-1);

                if (firstPart != null && itemParts[0] != firstPart)
                    return -1;

                int reply = 0;
                int index = 0;

                while (index < itemParts[1].Length && char.IsDigit(itemParts[1][index]))
                {
                    reply = (reply * 10) + (itemParts[1][index] - '0');
                    index++;
                }

                return reply;
            }
            else
            {
                int reply = 0;
                int index = 0;

                while (index < item.Length && char.IsDigit(item[index]))
                {
                    reply = (reply * 10) + (item[index] - '0');
                    index++;
                }

                return reply;
            }
        }

        private int getEpisodeNumber1(string identity)
        {
            int itemIndex = itemDescriptions.IndexOf(identity);
            if (itemIndex == -1 || itemIndex >= items.Count)
                return -1;

            string item = items[itemIndex].Trim();
            if (item == string.Empty)
                return -1;

            string[] itemParts = item.Split(new char[] { ' ' });
            string[] episodeParts = itemParts[0].Split(new char[] { '/' });

            try
            {
                return Int32.Parse(episodeParts[0].Trim());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private int getEpisodeCount(string identity)
        {
            int itemIndex = itemDescriptions.IndexOf(identity);
            if (itemIndex == -1 || itemIndex >= items.Count)
                return -1;

            string item = items[itemIndex].Trim();
            if (item == string.Empty)
                return -1;

            string[] itemParts = item.Split(new char[] { ' ' });
            string[] episodeParts = itemParts[0].Split(new char[] { '/' });

            if (episodeParts.Length == 2)
            {
                try
                {
                    return Int32.Parse(episodeParts[1].Trim());
                }
                catch (Exception)
                {
                    return -1;
                }
            }

            return -1;
        }

        private int getSeasonNumber(string identity)
        {
            int itemIndex = itemDescriptions.IndexOf(identity);
            if (itemIndex == -1 || itemIndex >= items.Count)
                return (-1);

            string item = items[itemIndex].Trim();
            if (item == string.Empty)
                return (-1);

            string[] itemParts = item.Split(new char[] { ' ' });

            if (itemParts.Length == 1)
                return -1;
            
            try
            {
                return Int32.Parse(itemParts[1].Trim());
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private Collection<string> getDescriptions(string[] identifiers)
        {
            if (itemDescriptions == null || items == null)
                return (null);

            Collection<string> descriptionIds = new Collection<string>(identifiers);
            Collection<string> results = new Collection<string>();
            int itemIndex = 0;

            foreach (string itemDescription in itemDescriptions)
            {
                if (descriptionIds.Contains(itemDescription.ToUpperInvariant()))
                {
                    if (itemIndex < items.Count)
                    {
                        string[] members = items[itemIndex].Split(new char[] { ',' });
                        foreach (string member in members)
                        {
                            string memberTrimmed = member.Trim();
                            if (!results.Contains(memberTrimmed))
                                results.Add(memberTrimmed);
                        }
                    }
                }

                itemIndex++;
            }

            return results.Count != 0 ? results : null; 
        }

        /// <summary>
        /// Validate the descriptor fields.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// A descriptor field is not valid.
        /// </exception>
        internal override void Validate() { }

        /// <summary>
        /// Log the descriptor fields.
        /// </summary>
        internal override void LogMessage() 
        {
            if (Logger.ProtocolLogger == null)
                return;

            string text;
            if (Text != null)
                text = Utils.GetString(Text, 0, Text.Length, true);
            else
                text = "n/a";

            Logger.ProtocolLogger.Write(Logger.ProtocolIndent + "DVB EXTENDED EVENT DESCRIPTOR: Descr no.: " + descriptorNumber +
                " Last descr no.: " + lastDescriptorNumber +
                " Lang code: " + languageCode +
                " Text: " + text +
                " Text CP: " + (textCodePage != null ? Utils.ConvertToHex(textCodePage) : " n/a"));

            if (itemDescriptions != null)
            {
                foreach (string itemDescription in itemDescriptions)
                {
                    Logger.ProtocolLogger.Write(Logger.ProtocolIndent + "    Item description: " + itemDescription +
                        " Value: " + items[itemDescriptions.IndexOf(itemDescription)]);
                }
            }
        }
    }
}

