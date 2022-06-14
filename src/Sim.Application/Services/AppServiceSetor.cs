using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceSetor: AppServiceBase<Setor>, IAppServiceSetor
    {
        private readonly IServiceSetor _setor;
        public AppServiceSetor(IServiceSetor setor)
            :base(setor)
        {
            _setor = setor;
        }

        public async Task<Setor> GetIdAsync(Guid id)
        {
            return await _setor.GetIdAsync(id);
        }

        public async Task<IEnumerable<Setor>> ListAllAsync()
        {
            return await _setor.ListAllAsync();
        }

        public async Task<IEnumerable<Setor>> ListSetorOwnerAsync(string secretaria)
        {
            return await _setor.ListSetorOwnerAsync(secretaria);
        }
    }
}
