using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Service;

namespace Sim.Application.Services
{
    using Interfaces;
    public class AppServiceSecretaria : AppServiceBase<EOrganizacao>, IAppServiceSecretaria
    {
        private readonly IServiceSecretaria _secretaria;

        public AppServiceSecretaria(IServiceSecretaria secretaria)
            :base(secretaria)
        {
            _secretaria = secretaria;
        }

        public async Task<IEnumerable<EOrganizacao>> DoListHierarquia0Async(IEnumerable<EOrganizacao> lista)
        {
            return await _secretaria.DoListHierarquia0Async(lista);
        }

        public async Task<IEnumerable<EOrganizacao>> DoListHierarquia1Async(IEnumerable<EOrganizacao> lista)
        {
            return await _secretaria.DoListHierarquia1Async(lista);
        }

        public async Task<IEnumerable<EOrganizacao>> DoListHierarquia1from0Async(IEnumerable<EOrganizacao> lista, Guid id)
        {
            return await _secretaria.DoListHierarquia1from0Async(lista, id);
        }

        public async Task<IEnumerable<EOrganizacao>> DoListHierarquia2Async(IEnumerable<EOrganizacao> lista)
        {
            return await _secretaria.DoListHierarquia2Async(lista);
        }

        public async Task<IEnumerable<EOrganizacao>> DoListHierarquia2from0Async(IEnumerable<EOrganizacao> lista, Guid id)
        {
            return await _secretaria.DoListHierarquia2from0Async(lista, id);
        }

        public async Task<IEnumerable<EOrganizacao>> DoListHierarquia2from1Async(IEnumerable<EOrganizacao> lista, Guid id)
        {
            return await _secretaria.DoListHierarquia2from1Async(lista, id);
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
