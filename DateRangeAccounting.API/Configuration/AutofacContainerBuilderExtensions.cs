using AutoMapper;
using DateRangeAccounting.API.Models.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Linq;
using Autofac;
using DateRangeAccounting.DAL.Configuration;

namespace DateRangeAccounting.API.Configuration
{
    namespace FeedReader.Api.Configurations
    {
        public static class AutofacContainerBuilderExtensions
        {
            public static ContainerBuilder ResolveDependencies(this ContainerBuilder builder)
            {
                builder.RegisterModule<DalModule>();

                builder
                    .RegisterType<ApplicationDbContext>()
                    .AsSelf()
                    .InstancePerRequest();

                builder
                    .Register(c =>
                        new IdentityFactoryOptions<ApplicationUserManager>
                        {
                            DataProtectionProvider =
                                new DpapiDataProtectionProvider("DateRangeAccounting.API")
                        });

                builder
                    .RegisterType<ApplicationUserManager>()
                    .AsSelf()
                    .InstancePerRequest();

                return builder;
            }

            public static ContainerBuilder UseAutoMapper(this ContainerBuilder builder)
            {
                if (builder == null) throw new NullReferenceException();

                var autoMapperProfileTypes =
                    AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(a =>
                            a.GetTypes()
                                .Where(p =>
                                    typeof(Profile).IsAssignableFrom(p) &&
                                    p.IsPublic &&
                                    !p.IsAbstract));

                var autoMapperProfiles =
                    autoMapperProfileTypes
                        .Select(p => (Profile)Activator.CreateInstance(p));

                builder
                    .Register(ctx =>
                        new MapperConfiguration(cfg =>
                        {
                            foreach (var profile in autoMapperProfiles)
                            {
                                cfg.AddProfile(profile);
                            }
                        }));

                builder
                    .Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
                    .As<IMapper>();

                return builder;
            }
        }

    }
}