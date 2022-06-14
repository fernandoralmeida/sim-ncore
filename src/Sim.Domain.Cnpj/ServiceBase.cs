namespace Sim.Domain.Cnpj
{    
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;

        public ServiceBase(IRepositoryBase<TEntity> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<IEnumerable<TEntity>> ListAllAsync()
        {
            return await _repositoryBase.ListAllAsync();
        }
    }
}
