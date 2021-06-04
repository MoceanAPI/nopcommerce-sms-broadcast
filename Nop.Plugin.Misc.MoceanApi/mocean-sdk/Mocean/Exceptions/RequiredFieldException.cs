namespace Mocean.Exceptions
{
    public class RequiredFieldException : MoceanErrorException
    {
        public RequiredFieldException(string errMsg) : base(errMsg)
        {

        }
    }
}
