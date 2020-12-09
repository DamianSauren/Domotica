using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Sampling
{
    public interface IArduinoState
    {
        float Temperature { get; }
        bool IsTriggered { get; }
        string HexColor { get; }
        uint TimeOfTrigger { get; }
        bool IsOn { get; }

        void UpdateTempState(float temperature);
        void UpdateMotionState(bool isTriggered, uint timeOfTrigger);
        void UpdateColorState(string hexColor);
        void UpdateLightState(string hexColor, bool isOn);
    }
}
