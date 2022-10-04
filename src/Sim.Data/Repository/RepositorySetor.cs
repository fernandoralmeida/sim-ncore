using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositorySetor : RepositoryBase<Setor>, IRepositorySetor
    {
        public RepositorySetor(ApplicationContext dbContext)
            :base(dbContext)
        {        }

        public async Task<Setor> GetIdAsync(Guid id)
        {
            return await _db.Setor.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Setor>> ListAllAsync()
        {
            return await _db.Setor
                .Include(s=>s.Canais)
                .Include(s=>s.Servicos)
                .ToListAsync();
        }

        public async Task<IEnumerable<Setor>> ListSetorOwnerAsync(string secretaria)
        {
            return await _db.Setor
                .Include(s=>s.Canais)
                .Include(s=>s.Servicos)
                .Where(u => u.Secretaria.Nome.Contains(secretaria)).ToListAsync();
        }
    }
}
