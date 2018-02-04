using System;

namespace JPSoft.Profiling
{
    public class Profile
    {
        internal Profile(ITestTaskRunner result)
        {
            Guid = Guid.NewGuid();
            Test = result.Test;
            Iterations = Test.Iterations;
            StartedOn = result.StartTime;
            EndedOn = result.EndTime;
            Exception = result.Exception;
            TestRunStatus =
                result.TestTask.IsFaulted ?
                TestRunStatus.Faulted :
                result.TestTask.IsCompleted ?
                TestRunStatus.Cancelled :
                TestRunStatus.Successful;
        }
        public Guid Guid { get; }
        public ITest Test { get; }
        public DateTime StartedOn { get; }
        public DateTime EndedOn { get; }
        public double Miliseconds
            =>(EndedOn - StartedOn).TotalMilliseconds;
        public double MilisecondsPerIteration
            => Miliseconds / Iterations;
        public double IterationsPerMilisecond
            => Iterations / Miliseconds;
        public long Iterations { get; }
        public Exception Exception { get; }
        public bool IsSuccessful => TestRunStatus == TestRunStatus.Successful;
        public TestRunStatus TestRunStatus { get; }
    }

    public enum TestRunStatus
    {
        Cancelled,
        Successful,
        Faulted
    }
}