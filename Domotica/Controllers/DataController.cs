using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domotica.DataHubs;
using Microsoft.AspNetCore.SignalR;
using Domotica.Sampling;

namespace Domotica.Controllers
{
    public class DataController : Controller
    {
        private readonly IArduinoState arduinoState;
        private readonly IHubContext<FeedHub> feedHub;

        public DataController (IArduinoState arduinoState, IHubContext<FeedHub> feedHub)
        {
            this.arduinoState = arduinoState;
            this.feedHub = feedHub;
        }

        [HttpPost]
        public async Task<ActionResult> ProvideReading(float temperature)
        {
            arduinoState.UpdateArduinoState(temperature);
            await Task.WhenAll(
                feedHub.Clients.All.SendAsync("newData", arduinoState.Temperature)
            );

            return StatusCode(200);
        }
    }
}
