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

using DomainObjects;

namespace SatIpDomainObjects
{
    /// <summary>
    /// The class that describes the SAT>IP logger.
    /// </summary>
    public class SatIpLogger : ILogger
    {
        /// <summary>
        /// Get an instance of a logger.
        /// </summary>
        public static SatIpLogger Instance
        {
            get
            {
                if (instance == null)
                    instance = new SatIpLogger();
                return (instance);
            }
        }

        private static SatIpLogger instance;
        private static Logger logger;

        private SatIpLogger()
        {
            logger = new DomainObjects.Logger("EPG Collector Network.log", false);
        }

        /// <summary>
        /// Write a log message.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public void Write(string message)
        {
            logger.Write(message);
        }

        /// <summary>
        /// Log a message response.
        /// </summary>
        /// <param name="title">The name of the response.</param>
        /// <param name="buffer">The buffer containing the response.</param>
        /// <param name="count">The length of the response.</param>
        public void LogReply(string title, byte[] buffer, int count)
        {
            LogReply(title, new StreamReader(new MemoryStream(buffer, 0, count)));            
        }

        /// <summary>
        /// Log a message response.
        /// </summary>
        /// <param name="title">The name of the response.</param>
        /// <param name="streamReader">The stream containing the response.</param>
        public void LogReply(string title, StreamReader streamReader)
        {
            while (!streamReader.EndOfStream)
                Instance.Write(title + streamReader.ReadLine());

            streamReader.Close();
        }
    }
}

