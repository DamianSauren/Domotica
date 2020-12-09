using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Author: Owen de Bree
namespace Domotica.Sampling
{
    public class ArduinoState : IArduinoState
    {
        public string TempId { get; set; }
        public string MotionId { get; set; }
        public string LightId { get; set; }
        public float Temperature { get; set; }
        public bool IsTriggered { get;set; }
        public string HexColor { get; set; }
        public uint TimeOfTrigger { get; set; }
        public bool IsOn { get; set; }
    }
}
