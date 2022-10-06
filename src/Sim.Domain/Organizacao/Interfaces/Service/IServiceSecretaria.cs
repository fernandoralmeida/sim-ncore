namespace Sim.Domain.Organizacao.Interfaces.Service
{
    using Model;
    public interface IServiceSecretaria : IServiceBase<EOrganizacao>
    {
        Task<IEnumerable<EOrganizacao>> ListSecretariaOwnerAsync(string setor);
        Task<EOrganizacao> GetIdAsync(Guid id);
        Task<IEnumerable<EOrganizacao>> ListAllAsync();
        Task<IEnumerable<EOrganizacao>> DoListHierarquia0Async(IEnumerable<EOrganizacao> lista);
        Task<IEnumerable<EOrganizacao>> DoListHierarquia1Async(IEnumerable<EOrganizacao> lista);
        Task<IEnumerable<EOrganizacao>> DoListHierarquia2Async(IEnumerable<EOrganizacao> lista);
        Task<IEnumerable<EOrganizacao>> DoListHierarquia2from1Async(IEnumerable<EOrganizacao> lista, Guid id);
        Task<IEnumerable<EOrganizacao>> DoListHierarquia1from0Async(IEnumerable<EOrganizacao> lista, Guid id);
        Task<IEnumerable<EOrganizacao>> DoListHierarquia2from0Async(IEnumerable<EOrganizacao> lista, Guid id);
    }
}
