using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    class TestTaskRunner : ITestTaskRunner
    {
        readonly Action _action;
        readonly CancellationTokenSource _source;
        readonly CancellationToken _token;
        readonly System.Timers.Timer _timer;
        public Task TestTask { get; private set; }
        public ITestInternal Test { get; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public Exception Exception => TestTask.Exception?.InnerException;

        public TestTaskRunner(ITestInternal test)
        {

            var process = new Process();

            Test = test;

            _action = TestActionFactory.Create(Test);

            _source = new CancellationTokenSource();

            _token = _source.Token;

            _timer = new System.Timers.Timer(Test.Timeout.TotalMilliseconds);

            _timer.AutoReset = false;

            _timer.Elapsed += (sender, args) => _source.Cancel();
        }

        public void Run()
        {
            StartTime = DateTime.Now;

            TestTask = Task.Run(_action, GetCancellationToken());

            Task.WaitAny(TestTask);

            EndTime = DateTime.Now;
        }

        CancellationToken GetCancellationToken()
        {
            if (_timer.Interval == default)
                return default;

            _timer.Start();

            return _token;
        }
    }
}