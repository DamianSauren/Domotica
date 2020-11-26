using System;

namespace Domotica.Class
{
    public class Device
    {
        public String ID { get; set; }
        public String MAC_Adres { get; set; }
        public String IP_Adres { get; set; }

        public enum DeviceCategory { motionSensor, light, dht }

        public class MotionSensor 
        {
            public Boolean IsMotionDetected { get; set; }
        }

        public class Light 
        {
            public Boolean SwitchState { get; set; }
            public String Color { get; set; }
        }

        public class DHT 
        {
            public float MyTemperature { get; set; }
            public float MyHumidity { get; set; }
        }
    }
}