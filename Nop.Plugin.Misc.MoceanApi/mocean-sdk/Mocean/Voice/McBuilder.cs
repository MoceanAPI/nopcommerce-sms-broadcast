using Mocean.Voice.McObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice
{
    public class McBuilder
    {
        protected List<AbstractMc> mcList;

        public McBuilder()
        {
            this.mcList = new List<AbstractMc>();
        }

        public McBuilder add(AbstractMc mc)
        {
            this.mcList.Add(mc);
            return this;
        }

        public List<Dictionary<string, object>> build()
        {
            List<Dictionary<string, object>> converted = new List<Dictionary<string, object>>();
            foreach (AbstractMc mc in this.mcList)
            {
                converted.Add(mc.GetRequestData());
            }
            return converted;
        }
    }
}
