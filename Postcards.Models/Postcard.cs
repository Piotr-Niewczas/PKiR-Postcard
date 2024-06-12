using System.ComponentModel.DataAnnotations;

namespace Postcards.Models;

public class Postcard(int locationId, string text, string userId)
{
    private Postcard() : this(0, "", "") // EF Core requires a parameterless constructor
    {
    }

    public int Id { get; private set; }
    public int LocationId { get; private set; } = locationId;
    public string Text { get; private set; } = text;
    public string UserId { get; private set; } = userId;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public string? ImageUrl { get; set; }
    public DateTime? FulfillmentDate { get; set; }
}