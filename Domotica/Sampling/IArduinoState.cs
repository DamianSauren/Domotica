using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Sampling
{
    public interface IArduinoState
    {
        float Temperature { get; }

        void UpdateArduinoState(float temperature);
    }
}
