using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mocean.Voice.Mapper
{
    [XmlRoot("result")]
    public class VoiceResponse : AbstractResponse
    {
        [JsonProperty("calls")]
        [XmlArray("calls")]
        [XmlArrayItem("call")]
        public List<Call> Calls { get; set; }

        [XmlRoot("call")]
        public class Call
        {
            [JsonProperty("status")]
            [XmlElement("status")]
            public string Status { get; set; }

            [JsonProperty("receiver")]
            [XmlElement("receiver")]
            public string Receiver { get; set; }

            [JsonProperty("session-uuid")]
            [XmlElement("session-uuid")]
            public string SessionUuid { get; set; }

            [JsonProperty("call-uuid")]
            [XmlElement("call-uuid")]
            public string CallUuid { get; set; }
        }
    }
}
