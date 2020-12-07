using System.Collections.Generic;
using System.Diagnostics;
using Domotica.Models;

namespace Domotica.Controllers
{
    public class Database
    {
        public List<DeviceModel> GetDevices()
        {
            var devices = new List<DeviceModel>
            {
                new DeviceModel {DeviceId = "id1", DeviceName = "name1", DeviceCategory = DeviceCategory.Light},
                new DeviceModel {DeviceId = "id2", DeviceName = "name2", DeviceCategory = DeviceCategory.Dht},
                new DeviceModel {DeviceId = "id3", DeviceName = "name3", DeviceCategory = DeviceCategory.MotionSensor}
            };

            return devices;
        }
    }
}
