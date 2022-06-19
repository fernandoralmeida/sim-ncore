using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServiceCanal : ServiceBase<Canal>, IServiceCanal
    {
        private readonly IRepositoryCanal _canal;
        public ServiceCanal(IRepositoryCanal repositoryCanal)
            :base(repositoryCanal)
        { _canal = repositoryCanal; }

        public async Task<Canal> GetIdAsync(Guid id)
        {
            return await _canal.GetIdAsync(id);
        }

        public async Task<IEnumerable<Canal>> ListAllAsync()
        {
            return await _canal.ListAllAsync();
        }

        public async Task<IEnumerable<Canal>> ListCanalOwner(string setor)
        {
            return await _canal.ListCanalOwner(setor);
        }

        public async Task<IEnumerable<(string canal, string value)>> ToListJson(string setor)
        {
            var list = await ListCanalOwner(setor);
            var canallist = new List<(string canal, string value)>();

            foreach(var item in list)
            {
                canallist.Add(new() { canal = item.Nome, value = item.Nome });
            }

            return canallist;
        }
    }
}
