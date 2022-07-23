namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryEvento : IRepositoryBase<Evento>
    {
        Task<IEnumerable<Evento>> ListOwnerAsync(string setor);
        Task<IEnumerable<Evento>> ListNomeAsync(string nome);
        Task<Evento> GetCodigoAsync(int codigo);
        Task<Evento> GetEventoToListParticipantes(int codigo);
        Task<Evento> GetCodigoParticipanteAsync(int codigo);
        Task<Evento> GetIdAsync(Guid id);
        Task<IEnumerable<Evento>> ListAllAsync();
        int LastCodigo();
    }
}
