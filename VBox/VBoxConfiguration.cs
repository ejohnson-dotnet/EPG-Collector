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

namespace VBox
{
    /// <summary>
    /// The class that describes the VBox configuration.
    /// </summary>
    public class VBoxConfiguration : NetworkConfiguration
    {
        /// <summary>
        /// Get the state of the VBox feature.
        /// </summary>
        public static bool VBoxEnabled 
        {
            get
            {
                if (File.Exists(VBoxEnabledFile))
                {
                    if (NetworkConfiguration.VBoxConfiguration == null)
                    {
                        NetworkConfiguration.VBoxConfiguration = new VBoxConfiguration();
                        NetworkConfiguration.VBoxConfiguration.Load();
                    }
                    return (true);
                }
                else
                {
                    NetworkConfiguration.VBoxConfiguration = null;
                    return (false);
                }
            } 
        }

        /// <summary>
        /// Get the name of the configuration file.
        /// </summary>
        public static string VBoxEnabledFile { get { return (Path.Combine(RunParameters.DataDirectory, "VBox Enabled")); } }        
                
        /// <summary>
        /// Initialize a new instance of the VBoxConfiguration class.
        /// </summary>
        public VBoxConfiguration() { }

        /// <summary>
        /// Load the configuration.
        /// </summary>
        /// <returns></returns>
        public override bool Load()
        {
            if (VBoxEnabled)
            {
                FileStream fileStream = null;

                try { fileStream = new FileStream(VBoxEnabledFile, FileMode.Open, FileAccess.Read); }
                catch (IOException)
                {
                    Logger.Instance.Write("Failed to open " + VBoxEnabledFile);
                    return (false);
                }

                StreamReader streamReader = new StreamReader(fileStream);

                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    if (line.StartsWith(localAddressLine))
                        LocalAddress = line.Substring(localAddressLine.Length).Trim();
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

            try { fileStream = new FileStream(VBoxEnabledFile, FileMode.Create, FileAccess.Write); }
            catch (IOException)
            {
                Logger.Instance.Write("Failed to create " + VBoxEnabledFile);
                return (false);
            }

            StreamWriter writer = new StreamWriter(fileStream);

            if (LocalAddress != null)
                writer.WriteLine(localAddressLine + LocalAddress);

            writer.Close();
            fileStream.Close();

            return (true);
        }
    }
}

