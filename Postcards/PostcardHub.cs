﻿using Microsoft.AspNetCore.SignalR;
using Postcards.Models;

namespace Postcards;

public class PostcardHub(ILogger<PostcardHub> logger, IPostcardRequestHandler postcardRequestHandler) : Hub
{
    Dictionary<string, string> _connectionIdToUserId = new();
    
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

    public async Task UpdateEventCompleted(List<Postcard> updatedIds)
    {
        List<Task> tasks = [];
        tasks.AddRange(updatedIds.Select(SendUpdateNotificationToClient));

        await Task.WhenAll(tasks);
        // log all completed or sth
        logger.LogInformation("All update notifications sent to front clients");
    }

    private async Task SendUpdateNotificationToClient(Postcard postcard)
    {
        await Clients.Group(postcard.UserId).SendAsync("ReceiveMessage", postcard.Id.ToString() , $"has been updated: {postcard.ImageUrl} link");
    }
    
    public async Task AddToGroup(string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        await Clients.Group(userId).SendAsync("ReceiveMessage", "System: ",$"Successfully joined the group {userId}.");
    }
    
}