using ErrorOr;
using Microsoft.AspNetCore.SignalR.Client;

namespace Postcards.Worker;

public class SignalRUpdateNotifier(HubConnection connection) : IUpdateNotifier
{
    public async Task<ErrorOr<string>> SendUpdateNotification(List<int> updatedPostcardIds)
    {
        try
        {
            await connection.StartAsync();
            await connection.InvokeAsync("UpdateEventCompleted", updatedPostcardIds);
            await connection.StopAsync();
            return "Update notification sent successfully";
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }
}