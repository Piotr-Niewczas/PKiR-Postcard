using ErrorOr;
using Microsoft.AspNetCore.SignalR.Client;
using Postcards.Models;

namespace Postcards.Worker;

public class SignalRUpdateNotifier(HubConnection connection) : IUpdateNotifier
{
    public async Task<ErrorOr<string>> SendUpdateNotification(List<Postcard> updatedPostcards)
    {
        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("UpdateEventCompleted", updatedPostcards);
            await connection.StopAsync();
            return "Update notification sent successfully";
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }
}