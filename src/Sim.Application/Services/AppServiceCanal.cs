using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Service;

namespace Sim.Application.Services
{
    using Interfaces;

    public class AppServiceCanal : AppServiceBase<ECanal>, IAppServiceCanal
    {
        private readonly IServiceCanal _canal;
        public AppServiceCanal(IServiceCanal canal)
            :base(canal)
        { _canal = canal; }

        public async Task<ECanal> GetIdAsync(Guid id)
        {
            return await _canal.GetIdAsync(id).ConfigureAwait(false);  
        }

        public async Task<IEnumerable<ECanal>> ListAllAsync()
        {
            return await _canal.ListAllAsync();
        }

        public async Task<IEnumerable<ECanal>> ListCanalOwner(string setor)
        {
            return await _canal.ListCanalOwner(setor);
        }

        public async Task<IEnumerable<(string canal, string value)>> ToListJson(string setor)
        {
            return await _canal.ToListJson(setor);
        }
    }
}
