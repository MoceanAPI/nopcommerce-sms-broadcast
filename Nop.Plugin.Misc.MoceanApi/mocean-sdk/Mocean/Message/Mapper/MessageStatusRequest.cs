using Newtonsoft.Json;

namespace Mocean.Message
{
    public class MessageStatusRequest
    {
        [JsonProperty("mocean-msgid")]
        public string mocean_msgid { get; set; }

        [JsonProperty("mocean-resp-format")]
        public string mocean_resp_format { get; set; }
    }
}
