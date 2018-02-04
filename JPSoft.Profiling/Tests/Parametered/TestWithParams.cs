using System;

namespace JPSoft.Profiling
{
    class Test<T> : AbstractTestWithParams<Action<T>>
    {
        public Test(Action<T> code) : base(code) { }
    }
    class Test<T1, T2> : AbstractTestWithParams<Action<T1, T2>>
    {
        public Test(Action<T1, T2> code) : base(code) { }
    }
    class Test<T1, T2, T3> : AbstractTestWithParams<Action<T1, T2, T3>>
    {
        public Test(Action<T1, T2, T3> code) : base(code) { }
    }
    class Test<T1, T2, T3, T4> : AbstractTestWithParams<Action<T1, T2, T3, T4>>
    {
        public Test(Action<T1, T2, T3, T4> code) : base(code) { }

    }

    class Test<T1, T2, T3, T4, T5> : AbstractTestWithParams<Action<T1, T2, T3, T4, T5>>
    {
        public Test(Action<T1, T2, T3, T4, T5> code) : base(code) { }
    }
}