using ErrorOr;

namespace Postcards.Worker;

public interface IPostcardGenerator
{
    Task<ErrorOr<string>> GeneratePostcard(string prompt);
}