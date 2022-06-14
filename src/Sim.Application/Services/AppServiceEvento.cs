using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceEvento : AppServiceBase<Evento>, IAppServiceEvento
    {
        private readonly IServiceEvento _evento;
        public AppServiceEvento(IServiceEvento evento)
            :base(evento)
        {
            _evento = evento;
        }

        public async Task<Evento> GetCodigoAsync(int codigo)
        {
            return await _evento.GetCodigoAsync(codigo);
        }

        public async Task<Evento> GetCodigoParticipanteAsync(int codigo)
        {
            return await _evento.GetCodigoParticipanteAsync(codigo);
        }

        public async Task<Evento> GetIdAsync(Guid id)
        {
            return await _evento.GetIdAsync(id);
        }

        public  int LastCodigo()
        {
            return _evento.LastCodigo();
        }

        public async Task<IEnumerable<Evento>> ListAllAsync()
        {
            return await _evento.ListAllAsync();
        }

        public async Task<IEnumerable<Evento>> ListEventosAtivosAsync(IEnumerable<Evento> eventos)
        {
            return await _evento.ListEventosAtivosAsync(eventos);
        }

        public async Task<IEnumerable<Evento>> ListEventosCanceladosAsync(IEnumerable<Evento> eventos)
        {
            return await _evento.ListEventosCanceladosAsync(eventos);
        }

        public async Task<IEnumerable<Evento>> ListEventosFinalizadosAsync(IEnumerable<Evento> eventos)
        {
            return await _evento.ListEventosFinalizadosAsync(eventos);
        }

        public async Task<IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)>> ListEventosPorMesAsync(IEnumerable<Evento> eventos)
        {
            return await _evento.ListEventosPorMesAsync(eventos);
        }

        public async Task<IEnumerable<Evento>> ListNomeAsync(string nome)
        {
            return await _evento.ListNomeAsync(nome);
        }

        public async Task<IEnumerable<Evento>> ListOwnerAsync(string setor)
        {
            return await _evento.ListOwnerAsync(setor);
        }
    }
}
