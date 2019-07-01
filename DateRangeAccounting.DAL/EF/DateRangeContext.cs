using System.Data.Entity;
using DateRangeAccounting.DAL.Domain;

namespace DateRangeAccounting.DAL.EF
{
    public class DateRangeContext : DbContext
    {
        public DateRangeContext(string connectionString) : base(connectionString)
        { }
        
        public virtual DbSet<DateRange> Ranges { get; set; }

        public virtual DbSet<LogMetadata> LogMetadata { get; set; }
    }
}
