using System;
using System.Collections.Generic;

namespace JPSoft.Profiling
{
    public interface ITest
    {
        Guid Guid { get; }
        string Name { get; }
        long Iterations { get; }
        TimeSpan Timeout { get; }
        byte ParameterCount { get; }
        bool TryGetParameters(out IEnumerable<object> parameters);
    }
}