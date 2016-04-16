using Funq;
using Quartz;
using Quartz.Spi;
using System;

namespace ServiceStackWithQuartz
{
    public class FunqJobFactory : IJobFactory
    {
        private readonly Container _container;

        public FunqJobFactory(Container container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            if (bundle == null) throw new ArgumentNullException(nameof(bundle));
            if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));

            var jobDetail = bundle.JobDetail;
            var newJob = (IJob)_container.TryResolve(jobDetail.JobType);

            if (newJob == null)
                throw new SchedulerConfigException(string.Format("Failed to instantiate Job {0} of type {1}", jobDetail.Key, jobDetail.JobType));

            return newJob;
        }

        public void ReturnJob(IJob job)
        {
            //Allows the job factory to destroy/cleanup the job if needed.
        }
    }
}
