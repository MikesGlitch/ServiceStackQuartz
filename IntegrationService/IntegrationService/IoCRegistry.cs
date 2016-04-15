using Funq;
using Quartz.Spi;

namespace IntegrationService
{
    public static class IoCRegistry
    {
        public static void RegisterAll(this Container container)
        {
            /* Quartz */
            container.RegisterAs<QuartzInitializer, IQuartzInitializer>();

            /* Jobs */
            new JobRegistrar(container).RegisterJobs();
            container.RegisterAs<FunqJobFactory, IJobFactory>();
        }
    }
}
