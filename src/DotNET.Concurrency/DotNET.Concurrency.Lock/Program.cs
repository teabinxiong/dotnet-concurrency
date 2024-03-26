// The lock keyword ensures that one thread does not enter a critical section of code while another thread
// is in the critical section. If another thread tries to enter a locked code,
// it will wait, block, until the object is released.
const int iterations = 10000;
var counter = 0;
var lockFlag = new object();

ThreadStart proc = () =>
{

    for (int i = 0; i < iterations; i++)
    {
        lock (lockFlag)
            counter++;

        Thread.SpinWait(100);

        lock (lockFlag)
            counter--;
    }
};

var threads = Enumerable
    .Range(0, 8)
    .Select(n => new Thread(proc))
    .ToArray();

foreach(var thread in threads)
{
    // waithing the threads 
    thread.Start();
}

foreach(var thread in threads)
{
    // waithing for thread to complete
    thread.Join();
}
Console.WriteLine(counter);