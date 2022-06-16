using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceEvento : IAppServiceBase<Evento>
    {
        Task<IEnumerable<Evento>> ListOwnerAsync(string setor);
        Task<IEnumerable<Evento>> ListNomeAsync(string nome);
        Task<Evento> GetCodigoAsync(int codigo);
        Task<Evento> GetCodigoParticipanteAsync(int codigo);
        int LastCodigo();
        Task<IEnumerable<Evento>> ListEventosAtivosAsync(int ano);
        Task<IEnumerable<Evento>> ListEventosCanceladosAsync(int ano);
        Task<IEnumerable<Evento>> ListEventosFinalizadosAsync(int ano);
        Task<IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)>> ListEventosPorMesAsync(IEnumerable<Evento> eventos);
        Task<Evento> GetIdAsync(Guid id);
        Task<IEnumerable<Evento>> ListAllAsync();

    }
}
