using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace ASPcoreServer.Hubs
{
    public class SomeMultiplayerGameHub : Hub
    {
        private static Dictionary<string, string> UserNameToConnIdMap = new Dictionary<string, string>();

        public void InformEnemyAboutButtonClick(int playerPoints)
        {
            Clients.All.SendAsync("RecieveMessage", playerPoints);
        }

        public void OnPlayerConnected(string userName, string userConnId)
        {
            UserNameToConnIdMap.Add(userName, userConnId);
        }

    }
}
