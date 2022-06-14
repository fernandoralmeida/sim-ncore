using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceParceiro : IAppServiceBase<Parceiro>
    {
        Task<IEnumerable<Parceiro>> ListParceirosAsync(string owner);
        Task<Parceiro> GetIdAsync(Guid id);
        Task<IEnumerable<Parceiro>> ListAllAsync();
    }
}
