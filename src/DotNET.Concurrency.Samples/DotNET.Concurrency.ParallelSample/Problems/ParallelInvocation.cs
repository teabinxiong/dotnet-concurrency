using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.ParallelSample.Problems
{
    /**
     * 
     * Problem:
     * You have a number of methods to call in parallel, and these methods are (mostly) independent of one another.
     * 
     */

    /*
     * Solution:
     * The Parallel class contains a simple Invoke member that is designed for this scenario. 
     * This example splits an array in half and processes each half independently:
     * 
     */
    public sealed class ParallelInvocation
    {
        public void ProcessArray(double[] array)
        {
            Parallel.Invoke(
                () => ProcessPartialArray(array, 0, array.Length / 2),
                () => ProcessPartialArray(array, array.Length / 2, array.Length)
            );
        }

        private void ProcessPartialArray(double[] array, int begin, int end)
        {
        }

        public void DoAction20Times(Action action, CancellationToken token)
        {
            Action[] actions = Enumerable.Repeat(action, 20).ToArray();
            Parallel.Invoke(new ParallelOptions { CancellationToken = token }, actions);
        }
    }
}
