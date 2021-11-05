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
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace DomainObjects
{
    /// <summary>
    /// The class that controls the HTTP proxy process.
    /// </summary>
    public class HttpProxy
    {
        /// <summary>
        /// Get the proxy url.
        /// </summary>
        public string ProxyUrl { get; private set; }
        /// <summary>
        /// Get the proxy port.
        /// </summary>
        public int ProxyPort { get; private set; }

        private Process httpProxyProcess;
        private bool proxyInitialized;

        private string httpConfigFile { get { return Path.Combine(RunParameters.DataDirectory, "Http Proxy.cfg"); } }
        private string proxyUrlLine = "ProxyUrl=";
        private string proxyPortLine = "ProxyPort=";
        private bool proxyChanged;        

        /// <summary>
        /// Initialize a new instance of the HttpProxy class.
        /// </summary>
        public HttpProxy() { }

        /// <summary>
        /// Initialize the instance.
        /// </summary>
        /// <returns></returns>
        public string Initialize()
        {
            if (proxyInitialized)
                return null;

            if (RunParameters.IsWinXp)
            {
                string configReply = getProxyConfig();
                if (configReply != null)
                    return configReply;

                Logger.Instance.Write("HTTP Proxy config: " + ProxyUrl + ":" + ProxyPort);

                proxyInitialized = true;
                return null;
            }

            httpProxyProcess = new Process();

            httpProxyProcess.StartInfo.FileName = Path.Combine(RunParameters.BaseDirectory, "HttpProxy.exe");
            httpProxyProcess.StartInfo.Arguments = "/logpath=\"" + RunParameters.DataDirectory + "\"";
            httpProxyProcess.StartInfo.WorkingDirectory = RunParameters.BaseDirectory;
            httpProxyProcess.StartInfo.UseShellExecute = false;
            httpProxyProcess.StartInfo.CreateNoWindow = true;
            httpProxyProcess.StartInfo.RedirectStandardOutput = true;
            httpProxyProcess.EnableRaisingEvents = true;
            httpProxyProcess.OutputDataReceived += new DataReceivedEventHandler(httpProxyProcessOutputDataReceived);
            httpProxyProcess.Exited += new EventHandler(httpProxyProcessExited);

            httpProxyProcess.Start();

            httpProxyProcess.BeginOutputReadLine();

            int retries = 0;
            while (!proxyInitialized)
            {
                retries++;
                if (retries > 100)
                    return ("Failed to initialize HTTP proxy");

                Thread.Sleep(100);
            }

            ProxyUrl = "127.0.0.1";

            return null;
        }

        /// <summary>
        /// Close the proxy process.
        /// </summary>
        public void Close()
        {
            if (httpProxyProcess != null)
            {
                try
                {
                    httpProxyProcess.Kill();
                }
                catch (Exception e) 
                {
                    Logger.Instance.Write("Failed to close HTTP proxy: " + e.Message);
                }
            }
        }

        private string getProxyConfig()
        {
            FileStream fileStream = null;

            try { fileStream = new FileStream(httpConfigFile, FileMode.Open, FileAccess.Read); }
            catch (IOException)
            {
                return "Failed to open " + httpConfigFile;                
            }

            StreamReader streamReader = new StreamReader(fileStream);
                
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                if (line.StartsWith(proxyUrlLine))
                    ProxyUrl = line.Substring(proxyUrlLine.Length).Trim();
                else
                {
                    if (line.StartsWith(proxyPortLine))
                    {
                        string proxyPort = line.Substring(proxyPortLine.Length);
                        try
                        {
                            ProxyPort = Int32.Parse(proxyPort);
                        }
                        catch (FormatException) { }
                        catch (OverflowException) { }
                    }
                    else
                    {
                    }
                }
            }

            streamReader.Close();
            fileStream.Close();

            return null;
        }

        private void httpProxyProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;

            if (!e.Data.ToLowerInvariant().Contains("client") && !e.Data.ToLowerInvariant().Contains("listening"))
                Logger.Instance.Write("HTTP proxy message: " + e.Data);

            string lowerCaseData = e.Data.ToLowerInvariant();

            if (e.Data.ToLowerInvariant().Contains("starting listener"))
            {
                string urlString = " for ";
                string portString = " port ";

                int startIndex = lowerCaseData.IndexOf(urlString);
                if (startIndex == -1)
                    return;

                startIndex += urlString.Length;

                int endUrlIndex = lowerCaseData.IndexOf(portString);
                if (endUrlIndex == -1)
                    return;

                string proxyUrl = lowerCaseData.Substring(startIndex, endUrlIndex - startIndex);

                startIndex = lowerCaseData.IndexOf(portString);
                if (startIndex == -1)
                    return;

                startIndex += portString.Length;
                int proxyPort = 0;

                while (startIndex < lowerCaseData.Length && Char.IsNumber(lowerCaseData[startIndex]))
                {
                    proxyPort = (proxyPort * 10) + (lowerCaseData[startIndex] - '0');
                    startIndex++;
                }

                if (ProxyUrl == null || (proxyUrl != ProxyUrl || proxyPort != ProxyPort))
                {
                    ProxyUrl = proxyUrl;
                    ProxyPort = proxyPort;
                    proxyChanged = true;
                }
                else
                    proxyChanged = false;

                return;
            }

            if (e.Data.ToLowerInvariant().Contains("listening"))
            {
                if (proxyChanged)
                {                    
                    Logger.Instance.Write("Proxy initialized for address " + ProxyUrl + ":" + ProxyPort);
                    Logger.Instance.Write("HTTP proxy message: " + e.Data);
                }

                proxyChanged = false;
                Thread.Sleep(1000);

                proxyInitialized = true;
                return;
            }
        }

        private void httpProxyProcessExited(object sender, EventArgs e)
        {
            Logger.Instance.Write("HTTP proxy has exited with exit code " + httpProxyProcess.ExitCode);
            httpProxyProcess.Close();

            httpProxyProcess = null;
        }
    }
}

