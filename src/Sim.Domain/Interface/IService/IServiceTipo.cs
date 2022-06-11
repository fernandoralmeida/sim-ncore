namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceTipo : IServiceBase<Tipo>
    {
        Task<IEnumerable<Tipo>> ListTipoOwnerAsync(string owner);
        Task<Tipo> GetIdAsync(Guid id);
        Task<IEnumerable<Tipo>> ListAllAsync();
    }
}
