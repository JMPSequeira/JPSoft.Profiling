using System;

namespace JPSoft.Profiling
{
    public class Test
    {
        public Test(string name,
            long iterations,
            Action code)
        {
            Guid = Guid.NewGuid();
            Name = name;
            Iterations = iterations;
            Code = code;
        }

        public Test(string name,
            long iterations,
            Action code,
            TimeSpan timeOut) : this(name, iterations, code) => TimeOut = timeOut;

        public Guid Guid { get; }
        public string Name { get; }
        public long Iterations { get; }
        public Action Code { get; }
        public TimeSpan TimeOut { get; }
    }
}