using System;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    interface ITestInternal : ITest
    {
        new string Name { get; set; }
        new long Iterations { get; set; }
        new TimeSpan Timeout { get; set; }
        void AddParameter(object parameter);
        bool IsOfTypeTest<T>() where T : ITest;
        object GetAction();
    }
}