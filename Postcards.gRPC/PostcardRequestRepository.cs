using Postcards.gRPC.Data;
using Postcards.Models;

namespace Postcards.gRPC;

public class PostcardRequestRepository(AppDbContext dbContext)
{
    public async Task AddPostcard(string baseImgName, string text, string userId)
    {
        dbContext.Postcards.Add(new Postcard(baseImgName, text, userId));
        await dbContext.SaveChangesAsync();
    }
}