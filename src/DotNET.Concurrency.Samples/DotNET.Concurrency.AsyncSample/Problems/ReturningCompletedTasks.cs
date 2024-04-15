using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.AsyncSample.Problems
{
    /*
     * Problem:
     * You need to implement a synchronous method with an asynchronous signature. 
     * This situation can arise if you’re inheriting from an asynchronous interface 
     * or base class but want to implement it synchronously. 
     * This technique is particularly useful when unit testing asynchronous code, 
     * when you need a simple stub or mock for an asynchronous interface.
     * 
     */

    /*
     * Solution:
     * - You can use Task.FromResult to create and return a new Task<T> that is already completed with the specified value.
     * - For methods that don’t have a return value, you can use Task.CompletedTask, which is a cached Task that is successfully completed.
     * - Similarly, there’s a Task.FromCanceled for creating tasks that have already been canceled from a given CancellationToken.
     * - If it is possible for your synchronous implementation to fail, then you should capture exceptions and use Task.FromException to return them.
     * 
     */
    public sealed class ReturningCompletedTasks
    {
        interface IMyAsyncInterface
        {
            Task<int> GetValueAsync();

            Task DoSomethingAsync();

            Task<T> NotImplementedAsync<T>();

            Task<int> GetValueAsync(CancellationToken cancellationToken);
        }

        class MySynchronousImplementation : IMyAsyncInterface
        {
            public Task<int> GetValueAsync()
            {
                return Task.FromResult(13);
            }

            public Task DoSomethingAsync()
            {
                return Task.CompletedTask;
            }

            public Task<T> NotImplementedAsync<T>()
            {
                return Task.FromException<T>(new NotImplementedException());
            }

            public Task<int> GetValueAsync(CancellationToken cancellationToken)
            {
                if (cancellationToken.IsCancellationRequested)
                    return Task.FromCanceled<int>(cancellationToken);
                return Task.FromResult(13);
            }
        }
    }
}
