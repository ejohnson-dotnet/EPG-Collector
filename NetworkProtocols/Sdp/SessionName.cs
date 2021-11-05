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

namespace NetworkProtocols.Sdp
{
    /// <summary>
    /// The class the describes a session name.
    /// </summary>
    public class SessionName
    {
        /// <summary>
        /// Get the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Get the number of DVB-S/S2 front ends.
        /// </summary>
        public int DvbsFrontEnds { get; private set; }
        /// <summary>
        /// Get the number of DVBT/T2 front ends.
        /// </summary>
        public int DvbtFrontEnds { get; private set; }
        /// <summary>
        /// Get the number of DVBC/C2 front ends.
        /// </summary>
        public int DvbcFrontEnds { get; private set; }

        /// <summary>
        /// Initialize a new instance of the SessionName class.
        /// </summary>
        public SessionName() { }

        /// <summary>
        /// Parse the session name data.
        /// </summary>
        /// <param name="part">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string part)
        {
            string[] parts = part.Split(new char[] { ' ' });

            if (parts.Length != 2)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 's' attribute is in the wrong format", part));                

            Name = parts[0].Trim();

            try
            {
                string[] frontEnds = parts[1].Split(new char[] { ',' });

                DvbsFrontEnds = Int32.Parse(frontEnds[0].Trim());

                if (frontEnds.Length > 1)
                {
                    DvbtFrontEnds = Int32.Parse(frontEnds[1].Trim());
                    if (frontEnds.Length > 2)
                    {
                        DvbcFrontEnds = Int32.Parse(frontEnds[2].Trim());
                    } 
                }  

                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 's' attribute front ends field is in the wrong format", part));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 's' attribute front ends field is in the wrong format", part));                
            }
        }
    }
}

