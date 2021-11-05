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

namespace VBox
{
    /// <summary>
    /// The class that describes DVB-T specific tuning parameters.
    /// </summary>
    public class VBoxDvbtParameters : VBoxTuningParameters
    {
        /// <summary>
        /// Get the modulation system.
        /// </summary>
        public override string ModulationSystem { get { return ("dvbt"); } }

        /// <summary>
        /// Get or set the bandwidth.
        /// </summary>
        public int BandWidth { get; set; }
        /// <summary>
        /// Get or set the transmission mode.
        /// </summary>
        public string TransmissionMode { get; set; }
        /// <summary>
        /// Get or set the modulation type.
        /// </summary>
        public string ModulationType { get; set; }
        /// <summary>
        /// Get or set the guard interval.
        /// </summary>
        public int GuardInterval { get; set; }
        /// <summary>
        /// Get or set the FEC value.
        /// </summary>
        public string Fec { get; set; }

        /// <summary>
        /// Initialize a new instance of the DvbtParameters class.
        /// </summary>
        /// <param name="frequency">The tuning frequency.</param>
        public VBoxDvbtParameters(int frequency) : base(frequency) 
        {
            BandWidth = 8;
            GuardInterval = -1;
        }

        /// <summary>
        /// Return a VBOX formatted string for this instance.
        /// </summary>
        /// <returns>A string containing this instances values.</returns>
        public override string ToString()
        {
            StringBuilder reply = new StringBuilder("&TunerType=TER");            
            reply.Append("&Frequency=" + Frequency);
            reply.Append("&Bandwidth=" + BandWidth + "MHZ");
            reply.Append("&Modulation=AUTO");
            reply.Append("&Transmission=AUTO");
            reply.Append("&GuardInterval=AUTO");
            reply.Append("&CodeRate=AUTO");

            return (reply.ToString());
        }
    }
}

