using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Sampling
{
    public class ArduinoState : IArduinoState
    {
        private DateTime? startTime;
        private uint firstTimeStamp;
        private uint lastTimeStamp;

        public DateTime LastSample { get; private set; }

        public float Temperature { get; private set; }

        public void UpdateArduinoState(uint sensorMilliseconds, float temperature)
        {
            // Determine our milliseconds offset from the sensor, so we can sort of 
            // sync our 'now' with the sensor clock.
            if (startTime == null || sensorMilliseconds < lastTimeStamp)
            {
                startTime = DateTime.Now;
                firstTimeStamp = sensorMilliseconds;
            }

            var determinedSampleTime = startTime.Value.AddMilliseconds(sensorMilliseconds - firstTimeStamp);

            if (Math.Abs((DateTime.Now - determinedSampleTime).TotalSeconds) > 5)
            {
                // Compensate for drift, allowing the sensor time to be reset if need be.
                firstTimeStamp = sensorMilliseconds;
            }
            LastSample = startTime.Value.AddMilliseconds(sensorMilliseconds - firstTimeStamp);
            Temperature = temperature;
            lastTimeStamp = sensorMilliseconds;
        }
    }
}
