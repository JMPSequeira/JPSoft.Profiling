using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    class TestTaskRunner : ITestTaskRunner
    {
        readonly Action<CancellationToken> _action;
        readonly CancellationTokenSource _source;
        readonly CancellationToken _token = CancellationToken.None;
        readonly System.Timers.Timer _timer;
        public Task TestTask { get; private set; }
        public ITestInternal Test { get; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public Exception Exception => TestTask.Exception?.InnerException;

        public double RunTime { get; private set; }

        public TestTaskRunner(ITestInternal test)
        {
            Test = test;

            _action = TestActionFactory.Create(Test);

            _source = new CancellationTokenSource();

            if (Test.Timeout > 0)
            {
                _token = _source.Token;

                _timer = new System.Timers.Timer(Test.Timeout);

                _timer.AutoReset = false;

                _timer.Elapsed += (sender, args) => _source.Cancel();
            }
        }

        public void Run()
        {
            var stopwatch = new Stopwatch();

            StartTime = DateTime.Now;

            stopwatch.Start();

            TestTask = Task.Run(() => _action(StartTimerAndGetCancellationToken()), _token);

            Task.WaitAny(TestTask);

            stopwatch.Stop();

            RunTime = stopwatch.ElapsedMilliseconds;

            EndTime = StartTime.AddMilliseconds(RunTime);
        }

        CancellationToken StartTimerAndGetCancellationToken()
        {
            if (_timer != null)
                _timer.Start();

            return _token;
        }
    }
}