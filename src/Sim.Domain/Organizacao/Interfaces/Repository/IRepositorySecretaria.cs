namespace Sim.Domain.Organizacao.Interfaces.Repository
{
    using Model;
    public interface IRepositorySecretaria : IRepositoryBase<EOrganizacao>
    {
        Task<IEnumerable<EOrganizacao>> ListSecretariaOwnerAsync(string setor);
        Task<EOrganizacao> GetIdAsync(Guid id);
        Task<IEnumerable<EOrganizacao>> ListAllAsync();
    }
}
