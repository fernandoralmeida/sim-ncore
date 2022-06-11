namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryCanal : IRepositoryBase<Canal>
    {
        Task<IEnumerable<Canal>> ListCanalOwner(string setor);
        Task<Canal> GetIdAsync(Guid id);
        Task<IEnumerable<Canal>> ListAllAsync();
    }
}
