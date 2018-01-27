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
        static List<Profile> _profiles = new List<Profile>();
        static List<Test> _tests = new List<Test>();

        public static Profile Run(Test test)
        {
            _tests.Add(test);

            var testTask = TestTaskCreator.Create(test);

            var informer = new InformationOutput(_output);

            if (_hasOutput)
                informer.Start(test.Name);

            var startTime = DateTime.Now;

            testTask.Start();

            while (!testTask.IsCompleted) { }

            var stopTime = DateTime.Now;

            var executionTime = startTime - stopTime;

            if (_hasOutput)
                if (testTask.IsFaulted)
                    informer.Stop(executionTime, testTask.Exception.InnerException);
                else
                    informer.Stop(executionTime);

            var profile = ProfileCreator.Create(test, testTask, startTime, stopTime);

            _profiles.Add(profile);

            return profile;
        }

        public static void SetOutput(IOutput output) => _output = output;

        public static void ToggleOutput(bool turnOn) => _hasOutput = turnOn;

        public static IEnumerable<Profile> GetProfiles() => _profiles;
        public static IEnumerable<Test> GetExecutedTests() => _tests;
    }
}