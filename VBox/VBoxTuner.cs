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
using NetworkProtocols;

namespace VBox
{
    /// <summary>
    /// The class that describes a VBox server.
    /// </summary>
    public class VBoxTuner : Tuner
    {
        /// <summary>
        /// Get the server name.
        /// </summary>
        public override string Name { get; set; }
        /// <summary>
        /// Get the server address.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Get the tuner ID.
        /// </summary>
        public int TunerId { get; set; }
        /// <summary>
        /// Get the unique identification.
        /// </summary>
        public override string UniqueIdentity { get; set; }

        /// <summary>
        /// Get the number of DVB-S/S2 tuners.
        /// </summary>
        public int DvbsFrontEnds { get; set; }
        /// <summary>
        /// Get the number of DVB-T/T2 tuners.
        /// </summary>
        public int DvbtFrontEnds { get; set; }
        /// <summary>
        /// Get the number of DVB-C/C2 tuners.
        /// </summary>
        public int DvbcFrontEnds { get; set; }

        /// <summary>
        /// Returns true to indicate a server tuner.
        /// </summary>
        public override bool IsServerTuner { get { return (true); } }

        /// <summary>
        /// Returns false to indicate not a Sat>IP tuner.
        /// </summary>
        public override bool IsSatIpTuner { get { return (false); } }

        /// <summary>
        /// Returns true to indicate a VBox tuner.
        /// </summary>
        public override bool IsVBoxTuner { get { return (true); } }

        /// <summary>
        /// Get the tuner node that supports DVB-S. 
        /// </summary>
        public override TunerNode DVBSatelliteNode
        {
            get
            {
                if (DvbsFrontEnds == 0)
                    return (null);
                else
                {
                    return (new TunerNode(0, TunerNodeType.Satellite));
                }
            }
        }

        /// <summary>
        /// Get or set the flag that indicates the LNB parameters hve been set.
        /// </summary>
        public bool LnbSet { get; set; }

        /// <summary>
        /// Get the tuner node that supports DVB-T. 
        /// </summary>
        public override TunerNode DVBTerrestrialNode
        {
            get
            {
                if (DvbtFrontEnds == 0)
                    return (null);
                else
                {
                    return (new TunerNode(0, TunerNodeType.Terrestrial));
                }
            }
        }

        /// <summary>
        /// Get the tuner node that supports DVB-C. 
        /// </summary>
        public override TunerNode DVBCableNode
        {
            get
            {
                if (DvbcFrontEnds == 0)
                    return (null);
                else
                {
                    return (new TunerNode(0, TunerNodeType.Cable));
                }
            }
        }

        

        /// <summary>
        /// Initialize a new instance of the SatIpServer class.
        /// </summary>
        public VBoxTuner() : base(null) { }

        /// <summary>
        /// Check if the server supports a tuner type.
        /// </summary>
        /// <param name="checkTunerType">The tuner type.</param>
        /// <returns>True if the tuner type is supported; false otherwise.</returns>
        public override bool Supports(TunerType checkTunerType)
        {
            if (checkTunerType == TunerType.Satellite)
                return (DvbsFrontEnds != 0);

            if (checkTunerType == TunerType.Terrestrial)
                return (DvbtFrontEnds != 0);

            if (checkTunerType == TunerType.Cable)
                return (DvbcFrontEnds != 0);

            return (false);
        }

        /// <summary>
        /// Check if the server supports a tuner node type.
        /// </summary>
        /// <param name="checkTunerNodeType">The tuner node type.</param>
        /// <returns>True if the tuner type is supported; false otherwise.</returns>
        public override bool Supports(TunerNodeType checkTunerNodeType)
        {
            if (checkTunerNodeType == TunerNodeType.Satellite)
                return (DvbsFrontEnds != 0);

            if (checkTunerNodeType == TunerNodeType.Terrestrial)
                return (DvbtFrontEnds != 0);

            if (checkTunerNodeType == TunerNodeType.Cable)
                return (DvbcFrontEnds != 0);

            return (false);
        }

        /// <summary>
        /// Get a description of the server.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (Name);
        }

        /// <summary>
        /// Load all servers.
        /// </summary>
        public static void LoadServers()
        {
            VBoxController controller = new VBoxController();
            Collection<VBoxTuner> servers = new Collection<VBoxTuner>();

            /*VBoxTuner dummyTuner = new VBoxTuner();
            dummyTuner.Name = "Dummy1 VBox DVB-T";
            dummyTuner.UniqueIdentity = "zzzz-bbbb";
            dummyTuner.DvbtFrontEnds = 1;
            servers.Add(dummyTuner);*/

            ErrorSpec loadReply = VBoxController.LoadServers(servers);
            if (loadReply != null)
            {
                VBoxLogger.Instance.Write("Error: " + loadReply.ToString());
                return;
            }

            /*VBoxTuner dummyTuner = new VBoxTuner();
            dummyTuner.Name = "Dummy2 VBox DVB-T";
            dummyTuner.UniqueIdentity = "eeee-gggg";
            dummyTuner.DvbtFrontEnds = 1;
            servers.Add(dummyTuner);*/

            if (servers.Count == 0)
            {
                VBoxLogger.Instance.Write("No servers found");
                return;
            }

            foreach (VBoxTuner server in servers)
            {
                TunerCollection.Add(server);

                StringBuilder supports = new StringBuilder();

                if (server.DvbsFrontEnds != 0)
                    supports.Append(" (Satellite");

                if (server.DvbtFrontEnds != 0)
                {
                    if (supports.Length == 0)
                        supports.Append(" (Terrestrial");
                    else
                        supports.Append(", Terrestrial");
                }

                if (server.DvbcFrontEnds != 0)
                {
                    if (supports.Length == 0)
                        supports.Append(" (Cable");
                    else
                        supports.Append(", Cable");
                }

                supports.Append(")");

                Logger.Instance.Write("Found tuner " + TunerCollection.Count + ": " + server.Name + supports);
            }

            Logger.Instance.Write(" ");
        }
    }
}

