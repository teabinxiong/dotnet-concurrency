using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.AsyncSample.Problems
{
    /*
     * Problem:
     * When an async method resumes after an await, by default it will resume executing within the same context. 
     * This can cause performance problems if that context was a UI context and a large number of async methods are resuming on the UI context.
     * 
     */


    /*
     * Solution: 
     * To avoid resuming on a context, await the result of ConfigureAwait and pass false for its continueOnCapturedContext parameter:
     * 
     */
    public sealed class AvoidingContextForContinuations
    {
        async Task ResumeOnContextAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            // This method resumes within the same context.
        }

        async Task ResumeWithoutContextAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

            // This method discards its context when it resumes.
        }
    }
}
