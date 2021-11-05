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

namespace WMCUtility
{
    class Program
    {
        private static string AssemblyVersion
        {
            get
            {
                System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
                return (version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision);
            }
        }

        private const string exportIdentity = "EXPORTDATA";
        private const string removeAffiliatesIdentity = "REMOVEAFFILIATES";
        private const string removeChannelLogosIdentity = "REMOVECHANNELLOGOS";
        private const string disableIdentity = "DISABLEGUIDELOADER";
        private const string clearGuideIdentity = "CLEARGUIDE";
        private const string tidyChannelsIdentity = "TIDYCHANNELS";

        internal static int OKExitCode = 0;
        internal static int ErrorExitCode = 17;

        static void Main(string[] args)
        {
            Logger.Instance.Write("Windows Media Center Utility build: " + AssemblyVersion);

            if (args == null || args.Length == 0)
            {
                Logger.Instance.Write("Windows Media Center Utility started with incorrect command line");
                Environment.Exit(ErrorExitCode);
            }

            string argsString = args[0];
            for (int index = 1; index < args.Length; index++)
                argsString += " " + args[index];
            Logger.Instance.Write("Windows Media Center Utility command line: " + argsString);

            if (args.Length != 1)
            {
                Logger.Instance.Write("Windows Media Center Utility started with incorrect command line - too many parameters");
                Environment.Exit(ErrorExitCode);
            }

            int exitCode;

            if (!args[0].StartsWith(removeAffiliatesIdentity))
            {
                switch (args[0])
                {
                    case exportIdentity:
                        exitCode = UtilityControl1.Export();
                        break;
                    case removeChannelLogosIdentity:
                        exitCode = UtilityControl1.RemoveChannelLogos();
                        break;
                    case disableIdentity:
                        exitCode = UtilityControl1.DisableGuideLoader();
                        break;
                    case clearGuideIdentity:
                        exitCode = UtilityControl1.ClearGuide();
                        break;
                    case tidyChannelsIdentity:
                        exitCode = UtilityControl1.TidyChannels();
                        break;
                    default:
                        Logger.Instance.Write("Windows Media Center Utility started with unknown command line: " + args[0]);
                        exitCode = ErrorExitCode;
                        break;
                }
            }
            else
            {
                string[] parts = args[0].Split(new char[] { '=' });
                if (parts.Length != 2)
                {
                    Logger.Instance.Write("Windows Media Center Utility started with incorrect command line");
                    Environment.Exit(ErrorExitCode);
                }

                exitCode = UtilityControl1.RemoveAffiliates(parts[1].Trim());
            }

            Logger.Instance.Write("Windows Media Center Utility exiting - reply code " + exitCode);
            Environment.Exit(exitCode);
        }        
    }
}

