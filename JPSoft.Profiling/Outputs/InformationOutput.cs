using System;
using System.Threading;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    class InformationOutput
    {
        IOutput _output;
        public InformationOutput(IOutput output) => _output = output;

        const string RUN = "Running...";

        const string STOP = "Finished!";
        const string COMPLETION = "To Completion";
        const string FAILED = "Failed";
        const string EXCEPTION_FORMAT = "Exception: {0}\r\nMessage: {1}";
        const string START_FORMAT = "Test '{0}' started...";
        const string END_FORMAT = "Execution Time: {0} ms\r\nStatus: {1}";

        public void Start(string name)
        {
            _output.WriteLine(string.Format(START_FORMAT, name));
            _output.WriteLine(RUN);
        }
        public void Stop(double milliseconds, Exception exception = null)
        {
            _output.WriteLine(STOP);

            var endWith = "";

            if (exception is null)
                endWith = COMPLETION;
            else
                endWith += $"{FAILED}\r\n" +
                $"{string.Format(EXCEPTION_FORMAT, exception.GetType().Name, exception.Message)}";

            _output.WriteLine(string.Format(END_FORMAT, milliseconds, endWith));
        }

    }

}