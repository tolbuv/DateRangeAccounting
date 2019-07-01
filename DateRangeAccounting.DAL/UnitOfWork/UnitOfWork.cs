using DateRangeAccounting.DAL.Interfaces.Repository;
using DateRangeAccounting.DAL.Interfaces.UnitOfWork;
using DateRangeAccounting.DAL.Repository;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DateRangeAccounting.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private IDateRangeRepository _ranges;
        private ILogMetadataRepository _logs;

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        public IDateRangeRepository Ranges => _ranges ?? (_ranges = new DateRangeRepository(_dbContext));

        public ILogMetadataRepository Logs => _logs ?? (_logs = new LogMetadataRepository(_dbContext));

        public void Save()
            => _dbContext.SaveChanges();

        public async Task SaveAsync()
            => await _dbContext.SaveChangesAsync();

        public void Dispose()
            => _dbContext.Dispose();
    }
}
