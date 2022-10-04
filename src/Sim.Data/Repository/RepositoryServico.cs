using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Repository;

namespace Sim.Data.Repository
{
    public class RepositoryServico : RepositoryBase<EServico>, IRepositoryServico
    {
        public RepositoryServico(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<EServico> GetIdAsync(Guid id)
        {
            return await _db.Servico.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EServico>> ListAllAsync()
        {
            return await _db.Servico.OrderBy(s => s.Nome).ToListAsync();
        }

        public async Task<IEnumerable<EServico>> ListServicoOwnerAsync(string setor)
        {
            return await _db.Servico
                .Include(s => s.Dominio)
                .Where(p => p.Dominio.Nome.Contains(setor) 
                || p.Dominio.Nome.Contains("Geral"))
                .OrderBy(s => s.Nome).ToListAsync();
        }
    }

}
