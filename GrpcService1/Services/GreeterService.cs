using Grpc.Core;
using GrpcService1;
using static System.Net.Mime.MediaTypeNames;

namespace GrpcService1.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogDebug("asdasd"); 

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });            
        }

        public override Task<TestResponse> Test(TestRequest request, ServerCallContext context)
        {
            Console.WriteLine($"{request.Name + context.Peer + context.Host + context.AuthContext + context.Status}");
            return Task.FromResult
                (
                    new TestResponse
                    {
                        Message = "fuck" + request.Name + context.Peer + context.Host + context.AuthContext + context.Status
                    }
                );
        }
    }
}