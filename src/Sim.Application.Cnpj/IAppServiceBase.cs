
namespace Sim.Application.Cnpj
{
    public interface IAppServiceBase<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> ListAllAsync();

    }
}
