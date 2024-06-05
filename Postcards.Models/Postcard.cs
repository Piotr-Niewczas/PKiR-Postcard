using System.ComponentModel.DataAnnotations;

namespace Postcards.Models;

public class Postcard(string prompt, string userId)
{
    public int Id { get; private set; }
    public string Prompt { get; private set; } = prompt;
    public string UserId { get; private set; } = userId;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public string? ImageUrl { get; set; }
    public DateTime? FulfillmentDate { get; set; }

    private Postcard() : this( prompt:"", userId:"") // EF Core requires a parameterless constructor
    {}
}