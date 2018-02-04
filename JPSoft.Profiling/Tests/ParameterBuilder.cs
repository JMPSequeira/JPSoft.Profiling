using System.Collections.Generic;

namespace JPSoft.Profiling
{
    class ParameterBuilder<T1>
        : AbstractParameterBuilder<ITestOptions, TestOptions, T1>,
        ITestParameterBuilder<T1>
        {
            public ParameterBuilder(ITestInternal buildee) : base(new TestOptions(buildee)) { }
        }

    class ParameterBuilder<T1, T2>
        : AbstractParameterBuilder<ITestParameterBuilder<T2>, ParameterBuilder<T2>, T1>,
        ITestParameterBuilder<T1, T2>
        {
            public ParameterBuilder(ITestInternal buildee) : base(new ParameterBuilder<T2>(buildee)) { }
        }

    class ParameterBuilder<T1, T2, T3>
        : AbstractParameterBuilder<ITestParameterBuilder<T2, T3>, ParameterBuilder<T2, T3>, T1>,
        ITestParameterBuilder<T1, T2, T3>
        {
            public ParameterBuilder(ITestInternal buildee) : base(new ParameterBuilder<T2, T3>(buildee)) { }
        }

    class ParameterBuilder<T1, T2, T3, T4>
        : AbstractParameterBuilder<ITestParameterBuilder<T2, T3, T4>, ParameterBuilder<T2, T3, T4>, T1>,
        ITestParameterBuilder<T1, T2, T3, T4>
        {
            public ParameterBuilder(ITestInternal buildee) : base(new ParameterBuilder<T2, T3, T4>(buildee)) { }
        }
    class ParameterBuilder<T1, T2, T3, T4, T5>
        : AbstractParameterBuilder<ITestParameterBuilder<T2, T3, T4, T5>, ParameterBuilder<T2, T3, T4, T5>, T1>,
        ITestParameterBuilder<T1, T2, T3, T4, T5>
        {
            public ParameterBuilder(ITestInternal buildee) : base(new ParameterBuilder<T2, T3, T4, T5>(buildee)) { }

        }
}