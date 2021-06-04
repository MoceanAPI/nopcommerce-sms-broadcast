using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice.McObj
{
    public class Sleep : AbstractMc
    {
        public int Duration { set => this.requestData["duration"] = value; get => (int)this.requestData["duration"]; }

        public Sleep() : this(new Dictionary<string, object>())
        {
        }

        public Sleep(Dictionary<string, object> parameter) : base(parameter)
        {
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "duration"
            };
        }

        protected override string Action()
        {
            return "sleep";
        }
    }
}
