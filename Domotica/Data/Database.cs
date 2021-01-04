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

        private const string Dht = "dth";
        private const string MotionSensor = "motion-sensor";
        private const string Light = "light";

        public Database(DomoticaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new device to the database
        /// </summary>
        /// <param name="userId">Id of the user to which the device will be added</param>
        /// <param name="deviceModel">DeviceModel containing all the device data</param>
        public void AddDevice(string userId, DeviceModel deviceModel)
        {
            var device = new Device()
            {
                Id = deviceModel.DeviceId,
                DeviceName = deviceModel.DeviceName,
                DeviceCategory = GetDeviceCategory(deviceModel.DeviceCategory),
                UserId = userId
                
            };

            _context.Device.Add(device);
            _context.SaveChanges();
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
                object deviceProperties = GetDeviceCategory(deviceListItem.DeviceCategory) switch
                {
                    DeviceCategory.Dht => new DeviceModel.TempSensor(),
                    DeviceCategory.MotionSensor => new DeviceModel.MotionSensor(),
                    DeviceCategory.Light => new DeviceModel.Light(),
                    _ => throw new ArgumentOutOfRangeException()
                };

                devices.Add(new DeviceModel()
                {
                    DeviceId = deviceListItem.Id,
                    DeviceName = deviceListItem.DeviceName,
                    DeviceCategory = GetDeviceCategory(deviceListItem.DeviceCategory),
                    DeviceProperties = deviceProperties
                });
            }

            return devices;
        }

        /// <summary>
        /// Get the device category string that is stored in the database
        /// </summary>
        /// <param name="category">Device category of enum type DeviceCategory</param>
        /// <returns>Category string</returns>
        private static string GetDeviceCategory(DeviceCategory category)
        {
            return category switch
            {
                DeviceCategory.Dht => Dht,
                DeviceCategory.MotionSensor => MotionSensor,
                DeviceCategory.Light => Light,
                _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
            };
        }

        /// <summary>
        /// Get the enum device category that is used throughout the app
        /// </summary>
        /// <param name="category">Device category string from te database</param>
        /// <returns>Category of enum DeviceCategory</returns>
        private static DeviceCategory GetDeviceCategory(string category)
        {
            return category switch
            {
                Dht => DeviceCategory.Dht,
                MotionSensor => DeviceCategory.MotionSensor,
                Light => DeviceCategory.Light,
                _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
            };
        }
    }
}