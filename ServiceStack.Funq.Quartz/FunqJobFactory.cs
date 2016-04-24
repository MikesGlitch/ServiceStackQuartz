using System;
using Funq;
using Quartz;
using Quartz.Spi;

namespace ServiceStack.Funq.Quartz
{
    /// <summary>
    ///     Resolve Quartz Job and it's dependencies from Funq container
    /// </summary>
    public class FunqJobFactory : IJobFactory
    {
        private readonly Container _container;

        /// <summary>
        /// Initialises a new instance of the FunqJobFactory
        /// </summary>
        /// <param name="container"></param>
        public FunqJobFactory(Container container)
        {
            _container = container;
        }

        /// <summary>
        ///     Called by the Quartz Scheduler at the time of the trigger firing
        ///     in order to produce a IJob instance on which to call execute
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        /// <returns></returns>
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

        /// <summary>
        ///     Allows the job factory to destroy/cleanup the job if needed
        /// </summary>
        /// <param name="job"></param>
        public void ReturnJob(IJob job)
        {
            if (job is IDisposable)
            {
                var disposableJob = (IDisposable)job;
                disposableJob.Dispose();
            }
        }
    }
}
