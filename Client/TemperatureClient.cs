using System;
using System.Threading.Tasks;
using Grpc.Core;
using Shared;

namespace Client
{
    internal class TemperatureClient
    {
        private readonly TempService.TempServiceClient _client;

        public TemperatureClient(Channel channel)
        {
            _client = new TempService.TempServiceClient(channel);
        }

        public async Task Execute()
        {
            using var duplex = _client.Median();
            var responseTask = Task.Run(async () =>
            {
                while (await duplex.ResponseStream.MoveNext())
                {
                    var response = duplex.ResponseStream.Current;
                    Console.WriteLine($"{response.Timestamp}: {response.Value}");
                }
            });
            var ts = 1;
            var temp = 28.0;
            var rnd = new Random();
            while (true)
            {
                await duplex.RequestStream.WriteAsync(new Temperature { Timestamp = ts, Value = temp });
                ts += 1;
                temp += rnd.NextDouble() - 0.5;
            }
        }
    }
}