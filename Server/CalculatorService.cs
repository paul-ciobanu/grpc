using System.Threading.Tasks;
using Grpc.Core;
using Shared;

namespace Server
{
    internal class CalculatorService : CalcService.CalcServiceBase
    {
        public override Task<CalculateReply> Calculate(CalculateRequest request, ServerCallContext context)
        {
            long result = -1;
            switch (request.Op)
            {
                case "+":
                    result = request.X + request.Y;
                    break;

                case "-":
                    result = request.X - request.Y;
                    break;

                case "*":
                    result = request.X * request.Y;
                    break;

                case "/":
                    if (request.Y != 0)
                    {
                        result = request.X / request.Y;
                    }
                    break;

                default:
                    break;
            }
            return Task.FromResult(new CalculateReply { Result = result });
        }
    }
}