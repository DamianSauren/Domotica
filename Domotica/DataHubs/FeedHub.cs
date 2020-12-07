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

        public FeedHub(IArduinoState arduinoState)
        {
            this.arduinoState = arduinoState;
        }
    }
}
