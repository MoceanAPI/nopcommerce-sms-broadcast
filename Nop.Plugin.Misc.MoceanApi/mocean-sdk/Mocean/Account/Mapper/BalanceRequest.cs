using Newtonsoft.Json;

namespace Mocean.Account
{
    public class BalanceRequest
    {
        [JsonProperty("mocean-resp-format")]
        public string mocean_resp_format { get; set; }
    }
}
