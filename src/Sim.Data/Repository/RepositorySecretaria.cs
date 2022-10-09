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
            return await _db.Secretaria
                .Include(p => p.Servicos)
                .Include(p => p.Canais)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<EOrganizacao>> ListSecretariaOwnerAsync(string setor)
        {
            return await _db.Secretaria
                .Include(p => p.Servicos)
                .Include(p => p.Canais)
                .Where(u => u.Nome == setor)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }
}
