using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServicePlaner : AppServiceBase<Planner>, IAppServicePlaner
    {
        private readonly IServicePlaner _planer;
        public AppServicePlaner(IServicePlaner planer)
            :base(planer)
        { _planer = planer; }

        public async Task<Planner> GetIdAsync(Guid id)
        {
            return await _planer.GetIdAsync(id);
        }

        public async Task<IEnumerable<Planner>> ListAllAsync()
        {
            return await _planer.ListAllAsync();
        }

        public async Task<IEnumerable<Planner>> ListDataAsync(DateTime? data)
        {
            return await _planer.ListDataAsync(data);
        }

        public async Task<IEnumerable<Planner>> ListPlannerAsync(DateTime? datai, DateTime? dataf, string username)
        {
            return await _planer.ListPlannerAsync(datai, dataf, username);
        }
    }
}
