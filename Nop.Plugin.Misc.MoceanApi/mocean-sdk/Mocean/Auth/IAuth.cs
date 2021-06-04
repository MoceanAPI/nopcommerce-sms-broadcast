using System.Collections.Generic;

namespace Mocean.Auth
{
    public interface IAuth
    {
        string GetAuthMethod();

        IDictionary<string, string> GetParams();
    }
}
