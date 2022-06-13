using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryPlaner : RepositoryBase<Planner>, IRepositoryPlaner
    {
        public RepositoryPlaner(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public Task<Planner> GetIdAsync(Guid id)
        {
            return _db.Planner.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Planner>> ListAllAsync()
        {
            return await _db.Planner.ToListAsync();
        }

        public async Task<IEnumerable<Planner>> ListDataAsync(DateTime? data)
        {
            return await _db.Planner.Where(u => u.DataInicial == data).ToListAsync();
        }

        public async Task<IEnumerable<Planner>> ListPlannerAsync(DateTime? datai, DateTime? dataf, string username)
        {
            return await _db.Planner
                .Where(s => s.DataInicial.Value.Date == datai
                && s.DataFinal.Value.Date == dataf
                && s.Owner_AppUser_Id == username)
                .OrderBy(o => o.DataInicial).ToListAsync();
        }
    }
}
