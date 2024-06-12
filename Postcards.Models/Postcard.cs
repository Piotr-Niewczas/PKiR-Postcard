using System.ComponentModel.DataAnnotations;

namespace Postcards.Models;

public class Postcard(string baseImgName, string text, string userId)
{
    private Postcard() : this("", "", "") // EF Core requires a parameterless constructor
    {
    }

    public int Id { get; private set; }
    public string baseImgName { get; private set; } = baseImgName;
    public string Text { get; private set; } = text;
    public string UserId { get; private set; } = userId;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public string? ImageUrl { get; set; }
    public DateTime? FulfillmentDate { get; set; }
}