using Microsoft.AspNetCore.SignalR;

namespace Postcard;

public class PostcardHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", "System", $"{Context.ConnectionId} joined the chat");
    }
    
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage",  message);
    }
}