using System;
using System.Threading;
using JPSoft.Profiling;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ProfileTest
    {
        Profile _profile;

        [SetUp]
        public void Setup() => _profile = new Profile();

        [Test]
        public void Miliseconds_DatesSet_ResturnsValidDifference()
        {
            var date1 = DateTime.Now;

            Thread.Sleep(100);

            var date2 = DateTime.Now;

            var expected = (date2 - date1).TotalMilliseconds;

            _profile.StartedOn = date1;
            _profile.EndedOn = date2;

            Assert.AreEqual(expected, _profile.Miliseconds);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsSuccessful_TrueAndFalse_Expected(bool expected)
        {
            if (!expected)
                _profile.Exception = new Exception();

            Assert.AreEqual(expected, _profile.IsSuccessful);
        }
    }
}