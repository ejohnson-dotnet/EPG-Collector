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
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Globalization;

using Newtonsoft.Json;

namespace DomainObjects
{
    /// <summary>
    /// The class that handles foreign language translations.
    /// </summary>
    public class TextTranslation
    {
        private static int errorCount;
        private static int maxErrorCount = 10;

        private static int translationRequestCount;
        private static int translationCharacterCount;
        private static int maxCharacterCount = 300000;
        private static TimeSpan translationTime;
        private static int locallyTranslatedCount;
        private static int localTranslationsCount;
        private static int webTranslations;

        private static string baseUrl = "https://translate.yandex.net/api/v1.5/tr.json";
        private static string translateEndPoint = "/translate";
        private static string languagesEndPoint = "/getLangs";

        private static Logger logger;
        private static string logHeader = "Translation Services";

        private static string localTranslationsFileName = "EPG Collector Translations.xml";

        private static Collection<TextTranslation> localTranslations;
        private static Collection<TextTranslationLanguage> languages;
        private static Collection<string> languagesDetected;

        private string originalText;
        private string language;
        private string translation;
        private DateTime dateLastUsed;

        private bool usedThisTime;

        private TextTranslation() { }

        /// <summary>
        /// Create an instance of the TextTranslation class.
        /// </summary>
        /// <param name="originalText">The original text to be translated.</param>
        /// <param name="language">The language code to translate to.</param>
        /// <param name="translation">The translated text.</param>
        /// <param name="dateLastUsed">The date the original text was last translated.</param>
        public TextTranslation(string originalText, string language, string translation, DateTime dateLastUsed)
        {
            this.originalText = originalText;
            this.language = language;
            this.translation = translation;
            this.dateLastUsed = dateLastUsed;
        }

        /// <summary>
        /// Get a list of supported languages.
        /// </summary>
        /// <returns>The list of supported languages.</returns>
        public static Collection<TextTranslationLanguage> GetLanguages()
        {
            return GetLanguages(RunParameters.Instance.TranslationApiKey);
        }

        /// <summary>
        /// Get a list of supported languages.
        /// </summary>
        /// <param name="apiKey">The API key to use.</param>
        /// <returns>A list of supported languages.</returns>
        public static Collection<TextTranslationLanguage> GetLanguages(string apiKey)
        {
            if (languages != null)
                return languages;

            logger = new Logger(Logger.StreamFilePath);

            WebRequestSpec requestSpec = new WebRequestSpec(baseUrl + languagesEndPoint + "?key=" + apiKey + "&ui=en", logger, logHeader);
            //requestSpec.HttpWebRequest.Method = "POST";
            requestSpec.ContentType = "application/x-www-form-urlencoded";
            ReplyBase reply = requestSpec.Process();
            if (reply.Message != null)
                return null;

            languages = new Collection<TextTranslationLanguage>();

            string jsonString = reply.ResponseData as string;
            TextTranslationLanguage deserializedList = JsonConvert.DeserializeObject<TextTranslationLanguage>(jsonString);
            foreach (KeyValuePair<string, string> keyValue in deserializedList.Languages)
                languages.Add(new TextTranslationLanguage(keyValue.Key, keyValue.Value));
            
            return languages;
        }

        /// <summary>
        /// Get a description of a language code.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns>A description of the language code.</returns>
        public static string GetLanguageDecode(string languageCode)
        {
            if (languages == null)
            {
                languages = GetLanguages();
                if (languages == null)
                    return languageCode + " (decode not available)";
            }

            foreach (TextTranslationLanguage language in languages)
            {
                if (language.LanguageCode == languageCode)
                    return language.Name;
            }

            return languageCode + " (decode not found)";
        }

        /// <summary>
        /// Process the EPG data for text translation.
        /// </summary>
        /// <param name="stations">The collection of channels.</param>
        /// <param name="translationSpecs">The collection of translation specs.</param>
        public static void Process(Collection<TVStation> stations, Collection<TextTranslationSpec> translationSpecs)
        {
            GetLanguages();

            if (translationSpecs == null)
                return;

            logger = new Logger(Logger.StreamFilePath);

            Logger.Instance.WriteSeparator("Text Translation (powered by Yandex.Translation http://translate.yandex.com)");

            ReplyBase reply = loadLocalTranslations();
            if (reply.Message != null)
                Logger.Instance.Write("Failed to load local translation table - " + reply.Message);

            foreach (TextTranslationSpec translationSpec in translationSpecs)
            {
                TVStation station = findStation(stations, translationSpec.OriginalNetworkID, translationSpec.TransportStreamID, translationSpec.ServiceID);
                if (station != null && station.Included)
                    processStation(station, RunParameters.Instance.TranslationLanguage);
            }

            reply = unloadTranslations();
            if (reply.Message != null)
                Logger.Instance.Write("Failed to unload local translation table - " + reply.Message);            

            logStats();

            Logger.Instance.WriteSeparator("End of Text Translation");
        }

        private static TVStation findStation(Collection<TVStation> stations, int originalNetworkID, int transportStreamID, int serviceID)
        {
            if (stations == null)
                return null;

            foreach (TVStation station in stations)
            {
                if (station.OriginalNetworkID == originalNetworkID && station.TransportStreamID == transportStreamID && station.ServiceID == serviceID)
                    return station;
            }

            return null;
        }

        private static void processStation(TVStation station, string outputLanguage)
        {
            if (station.EPGCollection == null)
                return;

            Logger.Instance.Write("Translating " + station.Name + " to " + GetLanguageDecode(outputLanguage));

            foreach (EPGEntry epgEntry in station.EPGCollection)
            {
                ReplyBase reply = getTranslatedText(null, outputLanguage, epgEntry.EventName);
                if (reply.Message == null)
                {
                    epgEntry.EventName = capitalize(reply.ResponseData as string);

                    if (!string.IsNullOrWhiteSpace(epgEntry.ShortDescription))
                    {
                        reply = getTranslatedText(null, outputLanguage, epgEntry.ShortDescription);
                        if (reply.Message == null)
                        {
                            epgEntry.ShortDescription = reply.ResponseData as string;

                            if (!string.IsNullOrWhiteSpace(epgEntry.EventSubTitle))
                            {
                                reply = getTranslatedText(null, outputLanguage, epgEntry.EventSubTitle);
                                if (reply.Message == null)
                                {
                                    epgEntry.EventSubTitle = capitalize(reply.ResponseData as string);
                                }
                            }
                        }
                    }
                }
                else
                    Logger.Instance.Write(reply.Message);
            }
        }

        private static ReplyBase getTranslatedText(string inputLanguage, string outputLanguage, string text)
        {
            translationRequestCount++;

            if (errorCount == maxErrorCount)
                return ReplyBase.ErrorReply("No translation - maximum error count reached - " + maxErrorCount);

            if (translationCharacterCount > maxCharacterCount)
                return ReplyBase.ErrorReply("No translation - maximum character count reached - " + maxCharacterCount);

            string translatedText = findTranslation(outputLanguage, text);
            if (translatedText != null)
            {
                locallyTranslatedCount++;
                return ReplyBase.DataReply(translatedText);
            }

            DateTime startTime = DateTime.Now;

            ReplyBase webReply = getWebTranslation(inputLanguage, outputLanguage, text);
            if (webReply.Message != null)
            {
                errorCount++;
                return webReply;
            }

            translationCharacterCount += text.Length;
            translationTime += DateTime.Now - startTime;

            TextTranslationReply translationReply = JsonConvert.DeserializeObject<TextTranslationReply>(webReply.ResponseData as string);
            if (translationReply.ResponseCode != 200)
            {
                errorCount++;
                return ReplyBase.ErrorReply("Translation failed - code " + translationReply.ResponseCode);
            }

            if (localTranslations == null)
                localTranslations = new Collection<TextTranslation>();
            localTranslations.Add(new TextTranslation(text, outputLanguage, translationReply.Translations[0], DateTime.Now));

            if (languagesDetected == null)
                languagesDetected = new Collection<string>();
            if (!languagesDetected.Contains(translationReply.Detected.Language))
                languagesDetected.Add(translationReply.Detected.Language);

            return ReplyBase.DataReply(translationReply.Translations[0]);
        }

        private static string capitalize(string uncasedString)
        {
            TextInfo textInfo = new CultureInfo(CultureInfo.CurrentCulture.LCID, false).TextInfo;
            return textInfo.ToTitleCase(uncasedString);
        }

        private static ReplyBase loadLocalTranslations()
        {
            XmlReader reader = null;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            string fullPath = Path.Combine(RunParameters.DataDirectory, localTranslationsFileName);

            try
            {
                reader = XmlReader.Create(fullPath, settings);
            }
            catch (IOException)
            {
                return ReplyBase.ErrorReply("Failed to open " + fullPath);
            }
            
            try
            {
                while (!reader.EOF)
                {
                    reader.Read();
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {  
                            case "Translation":
                                TextTranslation translation = new TextTranslation();
                                translation.language = reader.GetAttribute("language");
                                translation.dateLastUsed = DateTime.Parse(reader.GetAttribute("dateLastUsed"));
                                translation.load(reader.ReadSubtree());

                                if (localTranslations == null)
                                    localTranslations = new Collection<TextTranslation>();
                                localTranslations.Add(translation);

                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (XmlException e)
            {
                Logger.Instance.Write("Failed to load translation file " + fullPath);
                Logger.Instance.Write("Data exception: " + e.Message);
                return ReplyBase.ErrorReply("Failed to load translation file " + fullPath);
            }
            catch (IOException e)
            {
                Logger.Instance.Write("Failed to load translation file " + fullPath);
                Logger.Instance.Write("I/O exception: " + e.Message);
                return ReplyBase.ErrorReply("Failed to load translation file " + fullPath);
            }

            if (reader != null)
                reader.Close();

            return ReplyBase.NoReply();
        }

        private static string findTranslation(string language, string text)
        {
            if (localTranslations == null)
                return null;

            string lowercaseText = text.ToLowerInvariant();

            foreach (TextTranslation translation in localTranslations)
            {
                if (translation.language == language && translation.originalText.ToLowerInvariant() == lowercaseText)
                {
                    translation.usedThisTime = true;
                    return translation.translation;
                }
            }

            return null;
        }

        private static ReplyBase getWebTranslation(string inputLanguage, string outputLanguage, string text)
        {
            webTranslations++;
            string completeUrl = buildUrl(inputLanguage, outputLanguage, text);
            
            WebRequestSpec requestSpec = new WebRequestSpec(completeUrl, logger, logHeader);
            requestSpec.Method = "POST";
            requestSpec.ContentType = "application/x-www-form-urlencoded";

            byte[] textBytes = Encoding.UTF8.GetBytes("text=" + HttpUtility.UrlEncode(text));
            requestSpec.ContentLength = textBytes.Length;

            using (Stream stream = requestSpec.RequestStream)
            {
                stream.Write(textBytes, 0, textBytes.Length);
                return requestSpec.Process();                
            }
        }

        private static string buildUrl(string inputLanguage, string outputLanguage, string text)
        {
            string url = baseUrl + translateEndPoint + "?key=" + RunParameters.Instance.TranslationApiKey;

            if (!string.IsNullOrWhiteSpace(inputLanguage))
                url += "&lang=" + inputLanguage + "-" + outputLanguage;
            else
                url += "&lang=" + outputLanguage;

            url += "&options=1";

            return url;
        }

        private static ReplyBase unloadTranslations()
        {
            string fullPath = Path.Combine(RunParameters.DataDirectory, localTranslationsFileName);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = false;

            XmlWriter writer = null;

            try
            {
                writer = XmlWriter.Create(new FileStream(fullPath, FileMode.Create), settings);

                writer.WriteStartElement("Translations");
                writer.WriteAttributeString("generator-info-name", "DomainObjects " + RunParameters.AssemblyVersion);

                foreach (TextTranslation translation in localTranslations)
                {
                    if (translation.usedThisTime || translation.dateLastUsed > DateTime.Now.AddDays(-14))
                    {
                        writer.WriteStartElement("Translation");
                        
                        writer.WriteAttributeString("language", translation.language);
                        if (translation.usedThisTime)
                            writer.WriteAttributeString("dateLastUsed", DateTime.Now.ToString());
                        else
                            writer.WriteAttributeString("dateLastUsed", translation.dateLastUsed.ToString());
                        
                        writer.WriteElementString("OriginalText", translation.originalText);                        
                        writer.WriteElementString("TranslatedText", translation.translation);

                        writer.WriteEndElement();

                        localTranslationsCount++;
                    }
                }


                writer.WriteEndElement();
                writer.Close();
            }
            catch (XmlException e)
            {
                Logger.Instance.Write("Failed to unload " + fullPath);
                Logger.Instance.Write("Data exception: " + e.Message);
                return ReplyBase.ErrorReply("Failed to unload " + fullPath);
            }
            catch (IOException e)
            {
                Logger.Instance.Write("Failed to unload " + fullPath);
                Logger.Instance.Write("I/O exception: " + e.Message);
                return ReplyBase.ErrorReply("Failed to unload " + fullPath);
            }

            return ReplyBase.NoReply();

        }

        private static void logStats()
        {
            Logger.Instance.Write("Text translation statistics");

            Logger.Instance.Write("Total translation requests = " + translationRequestCount);            
            Logger.Instance.Write("Total web accesses = " + webTranslations);
            
            if (webTranslations != 0)
            {
                Logger.Instance.Write("Total web access time = " + translationTime);
                Logger.Instance.Write("Average web access time = " + new TimeSpan(translationTime.Ticks / webTranslations));
                Logger.Instance.Write("Total web character count = " + translationCharacterCount);
                Logger.Instance.Write("Total local translations = " + locallyTranslatedCount);

                if (languagesDetected != null)
                {
                    string detected = string.Empty;

                    foreach (string language in languagesDetected)
                    {
                        if (detected.Length != 0)
                            detected += ", ";
                        detected += GetLanguageDecode(language);
                    }

                    Logger.Instance.Write("Languages detected = " + detected);
                }
            }

            Logger.Instance.Write("Total translations stored locally = " + localTranslationsCount);

        }

        private void load(XmlReader reader)
        {
            while (!reader.EOF)
            {
                reader.Read();
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {  
                        case "OriginalText":
                            originalText = reader.ReadString();
                            break;
                        case "TranslatedText":                                
                            translation = reader.ReadString();
                            break;
                        default:
                            break;
                    }
                }
            }
            
            reader.Close();
        }
    }
}

