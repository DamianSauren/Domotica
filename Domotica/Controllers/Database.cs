using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domotica.Data;
using Domotica.Models;
using Microsoft.EntityFrameworkCore;

namespace Domotica.Controllers
{
    //Author: Damian Sauren
    public class Database
    {

        private readonly DomoticaContext _context;

        public Database(DomoticaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method returns a list of type <c>DeviceModel</c> from the database
        /// </summary>
        /// <param name="userId">Id of the logged in user</param>
        /// <returns>List of type <c>DeviceModel</c></returns>
        public List<DeviceModel> GetDevices(string userId)
        {
            var deviceList = _context.Device
                .Where(s => s.UserId == userId)
                .ToList();

            var devices = new List<DeviceModel>();

            foreach (var deviceListItem in deviceList)
            {
                var deviceCategory = deviceListItem.DeviceCategory switch
                {
                    "dht" => DeviceCategory.Dht,
                    "motion-sensor" => DeviceCategory.MotionSensor,
                    "light" => DeviceCategory.Light,
                    _ => throw new System.NotImplementedException()
                };

                object deviceProperties = deviceCategory switch
                {
                    DeviceCategory.Dht => new DeviceModel.Dht(),
                    DeviceCategory.MotionSensor => new DeviceModel.MotionSensor(),
                    DeviceCategory.Light => new DeviceModel.Light(),
                    _ => throw new ArgumentOutOfRangeException()
                };

                devices.Add(new DeviceModel()
                {
                    DeviceId = deviceListItem.Id,
                    DeviceName = deviceListItem.DeviceName,
                    DeviceCategory = deviceCategory,
                    DeviceProperties = deviceProperties
                });
            }

            return devices;
        }
    }
}