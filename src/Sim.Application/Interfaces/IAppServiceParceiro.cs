using Sim.Domain.Organizacao.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceParceiro : IAppServiceBase<EParceiro>
    {
        Task<IEnumerable<EParceiro>> ListParceirosAsync(string owner);
        Task<EParceiro> GetIdAsync(Guid id);
        Task<IEnumerable<EParceiro>> ListAllAsync();
    }
}
