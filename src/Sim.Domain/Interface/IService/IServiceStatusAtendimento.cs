namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceStatusAtendimento : IServiceBase<StatusAtendimento>
    {
        Task<IEnumerable<StatusAtendimento>> ListUserAsync(string username);
        Task<StatusAtendimento> GetIdAsync(Guid id);
        Task<IEnumerable<StatusAtendimento>> ListAllAsync();
    }
}
