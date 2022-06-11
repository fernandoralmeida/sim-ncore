namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryTipo : IRepositoryBase<Tipo>
    {
        Task<IEnumerable<Tipo>> ListTipoOwnerAsync(string owner);
        Task<Tipo> GetIdAsync(Guid id);
        Task<IEnumerable<Tipo>> ListAllAsync();
    }
}
