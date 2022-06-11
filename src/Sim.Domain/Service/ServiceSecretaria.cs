using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServiceSecretaria : ServiceBase<Secretaria>, IServiceSecretaria
    {
        private readonly IRepositorySecretaria _secretaria;

        public ServiceSecretaria(IRepositorySecretaria repositorySecretaria)
            :base(repositorySecretaria)
        {
            _secretaria = repositorySecretaria;
        }

        public async Task<IEnumerable<Secretaria>> ListSecretariaOwnerAsync(string setor)
        {
            return await _secretaria.ListSecretariaOwnerAsync(setor);
        }
    }
}
