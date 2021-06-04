using Newtonsoft.Json;

namespace Mocean.Account
{
    public class PricingRequest
    {
        [JsonProperty("mocean-resp-format")]
        public string mocean_resp_format { get; set; }

        [JsonProperty("mocean-type")]
        public string mocean_type { get; set; }

        [JsonProperty("mocean-mcc")]
        public string mocean_mcc { get; set; }

        [JsonProperty("mocean-mnc")]
        public string mocean_mnc { get; set; }

        [JsonProperty("mocean-delimiter")]
        public string mocean_delimiter { get; set; }
    }
}
