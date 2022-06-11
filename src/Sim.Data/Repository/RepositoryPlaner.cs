using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Cross.Data.Repository.Shared
{
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.Shared.Interface;
    using Context;
    public class RepositoryPlaner : RepositoryBase<Planner>, IRepositoryPlaner
    {
        public RepositoryPlaner(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public IEnumerable<Planner> GetByData(DateTime? data)
        {
            return _db.Planner.Where(u => u.DataInicial == data);
        }

        public async Task<IEnumerable<Planner>> GetMyPlanner(DateTime? datai, DateTime? dataf, string username)
        {
            var t = Task.Run(() => _db.Planner.Where(s => s.DataInicial.Value.Date == datai && s.DataFinal.Value.Date == dataf && s.Owner_AppUser_Id == username).OrderBy(o => o.DataInicial));
            await t;
            return t.Result;
        }
    }
}
