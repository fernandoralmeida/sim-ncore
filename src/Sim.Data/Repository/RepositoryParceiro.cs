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


    public class RepositoryParceiro : RepositoryBase<Parceiro>, IRepositoryParceiro
    {
        public RepositoryParceiro(ApplicationContext applicationContext)
            : base(applicationContext)
        {

        }

        public async Task<IEnumerable<Parceiro>> ListParceiros(string owner)
        {
            var t = Task.Run(() => _db.Parceiro.Include(s=>s.Secretaria).Where(u => u.Secretaria.Nome.Contains(owner)));
            await t;
            return t.Result;
        }
    }
}
