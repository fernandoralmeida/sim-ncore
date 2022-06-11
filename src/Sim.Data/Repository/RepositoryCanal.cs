using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository 
{ 
    public class RepositoryCanal : RepositoryBase<Canal>, IRepositoryCanal
    {
        public RepositoryCanal(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public Task<IEnumerable<Canal>> ListCanalOwner(string setor)
        {
            return Task.Run(() => _db.Canal.Where(u => u.Setor.Nome.Contains(setor) || u.Setor.Nome.Contains("Geral"));
        }
    }
}

