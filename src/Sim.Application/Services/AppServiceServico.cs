using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceServico : AppServiceBase<Servico>, IAppServiceServico
    {
        private readonly IServiceServico _servico;
        public AppServiceServico(IServiceServico servico)
            :base(servico)
        {
            _servico = servico;
        }

        public async Task<Servico> GetIdAsync(Guid id)
        {
            return await _servico.GetIdAsync(id);
        }

        public async Task<IEnumerable<Servico>> ListAllAsync()
        {
            return await _servico.ListAllAsync();
        }

        public async Task<IEnumerable<Servico>> ListServicoOwnerAsync(string setor)
        {
            return await _servico.ListServicoOwnerAsync(setor);
        }
    }
}
