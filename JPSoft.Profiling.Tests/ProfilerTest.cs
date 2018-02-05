using System;
using JPSoft.Profiling;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

public partial class TestActionFactoryTest
{
    [TestFixture]
    public class ProfilerTest
    {
        [Test]
        public void BuildTest_TestOptions_ITest()
        {

            ITest test = GetTest();

            Assert.IsInstanceOf(typeof(Test<int, int>), test);
        }

        [Test]
        public void Run_ValidTest_Profile()
        {
            var test = GetTest();

            var profile = Profiler.Run(test);

            Assert.IsInstanceOf(typeof(Profile), profile);
        }

        [Test]
        public void Run_ExceedTimeout_CanceledStatus()
        {
            var test = new Test(() => { var a = 2 + 5; var b = a + 5; });

            test.Iterations = 10000000000;

            test.Timeout = new TimeSpan(0, 0, 0, 1);

            var profile = Profiler.Run(test);

            Assert.IsTrue(profile.TaskRunStatus == TaskStatus.Canceled);
        }

        [Test]
        public void Run_ActionWithException_FaultedStatus()
        {
            var test = new Test(() =>
            {
                var a = 0;
                var b = 1 / a;
            });

            test.Iterations = 100;

            var profile = Profiler.Run(test);

            Assert.IsTrue(profile.TaskRunStatus == TaskStatus.Faulted);
        }

        static ITest GetTest()
        {
            Action<int, int> action = (i, j) => { var a = i + j; };

            return Profiler.BuildTest(
                (builder) =>
                builder.For(action)
                .WithParameter(2)
                .WithParameter(9)
                .WithIterations(100)
                .WithName("Test")
            );
        }
    }
}