using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Mocean.Verify
{
    [XmlRoot("result")]
    public class VerifyCodeResponse : AbstractResponse
    {
        [JsonProperty("reqid")]
        [XmlElement("reqid")]
        public string ReqId { get; set; }
    }
}
