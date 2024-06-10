using Microsoft.AspNetCore.SignalR;

namespace Postcards;

public class PostcardHub(ILogger<PostcardHub> logger, IPostcardRequestHandler postcardRequestHandler) : Hub
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
        var response = await postcardRequestHandler.AddPostcard(prompt, userId);
        
        await Clients.Caller.SendAsync("ReceiveMessage", "System", response);
    }

    public async Task UpdateEventCompleted(List<int> updatedIds)
    {
        List<Task> tasks = [];
        tasks.AddRange(updatedIds.Select(id => SendUpdateNotificationToClients(id, "")));

        await Task.WhenAll(tasks);
        // log all completed or sth
        logger.LogInformation("All update notifications sent to front clients");
    }

    private async Task SendUpdateNotificationToClients(int postcardId, string connectionId)
    {
        // what if SendAsync fails?
        await Clients.All.SendAsync("ReceiveMessage",  postcardId , "has been updated"); // change to send to specific client
    }
    
}