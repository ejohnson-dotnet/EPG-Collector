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
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Reflection;

using Brotli;

namespace DomainObjects
{
    /// <summary>
    /// The class the encapsulates a HTTP request.
    /// </summary>
    public class WebRequestSpec
    {  
        /// <summary>
        /// Get the destination URL.
        /// </summary>
        public Uri DestinationUri { get; private set; }
        
        /// <summary>
        /// Get or set the redirection limit.
        /// </summary>
        public int RedirectionCount { get; set; }
        
        /// <summary>
        /// Get or set the request method.
        /// </summary>
        public string Method
        {
            get { return method; }
            set
            {
                method = value;
                httpWebRequest.Method = value;
            }
        }
        
        /// <summary>
        /// Get or set the request content type.
        /// </summary>
        public string ContentType
        {
            get { return contentType; }
            set
            {
                contentType = value;
                httpWebRequest.ContentType = value;
            }
        }

        /// <summary>
        /// Get or set the request content length.
        /// </summary>
        public long ContentLength
        {
            get { return contentLength; }
            set
            {
                contentLength = value;
                httpWebRequest.ContentLength = value;
            }
        }

        /// <summary>
        /// Get or set the request accept value.
        /// </summary>
        public string Accept
        {
            get { return accept; }
            set
            {
                accept = value;
                httpWebRequest.Accept = value;
            }
        }

        /// <summary>
        /// Get or set the user agent.
        /// </summary>
        public string UserAgent
        {
            get { return userAgent; }
            set
            {
                userAgent = value;
                httpWebRequest.UserAgent = value;
            }
        }

        /// <summary>
        /// Get or set the request timeout.
        /// </summary>
        public int Timeout
        {
            get { return timeout; }
            set
            {
                timeout = value;
                httpWebRequest.Timeout = value + 5000;
                SetHeader("Proxy-Timeout", value.ToString());
            }
        }

        /// <summary>
        /// Get the request stream.
        /// </summary>
        public Stream RequestStream { get { return httpWebRequest.GetRequestStream(); } }

        private Logger logger;
        private string loggerIdentity;

        private HttpWebRequest httpWebRequest;
        private int redirectionCountLeft;

        private string method;
        private string contentType;
        private long contentLength;
        private string accept;
        private string userAgent;
        private Collection<Tuple<string, string>> headers = new Collection<Tuple<string,string>>();
        private int timeout;

        private WebRequestSpec() { }

        /// <summary>
        /// Create an instance of the WebRequestSpec class with a specified URL.
        /// </summary>
        /// <param name="destinationUri">The URL.</param>
        public WebRequestSpec(string destinationUri)
        {
            method = "GET";
            accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/png,image/apng,application/json,*/*;q=0.8";
            userAgent = getUserAgent();
            headers.Add(Tuple.Create<string, string>("Accept-Encoding", "gzip, deflate, br"));
            headers.Add(Tuple.Create<string, string>("Accept-Language", "en-US,en;q=0.9"));
            timeout = 20000;

            initialize(destinationUri);

            RedirectionCount = 20;
            redirectionCountLeft = RedirectionCount;
        }

        /// <summary>
        /// Create an instance of the WebRequestSpec class with a specified URL and logger.
        /// </summary>
        /// <param name="destinationUri">The URL.</param>
        /// <param name="logger">A logger instance.</param>
        /// <param name="loggerIdentity">The log message prefix.</param>
        public WebRequestSpec(string destinationUri, Logger logger, string loggerIdentity) : this(destinationUri)
        {
            this.logger = logger;
            this.loggerIdentity = loggerIdentity;
        }

        /// <summary>
        /// Add or update a request header.
        /// </summary>
        /// <param name="headerName">The name of the header.</param>
        /// <param name="headerValue">The value of the header</param>
        public void SetHeader(string headerName, string headerValue)
        {
            httpWebRequest.Headers[headerName] = headerValue;

            if (headers == null)
                headers = new Collection<Tuple<string, string>>();

            foreach (Tuple<string, string> header in headers)
            {
                if (header.Item1 == headerName)
                {
                    headers.Remove(header);
                    break;
                }
            }

            headers.Add(Tuple.Create<string, string>(headerName, headerValue));
        }

        private void initialize(string destinationUri)
        {
            DestinationUri = new Uri(destinationUri);            

            forceCanonicalPathAndQuery(DestinationUri);

            UriBuilder uriBuilder = new UriBuilder(RunParameters.Instance.HttpProxy.ProxyUrl);
            uriBuilder.Port = RunParameters.Instance.HttpProxy.ProxyPort;  

            httpWebRequest = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);
            httpWebRequest.Method = Method;
            httpWebRequest.Host = DestinationUri.Host;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.UserAgent = UserAgent;
            httpWebRequest.AllowAutoRedirect = false;
            httpWebRequest.Accept = Accept;

            if (headers != null)
            {
                foreach (Tuple<string, string> header in headers)
                    httpWebRequest.Headers.Add(header.Item1, header.Item2);
            }

            httpWebRequest.Headers.Add("authority", DestinationUri.Host);
            httpWebRequest.Headers.Add("Actual-Destination", destinationUri);
            
            httpWebRequest.Headers.Add("Proxy-Timeout", timeout.ToString());
            httpWebRequest.Timeout = timeout + 5000;
        }

        private void forceCanonicalPathAndQuery(Uri uri)
        {
            string paq = uri.PathAndQuery;
            FieldInfo flagsFieldInfo = typeof(Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
            ulong flags = (ulong)flagsFieldInfo.GetValue(uri);
            flags &= ~((ulong)0x30);
            flagsFieldInfo.SetValue(uri, flags);
        }

        /// <summary>
        /// Process the web request.
        /// </summary>
        /// <returns>A WebReply instance describing the result of the request.</returns>
        public WebReply Process()
        {
            HttpWebResponse response = null;

            try
            {
                logMessage(loggerIdentity);
                logMessage(loggerIdentity);
                logMessage(loggerIdentity + " Sending to " + httpWebRequest.Address + " mode is " + httpWebRequest.Method);
                logMessage(loggerIdentity + " ==================== Headers Sent ====================");
                foreach (string headerName in httpWebRequest.Headers.AllKeys)
                    logMessage(loggerIdentity + " Header: " + headerName + " value: " + httpWebRequest.Headers[headerName]);

                if (!string.IsNullOrWhiteSpace(httpWebRequest.Host))
                    logMessage(loggerIdentity + " Host: " + httpWebRequest.Host);

                response = (HttpWebResponse)httpWebRequest.GetResponse();

                logMessage(loggerIdentity);
                logMessage(loggerIdentity + " ==== Headers Received (response code " + response.StatusCode + ") ====");
                foreach (string headerName in response.Headers.AllKeys)
                    logMessage(loggerIdentity + " Header: " + headerName + " value: " + response.Headers[headerName]);

                string location = response.Headers["Location"];
                if (location != null)
                {
                    logMessage(loggerIdentity + " Redirection response header to " + location);

                    response.Close();
                    response = null;

                    if (redirectionCountLeft == 0)
                    {
                        logMessage(loggerIdentity + " Redirection count exhausted - returning error reply");
                        return new WebReply("Redirection count exhausted", null, location);
                    }
                    else
                    {
                        string relocation;

                        if (location.StartsWith("http"))
                            relocation = location;
                        else
                            relocation = DestinationUri.Scheme + "://" + DestinationUri.Host + location;

                        logMessage(loggerIdentity + " Resetting request to " + relocation);
                        initialize(relocation);                        

                        redirectionCountLeft--;
                        logMessage(loggerIdentity + " Redirection count now " + redirectionCountLeft);

                        return Process();
                    }
                }

                byte[] responseData = new byte[0];
                string responseString = string.Empty;

                if (response.ContentLength != 0)
                {
                    Stream reader = response.GetResponseStream();

                    ReplyBase reply;

                    switch (response.ContentEncoding.ToLowerInvariant())
                    {
                        case "gzip":
                            reply = processGzipData(reader);
                            break;
                        case "deflate":
                            reply = processDeflateData(reader);
                            break;
                        case "br":
                        case "brotli":
                            reply = processBrotliData(reader);
                            break;
                        default:
                            reply = processUncompressedData(reader);
                            break;
                    }

                    if (reply.Message != null)
                        return new WebReply(reply.Message, null);

                    responseData = reply.ResponseData as byte[];

                    if (!string.IsNullOrWhiteSpace(response.ContentType))
                    {
                        if (response.ContentType.StartsWith("image"))
                            return new WebReply(null, responseData);
                        if (response.ContentType.ToLowerInvariant().Contains("application/octet"))
                            return new WebReply(null, responseData);
                        if (response.ContentType.StartsWith("application/zip"))
                            return new WebReply(null, responseData);
                    }

                    if (DestinationUri.ToString().EndsWith(".jpg") || DestinationUri.ToString().EndsWith(".png"))
                        return new WebReply(null, responseData);

                    responseString = Encoding.UTF8.GetString(responseData);
                    return new WebReply(null, responseString);
                }

                if (!string.IsNullOrWhiteSpace(response.ContentType) && response.ContentType.StartsWith("image"))
                    return new WebReply(null, responseString);
                else
                    return new WebReply(null, responseData);
            }
            catch (Exception e)
            {
                logMessage(loggerIdentity + " Request failed: " + e.Message);
                return new WebReply(e.Message, null);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        private ReplyBase processUncompressedData(Stream reader)
        {
            Collection<BlockSpec> blocks = new Collection<BlockSpec>();
            int readCount = -1;

            do
            {
                byte[] readBlock = new byte[16384];
                readCount = reader.Read(readBlock, 0, readBlock.Length);
                if (readCount != 0)
                    blocks.Add(new BlockSpec(readCount, readBlock));

            }
            while (readCount != 0);

            if (blocks.Count == 0)
                return new WebReply("No data received in stream", null);
            
            int totalLength = 0;

            foreach (BlockSpec blockSpec in blocks)
                totalLength += blockSpec.DataLength;

            byte[] responseData = new byte[totalLength];
            int index = 0;

            foreach (BlockSpec blockSpec in blocks)
            {
                Array.Copy(blockSpec.Data, 0, responseData, index, blockSpec.DataLength);
                index += blockSpec.DataLength;
            }

            return ReplyBase.DataReply(responseData);
        }

        private ReplyBase processGzipData(Stream reader)
        {
            GZipStream gzipStream = new GZipStream(reader, CompressionMode.Decompress);
            MemoryStream memoryStream = new MemoryStream();
            gzipStream.CopyTo(memoryStream);

            byte[] responseData = new byte[memoryStream.Length];
            memoryStream.Position = 0;
            memoryStream.Read(responseData, 0, responseData.Length);

            return ReplyBase.DataReply(responseData);
        }

        private ReplyBase processGzipData(byte[] data)
        {
            MemoryStream reader = new MemoryStream(data);            
            GZipStream gzipStream = new GZipStream(reader, CompressionMode.Decompress);
            MemoryStream memoryStream = new MemoryStream();
            gzipStream.CopyTo(memoryStream);

            byte[] responseData = new byte[memoryStream.Length];
            memoryStream.Position = 0;
            memoryStream.Read(responseData, 0, responseData.Length);

            return ReplyBase.DataReply(responseData);
        }

        private ReplyBase processDeflateData(Stream reader)
        {
            DeflateStream deflateStream = new DeflateStream(reader, CompressionMode.Decompress);
            MemoryStream memoryStream = new MemoryStream();
            deflateStream.CopyTo(memoryStream);

            byte[] responseData = new byte[memoryStream.Length];
            memoryStream.Position = 0;
            memoryStream.Read(responseData, 0, responseData.Length);

            return ReplyBase.DataReply(responseData);
        }

        private ReplyBase processBrotliData(Stream reader)
        {
            BrotliStream brotliStream = new BrotliStream(reader, CompressionMode.Decompress);
            MemoryStream memoryStream = new MemoryStream();
            brotliStream.CopyTo(memoryStream);

            byte[] responseData = new byte[memoryStream.Length];
            memoryStream.Position = 0;
            memoryStream.Read(responseData, 0, responseData.Length);

            return ReplyBase.DataReply(responseData);
        }

        private string getUserAgent()
        {
            return "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.119 Safari/537.36";            
        }

        private void logMessage(string message)
        {
            if (logger != null)
                logger.Write(message);
        }
    }

    internal class BlockSpec
    {
        internal int DataLength { get; private set; }
        internal byte[] Data { get; private set; }

        private BlockSpec() { }

        internal BlockSpec(int dataLength, byte[] data)
        {
            DataLength = dataLength;
            Data = data;
        }
    }
}

