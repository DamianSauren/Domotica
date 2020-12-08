using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domotica.DataHubs;
using Microsoft.AspNetCore.SignalR;
using Domotica.Sampling;

namespace Domotica.Controllers
{
    [Route("api/[controller]")] //localhost:5000/api/Datacontroller/TempSens
    public class DataController : Controller
    {
        private readonly IArduinoState arduinoState;
        private readonly IHubContext<FeedHub> feedHub;

        public DataController (IArduinoState arduinoState, IHubContext<FeedHub> feedHub)
        {
            this.arduinoState = arduinoState;
            this.feedHub = feedHub;
        }

        [HttpPost("TempSens")]
        public async Task<ActionResult> ProvideReading(float temperature)
        {
            arduinoState.UpdateTempState(temperature);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newTemperatureData", arduinoState.Temperature)
            );

            return StatusCode(200);
        }

        [HttpPost("MotionSens")]
        public async Task<ActionResult> ProvideReading(bool isTriggered, uint timeOfTrigger)
        {
            arduinoState.UpdateMotionState(isTriggered, timeOfTrigger);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newMotionData", arduinoState.IsTriggered,
                                              arduinoState.TimeOfTrigger)
            );

            return StatusCode(200);
        }

        [AcceptVerbs(new[] { "GET", "HEAD" })] //temporary get for light http request
        public ActionResult GetLight()
        {
            // Should receive JSON obj: Color code, bool on/off
            // Should return JSON obj: (new) Color code, updated bool on/off
            return View();
        }
    }
}
