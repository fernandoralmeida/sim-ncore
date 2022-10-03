using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceTipo : IAppServiceBase<ETipo>
    {
        Task<IEnumerable<ETipo>> ListTipoOwnerAsync(string owner);
        Task<ETipo> GetIdAsync(Guid id);
        Task<IEnumerable<ETipo>> ListAllAsync();
    }
}
