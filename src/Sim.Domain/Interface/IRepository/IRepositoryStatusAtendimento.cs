namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryStatusAtendimento : IRepositoryBase<StatusAtendimento>
    {
        Task<IEnumerable<StatusAtendimento>> ListUserAsync(string username);
        Task<StatusAtendimento> GetIdAsync(Guid id);
        Task<IEnumerable<StatusAtendimento>> ListAllAsync();
    }
}
