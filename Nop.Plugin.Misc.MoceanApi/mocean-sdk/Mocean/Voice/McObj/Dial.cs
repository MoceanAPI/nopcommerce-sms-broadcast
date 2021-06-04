using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice.McObj
{
    public class Dial : AbstractMc
    {
        public string To { set => this.requestData["to"] = value; get => this.requestData["to"].ToString(); }

        public string From { set => this.requestData["from"] = value; get => this.requestData["from"].ToString(); }

        public bool DialSequentially { set => this.requestData["dial-sequentially"] = value; get => (bool)this.requestData["dial-sequentially"]; }

        public Dial() : this(new Dictionary<string, object>())
        {
        }

        public Dial(Dictionary<string, object> parameter) : base(parameter)
        {
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "to"
            };
        }

        protected override string Action()
        {
            return "dial";
        }
    }
}
