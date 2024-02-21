using Api.Models;
using Microsoft.AspNetCore.SignalR;

namespace Api.SignalR
{
    public class MyHub : Hub
    {
        public async Task Messages(Message message)
        {
            await Clients.All.SendAsync("Message", message);
        }
    }
}
