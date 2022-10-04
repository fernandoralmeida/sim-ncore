namespace Sim.Domain.Organizacao.Interfaces.Service
{
    using Model;
    public interface IServiceSecretaria : IServiceBase<EOrganizacao>
    {
        Task<IEnumerable<EOrganizacao>> ListSecretariaOwnerAsync(string setor);
        Task<EOrganizacao> GetIdAsync(Guid id);
        Task<IEnumerable<EOrganizacao>> ListAllAsync();
    }
}
