using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domotica.Models;

namespace Domotica.Sampling
{
    public interface IArduinoState
    {
        public string TempId { get; }
        public string MotionId { get; }
        public string LightId { get; }
        float Temperature { get; }
        bool IsTriggered { get; }
        string HexColor { get; }
        uint TimeOfTrigger { get; }
        bool IsOn { get; }

        void UpdateTempState(string tempId, float temperature);
        void UpdateMotionState(string motionId, bool isTriggered, uint timeOfTrigger);
        void UpdateLightState(string lightId, string hexColor, bool isOn);
    }
}
