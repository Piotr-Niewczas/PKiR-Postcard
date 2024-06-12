using ErrorOr;

namespace Postcards.Worker;

public class RemotePostcardGenerator(string generatorServiceUrl, string baseImageHostUrl) : IPostcardGenerator
{
    public async Task<ErrorOr<string>> GeneratePostcard(string baseImgName, string text, int requestId)
    {
        var httpClient = new HttpClient();

        try
        {
            var responseString = await httpClient.GetStringAsync(
                $"{generatorServiceUrl}/generate?baseImgName={Uri.EscapeDataString(baseImgName)}&text={Uri.EscapeDataString(text)}&requestId={requestId}");
            responseString = responseString.Trim('"');
            return responseString;
        }
        catch (Exception e)
        {
            return Error.Unexpected(description: e.Message);
        }
    }
}