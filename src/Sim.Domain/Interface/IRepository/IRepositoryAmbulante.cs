namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryAmbulante : IRepositoryBase<Ambulante>
    {
        Task<IEnumerable<Ambulante>> ListTitularAsync(string nome);
        Task<IEnumerable<Ambulante>> ListAuxiliarAsync(string nome);
        Task<IEnumerable<Ambulante>> ListAtividadeAsync(string atividade);
        Task<Ambulante> GetIdAsync(Guid id);
        Task<IEnumerable<Ambulante>> ListAllAsync();
    }
}
