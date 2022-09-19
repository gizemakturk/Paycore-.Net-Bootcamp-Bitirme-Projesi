using System;
namespace HangFireProject.Jobs

{
    public static class JobRecurring
    {
        public static string Run()
        {
            Console.WriteLine("JobRecurring");
            return "JobRecurring";
        }
    }
}
