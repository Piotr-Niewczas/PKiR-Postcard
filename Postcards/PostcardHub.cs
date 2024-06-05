using Microsoft.AspNetCore.SignalR;

namespace Postcards;

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
    
    public async Task GetGreet(string name)
    {
        
        
        await Clients.All.SendAsync("ReceiveMessage",  name);
    }
    
    public async Task AddPostcard(string prompt, string userId)
    {
        var response = await new PostcardRequestHandler().AddPostcard(prompt, userId);
        
        await Clients.Caller.SendAsync("ReceiveMessage", "System", response);
    }
}