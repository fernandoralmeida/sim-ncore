using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Repository;

namespace Sim.Data.Repository
{
    public class RepositoryTipo : RepositoryBase<ETipo>, IRepositoryTipo
    {
        public RepositoryTipo(ApplicationContext dbContext)
            :base(dbContext)
        {
                
        }

        public async Task<ETipo> GetIdAsync(Guid id)
        {
            return await _db.Tipos.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<ETipo>> ListAllAsync()
        {
            return await _db.Tipos.ToListAsync();
        }

        public async Task<IEnumerable<ETipo>> ListTipoOwnerAsync(string owner)
        {
            return await _db.Tipos.Where(u => u.Tipo.Contains(owner)).ToListAsync();
        }
    }
}
