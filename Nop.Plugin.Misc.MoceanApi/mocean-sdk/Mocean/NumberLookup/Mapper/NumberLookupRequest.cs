using Newtonsoft.Json;

namespace Mocean.NumberLookup
{
    public class NumberLookupRequest
    {
        [JsonProperty("mocean-to")]
        public string mocean_to { get; set; }

        [JsonProperty("mocean-nl-url")]
        public string mocean_nl_url { get; set; }

        [JsonProperty("mocean-resp-format")]
        public string mocean_resp_format { get; set; }
    }
}
