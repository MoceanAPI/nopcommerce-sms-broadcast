using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice.McObj
{
    public class Record : AbstractMc
    {
        public Record() : this(new Dictionary<string, object>())
        {
        }

        public Record(Dictionary<string, object> parameter) : base(parameter)
        {
        }

        protected override List<string> RequiredKey()
        {
            return new List<string>();
        }

        protected override string Action()
        {
            return "record";
        }
    }
}
