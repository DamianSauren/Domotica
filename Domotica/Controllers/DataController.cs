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
        private readonly ISampleWriter sampleWriter;
        private readonly IHubContext<FeedHub> feedHub;

        public DataController (IArduinoState arduinoState, ISampleWriter sampleWriter, IHubContext<FeedHub> feedHub)
        {
            this.arduinoState = arduinoState;
            this.sampleWriter = sampleWriter;
            this.feedHub = feedHub;
        }

        [HttpPost]
        public async Task<ActionResult> ProvideReading(uint milliseconds, float temperature)
        {
            arduinoState.UpdateArduinoState(milliseconds, temperature);
            await Task.WhenAll(
                sampleWriter.ProvideSample(arduinoState.LastSample, arduinoState.Temperature),
                feedHub.Clients.All.SendAsync("newData",
                                      arduinoState.LastSample.ToString("o"),
                                      arduinoState.Temperature)
            );
            
            

            return StatusCode(200);
        }
    }
}
