using System.Data.Entity;
using DateRangeAccounting.DAL.Domain;
using DateRangeAccounting.DAL.Interfaces.Repository;

namespace DateRangeAccounting.DAL.Repository
{
    // for scaling purposes
    public class LogMetadataRepository : Repository<LogMetadata>, ILogMetadataRepository
    {
        public LogMetadataRepository(DbContext context) : base(context)
        {
        }
    }
}
