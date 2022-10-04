namespace Sim.Domain.Evento.Interfaces.Repository
{
    using Model;
    public interface IRepositoryTipo : IRepositoryBase<ETipo>
    {
        Task<IEnumerable<ETipo>> ListTipoOwnerAsync(string owner);
        Task<ETipo> GetIdAsync(Guid id);
        Task<IEnumerable<ETipo>> ListAllAsync();
    }
}
