using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sim.Cross.Data.Repository.Shared
{
    using Sim.Domain.Shared.Entity;
    using Sim.Domain.Shared.Interface;
    using Context;
    public class RepositoryStatusAtendimento : RepositoryBase<StatusAtendimento>, IRepositoryStatusAtendimento
    {
        public RepositoryStatusAtendimento(ApplicationContext dbContext)
            : base(dbContext)
        { }

        public async Task<IEnumerable<StatusAtendimento>> ListByUser(string username)
        {
            var t = Task.Run(() => _db.StatusAtendimento.Where(s => s.UnserName == username));
            await t;
            return t.Result;
        }
    }
}
