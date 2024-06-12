using ErrorOr;
using System.Drawing;
using System.Net;
using System.Runtime.Versioning;

namespace Postcards.Worker;

public class LandscapePostcardGenerator(string thisServiceUrl, string baseImageHostUrl) : IPostcardGenerator
{
    [SupportedOSPlatform("windows")]
    public async Task<ErrorOr<string>> GeneratePostcard(string baseImgName, string text, int requestId)
    {
        // Get the image URL
        var imageUrl = $"{baseImageHostUrl}/img/{baseImgName}";

        // Download the image data
        byte[] imageData;
        using (var httpClient = new HttpClient())
        {
            imageData = await httpClient.GetByteArrayAsync(imageUrl);
        }

        // Load the image
        Bitmap bitmap;
        using (var ms = new MemoryStream(imageData))
        {
            bitmap = new Bitmap(ms);
        }

        // Create graphics from the image
        var graphics = Graphics.FromImage(bitmap);

        // Define text properties
        var font = new Font("Comic Sans MS", 40, FontStyle.Bold, GraphicsUnit.Pixel);
        var color = Color.HotPink;
        var brush = new SolidBrush(color);
        var startPoint = new Point(50, 50);

        // Draw the text onto the image
        graphics.DrawString(text, font, brush, startPoint);

        // Save the image to a file
        var filename = $"postcard{requestId}.jpg";
        var newImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", filename);
        bitmap.Save(newImagePath, System.Drawing.Imaging.ImageFormat.Png);

        // Return the URL of the saved image
        var newImageUrl = $"{thisServiceUrl}/img/{filename}";

        return newImageUrl;
    }
}