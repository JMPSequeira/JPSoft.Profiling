using System.Diagnostics;

namespace JPSoft.Profiling
{
    class DebugOutput : IOutput
    {
        public void WriteLine(string output) => Debug.WriteLine(output);
    }
}