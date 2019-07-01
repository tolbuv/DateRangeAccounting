using DateRangeAccounting.DAL.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DateRangeAccounting.DAL.Interfaces.Repository
{
    public interface IDateRangeRepository : IRepository<DateRange>
    {
        Task<IEnumerable<DateRange>> GetByRange(DateRange range);
    }
}
