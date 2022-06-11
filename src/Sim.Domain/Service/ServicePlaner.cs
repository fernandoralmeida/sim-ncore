using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServicePlaner : ServiceBase<Planner>, IServicePlaner
    {
        private readonly IRepositoryPlaner _planer;
        public ServicePlaner(IRepositoryPlaner repositoryPlaner)
            :base(repositoryPlaner)
        { _planer = repositoryPlaner; }

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
