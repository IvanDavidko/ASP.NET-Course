
using Autofac;
using Domain.Interfaces;
using PhotoManager.Repository.ServiceRepository;

namespace PhotoManager.UI.Infrastructure.IocRegistration
{
    public class BaseServiceRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserServiceRepository>()
                .As<IUserServiceRepository>();

            builder.RegisterType<AlbumServiceRepository>()
                .As<IAlbumServiceRepository>();

            builder.RegisterType<PhotoServiceRepository>()
                .As<IPhotoServiceRepository>();
        }
    }
}