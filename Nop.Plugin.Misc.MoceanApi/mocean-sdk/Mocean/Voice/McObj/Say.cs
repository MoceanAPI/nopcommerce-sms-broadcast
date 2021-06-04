using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice.McObj
{
    public class Say : AbstractMc
    {
        public string Language { set => this.requestData["language"] = value; get => this.requestData["language"].ToString(); }
        public string Text { set => this.requestData["text"] = value; get => this.requestData["text"].ToString(); }
        public bool BargeIn { set => this.requestData["barge-in"] = value; get => (bool)this.requestData["barge-in"]; }

        public bool ClearDigitCache { set => this.requestData["clear-digit-cache"] = value; get => (bool)this.requestData["clear-digit-cache"]; }

        public Say() : this(new Dictionary<string, object>())
        {
        }

        public Say(Dictionary<string, object> parameter) : base(parameter)
        {
            // default value
            this.requestData["language"] = this.requestData.ContainsKey("language") ? this.requestData["language"] : "en-US";
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "text", "language"
            };
        }

        protected override string Action()
        {
            return "say";
        }
    }
}
