using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Sampling
{
    public interface IArduinoState
    {
        DateTime LastSample { get; }
        float Temperature { get; }

        void UpdateArduinoState(uint sensorMilliseconds, float temperature);
    }
}
