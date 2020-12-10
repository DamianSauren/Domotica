using System;
using System.Collections.Generic;
using Domotica.Controllers;
using Domotica.Models;
using Domotica.Sampling;

namespace Domotica.Data
{
    //Author: Damian Sauren

    /// <summary>
    /// This class holds the data of the devices connected to the account
    /// This class uses a Singleton pattern
    /// </summary>
    public sealed class DeviceData: IArduinoUpdates
    {
        private static DeviceData _instance = null;
        private static readonly object Padlock = new object();

        private DeviceData() { }

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

        public void UpdateTempState(string tempId, float temperature)
        {
            throw new NotImplementedException();
        }

        public void UpdateMotionState(string motionId, bool isTriggered, uint timeOfTrigger)
        {
            throw new NotImplementedException();
        }

        public void UpdateLightState(string lightId, string hexColor, bool isOn)
        {
            throw new NotImplementedException();
        }
    }
}
