using System;
using System.Linq;
using Grpc.Core;
using Shared;

namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var port = args.Any() ? int.Parse(args[0]) : 9000;
            var server = new Grpc.Core.Server
            {
                Services =
                {
                    CalcService.BindService(new CalculatorService()),
                    TempService.BindService(new TemperatureService())
                },
                Ports = { new ServerPort("0.0.0.0", port, ServerCredentials.Insecure) }
            };
            server.Start();
            Console.WriteLine($"Server listening at port {port}. Press any key to terminate...");
            Console.ReadKey();
        }
    }
}