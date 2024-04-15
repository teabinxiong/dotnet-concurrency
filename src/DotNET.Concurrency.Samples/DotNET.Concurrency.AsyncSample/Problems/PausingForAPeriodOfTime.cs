using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.AsyncSample.Problems
{
    /**
     * 
     * Problem
     *   You need to (asynchronously) wait for a period of time. 
     *   This is a common scenario when unit testing or implementing retry delays. 
     *   It also comes up when coding simple timeouts.
     * */

    /**
     * 
     * The Task type has a static method Delay that returns a task that completes after the specified time.
     * 
     * */

    public sealed class PausingForAPeriodOfTime
    {
        public async Task<T> DelayResult<T>(T result, TimeSpan delay)
        {
            await Task.Delay(delay);
            return result;
        }
    }
}
