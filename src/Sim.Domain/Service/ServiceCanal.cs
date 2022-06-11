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

        public Task<Canal> GetIdAsync(Guid id)
        {
            return _canal.GetIdAsync(id);
        }

        public Task<IEnumerable<Canal>> ListAllAsync()
        {
            return _canal.ListAllAsync();
        }

        public async Task<IEnumerable<Canal>> ListCanalOwner(string setor)
        {
            return await _canal.ListCanalOwner(setor);
        }
    }
}
