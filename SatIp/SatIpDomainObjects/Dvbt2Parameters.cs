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
    /// The class that describes DVB-T2 specific tuning parameters.
    /// </summary>
    public class Dvbt2Parameters : DvbtParameters
    {
        /// <summary>
        /// Get the modulation system.
        /// </summary>
        public override string ModulationSystem { get { return ("dvbt2"); } }

        /// <summary>
        /// Get or set the PLP value.
        /// </summary>
        public int Plp { get; set; }
        /// <summary>
        /// Get or set the system ID.
        /// </summary>
        public int SystemId { get; set; }
        /// <summary>
        /// Get or set the SISO mode.
        /// </summary>
        public int SisoMode { get; set; }

        /// <summary>
        /// Initialize a new instance of the Dvbt2Parameters class.
        /// </summary>
        /// <param name="frequency">The tuning frequency.</param>
        public Dvbt2Parameters(int frequency) : base(frequency) 
        {
            Plp = -1;
            SystemId = -1;
            SisoMode = -1;            
        }

        /// <summary>
        /// Return a SAT>IP formatted string for this instance.
        /// </summary>
        /// <returns>A string containing this instances values.</returns>
        public override string ToString()
        {
            StringBuilder reply = new StringBuilder(base.ToString(false));

            reply.Append("&msys=" + ModulationSystem);

            if (Plp != -1)
                reply.Append("&plp=" + Plp);

            if (SystemId != -1)
                reply.Append("&t2id=" + SystemId);

            if (SisoMode != -1)
                reply.Append("&sm=" + SisoMode);

            return (reply.ToString());
        }
    }
}

