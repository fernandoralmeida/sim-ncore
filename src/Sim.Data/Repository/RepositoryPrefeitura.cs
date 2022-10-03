using Microsoft.EntityFrameworkCore;
using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
namespace Sim.Data.Repository;
public class RepositoryPrefeitura : RepositoryBase<EPrefeitura>, IRepositoryPrefeitura
{
    public RepositoryPrefeitura(ApplicationContext dbcontext) : base(dbcontext)
    {
    }

    public async Task<EPrefeitura> GetByIdAsync(Guid id) {
        return await _db.Prefeitura.Include(s => s.Secretarias).FirstOrDefaultAsync(u => u.Id == id);
    }
}