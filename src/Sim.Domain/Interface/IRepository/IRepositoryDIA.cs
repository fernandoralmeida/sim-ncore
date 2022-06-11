namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryDIA : IRepositoryBase<DIA>
    {
        Task<IEnumerable<DIA>> ListTitularAsync(string nome);
        Task<IEnumerable<DIA>> ListAuxiliarAsync(string nome);
        Task<IEnumerable<DIA>> ListAtividadeAsync(string atividade);
        Task<DIA> GetIdAsync(Guid id);
        Task<IEnumerable<DIA>> ListAllAsync();
    }
}
