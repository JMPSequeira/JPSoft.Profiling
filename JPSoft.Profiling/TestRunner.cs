using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    public static class TestRunner
    {
#if DEBUG 
        static IOutput _output = new DebugOutput();
#else
        static IOutput _output = new ConsoleOutput();
#endif
        static bool _hasOutput = true;
        static List<Profile> Profiles = new List<Profile>();
        static List<Test> Tests = new List<Test>();

        public static Profile Run(Test test)
        {
            Tests.Add(test);

            var testTask = TestTaskCreator.Create(test);

            var informer = new InformationOutput(_output);

            if (_hasOutput)
                informer.Start(test.Name);

            var startTime = DateTime.Now;

            testTask.Start();

            while (!testTask.IsCompleted)
            {

            }

            var stopTime = DateTime.Now;

            if (_hasOutput)
                if (testTask.IsFaulted)
                    informer.Stop(testTask.Exception.InnerException);
                else
                    informer.Stop();

            var profile = ProfileCreator.Create(test, testTask, startTime, stopTime);

            Profiles.Add(profile);

            return profile;
        }
    }
}