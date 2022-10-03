namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceTipo : IServiceBase<ETipo>
    {
        Task<IEnumerable<ETipo>> ListTipoOwnerAsync(string owner);
        Task<ETipo> GetIdAsync(Guid id);
        Task<IEnumerable<ETipo>> ListAllAsync();
    }
}
