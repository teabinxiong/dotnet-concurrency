using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.AsynchronousStreamsSample.Problems
{
    public sealed class UsingLinqWithAsynchronousStreams
    {
        /*
         * 
         * Problem:
         * You want to process an asynchronous stream using well-defined and well-tested operators like LINQ.
         * 
         */

        /*
         * Solution:
         * IEnumerable<T> has LINQ to Objects, and IObservable<T> has LINQ to Events. 
         * Both of these have libraries of extension methods that define operators you can use to build queries. 
         * IAsyncEnumerable<T> also has LINQ support, provided by the .NET community in the System.Linq.Async NuGet package.
         * 
         */


        public async Task run()
        {
            IAsyncEnumerable<int> values = SlowRange().WhereAwait(
                async value =>
                {
                    // Do some asynchronous work to determine
                    //  if this element should be included.
                    await Task.Delay(10);
                    return value % 2 == 0;
                });

            await foreach (int result in values)
            {
                Console.WriteLine(result);
            }
        }



        // Produce sequence that slows down as it progresses.
        public async IAsyncEnumerable<int> SlowRange()
        {
            for (int i = 0; i != 10; ++i)
            {
                await Task.Delay(i * 100);
                yield return i;
            }
        }

    }
}
