using System;
using System.Collections.Generic;
using Domotica.Controllers;
using Domotica.Models;
using Domotica.Sampling;

namespace Domotica.Data
{
    //Author: Damian Sauren

    /// <summary>
    /// This class holds the data of the devices connected to the account including the values we get from the Arduinos
    /// This class uses a Singleton pattern
    /// </summary>
    public sealed class DeviceData : IArduinoUpdates
    {
        private static DeviceData _instance = null;
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
        public string UserId { get; private set; }

        /// <summary>
        /// This method is required to setup values needed in the DeviceData class
        /// </summary>
        /// <param name="context">DomoticaContext</param>
        /// <param name="userId">Id of logged in user</param>
        public void Setup(DomoticaContext context, string userId)
        {
            Context = context;
            UserId = userId;

            GetDeviceList();
        }

        public List<DeviceModel> DeviceList { get; private set; }

        /// <summary>
        /// Get the device list from the database and store it in the property DeviceList
        /// </summary>
        private void GetDeviceList()
        {
            DeviceList = new Database(Context).GetDevices(UserId);
        }

        /// <summary>
        /// Update the device temperature properties
        /// </summary>
        /// <param name="tempId">Id of thermostat device</param>
        /// <param name="temperature">Temperature value</param>
        public void UpdateTempState(string tempId, float temperature)
        {
            foreach (var device in DeviceList)
            {
                if (device.DeviceId == tempId)
                {
                    ((DeviceModel.Dht) device.DeviceProperties).Temperature = temperature;

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
        public void UpdateMotionState(string motionId, bool isTriggered, uint timeOfTrigger)
        {
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
        /// <param name="hexColor">Color value of the light</param>
        /// <param name="isOn">Is the light on</param>
        public void UpdateLightState(string lightId, string hexColor, bool isOn)
        {
            foreach (var device in DeviceList)
            {
                if (device.DeviceId == lightId)
                {
                    ((DeviceModel.Light)device.DeviceProperties).HexColor = hexColor;
                    ((DeviceModel.Light)device.DeviceProperties).IsOn = isOn;

                    break; //Break the loop because the device is updated
                }
            }
        }
    }
}