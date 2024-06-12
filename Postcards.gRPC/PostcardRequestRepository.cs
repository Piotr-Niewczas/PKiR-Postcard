using Postcards.gRPC.Data;
using Postcards.Models;

namespace Postcards.gRPC;

public class PostcardRequestRepository(AppDbContext dbContext)
{
    public async Task AddPostcard(int locationId, string text, string userId)
    {
        dbContext.Postcards.Add(new Postcard(locationId, text, userId));
        await dbContext.SaveChangesAsync();
    }
}