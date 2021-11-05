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
using System.Reflection;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections.ObjectModel;

using DomainObjects;
using NetworkProtocols;

namespace VBox
{
    /// <summary>
    /// The class the defines the VBox requests.
    /// </summary>
    public sealed class VBoxApi
    {
        /// <summary>
        /// Get the protocol ID.
        /// </summary>
        public static string ProtocolId { get { return ("Http"); } }

        /// <summary>
        /// Get or set the software version info.
        /// </summary>
        public static VBoxQuerySwVersionResponse SoftwareVersion { get; set; }

        private static bool useUnsafeSet;
        private static string update1 = "2.66.9";

        private VBoxApi() { }

        /// <summary>
        /// Call the QuerySwVersion API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <returns>A VBox response instance containing the reply.</returns>
        public static VBoxQuerySwVersionResponse QuerySoftwareVersion(string address)
        {
            VBoxQuerySwVersionResponse response = new VBoxQuerySwVersionResponse();
            sendReceive(address, "QuerySwVersion", response);

            return (response);
        }

        /// <summary>
        /// Call the RegisterTuning API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <param name="tunerId">The identity of the tuner.</param>
        /// <returns>A VBox response instance containing the reply.</returns>
        public static VBoxResponse RegisterTuning(string address, int tunerId)
        {
            VBoxResponse response = new VBoxResponse();

            if (SoftwareVersion.CompareSoftwareVersions(update1) < 0)
                sendReceive(address, "RegisterNetworkTuning", response);
            else
                sendReceive(address, "BlockStreamerUseTuner&TunerID=" + tunerId, response);

            return (response);
        }

        /// <summary>
        /// Call the UnregisterTuning API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <param name="tunerId">The identity of the tuner.</param>
        /// <returns>A VBox response instance containing the reply.</returns>
        public static VBoxResponse UnregisterTuning(string address, int tunerId)
        {
            VBoxResponse response = new VBoxResponse();
            
            if (SoftwareVersion.CompareSoftwareVersions(update1) < 0)
                sendReceive(address, "UnregisterNetworkTuning", response);
            else
                sendReceive(address, "UnblockStreamerUseTuner&TunerID=" + tunerId, response);

            return (response);
        }

        /// <summary>
        /// Call the QueryNumOfTuners API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <returns>A VBox response instance containing the reply.</returns>
        public static VBoxQueryNumOfTunersResponse GetCountOfTuners(string address)
        {
            VBoxQueryNumOfTunersResponse response = new VBoxQueryNumOfTunersResponse();
            sendReceive(address, "QueryNumOfTuners", response);

            return (response);
        }

        /// <summary>
        /// Call the QueryTunerType API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <param name="tunerId">The identity of the tuner.</param>
        /// <returns>A VBox response instance containing the reply.</returns>
        public static VBoxQueryTunerTypeResponse GetTunerType(string address, int tunerId)
        {
            VBoxQueryTunerTypeResponse response = new VBoxQueryTunerTypeResponse();
            sendReceive(address, "QueryTunerType&TunerID=" + tunerId, response);

            return (response);
        }

        /// <summary>
        /// Call the SetSatLnb API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <param name="tunerId">The identity of the tuner.</param>
        /// <param name="type">The type of LNB (SINGLE/DUAL).</param>
        /// <param name="lowLnb">The LNB low value.</param>
        /// <param name="highLnb">The LNB high value.</param>
        /// <returns>A VBox response instance containing the reply.</returns>
        public static VBoxResponse SetSatLnb(string address, int tunerId, string type, int lowLnb, int highLnb)
        {
            VBoxResponse response = new VBoxResponse();
            if (lowLnb != highLnb)
                sendReceive(address, "SetSatLNB" +
                    "&TunerID=" + tunerId + 
                    "&LNBType=" + type + 
                    "&LowLNB=" + (((double)lowLnb) / 1000000) + 
                    "&HighLNB=" + (((double)highLnb) / 1000000), response);
            else
                sendReceive(address, "SetSatLNB" +
                    "&TunerID=" + tunerId +
                    "&LNBType=" + type +
                    "&LowLNB=" + (((double)lowLnb) / 1000000), response);

            return (response);
        }

        /// <summary>
        /// Call the SetFrequency API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <param name="tunerId">The identity of the tuner.</param>
        /// <param name="tuningParameters">The tuning parameters.</param>
        public static VBoxResponse SetFrequency(string address, int tunerId, VBoxTuningParameters tuningParameters)
        {
            VBoxResponse response = new VBoxResponse();
            sendReceive(address, "SetFrequency" +
                "&TunerID=" + tunerId + tuningParameters.ToString(), response);

            return (response);
        }

        /// <summary>
        /// Call the QueryLockStatus API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <param name="tunerId">The identity of the tuner.</param>
        /// <returns>A VBox response instance containing the reply.</returns>
        public static VBoxQueryLockStatusResponse QueryLockStatus(string address, int tunerId)
        {
            VBoxQueryLockStatusResponse response = new VBoxQueryLockStatusResponse();
            
            sendReceive(address, "QueryLockStatus" +
                "&TunerID=" + tunerId, response);

            return (response);
        }

        /// <summary>
        /// Call the OpenMuxStream API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <param name="tunerId">The identity of the tuner.</param>
        /// <param name="muxId">The identity of the mux (1-8).</param>
        /// <param name="pids">A list of pids to begin receiving.</param>
        /// <returns>A VBox response instance containing the reply.</returns>
        public static VBoxOpenMuxStreamResponse OpenMuxStream(string address, int tunerId, int muxId, int[] pids)
        {
            VBoxOpenMuxStreamResponse response = new VBoxOpenMuxStreamResponse();
            sendReceive(address, "OpenMuxStream" +                
                "&MuxID=" + muxId +
                "&TunerID=" + tunerId +
                "&PIDList=" + getPidString(pids), response);

            return (response);
        }

        /// <summary>
        /// Call the CloseMuxStream API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <param name="tunerId">The identity of the tuner.</param>
        /// <param name="muxId">The identity of the mux.</param>
        public static VBoxResponse CloseMuxStream(string address, int tunerId, int muxId)
        {
            VBoxResponse response = new VBoxResponse();
            sendReceive(address, "CloseMuxStream" + 
                "&TunerID=" + (tunerId == -1 ? "All" : tunerId.ToString()) +
                "&MuxID=" + (muxId == -1 ? "All" : muxId.ToString()), response);

            return (response);
        }

        /// <summary>
        /// Call the AddPidsToMuxStream API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <param name="tunerId">The identity of the tuner.</param>
        /// <param name="muxId">The identity of the mux.</param>
        /// <param name="pids">The list of pids to add.</param>
        /// <returns>A VBox response instance containing the reply.</returns>
        public static VBoxResponse AddPidsToMuxStream(string address, int tunerId, int muxId, int[] pids)
        {
            VBoxResponse response = new VBoxResponse();
            sendReceive(address, "AddPidsToMuxStream" +
                "&TunerID=" + tunerId +
                "&MuxID=" + muxId +
                "&PIDList=" + getPidString(pids), response);

            return (response);
        }

        /// <summary>
        /// Call the RemovePidsFromMuxStream API.
        /// </summary>
        /// <param name="address">The IP address of the VBox.</param>
        /// <param name="tunerId">The identity of the tuner.</param>
        /// <param name="muxId">The identity of the mux.</param>
        /// <param name="pids">The list of pids to remove. If null or zero entries all pids will be removed</param>
        /// <returns>A VBox response instance containing the reply.</returns>
        public static VBoxResponse RemovePidsFromMuxStream(string address, int tunerId, int muxId, int[] pids)
        {
            VBoxResponse response = new VBoxResponse();
            
            sendReceive(address, "RemovePidsFromMuxStream" +
                "&TunerID=" + tunerId +
                "&MuxID=" + muxId +
                "&PIDList=" + getPidString(pids), response);

            return (response);
        }

        private static void sendReceive(string address, string request, VBoxResponse response)
        {
            if (!setAllowUnsafeHeaderParsing())
            {
                VBoxLogger.Instance.Write("VBox: Failed to set unsafe header parsing");
                response.ErrorCode = -1;
                response.ErrorDescription = "Failed to set unsafe header parsing";
            }

            WebResponse webResponse = null;

            try
            {
                VBoxLogger.Instance.Write("VBox request: Address: " + address + " Request: " + request);
                
                WebRequest webRequest = WebRequest.Create(@"http://" + address + @"/cgi-bin/HttpControl/HttpControlApp?OPTION=1&Method=" + request);
                webRequest.ContentType = "text/xml";
                webRequest.Timeout = 180000;
                ((HttpWebRequest)webRequest).UserAgent = "EPG Collector";
                ((HttpWebRequest)webRequest).KeepAlive = false;  

                webResponse = (HttpWebResponse)webRequest.GetResponse();

                Stream receiveStream = webResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

                StreamReader readStream = new StreamReader(receiveStream, encode);
                XmlReader reader = XmlReader.Create(readStream);

                response.Process(reader);
                VBoxLogger.Instance.Write("VBox response: " + response.ToString());
                webResponse.Close();
            }
            catch (WebException e)
            {
                VBoxLogger.Instance.Write("An exception of type " + e.GetType().Name + " has occurred while communicating with the VBox server");
                VBoxLogger.Instance.Write("<e> " + e.Message);

                if (webResponse != null)
                    webResponse.Close();

                response.ErrorCode = (int)e.Status;
                response.ErrorDescription = e.Message;
            }
            catch (Exception e)
            {
                VBoxLogger.Instance.Write("An exception of type " + e.GetType().Name + " has occurred while communicating with the VBox server");
                VBoxLogger.Instance.Write("<e> " + e.Message);

                if (webResponse != null)
                    webResponse.Close();

                response.ErrorCode = -1;
                response.ErrorDescription = e.Message;
            }
        }

        private static bool setAllowUnsafeHeaderParsing()
        {
            if (useUnsafeSet)
                return (true);

            useUnsafeSet = true;

            Assembly assembly = Assembly.GetAssembly(typeof(System.Net.Configuration.SettingsSection));
            if (assembly == null)
                return (false);

            Type settingsType = assembly.GetType("System.Net.Configuration.SettingsSectionInternal");
            if (settingsType == null)
                return (false);

            object instance = settingsType.InvokeMember("Section", BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.NonPublic, null, null, new object[] { });
            if (instance == null)
                return (false);

            FieldInfo useUnsafeHeaderParsing = settingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic | BindingFlags.Instance);
            if (useUnsafeHeaderParsing == null)
                return (false);

            useUnsafeHeaderParsing.SetValue(instance, true);

            return true;
        }

        private static string getPidString(int[] pids)
        {
            if (pids == null || pids.Length == 0 || pids[0] == -1)
                return ("All");

            StringBuilder pidString = new StringBuilder();

            foreach (int pid in pids)
            {
                if (pidString.Length != 0)
                    pidString.Append(",");
                pidString.Append(pid);
            }

            return (pidString.ToString());
        }
    }
}

