namespace Sim.Domain.Cnpj
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> ListAllAsync();
    }
}
