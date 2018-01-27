using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    public static class TestRunner
    {
        static IOutput _output;
        static List<Profile> Profiles = new List<Profile>();
        static List<Test> Tests = new List<Test>();

        public static Profile Run(Test test)
        {
            Tests.Add(test);

            var testTask = TestTaskCreator.Create(test);

            var informer = new InformationOutput(_output);

            informer.Start(test.Name);

            var startTime = DateTime.Now;

            var finished = Task.WhenAny(testTask);

            var stopTime = DateTime.Now;

            if (finished.IsFaulted)
                informer.Stop(finished.Exception.InnerException);
            else
                informer.Stop();

            var profile = ProfileCreator.Create(test, finished, startTime, stopTime);

            Profiles.Add(profile);

            return profile;
        }
    }
}