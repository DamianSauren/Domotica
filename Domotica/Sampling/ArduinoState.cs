using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Sampling
{
    public class ArduinoState : IArduinoState
    {
        public float Temperature { get; private set; }

        public void UpdateArduinoState(float temperature)
        {
            Temperature = temperature;
        }
    }
}
