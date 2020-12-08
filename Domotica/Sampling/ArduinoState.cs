using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Sampling
{
    public class ArduinoState : IArduinoState
    {
        public float Temperature { get; private set; }
        public bool IsTriggered { get; private set; }
        public string HexColor { get; private set; }
        public uint TimeOfTrigger { get; private set; }
        public void UpdateTempState(float temperature)
        {
            Temperature = temperature;
        }

        public void UpdateMotionState(bool isTriggered, uint timeOfTrigger)
        {
            IsTriggered = isTriggered;
            TimeOfTrigger = timeOfTrigger;
        }

        public void UpdateColorState(string hexColor)
        {
            HexColor = hexColor;
        }
    }
}
