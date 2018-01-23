using System;

namespace JPSoft.Profiling
{
    public class Test
    {
        public int Id { get; internal set; }
        public long Iterations { get; internal set; }
        public TimeSpan TimeOut { get; internal set; }
        public Action Code { get; internal set; }
    }
}