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
using System.Xml;
using System.IO;

using DomainObjects;

namespace SchedulesDirect
{
    /// <summary>
    /// The class that describes the Schedules Direct configuration.
    /// </summary>
    public class SchedulesDirectConfig
    {
        /// <summary>
        /// Get the single instance of the SchedulesDirectConfig class.
        /// </summary>
        public static SchedulesDirectConfig Instance
        {
            get
            {
                if (instance == null)
                    instance = new SchedulesDirectConfig();                    
                return instance;
            }
        }

        /// <summary>
        /// Returns true if the instance has been successfully loaded; false otherwise.
        /// </summary>
        public bool IsLoaded { get; private set; }
        
        /// <summary>
        /// Get or set the user name.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Get or set the password.
        /// </summary>
        public string Password { get; set; }        
        
        private static SchedulesDirectConfig instance;
        
        private Collection<SchedulesDirectMd5Entry> schedulesMd5List = new Collection<SchedulesDirectMd5Entry>();
        private Collection<SchedulesDirectMd5Entry> programmesMd5List = new Collection<SchedulesDirectMd5Entry>();

        private string fileName = "Schedules Direct.cfg";

        private SchedulesDirectConfig() { }

        /// <summary>
        /// Load the instance from a file.
        /// </summary>
        /// <returns>Null if the load is successful; asn error message otherwise.</returns>
        public string Load()
        {
            if (IsLoaded)
                return null;

            XmlReader reader = null;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            try
            {
                reader = XmlReader.Create(Path.Combine(RunParameters.DataDirectory, fileName), settings);
            }
            catch (IOException e)
            {
                string error = e.Message;
                Logger.Instance.Write(error);
                return error;
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
                            case "account":
                                UserName = reader.GetAttribute("userName");
                                Password = reader.GetAttribute("password");
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (XmlException e)
            {
                string error = "Failed to load Schedules Direct configuration file: " + e.Message;
                Logger.Instance.Write(error);
                return (error);
            }
            catch (IOException e)
            {
                string error = "Failed to load Schedules Direct configuration file: " + e.Message;
                Logger.Instance.Write(error);
                return (error);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            IsLoaded = true;

            return (null);
        }

        /// <summary>
        /// Unload the instance to a file.
        /// </summary>
        public void Unload()
        {
            string fullPath = Path.Combine(RunParameters.DataDirectory, fileName);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = true;
            settings.Indent = true;
            settings.NewLineOnAttributes = false;

            XmlWriter writer = null;

            try
            {
                writer = XmlWriter.Create(new FileStream(fullPath, FileMode.Create), settings);

                writer.WriteStartElement("Configuration");
                writer.WriteAttributeString("generator-info-name", "SchedulesDirect " + SchedulesDirectController.AssemblyVersion);

                writer.WriteStartElement("account");
                writer.WriteAttributeString("userName", UserName);
                writer.WriteAttributeString("password", Password);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.Close();
            }
            catch (XmlException e)
            {
                Logger.Instance.Write("Failed to unload " + fullPath);
                Logger.Instance.Write("Data exception: " + e.Message);
            }
            catch (IOException e)
            {
                Logger.Instance.Write("Failed to unload " + fullPath);
                Logger.Instance.Write("I/O exception: " + e.Message);
            }
        }
    }
}

