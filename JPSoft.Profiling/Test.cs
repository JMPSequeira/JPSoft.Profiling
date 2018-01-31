using System;

namespace JPSoft.Profiling
{
    public class Test : AbstractTest<Action>
    {
        public Test(string name, long iterations, Action code) : base(name, iterations, code) { }

        public Test(string name, long iterations, Action code, TimeSpan timeOut) : base(name, iterations, code, timeOut) { }
    }

}