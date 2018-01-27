using System;
using System.Threading;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    static class TestTaskCreator
    {
        public static Task Create(Test test)=>(test.TimeOut == default)?
            GetTestTask(test.Iterations, test.Code):
            GetTaskWithTimeOutFor(test);

        static Task GetTaskWithTimeOutFor(Test test)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;

            var testTask = GetTestTask(test.Iterations, test.Code, token);

            var compositeTask = new Task(
                ()=>
                {
                    testTask.Start();

                    var first = Task.WhenAny(testTask, Task.Delay(test.TimeOut));

                    if (first.IsFaulted)
                        throw first.Exception;
                    if (first.Result != testTask)
                    {
                        source.Cancel();
                        throw new TimeoutException(
                            $"TimeOut: {test.TimeOut.Milliseconds}ms"
                        );
                    }
                }
            );

            return compositeTask;
        }

        static Task GetTestTask(
            long iterations,
            Action action,
            CancellationToken token = default)=> new Task(()=>
        {
            for (int i = 0; i < iterations; i++)
            {
                action();
            }
        }, token);
    }
}