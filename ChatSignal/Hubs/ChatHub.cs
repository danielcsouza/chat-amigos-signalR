using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ChatSignal.Hubs
{

    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        public static List<string> Users = new List<string>();

        public void Send(string nome, string message)
        {
            Clients.All.addMessage(nome, message);
          /*  var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.All.updateUsersOnlineCount(count);
            */
        }

       
        public override Task OnConnected()
        {
            var clientId = GetClientId();

            if (Users.IndexOf(clientId) == -1)
            {
                Users.Add(clientId);
            }

            // Send the current count of users
            Send("Sistema", "Seja bem vindo....");

            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            var clientId = GetClientId();
            if (Users.IndexOf(clientId) == -1)
            {
                Users.Add(clientId);
            }

            // Send the current count of users
            //Send("Sistema", "Já está indo?", Users.Count);

            return base.OnReconnected();
        }

        private string GetClientId()
        {
            string clientId = "";
            if (Context.QueryString["clientId"] != null)
            {
                // clientId passed from application 
                clientId = this.Context.QueryString["clientId"];
            }

            if (string.IsNullOrEmpty(clientId.Trim()))
            {
                clientId = Context.ConnectionId;
            }

            return clientId;
        }
    }
}
