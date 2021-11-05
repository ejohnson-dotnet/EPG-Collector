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
using System.IO;

using DomainObjects;
using NetworkProtocols;

namespace SatIp
{
    /// <summary>
    /// The class that describes the Sat>IP configuration.
    /// </summary>
    public class SatIpConfiguration : NetworkConfiguration
    {
        /// <summary>
        /// Get the state of the Sat>IP feature.
        /// </summary>
        public static bool SatIpEnabled 
        { 
            get 
            {
                if (File.Exists(SatIpEnabledFile))
                {
                    if (NetworkConfiguration.SatIpConfiguration == null)
                    {
                        NetworkConfiguration.SatIpConfiguration = new SatIpConfiguration();
                        NetworkConfiguration.SatIpConfiguration.Load();
                    }
                    return (true);
                }
                else
                {
                    NetworkConfiguration.SatIpConfiguration = null;
                    return (false);
                }
            } 
        }

        /// <summary>
        /// Get the name of the configuration file.
        /// </summary>
        public static string SatIpEnabledFile { get { return (Path.Combine(RunParameters.DataDirectory, "SatIP Enabled")); } }

        /// <summary>
        /// Get the frontend value.
        /// </summary>
        public int Frontend { get; set; }
        /// <summary>
        /// Get the announcement flag.
        /// </summary>
        public bool NoAnnouncements { get; set; }
        /// <summary>
        /// Get the RTSP port number
        /// </summary>
        public int RtspPort { get; set; }
        /// <summary>
        /// Get or set the flag that inhibits sending the port number in the RTSP Setup message.
        /// </summary>
        public bool NoSendRtspPort { get; set; }

        private string frontendLine = "Frontend=";
        private string noAnnouncementsLine = "NoAnnouncements=";
        private string rtspPortLine = "RtspPort=";
        private string noSendRtspPortLine = "NoSendRtspPort=";
        
        /// <summary>
        /// Initialize a new instance of the SatIpConfiguration class.
        /// </summary>
        public SatIpConfiguration() 
        {
            RtspPort = 554;
        }

        /// <summary>
        /// Load the configuration.
        /// </summary>
        /// <returns></returns>
        public override bool Load()
        {
            if (SatIpEnabled)
            {
                FileStream fileStream = null;

                try { fileStream = new FileStream(SatIpEnabledFile, FileMode.Open, FileAccess.Read); }
                catch (IOException)
                {
                    Logger.Instance.Write("Failed to open " + SatIpEnabledFile);
                    return (false);
                }

                StreamReader streamReader = new StreamReader(fileStream);
                
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    if (line.StartsWith(localAddressLine))
                        LocalAddress = line.Substring(localAddressLine.Length).Trim();
                    else
                    {
                        if (line.StartsWith(frontendLine))
                        {
                            string frontEnd = line.Substring(frontendLine.Length);

                            try
                            {
                                Frontend = Int32.Parse(frontEnd);
                            }
                            catch (FormatException) { }
                            catch (OverflowException) { }
                        }
                        else
                        {
                            if (line.StartsWith(noAnnouncementsLine))
                            {
                                string noAnnouncements = line.Substring(noAnnouncementsLine.Length);
                                NoAnnouncements = noAnnouncements.ToLowerInvariant().Trim() == "yes";
                            }
                            else
                            {
                                if (line.StartsWith(rtspPortLine))
                                {
                                    string rtspPort = line.Substring(rtspPortLine.Length);

                                    try
                                    {
                                        RtspPort = Int32.Parse(rtspPort);
                                    }
                                    catch (FormatException) { }
                                    catch (OverflowException) { }
                                }
                                else
                                {
                                    if (line.StartsWith(noSendRtspPortLine))
                                    {
                                        string noSendRtspPort = line.Substring(noSendRtspPortLine.Length);
                                        NoSendRtspPort = noSendRtspPort.ToLowerInvariant().Trim() == "yes";
                                    }
                                }
                            }
                        }
                    }
                }

                streamReader.Close();
                fileStream.Close();
            }

            return (true);
        }

        /// <summary>
        /// Unload the configuration.
        /// </summary>
        /// <returns></returns>
        public override bool Unload()
        {
            FileStream fileStream = null;

            try { fileStream = new FileStream(SatIpEnabledFile, FileMode.Create, FileAccess.Write); }
            catch (IOException)
            {
                Logger.Instance.Write("Failed to create " + SatIpEnabledFile);
                return (false);
            }

            StreamWriter writer = new StreamWriter(fileStream);

            if (LocalAddress != null)
                writer.WriteLine(localAddressLine + LocalAddress);
            if (Frontend != 0)
                writer.WriteLine(frontendLine + Frontend.ToString());
            if (NoAnnouncements)
                writer.WriteLine(noAnnouncementsLine + "yes");
            if (RtspPort != 554)
                writer.WriteLine(rtspPortLine + RtspPort);
            if (NoSendRtspPort)
                writer.WriteLine(noSendRtspPortLine + "yes");

            writer.Close();
            fileStream.Close();

            return (true);
        }
    }
}

