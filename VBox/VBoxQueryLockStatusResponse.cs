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
    /// The class that describes the response to a QueryLockStatus request.
    /// </summary>
    public class VBoxQueryLockStatusResponse : VBoxResponse
    {
        /// <summary>
        /// Get the lock status.
        /// </summary>
        public string LockStatus { get; private set; }
        /// <summary>
        /// Get the signal strength.
        /// </summary>
        public int SignalStrength { get; private set; }
        /// <summary>
        /// Get the RF level.
        /// </summary>
        public int RfLevel { get; private set; }
        /// <summary>
        /// Get the SNR value.
        /// </summary>
        public double SignalToNoiseRatio { get; private set; }

        /// <summary>
        /// Return true is signal is locked; false otherwise.
        /// </summary>
        public bool IsLocked { get { return (LockStatus == "LOCKED"); } }

        internal VBoxQueryLockStatusResponse() { }

        internal override void Process(XmlReader reader)
        {
            while (!reader.EOF)
            {
                reader.Read();
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Status":
                            base.Process(reader.ReadSubtree());
                            break;
                        case "LockStatus":
                            LockStatus = reader.ReadString();
                            break;
                        case "SignalStrength":
                            if (IsLocked)
                            {
                                try
                                {
                                    SignalStrength = Int32.Parse(reader.ReadString());
                                }
                                catch (FormatException) { }
                                catch (OverflowException) { }
                            }
                            break;
                        case "RFLevel":
                            if (IsLocked)
                            {
                                string rfLevel = reader.ReadString();
                                string[] levelParts = rfLevel.Split(new char[] { ' ' });
                                if (levelParts.Length == 2)
                                {
                                    try
                                    {
                                        RfLevel = Int32.Parse(levelParts[0].Substring(1));
                                    }
                                    catch (FormatException) { }
                                    catch (OverflowException) { }
                                }
                            }
                            break;
                        case "SNR":
                            if (IsLocked)
                            {
                                string snr = reader.ReadString();
                                string[] snrParts = snr.Split(new char[] { ' ' });
                                if (snrParts.Length == 2)
                                {
                                    try
                                    {
                                        SignalToNoiseRatio = Double.Parse(snrParts[0]);
                                    }
                                    catch (FormatException) { }
                                    catch (OverflowException) { }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Get a text representation of the response.
        /// </summary>
        /// <returns>The text description.</returns>
        public override string ToString()
        {
            return (base.ToString() + " LockStatus=" + LockStatus + 
                " SignalStrength=" + SignalStrength +
                " RFLevel=" + RfLevel +
                " SNR=" + SignalToNoiseRatio);
        }
    }
}

