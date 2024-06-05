using Grpc.Net.Client;
using Postcard_gRPC;

namespace Postcards;

public class PostcardRequestHandler
{
    public async Task<string> AddPostcard(string prompt, string userId)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5231/");
        var client = new PostcardService.PostcardServiceClient(channel);
        
        var request = new PostcardRequest { Prompt = prompt, UserId = userId };
        
        var reply = await client.GeneratePostcardRequestAsync(request);
        return reply.Status;
    }
}