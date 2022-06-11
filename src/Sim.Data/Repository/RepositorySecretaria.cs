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
    public class RepositorySecretaria : RepositoryBase<Secretaria>, IRepositorySecretaria
    {
        public RepositorySecretaria(ApplicationContext dbContext)
            :base(dbContext)
        {
                
        }

        public IEnumerable<Secretaria> GetByOwner(string setor)
        {
            return _db.Secretaria.Where(u => u.Owner.Contains(setor));
        }
    }
}
