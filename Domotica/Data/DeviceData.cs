using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Data
{
    /// <summary>
    /// This class holds the data of the devices connected to the account
    /// This class uses a Singleton pattern
    /// </summary>
    public sealed class DeviceData
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
    }
}
