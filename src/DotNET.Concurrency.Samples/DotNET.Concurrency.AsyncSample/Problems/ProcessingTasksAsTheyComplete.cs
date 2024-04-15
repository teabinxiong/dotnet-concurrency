using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.AsyncSample.Problems
{
    /*
     * Problem:
     * You have a collection of tasks to await, and you want to do some processing on each task after it completes. 
     * However, you want to do the processing for each one as soon as it completes, not waiting for any of the other tasks.
     * 
     */

    /*
     * Solution:
     * The easiest solution is to restructure the code by introducing a higher-level async method that handles awaiting the task and processing its result. 
     * 
     */
    public sealed class ProcessingTasksAsTheyComplete
    {
        public async Task<int> DelayAndReturnAsync(int value)
        {
            await Task.Delay(TimeSpan.FromSeconds(value));
            return value;
        }

        public async Task AwaitAndProcessAsync(Task<int> task)
        {
            int result = await task;
            Trace.WriteLine(result);
        }

        // This method now prints "1", "2", and "3".
        async Task ProcessTasksAsync()
        {
            // Create a sequence of tasks.
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);
            Task<int> taskC = DelayAndReturnAsync(1);
            Task<int>[] tasks = new[] { taskA, taskB, taskC };

            IEnumerable<Task> taskQuery =
                from t in tasks select AwaitAndProcessAsync(t);

            Task[] processingTasks = taskQuery.ToArray();

            // Await all processing to complete
            await Task.WhenAll(processingTasks);
        }
    }
}
