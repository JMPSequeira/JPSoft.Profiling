using System;

namespace JPSoft.Profiling
{
    public class Profile
    {
        public Guid Guid { get; internal set; }
        public DateTime StartedOn { get; internal set; }
        public DateTime EndedOn { get; internal set; }
        public double Miliseconds
            =>(EndedOn - StartedOn).TotalMilliseconds;
        public long Iterations { get; internal set; }
        public string Name { get; internal set; }
        public Exception Exception { get; internal set; }
        public bool IsSuccessful
            => Exception is null;
    }
}