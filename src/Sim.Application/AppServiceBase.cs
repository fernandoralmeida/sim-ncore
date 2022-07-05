using Sim.Domain;

namespace Sim.Application
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;
        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public async Task AddAsync(TEntity obj)
        {
            await _serviceBase.AddAsync(obj);
        }

        public async Task RemoveAsync(TEntity obj)
        {
            await _serviceBase.RemoveAsync(obj);
        }

        public async Task<TEntity> SingleIdAsync(Guid id)
        {
            return await _serviceBase.SingleIdAsync(id);
        }

        public async Task UpdateAsync(TEntity obj)
        {
            await _serviceBase.UpdateAsync(obj);
        }
    }
}
