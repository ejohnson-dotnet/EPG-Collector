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
    /// The class that describes DVB-S specific tuning parameters.
    /// </summary>
    public class VBoxDvbsParameters : VBoxTuningParameters
    {
        /// <summary>
        /// Get the modulation system.
        /// </summary>
        public override string ModulationSystem { get { return ("dvbs"); } }

        /// <summary>
        /// Get or set the modulation type.
        /// </summary>
        public string ModulationType { get; set; }
        /// <summary>
        /// Get or set the polarity.
        /// </summary>
        public string Polarity { get; set; }
        /// <summary>
        /// Get or set the symbol rate.
        /// </summary>
        public int SymbolRate { get; set; }
        /// <summary>
        /// Get or set the FEC value.
        /// </summary>
        public string Fec { get; set; }
        /// <summary>
        /// Get or set the LNB low frequency.
        /// </summary>
        public int LnbLow { get; set; }
        /// <summary>
        /// Get or set the LNB high frequency.
        /// </summary>
        public int LnbHigh { get; set; }
        /// <summary>
        /// Get or set the LNB switch frequency.
        /// </summary>
        public int LnbSwitch { get; set; }

        /// <summary>
        /// Get the LNB type.
        /// </summary>
        public string LnbType
        {
            get
            {
                if (LnbSwitch == 0 || LnbLow == LnbHigh)
                    return ("SINGLE");
                else
                    return ("DUAL");
            }
        }

        /// <summary>
        /// Initialize a new instance of the DvbsParameters class.
        /// </summary>
        /// <param name="frequency">The tuning frequency.</param>
        public VBoxDvbsParameters(int frequency) : base(frequency) 
        {
            Polarity = "h";
            SymbolRate = 22500;
            Fec = "34";

            LnbLow = 107500000;
            LnbHigh = 10750000;
        }

        /// <summary>
        /// Return a VBox formatted string for this instance.
        /// </summary>
        /// <returns>A string containing this instances values.</returns>
        public override string ToString()
        {
            StringBuilder reply = new StringBuilder("&TunerType=SAT");
            reply.Append("&Frequency=" + ((double)Frequency) / 1000);            
            reply.Append("&SymbolRate=" + SymbolRate);
            reply.Append("&Polarization=" + (Polarity == "h" ? "HORIZONTAL" : "VERTICAL"));
            reply.Append("&DvbsMode=DVBS");

            return (reply.ToString());
        }
    }
}

