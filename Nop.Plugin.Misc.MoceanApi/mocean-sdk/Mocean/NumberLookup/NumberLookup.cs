using System.Collections.Generic;

namespace Mocean.NumberLookup
{
    public class NumberLookup : AbstractClient
    {
        public NumberLookup(Client client, ApiRequest apiRequest) : base(client.Credentials, apiRequest)
        {
            this.requiredFields = new List<string> { "mocean-api-key", "mocean-api-secret", "mocean-to" };
        }

        public NumberLookupResponse Inquiry(NumberLookupRequest numberLookup)
        {
            this.ValidatedAndParseFields(numberLookup);
            
            string responseStr = this.ApiRequest.Post("/nl", this.parameters);
            return (NumberLookupResponse)ResponseFactory.CreateObjectfromRawResponse<NumberLookupResponse>(responseStr)
                .SetRawResponse(this.ApiRequest.RawResponse);
        }
    }
}
