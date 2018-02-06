using System;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    public class Profile
    {
        public Guid Guid { get; }
        public ITest Test { get; }
        public DateTime StartedOn { get; }
        public DateTime EndedOn { get; }
        public double Milliseconds { get; }

        public double IterationsPerMillisecond
            => Iterations / Milliseconds;
        public long Iterations { get; }
        public Exception Exception { get; }
        public bool IsSuccess
            => TaskRunStatus == TaskStatus.RanToCompletion;
        public TaskStatus TaskRunStatus { get; }

        internal Profile(ITestTaskRunner result)
        {
            Guid = Guid.NewGuid();
            Test = result.Test;
            Iterations = Test.Iterations;
            StartedOn = result.StartTime;
            EndedOn = result.EndTime;
            Exception = result.Exception;
            TaskRunStatus = result.TestTask.Status;
            Milliseconds = result.RunTime;
        }
    }
}