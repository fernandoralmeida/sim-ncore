﻿using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositorySecretaria : RepositoryBase<Secretaria>, IRepositorySecretaria
    {
        public RepositorySecretaria(ApplicationContext dbContext)
            :base(dbContext)
        {
                
        }

        public async Task<Secretaria> GetIdAsync(Guid id)
        {
            return await _db.Secretaria.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Secretaria>> ListAllAsync()
        {
            return await _db.Secretaria.ToListAsync();
        }

        public async Task<IEnumerable<Secretaria>> ListSecretariaOwnerAsync(string setor)
        {
            return await _db.Secretaria.Where(u => u.Owner.Contains(setor)).ToListAsync();
        }
    }
}
