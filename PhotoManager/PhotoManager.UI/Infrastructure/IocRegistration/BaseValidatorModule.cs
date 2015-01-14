
using Autofac;
using Domain.Models;
using Domain.Validators;
using FluentValidation;

namespace PhotoManager.UI.Infrastructure.IocRegistration
{
    public class BaseValidatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AlbumValidator>()
                .As<IValidator<Album>>()
                .SingleInstance();

            builder.RegisterType<PhotoValidator>()
                .As<IValidator<Photo>>()
                .SingleInstance();

            builder.RegisterType<ExtendedSearchValidator>()
                .As<IValidator<ExtendedSearch>>()
                .SingleInstance();
        }
    }
}