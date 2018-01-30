using System.Diagnostics;

namespace JPSoft.Profiling
{
    public class DebugOutput : IOutput
    {
        public void WriteLine(string output) => Debug.WriteLine(output);
    }
}