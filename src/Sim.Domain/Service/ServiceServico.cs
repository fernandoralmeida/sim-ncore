using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServiceServico : ServiceBase<Servico>, IServiceServico
    {
        private readonly IRepositoryServico _servico;
        public ServiceServico(IRepositoryServico repositoryServico)
            :base(repositoryServico)
        {
            _servico = repositoryServico;
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

        public async Task<IEnumerable<(string servico, string value)>> ToListJson(string setor)
        {
            var list = await ListServicoOwnerAsync(setor);
            var servicelist = new List<(string canal, string value)>();

            foreach (var item in list)
            {
                servicelist.Add(new() { canal = item.Nome, value = item.Nome });
            }

            return servicelist;
        }
    }
}
