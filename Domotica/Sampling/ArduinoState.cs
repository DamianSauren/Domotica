using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Sampling
{
    public class ArduinoState : IArduinoState
    {
        public string TempId { get; private set; }
        public string MotionId { get; private set; }
        public string LightId { get; private set; }
        public float Temperature { get; private set; }
        public bool IsTriggered { get; private set; }
        public string HexColor { get; private set; }
        public uint TimeOfTrigger { get; private set; }
        public bool IsOn { get; private set; }
        
        public void UpdateTempState(string tempId, float temperature)
        {
            TempId = tempId;
            Temperature = temperature;
        }

        public void UpdateMotionState(string motionId, bool isTriggered, uint timeOfTrigger)
        {
            MotionId = motionId;
            IsTriggered = isTriggered;
            TimeOfTrigger = timeOfTrigger;
        }

        public void UpdateLightState(string lightId, string hexColor, bool isOn)
        {
            LightId = lightId;
            IsOn = isOn;
            HexColor = hexColor;
        }
    }
}
