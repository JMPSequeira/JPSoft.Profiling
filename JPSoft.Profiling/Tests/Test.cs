using System;
using System.Threading.Tasks;
using System.Timers;

namespace JPSoft.Profiling
{
    class Test : AbstractTest<Action>
    {
        public Test(Action code) : base(code) { }
    }
}