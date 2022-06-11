namespace Sim.Domain.Interface.IRepository
{
    using Entity;
    public interface IRepositoryParceiro: IRepositoryBase<Parceiro>
    {
        Task<IEnumerable<Parceiro>> ListParceirosAsync(string owner);
        Task<Parceiro> GetIdAsync(Guid id);
        Task<IEnumerable<Parceiro>> ListAllAsync();
    }
}
