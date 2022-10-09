using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryInscricao : RepositoryBase<Inscricao>, IRepositoryInscricao
    {
        public RepositoryInscricao(ApplicationContext applicationContext)
            :base(applicationContext)
        {

        }

        public async Task<Inscricao> GetIdAsync(Guid id)
        {
            return await _db.Inscricao
                .Include(p => p.Participante)
                .Include(e => e.Empresa)
                .Include(e => e.Evento)
                .Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Inscricao> GetInscritoAsync(Guid id)
        {
            return await _db.Inscricao
                .Include(p => p.Participante)
                .Include(e => e.Empresa)
                .Include(e => e.Evento)
                .Where(s => s.Id == id).FirstOrDefaultAsync();
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

        public async Task<IEnumerable<Inscricao>> ListAllAsync()
        {
            return await _db.Inscricao.ToListAsync();
        }

        public async Task<IEnumerable<Inscricao>> ListEventoAsync(string evento)
        {
            return await _db.Inscricao
                .Include(p => p.Participante)
                .Include(e => e.Empresa)
                .Include(e => e.Evento)
                .Where(u => u.Evento.Codigo.ToString() == evento)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<Inscricao>> ListParticipanteAsync(string nome)
        {
            return await _db.Inscricao
                .Include(p => p.Participante)
                .Include(e => e.Empresa)
                .Include(e => e.Evento)
                .Where(s => s.Participante.CPF == nome)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<Inscricao>> ListTipoAsync(string evento)
        {
            return await _db.Inscricao
                .AsNoTracking()
                .Include(e => e.Evento)
                .Include(p => p.Participante)
                .Include(t=>t.Empresa)
                .Where(s=>s.Evento.Tipo.Contains(evento))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }
}
