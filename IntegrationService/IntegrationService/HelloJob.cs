using Quartz;
using Quartz.Spi;
using System;

namespace IntegrationService
{
    public class HelloJob : IJob
    {
        public HelloJob()
        {
        }

        public virtual void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(string.Format("Hello World! - {0}", System.DateTime.Now.ToString("r")));
        }
    }
}
