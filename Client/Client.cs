using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace Client
{
    internal class Client
    {
        private static async Task Main(string[] args)
        {
            var host = args.Any() ? args[0] : "localhost";
            var port = args.Length == 2 ? int.Parse(args[1]) : 9000;

            var channel = new Channel(host, port, ChannelCredentials.Insecure);

            var calcClient = new CalculatorClient(channel);
            calcClient.ExecuteAllOperations();
            Console.WriteLine($"Press any key to run streaming example...");
            Console.ReadKey();

            var client = new TemperatureClient(channel);
            await client.Execute();
        }
    }
}