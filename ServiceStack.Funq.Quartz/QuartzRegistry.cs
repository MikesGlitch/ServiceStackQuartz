using System;
using System.Collections.Specialized;
using System.Linq;
using Funq;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace ServiceStack.Funq.Quartz
{
    /// <summary>
    ///     Registers Quartz Scheduler and Jobs with the ServiceStack Funq container
    /// </summary>
    public static class QuartzRegistry
    {
        /// <summary>
        ///     Registers Quartz Scheduler and Jobs from specified assembly, with specified config
        /// </summary>
        /// <param name="container"></param>
        /// <param name="jobsAssembly"></param>
        /// <param name="quartzConfig"></param>
        public static void RegisterQuartzScheduler(this Container container, Type jobsAssembly, NameValueCollection quartzConfig = null)
        {
            if (jobsAssembly == null) throw new ArgumentNullException(nameof(jobsAssembly));

            container.RegisterAs<FunqJobFactory, IJobFactory>();
            jobsAssembly.Assembly.GetTypes()
                .Where(type => !type.IsAbstract && typeof(IJob).IsAssignableFrom(type))
                .Each(x => container.RegisterAutoWiredType(x));

            ISchedulerFactory schedFact = quartzConfig != null
                ? new StdSchedulerFactory(quartzConfig)
                : new StdSchedulerFactory();

            IScheduler scheduler = schedFact.GetScheduler();
            scheduler.JobFactory = container.Resolve<IJobFactory>();
            container.Register<IScheduler>(scheduler);
        }
    }
}