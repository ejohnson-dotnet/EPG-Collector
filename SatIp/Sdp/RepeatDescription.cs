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

using SatIpDomainObjects;

namespace Sdp
{
    /// <summary>
    /// The class that describes a repeat description.
    /// </summary>
    public class RepeatDescription
    {
        /// <summary>
        /// Get the interval.
        /// </summary>
        public TimeSpan Interval { get; private set; }
        /// <summary>
        /// Get the active duration.
        /// </summary>
        public TimeSpan ActiveDuration { get; private set; }
        /// <summary>
        /// Get the list of offsets.
        /// </summary>
        public Collection<TimeSpan> Offsets { get; private set; }

        /// <summary>
        /// Initialize a new instance of the RepeatDescription class.
        /// </summary>
        public RepeatDescription() { }

        /// <summary>
        /// Parse the repeat description data.
        /// </summary>
        /// <param name="part">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string part)
        {
            string[] parts = part.Split(new char[] { ' ' });

            if (parts.Length < 3)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'r' attribute is in the wrong format", part));                

            string interval = parts[0].Trim();
            int intervalLength = interval.Length;
            char type;

            if (interval[intervalLength - 1] == 's' ||
                interval[intervalLength - 1] == 'm' ||
                interval[intervalLength - 1] == 'h' ||
                interval[intervalLength - 1] == 'd')
            {
                type = interval[intervalLength - 1];
                intervalLength--;
            }
            else
            {
                if (interval[intervalLength - 1] < '0' || interval[intervalLength - 1] > '9')
                    return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'r' attribute interval type field is not recognized", part));                
                else
                    type = 's';
            }

            try
            {
                int intervalNumber = Int32.Parse(interval.Substring(0, intervalLength));
                switch (type)
                {
                    case 's':
                        Interval = new TimeSpan(intervalNumber * TimeSpan.TicksPerSecond);
                        break;
                    case 'm':
                        Interval = new TimeSpan(intervalNumber * TimeSpan.TicksPerMinute);
                        break;
                    case 'h':
                        Interval = new TimeSpan(intervalNumber * TimeSpan.TicksPerHour);
                        break;
                    case 'd':
                        Interval = new TimeSpan(intervalNumber * TimeSpan.TicksPerDay);
                        break;                    
                }
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'r' attribute interval field is in the wrong format", part));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'r' attribute interval field is out of range", part));                
            }

            try
            {
                ActiveDuration = new TimeSpan(Int32.Parse(parts[1].Trim()) * TimeSpan.TicksPerSecond);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'r' attribute active duration field is in the wrong format", part));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'r' attribute active duration field is out of range", part));                
            }

            for (int index = 2; index < parts.Length; index++)
            {
                try
                {
                    TimeSpan offset = new TimeSpan(Int32.Parse(parts[index].Trim()) * TimeSpan.TicksPerSecond);
                    if (Offsets == null)
                        Offsets = new Collection<TimeSpan>();
                    Offsets.Add(offset);
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'r' attribute offset field is in the wrong format", part));                
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'r' attribute offset field is out of range", part));                
                }
            }

            return (null);
        }
    }
}

