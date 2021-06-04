using System.Collections.Generic;

namespace Mocean.Message
{
    public class MessageStatus : AbstractClient
    {
        public MessageStatus(Client client, ApiRequest apiRequest) : base(client.Credentials, apiRequest)
        {
            this.requiredFields = new List<string> { "mocean-api-key", "mocean-api-secret", "mocean-msgid" };
        }

        public MessageStatusResponse Inquiry(MessageStatusRequest messageStatusRequest)
        {
            this.ValidatedAndParseFields(messageStatusRequest);

            string responseStr = this.ApiRequest.Get("/report/message", this.parameters);
            return (MessageStatusResponse)ResponseFactory.CreateObjectfromRawResponse<MessageStatusResponse>(responseStr)
                .SetRawResponse(this.ApiRequest.RawResponse);
        }
    }
}
