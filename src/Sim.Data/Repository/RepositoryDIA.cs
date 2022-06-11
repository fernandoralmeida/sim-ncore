using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    public class RepositoryDIA : RepositoryBase<DIA>, IRepositoryDIA
    {
        public RepositoryDIA(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<IEnumerable<DIA>> ListAtividadeAsync(string atividade)
        {
            return await Task.Run(() => _db.DIA.Where(c => c.Ambulante.Atividade.Contains(atividade)));
        }

        public async Task<IEnumerable<DIA>> ListAuxiliarAsync(string nome)
        {
            return await Task.Run(() => _db.DIA.Where(c => c.Ambulante.Pessoas.FirstOrDefault().Nome.Contains(nome)));
        }

        public async Task<IEnumerable<DIA>> ListTitularAsync(string nome)
        {
            return await Task.Run(() => _db.DIA.Where(c => c.Ambulante.Pessoas.FirstOrDefault().Nome.Contains(nome)));
        }
    }
}
