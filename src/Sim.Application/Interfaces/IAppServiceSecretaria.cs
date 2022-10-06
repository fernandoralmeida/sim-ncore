using Sim.Domain.Organizacao.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceSecretaria : IAppServiceBase<EOrganizacao>
    {
        Task<IEnumerable<EOrganizacao>> ListSecretariaOwnerAsync(string setor);
        Task<EOrganizacao> GetIdAsync(Guid id);
        Task<IEnumerable<EOrganizacao>> ListAllAsync();
        Task<IEnumerable<EOrganizacao>> DoListHierarquia0Async(IEnumerable<EOrganizacao> lista);
        Task<IEnumerable<EOrganizacao>> DoListHierarquia1Async(IEnumerable<EOrganizacao> lista);
        Task<IEnumerable<EOrganizacao>> DoListHierarquia2Async(IEnumerable<EOrganizacao> lista);
    }
}
