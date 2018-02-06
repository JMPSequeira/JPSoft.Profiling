using System;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    interface ITestTaskRunner
    {
        ITestInternal Test { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        double RunTime { get; }
        Task TestTask { get; }
        Exception Exception { get; }
    }
}