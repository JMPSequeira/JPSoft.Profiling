using System;
using System.Diagnostics;

namespace JPSoft.Profiling
{
    interface IOutput
    {
        void WriteLine(string output);
    }

    class ConsoleOutput : IOutput
    {
        public void WriteLine(string output) => Console.WriteLine(output);
    }

    class DebugOutput : IOutput
    {
        public void WriteLine(string output) => Debug.WriteLine(output);
    }
}