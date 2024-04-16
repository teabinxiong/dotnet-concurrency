using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.AsynchronousStreamsSample.Problems
{
    public sealed class AsynchronousStreamsAndCancellation
    {
        /*
         * Problem:
         * You want a way to cancel asynchronous streams.
         * 
         */

        /*
         * Solution:
         * use a CancellationToken
         * 
         */

        async Task ConsumeSequence(IAsyncEnumerable<int> items)
        {
            using var cts = new CancellationTokenSource(500);
            CancellationToken token = cts.Token;
            await foreach (int result in items.WithCancellation(token))
            {
                Console.WriteLine(result);
            }
        }

        // Produce sequence that slows down as it progresses.
        async IAsyncEnumerable<int> SlowRange(
            [EnumeratorCancellation] CancellationToken token = default)
        {
            for (int i = 0; i != 10; ++i)
            {
                await Task.Delay(i * 100, token);
                yield return i;
            }
        }

    }
}
