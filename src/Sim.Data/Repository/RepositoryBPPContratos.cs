using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.BancoPovo.Interfaces;
using Sim.Domain.BancoPovo.Models;

namespace Sim.Data.Repository;

public class RepositoryBPPContratos : RepositoryBase<EContrato>, IRepositoryContratos
{
    public RepositoryBPPContratos(ApplicationContext dbcontext) : base(dbcontext) {
    }

    public async Task<IEnumerable<EContrato>> DoListAsync(Expression<Func<EContrato, bool>> filter = null)
    {
        var _query = _db.BPPContratos.AsQueryable();

        if(filter != null)
            _query = _query
                .Where(filter)                
                .Include(i => i.Cliente)
                .Include(i => i.Empresa)
                .Include(i => i.Renegociacaoes)
                .AsNoTrackingWithIdentityResolution();

        return await _query.ToListAsync();
    }

    public async Task<EContrato> GetIdAsync(Guid id)
    {
        return await _db.BPPContratos
            .Include(i => i.Cliente)
            .Include(i => i.Empresa)
            .Include(i => i.Renegociacaoes)
            .Where(u => u.Id == id)
            .OrderBy(d => d.Data)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync();
    }
}