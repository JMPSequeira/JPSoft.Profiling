using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        public static Profile BuildAndRunTest(Func<ITestBuilder, ITestOptions> buildingOptions) => Run(BuildInternal(buildingOptions));

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

        static ITestInternal BuildInternal(Func<ITestBuilder, ITestOptions> buildingOptions)
        {
            var test = new TestBuilder(buildingOptions).Test;

            _tests.Add(test);

            return test;
        }
    }
}