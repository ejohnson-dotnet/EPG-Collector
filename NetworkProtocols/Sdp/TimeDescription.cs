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

namespace NetworkProtocols.Sdp
{
    /// <summary>
    /// The class that describes a time description.
    /// </summary>
    public class TimeDescription
    {
        /// <summary>
        /// Get the start time.
        /// </summary>
        public DateTime? StartTime { get; private set; }
        /// <summary>
        /// Get the end time.
        /// </summary>
        public DateTime? EndTime { get; private set; }

        /// <summary>
        /// Get the list of repeat descriptions.
        /// </summary>
        public Collection<RepeatDescription> Repeats;

        /// <summary>
        /// Initialize a new instance of the TimeDescription class.
        /// </summary>
        public TimeDescription() { }

        /// <summary>
        /// Parse the time description data.
        /// </summary>
        /// <param name="part">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string part)
        {
            string[] parts = part.Split(new char[] { ' ' });

            if (parts.Length != 2)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 't' attribute is in the wrong format", part));                

            try
            {
                int startTime = Int32.Parse(parts[0].Trim());
                int endTime = Int32.Parse(parts[1].Trim());

                if (startTime != 0)
                    StartTime = new DateTime(1900, 1, 1).AddSeconds(startTime);
                if (endTime != 0)
                    EndTime = new DateTime(1900, 1, 1).AddSeconds(endTime);

                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 't' attribute time field is in the wrong format", part));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 't' attribute time field is out of range", part));                
            }
        }
    }
}

