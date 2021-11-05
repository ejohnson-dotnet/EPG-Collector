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
using System.Net;
using System.Net.Sockets;

using SatIpDomainObjects;
using UPnP;

namespace Rtsp
{
    /// <summary>
    /// The class that describes an RTSP session.
    /// </summary>
    public class RtspSession
    {
        /// <summary>
        /// Get the setup data.
        /// </summary>
        public RtspSetup Setup { get; private set; }

        private string hostAddress;
        private int hostPort = 554;

        private int cseq = 1;

        private RtspSession() { }

        private RtspSession(string hostAddress, int hostPort)
        {
            this.hostAddress = hostAddress;
            this.hostPort = hostPort;
        }

        /// <summary>
        /// Start a session.
        /// </summary>
        /// <param name="tuningParameters">The tuning parameters.</param>
        /// <param name="path">The path.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Start(TuningParameters tuningParameters, string path)
        {
            Setup = new RtspSetup();

            int dataPort;

            try
            {
                dataPort = getPortNumbers();
            }
            catch (InvalidOperationException)
            {
                return (new ErrorSpec(RtspMessageBase.RtspProtocolId, ErrorCode.ServiceUnavailable, 0, "RTSP Session: No IPv4 address located to set the local IP address", null));
            }
            
            ErrorSpec errorSpec = Setup.Process(hostAddress, hostPort, cseq, false, null, dataPort, dataPort + 1, 5, tuningParameters, path);
            if (errorSpec != null)
                SatIpLogger.Instance.Write("SETUP failed: " + errorSpec);
            
            return (errorSpec);
        }

        private int getPortNumbers()
        {
            IPAddress localIPAddress = Utils.GetLocalIpAddress(SatIpLogger.Instance);
            if (localIPAddress == null)
                throw (new InvalidOperationException("No IPv4 address located to set the local IP address"));

            SatIpLogger.Instance.Write("Local IP address set to " + localIPAddress);

            bool found = false;
            int portNumber = UPnPMessage.UPnPClientPort + 1;
            int startPortNumber = portNumber;

            Socket socket = null;
            int increment = 1;

            while (!found)
            {
                try
                {
                    increment = 1;

                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    IPEndPoint localEndPoint = new IPEndPoint(localIPAddress, portNumber);
                    socket.Bind(localEndPoint);

                    socket.Close();
                    socket.Dispose();

                    increment = 2;

                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    localEndPoint = new IPEndPoint(localIPAddress, portNumber + 1);
                    socket.Bind(localEndPoint);

                    socket.Close();
                    socket.Dispose();

                    found = true;
                }
                catch (SocketException e)
                {
                    if (socket != null)
                    {
                        socket.Close();
                        socket.Dispose();
                    }

                    if (portNumber - startPortNumber < 1000)
                        portNumber += increment;
                    else
                        throw e;

                }
            }

            SatIpLogger.Instance.Write("RTP data port set to " + portNumber);

            return (portNumber);
        }

        /// <summary>
        /// Start the session playing.
        /// </summary>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Play()
        {
            cseq++;

            RtspPlay play = new RtspPlay();
            
            ErrorSpec errorSpec = play.Process(hostAddress, hostPort, cseq, Setup.StreamId, Setup.SessionId);
            if (errorSpec != null)
                SatIpLogger.Instance.Write("PLAY failed: " + errorSpec);
            
            return (errorSpec);
        }

        /// <summary>
        /// Change the tuning parameters.
        /// </summary>
        /// <param name="tuningParameters">The new tuning parameters.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Modify(TuningParameters tuningParameters)
        {
            cseq++;

            RtspPlay play = new RtspPlay();
            
            ErrorSpec errorSpec = play.Process(hostAddress, hostPort, cseq, Setup.StreamId, Setup.SessionId, tuningParameters);
            if (errorSpec != null)
                SatIpLogger.Instance.Write("PLAY failed: " + errorSpec);
            
            return (errorSpec);
        }

        /// <summary>
        /// Stop playing.
        /// </summary>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Stop()
        {
            cseq++;

            RtspTearDown tearDown = new RtspTearDown();
            
            ErrorSpec errorSpec = tearDown.Process(hostAddress, hostPort, cseq, Setup.SessionId, Setup.StreamId);
            if (errorSpec != null)
                SatIpLogger.Instance.Write("TEARDOWN failed: " + errorSpec);
            
            return (errorSpec);
        }

        /// <summary>
        /// Get a description of the current state of the server.
        /// </summary>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Describe()
        {
            cseq++;

            RtspDescribe describe = new RtspDescribe();
            
            ErrorSpec errorSpec = describe.Process(hostAddress, hostPort, cseq);
            if (errorSpec != null)
                SatIpLogger.Instance.Write("DESCRIBE failed: " + errorSpec);
            
            return (errorSpec);
        }

        /// <summary>
        /// Create an Instance of the RtspSession class.
        /// </summary>
        /// <param name="hostName"The host address.</param>
        /// <param name="hostPort">The host port.</param>
        /// <returns></returns>
        public static RtspSession CreateInstance(string hostName, int hostPort)
        {
            return (new RtspSession(hostName, hostPort));
        }
    }
}

