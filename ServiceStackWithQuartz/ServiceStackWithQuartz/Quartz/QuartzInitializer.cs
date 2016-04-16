using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace ServiceStackWithQuartz
{
    public class QuartzInitializer : IQuartzInitializer
    {
        private readonly IJobFactory _jobFactory;

        public QuartzInitializer(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }

        public void Start()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler scheduler = schedFact.GetScheduler();
            scheduler.JobFactory = _jobFactory;
            scheduler.Start();

            /* Schedule HelloJob */
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("myTrigger", "group1")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInSeconds(10)
                  .RepeatForever())
              .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
