using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.ParallelSample.Problems
{
    /*
     * Problem:
     * At the conclusion of a parallel operation, you need to aggregate the results. 
     * Examples of aggregation are summing up values or finding their average.
     * 
     */

    /*
     * The Parallel class supports aggregation through the concept of local values, 
     * which are variables that exist locally within a parallel loop. 
     * This means that the body of the loop can just access the value directly, 
     * without needing synchronization. 
     * 
     * When the loop is ready to aggregate each of its local results, it does so with the localFinally delegate. 
     * Note that the localFinally delegate does need to synchronize access to the variable that holds the final result. 
     * 
     */

    public class ParallelAggregation
    {
        public int ParallelSum(IEnumerable<int> values)
        {
            object mutex = new object();
            int result = 0;
            Parallel.ForEach(source: values,
                localInit: () => 0,
                body: (item, state, localValue) => localValue + item,
                localFinally: localValue =>
                {
                    lock (mutex)
                        result += localValue;
                });
            return result;
        }

        public int ParallelSumPLinq(IEnumerable<int> values)
        {
            return values.AsParallel().Sum();
        }

        public int ParallelSumLinq2(IEnumerable<int> values)
        {
            return values.AsParallel().Aggregate(
                seed: 0,
                func: (sum, item) => sum + item
            );
        }
    }
}
