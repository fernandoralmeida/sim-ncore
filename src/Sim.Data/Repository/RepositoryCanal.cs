using Sim.Data.Context;
using Microsoft.EntityFrameworkCore;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Repository;

namespace Sim.Data.Repository 
{ 
    public class RepositoryCanal : RepositoryBase<ECanal>, IRepositoryCanal
    {
        public RepositoryCanal(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<ECanal> GetIdAsync(Guid id)
        {
            return await _db.Canal
                .Include(s => s.Dominio)
                .Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ECanal>> ListAllAsync()
        {
            return await _db.Canal
                .Include(s => s.Dominio)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<ECanal>> ListCanalOwner(string setor)
        {
            return await _db.Canal.Where(u => u.Dominio.Nome.Contains(setor) || u.Dominio.Nome.Contains("Geral"))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }
}

