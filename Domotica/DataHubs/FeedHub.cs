using Microsoft.AspNetCore.SignalR;
using Domotica.Controllers;

//Author: Owen de Bree
namespace Domotica.DataHubs
{
    public class FeedHub : Hub
    {
        private readonly DataController dataController;
        public void TurnOn(string lightId)
        {
            bool state = true;
            dataController.UpdateLightState(lightId, state);
        }

        public void TurnOff(string lightId)
        {
            bool state = false;
            dataController.UpdateLightState(lightId, state);
        }

        public void ChangeColor(string lightId, string hexColor)
        {
            dataController.UpdateColorState(lightId, hexColor);
        }
    }
}
