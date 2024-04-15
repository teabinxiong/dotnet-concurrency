using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.AsyncSample.Problems
{
    /*
     * Problem: 
     * You have several tasks and need to wait for them all to complete.
     * 
     */

    /*
     * Solution:
     * The framework provides a Task.WhenAll method for this purpose. 
     * This method takes several tasks and returns a task that completes when all of those tasks have completed:
     * 
     */
    public sealed class WaitingForASetOfTasksToComplete
    {
        public async Task<int[]> ReturnNumbers()
        {
            Task<int> task1 = Task.FromResult(3);
            Task<int> task2 = Task.FromResult(5);
            Task<int> task3 = Task.FromResult(7);

            int[] results = await Task.WhenAll(task1, task2, task3);

            return results;
        }


        #region "Capture errors when using Task.WhenAll()"
        async Task ThrowNotImplementedExceptionAsync()
        {
            throw new NotImplementedException();
        }

        async Task ThrowInvalidOperationExceptionAsync()
        {
            throw new InvalidOperationException();
        }

        async Task ObserveOneExceptionAsync()
        {
            var task1 = ThrowNotImplementedExceptionAsync();
            var task2 = ThrowInvalidOperationExceptionAsync();

            try
            {
                await Task.WhenAll(task1, task2);
            }
            catch (Exception ex)
            {
                // "ex" is either NotImplementedException or InvalidOperationException.
            }
        }

        async Task ObserveAllExceptionsAsync()
        {
            var task1 = ThrowNotImplementedExceptionAsync();
            var task2 = ThrowInvalidOperationExceptionAsync();

            Task allTasks = Task.WhenAll(task1, task2);
            try
            {
                await allTasks;
            }
            catch
            {
                AggregateException allExceptions = allTasks.Exception;
            }
        }
        #endregion
    }
}
