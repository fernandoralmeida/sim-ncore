using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceTipo : IAppServiceBase<Tipo>
    {
        Task<IEnumerable<Tipo>> ListTipoOwnerAsync(string owner);
        Task<Tipo> GetIdAsync(Guid id);
        Task<IEnumerable<Tipo>> ListAllAsync();
    }
}
