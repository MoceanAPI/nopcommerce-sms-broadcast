using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mocean.Voice.Mapper
{
    public class RecordingResponse
    {
        public string Filename { get; private set; }

        public byte[] RecordingBuffer { get; private set; }

        public RecordingResponse(string filename, byte[] recordingBuffer)
        {
            this.Filename = filename;
            this.RecordingBuffer = recordingBuffer;
        }
    }
}
