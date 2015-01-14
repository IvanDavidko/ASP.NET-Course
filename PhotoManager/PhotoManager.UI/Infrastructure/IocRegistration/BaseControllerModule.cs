
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using Core.Mapping;
using Module = Autofac.Module;

namespace PhotoManager.UI.Infrastructure.IocRegistration
{
    public class BaseControllerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //register mvc controllers
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UtilityMapper>().As<UtilityMapper>();
        }
    }
}