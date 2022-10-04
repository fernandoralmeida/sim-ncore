using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Repository;

namespace Sim.Data.Repository
{

    public class RepositoryParceiro : RepositoryBase<EParceiro>, IRepositoryParceiro
    {
        public RepositoryParceiro(ApplicationContext applicationContext)
            : base(applicationContext)
        {

        }

        public async Task<EParceiro> GetIdAsync(Guid id)
        {
            return await _db.Parceiro.Include(s => s.Dominio).Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EParceiro>> ListAllAsync()
        {
            return await _db.Parceiro.Include(s => s.Dominio).ToListAsync();
        }

        public async Task<IEnumerable<EParceiro>> ListParceirosAsync(string owner)
        {
            return await _db.Parceiro.Include(s => s.Dominio).Where(u => u.Dominio.Nome.Contains(owner)).ToListAsync();
        }
    }
}
