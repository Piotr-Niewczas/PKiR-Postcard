using ErrorOr;

namespace Postcards.Generator;

public interface IPostcardGenerator
{
    Task<ErrorOr<string>> GeneratePostcard(string baseImgName, string text, int requestId);
}