namespace Sim.Domain.Cnpj
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> ListAllAsync(string situacaocadastral);
    }
}
