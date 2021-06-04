using System.Collections.Generic;

namespace Mocean.Account
{
    public class Balance : AbstractClient
    {
        public Balance(Client client, ApiRequest apiRequest) : base(client.Credentials, apiRequest)
        {
            this.requiredFields = new List<string> { "mocean-api-key", "mocean-api-secret" };
        }

        public BalanceResponse Inquiry(BalanceRequest balance = default(BalanceRequest))
        {
            this.ValidatedAndParseFields(balance);

            string responseStr = this.ApiRequest.Get("/account/balance", this.parameters);
            return (BalanceResponse)ResponseFactory.CreateObjectfromRawResponse<BalanceResponse>(responseStr)
                .SetRawResponse(this.ApiRequest.RawResponse);
        }
    }
}
