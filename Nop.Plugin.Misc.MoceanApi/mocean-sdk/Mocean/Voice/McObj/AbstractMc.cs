using Mocean.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice.McObj
{
    abstract public class AbstractMc
    {
        protected Dictionary<string, object> requestData;

        protected AbstractMc(Dictionary<string, object> parameter)
        {
            this.requestData = parameter;
        }

        public Dictionary<string, object> GetRequestData()
        {
            foreach (String requiredKey in this.RequiredKey())
            {
                if (!this.requestData.ContainsKey(requiredKey))
                {
                    throw new RequiredFieldException(requiredKey + " is mandatory field, can't be empty. (" + this.GetType().Name + ")");
                }
            }

            this.requestData["action"] = this.Action();
            return this.requestData;
        }

        abstract protected List<string> RequiredKey();

        abstract protected string Action();
    }
}
