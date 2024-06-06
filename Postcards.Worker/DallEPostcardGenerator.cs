using ErrorOr;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Postcards.Worker;

public class DallEPostcardGenerator : IPostcardGenerator
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DallEPostcardGenerator> _logger;

    public DallEPostcardGenerator(ILogger<DallEPostcardGenerator> logger, string apiKey)
    {
        _logger = logger;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    public async Task<ErrorOr<string>> GeneratePostcard(string prompt)
    {
        var requestBody = new
        {
            model = "dall-e-3",
            prompt,
            n = 1,
            size = "1024x1024",
            response_format = "url",
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://api.openai.com/v1/images/generations", content);

        if (!response.IsSuccessStatusCode) return Error.Failure(description: "Failed to generate image: " + response.ReasonPhrase);
        var responseString = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseString);

        var imageUrl = (string?)responseObject?.data[0].url;
        if (string.IsNullOrEmpty(imageUrl)) return Error.Failure(description: "Failed to generate image: returned URL is empty");
        _logger.LogInformation("Generated image {ImageUrl} with prompt {Prompt}", imageUrl, prompt);
        return imageUrl;
    }
}