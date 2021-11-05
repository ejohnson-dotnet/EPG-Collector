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
using System.Collections.ObjectModel;
using System.Text;

using NetworkProtocols;
using NetworkProtocols.Sdp;

namespace SatIp
{
    /// <summary>
    /// The class that describes a SAT>IP media format.
    /// </summary>
    public class SatIpMediaFormat : MediaFormat
    {
        /// <summary>
        /// Get the version.
        /// </summary>
        public string Version { get; private set; }
        /// <summary>
        /// Get the source.
        /// </summary>
        public int Source { get; private set; }

        /// <summary>
        /// Get the front end.
        /// </summary>
        public int FrontEnd { get; private set; }
        /// <summary>
        /// Get the level.
        /// </summary>
        public int Level { get; private set; }
        /// <summary>
        /// Return true if the signal is locked; false otherwise.
        /// </summary>
        public bool SignalLock { get; private set; }
        /// <summary>
        /// Get the signal quality.
        /// </summary>
        public int Quality { get; private set; }
        /// <summary>
        /// Get the frequency.
        /// </summary>
        public decimal Frequency { get; private set; }
        /// <summary>
        /// Get the polarization.
        /// </summary>
        public char Polarization { get; private set; }
        /// <summary>
        /// Get the transmission system.
        /// </summary>
        public string System { get; private set; }
        /// <summary>
        /// Get the modulation system.
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// Get the pilot.
        /// </summary>
        public string Pilot { get; private set; }
        /// <summary>
        /// Get the roll off.
        /// </summary>
        public string RollOff { get; private set; }
        /// <summary>
        /// Get the symbol rate.
        /// </summary>
        public int SymbolRate { get; private set; }
        /// <summary>
        /// Get the FEC.
        /// </summary>
        public string Fec { get; private set; }

        /// <summary>
        /// Get the bandwidth.
        /// </summary>
        public int Bandwidth { get; private set; }
        /// <summary>
        /// Get the transmission system.
        /// </summary>
        public string TransmissionMode { get; private set; }
        /// <summary>
        /// Get the guard interval.
        /// </summary>
        public int GuardInterval { get; private set; }
        /// <summary>
        /// Get the PLP.
        /// </summary>
        public int Plp { get; private set; }
        /// <summary>
        /// Get the T2 system identity.
        /// </summary>
        public int SystemId { get; private set; }
        /// <summary>
        /// Return true if the signal is miso; false otherwise.
        /// </summary>
        public bool SisoMiso { get; private set; }

        /// <summary>
        /// Get the tuning frequency type.
        /// </summary>
        public int TuningFrequencyType { get; private set; }
        /// <summary>
        /// Get the data slice.
        /// </summary>
        public int DataSlice { get; private set; }
        /// <summary>
        /// Return true if the spectrum inversion is off; false otherwise.
        /// </summary>
        public bool SpectrumInversion { get; private set; }

        /// <summary>
        /// Get the list of PID's.
        /// </summary>
        public Collection<int> Pids { get; private set; }

        /// <summary>
        /// Initialize a new instance of the SatIpMediaFormat class.
        /// </summary>
        public SatIpMediaFormat() { }

        /// <summary>
        /// Check if the processor supports the format.
        /// </summary>
        /// <param name="identity">The name of the format.</param>
        /// <returns>Returns true if the format is supported; false otherwise.</returns>
        public override bool IsFormat(string identity)
        {
            return (identity == "33");
        }

        /// <summary>
        /// Create an instance of the format processor.
        /// </summary>
        /// <param name="identity">The name of the processor.</param>
        /// <returns>An instance of the processor.</returns>
        public override ISdpFormat CreateFormat(string identity)
        {
            return (new SatIpMediaFormat());
        }

        /// <summary>
        /// Parse the format line.
        /// </summary>
        /// <param name="parameters">The data to be parsed.</param>
        /// <returns>An ErrorSpec instance if an error occurs; null otherwise.</returns>
        public override ErrorSpec Process(string parameters)
        {
            int index = parameters.IndexOf(' ');
            string[] fields = parameters.Substring(index + 1).Split(new char[] { ';' });

            foreach (string field in fields)
            {
                if (!string.IsNullOrWhiteSpace(field))
                {
                    int index1 = field.IndexOf('=');
                    if (index1 == -1)
                        return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute is in the wrong format", parameters));

                    string identity = field.Substring(0, index1).Trim();
                    string values = field.Substring(index1 + 1).Trim();

                    ErrorSpec error = null;

                    switch (identity)
                    {
                        case "ver":
                            error = processVersion(values);
                            break;
                        case "src":
                            error = processSource(values);
                            break;
                        case "tuner":
                            switch (Version)
                            {
                                case "1.0":
                                    error = processSatelliteTuner(values);
                                    break;
                                case "1.1":
                                    error = processTerrestrialTuner(values);
                                    break;
                                case "1.2":
                                    error = processCableTuner(values);
                                    break;
                                default:
                                    error = processSatelliteTuner(values);
                                    break;
                            }                            
                            break;
                        case "pids":
                            error = processPids(values);
                            break;
                        case "":
                            break;
                        default:
                            // Unrecognized attributes silently ignored from V4.3 FP6 because DVBViewer Recording Service uses custom atttributes
                            break;
                            /*return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute format field is not recognized", parameters));*/
                    }

                    if (error != null)
                        return (error);
                }
            }

            return (null);
        }

        private ErrorSpec processVersion(string parameters)
        {
            Version = parameters;
            return (null);
        }

        private ErrorSpec processSource(string parameters)
        {
            try
            {
                Source = Int32.Parse(parameters);
                return (null);
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute source field is in the wrong format", parameters));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute source field is out of range", parameters));                
            }
        }

        private ErrorSpec processSatelliteTuner(string parameters)
        {
            string[] values = parameters.Split(new char[] { ',' } );
            if (values.Length != 12)
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute tuner field is in the wrong format", parameters));                

            try
            {
                FrontEnd = Int32.Parse(values[0].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frontend field is in the wrong format", parameters));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frontend field is out of range", parameters));                
            }

            try
            {
                Level = Int32.Parse(values[1].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is in the wrong format", parameters));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is out of range", parameters));                
            }

            try
            {
                int signalLock = Int32.Parse(values[2].Trim());
                if (signalLock == 0)
                    SignalLock = false;
                else
                {
                    if (signalLock == 1)
                        SignalLock = true;
                    else
                        return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute lock field is out of range", parameters));                
                }
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute lock field is in the wrong format", parameters));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute lock field is out of range", parameters));                
            }

            try
            {
                Quality = Int32.Parse(values[3].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is in the wrong format", parameters));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is out of range", parameters));                
            }

            try
            {
                Frequency = decimal.Parse(values[4].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frequency field is in the wrong format", parameters));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frequency field is out of range", parameters));                
            }

            string polarization = values[5].Trim();
            if (polarization.Length != 1)
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute polarization field is in the wrong format", parameters));                

            if (polarization != "h" && polarization != "v" && polarization != "l" && polarization != "r")
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute polarization field is out of range", parameters));                

            Polarization = polarization[0];

            System = values[6].Trim();
            Type = values[7].Trim();
            Pilot = values[8].Trim();
            RollOff = values[9].Trim();

            try
            {
                SymbolRate = Int32.Parse(values[10].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute symbol rate field is in the wrong format", parameters));                
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute symbol rate field is out of range", parameters));                
            }

            Fec = values[11].Trim();

            return (null);
        }

        private ErrorSpec processTerrestrialTuner(string parameters)
        {
            string[] values = parameters.Split(new char[] { ',' });
            if (values.Length != 14)
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute tuner field is in the wrong format", parameters));

            try
            {
                FrontEnd = Int32.Parse(values[0].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frontend field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frontend field is out of range", parameters));
            }

            try
            {
                Level = Int32.Parse(values[1].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is out of range", parameters));
            }

            try
            {
                int signalLock = Int32.Parse(values[2].Trim());
                if (signalLock == 0)
                    SignalLock = false;
                else
                {
                    if (signalLock == 1)
                        SignalLock = true;
                    else
                        return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute lock field is out of range", parameters));
                }
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute lock field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute lock field is out of range", parameters));
            }

            try
            {
                Quality = Int32.Parse(values[3].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is out of range", parameters));
            }

            try
            {
                Frequency = decimal.Parse(values[4].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frequency field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frequency field is out of range", parameters));
            }

            try
            {
                Bandwidth = Int32.Parse(values[5].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute bandwidth field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute bandwidth field is out of range", parameters));
            }
            
            System = values[6].Trim();
            TransmissionMode = values[7].Trim();
            Type = values[8].Trim();

            if (!string.IsNullOrWhiteSpace(values[9]))
            {
                try
                {
                    GuardInterval = Int32.Parse(values[9].Trim());
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute guard interval field is in the wrong format", parameters));
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute guard interval field is out of range", parameters));
                }
            }
            
            Fec = values[10].Trim();

            if (!string.IsNullOrWhiteSpace(values[11]))
            {
                try
                {
                    Plp = Int32.Parse(values[11].Trim());
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute PLP field is in the wrong format", parameters));
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute PLP field is out of range", parameters));
                }
            }

            if (!string.IsNullOrWhiteSpace(values[12]))
            {
                try
                {
                    SystemId = Int32.Parse(values[12].Trim());
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute T2 system identity field is in the wrong format", parameters));
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute T2 system identity field is out of range", parameters));
                }
            }

            if (!string.IsNullOrWhiteSpace(values[13]))
            {
                try
                {
                    int sisoMiso = Int32.Parse(values[13].Trim());
                    if (sisoMiso == 0)
                        SisoMiso = false;
                    else
                    {
                        if (sisoMiso == 1)
                            SisoMiso = true;
                        else
                            return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute SISO/MISO field is out of range", parameters));
                    }
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute SISO/MISO field is in the wrong format", parameters));
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute SISO/MISO field is out of range", parameters));
                }
            }

            return (null);
        }

        private ErrorSpec processCableTuner(string parameters)
        {
            string[] values = parameters.Split(new char[] { ',' });
            if (values.Length != 13)
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute tuner field is in the wrong format", parameters));

            try
            {
                FrontEnd = Int32.Parse(values[0].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frontend field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frontend field is out of range", parameters));
            }

            try
            {
                Level = Int32.Parse(values[1].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is out of range", parameters));
            }

            try
            {
                int signalLock = Int32.Parse(values[2].Trim());
                if (signalLock == 0)
                    SignalLock = false;
                else
                {
                    if (signalLock == 1)
                        SignalLock = true;
                    else
                        return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute lock field is out of range", parameters));
                }
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute lock field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute lock field is out of range", parameters));
            }

            try
            {
                Quality = Int32.Parse(values[3].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute level field is out of range", parameters));
            }

            try
            {
                Frequency = decimal.Parse(values[4].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frequency field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute frequency field is out of range", parameters));
            }

            try
            {
                Bandwidth = Int32.Parse(values[5].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute bandwidth field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute bandwidth field is out of range", parameters));
            }

            System = values[6].Trim();
            Type = values[7].Trim();
            
            try
            {
                SymbolRate = Int32.Parse(values[8].Trim());
            }
            catch (FormatException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute symbol rate field is in the wrong format", parameters));
            }
            catch (OverflowException)
            {
                return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute symbol rate field is out of range", parameters));
            }

            if (!string.IsNullOrWhiteSpace(values[9]))
            {
                try
                {
                    TuningFrequencyType = Int32.Parse(values[9].Trim());                    
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute tuning frequency type field is in the wrong format", parameters));
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute tuning frequency type field is out of range", parameters));
                }
            }

            if (!string.IsNullOrWhiteSpace(values[10]))
            {
                try
                {
                    DataSlice = Int32.Parse(values[10].Trim());
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute data slice field is in the wrong format", parameters));
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute data slice field is out of range", parameters));
                }
            }

            if (!string.IsNullOrWhiteSpace(values[11]))
            {
                try
                {
                    Plp = Int32.Parse(values[11].Trim());
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute PLP field is in the wrong format", parameters));
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute PLP field is out of range", parameters));
                }
            }

            if (!string.IsNullOrWhiteSpace(values[12]))
            {
                try
                {
                    int spectrumInversion = Int32.Parse(values[12].Trim());
                    if (spectrumInversion == 0)
                        SpectrumInversion = false;
                    else
                    {
                        if (spectrumInversion == 1)
                            SpectrumInversion = true;
                        else
                            return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute spectrum inversion field is out of range", parameters));
                    }
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute spectrum inversion field is in the wrong format", parameters));
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute spectrum inversion field is out of range", parameters));
                }
            }

            return (null);
        }

        private ErrorSpec processPids(string parameters)
        {
            string[] values = parameters.Split(new char[] { ',' });

            if (values[0].Trim().ToLowerInvariant() == "none")
                return (null);

            Pids = new Collection<int>();

            if (values[0].Trim().ToLowerInvariant() == "all")
            {
                Pids.Add(-1);
                return (null);
            }

            foreach (string value in values)
            {
                try
                {
                    Pids.Add(Int32.Parse(value.Trim()));
                }
                catch (FormatException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute pid field is in the wrong format", parameters));                
                }
                catch (OverflowException)
                {
                    return (new ErrorSpec(SatIpController.SatIpProtocolId, ErrorCode.FormatError, 0, "The 'a' attribute pid field is out of range", parameters));                
                }
            }

            return (null);
        }

        /// <summary>
        /// Convert the instance data to a readable string.
        /// </summary>
        /// <returns></returns>
        public override string  ToString()
        {
            StringBuilder pidString = new StringBuilder();

            if (Pids != null && Pids.Count > 0)
            {
                if (Pids[0] != -1)
                {
                    pidString.Append("pids=");

                    foreach (int pid in Pids)
                    {
                        if (Pids.IndexOf(pid) != 0)
                            pidString.Append(",");
                        pidString.Append(pid);
                    }
                }
                else
                    pidString.Append("pids=all");

            }
            else
                pidString.Append("pids=none");

            switch (Version)
            {
                case "1.0":
                    return ("src=" + Source +
                        " fe=" + FrontEnd +
                        " level=" + Level +
                        " lock=" + SignalLock +
                        " quality=" + Quality +
                        " freq=" + Frequency +
                        " pol=" + Polarization +
                        " sys=" + System +
                        " type=" + Type +
                        " pilot=" + Pilot +
                        " ro=" + RollOff +
                        " sr=" + SymbolRate +
                        " fec=" + Fec +
                        " " + pidString.ToString());
                case "1.1":
                    return ("fe=" + FrontEnd +
                        " level=" + Level +
                        " lock=" + SignalLock +
                        " quality=" + Quality +
                        " freq=" + Frequency +
                        " bw=" + Bandwidth +
                        " sys=" + System +
                        " tmode=" + TransmissionMode +
                        " type=" + Type +
                        " gi=" + GuardInterval +
                        " fec=" + Fec +
                        " plp=" + Plp +
                        " t2id=" + SystemId +
                        " sm=" + SisoMiso +
                        " " + pidString.ToString());
                case "1.2":
                    return ("fe=" + FrontEnd +
                        " level=" + Level +
                        " lock=" + SignalLock +
                        " quality=" + Quality +
                        " freq=" + Frequency +
                        " bw=" + Bandwidth +
                        " sys=" + System +
                        " type=" + Type +
                        " sr=" + SymbolRate +
                        " c2tft=" + TuningFrequencyType +
                        " ds=" + DataSlice +
                        " plp=" + Plp +
                        " specinv=" + SpectrumInversion +
                        " " + pidString.ToString());
                default:
                    return ("src=" + Source +
                        " fe=" + FrontEnd +
                        " level=" + Level +
                        " lock=" + SignalLock +
                        " quality=" + Quality +
                        " freq=" + Frequency +
                        " pol=" + Polarization +
                        " sys=" + System +
                        " type=" + Type +
                        " pilot=" + Pilot +
                        " ro=" + RollOff +
                        " sr=" + SymbolRate +
                        " fec=" + Fec +
                        " " + pidString.ToString());
            }
        }
    }
}

