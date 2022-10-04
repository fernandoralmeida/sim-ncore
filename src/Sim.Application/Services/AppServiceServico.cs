using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Service;

namespace Sim.Application.Services
{
    using Interfaces;
    public class AppServiceServico : AppServiceBase<EServico>, IAppServiceServico
    {
        private readonly IServiceServico _servico;
        public AppServiceServico(IServiceServico servico)
            :base(servico)
        {
            _servico = servico;
        }

        public async Task<EServico> GetIdAsync(Guid id)
        {
            return await _servico.GetIdAsync(id);
        }

        public async Task<IEnumerable<EServico>> ListAllAsync()
        {
            return await _servico.ListAllAsync();
        }

        public async Task<IEnumerable<EServico>> ListServicoOwnerAsync(string setor)
        {
            return await _servico.ListServicoOwnerAsync(setor);
        }

        public async Task<IEnumerable<(string canal, string value)>> ToListJson(string setor)
        {
            return await _servico.ToListJson(setor);
        }
    }
}
