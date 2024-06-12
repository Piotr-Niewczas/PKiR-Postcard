using Grpc.Net.Client;
using Postcard_gRPC;

namespace Postcards;

public class PostcardRequestHandler(string grpcAddress) : IPostcardRequestHandler
{
    public async Task<string> AddPostcard(int locationId, string text, string userId)
    {
        var channel = GrpcChannel.ForAddress(grpcAddress);
        var client = new PostcardService.PostcardServiceClient(channel);

        var request = new PostcardRequest { LocationId = locationId, Text = text, UserId = userId };

        var reply = await client.GeneratePostcardRequestAsync(request);
        return reply.Status;
    }
}