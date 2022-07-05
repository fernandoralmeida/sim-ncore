using System.Threading.Tasks;

namespace Sim.Domain
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task UpdateAsync(TEntity obj);
        Task RemoveAsync(TEntity obj);
        Task<TEntity> SingleIdAsync(Guid id);
    }
}
