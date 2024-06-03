using Grpc.Net.Client;
using Postcard_gRPC;

namespace Postcard;

public class GreetHandler
{
    public async Task<string> GetGreet(string name)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5231/");
        var client = new Greeter.GreeterClient(channel);
        
        var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
        return reply.Message;
    }
}