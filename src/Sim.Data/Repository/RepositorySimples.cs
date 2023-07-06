using Sim.Domain.Sebrae.Model;
using Sim.Domain.Sebrae.Interfaces;
using System.Linq.Expressions;
using Sim.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Sim.Data.Repository;

public class RepositorySimples : RepositoryBase<ESimples>, IRepositorySimples
{
    public RepositorySimples(ApplicationContext dbContext)
    : base(dbContext)
    { }

    public async Task<IEnumerable<ESimples>> DoListAsync(Expression<Func<ESimples, bool>> filter = null)
    {
        var _query = _db.Simples.AsQueryable();

        if (filter != null)
            _query = _query
                .Include(i => i.Empresa)
                .Where(filter);


        return await _query.ToListAsync();
    }
}