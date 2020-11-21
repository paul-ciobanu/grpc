using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Shared;

namespace Server
{
    internal class TemperatureService : TempService.TempServiceBase
    {
        public override async Task Median(
            IAsyncStreamReader<Temperature> requestStream,
            IServerStreamWriter<Temperature> responseStream,
            ServerCallContext context)
        {
            var cachedValues = new List<double>();
            while (await requestStream.MoveNext())
            {
                var current = requestStream.Current;
                cachedValues.Add(current.Value);

                if (cachedValues.Count == 10)
                {
                    var arr = cachedValues.ToArray();
                    Array.Sort(arr);

                    var median = (arr[4] + arr[5]) / 2;
                    cachedValues.Clear();
                    await responseStream.WriteAsync(new Temperature { Timestamp = current.Timestamp, Value = median });
                }
            }
        }
    }
}