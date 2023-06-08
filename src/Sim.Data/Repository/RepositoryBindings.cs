using System.Linq.Expressions;
using Sim.Data.Context;
using Sim.Domain.Customer.Interfaces;
using Sim.Domain.Customer.Models;
using Microsoft.EntityFrameworkCore;

namespace Sim.Data.Repository;

public class RepositoryBindings : RepositoryBase<EBindings>, IRepositoryBindings
{
    public RepositoryBindings(ApplicationContext appcontext) : base(appcontext)
    {

    }

    public async Task<IEnumerable<EBindings>> DoListAsync(Expression<Func<EBindings, bool>> param = null)
    {
        var _query = _db.Vinculos.AsQueryable();

        if (param != null)
            _query = _query
                 .Where(param)
                 .Include(e => e.Empresa)
                 .Include(p => p.Pessoa)
                 .OrderBy(o => o.Pessoa.Nome)
                 .AsNoTrackingWithIdentityResolution();
        else
            _query = _query
                .Include(p => p.Pessoa)
                .Include(e => e.Empresa)
                .OrderBy(o => o.Pessoa.Nome)
                .AsNoTrackingWithIdentityResolution();

        return await _query.ToListAsync();
    }
}