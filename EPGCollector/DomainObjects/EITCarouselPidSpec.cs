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
using System.Xml;

namespace DomainObjects
{
    /// <summary>
    /// The class that defines the details for an EIT carousel pid.
    /// </summary>
    public class EITCarouselPidSpec
    {
        /// <summary>
        /// Get the pid number.
        /// </summary>
        public int Pid { get; private set; }
        /// <summary>
        /// Get the list of carousel directories.
        /// </summary>
        public Collection<string> CarouselDirectories { get; private set; }
        /// <summary>
        /// Get the list of zip directories.
        /// </summary>
        public Collection<string> ZipDirectories { get; private set; }

        /// <summary>
        /// Initialize a new instance of the EITCarouselPidSpec class. 
        /// </summary>
        public EITCarouselPidSpec() { }

        internal void Load(int pid, XmlReader reader)
        {
            Pid = pid;

            while (!reader.EOF)
            {
                reader.Read();
                if (reader.IsStartElement())
                {
                    switch (reader.Name.ToLowerInvariant())
                    {
                        case "carouseldirectory":
                            if (CarouselDirectories == null)
                                CarouselDirectories = new Collection<string>();
                            CarouselDirectories.Add(reader.ReadString());
                            break;
                        case "zipdirectory":
                            if (ZipDirectories == null)
                                ZipDirectories = new Collection<string>();
                            ZipDirectories.Add(reader.ReadString());
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

