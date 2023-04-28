namespace Sim.Domain.Organizacao.Service
{
    using System.Linq.Expressions;
    using Model;
    using Organizacao.Interfaces.Repository;
    using Organizacao.Interfaces.Service;
    public class ServiceServico : ServiceBase<EServico>, IServiceServico
    {
        private readonly IRepositoryServico _servico;
        public ServiceServico(IRepositoryServico repositoryServico)
            :base(repositoryServico)
        {
            _servico = repositoryServico;
        }

        public async Task<IEnumerable<EServico>> DoListAsync(Expression<Func<EServico, bool>>? filter = null)
        {
            return await _servico.DoListAsync(filter);
        }

        public async Task<IEnumerable<EServico>> DoListByDominioAsync(Guid id)
        {
            return await _servico.DoListByDominioAsync(id);
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

        public async Task<IEnumerable<(string servico, string value)>> ToListJson(string setor)
        {
            var list = await DoListAsync(s => s.Dominio!.Nome == setor || s.Dominio.Hierarquia == EHierarquia.Secretaria);

            var servicelist = new List<(string canal, string value)>();

            foreach (var item in list.OrderBy(s => s.Nome))
            {
                servicelist.Add(new() { canal = item.Nome!, value = item.Nome! });
            }

            return servicelist;
        }
    }
}
