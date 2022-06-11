using Sim.Data.Context;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;

namespace Sim.Data.Repository
{
    class RepositoryAmbulante : RepositoryBase<Ambulante>, IRepositoryAmbulante
    {
        public RepositoryAmbulante(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public async Task<IEnumerable<Ambulante>> ListAtividadeAsync(string atividade)
        {
            var t = Task.Run(() => _db.Ambulante.Where(c => c.Atividade.Contains(atividade)));

            return await t;
        }

        public async Task<IEnumerable<Ambulante>> ListAuxiliarAsync(string nome)
        {
            var t = Task.Run(() => _db.Ambulante.Where(c => c.Pessoas.FirstOrDefault().Nome.Contains(nome)));
            return await t;
        }

        public async Task<IEnumerable<Ambulante>> ListTitularAsync(string nome)
        {
            var t = Task.Run(() => _db.Ambulante.Where(c => c.Pessoas.FirstOrDefault().Nome.Contains(nome)));
            return await t;
        }
    }
}
