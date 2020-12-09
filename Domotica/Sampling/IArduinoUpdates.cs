using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Author: Owen de Bree
namespace Domotica.Sampling
{
    interface IArduinoUpdates
    {
        void UpdateTempState(string tempId, float temperature);
        void UpdateMotionState(string motionId, bool isTriggered, uint timeOfTrigger);
        void UpdateLightState(string lightId, string hexColor, bool isOn);
    }
}
