namespace Sim.Domain.Evento.Interfaces.Repository
{
    using Model;
    public interface IRepositoryEvento : IRepositoryBase<EEvento>
    {
        Task<IEnumerable<EEvento>> ListOwnerAsync(string setor);
        Task<IEnumerable<EEvento>> ListNomeAsync(string nome);
        Task<IEnumerable<EEvento>> DoListEventByParam(string nome, string tipo, string setor, int ano);
        Task<IEnumerable<EEvento>> DoListAsyncBy(string param);
        Task<EEvento> GetCodigoAsync(int codigo);
        Task<EEvento> GetEventoToListParticipantes(int codigo);
        Task<EEvento> GetCodigoParticipanteAsync(int codigo);
        Task<EEvento> GetIdAsync(Guid id);
        Task<IEnumerable<EEvento>> ListAllAsync();
        int LastCodigo();
    }
}
