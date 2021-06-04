using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Mocean.NumberLookup
{
    [XmlRoot("result")]
    public class NumberLookupResponse : AbstractResponse
    {
        [JsonProperty("msgid")]
        [XmlElement("msgid")]
        public string MsgId { get; set; }

        [JsonProperty("to")]
        [XmlElement("to")]
        public string To { get; set; }

        [JsonProperty("current_carrier")]
        [XmlElement("current_carrier")]
        public Carrier CurrentCarrier { get; set; }

        [JsonProperty("original_carrier")]
        [XmlElement("original_carrier")]
        public Carrier OriginalCarrier { get; set; }

        [JsonProperty("ported")]
        [XmlElement("ported")]
        public string Ported { get; set; }

        [JsonProperty("reachable")]
        [XmlElement("reachable")]
        public string Reachable { get; set; }

        public class Carrier
        {
            [JsonProperty("country")]
            [XmlElement("country")]
            public string Country { get; set; }

            [JsonProperty("name")]
            [XmlElement("name")]
            public string Name { get; set; }

            [JsonProperty("network_code")]
            [XmlElement("network_code")]
            public string NetworkCode { get; set; }

            [JsonProperty("mcc")]
            [XmlElement("mcc")]
            public string Mcc { get; set; }

            [JsonProperty("mnc")]
            [XmlElement("mnc")]
            public string Mnc { get; set; }
        }
    }
}
