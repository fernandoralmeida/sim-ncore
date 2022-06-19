using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceCanal : IAppServiceBase<Canal>
    {
        Task<IEnumerable<Canal>> ListCanalOwner(string setor);
        Task<Canal> GetIdAsync(Guid id);
        Task<IEnumerable<Canal>> ListAllAsync();
        Task<IEnumerable<(string canal, string value)>> ToListJson(string setor);
    }
}
