using Sim.Domain.Cnpj;

namespace Sim.Application.Cnpj
{
    public class AppServiceBase<TEntity> : IAppServiceBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceBase;
        public AppServiceBase(IServiceBase<TEntity> serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public async Task<IEnumerable<TEntity>> ListAllAsync()
        {
            return await _serviceBase.ListAllAsync();
        }
    }
}
