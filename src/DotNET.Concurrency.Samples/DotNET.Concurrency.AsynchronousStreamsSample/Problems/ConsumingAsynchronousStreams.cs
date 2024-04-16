using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.AsynchronousStreamsSample.Problems
{
    public sealed class ConsumingAsynchronousStreams
    {
        /*
         * Problem:
         * You need to process the results of an asynchronous stream, also known as an asynchronous enumerable.
         */

        /*
         * Solution:
         * Consuming an asynchronous operation is done via await, and consuming an enumerable is usually done via foreach. 
         * Consuming an asynchronous enumerable is done by combining these two into await foreach.
         * 
         */

        public async IAsyncEnumerable<string> GetValuesAsync(HttpClient client)
        {
            await Task.Delay(1000); // some asynchronous work
            yield return "10";
            await Task.Delay(1000); // more asynchronous work
            yield return "13";
        }

        public async Task ProcessValueAsync(HttpClient client)
        {
            await foreach (string value in GetValuesAsync(client))
            {
                await Task.Delay(100).ConfigureAwait(false); // asynchronous work
                Console.WriteLine(value);
            }
        }
    }
}
