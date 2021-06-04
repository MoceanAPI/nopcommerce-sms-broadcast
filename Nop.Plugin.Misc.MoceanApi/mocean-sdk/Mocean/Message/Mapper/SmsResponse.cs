using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Mocean.Message
{
    [XmlRoot("result")]
    public class SmsResponse : AbstractResponse
    {
        [JsonProperty("messages")]
        [XmlArray("messages")]
        [XmlArrayItem("message")]
        public List<Message> Messages { get; set; }

        [XmlRoot("message")]
        public class Message
        {
            [JsonProperty("status")]
            [XmlElement("status")]
            public string Status { get; set; }

            [JsonProperty("receiver")]
            [XmlElement("receiver")]
            public string Receiver { get; set; }

            [JsonProperty("msgid")]
            [XmlElement("msgid")]
            public string MsgId { get; set; }

            [JsonProperty("err_msg")]
            [XmlElement("err_msg")]
            public string ErrMsg { get; set; }
        }
    }
}
