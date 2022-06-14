using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceServico : IAppServiceBase<Servico>
    {
        Task<IEnumerable<Servico>> ListServicoOwnerAsync(string setor);
        Task<Servico> GetIdAsync(Guid id);
        Task<IEnumerable<Servico>> ListAllAsync();
    }
}
