using System;
using Grpc.Core;
using Shared;

namespace Client
{
    internal class CalculatorClient
    {
        private readonly CalcService.CalcServiceClient _client;

        public CalculatorClient(Channel channel)
        {
            _client = new CalcService.CalcServiceClient(channel);
        }

        public void ExecuteAllOperations()
        {
            var rnd = new Random();

            foreach (var op in new[] { "+", "-", "*", "/" })
            {
                var request = new CalculateRequest
                {
                    X = rnd.Next(100),
                    Y = rnd.Next(100),
                    Op = op
                };
                var reply = _client.Calculate(request);
                Console.WriteLine($"{request.X} {request.Op} {request.Y} = {reply.Result}");
            }
        }
    }
}