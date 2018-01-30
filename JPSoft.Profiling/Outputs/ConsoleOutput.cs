using System;

namespace JPSoft.Profiling
{
    public class ConsoleOutput : IOutput
    {
        public void WriteLine(string output) => Console.WriteLine(output);
    }
}