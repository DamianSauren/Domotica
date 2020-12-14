using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domotica.DataHubs;
using Microsoft.AspNetCore.SignalR;
using Domotica.Interfaces;

//Author: Owen de Bree
namespace Domotica.Controllers
{
    
    public class DataController : Controller, IHubContext<FeedHub>, IDeviceUpdate
    {
        public string HexColor { get; set; }
        public bool IsOn { get; set; }

        private readonly IHubContext<FeedHub> feedHub = new DataController();
        private readonly IDeviceUpdate deviceUpdates = new DataController();

        public IHubClients Clients => feedHub.Clients;
        public IGroupManager Groups => feedHub.Groups;

        /*public DataController (IArduinoState arduinoState, IHubContext<FeedHub> feedHub)
        {
            this.arduinoState = arduinoState;
            this.feedHub = feedHub;
        }*/

        [HttpPost]
        [ActionName("TempSens")]
        public async Task<ActionResult> TempSensPost(float temperature, string tempId)
        {
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
        [ActionName("GetLight")]
        public ActionResult GetLightRequest()
        {
            return View(HexColor, IsOn);
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
