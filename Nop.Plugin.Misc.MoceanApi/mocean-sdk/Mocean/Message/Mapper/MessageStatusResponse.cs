using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Mocean.Message
{
    [XmlRoot("result")]
    public class MessageStatusResponse : AbstractResponse
    {
        [JsonProperty("message_status")]
        [XmlElement("message_status")]
        public string MessageStatus { get; set; }

        [JsonProperty("msgid")]
        [XmlElement("msgid")]
        public string MsgId { get; set; }

        [JsonProperty("credit_deducted")]
        [XmlElement("credit_deducted")]
        public string CreditDeducted { get; set; }
    }
}
