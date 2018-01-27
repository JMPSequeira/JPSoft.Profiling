using System;
using System.Threading;
using NUnit.Framework;

namespace JPSoft.Profiling.Tests
{
    [TestFixture]
    public class TestRunnerTest
    {
        Test valid = new Test("Valid", 10, () =>
        {
            var a = 2;
            var b = a + 1;
        });

        Test invalid = new Test("Valid", 10, () =>
        {
            var a = 0;
            var b = 1 / a;
        });

        Test validtimeout = new Test("Valid", 10, () => Thread.Sleep(50), new TimeSpan(0, 0, 0, 0, 100));
        Test invalidtimeout = new Test("Valid", 10, () => Thread.Sleep(150), new TimeSpan(0, 0, 0, 0, 100));

        [Test]
        public void Run_ValidTest_Profile()
        {
            var profile = TestRunner.Run(valid);

            Assert.IsInstanceOf(typeof(Profile), profile);
        }

        [Test]
        public void Run_ValidTest_ValidProfile()
        {
            var profile = TestRunner.Run(valid);

            Assert.IsTrue(profile.IsSuccessful);
        }

        [Test]
        public void Run_TimeOutBiggerThanTest_HasTimeOutException()
        {
            var profile = TestRunner.Run(invalidtimeout);

            Assert.IsInstanceOf(typeof(TimeoutException), profile.Exception);
        }

        [Test]
        public void Run_InvalidTest_NotSuccessfulProfile()
        {
            var profile = TestRunner.Run(invalid);

            Assert.IsFalse(profile.IsSuccessful);
        }

        [Test]
        public void Run_TimeOutLessThanTest_SuccessfulProfile()
        {
            var profile = TestRunner.Run(validtimeout);

            Assert.IsFalse(profile.IsSuccessful);
        }
    }
}