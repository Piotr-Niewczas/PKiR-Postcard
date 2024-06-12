using Grpc.Core;
using Postcard_gRPC;

namespace Postcards.gRPC.Services;

public class PostcardRequestService(PostcardRequestRepository repository) : PostcardService.PostcardServiceBase
{
    public override async Task<PostcardResponse> GeneratePostcardRequest(PostcardRequest request,
        ServerCallContext context)
    {
        await repository.AddPostcard(request.LocationId, request.Text, request.UserId);
        return new PostcardResponse
        {
            Status = "Successfully added postcard request"
        };
    }
}