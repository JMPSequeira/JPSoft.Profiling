using System;

namespace JPSoft.Profiling
{
    public interface ITestOptions
    {
        ITestOptions WithTimeout(TimeSpan time);
        ITestOptions WithName(string name);
        ITestOptions WithIterations(long iterations);
    }
}