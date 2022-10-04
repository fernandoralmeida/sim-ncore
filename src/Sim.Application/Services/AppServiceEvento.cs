using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Service;

namespace Sim.Application.Services
{
    using Interfaces;
    public class AppServiceEvento : AppServiceBase<EEvento>, IAppServiceEvento
    {
        private readonly IServiceEvento _evento;
        public AppServiceEvento(IServiceEvento evento)
            :base(evento)
        {
            _evento = evento;
        }

        public async Task<IEnumerable<EEvento>> DoListAsyncBy(string param)
        {            
            return await _evento.DoListAsyncBy(param);
        }

        public async Task<IEnumerable<EEvento>> DoListEventByParam(string nome, string tipo, string setor, int ano)
        {
            return await _evento.DoListEventByParam(nome, tipo, setor, ano);
        }

        public async Task<IEnumerable<EEvento>> DoListSituacaoAsyncBy(EEvento.ESituacao situacao)
        {
            return await _evento.DoListSituacaoAsyncBy(await ListAllAsync(), situacao);
        }

        public async Task<EEvento> GetCodigoAsync(int codigo)
        {
            return await _evento.GetCodigoAsync(codigo);
        }

        public async Task<EEvento> GetCodigoParticipanteAsync(int codigo)
        {
            return await _evento.GetCodigoParticipanteAsync(codigo);
        }

        public async Task<EEvento> GetEventoToListParticipantes(int codigo)
        {
            return await _evento.GetEventoToListParticipantes(codigo);
        }

        public async Task<EEvento> GetIdAsync(Guid id)
        {
            return await _evento.GetIdAsync(id);
        }

        public  int LastCodigo()
        {
            return _evento.LastCodigo();
        }

        public async Task<IEnumerable<EEvento>> ListAllAsync()
        {
            return await _evento.ListAllAsync();
        }

        public async Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos)
        {
            return await _evento.ListEventosPorMesAsync(eventos);
        }

        public async Task<IEnumerable<EEvento>> ListNomeAsync(string nome)
        {
            return await _evento.ListNomeAsync(nome);
        }

        public async Task<IEnumerable<EEvento>> ListOwnerAsync(string setor)
        {
            return await _evento.ListOwnerAsync(setor);
        }
    }
}
