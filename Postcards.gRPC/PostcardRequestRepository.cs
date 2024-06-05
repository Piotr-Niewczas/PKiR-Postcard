using Postcards.gRPC.Data;
using Postcards.Models;

namespace Postcards.gRPC;

public class PostcardRequestRepository(AppDbContext dbContext)
{
    public async Task AddPostcard(string prompt, string userId)
    {
        dbContext.Postcards.Add(new Postcard(prompt, userId));
        await dbContext.SaveChangesAsync();
    }
}