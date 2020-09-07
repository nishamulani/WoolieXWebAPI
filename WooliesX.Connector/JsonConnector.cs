using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Interface;

namespace WooliesX.Connector
{
    public enum HttpVerb
    {
        Get,
        Post,
        Put,
        Delete
    }
    
        public class JsonConnector : IJsonConnector
        {
            public string EndPoint { get; set; }
            public HttpVerb Method { get; set; }
            public string ContentType { get; set; }
            public string PostData { get; set; }

            public JsonConnector()
            {
                EndPoint = "";
                Method = HttpVerb.Get;
                ContentType = "text/xml";
                PostData = "";
            }
            public JsonConnector(string endpoint)
            {
                EndPoint = endpoint;
                Method = HttpVerb.Get;
                ContentType = "text/xml";
                PostData = "";
            }
            public JsonConnector(string endpoint, HttpVerb method)
            {
                EndPoint = endpoint;
                Method = method;
                ContentType = "text/xml";
                PostData = "";
            }

            public JsonConnector(string endpoint, HttpVerb method, string postData)
            {
                EndPoint = endpoint;
                Method = method;
                ContentType = "text/xml";
                PostData = postData;
            }

            public JsonConnector(string endpoint, HttpVerb method, string postData, string contentType)
            {
                EndPoint = endpoint;
                Method = method;
                ContentType = contentType;
                PostData = postData;
            }
            public string MakeRequest()
            {
                return MakeRequest("");
            }
            public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            }

            public string MakeRequest(string parameters)
            {
                var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

                request.Method = Method.ToString();
                request.ContentLength = 0;
                request.ContentType = ContentType;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };


                if (!string.IsNullOrEmpty(PostData) && Method == HttpVerb.Post)
                {
                    var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                    request.ContentLength = bytes.Length;

                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }

                try
                {
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        var responseValue = string.Empty;

                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            var message = $"Request failed. Received HTTP {response.StatusCode}";
                            throw new ApplicationException(message);
                        }

                        // grab the response
                        using (var responseStream = response.GetResponseStream())
                        {
                            if (responseStream != null)
                                using (var reader = new StreamReader(responseStream))
                                {
                                    responseValue = reader.ReadToEnd();
                                }
                        }

                        return responseValue;
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse response = (HttpWebResponse)ex.Response;
                    var message = $"Request failed. Received HTTP {response.StatusCode}";
                    throw new ApplicationException(message);
                }
            }
        }
    }
 
