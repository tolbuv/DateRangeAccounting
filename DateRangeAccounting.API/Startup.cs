using Autofac;
using Autofac.Integration.WebApi;
using DateRangeAccounting.API.Configuration.FeedReader.Api.Configurations;
using Microsoft.Owin;
using Owin;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(DateRangeAccounting.API.Startup))]

namespace DateRangeAccounting.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.ResolveDependencies();
            builder.UseAutoMapper();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiModelBinderProvider();

            var container = builder.Build();

            var config = new HttpConfiguration
                { DependencyResolver = new AutofacWebApiDependencyResolver(container) };

            WebApiConfig.Register(config);

            app.UseAutofacMiddleware(container);
            ConfigureAuth(app);
            app.UseWebApi(config);
        }
    }
}
