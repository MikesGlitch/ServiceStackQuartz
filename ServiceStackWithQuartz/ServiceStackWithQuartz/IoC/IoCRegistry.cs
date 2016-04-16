using Funq;
using Quartz.Spi;

namespace ServiceStackWithQuartz
{
    public static class IoCRegistry
    {
        public static void RegisterAll(this Container container)
        {
            /* Quartz */
            container.RegisterAs<QuartzInitializer, IQuartzInitializer>();
            container.RegisterAs<FunqJobFactory, IJobFactory>();
            new JobRegistrar(container).RegisterJobs();
        }
    }
}
