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
    public class RepositorySetor : RepositoryBase<Setor>, IRepositorySetor
    {
        public RepositorySetor(ApplicationContext dbContext)
            :base(dbContext)
        {
                
        }

        public IEnumerable<Setor> GetByOwner(string secretaria)
        {
            return _db.Setor.Where(u => u.Secretaria.Nome.Contains(secretaria));
        }
    }
}
