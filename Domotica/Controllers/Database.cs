using System.Collections.Generic;
using System.Diagnostics;
using Domotica.Models;

namespace Domotica.Controllers
{
    public class Database
    {
        public List<Device> GetDevices()
        {
            var devices = new List<Device>
            {
                new Device {DeviceId = "id1", DeviceName = "name1", DeviceCategory = DeviceCategory.Light},
                new Device {DeviceId = "id2", DeviceName = "name2", DeviceCategory = DeviceCategory.Dht},
                new Device {DeviceId = "id3", DeviceName = "name3", DeviceCategory = DeviceCategory.MotionSensor}
            };

            return devices;
        }
    }
}
