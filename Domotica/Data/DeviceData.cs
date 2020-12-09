using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domotica.Data
{
    public sealed class DeviceData
    {
        private static DeviceData instance = null;
        private static readonly object padlock = new object();

        private DeviceData() { }

        public static DeviceData Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DeviceData();
                    }

                    return instance;
                }
            }
        }
    }
}
