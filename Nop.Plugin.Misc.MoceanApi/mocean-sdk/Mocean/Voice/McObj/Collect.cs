using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice.McObj
{
    public class Collect : AbstractMc
    {
        public string EventUrl { set => this.requestData["event-url"] = value; get => this.requestData["event-url"].ToString(); }
        public int Min { set => this.requestData["min"] = value; get => (int)this.requestData["min"]; }
        public int Max { set => this.requestData["max"] = value; get => (int)this.requestData["max"]; }
        public string Terminators { set => this.requestData["terminators"] = value; get => this.requestData["terminators"].ToString(); }
        public int Timeout { set => this.requestData["timeout"] = value; get => (int)this.requestData["timeout"]; }

        public Collect() : this(new Dictionary<string, object>())
        {
        }

        public Collect(Dictionary<string, object> parameter) : base(parameter)
        {
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "event-url", "min", "max", "timeout"
            };
        }

        protected override string Action()
        {
            return "collect";
        }
    }
}
