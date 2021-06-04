using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Mocean
{
    [XmlRoot("result")]
    public class ErrorResponse : AbstractResponse
    {
        [JsonProperty("err_msg")]
        [XmlElement("err_msg")]
        public string ErrMsg { get; set; }
    }
}
