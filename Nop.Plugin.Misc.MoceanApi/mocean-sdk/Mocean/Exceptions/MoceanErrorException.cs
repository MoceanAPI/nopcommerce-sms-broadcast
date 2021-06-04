using System;

namespace Mocean.Exceptions
{
    public class MoceanErrorException : Exception
    {
        public ErrorResponse ErrorResponse { get; private set; }

        public MoceanErrorException(string errMsg) : base(errMsg)
        {
        }

        public MoceanErrorException(ErrorResponse errorResponse) : base(errorResponse.RawResponse)
        {
            this.ErrorResponse = errorResponse;
        }
    }
}
