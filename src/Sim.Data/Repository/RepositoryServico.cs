﻿using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Repository;
using System.Linq.Expressions;

namespace Sim.Data.Repository
{
    public class RepositoryServico : RepositoryBase<EServico>, IRepositoryServico
    {
        public RepositoryServico(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<IEnumerable<EServico>> DoListAsync(Expression<Func<EServico, bool>> filter = null)
        {
            var _query = _db.Servico.AsQueryable();

            if(filter != null)
                _query = _query
                    .Where(filter)
                    .Include(i => i.Dominio)
                    .AsNoTrackingWithIdentityResolution();

            return await _query.ToListAsync();
        }

        public async Task<IEnumerable<EServico>> DoListByDominioAsync(Guid id) {
            return await _db.Servico
                .Include(s => s.Dominio)
                .Where(p => p.Dominio == null || p.Dominio.Id == id)
                .AsNoTrackingWithIdentityResolution()
                .OrderBy(s => s.Nome)
                .ToListAsync();
        }

        public async Task<EServico> GetIdAsync(Guid id)
        {
            return await _db.Servico.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EServico>> ListAllAsync()
        {
            return await _db.Servico
                .Include(d => d.Dominio)
                .OrderBy(s => s.Nome)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<EServico>> ListServicoOwnerAsync(string setor)
        {
            return await _db.Servico
                .Include(s => s.Dominio)
                .Where(p => p.Dominio.Nome.Contains(setor) 
                || p.Dominio == null)
                .OrderBy(s => s.Nome)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }

}
