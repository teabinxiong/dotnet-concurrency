using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.AsyncSample.Problems
{
    /*
     * Problem:
     * You need to respond to progress while an operation is executing.
     * 
     */

    /*
     * Solution:
     * Use the provided IProgress<T> and Progress<T> types. 
     * Your async method should take an IProgress<T> argument; the T is whatever type of progress you need to report.
     * 
     */

    public sealed class ReportingProgress
    {
        public async Task MyMethodAsync(IProgress<double> progress = null)
        {
            bool done = false;
            double percentComplete = 0;
            while (!done)
            {
                // do something here
                progress?.Report(percentComplete);
            }
        }
        public async Task CallMyMethodAsync()
        {
            var progress = new Progress<double>();
            progress.ProgressChanged += (sender, args) =>
            {
               // do something here
            };
            await MyMethodAsync(progress);
        }

    }
}
