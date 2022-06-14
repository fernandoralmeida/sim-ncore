using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceSecretaria : AppServiceBase<Secretaria>, IAppServiceSecretaria
    {
        private readonly IServiceSecretaria _secretaria;

        public AppServiceSecretaria(IServiceSecretaria secretaria)
            :base(secretaria)
        {
            _secretaria = secretaria;
        }

        public async Task<Secretaria> GetIdAsync(Guid id)
        {
            return await _secretaria.GetIdAsync(id);
        }

        public async Task<IEnumerable<Secretaria>> ListAllAsync()
        {
            return await _secretaria.ListAllAsync();
        }

        public async Task<IEnumerable<Secretaria>> ListSecretariaOwnerAsync(string setor)
        {
            return await _secretaria.ListSecretariaOwnerAsync(setor);
        }
    }
}
