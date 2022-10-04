using Sim.Domain.Organizacao.Model;
using Sim.Domain.Organizacao.Interfaces.Service;

namespace Sim.Application.Services
{
    using Interfaces;
    public class AppServiceParceiro : AppServiceBase<EParceiro>, IAppServiceParceiro
    {
        private readonly IServiceParceiro _parceiro;
        public AppServiceParceiro(IServiceParceiro parceiro)
            : base(parceiro)
        {
            _parceiro = parceiro;
        }

        public async Task<EParceiro> GetIdAsync(Guid id)
        {
            return await _parceiro.GetIdAsync(id);
        }

        public async Task<IEnumerable<EParceiro>> ListAllAsync()
        {
            return await _parceiro.ListAllAsync();
        }

        public async Task<IEnumerable<EParceiro>> ListParceirosAsync(string owner)
        {
            return await _parceiro.ListParceirosAsync(owner);
        }
    }
}
