using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Author: Owen de Bree
namespace Domotica.Sampling
{
    public class ArduinoUpdates : IArduinoUpdates
    {
        public readonly IArduinoState arduinoState;
        public void UpdateTempState(string tempId, float temperature)
        {
            arduinoState.TempId = tempId;
            arduinoState.Temperature = temperature;
        }

        public void UpdateMotionState(string motionId, bool isTriggered, uint timeOfTrigger)
        {
            arduinoState.MotionId = motionId;
            arduinoState.IsTriggered = isTriggered;
            arduinoState.TimeOfTrigger = timeOfTrigger;
        }

        public void UpdateLightState(string lightId, string hexColor, bool isOn)
        {
            arduinoState.LightId = lightId;
            arduinoState.IsOn = isOn;
            arduinoState.HexColor = hexColor;
        }
    }
}
