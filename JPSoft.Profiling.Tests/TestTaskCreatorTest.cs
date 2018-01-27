using System;
using System.Threading;
using System.Threading.Tasks;
using JPSoft.Profiling;
using NUnit.Framework;

namespace JPSoft.Profiling.Tests
{
    [TestFixture]
    public class TestTaskCreatorTest
    {
        [Test]
        public void Create_NoTimeOut_NormalTask()
        {
            var test = new Test("Test", 1, () =>
            {
                var a = 1 + 1;
                var b = a + 2;
            });

            var task = TestTaskCreator.Create(test);

            task.Start();

            Task.WaitAll(task);

            Assert.IsTrue(task.IsCompletedSuccessfully);
        }

        [Test]
        public void Create_WithTimeOut_TimeOutException()
        {
            var time = new TimeSpan(0, 0, 0, 0, 500);

            var test = new Test("Test", 1, () =>
                {
                    Thread.Sleep(1000);
                },
                time
            );

            var task = TestTaskCreator.Create(test);

            Assert.Throws(typeof(AggregateException), () => { task.Start(); task.Wait(); });
            Assert.IsTrue(task.Exception.InnerException.GetType() == typeof(TimeoutException));
        }

        [Test]
        public void Create_WithFaulted_ExceptionExcepion()
        {
            var test = new Test("Test", 1, () =>
            {
                var a = 0;
                var b = 2 / a;
            });

            var task = TestTaskCreator.Create(test);

            Assert.Throws(typeof(AggregateException), () => { task.Start(); task.Wait(); });
        }
    }
}