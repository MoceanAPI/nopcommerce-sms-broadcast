using Mocean.Voice.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public class Voice : AbstractClient
    {
        public Voice(Client client, ApiRequest apiRequest) : base(client.Credentials, apiRequest)
        {
            this.requiredFields = new List<string> { "mocean-api-key", "mocean-api-secret", "mocean-to" };
        }

        public VoiceResponse Call(VoiceRequest voice)
        {
            this.ValidatedAndParseFields(voice);

            string responseStr = this.ApiRequest.Post("/voice/dial", this.parameters);
            return (VoiceResponse)ResponseFactory.CreateObjectfromRawResponse<VoiceResponse>(responseStr)
                .SetRawResponse(this.ApiRequest.RawResponse);
        }

        public HangupResponse Hangup(string callUuid)
        {
            this.requiredFields = new List<string> { "mocean-api-key", "mocean-api-secret", "mocean-call-uuid" };

            this.parameters["mocean-call-uuid"] = callUuid;
            this.ValidatedAndParseFields();

            string responseStr = this.ApiRequest.Post("/voice/hangup", this.parameters);
            return (HangupResponse)ResponseFactory.CreateObjectfromRawResponse<HangupResponse>(responseStr)
                .SetRawResponse(this.ApiRequest.RawResponse);
        }

        public RecordingResponse Recording(string callUuid)
        {
            this.requiredFields = new List<string> { "mocean-api-key", "mocean-api-secret", "mocean-call-uuid" };

            this.parameters["mocean-call-uuid"] = callUuid;
            this.ValidatedAndParseFields();

            string uri = "/voice/rec";

            HttpResponseMessage response = this.ApiRequest.Send("GET", uri, this.parameters);

            string contentHeader = response.Content.Headers.TryGetValues("Content-Type", out var value) ? value.FirstOrDefault() : null;
            if ("audio/mpeg".Equals(contentHeader))
            {
                byte[] byteBody = response.Content.ReadAsByteArrayAsync().Result;
                response.Dispose();

                return new RecordingResponse(callUuid + ".mp3", byteBody);
            }

            //this method will throw exception if there's error
            this.ApiRequest.FormatResponse(response.Content.ReadAsStringAsync().Result, response.StatusCode, this.parameters["mocean-resp-format"].Equals("xml", StringComparison.CurrentCultureIgnoreCase), uri);
            response.Dispose();
            return null;
        }
    }
}
