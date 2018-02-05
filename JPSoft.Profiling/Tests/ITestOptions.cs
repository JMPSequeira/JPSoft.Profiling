using System;

namespace JPSoft.Profiling
{
    public interface ITestOptions
    {
        ITestOptions WithTimeout(int miliseconds);
        ITestOptions WithName(string name);
        ITestOptions WithIterations(long iterations);
    }
}