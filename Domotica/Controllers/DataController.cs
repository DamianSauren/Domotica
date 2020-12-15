using Domotica.DataHubs;
using Domotica.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

//Author: Owen de Bree
namespace Domotica.Controllers
{
    public class DataController : Controller
    {
        private IHubContext<FeedHub> feedHub;
        private IDeviceUpdate deviceUpdates;

        /*public DataController(IDeviceUpdate deviceUpdates, IHubContext<FeedHub> feedHub)
        {
            this.deviceUpdates = deviceUpdates;
            this.feedHub = feedHub;
        }*/

        public string HexColor { get; set; }
        public bool IsOn { get; set; }

        [HttpPost]
        public async Task<ActionResult> TempSens(float temperature, string tempId)
        {
            System.Diagnostics.Debug.WriteLine("temp:" + temperature.ToString());
            deviceUpdates.UpdateTempState(tempId, temperature);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newTemperatureData", temperature, tempId)
            );
            
            return StatusCode(200);
        }

        [HttpPost]
        [ActionName("MotionSens")]
        public async Task<ActionResult> MotionSensPost(bool isTriggered, uint timeOfTrigger, string motionId)
        {
            deviceUpdates.UpdateMotionState(motionId, isTriggered, timeOfTrigger);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newMotionData", isTriggered, timeOfTrigger, motionId)
            );

            return StatusCode(200);
        }

        [HttpPost] // Should receive post: Color code, bool on/off
        [ActionName("ReceiveLight")]
        public async Task<ActionResult> ReceiveLightPost(string hexColor, bool isOn, string lightId)
        {
            deviceUpdates.UpdateLightState(lightId, hexColor, isOn);

            HexColor = hexColor;
            IsOn = isOn;

            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newLightData", hexColor, isOn, lightId)
            );

            return StatusCode(200);
        }

        [AcceptVerbs(new[] { "GET", "HEAD" })] //temporary get for light http request
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ActionName("GetLight")]
        public ActionResult GetLightRequest()
        {
            return Ok();
        }

        public void UpdateTempState(string tempId, float temperature)
        {
            deviceUpdates.UpdateTempState(tempId, temperature);
        }

        public void UpdateMotionState(string motionId, bool isTriggered, uint timeOfTrigger)
        {
            deviceUpdates.UpdateMotionState(motionId, isTriggered, timeOfTrigger);
        }

        public void UpdateLightState(string lightId, string hexColor, bool isOn)
        {
            deviceUpdates.UpdateLightState(lightId, hexColor, isOn);
        }
    }
}
