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
using System.Xml;
using System.Collections.ObjectModel;

namespace VBox
{
    /// <summary>
    /// The class that describes the response to a QuerySwVersion request.
    /// </summary>
    public class VBoxQuerySwVersionResponse : VBoxResponse
    {
        /// <summary>
        /// Get the device type.
        /// </summary>
        public string DeviceType { get; private set; }

        /// <summary>
        /// Get the version prefix.
        /// </summary>
        public string VersionPrefix { get; private set; }

        /// <summary>
        /// Get the software version.
        /// </summary>
        public string SoftwareVersion { get; private set; }

        /// <summary>
        /// Get the Uboot version.
        /// </summary>
        public string UbootVersion { get; private set; }

        internal VBoxQuerySwVersionResponse() { }

        internal override void Process(XmlReader reader)
        {
            while (!reader.EOF)
            {
                reader.Read();
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Status":
                            base.Process(reader.ReadSubtree());
                            break;
                        case "DeviceType":
                            DeviceType = reader.ReadString();
                            break;
                        case "VersionPrefix":
                            VersionPrefix = reader.ReadString();
                            break;
                        case "SoftwareVersion":
                            SoftwareVersion = reader.ReadString();
                            break;
                        case "UbootVersion":
                            UbootVersion = reader.ReadString();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Compare version number with this instance.
        /// </summary>
        /// <param name="otherVersion">The other version number.</param>
        /// <returns>0 if equal, -1 if less than, otherwise +1.</returns>
        public int CompareSoftwareVersions(string otherVersion)
        {
            if (string.IsNullOrWhiteSpace(SoftwareVersion))
                return -1;
            if (string.IsNullOrWhiteSpace(otherVersion))
                return 1;

            if (SoftwareVersion.Trim() == otherVersion.Trim())
                return 0;

            Collection<string> versionParts = new Collection<string>();
            foreach (string vString in SoftwareVersion.Split(new char[] { '.' }))
                versionParts.Add(vString);
                
            Collection<string> otherVersionParts = new Collection<string>();
            foreach (string oString in otherVersion.Split(new char[] { '.' }))
                otherVersionParts.Add(oString);

            if (versionParts.Count > otherVersionParts.Count)
            {
                while (versionParts.Count > otherVersionParts.Count)
                    otherVersionParts.Add("0");
            }
            else
            {
                if (versionParts.Count < otherVersionParts.Count)
                {
                    while (versionParts.Count < otherVersionParts.Count)
                        versionParts.Add("0");
                }
            }

            for (int index = 0; index < versionParts.Count; index++)
            {
                int reply = compareVersionParts(versionParts[index], otherVersionParts[index]);
                if (reply != 0)
                    return reply;
            }

            return 0;
        }

        private int compareVersionParts(string versionPart, string otherVersionPart)
        {
            try
            {
                int versionPartNumber = Int32.Parse(versionPart);
                int otherVersionPartNumber = Int32.Parse(otherVersionPart);
                return versionPartNumber.CompareTo(otherVersionPartNumber);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Get a text representation of the response.
        /// </summary>
        /// <returns>The text description.</returns>
        public override string ToString()
        {
            return (base.ToString() + " Device type=" + DeviceType + " Version prefix=" + VersionPrefix + " Software version=" + SoftwareVersion + " Uboot version=" + UbootVersion);
        }
    }
}

