using Sim.Data.Context;
using Microsoft.EntityFrameworkCore;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Repository;
using System.Linq.Expressions;

namespace Sim.Data.Repository 
{ 
    public class RepositoryCanal : RepositoryBase<ECanal>, IRepositoryCanal
    {
        public RepositoryCanal(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<IEnumerable<ECanal>> DoListAsync(Expression<Func<ECanal, bool>> filter = null)
        {
            var _query = _db.Canal.AsQueryable();

            if(filter != null)
                _query = _query
                    .Where(filter)
                    .Include(s => s.Dominio)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<ECanal> GetIdAsync(Guid id)
        {
            return await _db.Canal
                .Include(s => s.Dominio)
                .Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ECanal>> ListCanalOwner(string setor)
        {
            return await _db.Canal.Where(u => u.Dominio.Nome.Contains(setor) || u.Dominio.Nome.Contains("Geral"))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }
}

