using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DotNET.Concurrency.AsynchronousStreamsSample.Problems
{
    /*
     * Problem:
     * You need to return multiple values, and each value may require some asynchronous work. This point is commonly reached from one of two paths:
     *
     *   -You have multiple values to return (as an IEnumerable<T>), and then need to add asynchronous work.
     *
     *   -You have a single asynchronous return (as a Task<T>), and then need to add other return values.
     * 
     */

    /*
     * Solution: 
     * 
     * Returning multiple values from a method can be done with yield return,
     * and asynchronous methods use async and await. 
     * 
     * With asynchronous streams, you can combine these two; 
     * just use a return type of IAsyncEnumerable<T>:
     */

    public sealed class CreatingAsynchronousStreams
    {
        public async IAsyncEnumerable<int> GetValuesAsync()
        {
            await Task.Delay(1000); // some asynchronous work
            yield return 10;
            await Task.Delay(1000); // more asynchronous work
            yield return 13;
        }
    }
}
