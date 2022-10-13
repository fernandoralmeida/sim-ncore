using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Repository;
using System.Linq;
using System.Linq.Expressions;

namespace Sim.Data.Repository
{
    public class RepositoryEvento : RepositoryBase<EEvento>, IRepositoryEvento
    {
        public RepositoryEvento(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<IEnumerable<EEvento>> DoListAsync(Expression<Func<EEvento, bool>> filter = null)
        {
            var _query = _db.Evento.AsQueryable();

            if(filter != null)
                _query = _query
                    .Where(filter)
                    .Include(i => i.Inscritos).ThenInclude(i => i.Participante)
                    .Include(i => i.Inscritos).ThenInclude(i => i.Empresa)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<EEvento> GetIdAsync(Guid id)
        {
            return await _db.Evento
                .Include(i => i.Inscritos).ThenInclude(i => i.Participante)
                .Include(i => i.Inscritos).ThenInclude(i => i.Empresa)
                .Where(u => u.Id == id)
                .OrderBy(d => d.Data)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync();
        }

        public int LastCodigo()
        {
            var cod = _db.Evento                
                .OrderBy(c => c.Codigo)
                .AsNoTrackingWithIdentityResolution()
                .LastOrDefault()?.Codigo;

            if (cod == null)
                return 0;
            else
                return (int)cod;
        }

    }
}
