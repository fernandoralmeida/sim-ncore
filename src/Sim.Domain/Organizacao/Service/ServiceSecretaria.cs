namespace Sim.Domain.Organizacao.Service
{
    using Model;
    using Organizacao.Interfaces.Repository;
    using Organizacao.Interfaces.Service;
    public class ServiceSecretaria : ServiceBase<EOrganizacao>, IServiceSecretaria
    {
        private readonly IRepositorySecretaria _secretaria;

        public ServiceSecretaria(IRepositorySecretaria repositorySecretaria)
            :base(repositorySecretaria)
        {
            _secretaria = repositorySecretaria;
        }

        public async Task<EOrganizacao> GetIdAsync(Guid id)
        {
            return await _secretaria.GetIdAsync(id);
        }

        public async Task<IEnumerable<EOrganizacao>> ListAllAsync()
        {
            return await _secretaria.ListAllAsync();
        }

        public async Task<IEnumerable<EOrganizacao>> ListSecretariaOwnerAsync(string setor)
        {
            return await _secretaria.ListSecretariaOwnerAsync(setor);
        }
    }
}
