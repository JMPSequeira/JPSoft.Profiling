using System;

namespace JPSoft.Profiling
{
    public class Profile
    {
        public Profile(Test test)
        {
            this.Id = Guid.NewGuid();
            this.Test = test;
            this.Iterations = test.Iterations;
        }
        public Guid Id { get; }
        public Test Test { get; }
        public DateTime StartedOn { get; internal set; }
        public DateTime EndedOn { get; internal set; }
        public double Miliseconds
            =>(EndedOn - StartedOn).TotalMilliseconds;
        public double MilisecondsPerIteration
            => Miliseconds / Iterations;
        public double IterationsPerMilisecond
            => Iterations / Miliseconds;
        public long Iterations { get; }
        public Exception Exception { get; internal set; }
        public bool IsSuccessful
            => Exception is null;
    }
}