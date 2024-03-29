﻿using Microsoft.EntityFrameworkCore;
using Sim.Data.Cnpj.Context;
using Sim.Domain.Cnpj;

namespace Sim.Data.Cnpj
{

    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly ApplicationContextCnpj _db;

        public RepositoryBase(ApplicationContextCnpj dbcontext)
        {
            _db = dbcontext;
        }
        
        public async Task<IEnumerable<TEntity>> ListAllAsync()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }
    }
}
