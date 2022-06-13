using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryTipo : RepositoryBase<Tipo>, IRepositoryTipo
    {
        public RepositoryTipo(ApplicationContext dbContext)
            :base(dbContext)
        {
                
        }

        public async Task<Tipo> GetIdAsync(Guid id)
        {
            return await _db.Tipos.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Tipo>> ListAllAsync()
        {
            return await _db.Tipos.ToListAsync();
        }

        public async Task<IEnumerable<Tipo>> ListTipoOwnerAsync(string owner)
        {
            return await _db.Tipos.Where(u => u.Owner.Contains(owner)).ToListAsync();
        }
    }
}
