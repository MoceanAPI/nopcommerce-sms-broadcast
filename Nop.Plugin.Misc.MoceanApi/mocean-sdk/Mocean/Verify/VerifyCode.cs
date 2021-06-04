using System.Collections.Generic;

namespace Mocean.Verify
{
    public class VerifyCode : AbstractClient
    {
        public VerifyCode(Client client, ApiRequest apiRequest) : base(client.Credentials, apiRequest)
        {
            this.requiredFields = new List<string> { "mocean-api-key", "mocean-api-secret", "mocean-reqid", "mocean-code" };
        }

        public VerifyCodeResponse Send(VerifyCodeRequest verifyCode)
        {
            this.ValidatedAndParseFields(verifyCode);

            string responseStr = this.ApiRequest.Post("/verify/check", this.parameters);
            return (VerifyCodeResponse)ResponseFactory.CreateObjectfromRawResponse<VerifyCodeResponse>(responseStr)
                .SetRawResponse(this.ApiRequest.RawResponse);
        }
    }
}
