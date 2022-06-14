using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServicePlaner : IAppServiceBase<Planner>
    {
        Task<IEnumerable<Planner>> ListDataAsync(DateTime? data);
        Task<IEnumerable<Planner>> ListPlannerAsync(DateTime? datai, DateTime? dataf, string username);
        Task<Planner> GetIdAsync(Guid id);
        Task<IEnumerable<Planner>> ListAllAsync();
    }
}
