using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Interfaces
{
    public interface IDeviceUpdate
    {
        public void UpdateTempState(string tempId, float temperature);
        public void UpdateMotionState(string motionId, bool isTriggered, uint timeOfTrigger);
        public void UpdateLightState(string lightId, string hexColor, bool isOn);
    }
}
