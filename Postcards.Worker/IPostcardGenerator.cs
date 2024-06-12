using ErrorOr;

namespace Postcards.Worker;

public interface IPostcardGenerator
{
    Task<ErrorOr<string>> GeneratePostcard(string baseImgName, string text, int requestId);
}