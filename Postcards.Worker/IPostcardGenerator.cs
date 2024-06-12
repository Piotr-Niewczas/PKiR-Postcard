using ErrorOr;

namespace Postcards.Worker;

public interface IPostcardGenerator
{
    Task<ErrorOr<string>> GeneratePostcard(int locationId, string prompt);
}