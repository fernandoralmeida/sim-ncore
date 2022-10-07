using Sim.Domain.Organizacao.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceServico : IAppServiceBase<EServico>
    {
        Task<IEnumerable<EServico>> ListServicoOwnerAsync(string setor);
        Task<EServico> GetIdAsync(Guid id);
        Task<IEnumerable<EServico>> ListAllAsync();
        Task<IEnumerable<EServico>> DoListByDominioAsync(Guid id);
        Task<IEnumerable<(string canal, string value)>> ToListJson(string setor);
    }
}
