using System;
using System.Threading.Tasks;
using DateRangeAccounting.DAL.Interfaces.Repository;

namespace DateRangeAccounting.DAL.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IDateRangeRepository Ranges { get; }
        ILogMetadataRepository Logs { get; }

        void Save();
        Task SaveAsync();
    }
}
