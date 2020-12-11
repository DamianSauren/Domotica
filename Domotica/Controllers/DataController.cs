using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domotica.DataHubs;
using Microsoft.AspNetCore.SignalR;
using Domotica.Sampling;

//Author: Owen de Bree
namespace Domotica.Controllers
{
    
    public class DataController : Controller
    {
        private readonly IArduinoState arduinoState;
        private readonly IArduinoUpdates arduinoUpdates;
        private readonly IHubContext<FeedHub> feedHub;

        public DataController (IArduinoState arduinoState, IHubContext<FeedHub> feedHub)
        {
            this.arduinoState = arduinoState;
            this.feedHub = feedHub;
        }

        [HttpPost] // Should receive JSON obj:
        public async Task<ActionResult> TempSens(float temperature, string tempId)
        {
            arduinoUpdates.UpdateTempState(tempId, temperature);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newTemperatureData", arduinoState.Temperature,
                                              arduinoState.TempId)
            );

            return StatusCode(200);
        }

        [HttpPost] // Should receive JSON obj: 
        public async Task<ActionResult> MotionSens(bool isTriggered, uint timeOfTrigger, string motionId)
        {
            arduinoUpdates.UpdateMotionState(motionId, isTriggered, timeOfTrigger);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newMotionData", arduinoState.IsTriggered,
                                              arduinoState.TimeOfTrigger,
                                              arduinoState.MotionId)
            );

            return StatusCode(200);
        }

        [HttpPost] // Should receive JSON obj: Color code, bool on/off
        public async Task<ActionResult> ReceiveLight(string hexColor, bool isOn, string lightId)
        {
            arduinoUpdates.UpdateLightState(lightId, hexColor, isOn);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newLightData", arduinoState.HexColor,
                                              arduinoState.IsOn,
                                              arduinoState.LightId)
            );

            return StatusCode(200);
        }

        [AcceptVerbs(new[] { "GET", "HEAD" })] //temporary get for light http request
        public ActionResult GetLight()
        {
            return View(arduinoState.HexColor, arduinoState.IsOn);
        }
    }
}
