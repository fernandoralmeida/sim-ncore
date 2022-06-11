namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceSecretaria : IServiceBase<Secretaria>
    {
        Task<IEnumerable<Secretaria>> ListSecretariaOwnerAsync(string setor);
        Task<Secretaria> GetIdAsync(Guid id);
        Task<IEnumerable<Secretaria>> ListAllAsync();
    }
}
