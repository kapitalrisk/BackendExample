namespace CodeExamples
{
    public class TaskBasics
    {
        public void StartATask()
        {
            var hotTaskFromFactory = Task.Factory.StartNew(() => Console.WriteLine("foo"));
            var hotTaskFromRun = Task.Run(() => Console.WriteLine("bar")); // is basically a shortcut to Task.Factory.Run

            // Does the same thing but the task is not directly started, not used a lot
            var coldTask = new Task(() => Console.WriteLine("baz"));
            coldTask.Start();

            // All those examples are similar to starting a thread like so but NOT NECESSARLY
            new Thread(() => Console.WriteLine("sim")).Start();

            // However Tasks include more logic than straigh up starting threads...
        }

        public void StartATaskAndWait()
        {
            var task = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("foo");
            });
            Console.WriteLine(task.IsCompleted); // false
            task.Wait(); // block until task is completed, can pass a a timeout parameter like task.Wait(1000);
            Console.WriteLine(task.IsCompleted); // true
        }

        public void TaskReturnValue()
        {
            Task<IEnumerable<int>> taskWithResult = Task.Run(() => Enumerable.Range(1, 1000));
            Console.WriteLine("task running...");
            Console.WriteLine($"task execution finished, array size generated : {taskWithResult.Result.Count()}");

            // Using ".Result" is a blocking operation
            // Task<TResult> can be though of as a "future" in that it encapsulate a "Result" that become available in the future
            // In java or other well knowned langages the therm "future" is often used comapred to "tasks" in C#
        }

        public async void ContinuationTasks()
        {
            // Verbose
            var taskWithAwaiter = Task.Run(() => Enumerable.Range(1, 1000));
            var taskAwaiter = taskWithAwaiter.GetAwaiter();
            taskAwaiter.OnCompleted(() =>
            {
                int result = taskAwaiter.GetResult().Count();
                Console.WriteLine($"{result} elements in taskWithAwaiter");
            });

            // Using .ContinueWith
            var taskWithContinueWith = Task.Run(() =>
            {
                return Enumerable.Range(1, 1000);
            }).ContinueWith((previousTaskResult) =>
            {
                Console.WriteLine($"{previousTaskResult.Result.Count()} elements in previousTaskResult");
            });

            // Using await keyword
            var result = await Task.Run(() => Enumerable.Range(1, 1000));
            Console.WriteLine($"{result} elements in task launched with await");
        }

        public async void AwaitMethodCall()
        {
            var result = await GetEnumerableAsync();
            Console.WriteLine($"result is {result}");
        }

        private Task<IEnumerable<int>> GetEnumerableAsync() => Task.Run(() => Enumerable.Range(1, 1000));

        public async void GetMultipleBigArrays()
        {
            // Using multiple manual tasks
            var taskToRun1 = Task.Run(() => Enumerable.Range(1, 1000));
            var taskToRun2 = Task.Run(() => Enumerable.Range(1, 1000));
            var taskToRun3 = Task.Run(() => Enumerable.Range(1, 1000));
            await Task.WhenAll(taskToRun1, taskToRun2, taskToRun3); // does the same as "await taskToRun1; await taskToRun2; await taskToRun3;", can pass also an array of tasks

            // A Task.WhenAny also exists to continue execution whenever one of the tasks finishes, can be usefull in some cases
        }

        public void ParrallelForEach()
        {
            Parallel.ForEach("Hello world", (c) => Console.Write(c));
        }
    }
}