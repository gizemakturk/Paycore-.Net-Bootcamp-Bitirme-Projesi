using System;

namespace HangFireProject.Jobs
{
    public static class JobContinuations
    {
        public static string Run()
        {
            Console.WriteLine("JobContinuations");
            return "JobContinuations";
        }
    }
}
