namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryTipo : IRepositoryBase<ETipo>
    {
        Task<IEnumerable<ETipo>> ListTipoOwnerAsync(string owner);
        Task<ETipo> GetIdAsync(Guid id);
        Task<IEnumerable<ETipo>> ListAllAsync();
    }
}
