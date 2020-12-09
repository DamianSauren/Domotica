using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domotica.DataHubs;
using Microsoft.AspNetCore.SignalR;
using Domotica.Sampling;

//Author: Owen de Bree
namespace Domotica.Controllers
{
    [Route("api/[controller]")] //localhost:5000/api/Datacontroller/TempSens_ID
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

        [HttpPost("TempSens")] // Should receive JSON obj:
        public async Task<ActionResult> ProvideReading(float temperature, string tempId)
        {
            arduinoUpdates.UpdateTempState(tempId, temperature);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newTemperatureData", arduinoState.Temperature,
                                              arduinoState.TempId)
            );

            return StatusCode(200);
        }

        [HttpPost("MotionSens")] // Should receive JSON obj: 
        public async Task<ActionResult> ProvideReading(bool isTriggered, uint timeOfTrigger, string motionId)
        {
            arduinoUpdates.UpdateMotionState(motionId, isTriggered, timeOfTrigger);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newMotionData", arduinoState.IsTriggered,
                                              arduinoState.TimeOfTrigger,
                                              arduinoState.MotionId)
            );

            return StatusCode(200);
        }

        [HttpPost("ReceiveLight")] // Should receive JSON obj: Color code, bool on/off
        public async Task<ActionResult> ProvideReading(string hexColor, bool isOn, string lightId)
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
