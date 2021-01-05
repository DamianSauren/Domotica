using System;
using System.ComponentModel.DataAnnotations;

//Author: Owen de Bree
namespace Domotica.Models
{
    public class DeviceModel
    {
        public DeviceModel()
        {
            DeviceProperties = DeviceCategory switch
            {
                DeviceCategory.TempSensor => (object) new TempSensor(),
                DeviceCategory.MotionSensor => new MotionSensor(),
                DeviceCategory.Light => new Light(),
                _ => throw new ArgumentOutOfRangeException(nameof(DeviceCategory), DeviceCategory, null)
            };
        }

        [Required]
        public string DeviceId { get; set; }
        [Required]
        public string DeviceName { get; set; }
        [Required]
        public DeviceCategory DeviceCategory { get; set; }
        public object DeviceProperties { get; set; }
        
        public class TempSensor
        {
            //Author: Owen de Bree
            public override string ToString()
            {
                string completeTemp = Temperature + " C°";
                return completeTemp;
            }
            public string Temperature { get; set; }
        }

        public class MotionSensor
        {
            //Author: Damian Sauren
            public bool IsTriggered { get; set; }
            public string TimeOfTrigger { get; set; }

        }

        public class Light
        {
            //Author: Damian Sauren
            public string HexColor { get; set; }
            public bool IsOn { get; set; }
        }

        public override string ToString()
        {
            return "DeviceId: " + DeviceId + " DeviceName: " + DeviceName + " DeviceCategory: " + DeviceCategory;
        }
    }
}
