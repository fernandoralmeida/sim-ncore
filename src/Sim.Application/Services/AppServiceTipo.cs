using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceTipo: AppServiceBase<Tipo>, IAppServiceTipo
    {
        private readonly IServiceTipo _tipo;
        public AppServiceTipo(IServiceTipo tipo)
            :base(tipo)
        {
            _tipo = tipo;
        }

        public async Task<Tipo> GetIdAsync(Guid id)
        {
            return await _tipo.GetIdAsync(id);
        }

        public async Task<IEnumerable<Tipo>> ListAllAsync()
        {
            return await _tipo.ListAllAsync();
        }

        public async Task<IEnumerable<Tipo>> ListTipoOwnerAsync(string owner)
        {
            return await _tipo.ListTipoOwnerAsync(owner);
        }
    }
}
