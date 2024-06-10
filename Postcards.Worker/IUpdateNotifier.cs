using ErrorOr;

namespace Postcards.Worker;

public interface IUpdateNotifier
{
    public Task<ErrorOr<string>> SendUpdateNotification(List<int> updatedPostcardIds);
}