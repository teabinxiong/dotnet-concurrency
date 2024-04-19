using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.ParallelSample.Problems
{
    /*
     * Problem:
     * You have a collection of data, and you need to perform the same operation on each element of the data. 
     * This operation is CPU-bound and may take some time.
     * 
     */

    /*
     * Solution:
     * The Parallel type contains a ForEach method specifically designed for this problem. 
     * The following example takes a collection of matrices and rotates them all:
     * 
     */

    public sealed class ParallelProcessingOfData
    {
        public void RotateMatrices(IEnumerable<Matrix> matrices, float degrees)
        {
            Parallel.ForEach(matrices, matrix => matrix.Rotate(degrees));
        }

        void InvertMatrices(IEnumerable<Matrix> matrices)
        {
            Parallel.ForEach(matrices, (matrix, state) =>
            {
                if (!matrix.IsInvertible)
                    state.Stop(); // cancel processing here
                else
                    matrix.Invert();
            });
        }

        public void RotateMatrices(IEnumerable<Matrix> matrices, float degrees, CancellationToken token)
        {
            Parallel.ForEach(matrices,
                new ParallelOptions { CancellationToken = token },
                matrix => matrix.Rotate(degrees));
        }
    }
}
