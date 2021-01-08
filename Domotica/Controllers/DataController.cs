using Domotica.DataHubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Domotica.Data;
using Domotica.Models;
using Microsoft.Extensions.Logging;

//Author: Owen de Bree
namespace Domotica.Controllers
{
    public class DataController : Controller
    {
        private readonly IHubContext<FeedHub> feedHub;
        private readonly ILogger _logger;

        public DataController(IHubContext<FeedHub> feedHub, ILogger<DataController> logger)
        {
            this.feedHub = feedHub;
            _logger = logger;
        }

        public string HexColor { get; set; }
        public bool IsOn { get; set; }

        [HttpPost]
        public async Task<ActionResult> TempSens(string tempId, string temperature)
        {
            if (DeviceData.Instance.DeviceList == null)
            {
                return StatusCode(200);
            }
            var temp = new DeviceModel.TempSensor
            {
                Temperature = temperature
            };

            var finalTemp = temp.ToString();

            DeviceData.Instance.UpdateData(tempId, finalTemp);
            _logger.LogInformation("temp:" + finalTemp);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newTemperatureData", tempId, finalTemp)
            );
            
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<ActionResult> MotionSens(string motionId, bool isTriggered, string timeOfTrigger)
        {
            if (DeviceData.Instance.DeviceList == null)
            {
                return StatusCode(200);
            }
            DeviceData.Instance.UpdateData(motionId, isTriggered, timeOfTrigger);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newMotionData", motionId, isTriggered, timeOfTrigger)
            );

            return StatusCode(200);
        }

        [HttpPost]
        public async Task<ActionResult> ReceiveLight(string lightId, string hexColor, bool isOn)
        {
            if (DeviceData.Instance.DeviceList == null)
            {
                return StatusCode(200);
            }
            var light = new DeviceModel.Light
            {
                HexColor = hexColor,
                IsOn = isOn
            };

            DeviceData.Instance.UpdateData(lightId, light);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newLightData", lightId, hexColor, isOn)
            );

            return StatusCode(200);
        }

        public static void UpdateLightState(string lightId, bool lightState)
        {
            var light = new DeviceModel.Light
            {
                HexColor = DeviceData.Instance.GetLight(lightId).HexColor,
                IsOn = lightState
            };

            DeviceData.Instance.UpdateData(lightId, light);
        }

        public static void UpdateColorState(string lightId, string hexColor)
        {
            var light = new DeviceModel.Light
            {
                HexColor = hexColor,
                IsOn = DeviceData.Instance.GetLight(lightId).IsOn
            };

            DeviceData.Instance.UpdateData(lightId, light);
        }

        [AcceptVerbs(new[] { "GET", "HEAD" })] //temporary get for light http request
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ActionName("GetLight")]
        public ActionResult RequestLight(string lightId)
        {
            if (DeviceData.Instance.DeviceList == null)
            {
                return StatusCode(200);
            }
            var Color = DeviceData.Instance.GetLight(lightId).HexColor;
            var isOn = DeviceData.Instance.GetLight(lightId).IsOn;

            return Ok(Color + "||"+ isOn);
        }
    }
}
