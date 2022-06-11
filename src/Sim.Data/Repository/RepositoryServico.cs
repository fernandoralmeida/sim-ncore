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
    public class RepositoryServico : RepositoryBase<Servico>, IRepositoryServico
    {
        public RepositoryServico(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public IEnumerable<Servico> GetByOwner(string setor)
        {
            if (setor != null)
            {
                return _db.Servico.Where(u => u.Setor.Nome.Contains(setor) || u.Setor.Nome.Contains("Geral")).OrderBy(o => o.Nome);                
            }
            else
            {
                return new List<Servico>();
            }
        }
    }

}
