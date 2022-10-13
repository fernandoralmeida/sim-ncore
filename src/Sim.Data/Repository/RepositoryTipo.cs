using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Repository;
using System.Linq.Expressions;

namespace Sim.Data.Repository
{
    public class RepositoryTipo : RepositoryBase<ETipo>, IRepositoryTipo
    {
        public RepositoryTipo(ApplicationContext dbContext)
            :base(dbContext)
        {
                
        }

        public async Task<IEnumerable<ETipo>> DoListAsync(Expression<Func<ETipo, bool>> filter = null)
        {
            var _query = _db.Tipos.AsQueryable();
            
            if(filter != null)
                _query = _query
                    .Where(filter)
                    .Include(s => s.Dominio)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<ETipo> GetIdAsync(Guid id)
        {
            return await _db.Tipos.FirstOrDefaultAsync(u => u.Id == id);
        }

    }
}
