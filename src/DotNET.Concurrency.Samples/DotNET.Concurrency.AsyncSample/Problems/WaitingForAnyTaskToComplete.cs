using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNET.Concurrency.AsyncSample.Problems
{
    /*
     * Problem:
     * You have several tasks and need to respond to just one of them that’s completing.
     * 
     * You’ll encounter this problem most commonly when you have multiple independent attempts at an operation, 
     * with a first-one-takes-all kind of structure. 
     * 
     * For example, you could request stock quotes from multiple web services simultaneously, 
     * but you only care about the first one that responds.
     */

    /*
     * Solution:
     * Use the Task.WhenAny method. 
     * 
     * The Task.WhenAny method takes a sequence of tasks and returns a task that completes when any of the tasks complete. 
     * 
     * The result of the returned task is the task that completed. 
     * 
     */
    public sealed class WaitingForAnyTaskToComplete
    {
        // Returns the length of data at the first URL to respond.
        public async Task<int> FirstRespondingUrlAsync(HttpClient client, string urlA, string urlB)
        {
            // Start both downloads concurrently.
            Task<byte[]> downloadTaskA = client.GetByteArrayAsync(urlA);
            Task<byte[]> downloadTaskB = client.GetByteArrayAsync(urlB);

            // Wait for either of the tasks to complete.
            Task<byte[]> completedTask = await Task.WhenAny(downloadTaskA, downloadTaskB);

            // Return the length of the data retrieved from that URL.
            byte[] data = await completedTask;
            return data.Length;
        }
    }
}
