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
using System.Xml;

namespace VBox
{
    /// <summary>
    /// The base class for VBox responses.
    /// </summary>
    public class VBoxResponse
    {
        /// <summary>
        /// Get the error code.
        /// </summary>
        public int ErrorCode { get; internal set; }
        /// <summary>
        /// Get the error description.
        /// </summary>
        public string ErrorDescription { get; internal set; }

        /// <summary>
        /// Return true if the request was successful; false otherwise.
        /// </summary>
        public bool IsOK { get { return ErrorCode == 0; } }

        internal VBoxResponse() { }

        internal VBoxResponse(int errorCode, string errorDescription)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }

        internal virtual void Process(XmlReader reader)
        {
            while (!reader.EOF)
            {
                reader.Read();
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "ErrorCode":
                            ErrorCode = Int32.Parse(reader.ReadString());
                            break;
                        case "ErrorDescription":
                            ErrorDescription = reader.ReadString();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Get a string representation of the response.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (" ErrorCode=" + ErrorCode +
                " ErrorDescription=" + ErrorDescription);
        }
    }
}

