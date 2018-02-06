using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    public static class Profiler
    {
        static IOutput _output;
        static bool _hasOutput = _output != null;
        static List<Profile> _profiles = new List<Profile>();
        static List<ITestInternal> _tests = new List<ITestInternal>();
        public static IEnumerable<Profile> GetProfiles() => _profiles;
        public static IEnumerable<ITest> GetTests() => _tests;

        public static ITest BuildTest(Func<ITestBuilder, ITestOptions> buildingOptions) => BuildInternal(buildingOptions);

        public static Profile Run(ITest test) => Run(ValidateTest(test));

        public static IEnumerable<Profile> RunMultiple(ITest test, int times, bool stopOnException = false)
        {
            var internalTest = ValidateTest(test);

            return RunMultiple(internalTest, times, stopOnException);
        }

        public static Profile BuildAndRunTest(Func<ITestBuilder, ITestOptions> buildingOptions) => Run(BuildInternal(buildingOptions));
        public static IEnumerable<Profile> BuildAndRunTestMultiple(Func<ITestBuilder, ITestOptions> buildingOptions, int times, bool stopOnException) => RunMultiple(BuildInternal(buildingOptions), times, stopOnException);

        public static void SetOutput(IOutput output) => _output = output;

        static ITestInternal ValidateTest(ITest test) => test is ITestInternal internalTest ? internalTest : throw new ArgumentException($"ITest {test.Name} is not a valid test.");

        static Profile Run(ITestInternal internalTest)
        {
            var runner = new TestTaskRunner(internalTest);

            var informer = new InformationOutput(_output);

            if (_hasOutput)
                informer.Start(internalTest.Name);

            runner.Run();

            if (_hasOutput)
                informer.Stop(runner.RunTime, runner.Exception);

            var profile = new Profile(runner);

            _profiles.Add(profile);

            return profile;
        }

        static IEnumerable<Profile> RunMultiple(ITestInternal test, int times, bool stopOnException = false)
        {
            int i = 0;

            var profiles = new List<Profile>(times);

            for (; i < times; i++)
            {
                var profile = Run(test);

                profiles.Add(profile);

                if (stopOnException && profile.TaskRunStatus == TaskStatus.Faulted)
                    break;
            }

            return profiles;
        }

        static ITestInternal BuildInternal(Func<ITestBuilder, ITestOptions> buildingOptions)
        {
            var test = new TestBuilder(buildingOptions).Test;

            _tests.Add(test);

            return test;
        }
    }
}