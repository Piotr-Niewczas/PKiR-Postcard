using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Postcards.Models;

namespace Postcards.Worker.Data;

public class PostcardRepository
{
    public async Task<List<Postcard>> GetEmptyPostcards(CancellationToken cancellationToken)
    {
        await using var context = new AppDbContext();
        return (await context.Postcards
            .Where(x => string.IsNullOrEmpty(x.ImageUrl))
            .ToListAsync(cancellationToken))!;
    }

    public async Task<ErrorOr<Postcard>> UpdatePostcard(int postcardId, string imageUrl)
    {
        await using var context = new AppDbContext();
        var postcard = await context.Postcards.FindAsync(postcardId);

        if (postcard is null)
        {
            return Error.NotFound(description: "Postcard not found");
        }

        postcard.FulfillmentDate = DateTime.UtcNow;
        postcard.ImageUrl = imageUrl;
        context.Postcards.Update(postcard);
        await context.SaveChangesAsync();
        return postcard;
    }
}