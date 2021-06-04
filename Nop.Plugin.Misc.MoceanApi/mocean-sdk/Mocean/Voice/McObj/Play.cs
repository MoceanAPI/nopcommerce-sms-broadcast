using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice.McObj
{
    public class Play : AbstractMc
    {
        public string File { set => this.requestData["file"] = value; get => this.requestData["file"].ToString(); }

        public bool BargeIn { set => this.requestData["barge-in"] = value; get => (bool)this.requestData["barge-in"]; }

        public bool ClearDigitCache { set => this.requestData["clear-digit-cache"] = value; get => (bool)this.requestData["clear-digit-cache"]; }

        public Play() : this(new Dictionary<string, object>())
        {
        }

        public Play(Dictionary<string, object> parameter) : base(parameter)
        {
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>
            {
                "file"
            };
        }

        protected override string Action()
        {
            return "play";
        }
    }
}
