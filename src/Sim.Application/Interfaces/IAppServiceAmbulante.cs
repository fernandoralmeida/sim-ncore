using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceAmbulante : IAppServiceBase<Ambulante>
    {
        Task<IEnumerable<Ambulante>> ListTitularAsync(string nome);
        Task<IEnumerable<Ambulante>> ListAuxiliarAsync(string nome);
        Task<IEnumerable<Ambulante>> ListAtividadeAsync(string atividade);
        Task<Ambulante> GetIdAsync(Guid id);
        Task<IEnumerable<Ambulante>> ListAllAsync();
    }
}
