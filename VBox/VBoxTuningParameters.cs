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

namespace VBox
{
    /// <summary>
    /// The base class for tuning parameters.
    /// </summary>
    public abstract class VBoxTuningParameters
    {        
        /// <summary>
        /// Get or set the frequency.
        /// </summary>
        public int Frequency { get; protected set; }
        
        /// <summary>
        /// Get the modulation system. Overridden by derived classes.
        /// </summary>
        public abstract string ModulationSystem { get; }

        private VBoxTuningParameters() { }

        /// <summary>
        /// Initialize a new instance of the TuningParameters class.
        /// </summary>
        /// <param name="frequency">The tuning frequency.</param>
        protected VBoxTuningParameters(int frequency) : this()
        {
            Frequency = frequency;
        }

        /// <summary>
        /// Convert a tuning spec to SAT>IP values.
        /// </summary>
        /// <param name="tuningSpec">The tuning spec instance.</param>
        /// <param name="diseqcSetting">The DiSEqC value (1-4).</param>
        /// <returns>A TuningParameter instance containg the tuning spec values.</returns>
        public static VBoxTuningParameters GetParameters(TuningSpec tuningSpec, int diseqcSetting)
        {
            SatelliteFrequency satelliteFrequency = tuningSpec.Frequency as SatelliteFrequency;
            if (satelliteFrequency != null)
            {
                if (!satelliteFrequency.IsS2)
                {
                    VBoxDvbsParameters satelliteParameters = new VBoxDvbsParameters(satelliteFrequency.Frequency / 1000);                    
                    satelliteParameters.Fec = satelliteFrequency.FEC.Rate.Replace("/", "");
                    satelliteParameters.ModulationType = convertModulation(satelliteFrequency.Modulation);
                    satelliteParameters.Polarity = satelliteFrequency.Polarization.PolarizationAbbreviation.ToLowerInvariant();
                    satelliteParameters.SymbolRate = satelliteFrequency.SymbolRate;
                    satelliteParameters.LnbLow = satelliteFrequency.SatelliteDish.LNBLowBandFrequency;
                    satelliteParameters.LnbHigh = satelliteFrequency.SatelliteDish.LNBHighBandFrequency;
                    satelliteParameters.LnbSwitch = satelliteFrequency.SatelliteDish.LNBSwitchFrequency; 
                    return (satelliteParameters);
                }
                else
                {
                    VBoxDvbs2Parameters satelliteParameters = new VBoxDvbs2Parameters(satelliteFrequency.Frequency / 1000);
                    satelliteParameters.Fec = satelliteFrequency.FEC.Rate.Replace("/", "");
                    satelliteParameters.ModulationType = convertModulation(satelliteFrequency.Modulation);
                    satelliteParameters.Polarity = satelliteFrequency.Polarization.PolarizationAbbreviation.ToLowerInvariant();
                    satelliteParameters.SymbolRate = satelliteFrequency.SymbolRate;
                    satelliteParameters.Pilot = satelliteFrequency.Pilot.ToString().ToLowerInvariant();
                    satelliteParameters.RollOff = convertRollOff(satelliteFrequency.RollOff);
                    satelliteParameters.LnbLow = satelliteFrequency.SatelliteDish.LNBLowBandFrequency;
                    satelliteParameters.LnbHigh = satelliteFrequency.SatelliteDish.LNBHighBandFrequency;
                    satelliteParameters.LnbSwitch = satelliteFrequency.SatelliteDish.LNBSwitchFrequency; 
                    return (satelliteParameters);
                }
            }
            
            TerrestrialFrequency terrestrialFrequency = tuningSpec.Frequency as TerrestrialFrequency;
            if (terrestrialFrequency != null)
            {
                if (!terrestrialFrequency.IsT2)
                {
                    VBoxDvbtParameters terrestrialParameters = new VBoxDvbtParameters(terrestrialFrequency.Frequency);
                    terrestrialParameters.BandWidth = terrestrialFrequency.Bandwidth;
                    return (terrestrialParameters);
                }
                else
                {
                    VBoxDvbt2Parameters terrestrialParameters = new VBoxDvbt2Parameters(terrestrialFrequency.Frequency);
                    terrestrialParameters.BandWidth = terrestrialFrequency.Bandwidth;
                    terrestrialParameters.Plp = terrestrialFrequency.PlpNumber;
                    return (terrestrialParameters);
                }                    
            }

            CableFrequency cableFrequency = tuningSpec.Frequency as CableFrequency;
            if (cableFrequency != null)
            {
                VBoxDvbcParameters cableParameters = new VBoxDvbcParameters(cableFrequency.Frequency / 1000);
                cableParameters.SymbolRate = cableFrequency.SymbolRate;
                return (cableParameters);
            }

            return (null);
        }

        private static string convertModulation(SignalModulation.Modulation modulation)
        {
            if (modulation == SignalModulation.Modulation.PSK8)
                return ("8PSK");
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

