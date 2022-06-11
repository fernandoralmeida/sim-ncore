namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryPlaner : IRepositoryBase<Planner>
    {
        Task<IEnumerable<Planner>> ListDataAsync(DateTime? data);
        Task<IEnumerable<Planner>> ListPlannerAsync(DateTime? datai, DateTime? dataf, string username);
        Task<Planner> GetIdAsync(Guid id);
        Task<IEnumerable<Planner>> ListAllAsync();
    }
}
