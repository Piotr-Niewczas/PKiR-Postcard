using Grpc.Net.Client;
using Postcard_gRPC;

namespace Postcards;

public class PostcardRequestHandler(string grpcAddress) : IPostcardRequestHandler
{
    public async Task<string> AddPostcard(string baseImgName, string text, string userId)
    {
        var channel = GrpcChannel.ForAddress(grpcAddress);
        var client = new PostcardService.PostcardServiceClient(channel);

        var request = new PostcardRequest { BaseImgName = baseImgName, Text = text, UserId = userId };

        var reply = await client.GeneratePostcardRequestAsync(request);
        return reply.Status;
    }
}