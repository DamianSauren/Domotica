using System;

namespace Domotica.Models
{
    public class DeviceModel
    {
        public DeviceModel()
        {
            DeviceProperties = DeviceCategory switch
            {
                DeviceCategory.Dht => (object) new Dht(),
                DeviceCategory.MotionSensor => new MotionSensor(),
                DeviceCategory.Light => new Light(),
                _ => throw new ArgumentOutOfRangeException(nameof(DeviceCategory), DeviceCategory, null)
            };
        }

        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public DeviceCategory DeviceCategory { get; set; }
        public object DeviceProperties { get; set; }
        
        public class Dht
        {

        }

        public class MotionSensor
        {

        }

        public class Light
        {

        }
    }
}
