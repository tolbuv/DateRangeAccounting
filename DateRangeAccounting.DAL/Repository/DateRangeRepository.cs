using DateRangeAccounting.DAL.Domain;
using DateRangeAccounting.DAL.Interfaces.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DateRangeAccounting.DAL.Repository
{
    public class DateRangeRepository : Repository<DateRange>, IDateRangeRepository
    {
        public DateRangeRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DateRange>> GetByRange(DateRange range)
            => await Entities.AsNoTracking()
                .Where(x => x.Start >= range.Start && x.End <= range.End).ToListAsync();
    }
}
