using System;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    interface ITestInternal : ITest
    {
        new string Name { get; set; }
        new long Iterations { get; set; }
        new double Timeout { get; set; }
        void InsertParameter(object parameter);
    }
}