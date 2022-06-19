namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceCanal : IServiceBase<Canal>
    {
        Task<IEnumerable<Canal>> ListCanalOwner(string setor);
        Task<Canal> GetIdAsync(Guid id);
        Task<IEnumerable<Canal>> ListAllAsync();
        Task<IEnumerable<(string canal, string value)>> ToListJson(string setor);
    }
}
