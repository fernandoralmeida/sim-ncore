using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{

    public class AppServiceAmbulante : AppServiceBase<Ambulante>, IAppServiceAmbulante
    {
        private readonly IServiceAmbulante _ambulante;
        public AppServiceAmbulante(IServiceAmbulante ambulante) : base(ambulante)
        {
            _ambulante = ambulante;
        }

        public async Task<Ambulante> GetIdAsync(Guid id)
        {
            return await _ambulante.GetIdAsync(id);
        }

        public async Task<IEnumerable<Ambulante>> ListAllAsync()
        {
            return await _ambulante.ListAllAsync();
        }

        public async Task<IEnumerable<Ambulante>> ListAtividadeAsync(string atividade)
        {
            return await _ambulante.ListAtividadeAsync(atividade);
        }

        public async Task<IEnumerable<Ambulante>> ListAuxiliarAsync(string nome)
        {
            return await _ambulante.ListAuxiliarAsync(nome);
        }

        public async Task<IEnumerable<Ambulante>> ListTitularAsync(string nome)
        {
            return await _ambulante.ListTitularAsync(nome);
        }
    }
}
