using System;

namespace JPSoft.Profiling
{
    public abstract class AbstractTest<TAction>
    {
        public Guid Guid { get; }
        public string Name { get; }
        public long Iterations { get; }
        public TAction Code { get; }
        public TimeSpan TimeOut { get; }
        internal AbstractTest(string name,
            long iterations,
            TAction code)
        {
            Guid = Guid.NewGuid();
            Name = name;
            Iterations = iterations;
            Code = code;
        }

        internal AbstractTest(string name,
            long iterations,
            TAction code,
            TimeSpan timeOut) : this(name, iterations, code) => TimeOut = timeOut;

    }
}