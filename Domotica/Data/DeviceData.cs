using System.Collections.Generic;
using Domotica.Controllers;
using Domotica.Models;
using Microsoft.Extensions.Logging;

namespace Domotica.Data
{
    //Author: Damian Sauren

    /// <summary>
    /// This class holds the data of the devices connected to the account including the values we get from the Arduinos
    /// This class uses a Singleton pattern
    /// </summary>
    public sealed class DeviceData
    {
        private static DeviceData _instance;
        private static readonly object Padlock = new object();

        private DeviceData()
        {
        }

        public static DeviceData Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new DeviceData();
                    }

                    return _instance;
                }
            }
        }

        public DomoticaContext Context { get; private set; }
        public List<DeviceModel> DeviceList = new List<DeviceModel>();

        private bool _isDone;

        private ILogger<HomeController> _logger;
        /// <summary>
        /// This method is required to setup values needed in the DeviceData class
        /// </summary>
        /// <param name="context">DomoticaContext</param>
        /// <param name="userId">Id of logged in user</param>
        public void Setup(DomoticaContext context, string userId, ILogger<HomeController> logger)
        {
            Context = context;
            if (_isDone) return;
            _isDone = true;
            _logger = logger;
        }

        /// <summary>
        /// Get the devices for user
        /// </summary>
        /// <param name="userId">Id of logged in user</param>
        /// <returns>List of devices of type DeviceModel</returns>
        public List<DeviceModel> GetDeviceList(string userId)
        {
            List<DeviceModel> userList = new List<DeviceModel>();

            foreach(DeviceModel device in DeviceList)
            {
                if(device.UserId == userId)
                {
                    userList.Add(device);
                    _logger.LogInformation(device.ToString());
                }
            }

            return userList;
        }

        /// <summary>
        /// Update the existing device list with new devices that are not yet in current list
        /// </summary>
        /// <param name="userId">Id of logged in user</param>
        public void UpDateDeviceList(string userId)
        {
            List<DeviceModel> devices = new Database(Context).GetDevices(userId);

            if (DeviceList.Count == 0 || DeviceList == null)
            {
                //Add whole list if DeviceList is empty
                DeviceList.AddRange(devices);
            }

            foreach(DeviceModel device in devices)
            {
               // _logger.LogInformation(device.ToString());

                if (!DeviceList.Contains(device))
                {
                    //Device is not yet in list so add it
                    DeviceList.Add(device);
                }
            }
        }

        public DeviceModel.Light GetLight(string id)
        {
            foreach(DeviceModel device in DeviceList)
            {
                if (device.DeviceId == id)
                {
                    return device.DeviceProperties as DeviceModel.Light;
                }
            }
            return new DeviceModel.Light();
            
        }

        /// <summary>
        /// Update the device temperature properties
        /// </summary>
        /// <param name="tempId">Id of thermostat device</param>
        /// <param name="temperature">Temperature value</param>
        public void UpdateData(string tempId, string temperature)
        {
            if (DeviceList == null) return;

            foreach (var device in DeviceList)
            {
                if (device.DeviceId == tempId)
                {
                    ((DeviceModel.TempSensor) device.DeviceProperties).Temperature = temperature;

                    break; //Break the loop because the device is updated
                }
            }
        }

        /// <summary>
        /// Update motion sensor properties
        /// </summary>
        /// <param name="motionId">Id of motion sensor</param>
        /// <param name="isTriggered">Is the sensor triggered</param>
        /// <param name="timeOfTrigger">Time the sensor is triggered</param>
        public void UpdateData(string motionId, bool isTriggered, string timeOfTrigger)
        {
            if (DeviceList == null) return;

            foreach (var device in DeviceList)
            {
                if (device.DeviceId == motionId)
                {
                    ((DeviceModel.MotionSensor) device.DeviceProperties).IsTriggered = isTriggered;
                    ((DeviceModel.MotionSensor) device.DeviceProperties).TimeOfTrigger = timeOfTrigger;

                    break; //Break the loop because the device is updated
                }
            }
        }

        /// <summary>
        /// Update the light properties
        /// </summary>
        /// <param name="lightId">Id of the light</param>
        /// <param name="light">Values of the light</param>
        public void UpdateData(string lightId, DeviceModel.Light light )
        {
            if (DeviceList == null) return;

            foreach (var device in DeviceList)
            {
                if (device.DeviceId == lightId)
                {
                    ((DeviceModel.Light)device.DeviceProperties).HexColor = light.HexColor;
                    ((DeviceModel.Light)device.DeviceProperties).IsOn = light.IsOn;

                    break; //Break the loop because the device is updated
                }
            }
        }

        public void AddNewDevice(DeviceModel device)
        {
            DeviceList.Add(device);
            
            _logger.LogInformation(device.ToString());
        }
    }
}