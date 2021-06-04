using Mocean.Auth;
using Mocean.Exceptions;
using System.Collections.Generic;

namespace Mocean
{
    abstract public class AbstractClient
    {
        protected IDictionary<string, string> parameters;
        protected List<string> requiredFields;
        protected IAuth credentials;
        protected ApiRequest ApiRequest { get; private set; }

        protected AbstractClient(IAuth credentials, ApiRequest apiReqest)
        {
            this.credentials = credentials;
            this.ApiRequest = apiReqest;
            this.parameters = new Dictionary<string, string>();
            this.requiredFields = new List<string>();
        }

        protected void ValidatedAndParseFields(object inputParameters = null)
        {
            if (inputParameters != null)
            {
                this.parameters = Utils.ConvertClassToDictionary(inputParameters);
            }

            this.PutCredentials();

        }

        private void PutCredentials()
        {
            this.parameters["mocean-api-key"] = this.credentials.GetParams()["mocean-api-key"];
            this.parameters["mocean-api-secret"] = this.credentials.GetParams()["mocean-api-secret"];
        }
    }
}
