using Sim.Domain.Entity;

namespace Sim.Application.Interfaces
{
    public interface IAppServiceDIA : IAppServiceBase<DIA>
    {
        Task<IEnumerable<DIA>> ListTitularAsync(string nome);
        Task<IEnumerable<DIA>> ListAuxiliarAsync(string nome);
        Task<IEnumerable<DIA>> ListAtividadeAsync(string atividade);
        Task<DIA> GetIdAsync(Guid id);
        Task<IEnumerable<DIA>> ListAllAsync();
    }
}
