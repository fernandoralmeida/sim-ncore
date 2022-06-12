using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Sim.Data.Repository 
{ 
    public class RepositoryCanal : RepositoryBase<Canal>, IRepositoryCanal
    {
        public RepositoryCanal(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<Canal> GetIdAsync(Guid id)
        {
            return await _db.Canal
                .Include(s => s.Secretaria)
                .Include(t => t.Setor)
                .Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Canal>> ListAllAsync()
        {
            return await _db.Canal
                .Include(s => s.Secretaria)
                .Include(t => t.Setor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Canal>> ListCanalOwner(string setor)
        {
            return await Task.Run(() => _db.Canal.Where(u => u.Setor.Nome.Contains(setor) || u.Setor.Nome.Contains("Geral")));
        }
    }
}

