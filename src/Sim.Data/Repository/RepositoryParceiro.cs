using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{

    public class RepositoryParceiro : RepositoryBase<Parceiro>, IRepositoryParceiro
    {
        public RepositoryParceiro(ApplicationContext applicationContext)
            : base(applicationContext)
        {

        }

        public async Task<Parceiro> GetIdAsync(Guid id)
        {
            return await _db.Parceiro.Include(s => s.Secretaria).Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Parceiro>> ListAllAsync()
        {
            return await _db.Parceiro.Include(s => s.Secretaria).ToListAsync();
        }

        public async Task<IEnumerable<Parceiro>> ListParceirosAsync(string owner)
        {
            return await _db.Parceiro.Include(s => s.Secretaria).Where(u => u.Secretaria.Nome.Contains(owner)).ToListAsync();
        }
    }
}
