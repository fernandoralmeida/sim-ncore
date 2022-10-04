using Sim.Domain.Evento.Model;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceEvento : IAppServiceBase<EEvento>
    {
        Task<IEnumerable<EEvento>> ListOwnerAsync(string setor);
        Task<IEnumerable<EEvento>> ListNomeAsync(string nome);
        Task<IEnumerable<EEvento>> DoListEventByParam(string nome, string tipo, string setor, int ano);
        Task<IEnumerable<EEvento>> DoListAsyncBy(string param);
        Task<IEnumerable<EEvento>> DoListSituacaoAsyncBy(EEvento.ESituacao situacao);
        Task<EEvento> GetCodigoAsync(int codigo);
        Task<EEvento> GetCodigoParticipanteAsync(int codigo);
        Task<EEvento> GetEventoToListParticipantes(int codigo);
        int LastCodigo();
        Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos);
        Task<EEvento> GetIdAsync(Guid id);
        Task<IEnumerable<EEvento>> ListAllAsync();

    }
}
