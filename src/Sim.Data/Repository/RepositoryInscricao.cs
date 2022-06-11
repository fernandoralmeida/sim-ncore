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
    public class RepositoryInscricao : RepositoryBase<Inscricao>, IRepositoryInscricao
    {
        public RepositoryInscricao(ApplicationContext applicationContext)
            :base(applicationContext)
        {

        }

        public IEnumerable<Inscricao> GetByEvento(string evento)
        {
            return _db.Inscricao.Where(u => u.Numero.ToString() == evento);
        }

        public IEnumerable<Inscricao> GetByParticipante(string nome)
        {
            var query = _db.Inscricao
                .Include(p => p.Participante)
                .Include(e => e.Empresa)
                .Include(e => e.Evento)
                .Where(s => s.Participante.CPF == nome).ToList();

            return query; //_db.Inscricao.Select(r => r);
        }

        public IEnumerable<Inscricao> GetByTipo(string evento)
        {
            return _db.Inscricao.Select(r => r);
        }

        public async Task<IEnumerable<Inscricao>> GetInscrito(Guid id)
        {
            var query = await Task.Run(() => _db.Inscricao
                .Include(p => p.Participante)
                .Include(e => e.Empresa)
                .Include(e => e.Evento)
                .Where(s => s.Id == id));

            return query; //_db.Inscricao.Select(r => r);
        }

        public bool JaInscrito(string cpf, int evento)
        {
            var query = _db.Inscricao
                .Include(p => p.Participante)
                .Include(p => p.Evento)
                .Where(p => p.Participante.CPF == cpf && p.Evento.Codigo == evento)
                .FirstOrDefault();

            if (query == null)
                return false;
            else
                return true;
        }

        public int LastCodigo()
        {
            var cod = _db.Inscricao
                .AsNoTracking()
                .OrderBy(c => c.Numero)
                .LastOrDefault()?.Numero;

            if (cod == null)
                return 0;
            else
                return (int)cod;
        }

    }
}
