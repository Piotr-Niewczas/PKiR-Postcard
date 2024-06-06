using ErrorOr;

namespace Postcards.Worker;

public class DummyPostcardGenerator : IPostcardGenerator
{
    public async Task<ErrorOr<string>> GeneratePostcard(string prompt)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        return "https://i.imgflip.com/5epsjx.jpg";
    }
}