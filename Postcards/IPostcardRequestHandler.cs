namespace Postcards;

public interface IPostcardRequestHandler
{
    public Task<string> AddPostcard(int locationId, string text, string userId);
}