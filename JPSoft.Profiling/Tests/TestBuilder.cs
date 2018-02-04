using System;
using System.Collections.Generic;
using System.Linq;

namespace JPSoft.Profiling
{

    static class TestFactory
    {
        public static T Create<T>(params object[] args)
        where T : class => Activator.CreateInstance(typeof(T), args) as T;
    }

    public interface ITestBuilder
    {
        ITestOptions For(Action action);
        ITestParameterBuilder<T1> For<T1>(Action<T1> action);
        ITestParameterBuilder<T1, T2> For<T1, T2>(Action<T1, T2> action);
        ITestParameterBuilder<T1, T2, T3> For<T1, T2, T3>(Action<T1, T2, T3> action);
        ITestParameterBuilder<T1, T2, T3, T4> For<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action);
        ITestParameterBuilder<T1, T2, T3, T4, T5> For<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action);
    }

    class TestBuilder : ITestBuilder, ITestHolder
    {
        public ITestInternal Test { get; private set; }

        TReturn GetTestHolder<TReturn, TTest, TAction>(TAction action)
        where TTest : AbstractTest<TAction>
            where TReturn : class
            {
                Test = TestFactory.Create<TTest>(action);

                return Activator.CreateInstance(typeof(TReturn), Test) as TReturn;
            }

        public ITestOptions For(Action action) => GetTestHolder<TestOptions, Test, Action>(action);

        public ITestParameterBuilder<T1> For<T1>(Action<T1> action) => GetTestHolder<ParameterBuilder<T1>, Test<T1>, Action<T1>>(action);

        public ITestParameterBuilder<T1, T2> For<T1, T2>(Action<T1, T2> action) => GetTestHolder<ParameterBuilder<T1, T2>, Test<T1, T2>, Action<T1, T2>>(action);

        public ITestParameterBuilder<T1, T2, T3> For<T1, T2, T3>(Action<T1, T2, T3> action) => GetTestHolder<ParameterBuilder<T1, T2, T3>, Test<T1, T2, T3>, Action<T1, T2, T3>>(action);

        public ITestParameterBuilder<T1, T2, T3, T4> For<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) => GetTestHolder<ParameterBuilder<T1, T2, T3, T4>, Test<T1, T2, T3, T4>, Action<T1, T2, T3, T4>>(action);

        public ITestParameterBuilder<T1, T2, T3, T4, T5> For<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action) => GetTestHolder<ParameterBuilder<T1, T2, T3, T4, T5>, Test<T1, T2, T3, T4, T5>, Action<T1, T2, T3, T4, T5>>(action);
    }

}