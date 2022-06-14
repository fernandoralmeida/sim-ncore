using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceDIA : AppServiceBase<DIA>, IAppServiceDIA
    {
        private readonly IServiceDIA _dia;
        public AppServiceDIA(IServiceDIA serviceDIA):base(serviceDIA)
        {
            _dia = serviceDIA;
        }

        public async Task<DIA> GetIdAsync(Guid id)
        {
            return await _dia.GetIdAsync(id);
        }

        public async Task<IEnumerable<DIA>> ListAllAsync()
        {
            return await _dia.ListAllAsync();
        }

        public async Task<IEnumerable<DIA>> ListAtividadeAsync(string atividade)
        {
            return await _dia.ListAtividadeAsync(atividade);
        }

        public async Task<IEnumerable<DIA>> ListAuxiliarAsync(string nome)
        {
            return await _dia.ListAuxiliarAsync(nome);
        }

        public async Task<IEnumerable<DIA>> ListTitularAsync(string nome)
        {
            return await _dia.ListTitularAsync(nome);
        }
    }
}
