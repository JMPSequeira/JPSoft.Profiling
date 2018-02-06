using System;

namespace JPSoft.Profiling
{
    public interface ITestOptions
    {
        ITestOptions WithTimeout(double milliseconds);
        ITestOptions WithName(string name);
        ITestOptions WithIterations(long iterations);
    }
}