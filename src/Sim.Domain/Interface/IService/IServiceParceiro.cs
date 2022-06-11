namespace Sim.Domain.Interface.IService
{
    using Entity;
    public interface IServiceParceiro : IServiceBase<Parceiro>
    {
        Task<IEnumerable<Parceiro>> ListParceirosAsync(string owner);
        Task<Parceiro> GetIdAsync(Guid id);
        Task<IEnumerable<Parceiro>> ListAllAsync();
    }
}
