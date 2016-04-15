using Funq;
using Quartz;
using ServiceStack;
using System.Linq;

namespace IntegrationService
{
    public class JobRegistrar
    {
        private readonly Container _container;

        public JobRegistrar(Container container)
        {
            _container = container;
        }

        public void RegisterJobs()
        {
            /* Register Jobs */
            typeof(AppHost).Assembly.GetTypes()
                .Where(type => !type.IsAbstract && typeof(IJob).IsAssignableFrom(type))
                .Each(x => _container.RegisterAutoWiredType(x));
        }
    }
}
