using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceParceiro : AppServiceBase<Parceiro>, IAppServiceParceiro
    {
        private readonly IServiceParceiro _parceiro;
        public AppServiceParceiro(IServiceParceiro parceiro)
            : base(parceiro)
        {
            _parceiro = parceiro;
        }

        public async Task<Parceiro> GetIdAsync(Guid id)
        {
            return await _parceiro.GetIdAsync(id);
        }

        public async Task<IEnumerable<Parceiro>> ListAllAsync()
        {
            return await _parceiro.ListAllAsync();
        }

        public async Task<IEnumerable<Parceiro>> ListParceirosAsync(string owner)
        {
            return await _parceiro.ListParceirosAsync(owner);
        }
    }
}
