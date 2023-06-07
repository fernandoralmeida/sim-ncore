using System.Linq.Expressions;
using Sim.Data.Context;
using Sim.Domain.Customer.Interfaces;
using Sim.Domain.Customer.Models;
using Microsoft.EntityFrameworkCore;

namespace Sim.Data.Repository;

public class RepositoryBind : RepositoryBase<EBind>, IRepositoryBind
{
    public RepositoryBind(ApplicationContext appcontext) : base(appcontext)
    {

    }

    public async Task<IEnumerable<EBind>> DoListAsync(Expression<Func<EBind, bool>> param = null)
    {
        var _query = _db.Vinculos.AsQueryable();

        if (param != null)
            _query = _query
                 .Where(param)
                 .Include(e => e.Empresa)
                 .Include(p => p.Pessoa)
                 .AsNoTrackingWithIdentityResolution();

        return await _query.ToListAsync();
    }
}