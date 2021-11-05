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

using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.ObjectModel;

using DomainObjects;

using SatIp;

namespace SatIpDomainObjects
{
    /// <summary>
    /// Support class for the SAT>IP classes.
    /// </summary>
    public sealed class Utils
    {
        private Utils() { }

        /// <summary>
        /// Convert 2 bytes to an int.
        /// </summary>
        /// <param name="buffer">The buffer containing the bytes.</param>
        /// <param name="offset">The offset to the first byte.</param>
        /// <returns>The converted value.</returns>
        public static int Convert2BytesToInt(byte[] buffer, int offset)
        {
            int temp = (int)buffer[offset];
            temp = (temp * 256) + buffer[offset + 1];

            return (temp);
        }

        /// <summary>
        /// Convert 3 bytes to an int.
        /// </summary>
        /// <param name="buffer">The buffer containing the bytes.</param>
        /// <param name="offset">The offset to the first byte.</param>
        /// <returns>The converted value.</returns>
        public static int Convert3BytesToInt(byte[] buffer, int offset)
        {
            int temp = (int)buffer[offset];
            temp = (temp * 256) + buffer[offset + 1];
            temp = (temp * 256) + buffer[offset + 2];            

            return (temp);
        }

        /// <summary>
        /// Convert 4 bytes to an int.
        /// </summary>
        /// <param name="buffer">The buffer containing the bytes.</param>
        /// <param name="offset">The offset to the first byte.</param>
        /// <returns>The converted value.</returns>
        public static int Convert4BytesToInt(byte[] buffer, int offset)
        {
            int temp = (int)buffer[offset];
            temp = (temp * 256) + buffer[offset + 1];
            temp = (temp * 256) + buffer[offset + 2];
            temp = (temp * 256) + buffer[offset + 3];

            return (temp);
        }

        /// <summary>
        /// Convert 8 bytes to a long.
        /// </summary>
        /// <param name="buffer">The buffer containing the bytes.</param>
        /// <param name="offset">The offset to the first byte.</param>
        /// <returns>The converted value.</returns>
        public static long Convert8BytesToLong(byte[] buffer, int offset)
        {
            long temp = 0;

            for (int index = 0; index < 8; index++)
                temp = (temp * 256) + buffer[offset + index];

            return (temp);
        }

        /// <summary>
        /// Convert a number of bytes to a string.
        /// </summary>
        /// <param name="buffer">The buffer containing the bytes.</param>
        /// <param name="offset">The offset to the first byte.</param>
        /// <param name="length">The number of bytes to convert.</param>
        /// <returns>The converted string.</returns>
        public static string ConvertBytesToString(byte[] buffer, int offset, int length)
        {
            StringBuilder reply = new StringBuilder(4);

            for (int index = 0; index < length; index++)
                reply.Append((char)buffer[offset + index]);            

            return (reply.ToString());
        }

        /// <summary>
        /// Get a local IPv4 address.
        /// </summary>
        /// <param name="logger">The logger to use.</param>
        /// <returns></returns>
        public static IPAddress GetLocalIpAddress(ILogger logger)
        {
            IPAddress localAddress = getUserSpecifiedAddress();
            if (localAddress != null)
            {
                logger.Write("User specified IP Address " + localAddress);
                return (localAddress);
            }

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                foreach (UnicastIPAddressInformation addressInfo in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    string addressString = addressInfo.Address.ToString();
                    if (logger != null)
                        logger.Write("Network information IP Address " + addressString);

                    if (!addressString.Contains(":"))
                    {
                        string[] addressParts = addressString.Split(new char[] { '.' });

                        if (addressParts.Length == 4 && addressParts[0] != "127")
                            localAddress = addressInfo.Address;
                    }
                }
            }

            return (localAddress);
        }

        private static IPAddress getUserSpecifiedAddress()
        {
            if (SatIpController.Configuration == null)
                return (null);
            if (SatIpController.Configuration.LocalAddress == null)
                return (null);
            
            return (IPAddress.Parse(SatIpController.Configuration.LocalAddress));
            
        }

        public static Collection<IPAddress> GetLocalIpAddresses()
        {
            Collection<IPAddress> localAddresses = new Collection<IPAddress>();

            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                foreach (UnicastIPAddressInformation addressInfo in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    string addressString = addressInfo.Address.ToString();                    

                    if (!addressString.Contains(":"))
                    {
                        string[] addressParts = addressString.Split(new char[] { '.' });

                        if (addressParts.Length == 4 && addressParts[0] != "127")
                            localAddresses.Add(addressInfo.Address);
                    }
                }
            }

            return (localAddresses);
        }
    }
}

