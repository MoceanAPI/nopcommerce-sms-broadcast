using Mocean.Voice.McObj;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice.Mapper
{
    public class VoiceRequest
    {
        [JsonProperty("mocean-to")]
        public string mocean_to { get; set; }

        [JsonProperty("mocean-event-url")]
        public string mocean_event_url { get; set; }

        [JsonProperty("mocean-command")]
        public object mocean_command
        {
            get
            {
                var builderMoceanCommand = _moceanCommand as McBuilder;
                if(builderMoceanCommand != null)
                {
                    return JsonConvert.SerializeObject(((McBuilder)_moceanCommand).build());
                }

                var objMoceanCommand = _moceanCommand as AbstractMc;
                if(objMoceanCommand != null)
                {
                    return JsonConvert.SerializeObject((new McBuilder()).add((AbstractMc)_moceanCommand).build());
                }

                var dictMoceanCommand = _moceanCommand as Dictionary<string, object>;
                if(dictMoceanCommand != null)
                {
                    return JsonConvert.SerializeObject(new List<Dictionary<string, object>>
                    {
                        (Dictionary<string, object>)_moceanCommand
                    });
                }

                return _moceanCommand;
            }
            set
            {
                _moceanCommand = value;
            }
        }
        private object _moceanCommand;

        [JsonProperty("mocean-resp-format")]
        public string mocean_resp_format { get; set; }
    }
}
