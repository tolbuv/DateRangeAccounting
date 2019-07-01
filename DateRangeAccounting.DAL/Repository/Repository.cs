using DateRangeAccounting.DAL.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DateRangeAccounting.DAL.Repository
{
    public abstract class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<T> Entities;

        protected Repository(DbContext context)
        {
            Context = context;
            Entities = context.Set<T>();
        }

        public virtual T Get(long id)
            => Entities.Find(id);

        public async Task<T> GetAsync(long id)
            => await Entities.AsNoTracking().FirstAsync();

        public virtual IEnumerable<T> GetAll()
            => Entities.AsNoTracking().ToList();

        public async Task<IEnumerable<T>> GetAllAsync()
            => await Entities.AsNoTracking().ToListAsync();

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
            => Entities.AsNoTracking().Where(predicate).ToList();

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await Entities.AsNoTracking().Where(predicate).ToListAsync();

        public virtual T Add(T entity)
            => Entities.Add(entity);

        public virtual void AddRange(IEnumerable<T> entities)
            => Entities.AddRange(entities);

        public virtual void Remove(T entity)
            => Entities.Remove(entity);

        public virtual void RemoveRange(IEnumerable<T> entities)
            => Entities.RemoveRange(entities);
    }
}
