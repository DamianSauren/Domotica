using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domotica.Sampling;

namespace Domotica.DataHubs
{
    public class FeedHub : Hub
    {
        private readonly IArduinoState arduinoState;
        private readonly ISampleWriter sampleWriter;

        public FeedHub(IArduinoState arduinoState, ISampleWriter sampleWriter)
        {
            this.arduinoState = arduinoState;
            this.sampleWriter = sampleWriter;
        }

        public void ResetCount()
        {
            sampleWriter.StartNewFile();
        }
    }
}
