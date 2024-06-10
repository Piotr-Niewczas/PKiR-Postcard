using ErrorOr;
using Postcards.Models;

namespace Postcards.Worker;

public interface IUpdateNotifier
{
    public Task<ErrorOr<string>> SendUpdateNotification(List<Postcard> updatedPostcards);
}