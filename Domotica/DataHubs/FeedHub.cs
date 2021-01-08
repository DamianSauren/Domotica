using Microsoft.AspNetCore.SignalR;
using Domotica.Controllers;
using Microsoft.Extensions.Logging;

//Author: Owen de Bree
namespace Domotica.DataHubs
{
    public class FeedHub : Hub
    {
        public void TurnOn(string lightId)
        {
            bool state = true;
            DataController.UpdateLightState(lightId, state);
        }

        public void TurnOff(string lightId)
        {
            bool state = false;
            DataController.UpdateLightState(lightId, state);
        }

        public void ChangeColor(string lightId, string hexColor)
        {
            DataController.UpdateColorState(lightId, hexColor);
        }
    }
}
