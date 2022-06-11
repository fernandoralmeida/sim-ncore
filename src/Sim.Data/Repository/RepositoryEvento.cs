using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sim.Cross.Data.Repository.Shared
{
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.Shared.Interface;
    using Context;
    public class RepositoryEvento : RepositoryBase<Evento>, IRepositoryEvento
    {
        public RepositoryEvento(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public Evento GetByCodigo(int codigo)
        {
            var eve = _db.Evento
                .Include(i => i.Inscritos)
                .Where(c => c.Codigo == codigo)
                .FirstOrDefault();

            if (eve == null)
                return null;

            var insc = _db.Inscricao
                .Include(p => p.Participante)
                .Include(e => e.Empresa)
                .Include(e=>e.Evento)
                .Where(s => s.Evento.Codigo == eve.Codigo).OrderBy(s=>s.Data_Inscricao);

            eve.Inscritos = insc.ToList();

            return eve;
        }

        public Evento GetByCodigo_Participantes(int codigo)
        {
            var eve = _db.Evento
                .Include(i => i.Inscritos)
                .Where(c => c.Codigo == codigo)
                .FirstOrDefault();

            if (eve == null)
                return null;

            var insc = _db.Inscricao
                .Include(p => p.Participante)
                .Include(e => e.Empresa)
                .Include(e => e.Evento)
                .Where(s => s.Evento.Codigo == eve.Codigo).OrderBy(s => s.Participante.Nome);

            eve.Inscritos = insc.ToList();

            return eve;
        }

        public IEnumerable<Evento> GetByNome(string nome)
        {
            return _db.Evento.Where(u => u.Nome.Contains(nome)).OrderBy(d => d.Data).ThenByDescending(d => d.Data);
        }

        public IEnumerable<Evento> GetByOwner(string setor)
        {
            return _db.Evento.Where(u => u.Owner.Contains(setor));
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

        public IEnumerable<Evento> ListAll()
        {
            return _db.Evento
                .Include(i => i.Inscritos)                
                .ToList();
        }
    }
}
