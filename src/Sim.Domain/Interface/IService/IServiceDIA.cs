namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceDIA : IServiceBase<DIA>
    {
        Task<IEnumerable<DIA>> ListTitularAsync(string nome);
        Task<IEnumerable<DIA>> ListAuxiliarAsync(string nome);
        Task<IEnumerable<DIA>> ListAtividadeAsync(string atividade);
        Task<DIA> GetIdAsync(Guid id);
        Task<IEnumerable<DIA>> ListAllAsync();
    }
}
