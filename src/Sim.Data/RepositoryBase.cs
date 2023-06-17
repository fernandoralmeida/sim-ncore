using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain;

namespace Sim.Data
{

    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly ApplicationContext _db;

        public RepositoryBase(ApplicationContext dbcontext)
        {
            _db = dbcontext;
        }

        public async Task AddAsync(TEntity obj)
        {
            await _db.Set<TEntity>().AddAsync(obj);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity obj)
        {
            _db.Entry(obj).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity obj)
        {
            _db.Set<TEntity>().Remove(obj);
            await _db.SaveChangesAsync();
        }

        public async Task<TEntity> SingleIdAsync(Guid id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> DoList(Expression<Func<TEntity, bool>> filter = null)
        {
            var _query = _db.Set<TEntity>().AsQueryable();

            if(filter != null)
                _query = _query
                    .Where(filter);

            return await _query.ToListAsync();
        }
    }
}
