using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using System.Linq;

namespace Sim.Data.Repository
{
    public class RepositoryEvento : RepositoryBase<Evento>, IRepositoryEvento
    {
        public RepositoryEvento(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<IEnumerable<Evento>> DoListAsyncBy(string param)
        {
            return await _db.Evento                
                .Include(i => i.Inscritos).ThenInclude(i => i.Participante)
                .Include(i => i.Inscritos).ThenInclude(i => i.Empresa)
                .Where(s => s.Nome.Contains(param) ||
                            s.Tipo.Contains(param) ||
                            s.Owner == param ||
                            s.Inscritos.Any(p => p.Participante.CPF == param ||
                                                 p.Empresa.CNPJ == param))
                .OrderBy(o => o.Data)
                .AsNoTracking()
                .ToListAsync();    
        }

        public async Task<IEnumerable<Evento>> DoListEventByParam(string nome, string tipo, string setor, int ano)
        {
            var n = nome ?? "";
            var t = tipo ?? "";
            var o = setor ?? "";

            return await _db.Evento                
                .Include(i => i.Inscritos)   
                .Where(s => s.Nome.Contains(n))
                .Where(s => s.Tipo.Contains(t))
                .Where(s => s.Owner.Contains(o))
                .Where(s => s.Data.Value.Year == ano)          
                .AsNoTracking()
                .ToListAsync();    
        }

        public async Task<Evento> GetCodigoAsync(int codigo)
        {
            return await _db.Evento                
                .Include(i => i.Inscritos).ThenInclude(i => i.Participante)
                .Include(i => i.Inscritos).ThenInclude(i => i.Empresa)
                .Where(i => i.Codigo == codigo)
                .AsNoTracking()
                .FirstOrDefaultAsync();           
        }

        public async Task<Evento> GetCodigoParticipanteAsync(int codigo)
        {
            return await _db.Evento
                .Include(e => e.Inscritos)
                .Where(p => p.Codigo == codigo)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Evento> GetEventoToListParticipantes(int codigo)
        {
            return await _db.Evento                
                .Include(i => i.Inscritos).ThenInclude(i => i.Participante)
                .Include(i => i.Inscritos).ThenInclude(i => i.Empresa)
                .Where(i => i.Codigo == codigo)                
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Evento> GetIdAsync(Guid id)
        {
            return await _db.Evento
                .Include(i => i.Inscritos)
                .Where(u => u.Id == id)
                .OrderBy(d => d.Data)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public int LastCodigo()
        {
            var cod = _db.Evento                
                .OrderBy(c => c.Codigo)
                .AsNoTracking()
                .LastOrDefault()?.Codigo;

            if (cod == null)
                return 0;
            else
                return (int)cod;
        }

        public async Task<IEnumerable<Evento>> ListAllAsync()
        {
            return await _db.Evento                
                .Include(i => i.Inscritos).OrderBy(o => o.Data)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Evento>> ListNomeAsync(string nome)
        {
            return await _db.Evento                
                .Include(i=>i.Inscritos)
                .Where(u => u.Nome.Contains(nome))
                .OrderBy(d => d.Data)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Evento>> ListOwnerAsync(string setor)
        {
            return await _db.Evento                
                .Include(i => i.Inscritos)
                .Where(u => u.Owner.Contains(setor))
                .OrderBy(d => d.Data)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
