namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositorySecretaria : IRepositoryBase<Secretaria>
    {
        Task<IEnumerable<Secretaria>> ListSecretariaOwnerAsync(string setor);
        Task<Secretaria> GetIdAsync(Guid id);
        Task<IEnumerable<Secretaria>> ListAllAsync();
    }
}
