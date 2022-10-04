using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Repository;

namespace Sim.Data.Repository
{
    public class RepositorySecretaria : RepositoryBase<EOrganizacao>, IRepositorySecretaria
    {
        public RepositorySecretaria(ApplicationContext dbContext)
            :base(dbContext)
        {
                
        }

        public async Task<EOrganizacao> GetIdAsync(Guid id)
        {
            return await _db.Secretaria.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<EOrganizacao>> ListAllAsync()
        {
<<<<<<< HEAD
            return await _db.Secretaria.Include(p => p.Acronimo).ToListAsync();
=======
            return await _db.Secretaria.Include(p => p.Owner).ToListAsync();
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
        }

        public async Task<IEnumerable<EOrganizacao>> ListSecretariaOwnerAsync(string setor)
        {
            return await _db.Secretaria
<<<<<<< HEAD
                .Include(p => p.Acronimo)
                .Where(u => u.Dominio == setor)
=======
                .Include(p => p.Owner)
                .Where(u => u.Owner.Id == new Guid(setor))
>>>>>>> c0015656c1f538df7daa8cd99c2f51ed66d91cfd
                .ToListAsync();
        }
    }
}
