using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Models
{
    public class Device
    {
        public String DeviceID { get; set; }
        public String DeviceName { get; set; }
        public object DeviceProperties { get; set; }
        public DeviceCategory DeviceCategory { get; set; }
        
        public class DHT
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
