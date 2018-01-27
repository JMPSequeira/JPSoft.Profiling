using System;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace JPSoft.Profiling.Tests
{
    [TestFixture]
    public class InformationOutputTest
    {
        TestOutPut testOut;
        InformationOutput informer;
        const string NAME = "Test";

        [SetUp]
        public void SetUp()
        {
            testOut = new TestOutPut();
            informer = new InformationOutput(testOut);
        }

        [Test]
        public void Start_Void_StartString()
        {
            var expected = $"Test '{NAME}' started...\r\nRunning...\r\n";

            informer.Start(NAME);

            Assert.IsTrue(expected == testOut.Output);
        }

        [Test]
        public void Finish_NoException_FinishString()
        {
            var expected = $"Test '{NAME}' started..." +
                "\r\nRunning...\r\nFinished!\r\nStatus: To Completion\r\n";

            informer.Start(NAME);

            Thread.Sleep(1);

            informer.Stop();

            Assert.IsTrue(expected == testOut.Output);
        }

        [Test]
        [TestCase("Test timedOut")]
        [TestCase("Argument null")]
        public void Fisnish_WithException_FinishString(string message)
        {
            var exception = new Exception(message);

            var expected = $"Test '{NAME}' started..." +
                "\r\nRunning...\r\nFinished!\r\nStatus: " +
                $"Failed\r\nException: {exception.GetType().Name}\r\nMessage: {message}\r\n";

            informer.Start(NAME);

            Thread.Sleep(10);

            informer.Stop(exception);

            Assert.IsTrue(expected == testOut.Output);
        }

        private class TestOutPut : IOutput
        {
            StringBuilder _builder = new StringBuilder();
            public string Output => _builder.ToString();
            public void Write(string output)
            {
                if (output.StartsWith('\b'))
                {
                    var length = output.Length;
                    _builder.Remove((_builder.Length) - length, length);
                }
                else
                    _builder.Append(output);
            }

            public void WriteLine(string output) => _builder.AppendLine(output);
        }
    }
}