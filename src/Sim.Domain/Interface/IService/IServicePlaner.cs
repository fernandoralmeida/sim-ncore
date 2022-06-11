namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServicePlaner : IServiceBase<Planner>
    {
        Task<IEnumerable<Planner>> ListDataAsync(DateTime? data);
        Task<IEnumerable<Planner>> ListPlannerAsync(DateTime? datai, DateTime? dataf, string username);
        Task<Planner> GetIdAsync(Guid id);
        Task<IEnumerable<Planner>> ListAllAsync();
    }
}
