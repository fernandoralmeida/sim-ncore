using System.Linq.Expressions;

namespace Sim.Domain
{
    
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;

        public ServiceBase(IRepositoryBase<TEntity> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task AddAsync(TEntity obj)
        {
             await _repositoryBase.AddAsync(obj);
        }

        public async Task<IEnumerable<TEntity>> DoList(Expression<Func<TEntity, bool>>? filter = null) =>
            await _repositoryBase.DoList(filter);

        public async Task RemoveAsync(TEntity obj)
        {
            await _repositoryBase.RemoveAsync(obj);
        }

        public async Task<TEntity> SingleIdAsync(Guid id)
        {
            return await _repositoryBase.SingleIdAsync(id);
        }

        public async Task UpdateAsync(TEntity obj)
        {
            await _repositoryBase.UpdateAsync(obj);
        }

    }
}
