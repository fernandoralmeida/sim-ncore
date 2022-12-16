namespace Sim.Domain.Interface.IService
{
    using System.Linq.Expressions;
    using Entity;
    public interface IServiceStatusAtendimento : IServiceBase<StatusAtendimento>
    {
        Task<IEnumerable<StatusAtendimento>> ListUserAsync(string username);
        Task<StatusAtendimento> GetIdAsync(Guid id);
        Task<IEnumerable<StatusAtendimento>> ListAllAsync();
        Task<StatusAtendimento> MyStatusAsync(string username);
    }
}
