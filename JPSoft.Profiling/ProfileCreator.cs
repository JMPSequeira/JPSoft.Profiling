using System;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    static class ProfileCreator
    {
        public static Profile Create(
            Test test,
            Task<Task> finished,
            DateTime startTime,
            DateTime stopTime)
        {
            var testTask = finished.Result;

            var profile = new Profile(test)
            {
                StartedOn = startTime,
                EndedOn = stopTime,
                Exception = testTask.Exception
            };

            return profile;
        }
    }
}