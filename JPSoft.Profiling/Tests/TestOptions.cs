using System;

namespace JPSoft.Profiling
{

    class TestOptions : ITestOptions, ITestHolder
    {
        public ITestInternal Test { get; }

        public TestOptions(ITestInternal test) => Test = test;

        public ITestOptions WithIterations(long iterations)
        {
            Test.Iterations = iterations;

            return this;
        }

        public ITestOptions WithName(string name)
        {
            Test.Name = name;

            return this;
        }

        public ITestOptions WithTimeout(TimeSpan time)
        {
            Test.Timeout = time;

            return this;
        }
    }

}