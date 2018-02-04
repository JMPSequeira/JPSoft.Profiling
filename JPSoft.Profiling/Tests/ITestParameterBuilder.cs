namespace JPSoft.Profiling
{
    public interface ITestParameterBuilder<T1> : ITestParameterBase<ITestOptions, T1> { }
    public interface ITestParameterBuilder<T1, T2> : ITestParameterBase<ITestParameterBuilder<T2>, T1> { }
    public interface ITestParameterBuilder<T1, T2, T3> : ITestParameterBase<ITestParameterBuilder<T2, T3>, T1> { }
    public interface ITestParameterBuilder<T1, T2, T3, T4> : ITestParameterBase<ITestParameterBuilder<T2, T3, T4>, T1> { }
    public interface ITestParameterBuilder<T1, T2, T3, T4, T5> : ITestParameterBase<ITestParameterBuilder<T2, T3, T4, T5>, T1> { }
}