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

using System.IO;

using Sdp;
using SatIpDomainObjects;

namespace Rtsp
{
    /// <summary>
    /// The class that describes an RTSP Describe message
    /// </summary>
    public class RtspDescribe : RtspMessageBase
    {
        /// <summary>
        /// Get the session description.
        /// </summary>
        public SessionDescription SessionDescription { get; private set; }

        /// <summary>
        /// Initialize a new instance of the RtspDescribe class.
        /// </summary>
        public RtspDescribe() { }

        /// <summary>
        /// Send a Desribe request and receive and process the response.
        /// </summary>
        /// <param name="hostAddress">The address of the server.</param>
        /// <param name="hostPort">The port used by the server.</param>
        /// <param name="cseq">The sequence number to be used for the message.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string hostAddress, int hostPort, int cseq)
        {
            MemoryStream outputStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(outputStream);

            streamWriter.WriteLine("DESCRIBE rtsp://" + hostAddress + ":" + hostPort + "/ RTSP/1.0");
            streamWriter.WriteLine("Cseq:" + cseq);
            streamWriter.WriteLine("Accept:application/sdp");
            streamWriter.WriteLine("Connection:close");
            streamWriter.WriteLine(string.Empty);
            streamWriter.Close();

            StreamReader streamReader = ProcessRequest("Describe", outputStream, hostAddress, hostPort);

            ErrorSpec error = StatusCode.Process(streamReader, RtspProtocolId, RtspProtocolVersion);
            if (error != null)
                return (error);

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    SessionDescription = new SessionDescription();
                    error = SessionDescription.Process(streamReader);
                }
                else
                    error = base.ProcessLine(line, cseq);

                if (error != null)
                {
                    streamReader.Close();
                    return (error);
                }
            }

            streamReader.Close();

            return (null);
        }
    }
}}