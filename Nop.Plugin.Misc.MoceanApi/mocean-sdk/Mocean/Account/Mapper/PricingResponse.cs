using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mocean.Account
{
    [XmlRoot("result")]
    public class PricingResponse : AbstractResponse
    {
        [JsonProperty("destinations")]
        [XmlArray("destinations")]
        [XmlArrayItem("destination")]
        public List<Destination> Destinations { get; set; }

        [XmlRoot("destination")]
        public class Destination
        {
            [JsonProperty("country")]
            [XmlElement("country")]
            public string Country { get; set; }

            [JsonProperty("operator")]
            [XmlElement("operator")]
            public string Operator { get; set; }

            [JsonProperty("mcc")]
            [XmlElement("mcc")]
            public string Mcc { get; set; }

            [JsonProperty("mnc")]
            [XmlElement("mnc")]
            public string Mnc { get; set; }

            [JsonProperty("price")]
            [XmlElement("price")]
            public string Price { get; set; }

            [JsonProperty("currency")]
            [XmlElement("currency")]
            public string Currency { get; set; }

            [JsonProperty("err_msg")]
            [XmlElement("err_msg")]
            public string ErrMsg { get; set; }
        }
    }
    
}
