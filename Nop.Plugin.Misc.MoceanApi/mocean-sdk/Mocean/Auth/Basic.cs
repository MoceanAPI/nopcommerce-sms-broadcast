using Mocean.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mocean.Auth
{
    public class Basic : IAuth
    {
        private IDictionary<string, string> parameters;

        public string ApiKey { get => this.parameters["mocean-api-key"]; set => this.parameters["mocean-api-key"] = value; }
        public string ApiSecret { get => this.parameters["mocean-api-secret"]; set => this.parameters["mocean-api-secret"] = value; }

        public Basic()
        {
            this.parameters = new Dictionary<string, string>();
        }

        public Basic(string apiKey, string apiSecret) : this()
        {
            this.parameters["mocean-api-key"] = apiKey;
            this.parameters["mocean-api-secret"] = apiSecret;
        }

        public Basic(Credential credential) : this()
        {
            this.parameters = (IDictionary<string, string>)Utils.ConvertClassToDictionary(credential);
        }

        public string GetAuthMethod()
        {
            return "basic";
        }

        public IDictionary<string, string> GetParams()
        {
            return this.parameters;
        }
    }

    public class Credential
    {
        [JsonProperty("mocean-api-key")]
        public string ApiKey { get; set; }

        [JsonProperty("mocean-api-secret")]
        public string ApiSecret { get; set; }
    }
}
