using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Repository;
using System.Linq.Expressions;

namespace Sim.Data.Repository
{

    public class RepositoryParceiro : RepositoryBase<EParceiro>, IRepositoryParceiro
    {
        public RepositoryParceiro(ApplicationContext applicationContext)
            : base(applicationContext)
        {

        }

        public async Task<IEnumerable<EParceiro>> DoListAsync(Expression<Func<EParceiro, bool>> filter = null) {
            var _query = _db.Parceiro.AsQueryable();

            if(filter != null)
                _query = _query
                    .Where(filter)
                    .Include(s => s.Dominio)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<EParceiro> GetIdAsync(Guid id)
        {
            return await _db.Parceiro.Include(s => s.Dominio).Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EParceiro>> ListAllAsync()
        {
            return await _db.Parceiro.Include(s => s.Dominio)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<EParceiro>> ListParceirosAsync(string owner)
        {
            return await _db.Parceiro.Include(s => s.Dominio).Where(u => u.Dominio.Nome.Contains(owner))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }
}
