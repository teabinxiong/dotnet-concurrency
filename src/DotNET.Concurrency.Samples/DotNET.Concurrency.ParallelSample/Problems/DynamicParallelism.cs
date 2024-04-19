using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DotNET.Concurrency.ParallelSample.Problems
{
    public sealed class DynamicParallelism
    {
        /**
         * Problem:
         * You have a more complex parallel situation where the structure and number of parallel tasks depend on information known only at runtime.
         * 
         * 
         */

        /**
         * Solution:
         * The Task Parallel Library (TPL) is centered around the Task type. The Parallel class and Parallel LINQ are just convenience wrappers around the powerful Task. When you need dynamic parallelism, it’s easiest to use the Task type directly.
         * 
         */
        public void Traverse(Node current)
        {
            //DoExpensiveActionOnNode(current);
            if (current.Left != null)
            {
                Task.Factory.StartNew(
                    () => Traverse(current.Left),
                    CancellationToken.None,
                    TaskCreationOptions.AttachedToParent,
                    TaskScheduler.Default);
            }
            if (current.Right != null)
            {
                Task.Factory.StartNew(
                    () => Traverse(current.Right),
                    CancellationToken.None,
                    TaskCreationOptions.AttachedToParent,
                    TaskScheduler.Default);
            }
        }

        public void ProcessTree(Node root)
        {
            Task task = Task.Factory.StartNew(
                () => Traverse(root),
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.Default);
            task.Wait();
        }

        public class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Data { get; set; }
        }

    }
}
