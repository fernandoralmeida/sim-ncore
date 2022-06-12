﻿using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryEvento : RepositoryBase<Evento>, IRepositoryEvento
    {
        public RepositoryEvento(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<Evento> GetCodigoAsync(int codigo)
        {
            return await _db.Evento
                .Include(e => e.Inscritos)
                .Where(p => p.Codigo == codigo).FirstOrDefaultAsync();
        }

        public async Task<Evento> GetCodigoParticipanteAsync(int codigo)
        {
            return await _db.Evento
                .Include(e => e.Inscritos)
                .Where(p => p.Codigo == codigo).FirstOrDefaultAsync();
        }

        public async Task<Evento> GetIdAsync(Guid id)
        {
            return await _db.Evento
                .Include(i => i.Inscritos)
                .Where(u => u.Id == id).OrderBy(d => d.Data).ThenByDescending(d => d.Data).FirstOrDefaultAsync();
        }

        public int LastCodigo()
        {
            var cod = _db.Evento
                .AsNoTracking()
                .OrderBy(c => c.Codigo)
                .LastOrDefault()?.Codigo;

            if (cod == null)
                return 0;
            else
                return (int)cod;
        }

        public async Task<IEnumerable<Evento>> ListAllAsync()
        {
            return await _db.Evento
                .AsNoTracking()
                .Include(i => i.Inscritos)
                .ToListAsync();
        }

        public async Task<IEnumerable<Evento>> ListNomeAsync(string nome)
        {
            return await _db.Evento
                .AsNoTracking()
                .Include(i=>i.Inscritos)
                .Where(u => u.Nome.Contains(nome)).OrderBy(d => d.Data).ThenByDescending(d => d.Data).ToListAsync();
        }

        public async Task<IEnumerable<Evento>> ListOwnerAsync(string setor)
        {
            return await _db.Evento
                .AsNoTracking()
                .Include(i => i.Inscritos)
                .Where(u => u.Owner.Contains(setor)).OrderBy(d => d.Data).ThenByDescending(d => d.Data).ToListAsync();
        }
    }
}
