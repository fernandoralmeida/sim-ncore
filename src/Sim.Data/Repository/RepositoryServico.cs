using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryServico : RepositoryBase<Servico>, IRepositoryServico
    {
        public RepositoryServico(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<Servico> GetIdAsync(Guid id)
        {
            return await _db.Servico.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Servico>> ListAllAsync()
        {
            return await _db.Servico.ToListAsync();
        }

        public async Task<IEnumerable<Servico>> ListServicoOwnerAsync(string setor)
        {
            return await _db.Servico
                .Include(s => s.Setor)
                .Include(s => s.Secretaria)
                .Where(p => p.Setor.Nome.Contains(setor)).ToListAsync();
        }
    }

}
