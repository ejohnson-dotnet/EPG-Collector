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

using System.Collections.ObjectModel;
using System.Text;

using DomainObjects;
using SatIp;
using NetworkProtocols;

namespace SatIpDomainObjects
{
    /// <summary>
    /// The base class for tuning parameters.
    /// </summary>
    public abstract class TuningParameters
    {        
        /// <summary>
        /// Get or set the frontend value.
        /// </summary>
        public int FrontEnd { get; set; }
        /// <summary>
        /// Get or set the frequency.
        /// </summary>
        public int Frequency { get; protected set; }
        /// <summary>
        /// Get or set the list of PID's.
        /// </summary>
        public Collection<int> Pids { get; set; }

        /// <summary>
        /// Get the modulation system. Overridden by derived classes.
        /// </summary>
        public abstract string ModulationSystem { get; }

        private TuningParameters() 
        {            
            FrontEnd = -1;
        }

        /// <summary>
        /// Initialize a new instance of the TuningParameters class.
        /// </summary>
        /// <param name="frequency">The tuning frequency.</param>
        protected TuningParameters(int frequency) : this()
        {
            Frequency = frequency;
        }

        /// <summary>
        /// Return a SAT>IP formatted string for this instance.
        /// </summary>
        /// <returns>A string containing this instances values.</returns>
        public override string ToString()
        {
            StringBuilder reply = new StringBuilder();

            if (FrontEnd != -1)
                reply.Append("fe=" + FrontEnd);
            else
            {
                if (((SatIpConfiguration)NetworkConfiguration.SatIpConfiguration).Frontend != 0)
                    reply.Append("fe=" + ((SatIpConfiguration)NetworkConfiguration.SatIpConfiguration).Frontend);
            }

            if (reply.Length != 0)
                reply.Append("&");
            reply.Append("freq=" + Frequency);

            if (Pids != null && Pids.Count > 0 && Pids[0] != -1)
            {
                reply.Append("&pids=");

                foreach (int pid in Pids)
                {
                    if (Pids.IndexOf(pid) != 0)
                        reply.Append(",");
                    reply.Append(pid);
                }
            }
            else
                reply.Append("&pids=all");

            return (reply.ToString());
        }

        /// <summary>
        /// Convert a tuning spec to SAT>IP values.
        /// </summary>
        /// <param name="tuningSpec">The tuning spec instance.</param>
        /// <param name="diseqcSetting">The DiSEqC value (1-4).</param>
        /// <returns>A TuningParameter instance containg the tuning spec values.</returns>
        public static TuningParameters GetParameters(TuningSpec tuningSpec, int diseqcSetting)
        {
            SatelliteFrequency satelliteFrequency = tuningSpec.Frequency as SatelliteFrequency;
            if (satelliteFrequency != null)
            {
                if (!satelliteFrequency.IsS2)
                {
                    DvbsParameters satelliteParameters = new DvbsParameters(satelliteFrequency.Frequency / 1000);
                    satelliteParameters.Fec = satelliteFrequency.FEC.Rate.Replace("/", "");
                    satelliteParameters.ModulationType = convertModulation(satelliteFrequency.Modulation);
                    satelliteParameters.Polarity = satelliteFrequency.Polarization.PolarizationAbbreviation.ToLowerInvariant();

                    if (diseqcSetting != 0)
                        satelliteParameters.Source = diseqcSetting;

                    satelliteParameters.SymbolRate = satelliteFrequency.SymbolRate;

                    satelliteParameters.Pids = new Collection<int>();
                    satelliteParameters.Pids.Add(0x14);

                    satelliteParameters.FrontEnd = satelliteFrequency.SatIpFrontend;

                    return (satelliteParameters);
                }
                else
                {
                    Dvbs2Parameters satelliteParameters = new Dvbs2Parameters(satelliteFrequency.Frequency / 1000);
                    satelliteParameters.Fec = satelliteFrequency.FEC.Rate.Replace("/", "");
                    satelliteParameters.ModulationType = convertModulation(satelliteFrequency.Modulation);
                    satelliteParameters.Polarity = satelliteFrequency.Polarization.PolarizationAbbreviation.ToLowerInvariant();

                    if (diseqcSetting != 0)
                        satelliteParameters.Source = diseqcSetting;

                    satelliteParameters.SymbolRate = satelliteFrequency.SymbolRate;

                    satelliteParameters.Pilot = satelliteFrequency.Pilot.ToString().ToLowerInvariant();
                    satelliteParameters.RollOff = convertRollOff(satelliteFrequency.RollOff);

                    satelliteParameters.FrontEnd = satelliteFrequency.SatIpFrontend;

                    return (satelliteParameters);
                }
            }
            
            TerrestrialFrequency terrestrialFrequency = tuningSpec.Frequency as TerrestrialFrequency;
            if (terrestrialFrequency != null)
            {
                if (!terrestrialFrequency.IsT2)
                {
                    DvbtParameters terrestrialParameters = new DvbtParameters(terrestrialFrequency.Frequency / 1000);
                    terrestrialParameters.BandWidth = terrestrialFrequency.Bandwidth;
                    terrestrialParameters.FrontEnd = terrestrialFrequency.SatIpFrontend;
                    return (terrestrialParameters);
                }
                else
                {
                    Dvbt2Parameters terrestrialParameters = new Dvbt2Parameters(terrestrialFrequency.Frequency / 1000);
                    terrestrialParameters.BandWidth = terrestrialFrequency.Bandwidth;
                    terrestrialParameters.Plp = terrestrialFrequency.PlpNumber;
                    terrestrialParameters.FrontEnd = terrestrialFrequency.SatIpFrontend;
                    return (terrestrialParameters);
                }                    
            }

            CableFrequency cableFrequency = tuningSpec.Frequency as CableFrequency;
            if (cableFrequency != null)
            {
                DvbcParameters cableParameters = new DvbcParameters(cableFrequency.Frequency / 1000);
                cableParameters.SymbolRate = cableFrequency.SymbolRate;
                cableParameters.FrontEnd = cableFrequency.SatIpFrontend;
                return (cableParameters);
            }

            return (null);
        }

        private static string convertModulation(SignalModulation.Modulation modulation)
        {
            if (modulation == SignalModulation.Modulation.PSK8)
                return ("8psk");
            else
                return (modulation.ToString().ToLowerInvariant());                
        }

        private static string convertRollOff(SignalRollOff.RollOff rollOff)
        {
            switch (rollOff)
            {
                case SignalRollOff.RollOff.RollOff20:
                    return ("0.20");
                case SignalRollOff.RollOff.RollOff25:
                    return ("0.25");
                case SignalRollOff.RollOff.RollOff35:
                    return ("0.35");
                default:
                    return ("0.35");
            }
        }
    }
}

