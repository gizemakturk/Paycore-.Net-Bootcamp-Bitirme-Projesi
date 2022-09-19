using System;
namespace HangFireProject.Jobs

{
    public static class JobDelayed
    {
        public static string Run()
        {
            Console.WriteLine("JobDelayed");
            return "JobDelayed";
        }

        public static string Run(string name)
        {
            Console.WriteLine("JobDelayed");
            return "JobDelayed";
        }
    }
}
