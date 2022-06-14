using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceInscricao : IAppServiceBase<Inscricao>
    {
        Task<Inscricao> GetInscritoAsync(Guid id);
        Task<IEnumerable<Inscricao>> ListEventoAsync(string evento);
        Task<IEnumerable<Inscricao>> ListParticipanteAsync(string nome);
        Task<IEnumerable<Inscricao>> ListTipoAsync(string evento);
        Task<Inscricao> GetIdAsync(Guid id);
        Task<IEnumerable<Inscricao>> ListAllAsync();
        bool JaInscrito(string cpf, int evento);
        int LastCodigo();
    }
}
