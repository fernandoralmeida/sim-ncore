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
    public class RepositoryTipo : RepositoryBase<Tipo>, IRepositoryTipo
    {
        public RepositoryTipo(ApplicationContext dbContext)
            :base(dbContext)
        {
                
        }

        public IEnumerable<Tipo> GetByOwner(string owner)
        {
            return _db.Tipos.Where(u => u.Owner.Contains(owner));
        }

    }
}
