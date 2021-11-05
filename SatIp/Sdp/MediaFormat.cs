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

using SatIpDomainObjects;

namespace Sdp
{
    /// <summary>
    /// The class that describes a media format.
    /// </summary>
    public class MediaFormat : ICreateFormat, ISdpFormat
    {
        /// <summary>
        /// Get or set the identity.
        /// </summary>
        public string Identity { get; set; }
        /// <summary>
        /// Get the media parameters.
        /// </summary>
        public string Parameters { get; private set; }

        /// <summary>
        /// Get the list of format processors.
        /// </summary>
        public static Collection<ISdpFormat> Formats;
        
        private static Collection<ICreateFormat> formatCreators;

        /// <summary>
        /// Initialize a new instance of the MediaFormat class.
        /// </summary>
        public MediaFormat() { }

        /// <summary>
        /// Parse the media format.
        /// </summary>
        /// <param name="parameters">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public virtual ErrorSpec Process(string parameters)
        {
            int index = parameters.IndexOf(' ');
            Parameters = parameters.Substring(index + 1);

            return (null);
        }

        /// <summary>
        /// Check if a processor accepts a format.
        /// </summary>
        /// <param name="identity">The format to be checked.</param>
        /// <returns>True if the processor accepts the format; false otherwise.</returns>
        public virtual bool IsFormat(string identity)
        {
            return (identity == Identity);
        }

        /// <summary>
        /// Create a format processor.
        /// </summary>
        /// <param name="identity">The name of the processor.</param>
        /// <returns>A new instance of the format processor.</returns>
        public virtual ISdpFormat CreateFormat(string identity)
        {
            return (new MediaFormat());
        }

        /// <summary>
        /// Register a format.
        /// </summary>
        /// <param name="format">An instance of a format creator.</param>
        public static void RegisterFormat(ICreateFormat format)
        {
            if (formatCreators == null)
                formatCreators = new Collection<ICreateFormat>();

            formatCreators.Add(format);
        }

        /// <summary>
        /// Get an instance of a format processor.
        /// </summary>
        /// <param name="identity">The identity of the processor.</param>
        /// <returns>An instance of the processor.</returns>
        public static ISdpFormat GetInstance(string identity)
        {
            ICreateFormat selectedFormat = null;

            if (formatCreators == null)
                selectedFormat = new MediaFormat();
            else
            {
                foreach (ICreateFormat formatCreator in formatCreators)
                {
                    if (formatCreator.IsFormat(identity))
                    {
                        selectedFormat = formatCreator;
                        break;
                    }

                    selectedFormat = new MediaFormat();
                }
            }

            ISdpFormat format = selectedFormat.CreateFormat(identity);
            format.Identity = identity;

            return (format);
        }
    }
}

