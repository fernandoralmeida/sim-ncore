using Sim.Domain.Organizacao.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceCanal : IAppServiceBase<ECanal>
    {
        Task<IEnumerable<ECanal>> ListCanalOwner(string setor);
        Task<ECanal> GetIdAsync(Guid id);
        Task<IEnumerable<ECanal>> ListAllAsync();
        Task<IEnumerable<(string canal, string value)>> ToListJson(string setor);
    }
}
