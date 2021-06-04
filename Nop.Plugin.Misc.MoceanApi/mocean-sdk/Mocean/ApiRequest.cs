using Mocean.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Mocean
{
    public class ApiRequest
    {
        public ApiRequestConfig ApiRequestConfig { get; set; }
        protected HttpClient httpClient { get; set; }

        public string RawResponse { get; private set; }

        public ApiRequest(ApiRequestConfig apiRequestConfig, HttpClient httpClient)
        {
            this.ApiRequestConfig = apiRequestConfig;
            this.httpClient = httpClient;
        }

        public ApiRequest(ApiRequestConfig apiRequestConfig) : this(apiRequestConfig, new HttpClient()) { }

        public ApiRequest(HttpClient httpClient) : this(ApiRequestConfig.make(), httpClient) { }

        public ApiRequest() : this(ApiRequestConfig.make()) { }

        public string Get(string uri, IDictionary<string, string> parameters)
        {
            return this.SendAndReturnDecodedBody("get", uri, parameters);
        }

        public string Post(string uri, IDictionary<string, string> parameters)
        {
            return this.SendAndReturnDecodedBody("post", uri, parameters);
        }

        public virtual string SendAndReturnDecodedBody(string method, string uri, IDictionary<string, string> parameters)
        {
            HttpResponseMessage response = this.Send(method, uri, parameters);
            return this.FormatResponse(response.Content.ReadAsStringAsync().Result, response.StatusCode, parameters["mocean-resp-format"].Equals("xml", StringComparison.CurrentCultureIgnoreCase), uri);
        }

        public virtual HttpResponseMessage Send(string method, string uri, IDictionary<string, string> parameters)
        {
            parameters["mocean-medium"] = "DOTNET-SDK";

            //use json if default not set
            if (!parameters.ContainsKey("mocean-resp-format"))
            {
                parameters["mocean-resp-format"] = "json";
            }

            HttpResponseMessage response;
            using (var tempHttpClient = this.httpClient)
            {
                if (method.Equals("get", StringComparison.CurrentCultureIgnoreCase))
                {
                    response = tempHttpClient.GetAsync(this.ApiRequestConfig.BaseUrl + "/rest/" + this.ApiRequestConfig.Version + uri + "?" + BuildQueryString(parameters)).Result;
                }
                else
                {
                    response = tempHttpClient.PostAsync(this.ApiRequestConfig.BaseUrl + "/rest/" + this.ApiRequestConfig.Version + uri, new FormUrlEncodedContent(parameters)).Result;
                }
            }

            return response;
        }

        public string FormatResponse(string responseString, HttpStatusCode responseCode, bool isXml, string uri)
        {
            this.RawResponse = responseString;
            var tempString = responseString;

            //remove these field for v1, no effect for v2
            tempString = tempString
                .Replace("<verify_request>", "")
                .Replace("</verify_request>", "")
                .Replace("<verify_check>", "")
                .Replace("</verify_check>", "");

            if (isXml && this.ApiRequestConfig.Version.Equals("1") && !string.IsNullOrEmpty(uri))
            {
                if (uri.Equals("/account/pricing", StringComparison.CurrentCultureIgnoreCase))
                {
                    tempString = tempString
                        .Replace("<data>", "<destinations>")
                        .Replace("</data>", "</destinations>");
                }
                else if (uri.Equals("/sms", StringComparison.CurrentCultureIgnoreCase))
                {
                    tempString = tempString
                        .Replace("<result>", "<result><messages>")
                        .Replace("</result>", "</messages></result>");
                }
            }

            return tempString;
        }

        public static StringBuilder BuildQueryString(IDictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            foreach (var kvp in parameters)
            {
                JsonConvert.SerializeObject(kvp.Key, Formatting.Indented);
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }

                sb.AppendFormat("{0}={1}", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value));
            }

            return sb;
        }
    }
}
