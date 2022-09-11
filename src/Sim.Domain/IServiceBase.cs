using System.Linq.Expressions;

namespace Sim.Domain;
public interface IServiceBase<TEntity> where TEntity : class
{
    Task AddAsync(TEntity obj);
    Task UpdateAsync(TEntity obj);
    Task RemoveAsync(TEntity obj);
    Task<TEntity> SingleIdAsync(Guid id);
    Task<IEnumerable<TEntity>> DoList(Expression<Func<TEntity, bool>>? filter = null);
}

