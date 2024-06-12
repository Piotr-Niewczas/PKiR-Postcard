namespace Postcards;

public interface IPostcardRequestHandler
{
    public Task<string> AddPostcard(string baseImgName, string text, string userId);
}