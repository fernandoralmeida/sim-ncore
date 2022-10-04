namespace Sim.Domain.Evento.Interfaces.Service
{
    using Model;
    public interface IServiceEvento : IServiceBase<EEvento>
    {
        Task<IEnumerable<EEvento>> ListOwnerAsync(string setor);
        Task<IEnumerable<EEvento>> ListNomeAsync(string nome);
        Task<IEnumerable<EEvento>> DoListEventByParam(string nome, string tipo, string setor, int ano);
        Task<IEnumerable<EEvento>> DoListAsyncBy(string param);
        Task<IEnumerable<EEvento>> DoListSituacaoAsyncBy(IEnumerable<EEvento> eventos, EEvento.ESituacao situacao);
        Task<EEvento> GetCodigoAsync(int codigo);
        Task<EEvento> GetCodigoParticipanteAsync(int codigo);
        Task<EEvento> GetEventoToListParticipantes(int codigo);
        int LastCodigo();
        Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos);
        Task<EEvento> GetIdAsync(Guid id);
        Task<IEnumerable<EEvento>> ListAllAsync();
    }
}
