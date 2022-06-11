using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{
    public class ServiceAmbulante : ServiceBase<Ambulante>, IServiceAmbulante
    {
        private readonly IRepositoryAmbulante _ambulante;
        public ServiceAmbulante(IRepositoryAmbulante ambulante) : base(ambulante)
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
