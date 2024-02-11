using Autofac;
using HackerNews.BusinessLogic.Services;
using HackerNews.BusinessLogic.Contracts;

namespace HackerNews.BusinessLogic.Configuration
{
    public class BusinessLogicAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .PropertiesAutowired()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<BestStoryService>().As<IBestStoryService>();
        }
    }
}
