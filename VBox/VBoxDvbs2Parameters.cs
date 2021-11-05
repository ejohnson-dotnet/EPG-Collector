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
    /// The class that describes DVB-S2 specific tuning parameters.
    /// </summary>
    public class VBoxDvbs2Parameters : VBoxDvbsParameters
    {
        /// <summary>
        /// Get the modulation system.
        /// </summary>
        public override string ModulationSystem { get { return ("dvbs2"); } }

        /// <summary>
        /// Get or set the roll off value.
        /// </summary>
        public string RollOff { get; set; }
        /// <summary>
        /// Get or set the pilot value.
        /// </summary>
        public string Pilot { get; set; }        

        /// <summary>
        /// Initialize a new instance of the Dvbs2Parameters class.
        /// </summary>
        /// <param name="frequency">The tuning frequency.</param>
        public VBoxDvbs2Parameters(int frequency) : base(frequency) { }

        /// <summary>
        /// Return a SAT>IP formatted string for this instance.
        /// </summary>
        /// <returns>A string containing this instances values.</returns>
        public override string ToString()
        {
            StringBuilder reply = new StringBuilder(base.ToString());
            reply.Append("2");

            reply.Append("&Modulation=" + ModulationType);

            if (RollOff != null)
                reply.Append("&RollOff=" + RollOff);

            if (Pilot != null)
                reply.Append("&Pilot=" + Pilot.ToUpperInvariant());

            return (reply.ToString());
        }
    }
}

