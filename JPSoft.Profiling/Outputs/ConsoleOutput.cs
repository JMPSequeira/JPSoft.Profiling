using System;

namespace JPSoft.Profiling
{
    class ConsoleOutput : IOutput
    {
        public void WriteLine(string output) => Console.WriteLine(output);
    }
}