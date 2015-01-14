
using Autofac;
using PhotoManager.UI.Infrastructure.IocRegistration;

namespace PhotoManager.UI.App_Start
{
    public class IocConfig
    {
        public static ILifetimeScope GetContainerInstance()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new BaseControllerModule());
            builder.RegisterModule(new BaseServiceRepositoryModule());
            builder.RegisterModule(new BaseValidatorModule());

            var container = builder.Build();

            return container.BeginLifetimeScope();
        }
    }
}