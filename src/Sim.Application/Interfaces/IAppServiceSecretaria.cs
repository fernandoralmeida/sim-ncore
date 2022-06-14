using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceSecretaria : IAppServiceBase<Secretaria>
    {
        Task<IEnumerable<Secretaria>> ListSecretariaOwnerAsync(string setor);
        Task<Secretaria> GetIdAsync(Guid id);
        Task<IEnumerable<Secretaria>> ListAllAsync();
    }
}
