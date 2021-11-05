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

using SatIpDomainObjects;

namespace Sdp
{
    /// <summary>
    /// The class the describes an encryption key.
    /// </summary>
    public class EncryptionKey
    {
        /// <summary>
        /// Get the type of the key.
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// Get the encryption key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Initialize a new instance of the EncryptionKey class.
        /// </summary>
        public EncryptionKey() { }

        /// <summary>
        /// Parse the encryption key line.
        /// </summary>
        /// <param name="part">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public ErrorSpec Process(string part)
        {
            string[] parts = part.Split(new char[] { ':' });

            if (parts.Length > 2)
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'k' attribute is in the wrong format", part));                

            Type = parts[0].Trim();
            if (!IANAConstants.IsValid(Type, IANAConstants.EncryptionType))
                return (new ErrorSpec(SessionDescription.SdpProtocolId, ErrorCode.FormatError, 0, "The 'k' attribute key type is not recognized", part));                

            if (parts.Length == 2)
                Key = parts[1]; 

            return (null);
        }
    }
}

