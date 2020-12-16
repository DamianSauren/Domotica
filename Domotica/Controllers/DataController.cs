using Domotica.DataHubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Domotica.Data;

//Author: Owen de Bree
namespace Domotica.Controllers
{
    public class DataController : Controller
    {
        private readonly IHubContext<FeedHub> feedHub;

        public DataController(IHubContext<FeedHub> feedHub)
        {
            this.feedHub = feedHub;
        }

        public string HexColor { get; set; }
        public bool IsOn { get; set; }

        [HttpPost]
        public async Task<ActionResult> TempSens(string tempId, float temperature)
        {
            DeviceData.Instance.UpdateTempState(tempId, temperature);
            System.Diagnostics.Debug.WriteLine("temp:" + temperature.ToString());
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newTemperatureData", tempId, temperature)
            );
            
            return StatusCode(200);
        }

        [HttpPost]
        [ActionName("MotionSens")]
        public async Task<ActionResult> MotionSensPost(string motionId, bool isTriggered, uint timeOfTrigger)
        {
            DeviceData.Instance.UpdateMotionState(motionId, isTriggered, timeOfTrigger);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newMotionData", motionId, isTriggered, timeOfTrigger)
            );

            return StatusCode(200);
        }

        [HttpPost] // Should receive post: Color code, bool on/off
        [ActionName("ReceiveLight")]
        public async Task<ActionResult> ReceiveLightPost(string lightId, string hexColor, bool isOn)
        {
            HexColor = hexColor;
            IsOn = isOn;
            DeviceData.Instance.UpdateLightState(lightId, hexColor, isOn);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newLightData", lightId, hexColor, isOn)
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
    }
}
