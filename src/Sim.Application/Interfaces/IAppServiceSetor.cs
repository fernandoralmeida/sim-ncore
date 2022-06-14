using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceSetor : IAppServiceBase<Setor>
    {
        Task<IEnumerable<Setor>> ListSetorOwnerAsync(string secretaria);
        Task<Setor> GetIdAsync(Guid id);
        Task<IEnumerable<Setor>> ListAllAsync();
    }
}
