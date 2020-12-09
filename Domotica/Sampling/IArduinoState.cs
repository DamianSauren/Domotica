using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domotica.Models;

//Author: Owen de Bree
namespace Domotica.Sampling
{
    public interface IArduinoState
    {
        public string TempId { get; set; }
        public string MotionId { get; set; }
        public string LightId { get; set; }
        float Temperature { get; set; }
        bool IsTriggered { get; set; }
        string HexColor { get; set; }
        uint TimeOfTrigger { get; set; }
        bool IsOn { get; set; }
    }
}
