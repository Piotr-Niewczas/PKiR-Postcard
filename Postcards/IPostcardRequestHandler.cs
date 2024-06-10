namespace Postcards;

public interface IPostcardRequestHandler
{
    public Task<string> AddPostcard(string prompt, string userId);
}