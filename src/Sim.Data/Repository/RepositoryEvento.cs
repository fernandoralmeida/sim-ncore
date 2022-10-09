using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Repository;
using System.Linq;

namespace Sim.Data.Repository
{
    public class RepositoryEvento : RepositoryBase<EEvento>, IRepositoryEvento
    {
        public RepositoryEvento(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<IEnumerable<EEvento>> DoListAsyncBy(string param)
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
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();    
        }

        public async Task<IEnumerable<EEvento>> DoListEventByParam(string nome, string tipo, string setor, int ano)
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
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();    
        }

        public async Task<EEvento> GetCodigoAsync(int codigo)
        {
            return await _db.Evento                
                .Include(i => i.Inscritos).ThenInclude(i => i.Participante)
                .Include(i => i.Inscritos).ThenInclude(i => i.Empresa)
                .Where(i => i.Codigo == codigo)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync();           
        }

        public async Task<EEvento> GetCodigoParticipanteAsync(int codigo)
        {
            return await _db.Evento
                .Include(e => e.Inscritos)
                .Where(p => p.Codigo == codigo)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync();
        }

        public async Task<EEvento> GetEventoToListParticipantes(int codigo)
        {
            return await _db.Evento                
                .Include(i => i.Inscritos).ThenInclude(i => i.Participante)
                .Include(i => i.Inscritos).ThenInclude(i => i.Empresa)
                .Where(i => i.Codigo == codigo)                
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync();
        }

        public async Task<EEvento> GetIdAsync(Guid id)
        {
            return await _db.Evento
                .Include(i => i.Inscritos)
                .Where(u => u.Id == id)
                .OrderBy(d => d.Data)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync();
        }

        public int LastCodigo()
        {
            var cod = _db.Evento                
                .OrderBy(c => c.Codigo)
                .AsNoTrackingWithIdentityResolution()
                .LastOrDefault()?.Codigo;

            if (cod == null)
                return 0;
            else
                return (int)cod;
        }

        public async Task<IEnumerable<EEvento>> ListAllAsync()
        {
            return await _db.Evento                
                .Include(i => i.Inscritos).OrderBy(o => o.Data)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<EEvento>> ListNomeAsync(string nome)
        {
            return await _db.Evento                
                .Include(i=>i.Inscritos)
                .Where(u => u.Nome.Contains(nome))
                .OrderBy(d => d.Data)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }

        public async Task<IEnumerable<EEvento>> ListOwnerAsync(string setor)
        {
            return await _db.Evento                
                .Include(i => i.Inscritos)
                .Where(u => u.Owner.Contains(setor))
                .OrderBy(d => d.Data)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();
        }
    }
}
