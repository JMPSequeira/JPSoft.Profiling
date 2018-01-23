using System;

namespace JPSoft.Profiling
{
    public class Profile
    {
        public int Id { get; internal set; }
        public Test Test { get; internal set; }
        public DateTime StartedOn { get; internal set; }
        public DateTime EndedOn { get; internal set; }
        public double Miliseconds
            =>(EndedOn - StartedOn).TotalMilliseconds;
        public double MilisecondsPerIteration
            => Miliseconds / Iterations;
        public double IterationsPerMilisecond
            => Iterations / Miliseconds;
        public long Iterations { get; internal set; }
        public string Name { get; internal set; }
        public Exception Exception { get; internal set; }
        public bool IsSuccessful
            => Exception is null;
    }

    public class Test
    {
        public int Id { get; internal set; }
        public long Iterations { get; internal set; }
        public TimeSpan TimeOut { get; internal set; }
        public Action Code { get; internal set; }
    }
}