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

        public static Profile Run(ITest test)
        {
            var internalTest = ValidateTest(test);

            var runner = new TestTaskRunner(internalTest);

            var informer = new InformationOutput(_output);

            if (_hasOutput)
                informer.Start(test.Name);

            runner.Run();

            if (_hasOutput)
                informer.Stop(runner.EndTime - runner.StartTime, runner.Exception);

            var profile = new Profile(runner);

            _profiles.Add(profile);

            return profile;
        }

        public static void SetOutput(IOutput output) => _output = output;

        public static IEnumerable<Profile> GetProfiles() => _profiles;
        public static IEnumerable<ITest> GetTests() => _tests;

        public static ITest BuildTest(Expression<Func<ITestBuilder, ITestOptions>> buildingOptions)
        {
            var builder = new TestBuilder();

            buildingOptions.Compile() (builder);

            var test = builder.Test;

            _tests.Add(test);

            return test;
        }

        static ITestInternal ValidateTest(ITest test)
        {
            if (test is ITestInternal internalTest)
                return internalTest;

            throw new ArgumentException($"ITest {test.Name} is not a valid test.");
        }
    }
}