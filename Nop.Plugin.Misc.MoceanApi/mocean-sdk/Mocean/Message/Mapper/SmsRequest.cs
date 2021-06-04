using Newtonsoft.Json;

namespace Mocean.Message
{
    public class SmsRequest
    {
        [JsonProperty("mocean-from")]
        public string mocean_from { get; set; }

        [JsonProperty("mocean-to")]
        public string mocean_to { get; set; }

        [JsonProperty("mocean-text")]
        public string mocean_text { get; set; }

        [JsonProperty("mocean-udh")]
        public string mocean_udh { get; set; }

        [JsonProperty("mocean-coding")]
        public string mocean_coding { get; set; }

        [JsonProperty("mocean-dlr-mask")]
        public string mocean_dlr_mask { get; set; }

        [JsonProperty("mocean-dlr-url")]
        public string mocean_dlr_url { get; set; }

        [JsonProperty("mocean-schedule")]
        public string mocean_schedule { get; set; }

        [JsonProperty("mocean-mclass")]
        public string mocean_mclass { get; set; }

        [JsonProperty("mocean-alt-dcs")]
        public string mocean_alt_dcs { get; set; }

        [JsonProperty("mocean-charset")]
        public string mocean_charset { get; set; }

        [JsonProperty("mocean-validity")]
        public string mocean_validity { get; set; }

        [JsonProperty("mocean-medium")]
        public string mocean_medium { get; set; }

        [JsonProperty("mocean-resp-format")]
        public string mocean_resp_format { get; set; }
    }
}
