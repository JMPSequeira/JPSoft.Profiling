using System;
using System.Threading.Tasks;

namespace JPSoft.Profiling
{
    static class ProfileCreator
    {
        public static Profile Create(
            Test test,
            Task testTask,
            DateTime startTime,
            DateTime stopTime)
        {

            var profile = new Profile(test)
            {
                StartedOn = startTime,
                EndedOn = stopTime,
                Exception = testTask.Exception?.InnerException
            };

            return profile;
        }
    }
}