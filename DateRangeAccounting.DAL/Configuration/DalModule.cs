using Autofac;
using DateRangeAccounting.DAL.EF;
using DateRangeAccounting.DAL.Interfaces.Repository;
using DateRangeAccounting.DAL.Interfaces.UnitOfWork;
using DateRangeAccounting.DAL.Repository;
using System.Configuration;
using System.Data.Entity;

namespace DateRangeAccounting.DAL.Configuration
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["dateRangeConnectionString"].ConnectionString;

            builder
                .RegisterType<DateRangeContext>()
                .As<DbContext>()
                .WithParameter("connectionString", connectionString)
                .InstancePerLifetimeScope();

            builder
                .RegisterType<DateRangeRepository>()
                .As<IDateRangeRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<LogMetadataRepository>()
                .As<ILogMetadataRepository>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<UnitOfWork.UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            base.Load(builder);
        }
    }
}
