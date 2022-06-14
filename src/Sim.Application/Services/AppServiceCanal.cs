using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{

    public class AppServiceCanal : AppServiceBase<Canal>, IAppServiceCanal
    {
        private readonly IServiceCanal _canal;
        public AppServiceCanal(IServiceCanal canal)
            :base(canal)
        { _canal = canal; }

        public async Task<Canal> GetIdAsync(Guid id)
        {
            return await _canal.GetIdAsync(id).ConfigureAwait(false);  
        }

        public async Task<IEnumerable<Canal>> ListAllAsync()
        {
            return await _canal.ListAllAsync();
        }

        public async Task<IEnumerable<Canal>> ListCanalOwner(string setor)
        {
            return await _canal.ListCanalOwner(setor);
        }
    }
}
