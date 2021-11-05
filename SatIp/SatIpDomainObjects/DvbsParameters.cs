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

namespace SatIpDomainObjects
{
    /// <summary>
    /// The class that describes DVB-S specific tuning parameters.
    /// </summary>
    public class DvbsParameters : TuningParameters
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
        /// Get or set the source value.
        /// </summary>
        public int Source { get; set; }

        /// <summary>
        /// Initialize a new instance of the DvbsParameters class.
        /// </summary>
        /// <param name="frequency">The tuning frequency.</param>
        public DvbsParameters(int frequency) : base(frequency) 
        {
            Polarity = "h";
            SymbolRate = 22500;
            Fec = "34";
            Source = 1;
        }

        /// <summary>
        /// Return a SAT>IP formatted string for this instance.
        /// </summary>
        /// <returns>A string containing this instances values.</returns>
        public override string ToString()
        {
            return (ToString(true));
        }

        /// <summary>
        /// Return a SAT>IP formatted string for this instance.
        /// </summary>
        /// <param name="includeModulationSystem">True if the modulation system value is to be included.</param>
        /// <returns>A string containing this instances values.</returns>
        public string ToString(bool includeModulationSystem)
        {
            StringBuilder reply = new StringBuilder(base.ToString());

            if (includeModulationSystem)
                reply.Append("&msys=" + ModulationSystem);

            if (ModulationType != null)
            {
                if (ModulationType == "psk8")
                    reply.Append("&mtype=8psk");
                else
                    reply.Append("&mtype=" + ModulationType);
            }

            if (Source != -1)
                reply.Append("&src=" + Source);

            reply.Append("&pol=" + Polarity);
            reply.Append("&sr=" + SymbolRate);
            reply.Append("&fec=" + Fec);

            return (reply.ToString());
        }
    }
}

