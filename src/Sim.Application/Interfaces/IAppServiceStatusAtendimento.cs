using System.Linq.Expressions;
using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceStatusAtendimento : IAppServiceBase<StatusAtendimento>
    {
        Task<IEnumerable<StatusAtendimento>> ListUserAsync(string username);
        Task<StatusAtendimento> GetIdAsync(Guid id);
        Task<IEnumerable<StatusAtendimento>> ListAllAsync();
        Task<StatusAtendimento> MyStatusAsync(string username);
    }
}
